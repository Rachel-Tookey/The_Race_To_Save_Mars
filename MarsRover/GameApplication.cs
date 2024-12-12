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

        public User User { get; set; }

        public GameApplication()
        {
            User = new User();

            MissionControl = new MissionControl(new Plateau(60, 20));

            CurrentWindow = new OpeningLevel(this); 

            SwitchToNextLevel(CurrentWindow); 
        }

     
        public void SwitchToNextLevel(Window nextWindow)
        {
            if (Terminal.Gui.Application.Top != null)
            {
                Terminal.Gui.Application.Top.RemoveAll();

            }

            CurrentWindow = nextWindow; 

            Terminal.Gui.Application.Top.Add(CurrentWindow);

            Terminal.Gui.Application.Refresh();

        }

        public void Stop()
        {
            Terminal.Gui.Application.RequestStop();
        }



    }
}
