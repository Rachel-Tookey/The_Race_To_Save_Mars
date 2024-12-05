using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class Plateau
    {
        public int _x {  get; init; }

        public int _y { get; init; }

        public char[,] Grid { get; init; }

        public Plateau(int x, int y)
        {
            _x = x;
            _y = y;
            Grid = new char[x, y];
        }
        
    }
}
