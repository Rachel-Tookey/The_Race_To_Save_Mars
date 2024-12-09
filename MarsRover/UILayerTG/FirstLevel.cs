using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayerTG.Utils;

namespace MarsRover.UILayerTG
{
    public class FirstLevel : ILevel
    {
        public GameApplication Application { get; set; }

        public FirstLevel(GameApplication game)
        {

            Application = game;
        }

        public Window GetWindow()
        {
            var openingWindow = new StyledWindow("Level One");

            return openingWindow;
        }

    }
}
