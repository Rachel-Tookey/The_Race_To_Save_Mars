using MarsRover.Enums;
using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class OpeningLevel : ILevel 
    {

        public GameApplication Application { get; set; }

        public OpeningLevel(GameApplication game)
        {

            Application = game;
        }

        public Window WindowRun()
        {

            var openingWindow = new Terminal.Gui.Window("Race to Save Mars")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            string introText = """
                An alien race has landed on Mars intending to take its resources for themselves...
                The group of cyberpunks and Martians form a rebel alliance to fight back.
                They put a call-out for an aspiring software developer to pilot their rovers.
                You stop your job hunting on LinkedIn and think 'Hey, I could do that!' 
                """;


            var label = new Terminal.Gui.Label(introText)
            {
                X = Pos.Center(),
                Y = Pos.Center() - 4,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
                },
                Border = new Terminal.Gui.Border()
                {
                    BorderStyle = BorderStyle.Single,
                    Padding = new Thickness(0),
                    BorderBrush = Color.BrightMagenta,
                    Background = Color.Black,
                }

            };


            var nextButton = new Terminal.Gui.Button("Are you ready to save Mars?")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 3,
            };



            nextButton.Clicked += () =>
            {
                Application.SwitchToNextLevel(new AddRoversLevel(Application));
            };

            openingWindow.Add(label, nextButton);

            return openingWindow;   
        }

    }
}
