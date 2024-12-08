using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums; 

namespace MarsRover.LogicLayer.Models
{
    public class Rover : Vehicle 
    {
        public static ulong RoverCounter { get; set; }
        
        public ulong Id { get; init; }

        public XYPosition Position { get; set; }

        public Facing Direction { get; set; }

        public Boolean IsIntact { get; set; }

        public Rover(XYPosition position, Facing direction)
        {
            Position = position;
            Direction = direction; 
            Id = RoverCounter;
            IsIntact = true;
            RoverCounter++; 
        }

        public void RotateRover(Instructions instruction)
        {
            if (instruction == Instructions.M) return;

            int rotateBy = instruction == Instructions.R ? 1 : -1;
            int shiftPosition = (int)Direction + rotateBy;

            if (shiftPosition == -1) shiftPosition = 3;
            if (shiftPosition == 4) shiftPosition = 0;

            Direction = (Facing)(shiftPosition);
        }


        public void MoveRover()
        {
            int[,] _positionConversion = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
            int newXAxis = Position.xAxis + _positionConversion[(int)Direction, 0];
            int newYAxis = Position.yAxis + _positionConversion[(int)Direction, 1];
            Position = (newXAxis, newYAxis);
        }

        public override string ToString()
        {
            return $"Rover {Id} is at ({Position.xAxis}, {Position.yAxis}) facing {Direction}";
        }

    }
}
