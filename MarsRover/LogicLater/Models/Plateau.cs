using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLater.Models
{
    public class Plateau
    {
        public int _x { get; init; }

        public int _y { get; init; }

        public char[,] Grid { get; init; }

        public Plateau(int x, int y)
        {
            _x = x;
            _y = y;
            Grid = MakeGrid(x + 1, y + 1);
        }

        private static char[,] MakeGrid(int x, int y)
        {
            char[,] myGrid = new char[x, y];
            for (int rows = 0; rows < x; rows++)
            {
                for (int cols = 0; cols < y; cols++)
                {
                    myGrid[rows, cols] = '_';
                }
            }
            return myGrid;
        }

    }
}
