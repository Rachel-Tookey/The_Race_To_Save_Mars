using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;

namespace MarsRover.Models
{

    public class Position
    {
        private readonly int[,] _positionConversion = { { 0, 1 },
            {0, -1 },
            { 1, 0},
            { -1, 0 }
        }; 

        public int x {  get; set; }

        public int y { get; set; }  

        public Facing Direction { get; set; }

        public Position(int xAxis, int yAxis, Facing direction)
        {
            x = xAxis;
            y = yAxis;
            Direction = direction;
        }

    }
}
