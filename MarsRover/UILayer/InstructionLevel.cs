using MarsRover.UILayer.Superclasses;
using MarsRover.UILayer.Utils;
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

        public GameApplication App { get; set; }

        public InstructionLevel(GameApplication game) : base("Instructions")
        {
            App = game;

            var instructionText = new StyledLabel(LabelText.instructionLevel)
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
                App.SwitchToNextLevel(new TrainingLevel(App));
            };

            Add(instructionText,nextButton);

        }

    }
}


    

