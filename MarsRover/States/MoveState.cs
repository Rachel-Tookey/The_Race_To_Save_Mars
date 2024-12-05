using MarsRover.Enums;
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

            InstructionParser userIP = new (GetUserInput("How do you want to move? i.e. LLRM"));

            while (!userIP.Success)
            {
                Console.WriteLine(userIP.Message);
                userIP = new (GetUserInput("How do you want to move? i.e. LLRM"));
            }
            List<Instructions> userInstructions = userIP.Result;

            foreach (Instructions instruction in userInstructions)
            {
                Console.WriteLine(instruction);
            }

            _application.Stop();
        }

    }
}
