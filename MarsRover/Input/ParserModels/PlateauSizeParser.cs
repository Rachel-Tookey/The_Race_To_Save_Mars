using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Input.ParserModels
{
    internal class PlateauSizeParser
    {
        public bool Success { get; set; } = false;

        public Plateau? Result { get; set; } = null;

        public string Message { get; set; } = "";

        public PlateauSizeParser(String UserInput) {
            Regex userPattern = new Regex("[0-9]+\\s[0-9]+"); 
            if (userPattern.IsMatch(UserInput))
            {
                string[] splitUI = UserInput.Split(' ');
                if ((int.TryParse(splitUI[0], out int XAxis)) &&  (int.TryParse(splitUI[1], out int YAxis))) {
                    if (XAxis <= 0 || YAxis <= 0)
                    {
                        Message = "X and Y must be greater than 0";
                    } else
                    {
                        Result = new Plateau(XAxis, YAxis); 
                        Success = true;
                    }
                } else
                {
                    Message = "X and Y must be numerical values";
                }

            } else
            {
                Message = "Incorrect format provided. It must be 'x y'"; 
            }        
        }

    }
}
