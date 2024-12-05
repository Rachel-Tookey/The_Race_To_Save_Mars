using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLater.Models;
using Sharprompt;

namespace MarsRover.States
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
            Prompt.ColorSchema.Select = ConsoleColor.DarkGreen;
            Prompt.ColorSchema.Answer = ConsoleColor.Cyan;
            string? userInput = Prompt.Input<string>(request);
            return userInput != null ? userInput : "";
        }

        public void Run()
        {
            
            Prompt.Confirm("Are you ready to explore Mars?", defaultValue: true);


            PlateauSizeParser userPSP = new (GetUserInput("Set the size of the plateau. Format: 'x y'"));
            while (!userPSP.Success)
            {
                Console.WriteLine(userPSP.Message);
                userPSP = new (GetUserInput("Set the size of the plateau. Format: 'x y'"));
            }

            _application.MissionControl = new MissionControl(userPSP.Result);

        
            _application.CurrentState = new AddRoverState(_application); 

        }

    }
}
