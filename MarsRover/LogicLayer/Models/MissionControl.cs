global using XYPosition = (int xAxis, int yAxis);

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;
using MarsRover.LogicLayer.Models.Superclasses;
using Sharprompt;

namespace MarsRover.LogicLayer.Models
{
    public class MissionControl
    {
        public Plateau Plateau { get; set; }

        public List<Rover> Rovers { get; private set; } = new List<Rover>();

        public List<XYPosition> Obstructions { get; private set; } = new List<XYPosition> { };

        public List<XYPosition> Health { get; private set; }

        public XYPosition EndOfLevel { get; set; }

        public List<string> GridSymbols = new List<string>();

        public MissionControl(Plateau plateau)
        {
            Plateau = plateau;
        }

        public void AddObject(Rover rover)
        {
            Rovers.Add(rover);
        }

        public Boolean IsPositionHealth(XYPosition xypos)
        {
            if (Health.Where(x => x == xypos).Any())
            {
                Health.Remove(xypos);
                return true; 
            }
            return false; 

        }

        public void IsPositionFly()
        {

        }

        public void IsPositionOil()
        {

        }

        public Rover GetRoverById(ulong Id)
        {
            return Rovers.Where(x => x.Id == Id).First();
        }

        public void RunInstructions(Rover roverToMove, Instructions instruction)
        {
            if (roverToMove.Health == 0) return;
            if ((instruction == Instructions.L) || (instruction == Instructions.R))
            {
                roverToMove.RotateRover(instruction);
            }
            else
            {
                roverToMove.MoveRover(instruction);
                if (!IsPositionEmptyRocks(roverToMove.Position))
                {
                    roverToMove.Health -= 10;
                } else if (IsPositionHealth(roverToMove.Position)) {
                    roverToMove.Health += 20; 
                }
                else if ((!Plateau.IsPositionInRange(roverToMove.Position)) || (HaveRoversCollided(roverToMove)))
                {
                    roverToMove.Health = 0;
                }
            }
        }


        public Boolean HaveRoversCollided(Rover rover)
        {
            if (Rovers.Where(x => x.Position == rover.Position && x.Id != rover.Id).Any())
            {
                Rover roverCrash = Rovers.Where(x => x.Position == rover.Position && x.Id != rover.Id).First();
                roverCrash.Health = 0;
                return true;
            }
            return false;
        }


        public Boolean IsPositionEmptyRovers(XYPosition xyPosition)
        {
            return !Rovers.Where(x => x.Position == xyPosition).Any();
        }

        public Boolean IsPositionEmptyRocks(XYPosition xyPosition)
        {
            return !Obstructions.Where(x => x == xyPosition).Any();
        }


        public Boolean AreRoversIntact()
        {
            return Rovers.Where(x => x.Health > 0).Any();
        }

        public Dictionary<ulong, XYPosition> GetRoverPositions()
        {

            Dictionary<ulong, XYPosition> positions = new Dictionary<ulong, XYPosition>();
            foreach (Rover rover in Rovers.Where(x => x.Health != 0).ToList())
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

            while (!IsPositionEmptyRovers((xAxis, yAxis)))
            {
                xAxis = random.Next(1, Plateau._x - 1);
                yAxis = random.Next(1, Plateau._y - 1);
            }

            return (xAxis, yAxis);
        }

        public List<XYPosition> GeneratePositions(int percent)
        {
            Random rand = new Random();
            List<XYPosition> positionHolder = new List<XYPosition>();
            for (int i = 0; i < Plateau._x * Plateau._y; i++)
            {
                if (rand.Next(0, 100) < percent)
                {
                    XYPosition genPos = PositionGenerator();
                    positionHolder.Add(genPos);
                }
            }
            return positionHolder;

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
                        newGrid[rows, cols] = GridSymbols[0];
                    }
                    else
                    {
                        newGrid[rows, cols] = " ";
                    }
                }
            }

            foreach (XYPosition xYPosition in Obstructions)
            {
                newGrid[xYPosition.yAxis + 2, xYPosition.xAxis + 2] = GridSymbols[1];
            }


            foreach (XYPosition xYPosition in Health)
            {
                newGrid[xYPosition.yAxis + 2, xYPosition.xAxis + 2] = "⟡";
            }


            foreach (ulong key in CurrentRoverPositions.Keys)
            {
                newGrid[CurrentRoverPositions[key].yAxis + 2, CurrentRoverPositions[key].xAxis + 2] = $"{key}";
            }

            newGrid[EndOfLevel.yAxis + 2, EndOfLevel.xAxis + 2] = "⊕";

            return newGrid;
        }

        public string GetObjectByPosition(XYPosition posToSearch)
        {
            string[,] myGrid = GetGrid();
            return myGrid[posToSearch.yAxis + 2, posToSearch.xAxis + 2];
        }


        public void SetUpTrainingLevel()
        {
            GridSymbols = new List<string> { "⣫", "⡺" };
            EndOfLevel = PositionGenerator();
            Obstructions = GeneratePositions(20);
        }

        public void SetUpFirstLevel()
        {
            GridSymbols = new List<string> { "⠿", "⣤"};
            Plateau = new Plateau(110, 20);
            EndOfLevel = PositionGenerator();
            Obstructions = GeneratePositions(25);
            Health = GeneratePositions(5);
        }

        public void SetUpSecondLevel()
        {
            Obstructions = GeneratePositions(30);

        }

        public void SetUpThirdLevel()
        {
            Obstructions = GeneratePositions(35);

        }

    }
}
