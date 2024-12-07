using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayerTG;


namespace MarsRover
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var game = new GameApplication();
            game.Run();

            //Plateau newplat = new Plateau(10, 10);

            //MissionControl mc = new MissionControl(newplat);

            //Position newPosition = new Position(3, 3, Facing.NORTH);

            //Rover newRov = new Rover(newPosition);

            //mc.AddObject(newRov);

            //string[,] myGrid = mc.GetGrid();

            //Console.WriteLine(myGrid.GetLength(0));
            //Console.WriteLine(myGrid.GetLength(1));

            //foreach (string grid in myGrid)
            //{
            //    Console.WriteLine(grid);
            //}

        }
    }
}
