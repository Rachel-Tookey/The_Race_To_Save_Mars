using MarsRover.Enums;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayerTG;
using MarsRover.UILayerTG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayer.Superclasses
{
    public abstract class GameLevel : Window
    {
        public GameApplication App { get; set; }

        public Boolean HasTimeOut = false;

        public Rover SelectedRover;

        public int Seconds;

        public GridView DisplayGrid;

        public Terminal.Gui.Label TimerLabel;

        public ResponseLabel RoverLabel;

        public Window NextLevel;

        public GameLevel(string text, GameApplication app, int seconds) : base(text)
        {
            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();
            App = app;
            Seconds = seconds;
            SelectedRover = App.MissionControl.Rovers.Where(x => x.Health > 0).First();

        }

        public abstract void AddUI();

        public virtual void SetTimer()
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
                    MessageBox.Query("Game over", "Game Over! You're out of time!", buttons: ["Okay"]);
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
                    LevelUp(); 
                }
                else if (!App.MissionControl.AreRoversIntact())
                {
                    MessageBox.Query("Game over", "Game Over! Your rovers are destroyed", buttons: ["Okay"]);
                    App.SwitchToNextLevel(new EndLevel(App));
                }
            }

            return true;
        }


        public abstract void LevelUp();
  
    }
}