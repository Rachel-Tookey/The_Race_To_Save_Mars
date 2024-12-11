using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models.Superclasses
{
    public abstract class Item
    {
        public bool IsUsed = false;

        public XYPosition Position;

    }
}
