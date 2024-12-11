using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayer.Superclasses
{
    public abstract class StyledWindow : Window
    {
        public StyledWindow(string text) : base(text)
        {
            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();

        }
    }
}
