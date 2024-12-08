using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using Spectre.Console;
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
    public class TrainingLevel : ILevel
    {
        public GameApplication Application { get; set; }

        public TrainingLevel(GameApplication game)
        {

            Application = game;
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
                    else if (myGrid[i, j] == "@")
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
            MissionControl missionControl = Application.MissionControl;

            var openingWindow = new Terminal.Gui.Window("Training Level")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };


            XYPosition randomPos = missionControl.PositionGenerator();
            missionControl.AddObject(new Hole(randomPos));

            var displayGrid = GetGrid();


            string instructionLabel = """
                Your Rovers have landed on Mars to find it...empty. 
                Then you remember, the Martians live underground!
                You spot a door (@)...the entrance! 
                You must get one of your rovers there.  
                You can rotate right (R) or (L).
                You can move one space at a time. 
                You have 2 minutes...
                """;
            
            var textLabel = new Label(instructionLabel)
            {
                X = 65,
                Y = 3,
                Width = Dim.Fill()
            };



            var comboBoxLabel = new Label("Select a Rover:")
            {
                X = 65,
                Y = Pos.Bottom(textLabel) + 2,
                Width = Dim.Fill()
            };

            var comboBox = new ComboBox
            {
                X = 65,
                Y = Pos.Bottom(comboBoxLabel),
                Width = 40,
                Height = 40
            };


            var roverPosition = new Label()
            {
                X = 65,
                Y = Pos.Bottom(comboBoxLabel) + 2,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
            };

            comboBox.SetSource(missionControl.Rovers.Select(x => x.Id).ToList());

            Rover selectedRover = missionControl.Rovers[0];

            comboBox.SelectedItemChanged += (e) =>
            {
                selectedRover = missionControl.GetRoverById( (ulong) comboBox.SelectedItem);
                roverPosition.Text = selectedRover.ToString(); 
            };


            var buttonLabel = new Terminal.Gui.Label("Enter your sequence of moves:")
            {
                X = 65,
                Y = Pos.Center() + 3,

            };


            var textField = new TextField("i.e.: LLRMMMRLR")
            {
                X = 65,
                Y = Pos.Bottom(buttonLabel) + 2,
                Width = 40

            };

            openingWindow.Add(textField);

            var submitButton = new Button("Submit")
            {
                X = 65,
                Y = Pos.Bottom(textField) + 2,
            };


            var responseLabel = new Label()
            {
                X = 65,
                Y = Pos.Bottom(submitButton) + 2,
                TextAlignment = TextAlignment.Right,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
            };

            int attempts = 0; 

            submitButton.Clicked += () =>
            {
                if (attempts < 9) { 
                InstructionParser userInput = new InstructionParser(textField.Text.ToString());
                    if (!userInput.Success)
                    {
                        responseLabel.Text = userInput.Message.ToString();

                    }
                    else
                    {
                        attempts += 1;
                        missionControl.RunInstructions(selectedRover, userInput.Result);
                        openingWindow.Remove(displayGrid);
                        displayGrid = GetGrid();
                        openingWindow.Add(displayGrid); 
                        openingWindow.SetNeedsDisplay();
                        roverPosition.Text = selectedRover.ToString();

                        if (selectedRover.Position == missionControl.Hole.Position)
                        {
                            Application.SwitchToNextLevel(new FirstLevel(Application));
                        }
                        else if (missionControl.AreRoversIntact() != true)
                        {
                            Application.SwitchToNextLevel(new EndLevel(Application));

                        }

                    }
                } else
                {
                    Application.SwitchToNextLevel(new EndLevel(Application));
                }


            };


            openingWindow.Add(displayGrid, comboBox, comboBoxLabel, roverPosition, textLabel, buttonLabel, textField, submitButton, responseLabel);


            return openingWindow; 
        }

    }
}
