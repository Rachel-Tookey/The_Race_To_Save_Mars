using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class InstructionLevel : ILevel 
    {

        public GameApplication Application { get; set; }

        public InstructionLevel(GameApplication game)
        {

            Application = game;
        }


        public Window WindowRun()
        {

            var openingWindow = new Terminal.Gui.Window("Instructions")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            string introText = """
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


            var label = new Terminal.Gui.Label(introText)
            {
                X = Pos.Center(),
                Y = 2,
                ColorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
                },
                Border = new Terminal.Gui.Border()
                {
                    BorderStyle = BorderStyle.Single,
                    Padding = new Thickness(0),
                    BorderBrush = Color.BrightMagenta,
                    Background = Color.Black,
                }

            };


            var nextButton = new Terminal.Gui.Button("Enter the training level")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(label) + 4,
            };



            nextButton.Clicked += () =>
            {
                Application.SwitchToNextLevel(new TrainingLevel(Application));
            };

            openingWindow.Add(label, nextButton);

            return openingWindow;
        }

    }
}


    

