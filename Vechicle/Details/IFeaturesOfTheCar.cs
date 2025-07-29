using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace async.Vechicle.Details
{
    internal interface IFeaturesOfTheCar
    {
        string Name { get; }
        Task LogicCheckFuel();
        Task LugicCheckCurrentSpeed();
        Task ReduceSpeed();
        Task IncreaseSpeed();
    }
}
