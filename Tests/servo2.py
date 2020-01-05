import RPi.GPIO as gpio
import time
import numpy as np
# Servo-GPIO (PWM-GPIO 18, Pin 12)
servopin = 2
pmwClosedToOpen = np.linspace(12.5,8.5,80)
pmwOpenToClosed = np.linspace(8.5,12.5,80)
cur = "closed"

# GPIO initialisieren
gpio.setmode(gpio.BCM)
gpio.setup(servopin, gpio.OUT)

# PWM-Frequenz auf 50 Hz setzen
servo = gpio.PWM(servopin, 50)

# PWM starten, Servo auf 180 Grad
servo.start(12.5)

# Umrechnung Grad in Tastverhaeltnis
def setservo():
    global cur

    if cur == "closed":
        cur = "open"
        for val in pmwClosedToOpen:
            time.sleep(0.015)
            servo.start(val)
    else:
        cur = "closed"
        for val in pmwOpenToClosed:
            servo.start(val)
            time.sleep(0.01)


try:
  # Endlosschleife Servoansteuerung
  while True:
    winkel = raw_input("trigger")
    setservo()
       
except KeyboardInterrupt:
    servo.ChangeDutyCycle(2.5)
    servo.stop()
    gpio.cleanup()
