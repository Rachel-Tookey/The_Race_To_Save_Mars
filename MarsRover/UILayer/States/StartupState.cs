using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using Sharprompt;

namespace MarsRover.UILayer.States
{
    public class StartupState : IState
    {
        public Application _application;

        public StartupState(Application application)
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

            Console.WriteLine("A billionaire has landed on Mars...");
            Console.ReadLine();
            Console.WriteLine("He intends to take the riches of Mars for himself...");
            Console.ReadLine();
            Console.WriteLine("There is only one person who can save Mars...");
            Console.ReadLine();
            Console.WriteLine("(We mean you.)");
            Console.ReadLine();
            bool result = Prompt.Confirm("Are you ready to save Mars?", defaultValue: true);
            if (!result)
            {
                Console.WriteLine("See you next time then.");
                _application.Stop() ;
            } else
            {
                Console.Clear();
            

            PlateauSizeParser userPSP = new(GetUserInput("Set the size of the plateau. Format: 'x y'"));

            while (!userPSP.Success)
            {
                Console.WriteLine(userPSP.Message);
                userPSP = new(GetUserInput("Set the size of the plateau. Format: 'x y'"));
            }

            _application.MissionControl = new MissionControl(userPSP.Result);


            _application.CurrentState = new AddRoverState(_application);
            }
        }

    }
}
