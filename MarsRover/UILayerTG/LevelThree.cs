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

            int startX = 2;
            int startY = 2; 
            for (int i = 0; i < myGrid.GetLength(0); i++)
            {
                for (int j = 0; j < myGrid.GetLength(1); j++)
                {
                    var colorSet = new ColorScheme
                    {
                        Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Black)
                    };

                    if (myGrid[i, j] == "X")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black)
                        };

                    }
                    else if (myGrid[i, j] == "_")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black)
                        };

                    }

                    var label = new Label(myGrid[i, j])
                    {
                        X = startX + j,
                        Y = startY + i,
                        //Width = 4,
                        Height = 1,
                        ColorScheme = colorSet
                    };

                    openingWindow.Add(label); 
                }
            }



            return openingWindow; 
        }

    }
}
