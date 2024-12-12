using MarsRover.Enums;
using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayerTG.Utils;
using MarsRover.UILayer.Superclasses;

namespace MarsRover.UILayerTG
{
    public class OpeningLevel : StyledWindow  
    {

        public GameApplication App { get; set; }

        public OpeningLevel(GameApplication game) : base("Race to Save Mars")
        {
            App = game;
            AddUI(); 
        }

        public void AddUI() { 

            var introLabel = new StyledLabel(Utils.Text.GetLevelText("Opening Level"))
            {
                X = Pos.Center(),
                Y = Pos.Center() - 2,
            };

            var nextButton = new Button()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(introLabel) + 4,
                Text = "Are you ready to save Mars?"
            };

            nextButton.MouseClick += (s, e) =>
            {
                App.SwitchToNextLevel(new AddRoversLevel(App));
            };        

            Add(introLabel, nextButton);

    }
}
}
