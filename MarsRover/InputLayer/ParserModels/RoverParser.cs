using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarsRover.Models;
using MarsRover.Enums;

namespace MarsRover.Input.ParserModels
{
    public class RoverParser
    {
        public bool Success { get; set; } = false;

        public Rover? Result { get; set; } = null; 

        public string Message { get; set; } = "";

        public Facing FacingConverter(String userInput) => userInput switch
        {
            "N" => Facing.NORTH,
            "S" => Facing.SOUTH,
            "W" => Facing.WEST,
            "E" => Facing.EAST
        };


        public RoverParser(String userInput, Plateau plateau) {
            Regex userPattern = new Regex("^[0-9]+\\s[0-9]+\\s[NnSsEeWw]+$");
            if (userPattern.IsMatch(userInput)) {
                string[] userInputArray = userInput.Split(" ");
                int xAxis = Int32.Parse(userInputArray[0]);
                int yAxis = Int32.Parse(userInputArray[1]);
                Facing roverIsFacing = FacingConverter(userInputArray[2].ToUpper());
                if ((xAxis > plateau._x) || (yAxis > plateau._y))
                {
                    Success = false;
                    Message = "These coordinates are outside the plateau"; 
                } else
                {
                    Position startingPosition = new Position(xAxis, yAxis, roverIsFacing);
                    Result = new Rover(startingPosition);
                    Success = true; 
                }
            
            }
            else {
                Success = false;
                Message = "This was not in the correct format: x y d";
            }

        }

    }
}
