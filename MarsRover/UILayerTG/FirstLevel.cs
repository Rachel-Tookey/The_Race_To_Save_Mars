using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class FirstLevel : ILevel
    {
        public GameApplication Application { get; set; }

        public FirstLevel(GameApplication game)
        {

            Application = game;
        }

        public Window WindowRun()
        {
            var openingWindow = new Terminal.Gui.Window("Level One")
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
