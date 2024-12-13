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

        public List<XYPosition> Health { get; private set; } = new List<XYPosition> { };

        public List<XYPosition> Oil { get; private set; } = new List<XYPosition> { }; 

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

        public Boolean IsPositionUsable(List<XYPosition> list, XYPosition xypos)
        {
            if (list.Where(x => x == xypos).Any())
            {
                list.Remove(xypos);
                return true; 
            }
            return false; 
        }

        public void IsPositionFly()
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
                }
                
                if (IsPositionUsable(Health, roverToMove.Position)) {
                    roverToMove.Health += 20; 
                }

                if (IsPositionUsable(Oil, roverToMove.Position))
                {
                    roverToMove.Acceleration = 5; 
                }

                if ((!Plateau.IsPositionInRange(roverToMove.Position)) || (HaveRoversCollided(roverToMove)))
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

            string[,] newGrid = new string[plateau._y + 14, plateau._x + 14];

            Dictionary<ulong, XYPosition> CurrentRoverPositions = GetRoverPositions();

            for (int rows = plateau._y + 13; rows >= 0; rows--)
            {
                for (int cols = 0; cols < plateau._x + 14; cols++)
                {
                    if ((cols < 7) || (rows < 7) || (rows >= plateau._y + 7) || (cols >= plateau._x + 7) )
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
                newGrid[xYPosition.yAxis + 7, xYPosition.xAxis + 7] = GridSymbols[1];
            }


            foreach (XYPosition xYPosition in Health)
            {
                newGrid[xYPosition.yAxis + 7, xYPosition.xAxis + 7] = "⟡";
            }


            foreach (XYPosition xYPosition in Oil)
            {
                newGrid[xYPosition.yAxis + 7, xYPosition.xAxis + 7] = "🝆";
            }


            foreach (ulong key in CurrentRoverPositions.Keys)
            {
                newGrid[CurrentRoverPositions[key].yAxis + 7, CurrentRoverPositions[key].xAxis + 7] = $"{key}";
            }

            newGrid[EndOfLevel.yAxis + 7, EndOfLevel.xAxis + 7] = "⊕";

            return newGrid;
        }

        public string GetObjectByPosition(XYPosition posToSearch)
        {
            string[,] myGrid = GetGrid();
            return myGrid[posToSearch.yAxis + 7, posToSearch.xAxis + 7];
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
            Obstructions = GeneratePositions(35);
            Health = GeneratePositions(1);
            Oil = GeneratePositions(1);
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
