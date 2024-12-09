using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MarsRover.UILayerTG.Utils;

namespace MarsRover.UILayerTG
{
    public class FirstLevel : StyledWindow
    {

        public GameApplication Application { get; set; }

        public FirstLevel(GameApplication game) : base("Level One")
        {
            Application = game;
            InitialiseLevel();
        }

        public void InitialiseLevel()
        {

        }


    }

}
