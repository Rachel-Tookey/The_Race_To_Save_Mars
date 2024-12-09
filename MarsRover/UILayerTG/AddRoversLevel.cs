using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayerTG.Utils;
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

        public Window GetWindow()
        {

            Plateau newPlateau = new(60, 20);
            Application.MissionControl = new MissionControl(newPlateau);


            var openingWindow = new StyledWindow("Add your rovers");


            var instructionLabel = new StyledLabel(Text.GetLevelText("Add Rovers Level"))
            {
                X = Pos.Center(),
                Y = 2,
            };


            var comboLabel = new Terminal.Gui.Label("Select a starting direction:")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(instructionLabel) + 4,

            };

            var comboBox = new ComboBox()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(comboLabel) + 1,
                Width = 40,
                Height = 40,
            };

            comboBox.SetSource(new List<Facing>() { Facing.NORTH, Facing.EAST, Facing.SOUTH, Facing.WEST });


            var positionLabel = new Terminal.Gui.Label("Enter a starting position:")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 1,

            };

            var textField = new TextField()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(positionLabel) + 1,
                Width = 40,
                Text = $"Max: {newPlateau._x - 1}, {newPlateau._y - 1}"
            };

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(textField) + 3,
            };

            var responseLabel = new ResponseLabel()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(submitButton) + 2,
                TextAlignment = TextAlignment.Centered,
                Width = Dim.Fill()
            };


            submitButton.Clicked += () =>
            {
                Facing selectedEnum = (Facing)comboBox.SelectedItem;
                if ((int)selectedEnum < 0)
                {
                    responseLabel.Text = "Please select a direction";
                }
                else
                {
                    RoverParser userInput = new RoverParser(textField.Text.ToString(), selectedEnum, Application.MissionControl.Plateau);
                    if (!userInput.Success)
                    {
                        responseLabel.Text = userInput.Message.ToString();
                    }
                    else
                    {
                        Application.MissionControl.AddObject(userInput.Result);
                        if ((Application.MissionControl.Rovers.Count == 3) || (MessageBox.Query("Continue?", "Do you wish to add any more rovers?", buttons: ["Yes", "No"]) == 1))
                        {
                            Application.SwitchToNextLevel(new InstructionLevel(Application));
                        }
                        else
                        {
                            textField.Text = "x, y";
                            comboBox.SelectedItem = -1;
                        }
                    }

                }


            };

            openingWindow.Add(instructionLabel, comboLabel, comboBox, positionLabel, textField, responseLabel, submitButton);

            return openingWindow;


        }
    }
}
