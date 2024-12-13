using MarsRover.Enums;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayer.Superclasses;
using MarsRover.UILayerTG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayer.Utils; 

namespace MarsRover.UILayerTG
{

    public class AddRoversLevel : StyledWindow
    {
        public GameApplication App { get; set; }

        public ResponseLabel ResponseLabel { get; set; }

        public ComboBox ComboBox { get; set; }

        public AddRoversLevel(GameApplication game) : base("Add your rovers")
        {
            App = game;
            AddUI(); 
        }


        public void AddUI() { 

            var instructionLabel = new StyledLabel(LabelText.addRoverLevel)
            {
                X = Pos.Center(),
                Y = 2,
            };

            var comboLabel = new Terminal.Gui.Label("How many rovers?")
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

            ComboBox.SetSource(new List<int>() { 1, 2, 3 });


            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = 15,
            };

            ResponseLabel = new ResponseLabel()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(submitButton) + 2,
                TextAlignment = TextAlignment.Centered,
                Width = Dim.Fill()
            };

            submitButton.Clicked += SubmitButtonClicked; 

            Add(instructionLabel, comboLabel, ComboBox, submitButton, ResponseLabel);

        }


        public void SubmitButtonClicked()
        {
            if (ComboBox.SelectedItem > -1) { 
            
            for (int i = 0; i <= ComboBox.SelectedItem; i++)
            {
                App.MissionControl.AddRover(new Rover((0, i))); }

             App.SwitchToNextLevel(new InstructionLevel(App));
            } else
            {
                ResponseLabel.Text = "Nothing selected"; 
            }

        }

    }


}

