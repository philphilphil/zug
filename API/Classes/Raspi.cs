using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ControllerApi.Classes
{
    public static class Raspi
    {
        public static void TogglePin(int pinId)
        {
            GpioController controller = new GpioController();

            // Sets the pin to output mode
            controller.OpenPin(pinId, PinMode.Output);

            //set pin to value
            controller.Write(pinId, PinValue.High);

            //wait 500ms and turn off (no need for power on the switch after the switching operation) and 
            Thread.Sleep(500);
            controller.Write(pinId, PinValue.Low);

            //close pin again
            controller.ClosePin(pinId);

        }

        public static void SetPin(int pinId, PinValue value)
        {
            GpioController controller = new GpioController();

            // Sets the pin to output mode
            controller.OpenPin(pinId, PinMode.Output);

            //set pin to value
            controller.Write(pinId, value);
        }
    }
}
