using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;
using PositionCheck = (int xAxis, int yAxis); 

namespace MarsRover.LogicLater.Models
{
    public class MissionControl
    {
        public Plateau Plateau { get; set; }

        public List<Rover> Rovers { get; set; } = new List<Rover>();

        public MissionControl(Plateau plateau)
        {
            Plateau = plateau;
        }

        public void AddRover(Rover rover)
        {
            Rovers.Add(rover);
        }


        public PositionCheck ReturnNewPosition(Rover roverToMove)
        {
            int[,] _positionConversion = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            int futureX = roverToMove.Position.x + _positionConversion[(int)roverToMove.Position.Direction, 0];
            int futureY = roverToMove.Position.y + _positionConversion[(int)roverToMove.Position.Direction, 1];
            return (futureX, futureY);
        }


        public void RunInstructions(Rover roverToMove, List<Instructions> listOfMoves)
        {
            foreach (Instructions move in listOfMoves) { 
                if ((move == Instructions.L) || (move == Instructions.R))
                {
                    roverToMove.RotateRover(move);
                } else
                {
                    PositionCheck futurePosition = ReturnNewPosition(roverToMove);
                    Boolean CheckPosition = IsPositionEmpty(futurePosition.xAxis, futurePosition.yAxis);
                    Boolean CheckBounds = IsPositionInRange(futurePosition.xAxis, futurePosition.yAxis);
                    if (CheckPosition && CheckBounds)
                    {
                        roverToMove.MoveRover();
                    }
                }
                        

            }

        }



        public Boolean IsPositionEmpty(int x, int y)
        {
            foreach (Rover rover in Rovers)
            {
                if ((rover.Position.x == x) && (rover.Position.y == y)) {
                    return false; 
                }
            }
            return true; 
        }

        public Boolean IsPositionInRange(int x, int y)
        {
            if ((x > Plateau._x) || (y > Plateau._y))
            {
                return false; 
            }
            return true; 
        }



    }
}
