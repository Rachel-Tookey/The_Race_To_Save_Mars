using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui; 

namespace MarsRover.UILayerTG.Utils
{
    public class StyledLabel : Terminal.Gui.Label
    {
        public FrameView Frame {  get; private set; }

        public StyledLabel(string text) : base()
        {

            Text = text; 
            ColorScheme = new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
            };
            Frame = new FrameView()
            {
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.BrightMagenta)
                }

            };
            
        }



    }


}
