using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;
using Sharprompt;
using Spectre.Console;
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
            int[,] _positionConversion = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
            int futureX = roverToMove.Position.x + _positionConversion[(int)roverToMove.Position.Direction, 0];
            int futureY = roverToMove.Position.y + _positionConversion[(int)roverToMove.Position.Direction, 1];
            return (futureX, futureY);
        }


        public void RunInstructions(Rover roverToMove, List<Instructions> listOfMoves)
        {
            foreach (Instructions direction in listOfMoves) { 
                if ((direction == Instructions.L) || (direction == Instructions.R))
                {
                    roverToMove.RotateRover(direction);
                } else if (roverToMove.IsIntact)
                {
                    PositionCheck futurePosition = ReturnNewPosition(roverToMove);
                    if (!IsPositionInRange(futurePosition.xAxis, futurePosition.yAxis))
                    {
                        roverToMove.MoveRover();
                        roverToMove.IsIntact = false;
                    } else if (IsPositionEmpty(futurePosition.xAxis, futurePosition.yAxis))
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
            if ((x >= Plateau._x) || (y >= Plateau._y) || (x < 0) || (y < 0))
            {
                return false; 
            }
            return true; 
        }


        public Dictionary<ulong, PositionCheck> GetRoverPositions()
        {

            Dictionary<ulong, PositionCheck> positions = new Dictionary<ulong, PositionCheck>();
            foreach (Rover rover in Rovers)
            {
                positions.Add(rover.Id, (rover.Position.x, rover.Position.y));
            }
            return positions; 
        }


        public void DisplayGrid()
        {

            var grid = new Grid();

            Plateau plateau = this.Plateau;
            char[,] plat = this.Plateau.Grid;


            for (int cols = 0; cols < plateau._x + 4; cols++)
            {
                grid.AddColumn();
            }

            Dictionary<ulong, PositionCheck> CurrentRoverPositions = GetRoverPositions();

            for (int rows = 0; rows < plateau._y + 4; rows++)
            {
                Text[] gridContents = new Text[plateau._x + 4];
                for (int cols = 0; cols < plateau._x + 4; cols++)
                {
                    if ((cols == 1) || (cols == 0) || (rows == 0) || (rows == 1) || (rows == plateau._y + 2) || (rows == plateau._y + 3) || (cols == plateau._x + 2) || (cols == plateau._x + 3))
                    {
                        gridContents[cols] = new Text(new Symbol("☠", "X"), new Style(Color.DarkKhaki));
                    } 
                    else
                    {
                        gridContents[cols] = new Text($"{plat[cols - 2, rows - 2]}", new Style(Color.Red, Color.Black));
                    }

                    foreach (ulong key in CurrentRoverPositions.Keys)
                    {
                        if (CurrentRoverPositions[key] == (cols - 2 , rows - 2 ))
                        {
                            gridContents[cols] = new Text($"{key}", new Style(Color.Aquamarine1));

                        }

                    }


                }
                grid.AddRow(gridContents);
            }

            AnsiConsole.Write(grid);

        }


    }
}
