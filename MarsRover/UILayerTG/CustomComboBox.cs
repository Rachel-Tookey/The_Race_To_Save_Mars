using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class CustomComboBox : ComboBox
    {

        public override bool ProcessKey(KeyEvent e)
        {
            if (e.Key == Key.CursorLeft || e.Key == Key.CursorRight)
            {
                return false;
            }
            return base.ProcessKey(e);
        }
    }
}
