using MarsRover;
using MarsRover.Input.ParserModels;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using MarsRover.Enums;
using MarsRover.LogicLayer.Models;

namespace MarsRoverTests
{
    public class ParserModelTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test, Description("RoverParser - correct input")]
        public void RoverParser_ValidInput()
        {
            String mockUserString = "5 7";

            Plateau testPlateau = new Plateau(10, 10);

            RoverParser testRP = new RoverParser(mockUserString, Facing.NORTH, testPlateau);

            testRP.Success.Should().BeTrue();
            testRP.Message.Should().Be("");
            testRP.Result.Should().NotBeNull();
            testRP.Result.Position.xAxis.Should().Be(5);
            testRP.Result.Position.yAxis.Should().Be(7);
            testRP.Result.Direction.Should().Be(MarsRover.Enums.Facing.NORTH);

        }


        [Test, Description("RoverParser - invalid input")]
        public void RoverParser_InvalidInput()
        {
            String mockUserString = "57 N";

            Plateau testPlateau = new Plateau(10, 10);

            RoverParser testRP = new RoverParser(mockUserString, Facing.SOUTH, testPlateau);

            testRP.Success.Should().BeFalse();
            testRP.Message.Should().Be("This was not in the correct format: x y");
            testRP.Result.Should().BeNull();
   
        }


        [Test, Description("RoverParser - valid input, outside plateau")]
        public void RoverParser_ValidInput_OutsidePlateau()
        {
            String mockUserString = "55 77";

            Plateau testPlateau = new Plateau(10, 10);

            RoverParser testRP = new RoverParser(mockUserString, Facing.NORTH, testPlateau);

            testRP.Success.Should().BeFalse();
            testRP.Message.Should().Be("These coordinates are outside the plateau");
            testRP.Result.Should().BeNull();

        }


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