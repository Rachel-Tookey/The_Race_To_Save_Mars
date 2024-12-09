using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Terminal.Gui.Graphs;
using static System.Net.Mime.MediaTypeNames;
using NStack;

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
            for (int i = myGrid.GetLength(0) - 1; i >= 0; i--)
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

            int xAlignment = 60;
 
            XYPosition randomPos = missionControl.PositionGenerator();
            missionControl.AddObject(new Hole(randomPos));

            var displayGrid = GetGrid();

            string instructionLabel = $"""
                Your mission: 
                Get to the entrance (@) of the Martian Kingdom.
                """;


            var textLabel = new Label(instructionLabel)
            {
                X = xAlignment,
                Y = 3,
                Width = 40,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
                },
                Border = new Terminal.Gui.Border()
                {
                    BorderStyle = BorderStyle.Single,
                    Padding = new Thickness(0),
                    BorderBrush = Color.BrightMagenta,
                    Background = Color.Black,
                }
            };

            int seconds = 180 / missionControl.Rovers.Count();


            var timerLabel = new Label($"Time left: {seconds}s")
            {
                X = xAlignment,
                Y = Pos.Bottom(textLabel) + 2,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
            };


            Terminal.Gui.Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), _ =>
            {
                seconds--;
                if (seconds > 0)
                {
                    timerLabel.Text = $"Time left: {seconds}s";
                    return true;
                }
                else
                {
                    Application.SwitchToNextLevel(new EndLevel(Application));
                    return false;
                }
            });

            Rover selectedRover = missionControl.Rovers[0];



            var comboBoxLabel = new Label("Select a rover:")
            {
                X = xAlignment,
                Y = Pos.Bottom(timerLabel) + 1,
                Width = Dim.Fill()
            };

            var comboBox = new ComboBox
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 1,
                Width = 40,
                Height = 40

            };


            var roverPosition = new Label()
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 3,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
            };

            comboBox.SetSource(missionControl.Rovers.Select(x => x.Id).ToList());


            comboBox.SelectedItemChanged += (e) =>
            {
                selectedRover = missionControl.GetRoverById((ulong)comboBox.SelectedItem);
                roverPosition.Text = selectedRover.ToString();
            };



            var responseLabel = new Label()
            {
                X = xAlignment,
                Y = Pos.Center() + 4,
                TextAlignment = TextAlignment.Right,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
            };


            openingWindow.KeyDown += (e) =>
            {
                Instructions inputInstruction = (Instructions) (-1);
                if (e.KeyEvent.Key == Key.CursorLeft)
                {
                    inputInstruction = Instructions.L;
                } 
                else if (e.KeyEvent.Key == Key.CursorRight)
                {
                    inputInstruction = Instructions.R;
                }
                else if (e.KeyEvent.Key == Key.CursorUp)
                {
                    inputInstruction = Instructions.M;
                } else if (e.KeyEvent.Key == Key.CursorDown)
                {
                    inputInstruction = Instructions.B;
                } 

                if (inputInstruction != (Instructions)(-1))
                {
                    e.Handled = true;
                    missionControl.RunInstructions(selectedRover, inputInstruction);
                    openingWindow.Remove(displayGrid);
                    displayGrid = GetGrid();
                    openingWindow.Add(displayGrid);
                    openingWindow.SetNeedsDisplay();
                    roverPosition.Text = selectedRover.ToString();

                    if (selectedRover.Position == missionControl.Hole.Position)
                    {
                        Application.SwitchToNextLevel(new FirstLevel(Application));
                    }
                    else if (!missionControl.AreRoversIntact())
                    {
                        // add in a query box -> game over? 
                        Application.SwitchToNextLevel(new EndLevel(Application));
                    }

                }

            };

           
            openingWindow.Add(timerLabel, displayGrid, comboBox, comboBoxLabel, roverPosition, textLabel, responseLabel);


            return openingWindow;
        }

    }
}
