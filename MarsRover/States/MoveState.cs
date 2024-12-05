using MarsRover.Enums;
using MarsRover.LogicLater.Models;
using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.States
{
    public class MoveState : IState
    {
        public Application _application;

        public MoveState(Application application)
        {
            _application = application;
        }

        public string GetUserInput(string request)
        {
            Console.WriteLine(request);
            string? userInput = Console.ReadLine();
            return userInput != null ? userInput : "";
        }

        public void Run()
        {
            Console.WriteLine("Let's get playin'");

            foreach (Rover rover in _application.MissionControl.Rovers)
            {
                Console.WriteLine($"Rover {rover.Id} {rover.Name} is at {rover.Position.ToString()}");
                InstructionParser userIP = new (GetUserInput("How do you want to move? i.e. LLRM"));

                while (!userIP.Success)
                    {
                    Console.WriteLine(userIP.Message);
                    userIP = new (GetUserInput("How do you want to move? i.e. LLRM"));
                    }
                
                List<Instructions> userInstructions = userIP.Result;

                _application.MissionControl.RunInstructions(rover, userInstructions);

                Console.WriteLine($"Rover {rover.Id} {rover.Name} is now at {rover.Position.ToString()}");


            }

            _application.CurrentState = new DisplayGrid(_application);

        }

    }
}
