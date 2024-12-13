using MarsRover.UILayer.Superclasses;
using MarsRover.UILayerTG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UILayer
{
    public class SecondLevel : GameLevel
    {
        public SecondLevel(GameApplication game) : base("Level Two", game, 180)
        {

            DisplayGrid = new GridView(1, App.MissionControl.GetGrid());

            AddUI();

            SetTimer();
        }

        public override void AddUI()
        {
            
        }

        public override void LevelUp()
        {
            
        }

    }
}
