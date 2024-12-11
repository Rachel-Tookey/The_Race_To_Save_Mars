using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayer.Superclasses;


namespace MarsRover.UILayerTG
{
    public class EndLevel : StyledWindow 
    {
        public GameApplication App { get; set; }

        public EndLevel(GameApplication game) : base("Game Over")
        {
            App = game;
            InitialiseLevel();
        }

        public void InitialiseLevel()
        {
            App.Stop();

        }


    }

}
