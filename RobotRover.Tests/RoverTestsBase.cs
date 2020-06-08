using System;
using NUnit.Framework;
using RobotRover.Models;

namespace RobotRover.Tests
{
    [TestFixture]
    public abstract class RoverTestsBase
    {
        // can add other properties common to unit tests here

        internal Rover rover;
        internal Plateau plateau;

        [SetUp]
        public void Setup()
        {
            plateau = new Plateau(40, 30);
            rover = new Rover(plateau);
        }
    }
}
