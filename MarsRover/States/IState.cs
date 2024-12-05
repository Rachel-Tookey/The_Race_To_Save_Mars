using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.States
{
    public interface IState
    {
        public void Run();

        public string GetUserInput(string request);

    }


}
