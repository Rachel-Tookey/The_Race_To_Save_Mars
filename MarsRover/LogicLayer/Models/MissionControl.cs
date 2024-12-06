global using XYPosition = (int xAxis, int yAxis);

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;
using Sharprompt;
using Spectre.Console;

namespace MarsRover.LogicLayer.Models
{
    public class MissionControl
    {
        public Plateau Plateau { get; set; }

        public List<Rover> Rovers { get; private set; } = new List<Rover>();

        public ChargingStation ChargingStation { get; private set; }    

        public MissionControl(Plateau plateau)
        {
            Plateau = plateau;
        }

        public void AddObject(Rover rover)
        {
            Rovers.Add(rover);
        }

        public void AddObject(ChargingStation chargingStation)
        {
            ChargingStation = chargingStation;
        }

        public void RunInstructions(Rover roverToMove, List<Instructions> listOfMoves)
        {
            foreach (Instructions direction in listOfMoves)
            {
                if ((direction == Instructions.L) || (direction == Instructions.R))
                {
                    roverToMove.RotateRover(direction);
                }
                else if (roverToMove.IsIntact)
                {
                    XYPosition futurePosition = roverToMove.PredictNextPosition();
                    if (!Plateau.IsPositionInRange((futurePosition.xAxis, futurePosition.yAxis)))
                    {
                        roverToMove.MoveRover();
                        roverToMove.IsIntact = false;
                    }
                    else if (IsPositionEmpty((futurePosition.xAxis, futurePosition.yAxis)))
                    {
                        roverToMove.MoveRover();
                    }
                }
            }

        }


        public Boolean IsPositionEmpty(XYPosition xyPosition)
        {
            foreach (Rover rover in Rovers)
            {
                if ((rover.Position.x == xyPosition.xAxis) && (rover.Position.y == xyPosition.yAxis)) {
                    return false; 
                }
            }
            return true; 
        }

        public Boolean AreRoversIntact()
        {
            foreach (Rover rover in Rovers)
            {
                if (rover.IsIntact)
                {
                    return true; 
                }
            }
            return false;
        }

        public Dictionary<ulong, XYPosition> GetRoverPositions()
        {

            Dictionary<ulong, XYPosition> positions = new Dictionary<ulong, XYPosition>();
            foreach (Rover rover in Rovers)
            {
                positions.Add(rover.Id, (rover.Position.x, rover.Position.y));
            }
            return positions;
        }


        public XYPosition PositionGenerator()
        {
            Random random = new Random();
            int xAxis = random.Next(1, Plateau._x);
            int yAxis = random.Next(1, Plateau._y);

            while (!IsPositionEmpty((xAxis, yAxis)))
            {
                xAxis = random.Next(1, Plateau._x);
                yAxis = random.Next(1, Plateau._y);
            }

            return (xAxis, yAxis);
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

            Dictionary<ulong, XYPosition> CurrentRoverPositions = GetRoverPositions();

            for (int rows = plateau._y + 3; rows >= 0; rows--)
            {
                Text[] gridContents = new Text[plateau._x + 4];
                for (int cols = 0; cols < plateau._x + 4; cols++)
                {
                    if ((cols == 1) || (cols == 0) || (rows == 0) || (rows == 1) || (rows == plateau._y + 2) || (rows == plateau._y + 3) || (cols == plateau._x + 2) || (cols == plateau._x + 3))
                    {
                        gridContents[cols] = new Text(new Symbol("☠", "X"), new Style(Color.DarkKhaki));
                    } else if (ChargingStation.Position == (cols - 1, rows - 1)) {
                        gridContents[cols] = new Text(new Symbol("⚕", "£"), new Style(Color.DeepPink3));
                    }
                    else
                    {
                        gridContents[cols] = new Text($"{plat[cols - 2, rows - 2]}", new Style(Color.Red, Color.Black));
                    }


                    foreach (ulong key in CurrentRoverPositions.Keys)
                    {
                        if (CurrentRoverPositions[key] == (cols - 1 , rows - 1 ))
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
