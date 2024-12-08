using MarsRover.LogicLayer.Models;
using MarsRover.UILayerTG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using static System.Net.Mime.MediaTypeNames;

namespace MarsRover
{
    public class GameApplication
    {
        public Window CurrentWindow { get; set; }

        public MissionControl MissionControl { get; set; }

        public Toplevel Toplevel { get; set; }

        public void Run()
        {

            Terminal.Gui.Application.Init();

            Toplevel = Terminal.Gui.Application.Top;

            CurrentWindow = new OpeningLevel(this).WindowRun();

            Toplevel.Add(CurrentWindow);

            Terminal.Gui.Application.Run();


        }

        public void SwitchToNextLevel(ILevel currentLevel)
        {
            Toplevel.Remove(CurrentWindow);

            CurrentWindow = currentLevel.WindowRun();

            Toplevel.Add(CurrentWindow);

            Terminal.Gui.Application.Refresh();

        }



    }
}
