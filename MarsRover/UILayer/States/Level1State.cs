using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UILayer.States
{
    public class Level1State : IState
    {
        public Application _application;

        public Level1State(Application application)
        {
            _application = application;
        }

        public string GetUserInput(string request)
        {
            string? userInput = Prompt.Input<string>(request);
            Console.Clear();
            return userInput != null ? userInput : "";
        }

        public void Run()
        {
            Console.WriteLine("Level 1"); 

            _application.Stop();
        }
    }
}
