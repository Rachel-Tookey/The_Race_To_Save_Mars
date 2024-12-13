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

        public ComboBox ComboBoxHowMany { get; set; }

        public AddRoversLevel(GameApplication game) : base("Add your rovers", game)
        {
            AddUI();
        }


        public override void AddUI()
        {

            var instructionLabel = new StyledLabel(LabelText.addRoverLevel)
            {
                X = Pos.Center(),
                Y = 2,
            };


            var comboLabelBox1 = new Terminal.Gui.Label("Select number of rovers:")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(instructionLabel) + 2,
            };


            ComboBoxHowMany = new ComboBox()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(comboLabelBox1) + 1,
                Width = 40,
                Height = 40,
            };

            ComboBoxHowMany.SetSource(new List<int>() { 1, 2, 3 });


            var comboLabelBox2 = new Terminal.Gui.Label("Select a starting direction:")
            {
                X = Pos.Center(),
                Y = Pos.Center(),
                Visible = false
            };


            ComboBox = new ComboBox()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(comboLabelBox2) + 1,
                Width = 40,
                Height = 40,
                Visible = false
            };

            ComboBox.SetSource(new List<Facing>() { Facing.NORTH, Facing.EAST, Facing.SOUTH, Facing.WEST });


            var positionLabel = new Terminal.Gui.Label("Enter a landing position:")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 4,
                Visible = false

            };

            TextField = new TextField()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(positionLabel) + 1,
                Width = 40,
                Text = $"Max: {App.MissionControl.Plateau._x - 1}, {App.MissionControl.Plateau._y - 3}",
                Visible = false
            };

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(TextField) + 3,
                Visible = false
            };

            ResponseLabel = new ResponseLabel()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(TextField) + 1,
                TextAlignment = TextAlignment.Centered,
                Width = Dim.Fill()
            };

            ComboBoxHowMany.SelectedItemChanged += (e) =>
            {
                comboLabelBox2.Visible = true;
                ComboBox.Visible = true;
            };

            ComboBox.SelectedItemChanged += (e) =>
            {
                positionLabel.Visible = true;
                TextField.Visible = true;
                submitButton.Visible = true;
            };


            submitButton.Clicked += SubmitButtonClicked;

            Add(instructionLabel, comboLabelBox1, ComboBoxHowMany, comboLabelBox2, ComboBox, positionLabel, TextField, ResponseLabel, submitButton);

        }


        public void SubmitButtonClicked()
        {
            Facing selectedEnum = (Facing)ComboBox.SelectedItem;
            RoverParser userInput = new RoverParser(ComboBoxHowMany.SelectedItem, TextField.Text.ToString(), selectedEnum, App.MissionControl);
            if (!userInput.Success)
            {
                ResponseLabel.Text = userInput.Message.ToString();
            }
            else
            {
                App.MissionControl.Rovers.AddRange(userInput.Result);
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




