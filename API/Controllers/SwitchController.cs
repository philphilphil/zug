using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchController : ControllerBase
    {
        private readonly ISetup _setup;
        public SwitchController(ISetup setup)
        {
            _setup = setup;
        }

        [HttpGet]
        [Route("ToggleSwitch")]
        public string ToogleSwitch()
        {
            GpioController controller = new GpioController();

            // GPIO 17 which is physical pin 11
            int ledPin1 = 17;
            // Sets the pin to output mode so we can switch something on
            controller.OpenPin(ledPin1, PinMode.Output);

            // turn on the LED
            controller.Write(ledPin1, PinValue.High);

            //wait 500ms and turn off (no need for power on the switch after the switching operation)
            //Thread.Sleep(500);
            //controller.Write(ledPin1, PinValue.Low);

            return _setup.Switches[0].Name;

        }

        [HttpGet]
        public string SetSwitch()
        {
            GpioController controller = new GpioController();
            // GPIO 17 which is physical pin 11
            int ledPin1 = 17;
            // Sets the pin to output mode so we can switch something on
            controller.OpenPin(ledPin1, PinMode.Output);

            // turn on the LED
            controller.Write(ledPin1, PinValue.Low);

            return _setup.Switches[0].Name;
        }
    }
}