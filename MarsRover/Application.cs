using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayer.States;
using Sharprompt;
using Terminal.Gui;

namespace MarsRover
{
    public class Application
    {
        private bool _isRunning = false; 
        public IState CurrentState { get; set; }
        public MissionControl MissionControl { get; set; }

        public Toplevel Toplevel { get; set; }

        public Application(Toplevel top)
        {
            CurrentState = new StartupState(this);
            Toplevel = top; 
        }

        public void Run()
        {

            _isRunning = true;
            while (_isRunning)
            {
                CurrentState.Run();
            }

        }

        public void Stop()
        {
            _isRunning = false;
            Console.WriteLine("Thank you for playing");
        }

    }
}
