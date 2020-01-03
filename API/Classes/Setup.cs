using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Setup : ISetup
    {
        //this is the setup for all the switches used in my build
        public List<Switch> Switches { get; set; }

        public Setup()
        {
            SetupSwitches();
        }

        private void SetupSwitches()
        {
            this.Switches = new List<Switch>
            {
                new Switch { Name = "Gleis 1/2", RPI_Pin1 = 18, RPI_Pin2 = 23, Id = 1, Type = Type.Left },
                new Switch { Name = "Werkstatt / Gleis 1", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 2, Type = Type.Left },
                new Switch { Name = "Schleife / Werkstatt", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 3, Type = Type.Left },
                new Switch { Name = "Werkstatt 1/2", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 4, Type = Type.Left },
                new Switch { Name = "Gleis 2 / 3", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 5, Type = Type.Multiple },
                new Switch { Name = "Schleife / Gleis 1/2", RPI_Pin1 = 12, RPI_Pin2 = 12, Id = 6, Type = Type.Multiple }
            };
        }
    }
}
