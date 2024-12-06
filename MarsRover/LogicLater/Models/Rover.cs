using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums; 

namespace MarsRover.LogicLater.Models
{
    public class Rover
    {
        public static ulong RoverCounter { get; set; }
        public ulong Id { get; init; }
        public Position Position { get; set; }

        public Boolean IsIntact { get; set; }

        public Rover(Position position)
        {
            Position = position;
            Id = RoverCounter;
            IsIntact = true;
            RoverCounter++; 
        }

        public void RotateRover(Instructions instruction)
        {
            if (instruction == Instructions.M) return;

            int rotateBy = instruction == Instructions.R ? 1 : -1;
            int shiftPosition = (int)Position.Direction + rotateBy;

            if (shiftPosition == -1) shiftPosition = 3;
            if (shiftPosition == 4) shiftPosition = 0;

            Position.Direction = (Facing)(shiftPosition);
        }
        

        public void MoveRover()
        {
            int[,] _positionConversion = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
            Position.x += _positionConversion[(int)Position.Direction, 0];
            Position.y += _positionConversion[(int)Position.Direction, 1];
        }

    }
}
