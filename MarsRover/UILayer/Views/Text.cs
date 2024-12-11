using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UILayerTG.Utils
{
    public class Text
    {
        public static string GetLevelText(string LevelName)
        {

            string openingLevelText = """
                An alien race has landed on Mars intending to take its resources for themselves...
                The group of cyberpunks and Martians form a rebel alliance to fight back.
                They put a call-out for an aspiring software developer to pilot their rovers.
                You stop your job hunting on LinkedIn and think 'Hey, I could do that!' 
                """;

            string trainingLevel = """
                Your mission: 
                Get to the entrance (⊕) of the Martian Kingdom.
                Do not crash into the rocks (⡺).
                """;

            string instructionLevel = """
                You arrive at the cyberpunk's headquarters and ask, "So, we didn't discuss rates on the phone-"

                But the cyberpunks cut you off: 

                "There's no time! The rovers have already landed. You must get them to the entrance of the underground kingdom.

                        Use the Mouse to select the Rover to control from the dropdown. 
                        Up arrow to move the selected Rover forward.
                        Down arrow to reverse.
                        Left arrow to turn 90 degrees left.
                        Right arrow to turn 90 degrees left.
                
                Good luck! Oh, and breaks are not part of your billable hours"
                """;

            string addRoverLevel = """
                You can add up to 3 rovers for the duration of the game.
                But due to a quirk in the time-space continuum:
                The more rovers you have, the less time you get for each level 
                Take your pick...
                """;

            Dictionary<string, string> LevelText = new Dictionary<string, string>();
            LevelText.Add("Opening Level", openingLevelText);
            LevelText.Add("Instruction Level", instructionLevel);
            LevelText.Add("Training Level", trainingLevel);
            LevelText.Add("Add Rovers Level", addRoverLevel);

            return LevelText.ContainsKey(LevelName) ? LevelText[LevelName] : "";




        }
    }
}
