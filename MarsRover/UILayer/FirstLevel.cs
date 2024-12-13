using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayer.Superclasses;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayerTG.Utils;
using MarsRover.Enums;
using MarsRover.UILayer;

namespace MarsRover.UILayerTG
{
    public class FirstLevel : GameLevel
    {

        public FirstLevel(GameApplication game) : base("Level One", game, 240)
        {
            
            App.MissionControl.SetUpFirstLevel();
            
            DisplayGrid = new GridView(1, App.MissionControl.GetGrid());

            AddUI();
            
            SetTimer();
        }

        public override void AddUI()
        {

            TimerLabel = new Terminal.Gui.Label($"Time left: {Seconds}s")
            {
                X = 20,
                Y = 0,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
            };


            var comboBoxLabel = new Terminal.Gui.Label("Select a rover:")
            {
                X = Pos.Right(TimerLabel) + 3,
                Y = 0
            };

            var comboBox = new ComboBox
            {
                X = Pos.Right(comboBoxLabel) + 3,
                Y = 0,
                Width = 15,
                Height = 40
            };

            comboBox.SetSource(App.MissionControl.Rovers.Select(x => x.Id).ToList());


            RoverLabel = new ResponseLabel()
            {
                X = Pos.Right(comboBox) + 3,
                Y = 0,
                Text = SelectedRover.ToString(),
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Brown, Terminal.Gui.Color.Black)
                },
            };


            comboBox.SelectedItemChanged += (e) =>
            {
                SelectedRover = App.MissionControl.GetRoverById((ulong)comboBox.SelectedItem);
                RoverLabel.Text = SelectedRover.ToString();
            };

            Add(comboBoxLabel, comboBox, RoverLabel, TimerLabel, DisplayGrid);

        }

      

        public override void LevelUp()
        {
            App.SwitchToNextLevel(new SecondLevel(App));
        }


    }

}
