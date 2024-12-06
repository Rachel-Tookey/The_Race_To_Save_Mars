using MarsRover;
using MarsRover.States;
using MarsRover.Input.ParserModels;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using MarsRover.Enums;
using MarsRover.LogicLater.Models;

namespace MarsRoverTests
{
    public class MissionControlTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Description("Add Rover")]
        public void AddRover()
        {
            Position newPos = new Position(5, 5, Facing.NORTH); 
            Rover newRover = new Rover(newPos);
            Plateau newPlateau = new Plateau(10, 10);
            MissionControl testMissionControl = new MissionControl(newPlateau);

            List<Rover> rovers = new List<Rover>() {newRover };

            testMissionControl.AddRover(newRover);

            testMissionControl.Rovers.Should().HaveCount(1);
            testMissionControl.Rovers.Should().BeEquivalentTo(rovers);

        }

        [Test, Description("Check if position is empty, true")]
        public void IsPositionEmpty_True()
        {
            Position newPos = new Position(5, 5, Facing.NORTH);
            Rover newRover = new Rover(newPos);
            Plateau newPlateau = new Plateau(10, 10);
            MissionControl testMissionControl = new MissionControl(newPlateau);
            testMissionControl.AddRover(newRover);

            Boolean result = testMissionControl.IsPositionEmpty(5, 6);

            result.Should().BeTrue();

        }

        [Test, Description("Check if a position is empty - false")]
        public void IsPositionEmpty_False()
        {
            Position newPos = new Position(5, 5, Facing.NORTH);
            Rover newRover = new Rover(newPos);
            Plateau newPlateau = new Plateau(10, 10);
            MissionControl testMissionControl = new MissionControl(newPlateau);
            testMissionControl.AddRover(newRover);

            Boolean result = testMissionControl.IsPositionEmpty(5, 5);

            result.Should().BeFalse();
        }

        [Test, Description("Check if a position is in bounds - true")]
        public void IsPositionIsInBounds_True()
        {
            Plateau newPlateau = new Plateau(10, 10);
            MissionControl testMissionControl = new MissionControl(newPlateau);
            Boolean result = testMissionControl.IsPositionInRange(10, 10); 
            result.Should().BeTrue(); 

        }


        [Test, Description("Check if a position is in bounds - false")]
        public void IsPositionIsInBounds_False()
        {
            Plateau newPlateau = new Plateau(10, 10);
            MissionControl testMissionControl = new MissionControl(newPlateau);
            Boolean result = testMissionControl.IsPositionInRange(11, 11);
            result.Should().BeFalse(); 

        }

    }
}