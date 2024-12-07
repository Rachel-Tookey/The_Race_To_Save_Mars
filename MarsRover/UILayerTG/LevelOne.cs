using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.LogicLayer.Models;

namespace MarsRover.UILayerTG
{
    public class LevelOne 
    {
        public GameApplication Application { get; set; }

        public Window Window { get; set; }

        public LevelOne(GameApplication game) {

            Application = game;        
            Window = WindowRun();
        }

        public Window WindowRun()
        {
            var openingWindow = new Terminal.Gui.Window("Build your plateau")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            var label = new Terminal.Gui.Label("Please enter the size of your plateau:")
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

            submitButton.Clicked += () =>
            {
                PlateauSizeParser userInput = new PlateauSizeParser(textField.Text.ToString());
                if (userInput.Success)
                {
                    Application.MissionControl = new MissionControl(userInput.Result);
                    Application.SwitchToNextLevel(new LevelTwo(Application).Window);
                }
                else
                {
                    responseLabel.Text = userInput.Message.ToString();
                    responseLabel.SetNeedsDisplay();
                }

            };

            openingWindow.Add(submitButton);
            openingWindow.Add(responseLabel);

            return openingWindow;

        }

    }
}
