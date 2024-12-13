using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarsRover.Enums;
using MarsRover.LogicLayer.Models;

namespace MarsRover.Input.ParserModels
{
    public class RoverParser
    {
        public bool Success { get; set; } = false;

        public List<Rover> Result { get; set; } = new List<Rover>();

        public string Message { get; set; } = "";

        public RoverParser(int numb, String userInput, Facing direction, MissionControl mc)
        {
            if (numb != -1)
            {
                if ((int)direction != -1)
                {
                    Regex userPattern = new Regex("^[0-9]+,\\s[0-9]+$");
                    if (userPattern.IsMatch(userInput))
                    {
                        string[] userInputArray = userInput.Split(", ");
                        int xAxis = Int32.Parse(userInputArray[0]);
                        int yAxis = Int32.Parse(userInputArray[1]);

                        if ((xAxis >= mc.Plateau._x) || (yAxis >= mc.Plateau._y - 3))
                        {
                            Message = "These coordinates are outside the plateau";
                        }
                        else
                        {
                            for (int i = 0; i <= numb; i++)
                            {
                                Result.Add(new Rover((xAxis, yAxis + i), direction));
                            }
                            Success = true;
                        }

                    }
                    else
                    {
                        Message = "This was not in the correct format: x, y";
                    }
                } else
                {
                    Message = "Select a direction";
                }
            }
            else
            {
                Message = "Select a number";
            }
        }

    }
}
