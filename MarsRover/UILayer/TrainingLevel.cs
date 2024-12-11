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


namespace MarsRover.UILayerTG
{
    public class TrainingLevel : StyledWindow 
    {
        public GameApplication App { get; set; }

        public int xAlignment = 70; 

        public TrainingLevel(GameApplication game) : base("Training Level")
        {

            App = game;
            InitialiseLevel();
        }


        public void InitialiseLevel()
        {
            MissionControl mc = App.MissionControl;

            mc.SetUpTrainingLevel();

            Rover selectedRover = mc.Rovers[0];

            var displayGrid = new GridView(mc.GetGrid());

            var textLabel = new StyledLabel(Utils.Text.GetLevelText("Training Level"))
            {
                X = xAlignment,
                Y = 3,
                Width = 40,
            };


            int seconds = 360 / App.MissionControl.Rovers.Where(x => x.Health != 0).Count();

            var timerLabel = new Terminal.Gui.Label($"Time left: {seconds}s")
            {
                X = xAlignment,
                Y = Pos.Bottom(textLabel) + 2,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
            };

            Boolean stopTimeout = false; 
            Terminal.Gui.Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), _ =>
            {
                seconds--;
                if (stopTimeout)
                {
                    return false;
                }

                if (seconds > 0)
                {
                    timerLabel.Text = $"Time left: {seconds}s";
                    return true;
                }
                else
                {
                    App.SwitchToNextLevel(new EndLevel(App));
                    return false;
                }
            });


            var comboBoxLabel = new Terminal.Gui.Label("Select a rover:")
            {
                X = xAlignment,
                Y = Pos.Bottom(timerLabel) + 1,
                Width = Dim.Fill()
            };

            var comboBox = new ComboBox
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 1,
                Width = 40,
                Height = 40

            };

            comboBox.SetSource(mc.Rovers.Select(x => x.Id).ToList());


            var roverPosition = new ResponseLabel()
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 3,
            };

            comboBox.SelectedItemChanged += (e) =>
            {
                selectedRover = mc.GetRoverById((ulong)comboBox.SelectedItem);
                roverPosition.Text = selectedRover.ToString();
            };

            var responseLabel = new ResponseLabel()
            {
                X = xAlignment,
                Y = Pos.Center() + 4,
                TextAlignment = TextAlignment.Right,
            };



            this.KeyDown += (e) =>
            {
                Instructions inputInstruction = (e.KeyEvent.Key) switch
                {
                    Key.CursorLeft => Instructions.L,
                    Key.CursorRight => Instructions.R,
                    Key.CursorUp => Instructions.M,
                    Key.CursorDown => Instructions.B,
                    _ => (Instructions)(-1),
                };

                if (inputInstruction != (Instructions)(-1))
                {

                    e.Handled = true;
                    XYPosition oldPos = selectedRover.Position;
                    mc.RunInstructions(selectedRover, inputInstruction);

                    if (inputInstruction == Instructions.M || inputInstruction == Instructions.B) {
                        displayGrid.MoveObjectTo(selectedRover.Id, oldPos, selectedRover.Position, mc.GetObjectByPosition(oldPos));
                        this.SetNeedsDisplay();
                    }

                    roverPosition.Text = selectedRover.ToString();
                   
                    if (selectedRover.Position == mc.EndOfLevel)
                    {
                        stopTimeout = true; 
                        App.SwitchToNextLevel(new FirstLevel(App));
                    }
                    else if (!mc.AreRoversIntact())
                    {
                        App.SwitchToNextLevel(new EndLevel(App));
                    } 
                }
            };


            Add(textLabel, timerLabel, displayGrid, comboBox, comboBoxLabel, roverPosition, responseLabel);

        }

    }
}