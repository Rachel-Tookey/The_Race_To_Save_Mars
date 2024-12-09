using MarsRover.Enums;
using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayerTG.Utils; 

namespace MarsRover.UILayerTG
{
    public class OpeningLevel : ILevel 
    {

        public GameApplication Application { get; set; }

        public OpeningLevel(GameApplication game)
        {

            Application = game;
        }

        public Window GetWindow()
        {

            var openingWindow = new StyledWindow("Race to Save Mars");

            var introLabel = new StyledLabel(Text.GetLevelText("Opening Level"))
            {
                X = Pos.Center(),
                Y = Pos.Center() - 2,
            };

            var nextButton = new Terminal.Gui.Button("Are you ready to save Mars?")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(introLabel) + 4,
            };

            nextButton.Clicked += () =>
            {
                Application.SwitchToNextLevel(new AddRoversLevel(Application));
            };

            openingWindow.Add(introLabel, nextButton);

            return openingWindow;   
        }

    }
}
