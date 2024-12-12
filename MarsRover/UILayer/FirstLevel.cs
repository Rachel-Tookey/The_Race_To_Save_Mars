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

namespace MarsRover.UILayerTG
{
    public class FirstLevel : StyledWindow
    {

        public GameApplication App { get; set; }

        public int Seconds = 240;

        public Boolean HasTimeOut = false;

        public Rover SelectedRover;

        public GridView DisplayGrid;

        public Terminal.Gui.Label TimerLabel;

        public ResponseLabel RoverLabel; 

        public FirstLevel(GameApplication game) : base("Level One")
        {
            App = game;
            SelectedRover = App.MissionControl.Rovers.Where(x => x.Health > 0).First();
            App.MissionControl.SetUpFirstLevel();
            DisplayGrid = new GridView(1, App.MissionControl.GetGrid());
            AddUI();
            SetTimer();
        }

        public void AddUI()
        {

            TimerLabel = new Terminal.Gui.Label()
            {
                X = 20,
                Y = 0,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
                Text = $"Time left: {Seconds}s"
            };


            var comboBoxLabel = new Terminal.Gui.Label()
            {
                X = Pos.Right(TimerLabel) + 3,
                Y = 0,
                Text = "Select a rover:"
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
                Text = SelectedRover.ToString()
            };


            comboBox.SelectedItemChanged += (s, e) =>
            {
                SelectedRover = App.MissionControl.GetRoverById((ulong)comboBox.SelectedItem);
                RoverLabel.Text = SelectedRover.ToString();
            };

            Add(comboBoxLabel, comboBox, RoverLabel, TimerLabel, DisplayGrid);

        }


        public void SetTimer()
        {

            Seconds = Seconds / App.MissionControl.Rovers.Where(x => x.Health != 0).Count();

            Terminal.Gui.Application.AddTimeout(TimeSpan.FromSeconds(1), () =>
            {
                Seconds--;
                if (HasTimeOut)
                {
                    return false;
                }

                if (Seconds > 0)
                {
                    TimerLabel.Text = $"Time left: {Seconds}s";
                    return true;
                }
                else
                {
                    App.SwitchToNextLevel(new EndLevel(App));
                    return false;
                }
            });


        }

        public override bool OnProcessKeyDown(Key e)
        {

            Instructions inputInstruction = (e.KeyCode) switch
            {
                (KeyCode)37 => Instructions.L,
                (KeyCode)39 => Instructions.R,
                (KeyCode)38 => Instructions.M,
                (KeyCode)40 => Instructions.B,
                _ => (Instructions)(-1),
            };

            if (inputInstruction != (Instructions)(-1))
            {
                XYPosition oldPos = SelectedRover.Position;
                App.MissionControl.RunInstructions(SelectedRover, inputInstruction);
                RoverLabel.Text = SelectedRover.ToString();


                if (inputInstruction == Instructions.M || inputInstruction == Instructions.B)
                {
                    DisplayGrid.MoveObjectTo(SelectedRover.Id, oldPos, SelectedRover.Position, App.MissionControl.GetObjectByPosition(oldPos));
                    this.SetNeedsDisplay();
                }


                if (SelectedRover.Position == App.MissionControl.EndOfLevel)
                {
                    HasTimeOut = true;
                    App.SwitchToNextLevel(new OpeningLevel(App));
                }
                else if (!App.MissionControl.AreRoversIntact())
                {
                    App.SwitchToNextLevel(new EndLevel(App));
                }
            }

            return true;
        }



    }

}
