using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models
{
    public class ChargingStation
    {
        public PositionCheck Position { get; set; }

        public Boolean IsUsed { get; set; } = false; 

        public ChargingStation(PositionCheck position) {
           Position = position;
        
        }

    }
}
