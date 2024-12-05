using MarsRover.LogicLater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Enums; 

namespace MarsRover.Input.ParserModels
{
    public class InstructionParser
    {
        public bool Success { get; set; } = false;

        public List<Instructions> Result { get; set; } = new List<Instructions>();

        public string Message { get; set; } = "";


        public static Instructions? InstructionSwitcher(char c) => c switch
        {
            'L' => Instructions.L,
            'R' => Instructions.R,
            'M' => Instructions.M,
            _ => null, 
        };


    public InstructionParser(String userInput) { 
        
            char[] instructionChar = userInput.ToUpper().ToCharArray();
            foreach (char instruction in instructionChar)
            {
                Instructions? conversion = InstructionSwitcher(instruction);    
                if (conversion is Instructions conversionNotNull)
                {
                    Result.Add(conversionNotNull);
                }
            }
            if (Result.Count == 0)
            {
                Success = false;
                Message = "There was no recognisable instruction there. Try again";
            }
            else
            {
                Success = true;
            }
        }

    }
}
