using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Terminal.Gui.Graphs;
using static System.Net.Mime.MediaTypeNames;

namespace MarsRover.UILayerTG
{
    public class LevelThree
    {
        public GameApplication Application { get; set; }

        public Window Window { get; set; }

        public LevelThree(GameApplication game)
        {

            Application = game;
            Window = WindowRun();
        }


        public View GetGrid()
        {
            string[,] myGrid = Application.MissionControl.GetGrid();

            var displayGrid = new View()
            {
                X = 0,
                Y = 0,
                Width = 4 + myGrid.GetLength(1),
                Height = 4 + myGrid.GetLength(0),
            };


            int startX = 2;
            int startY = 2;
            for (int i = 0; i < myGrid.GetLength(0); i++)
            {
                for (int j = 0; j < myGrid.GetLength(1); j++)
                {
                    var colorSet = new ColorScheme
                    {
                        Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Black)
                    };

                    if (myGrid[i, j] == "X")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black)
                        };

                    }
                    else if (myGrid[i, j] == "_")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black)
                        };

                    }
                    else if (myGrid[i, j] == "$")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black)
                        };
                    }

                    var label = new Label(myGrid[i, j])
                    {
                        X = startX + j,
                        Y = startY + i,
                        Height = 1,
                        ColorScheme = colorSet
                    };

                    displayGrid.Add(label);
                }
            }

            return displayGrid;

        }


        public Window WindowRun()
        {
            var openingWindow = new Terminal.Gui.Window("Training Level")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            XYPosition randomPos = Application.MissionControl.PositionGenerator();
            Application.MissionControl.AddObject(new ChargingStation(randomPos));

            var displayGrid = GetGrid(); 

            string instructionLabel = """
                Your Rovers have landed on Mars to find the capital city abandonned. 
                The journey has sapped your strength. 
                You must get to the charging station ($).  
                You can rotate right (R) or (L), and move one space at a time. 
                You have three attempts. 
                """;
            
            var textLabel = new Label(instructionLabel)
            {
                X = Pos.Right(displayGrid) + 4,
                Y = 3,
                Width = Dim.Fill()
            };

            var buttonLabel = new Terminal.Gui.Label("Enter your sequence of moves:")
            {
                X = Pos.Right(displayGrid) + 5,
                Y = 7,

            };

            var textField = new TextField("i.e.: LLRMMMRLR")
            {
                X = Pos.Right(displayGrid) + 6,
                Y = 9,
                Width = 40

            };

            openingWindow.Add(textField);

            var submitButton = new Button("Submit")
            {
                X = Pos.Right(displayGrid) + 7,
                Y = 11
            };


            var responseLabel = new Label()
            {
                X = Pos.Right(displayGrid) + 8,
                Y = 12,
                TextAlignment = TextAlignment.Right,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
            };


            // if Rover position == 

            int attempts = 0; 

            submitButton.Clicked += () =>
            {
                if (attempts != 3) { 
                InstructionParser userInput = new InstructionParser(textField.Text.ToString());
                    if (!userInput.Success)
                    {
                        responseLabel.Text = userInput.Message.ToString();

                    }
                    else
                    {
                        Rover thisRover = Application.MissionControl.Rovers[0];
                        attempts += 1;
                        Application.MissionControl.RunInstructions(thisRover, userInput.Result);
                        openingWindow.Remove(displayGrid);
                        displayGrid = GetGrid();
                        openingWindow.Add(displayGrid);
                        openingWindow.SetNeedsDisplay();

                        if ((thisRover.Position.x, thisRover.Position.y) == randomPos)
                        {
                            Application.SwitchToNextLevel(new LevelFour(Application).Window);
                        }
                        else if (thisRover.IsIntact != true)
                        {
                            responseLabel.Text = "Game over";
                            Application.SwitchToNextLevel(new LevelOne(Application).Window);

                        }

                    }
                } else
                {
                    responseLabel.Text = "Game over";
                }


            };



            openingWindow.Add(displayGrid, textLabel, buttonLabel, textField, submitButton, responseLabel);


            return openingWindow; 
        }

    }
}
