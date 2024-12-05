using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input.ParserModels;
using MarsRover.Models;

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
            Console.WriteLine(request);
            string? userInput = Console.ReadLine();
            return userInput != null ? userInput : "";
        }

        public void Run()
        {
            Console.WriteLine("Are you ready to play?");
            Console.WriteLine("Let's get some user inputs...");

            PlateauSizeParser userPSP = new (GetUserInput("How big do you want your plateau? Format: 'x y'"));
            while (!userPSP.Success)
            {
                Console.WriteLine(userPSP.Message);
                userPSP = new (GetUserInput("How big do you want your plateau? Format: 'x y'"));
            }

            _application.Plateau = userPSP.Result != null ? userPSP.Result as Plateau : new Plateau(5, 5);


            RoverParser userRP = new (GetUserInput("Please provide starting position and direction for your rover: x y d"), _application.Plateau);
            while (!userRP.Success) {
                Console.WriteLine(userRP.Message);
                userRP = new (GetUserInput("Please provide starting position and direction for your rover: x y d"), _application.Plateau
                 );
            }

            _application.UserRover = userRP.Rover != null ? userRP.Rover as Rover : new Rover(new Position(2, 2, Enums.Facing.NORTH));

            Console.WriteLine("Thank you. Let's begin!");

            _application.CurrentState = new MoveState(_application); 

        }

    }
}
