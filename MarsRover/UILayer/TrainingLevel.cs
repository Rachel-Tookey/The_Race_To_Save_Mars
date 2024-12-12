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


namespace MarsRover.UILayerTG
{
    public class TrainingLevel : StyledWindow 
    {
        public GameApplication App { get; set; }

        public int xAlignment = 70;

        public int Seconds = 360;

        public Boolean HasTimeOut = false;

        public Rover SelectedRover;

        public GridView DisplayGrid;

        public ResponseLabel RoverLabel;

        public Terminal.Gui.Label TimerLabel; 

        public TrainingLevel(GameApplication game) : base("Training Level")
        {

            App = game;

            SelectedRover = App.MissionControl.Rovers.Where(x => x.Health > 0).First();
            
            App.MissionControl.SetUpTrainingLevel();
            
            SetTimer();
            
            DisplayGrid = new GridView(0, App.MissionControl.GetGrid());
            
            AddUI();

        }

        public void AddUI() { 


            var textLabel = new StyledLabel(Utils.Text.GetLevelText("Training Level"))
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
                    App.SwitchToNextLevel(new FirstLevel(App));
                }
                else if (!App.MissionControl.AreRoversIntact())
                {
                    App.SwitchToNextLevel(new EndLevel(App));
                }
            }

            return true ;
        }


    }
}