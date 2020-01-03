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

        [HttpGet]
        [Route("ToggleSwitch")]
        public string ToogleSwitch(int id)
        {
            string debug = "";
            using (var db = new ApplicationDbContext())
            {

                var s = GetSwitch(id, db);

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

                db.SaveChanges();
            }

            return debug;

        }

        //[HttpGet]
        //public string SetSwitch(int id, bool high)
        //{
        //    var s = GetSwitch(id);

        //    var value = PinValue.Low;
        //    if (high) value = PinValue.High;

        //    Raspi.SetPin(id, value);

        //    return s.Name;
        //}

        [HttpGet]
        [Route("SetupSwitches")]
        public string SetupSwitches() //Todo: refactor to allow setup of new switches via frontend
        {
            List<Switch> Switches = new List<Switch>{
                new Switch { Name = "Gleis 1/2", RPI_Pin1 = 18, RPI_Pin2 = 23, Id = 1, Type = Type.Left },
                new Switch { Name = "Werkstatt / Gleis 1", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 2, Type = Type.Left },
                new Switch { Name = "Schleife / Werkstatt", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 3, Type = Type.Left },
                new Switch { Name = "Werkstatt 1/2", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 4, Type = Type.Left },
                new Switch { Name = "Gleis 2 / 3", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 5, Type = Type.Multiple },
                new Switch { Name = "Schleife / Gleis 1/2", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 6, Type = Type.Multiple }
                 };

            using (var db = new ApplicationDbContext())
            {
                foreach (var s in Switches)
                {
                    if (!db.Switches.Where(x => x.Id == s.Id).Any())
                    {
                        db.Switches.Add(s);
                    }
                }

                db.SaveChanges();
            }

            return "done";
        }

        [HttpGet]
        [Route("SetAllSwitchesToDefault")]
        public string SetAllSwitchesToDefault()
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var s in db.Switches)
                {
                    Raspi.TogglePin(s.RPI_Pin1);
                    s.CurrentState = Pin.Pin1;
                }
                db.SaveChanges();
            }
            return "success";
        }

        private Switch GetSwitch(int id, ApplicationDbContext db)
        {

            var s = db.Switches.Where(x => x.Id == id).ToList();
            if (s.Count != 1) throw new Exception("None or multiple switches with id " + id + " found.");
            return s[0];


        }


    }
}