using MarsRover.Enums;
using MarsRover.Input.ParserModels;


namespace MarsRover
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Terminal.Gui.Application.Init();
            var top = Terminal.Gui.Application.Top;
            Application application = new Application(top);
            application.Run();

        }
    }
}
