using MarsRover;
using MarsRover.States;
using MarsRover.Models;
using MarsRover.Input.ParserModels;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using MarsRover.Enums;

namespace MarsRoverTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // PlateauSizeParser

        [Test, Description("PlateauSizeParser - correct input")]
        public void PlateauSizeParser_ValidInput()
        {
            String mockUserString = "10 20"; 

            PlateauSizeParser testPSP = new PlateauSizeParser(mockUserString);

            testPSP.Success.Should().BeTrue();
            testPSP.Message.Should().Be("");
            testPSP.Result._x.Should().Be(10);
            testPSP.Result._y.Should().Be(20);
         
        }

        [Test, Description("PlateauSizeParser - invalid input, no space")]
        public void PlateauSizeParser_InvalidInput_NoSpace()
        {
            String mockUserString = "1020";

            PlateauSizeParser testPSP = new PlateauSizeParser(mockUserString);

            testPSP.Success.Should().BeFalse();
            testPSP.Message.Should().Be("Incorrect format provided. It must be 'x y'");
            testPSP.Result.Should().BeNull();
        }


        [Test, Description("PlateauSizeParser - invalid input, negative numbers")]
        public void PlateauSizeParser_InvalidInput_NegNumbers()
        {
            String mockUserString = "10 -20";

            PlateauSizeParser testPSP = new PlateauSizeParser(mockUserString);

            testPSP.Success.Should().BeFalse();
            testPSP.Message.Should().Be("Incorrect format provided. It must be 'x y'");
            testPSP.Result.Should().BeNull();
        }

        [Test, Description("PlateauSizeParser - invalid input, too many numbers")]
        public void PlateauSizeParser_InvalidInput_TooManyNumbers()
        {
            String mockUserString = "10 20 30";

            PlateauSizeParser testPSP = new PlateauSizeParser(mockUserString);

            testPSP.Success.Should().BeFalse();
            testPSP.Message.Should().Be("Incorrect format provided. It must be 'x y'");
            testPSP.Result.Should().BeNull();
        }

        // Rover Parser 

        [Test, Description("RoverParser - correct input")]
        public void RoverParser_ValidInput()
        {
            String mockUserString = "5 7 N";

            Plateau testPlateau = new Plateau(10, 10);

            RoverParser testRP = new RoverParser(mockUserString, testPlateau);

            testRP.Success.Should().BeTrue();
            testRP.Message.Should().Be("");
            testRP.Result.Should().NotBeNull();
            testRP.Result.Position.x.Should().Be(5);
            testRP.Result.Position.y.Should().Be(7);
            testRP.Result.Position.Direction.Should().Be(MarsRover.Enums.Facing.NORTH);

        }


        [Test, Description("RoverParser - invalid input")]
        public void RoverParser_InvalidInput()
        {
            String mockUserString = "57 N";

            Plateau testPlateau = new Plateau(10, 10);

            RoverParser testRP = new RoverParser(mockUserString, testPlateau);

            testRP.Success.Should().BeFalse();
            testRP.Message.Should().Be("This was not in the correct format: x y d");
            testRP.Result.Should().BeNull();
   
        }


        [Test, Description("RoverParser - valid input, outside plateau")]
        public void RoverParser_ValidInput_OutsidePlateau()
        {
            String mockUserString = "55 77 N";

            Plateau testPlateau = new Plateau(10, 10);

            RoverParser testRP = new RoverParser(mockUserString, testPlateau);

            testRP.Success.Should().BeFalse();
            testRP.Message.Should().Be("These coordinates are outside the plateau");
            testRP.Result.Should().BeNull();

        }


        // Instruction Parser 

        [Test, Description("Instruction Parser - valid input")]
        public void InstructionParser_ValidInput()
        {
            String mockUserString = "LRLRM";

            List<Instructions> expectedInstructions = new List<Instructions>() {
            Instructions.L,
            Instructions.R,
            Instructions.L,
            Instructions.R,
            Instructions.M
            };

            InstructionParser testIP = new InstructionParser(mockUserString);

            testIP.Success.Should().BeTrue();
            testIP.Message.Should().Be("");
            testIP.Result.Count().Should().Be(5);
            testIP.Result.First().Should().Be(Instructions.L);
            testIP.Result.Should().BeEquivalentTo(expectedInstructions);

 
        }

        [Test, Description("Instruction Parser - invalid input")]
        public void InstructionParser_InvalidInput()
        {
            String mockUserString = "TEXAS";

            List<Instructions> expectedInstructions = new List<Instructions>();

            InstructionParser testIP = new InstructionParser(mockUserString);

            testIP.Success.Should().BeFalse();
            testIP.Message.Should().Be("There was no recognisable instruction there. Try again");
            testIP.Result.Count().Should().Be(0);
            testIP.Result.Should().BeEquivalentTo(expectedInstructions);

        }

        [Test, Description("Instruction Parser - mix of valid and invalid input")]
        public void InstructionParser_MixedInput()
        {
            String mockUserString = "TELMXASR";

            List<Instructions> expectedInstructions = new List<Instructions>() {
            Instructions.L,
            Instructions.M,
            Instructions.R
            };

            InstructionParser testIP = new InstructionParser(mockUserString);

            testIP.Success.Should().BeTrue();
            testIP.Message.Should().Be("");
            testIP.Result.Count().Should().Be(3);
            testIP.Result.Should().BeEquivalentTo(expectedInstructions);

        }

    }
}