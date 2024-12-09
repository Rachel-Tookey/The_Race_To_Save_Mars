using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayerTG.Utils;


namespace MarsRover.UILayerTG
{
    public class EndLevel : StyledWindow 
    {
        public GameApplication Application { get; set; }

        public EndLevel(GameApplication game) : base("Game Over")
        {
            Application = game;
            InitialiseLevel();
        }

        public void InitialiseLevel()
        {
            Application.Stop();

        }


    }

}
