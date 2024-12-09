using MarsRover.UILayerTG.Utils;
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


        public Window GetWindow()
        {

            var openingWindow = new StyledWindow("Instructions");


            var instructionText = new StyledLabel(Text.GetLevelText("Instruction Level"))
            {
                X = Pos.Center(),
                Y = 2,
            };


            var nextButton = new Terminal.Gui.Button("Enter the training level")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(instructionText) + 4,
            };


            nextButton.Clicked += () =>
            {
                Application.SwitchToNextLevel(new TrainingLevel(Application));
            };

            openingWindow.Add(instructionText, nextButton);

            return openingWindow;
        }

    }
}


    

