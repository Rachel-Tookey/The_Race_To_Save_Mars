using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MarsRover.LogicLayer.Models;
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
            Rover roverToTest = new Rover((5 ,5), startDirection);

            roverToTest.RotateRover(Instructions.R);

            roverToTest.Direction.Should().Be(finishDirection);
        }

        [TestCase(Facing.NORTH, Facing.WEST)]
        [TestCase(Facing.WEST, Facing.SOUTH)]
        [TestCase(Facing.SOUTH, Facing.EAST)]
        [TestCase(Facing.EAST, Facing.NORTH)]
        public void RotateRover_Left(Facing startDirection, Facing finishDirection)
        {
            Rover roverToTest = new Rover((5, 5), startDirection);

            roverToTest.RotateRover(Instructions.L);

            roverToTest.Direction.Should().Be(finishDirection);
        }

        [Test]
        public void RotateRover_InvalidInput()
        {
            Rover roverToTest = new Rover((5, 5), Facing.NORTH);

            roverToTest.RotateRover(Instructions.M);

            roverToTest.Direction.Should().Be(Facing.NORTH);
        }

        [TestCase(Facing.NORTH, 5, 4)]
        [TestCase(Facing.WEST, 4, 5)]
        [TestCase(Facing.SOUTH, 5, 6)]
        [TestCase(Facing.EAST, 6, 5)]
        public void MoveRover(Facing direction, int endX, int endY)
        {
            Rover roverToTest = new Rover((5, 5), direction);


            roverToTest.MoveRover(); 

            roverToTest.Position.xAxis.Should().Be(endX);
            roverToTest.Position.yAxis.Should().Be(endY);

        }


    }
}
