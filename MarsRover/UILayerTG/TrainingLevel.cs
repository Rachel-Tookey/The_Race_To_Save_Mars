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


namespace MarsRover.UILayerTG
{
    public class TrainingLevel : ILevel
    {
        public GameApplication Application { get; set; }

        public TrainingLevel(GameApplication game)
        {

            Application = game;
        }


        public Window GetWindow()
        {
            int xAlignment = 70;

            MissionControl missionControl = Application.MissionControl;
            
            // adding the objects of a class? 
            // where should this happen? 
            XYPosition randomPos = missionControl.PositionGenerator();
            missionControl.AddObject(new Hole(randomPos));

            Rover selectedRover = missionControl.Rovers[0];


            var displayGrid = Views.GetGrid(missionControl.GetGrid());

            var openingWindow = new StyledWindow("Training Level");


            var textLabel = new StyledLabel(Text.GetLevelText("Training Level"))
            {
                X = xAlignment,
                Y = 3,
                Width = 40,
            };


            int seconds = 180 / missionControl.Rovers.Count();

            var timerLabel = new Label($"Time left: {seconds}s")
            {
                X = xAlignment,
                Y = Pos.Bottom(textLabel) + 2,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.White)
                },
            };

            Terminal.Gui.Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), _ =>
            {
                seconds--;
                if (seconds > 0)
                {
                    timerLabel.Text = $"Time left: {seconds}s";
                    return true;
                }
                else
                {
                    Application.SwitchToNextLevel(new EndLevel(Application));
                    return false;
                }
            });


            var comboBoxLabel = new Label("Select a rover:")
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

            comboBox.SetSource(missionControl.Rovers.Select(x => x.Id).ToList());


            var roverPosition = new ResponseLabel()
            {
                X = xAlignment,
                Y = Pos.Bottom(comboBoxLabel) + 3,
            };



            comboBox.SelectedItemChanged += (e) =>
            {
                selectedRover = missionControl.GetRoverById((ulong)comboBox.SelectedItem);
                roverPosition.Text = selectedRover.ToString();
            };



            var responseLabel = new ResponseLabel()
            {
                X = xAlignment,
                Y = Pos.Center() + 4,
                TextAlignment = TextAlignment.Right,
            };


            openingWindow.KeyDown += (e) =>
            {
                Instructions inputInstruction = (Instructions) (-1);
                if (e.KeyEvent.Key == Key.CursorLeft)
                {
                    inputInstruction = Instructions.L;
                } 
                else if (e.KeyEvent.Key == Key.CursorRight)
                {
                    inputInstruction = Instructions.R;
                }
                else if (e.KeyEvent.Key == Key.CursorUp)
                {
                    inputInstruction = Instructions.M;
                } else if (e.KeyEvent.Key == Key.CursorDown)
                {
                    inputInstruction = Instructions.B;
                } 

                if (inputInstruction != (Instructions)(-1))
                {
                    e.Handled = true;
                    missionControl.RunInstructions(selectedRover, inputInstruction);
                    openingWindow.Remove(displayGrid);
                    displayGrid = Views.GetGrid(missionControl.GetGrid());
                    openingWindow.Add(displayGrid);
                    openingWindow.SetNeedsDisplay();
                    roverPosition.Text = selectedRover.ToString();

                    if (selectedRover.Position == missionControl.Hole.Position)
                    {
                        Application.SwitchToNextLevel(new FirstLevel(Application));
                    }
                    else if (!missionControl.AreRoversIntact())
                    {
                        Application.SwitchToNextLevel(new EndLevel(Application));
                    }

                }

            };

           
            openingWindow.Add(timerLabel, displayGrid, comboBox, comboBoxLabel, roverPosition, textLabel, responseLabel);


            return openingWindow;
        }

    }
}
