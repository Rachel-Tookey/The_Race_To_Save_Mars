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
        public EndLevel(GameApplication game) : base("Game Over", game)
        {
            AddUI();
        }

        public override void AddUI()
        {
            App.Stop();

        }


    }

}
