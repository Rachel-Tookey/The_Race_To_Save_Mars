using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayer.States;
using Sharprompt;

namespace MarsRover
{
    public class Application
    {
        private bool _isRunning = false; 
        public IState CurrentState { get; set; }
        public MissionControl MissionControl { get; set; }

        public Application()
        {
            CurrentState = new StartupState(this);
        }

        public void Run()
        {
            _isRunning = true;
            Console.WriteLine("Welcome to The Race For Mars. Press Enter to continue.");
            Console.ReadLine(); 
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
