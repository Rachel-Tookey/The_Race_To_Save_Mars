using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui; 

namespace MarsRover.UILayerTG.Utils
{
    public class StyledLabel : Terminal.Gui.Label 
    {
        public StyledLabel(string text) : base(text)
        {
            ColorScheme = new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
            };
            Border = new Terminal.Gui.Border()
            {
                BorderStyle = BorderStyle.Single,
                Padding = new Thickness(0),
                BorderBrush = Color.BrightMagenta,
                Background = Color.Black,
            };
        }
    }
}
