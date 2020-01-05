#!flask/bin/python
from flask import Flask
import RPi.GPIO as GPIO
import time

app = Flask(__name__)
GPIO.setmode(GPIO.BCM)
GPIO.setup(18, GPIO.OUT)
GPIO.output(18, GPIO.HIGH)

### Switches
@app.route('/Switch/SetAllToDefault', methods=['GET'])
def switch_AllToDefault():
    GPIO.output(18, GPIO.LOW)
    time.sleep(0.5)
    GPIO.output(18, GPIO.HIGH)
    GPIO.
    return "done"
 
@app.route('/Switch/SetAllToDefault', methods=['GET'])
def switch_AllToDefault(): 


if __name__ == '__main__':
    app.run(debug=True, host= '0.0.0.0')
    
