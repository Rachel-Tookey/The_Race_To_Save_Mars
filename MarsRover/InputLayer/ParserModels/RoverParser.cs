using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Rover? Result { get; set; } = null; 

        public string Message { get; set; } = "";

        public RoverParser(String userInput, Facing direction, MissionControl mc) {

            Regex userPattern = new Regex("^[0-9]+,\\s[0-9]+$");
            
            if (userPattern.IsMatch(userInput)) {

                string[] userInputArray = userInput.Split(", ");
                int xAxis = Int32.Parse(userInputArray[0]);
                int yAxis = Int32.Parse(userInputArray[1]);

                if ((xAxis >= mc.Plateau._x) || (yAxis >= mc.Plateau._y))
                {
                    Success = false;
                    Message = "These coordinates are outside the plateau"; 
                } else if (!mc.IsPositionEmptyRovers((xAxis, yAxis)))
                {
                    Success = false;
                    Message = "There is already a rover at that position";
                }
                else
                {
                    Result = new Rover((xAxis, yAxis), direction);
                    Success = true; 
                }
            
            }
            else {
                Success = false;
                Message = "This was not in the correct format: x, y";
            }

        }

    }
}
