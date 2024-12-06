using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UILayer.States
{
    public class EndState : IState
    {
        public Application _application;

        public EndState(Application application)
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
            Console.WriteLine("This was a console app by Tookles");
            Console.WriteLine("The End"); 
        }

    }
}
