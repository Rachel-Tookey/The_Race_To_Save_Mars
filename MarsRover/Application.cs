using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Models;
using MarsRover.States; 

namespace MarsRover
{
    public class Application
    {
        private bool _isRunning = false; 
        public IState CurrentState { get; set; }

        // OR MISSION CONTROL CLASS HERE? 
        public Plateau Plateau { get; set; }

        public Rover UserRover { get; set; }

        public Application()
        {
            CurrentState = new StartupState(this);
        }

        public void Run()
        {
            _isRunning = true;
            Console.WriteLine("Welcome to Revenge of the Musk");
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
