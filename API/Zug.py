#!flask/bin/python
from flask import *
import RPi.GPIO as GPIO
import time
import numpy as np
from DataClasses import *
from collections import namedtuple

app = Flask(__name__)
switches = []
servos = []
leds = []

### Frontend
@app.route('/control')
def control():
    return send_file("static/Index.html")
    
### Switches API
@app.route('/Switch/AllToDefault', methods=['GET'])
def switch_all_to_default():
    for s in switches:
        toggle_rpi_pin(s.rpiPinStraight)
        set_rpi_pin_high(s.rpiPinDiverging)
        s.state= 'straight'
    return "done"
 
@app.route('/Switch/Toggle/<int:id>', methods=['GET'])
def switch_toggle(id):
    global switches
    s = get_switch(id)
    
    if not s:
        return "No switch found"
    
    outputPin = 0
    if s.state == 'straight':
        outputPin = s.rpiPinDiverging
        s.state= 'diverging'
    else:
        outputPin = s.rpiPinStraight
        s.state= 'straight'
    
    #print("Pin:" , outputPin, s)
    toggle_rpi_pin(outputPin)
   
    return s.state

### Servo API
@app.route('/Servo/Toggle/<int:id>', methods=['GET'])
def servo_toggle(id):
    global servos
    s = get_servo(id)
    move_servo(s)

    return s.state

@app.route('/Servo/AllToDefault', methods=['GET'])
def servo_all_to_default():
    for s in servos:
        s.state = 'open'
        move_servo(s)
    return "done"

### Setup
def base_setup():
    global switches, servos
    GPIO.setmode(GPIO.BCM)
    GPIO.setwarnings(False)

    switches.append(Switch('Gleis 1/2', 1, 4, 3, 'straight'))  
    switches.append(Switch('Werkstatt / Gleis 2', 2, 27, 17, 'straight'))  
    switches.append(Switch('Schleife / Werkstatt', 3, 10, 22, 'straight'))  
    switches.append(Switch('Werkstatt 1/2', 4, 9, 11, 'straight'))  
    switches.append(Switch('Gleis 2 / 3', 5, 25, 24, 'straight'))  
    switches.append(Switch('Schleife / Gleis 1/2', 6, 8, 7, 'straight'))  

    servos.append(Servo('Lokschuppen', 1, 2, np.linspace(12.5,8.5,200), np.linspace(8.5,12.5,200), 'closed'))
    
    #currently not used RPI Pins
    set_rpi_pin_high(12)
    set_rpi_pin_high(16)
    set_rpi_pin_high(20)
    set_rpi_pin_high(21)

### Helper Methods
def get_switch(id):
    for s in switches:
        #print(s)
        if s.id == id:
            return s   
    return

def get_servo(id):
    for s in servos:
        #print(s)
        if s.id == id:
            return s   
    return

def toggle_rpi_pin(id):
    GPIO.setup(id, GPIO.OUT)
    GPIO.output(id, GPIO.LOW)
    time.sleep(0.1)
    GPIO.output(id, GPIO.HIGH)
    
def set_rpi_pin_high(id):
    GPIO.setup(id, GPIO.OUT)
    GPIO.output(id, GPIO.HIGH)

def move_servo(s):
    GPIO.setup(s.rpiPin, GPIO.OUT)
    servo = GPIO.PWM(s.rpiPin, 50)

    if s.state == "closed":
        s.state = "open"
        for val in s.pmwClosedToOpen:
            time.sleep(0.015)
            servo.start(val)
    else:
        s.state = "closed"
        for val in s.pmwOpenToClosed:
            servo.start(val)
            time.sleep(0.01)
### Main
if __name__ == '__main__':
    base_setup()
    switch_all_to_default()
    servo_all_to_default()
    app.run(debug=True, host= '0.0.0.0')