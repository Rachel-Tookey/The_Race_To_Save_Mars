using MarsRover.LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Input.ParserModels
{
    public class PlateauSizeParser
    {
        public bool Success { get; set; } = false;

        public Plateau? Result { get; set; } = null;

        public string Message { get; set; } = "";

        public PlateauSizeParser(String UserInput) {
            Regex userPattern = new Regex("^[0-9]+\\s[0-9]+$"); 
            if (userPattern.IsMatch(UserInput))
            {
                string[] splitUI = UserInput.Split(' ');
                if ((int.TryParse(splitUI[0], out int YAxis)) &&  (int.TryParse(splitUI[1], out int XAxis))) {

                    if ((XAxis < 20) || (YAxis < 20)) {
                        Message = "Plateau is too small. It must be a minimum of 20 20";
                        Success = false; 
                    } else
                    {
                        Result = new Plateau(XAxis, YAxis);
                        Success = true;
                    }
                }
            } else
            {
                Message = "Incorrect format provided. It must be 'x y'"; 
                Success = false;
            }        
        }

    }
}
