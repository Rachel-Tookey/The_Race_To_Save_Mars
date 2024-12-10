using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG.Utils
{
    public class ResponseLabel : Terminal.Gui.Label 
    {
        public ResponseLabel() : base()
        {
            ColorScheme = new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
            };
        }
    }
}
