using System;
using System.Collections.Generic;
using System.Linq;
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
            return _setup.Switches[0].Name;

        }

        [HttpGet]
        public string SetSwitch()
        {
            return "success";

        }
    }
}