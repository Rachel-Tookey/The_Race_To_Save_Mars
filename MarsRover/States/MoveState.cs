using MarsRover.Enums;
using MarsRover.LogicLater.Models;
using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharprompt;

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
            string? userInput = Prompt.Input<string>(request);
            return userInput != null ? userInput : "";
        }

        public void Run()
        {

            foreach (Rover rover in _application.MissionControl.Rovers)
            {
                if (rover.IsIntact)
                {

                    _application.MissionControl.DisplayGrid();
                    Console.WriteLine($"Rover {rover.Id} is at {rover.Position.ToString()}");

                //var instructions = Prompt.MultiSelect("Choose your moves", new[] { Instructions.L, Instructions.R, Instructions.M, Instructions.L, Instructions.R, Instructions.M, Instructions.L, Instructions.R, Instructions.M }, pageSize: 3);

                    InstructionParser userIP = new(GetUserInput("How do you want to move? i.e. LLRM"));

                    while (!userIP.Success)
                    {
                        Console.WriteLine(userIP.Message);
                        userIP = new(GetUserInput("How do you want to move? i.e. LLRM"));
                    }

                    List<Instructions> userInstructions = userIP.Result;
                    
                    _application.MissionControl.RunInstructions(rover, userInstructions);
                    
                    _application.MissionControl.DisplayGrid();
                    
                    if (!rover.IsIntact)
                    {
                        Console.WriteLine($"Rover {rover.Id} is destroyed");
                    } else
                    {
                        Console.WriteLine($"Rover {rover.Id} is now at {rover.Position.ToString()}");
                    }

                }
            }

            _application.Stop();

        }

    }
}
