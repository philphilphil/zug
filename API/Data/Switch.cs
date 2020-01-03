using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Switch
    {
        //Number of the RPI Pin which is connected to the cable which sets the switch to the default (streight) route
        public int RPI_Pin1 { get; set; }

        //Number of the RPI Pin which is connected to the cable which sets the switch to the curve
        public int RPI_Pin2 { get; set; }

        //for internal usage and help to keep everything in order
        public string Name { get; set; }

        public int Id { get; set; }

        public Type Type { get; set; }

        public Pin CurrentState { get; set; }
    }

    //Fleischmann switches and theyr artno
    public enum Type
    {
        Left = 9170,
        Right = 9171,
        CurvedRight = 9175,
        CurvedLeft = 9174,
        Multiple = 9999
    }

    public enum Pin
    {
        Pin1 = 1,
        Pin2 = 2
    }
}
