using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using Sharprompt;
using Terminal.Gui;

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

            Toplevel top = _application.Toplevel; 

            top.RemoveAll();

            var window = new Terminal.Gui.Window("Race to Save Mars") {
                X=0,
                Y=0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),    
            
            };

            string literal = """
                A billionaire has landed on Mars...
                There is only one person who can save it...
                (We mean you)
                """;


            var label = new Terminal.Gui.Label(literal) {
                X = Pos.Center(),
                Y = Pos.Center() - 1,

            };

            window.Add(label);

            var nextButton = new Terminal.Gui.Button("Are you ready to save Mars?") {
                X = Pos.Center(),
                Y = Pos.Center() + 3,

            };

            nextButton.Clicked += () =>
            {
                _application.CurrentState = new AddRoverState(_application);
            };

            window.Add(nextButton); 

            top.Add(window);

            Terminal.Gui.Application.Run();
          

            //Console.Clear();

            //PlateauSizeParser userPSP = new(GetUserInput("Set the size of the plateau. Format: 'x y'"));

            //while (!userPSP.Success)
            //{
            //    Console.WriteLine(userPSP.Message);
            //    userPSP = new(GetUserInput("Set the size of the plateau. Format: 'x y'"));
            //}

            //_application.MissionControl = new MissionControl(userPSP.Result);

        }

    }
}
