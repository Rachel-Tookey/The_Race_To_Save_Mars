using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using static System.Net.Mime.MediaTypeNames;

namespace MarsRover.UILayerTG
{
    public class GameApplication
    {
        public Window CurrentWindow { get; set; }   

        public MissionControl MissionControl {  get; set; } 

        public Toplevel Toplevel { get; set; }  


        public void Run()
        {

            Terminal.Gui.Application.Init();

            Toplevel = Terminal.Gui.Application.Top;

            var openingWindow = new Terminal.Gui.Window("Race to Save Mars")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            string literal = """
                Elon Musk has landed on Mars intending to take its resources for himself...
                There is only one person who can save it...
                (We mean you)
                """;


            var label = new Terminal.Gui.Label(literal)
            {
                X = Pos.Center(),
                Y = Pos.Center() - 1,

            };

            openingWindow.Add(label);

            var nextButton = new Terminal.Gui.Button("Are you ready to save Mars?")
            {
                X = Pos.Center(),
                Y = Pos.Center() + 3,
            };


            nextButton.Clicked += () =>
            {
                SwitchToNextLevel(new LevelOne(this).Window); 
            };

            openingWindow.Add(nextButton);

            CurrentWindow = openingWindow;

            Toplevel.Add(CurrentWindow);

            Terminal.Gui.Application.Run();


        }

        public void SwitchToNextLevel(Window window)
        {
            Toplevel.Remove(CurrentWindow);

            CurrentWindow = window;

            Toplevel.Add(CurrentWindow);

            Terminal.Gui.Application.Refresh(); 

        }



    }
}
