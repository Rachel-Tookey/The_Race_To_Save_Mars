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

        public Boolean HasTimeOut = false;

        public Rover SelectedRover;

        public int Seconds = 240;

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

            TimerLabel = new Terminal.Gui.Label($"Time left: {Seconds}s")
            {
                X = 2,
                Y = 0,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
            };


            var comboBoxLabel = new Terminal.Gui.Label("Select a rover:")
            {
                X = Pos.Right(TimerLabel) + 2,
                Y = 0
            };

            var comboBox = new ComboBox
            {
                X = Pos.Right(comboBoxLabel) + 2,
                Y = 0,
                Width = 15,
                Height = 40
            };

            comboBox.SetSource(App.MissionControl.Rovers.Select(x => x.Id).ToList());


            RoverLabel = new ResponseLabel()
            {
                X = Pos.Right(comboBox) + 3,
                Y = 0,
                Text = App.MissionControl.EndOfLevel.ToString()
            };


            comboBox.SelectedItemChanged += (e) =>
            {
                SelectedRover = App.MissionControl.GetRoverById((ulong)comboBox.SelectedItem);
                RoverLabel.Text = SelectedRover.ToString();
            };

            Add(comboBoxLabel, comboBox, RoverLabel, TimerLabel, DisplayGrid);

        }


        public void SetTimer()
        {

            Seconds = Seconds / App.MissionControl.Rovers.Where(x => x.Health != 0).Count();

            Terminal.Gui.Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), _ =>
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

        public override bool OnKeyDown(KeyEvent keyEvent)
        {

            Instructions inputInstruction = (keyEvent.Key) switch
            {
                Key.CursorLeft => Instructions.L,
                Key.CursorRight => Instructions.R,
                Key.CursorUp => Instructions.M,
                Key.CursorDown => Instructions.B,
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
