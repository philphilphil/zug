using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public interface ISetup
    {
        List<Switch> Switches { get; set; }
    }
}
