using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;

namespace MarsRover.LogicLater.Models
{

    public class Position
    {

        public int x { get; set; }

        public int y { get; set; }

        public Facing Direction { get; set; }

        public Position(int xAxis, int yAxis, Facing direction)
        {
            x = xAxis;
            y = yAxis;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{this.x}, {this.y} facing {this.Direction}";
        }

    }
}
