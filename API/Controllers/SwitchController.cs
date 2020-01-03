using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ControllerApi.Classes;
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
        public string ToogleSwitch(int id)
        {
            string debug = "";
            var s = GetSwitch(id);

            //debug - skip RPI part
            //return "true";
            debug += "State" + s.CurrentState.ToString() + " ";
            var pinToToggle = 0;
            debug += pinToToggle.ToString();

            if (s.CurrentState == Pin.Pin1)
            {
                pinToToggle = s.RPI_Pin2;
                s.CurrentState = Pin.Pin2;
            }
            else
            {
                pinToToggle = s.RPI_Pin1;
                s.CurrentState = Pin.Pin1;
            }
            debug += pinToToggle.ToString() + " ";
            Raspi.TogglePin(pinToToggle);
            debug += "State" + s.CurrentState.ToString() + " ";
            return debug;

        }

        [HttpGet]
        public string SetSwitch(int id, bool high)
        {
            var s = GetSwitch(id);

            var value = PinValue.Low;
            if (high) value = PinValue.High;

            Raspi.SetPin(id, value);

            return s.Name;
        }

        [HttpGet]
        [Route("SetAllSwitchesToDefault")]
        public string SetAllSwitchesToDefault()
        {
            foreach (var s in _setup.Switches)
            {
                Raspi.TogglePin(s.RPI_Pin1);
                s.CurrentState = Pin.Pin1;
            }

            return "success";
        }

        private Switch GetSwitch(int id)
        {
            var s = _setup.Switches.Where(x => x.Id == id).ToList();
            if (s.Count != 1) throw new Exception("None or multiple switches with id " + id + " found.");

            return s[0];
        }
    }
}