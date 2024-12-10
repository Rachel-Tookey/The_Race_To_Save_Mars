using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
using MarsRover.UILayerTG;
using System.Text;


namespace MarsRover
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Terminal.Gui.Application.Init();

            GameApplication game = new GameApplication();

            Terminal.Gui.Application.Run();
            Terminal.Gui.Application.Shutdown(); 

        }
    }
}
