using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class LevelFour
    {
        public GameApplication Application { get; set; }

        public Window Window { get; set; }

        public LevelFour(GameApplication game)
        {

            Application = game;
            Window = WindowRun();
        }

        public Window WindowRun()
        {
            var openingWindow = new Terminal.Gui.Window("Level Four")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            return openingWindow;
        }

    }
}
