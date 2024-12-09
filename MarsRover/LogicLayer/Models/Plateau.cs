using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models
{
    public class Plateau
    {
        public int _x { get; init; }

        public int _y { get; init; }

        public int _z { get; init; } 

        public Plateau(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Boolean IsPositionInRange(XYPosition xyPosition)
        {
            if ((xyPosition.xAxis >= _x) || (xyPosition.yAxis >= _y) || (xyPosition.xAxis < 0) || (xyPosition.yAxis < 0))
            {
                return false;
            }
            return true;
        }

    }
}
