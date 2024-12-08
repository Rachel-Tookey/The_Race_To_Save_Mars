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

            string literal = """
                Elon Musk has landed on Mars intending to take its resources for himself...
                There is only one person who can save it...
                (We mean you)
                """;


            var label = new Terminal.Gui.Label(literal)
            {
                X = Pos.Center(),
                Y = Pos.Center() - 1,

            };

            openingWindow.Add(label);

            var nextButton = new Terminal.Gui.Button("Are you ready to save Mars?")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 3,
            };


            nextButton.Clicked += () =>
            {
                Application.SwitchToNextLevel(new AddRoversLevel(Application));
            };

            openingWindow.Add(nextButton);

            return openingWindow;   
        }

    }
}
