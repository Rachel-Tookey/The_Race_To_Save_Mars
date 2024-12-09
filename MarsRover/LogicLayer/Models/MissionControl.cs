﻿global using XYPosition = (int xAxis, int yAxis);

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums;

namespace MarsRover.LogicLayer.Models
{
    public class MissionControl
    {
        public Plateau Plateau { get; set; }

        public List<Rover> Rovers { get; private set; } = new List<Rover>();

        public List<XYPosition> Rocks { get; private set; } = new List<XYPosition> { };

        public Hole Hole { get; private set; } // turn into a list of objects?

        public XYPosition EndOfLevel { get; set; }

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

        public void RunInstructions(Rover roverToMove, Instructions instruction)
        {
            if (!roverToMove.IsIntact) return; 
            if ((instruction == Instructions.L) || (instruction == Instructions.R))
            {
                roverToMove.RotateRover(instruction);
            }
            else 
            {
                roverToMove.MoveRover(instruction); 
                if ((!Plateau.IsPositionInRange(roverToMove.Position)) || (HaveRoversCollided(roverToMove)) || !IsPositionEmptyRocks(roverToMove.Position) )
                {
                    roverToMove.IsIntact = false;
                }
            }
        }
    

        public Boolean HaveRoversCollided(Rover rover)
        {
            if (Rovers.Where(x => x.Position == rover.Position && x.Id != rover.Id).Any())
            {
                Rover roverCrash = Rovers.Where(x => x.Position == rover.Position && x.Id != rover.Id).First();
                roverCrash.IsIntact = false;
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
            return !Rocks.Where(x => x == xyPosition).Any();
        }


        public Boolean AreRoversIntact()
        {
            return Rovers.Where(x => x.IsIntact).Any();
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

            while (!IsPositionEmptyRovers((xAxis, yAxis)))
            {
                xAxis = random.Next(0, Plateau._x - 1);
                yAxis = random.Next(0, Plateau._y - 1);
            }

            return (xAxis, yAxis);
        }

        public void RockGenerator(int percent, XYPosition EndLeveLPosition)
        {
            Random rand = new Random();
            for (int i = 0; i < Plateau._x; i++)
            {
                for (int j = 0; j < Plateau._y; j++)
                {
                    if (rand.Next(0, 100) < percent)
                    {
                        XYPosition genPos = PositionGenerator();
                        if (genPos != EndLeveLPosition)
                        {
                            Rocks.Add(genPos);
                        }
                    }
                }
            }
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
                        newGrid[rows, cols] = "V";
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

            foreach (XYPosition xYPosition in Rocks)
            {
                newGrid[xYPosition.yAxis + 2, xYPosition.xAxis + 2] = "V"; 
            }

            foreach (ulong key in CurrentRoverPositions.Keys)
                {
                    newGrid[CurrentRoverPositions[key].yAxis + 2, CurrentRoverPositions[key].xAxis + 2] = $"{key}"; 
                }
            
            return newGrid; 
        }

        public void SetUpTrainingLevel()
        {
            XYPosition EndOfLevel = PositionGenerator();

            RockGenerator(20, EndOfLevel);

            EndOfLevel = EndOfLevel;

            AddObject(new Hole(EndOfLevel));

        }


    }
    }
