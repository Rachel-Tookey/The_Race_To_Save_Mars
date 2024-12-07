using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Terminal.Gui.Graphs;
using static System.Net.Mime.MediaTypeNames;

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

            //XYPosition randomPos = Application.MissionControl.PositionGenerator();
            //Application.MissionControl.AddObject(new ChargingStation(randomPos));

            //string[,] myGrid = Application.MissionControl.GetGrid();

            //int startX = 2;
            //int startY = 2;
            //for (int i = 0; i < myGrid.GetLength(0); i++)
            //{
            //    for (int j = 0; j < myGrid.GetLength(1); j++)
            //    {
            //        var colorSet = new ColorScheme
            //        {
            //            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Black)
            //        };

            //        if (myGrid[i, j] == "X")
            //        {
            //            colorSet = new ColorScheme
            //            {
            //                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black)
            //            };

            //        }
            //        else if (myGrid[i, j] == "_")
            //        {
            //            colorSet = new ColorScheme
            //            {
            //                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black)
            //            };

            //        }
            //        else if (myGrid[i, j] == "$")
            //        {
            //            colorSet = new ColorScheme
            //            {
            //                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black)
            //            };
            //        }

            //        var label = new Label(myGrid[i, j])
            //        {
            //            X = startX + j,
            //            Y = startY + i,
            //            Height = 1,
            //            ColorScheme = colorSet
            //        };

            //        openingWindow.Add(label);
            //    }
            //}

            var graphView = new GraphView
            {
                X = 2,
                Y = 2,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

            };

            (int, int)[] getData = Application.MissionControl.GetRoverPositions().Select(x => x.Value).ToArray();

            List<PointF> myPoints = new List<PointF>(); 
            foreach (var item in getData)
            {
                Terminal.Gui.PointF myPoint = new Terminal.Gui.PointF(item.Item1, item.Item2);
                myPoints.Add(myPoint);

            }

            graphView.Series.Add(new ScatterSeries() { Points =  myPoints});

            graphView.CellSize = new PointF(1, 5);

            graphView.AxisX.Increment = 1;
            graphView.AxisX.ShowLabelsEvery = 5;
            graphView.AxisY.Increment = 1;
            graphView.AxisY.ShowLabelsEvery = 5;

            graphView.AutoSize = true;

            openingWindow.Add(graphView);

            graphView.SetNeedsDisplay();


            return openingWindow; 
        }

    }
}
