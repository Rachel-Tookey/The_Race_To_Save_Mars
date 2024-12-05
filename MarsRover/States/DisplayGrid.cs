using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MarsRover.LogicLater.Models;
using Spectre.Console; 

namespace MarsRover.States
{
    internal class DisplayGrid : IState 
    {
        public Application _application;

        public DisplayGrid(Application application)
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
            var grid = new Grid();

            Plateau plateau = _application.MissionControl.Plateau;
            char[,] plat = _application.MissionControl.Plateau.Grid;

            plat[15, 15] = 'X';  

            for (int cols = 0; cols < plateau._y; cols++)
            {
                grid.AddColumn();
            }


            for (int rows = 0; rows < plateau._x; rows++)
            {
                Text[] gridContents = new Text [plateau._y];
                for (int cols = 0; cols < plateau._y; cols++)
                {
                    gridContents[cols] = new Text( $"{plat[rows,cols]}", new Style(Color.Red, Color.Black));
                }
                grid.AddRow(gridContents);
            }

            AnsiConsole.Write(grid);

            _application.Stop(); 
        }

    }
}
