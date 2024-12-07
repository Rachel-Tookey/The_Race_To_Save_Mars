using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class LevelTwo
    {
        public GameApplication Application { get; set; }

        public Window Window { get; set; }

        public LevelTwo(GameApplication game)
        {

            Application = game;
            Window = WindowRun();
        }

        public Window WindowRun()
        {
            var openingWindow = new Terminal.Gui.Window("Add your rovers")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };


            var label = new Terminal.Gui.Label("Enter a starting position:")
            {
                X = Pos.Center(),
                Y = Pos.Center(),

            };

            openingWindow.Add(label);

            var textField = new TextField("x y")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 4,
                Width = 40

            };

            openingWindow.Add(textField);

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 5,
            };


            var responseLabel = new Label()
            {
                X = Pos.Center(),
                Y = Pos.Center() + 2,
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
                Width = Dim.Fill()
            };

            var comboLabel = new Terminal.Gui.Label("Enter a starting direction:")
            {
                X = Pos.Center(),
                Y = 1,

            };

            openingWindow.Add(comboLabel);

            var responseCBLabel = new Label()
            {
                X = Pos.Center(),
                Y = 2,
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                },
                Width = Dim.Fill()
            };




            var comboBox = new ComboBox()
            {
                X = Pos.Center(),
                Y = 2,
                Width = 40,
                Height = 40, 
            };

            comboBox.SetSource(new List<Facing>() { Facing.NORTH, Facing.EAST, Facing.SOUTH, Facing.WEST });
      
            openingWindow.Add(submitButton);
            openingWindow.Add(responseLabel);
            openingWindow.Add(comboBox);


            submitButton.Clicked += () =>
            {
                Facing selectedEnum = (Facing)comboBox.SelectedItem;
                if ((int)selectedEnum < 0)
                {
                    responseLabel.Text = "Please select a direction";
                    responseLabel.SetNeedsDisplay();
                }
                else { 
                    RoverParser userInput = new RoverParser(textField.Text.ToString(), selectedEnum, Application.MissionControl.Plateau);
                    if (userInput.Success)
                    {
                        Application.MissionControl.AddObject(userInput.Result);
                        if (Application.MissionControl.Rovers.Count < 3)
                        {
                            var result = MessageBox.Query("Continue?", "Do you wish to add any more rovers?", buttons: ["Yes", "No"]);
                            if (result == 0)
                            {
                                Application.SwitchToNextLevel(new LevelTwo(Application).Window);
                            }
                        }
                        Application.SwitchToNextLevel(new LevelThree(Application).Window);
                    }
                    else
                    {
                        responseLabel.Text = userInput.Message.ToString();
                        responseLabel.SetNeedsDisplay();
                     }
                    }

            };

            openingWindow.Add(submitButton);
            openingWindow.Add(responseLabel);


            return openingWindow;


        }
    }
}
