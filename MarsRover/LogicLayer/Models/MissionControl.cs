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

        public Hole Hole { get; private set; }    

        public MissionControl(Plateau plateau)
        {
            Plateau = plateau;
        }

        public void AddObject(Rover rover)
        {
            Rovers.Add(rover);
        }

        public void AddObject(Hole hole)
        {
            Hole = hole;
        }

        public Rover GetRoverById(ulong Id)
        {
            return Rovers.Where(x => x.Id == Id).First(); 
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
                    roverToMove.MoveRover();
                    if (!Plateau.IsPositionInRange(roverToMove.Position)) 
                    {
                        roverToMove.IsIntact = false;
                    }
                    else if (IsPositionEmpty(roverToMove.Position))
                    {
                        roverToMove.IsIntact = false;
                    }
                }
            }

        }


        public Boolean IsPositionEmpty(XYPosition xyPosition)
        {
            foreach (Rover rover in Rovers)
            {
                if (rover.Position == xyPosition) {
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
                positions.Add(rover.Id, rover.Position);
            }
            return positions;
        }


        public XYPosition PositionGenerator()
        {
            Random random = new Random();
            int xAxis = random.Next(0, Plateau._x - 1);
            int yAxis = random.Next(0, Plateau._y - 1);

            while (!IsPositionEmpty((xAxis, yAxis)))
            {
                xAxis = random.Next(0, Plateau._x - 1);
                yAxis = random.Next(0, Plateau._y - 1);
            }

            return (xAxis, yAxis);
        }



        public string[,] GetGrid()
        {
            // myGrid is indexed [y, x]

            Plateau plateau = this.Plateau;

            string[,] newGrid = new string[plateau._y + 4, plateau._x + 4];

            Dictionary<ulong, XYPosition> CurrentRoverPositions = GetRoverPositions();

            for (int rows = plateau._y + 3; rows >= 0; rows--)
            {
                for (int cols = 0; cols < plateau._x + 4; cols++)
                {
                    if ((cols == 1) || (cols == 0) || (rows == 0) || (rows == 1) || (rows == plateau._y + 3) || (rows == plateau._y + 2) || (cols == plateau._x + 2) || (cols == plateau._x + 3))
                    {
                        newGrid[rows, cols] = new Symbol("☠️", "X");
                    }
                    else if (Hole.Position == (cols - 2, rows - 2))
                    {
                        newGrid[rows, cols] = "@";
                    }
                    else
                    {
                        newGrid[rows, cols] = "_";
                    }
                }
            }


            foreach (ulong key in CurrentRoverPositions.Keys)
                {
                    newGrid[CurrentRoverPositions[key].yAxis + 2, CurrentRoverPositions[key].xAxis + 2] = $"{key}"; 
                }
            

            return newGrid; 
        }


    }
}
