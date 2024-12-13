using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using MarsRover.Enums;
using MarsRover.UILayerTG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using NStack;
using System.Reflection.Emit;
using MarsRover.UILayer.Superclasses;
using MarsRover.UILayer.Utils;


namespace MarsRover.UILayerTG
{
    public class TrainingLevel : GameLevel 
    {
        public int xAlignment = 70;

        public TrainingLevel(GameApplication game) : base("Training Level", game, 360)
        {
            
            App.MissionControl.SetUpTrainingLevel();
            
            SetTimer();
            
            DisplayGrid = new GridView(0, App.MissionControl.GetGrid());
            
            AddUI();

        }

        public override void AddUI() { 


            var textLabel = new StyledLabel(LabelText.trainingLevel)
            {
                X = xAlignment,
                Y = 3,
                Width = 40,
            };

            TimerLabel = new Terminal.Gui.Label($"Time left: {Seconds}s")
            {
                X = xAlignment,
                Y = Pos.Bottom(textLabel) + 2,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
            };


            var comboBoxLabel = new Terminal.Gui.Label("Select a rover:")
            {
                X = xAlignment,
                Y = Pos.Bottom(TimerLabel) + 1,
                Width = Dim.Fill()
            };

            var comboBox = new ComboBox
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 1,
                Width = 40,
                Height = 40

            };

            comboBox.SetSource(App.MissionControl.Rovers.Select(x => x.Id).ToList());

            RoverLabel = new ResponseLabel()
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 3,
            };


            comboBox.SelectedItemChanged += (e) =>
            {
                SelectedRover = App.MissionControl.GetRoverById((ulong)comboBox.SelectedItem);
                RoverLabel.Text = SelectedRover.ToString();
            };

            Add(textLabel, TimerLabel, DisplayGrid, comboBox, comboBoxLabel, RoverLabel);

        }


        public override void LevelUp()
        {
            App.SwitchToNextLevel(new FirstLevel(App));
        }

    }
}