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

            //set pin to value, relay triggers on low!
            controller.Write(pinId, PinValue.Low);

            //wait 100ms and turn RELAY off (which is pin high)
            Thread.Sleep(100);
            controller.Write(pinId, PinValue.High);

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
