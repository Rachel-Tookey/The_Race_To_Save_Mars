using MarsRover.UILayerTG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG
{
    public class InstructionLevel : StyledWindow  
    {

        public GameApplication Application { get; set; }

        public InstructionLevel(GameApplication game) : base("Instructions")
        {
            Application = game;
            InitialiseLevel(); 
        }


        public void InitialiseLevel()
        {
            var instructionText = new StyledLabel(Utils.Text.GetLevelText("Instruction Level"))
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

            Add(instructionText,nextButton);

        }

    }
}


    

