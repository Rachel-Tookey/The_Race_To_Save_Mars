using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    internal class Rover
    {
        public string Name { get; set; }

        public Position Position { get; set; }

        public Rover(Position position) {
            Position = position;
        }
    }
}
