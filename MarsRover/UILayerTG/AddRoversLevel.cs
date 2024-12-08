using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class AddRoversLevel : ILevel
    {
        public GameApplication Application { get; set; }

        public AddRoversLevel(GameApplication game)
        {

            Application = game;
        }

        public Window WindowRun()
        {
            Plateau newPlateau = new(50, 20);
            Application.MissionControl = new MissionControl(newPlateau);


            var openingWindow = new Terminal.Gui.Window("Add your rovers")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            var instructionLabel = new Terminal.Gui.Label("You are allowed up to 3 rovers for the duration of the game.")
            {
                X = Pos.Center(),
                Y = 1,

            };


            var comboLabel = new Terminal.Gui.Label("Enter a starting direction:")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(instructionLabel) + 4,

            };


            var responseCBLabel = new Label()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(comboLabel) + 1,
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
                Width = Dim.Fill()
            };


            var comboBox = new ComboBox()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(responseCBLabel) + 1,
                Width = 40,
                Height = 40,
            };

            comboBox.SetSource(new List<Facing>() { Facing.NORTH, Facing.EAST, Facing.SOUTH, Facing.WEST });


            var positionLabel = new Terminal.Gui.Label("Enter a starting position:")
            {
                X = Pos.Center(),
                Y = Pos.Center(),

            };



            var textField = new TextField("x y")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(positionLabel),
                Width = 40

            };

    

            var responseLabel = new Label()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(textField),
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
                Width = Dim.Fill()
            };

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(responseLabel),
            };



            submitButton.Clicked += () =>
            {
                Facing selectedEnum = (Facing)comboBox.SelectedItem;
                if ((int)selectedEnum < 0)
                {
                    responseLabel.Text = "Please select a direction";
                    responseLabel.SetNeedsDisplay();
                }
                else { 
                    RoverParser userInput = new RoverParser(textField.Text.ToString(), selectedEnum, Application.MissionControl.Plateau);
                    if (userInput.Success)
                    {
                        Application.MissionControl.AddObject(userInput.Result);
                        if (Application.MissionControl.Rovers.Count < 3)
                        {
                            if (MessageBox.Query("Continue?", "Do you wish to add any more rovers?", buttons: ["Yes", "No"]) == 1)
                            {
                                Application.SwitchToNextLevel(new TrainingLevel(Application));
                            } else
                            {
                                textField.Text = "x y"; 
                                comboBox.SelectedItem = -1;
                            }
                        } else
                        {
                            Application.SwitchToNextLevel(new TrainingLevel(Application));
                        }
                    }
                    else
                    {
                        responseLabel.Text = userInput.Message.ToString();
                        responseLabel.SetNeedsDisplay();
                     }
                    }

            };

            openingWindow.Add(instructionLabel, comboLabel, responseCBLabel, comboBox, positionLabel, textField, responseLabel, submitButton);

            return openingWindow;


        }
    }
}
