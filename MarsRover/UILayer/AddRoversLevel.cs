using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayer.Superclasses;
using MarsRover.UILayer.Utils;
using MarsRover.UILayerTG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{

    public class AddRoversLevel : StyledWindow
    {
        public ResponseLabel ResponseLabel { get; set; }

        public TextField TextField { get; set; }

        public ComboBox ComboBox { get; set; }

        public AddRoversLevel(GameApplication game) : base("Add your rovers", game)
        {
            AddUI(); 
        }


        public override void AddUI() { 

            var instructionLabel = new StyledLabel(LabelText.addRoverLevel)
            {
                X = Pos.Center(),
                Y = 2,
            };

            var comboLabel = new Terminal.Gui.Label("Select a starting direction:")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(instructionLabel) + 4,
            };

            ComboBox = new ComboBox()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(comboLabel) + 1,
                Width = 40,
                Height = 40,
            };

            ComboBox.SetSource(new List<Facing>() { Facing.NORTH, Facing.EAST, Facing.SOUTH, Facing.WEST });


            var positionLabel = new Terminal.Gui.Label("Enter a starting position:")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 1,

            };

            TextField = new TextField()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(positionLabel) + 1,
                Width = 40,
                Text = $"Max: {App.MissionControl.Plateau._x - 1}, {App.MissionControl.Plateau._y - 1}"
            };

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(TextField) + 3,
            };

            ResponseLabel = new ResponseLabel()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(submitButton) + 2,
                TextAlignment = TextAlignment.Centered,
                Width = Dim.Fill()
            };

            submitButton.Clicked += SubmitButtonClicked; 

            Add(instructionLabel, comboLabel, ComboBox, positionLabel, TextField, ResponseLabel, submitButton);

        }


        public void SubmitButtonClicked()
        {
            Facing selectedEnum = (Facing)ComboBox.SelectedItem;
            if ((int)selectedEnum < 0)
            {
                ResponseLabel.Text = "Please select a direction";
            }
            else
            {
                RoverParser userInput = new RoverParser(TextField.Text.ToString(), selectedEnum, App.MissionControl);
                if (!userInput.Success)
                {
                    ResponseLabel.Text = userInput.Message.ToString();
                }
                else
                {
                    App.MissionControl.AddRover(userInput.Result);
                    if ((App.MissionControl.Rovers.Count == 3) || (MessageBox.Query("Continue?", "Do you wish to add any more rovers?", buttons: ["Yes", "No"]) == 1))
                    {
                        App.SwitchToNextLevel(new InstructionLevel(App));
                    }
                    else
                    {
                        TextField.Text = "x, y";
                        ComboBox.SelectedItem = -1;
                    }
                }

            }


        }

    }


}

