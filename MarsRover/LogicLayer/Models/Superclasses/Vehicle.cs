using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models.Superclasses
{
    public abstract class Vehicle
    {
        ulong Id;

        XYPosition Position;

        Facing Direction;

        bool IsIntact;

        int Acceleration;

    }
}
