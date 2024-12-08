using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class EndLevel : ILevel 
    {
        public GameApplication Application { get; set; }

        public EndLevel(GameApplication game)
        {

            Application = game;
        }

        public Window WindowRun()
        {

            var openingWindow = new Terminal.Gui.Window("Game Over")
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
