using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models
{
    public class Hole : Landmark
    {
        public XYPosition Position { get; set; }

        public Hole(XYPosition position) {
           Position = position;
        
        }

    }
}
