using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models
{
    public class ChargingStation
    {
        public XYPosition Position { get; set; }

        public Boolean IsUsed { get; set; } = false; 

        public ChargingStation(XYPosition position) {
           Position = position;
        
        }

    }
}
