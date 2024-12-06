using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MarsRover.LogicLater.Models;
using MarsRover.Enums; 

namespace MarsRoverTests
{
    public class RoverTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(Facing.NORTH, Facing.EAST)]
        [TestCase(Facing.EAST, Facing.SOUTH)]
        [TestCase(Facing.SOUTH, Facing.WEST)]
        [TestCase(Facing.WEST, Facing.NORTH)]
        public void RotateRover_Right(Facing startDirection, Facing finishDirection)
        {
            Position roverPosition = new Position(5, 5, startDirection);
            Rover roverToTest = new Rover(roverPosition);

            roverToTest.RotateRover(Instructions.R);

            roverToTest.Position.Direction.Should().Be(finishDirection);
        }

        [TestCase(Facing.NORTH, Facing.WEST)]
        [TestCase(Facing.WEST, Facing.SOUTH)]
        [TestCase(Facing.SOUTH, Facing.EAST)]
        [TestCase(Facing.EAST, Facing.NORTH)]
        public void RotateRover_Left(Facing startDirection, Facing finishDirection)
        {
            Position roverPosition = new Position(5, 5, startDirection);
            Rover roverToTest = new Rover(roverPosition);

            roverToTest.RotateRover(Instructions.L);

            roverToTest.Position.Direction.Should().Be(finishDirection);
        }

        [Test]
        public void RotateRover_InvalidInput()
        {
            Position roverPosition = new Position(5, 5, Facing.NORTH);
            Rover roverToTest = new Rover(roverPosition);

            roverToTest.RotateRover(Instructions.M);

            roverToTest.Position.Direction.Should().Be(Facing.NORTH);
        }

        [TestCase(Facing.NORTH, 5, 6)]
        [TestCase(Facing.WEST, 4, 5)]
        [TestCase(Facing.SOUTH, 5, 4)]
        [TestCase(Facing.EAST, 6, 5)]
        public void MoveRover(Facing direction, int endX, int endY)
        {
            Position roverPosition = new Position(5, 5, direction);
            Rover roverToTest = new Rover(roverPosition);

            roverToTest.MoveRover(); 

            roverToTest.Position.x.Should().Be(endX);
            roverToTest.Position.y.Should().Be(endY);

        }


    }
}
