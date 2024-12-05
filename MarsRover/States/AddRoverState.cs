using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.States
{
    internal class AddRoverState : IState 
    {
        public Application _application;

        public AddRoverState(Application application)
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
            Console.WriteLine("Let's add some rovers!");
            Boolean IsUserAdding = true;

            while (IsUserAdding)
            {
                RoverParser userRP = new(GetUserInput("Please provide starting position and direction for your rover: name x y d"),
                    _application.MissionControl.Plateau);
                while (!userRP.Success)
                {
                    Console.WriteLine(userRP.Message);
                    userRP = new(GetUserInput("Please provide starting position and direction for your rover: name x y d"),
                    _application.MissionControl.Plateau);
                }
                _application.MissionControl.AddRover(userRP.Result);

                String userAdding = GetUserInput("Press enter to stop adding rover. Press any key to continue");
                IsUserAdding = userAdding == "" ? false : true; 
            }

            _application.CurrentState = new MoveState(_application);
        }

    }
}
