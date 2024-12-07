using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class LevelThree
    {
        public GameApplication Application { get; set; }

        public Window Window { get; set; }

        public LevelThree(GameApplication game)
        {

            Application = game;
            Window = WindowRun();
        }

        public Window WindowRun()
        {
            var openingWindow = new Terminal.Gui.Window("Display Grid")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            string[,] myGrid = Application.MissionControl.GetGrid();

            int startX = 10;
            int startY = 2; 
            for (int i = myGrid.GetLength(0) - 1;  i >= 0; i--)
            {
                for (int j = 0; j < myGrid.GetLength(1); j++)
                {
                    var label = new Label(myGrid[i, j])
                    {
                        X = startX + j,
                        Y = startY + i,
                        Width = 4,
                        Height = 1
                    };
                    openingWindow.Add(label); 
                }
            }




            return openingWindow; 
        }

    }
}
