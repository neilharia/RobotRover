using System;
using NUnit.Framework;

namespace RobotRover.Tests
{
    [TestFixture]
    public class MoveRoverUnitTests : RoverTestsBase
    {
        [TestCase("R1R3L2L1", "[13, 8, N]", true)]
        [TestCase("R1R1L1L1", "[12, 10, N]", true)]
        [TestCase("L20R11R33L45", "Rover has fallen off the plateau!", false)]
        [TestCase("R1RL2L1", "Invalid command string", false)]
        [TestCase("", "Command is null or empty", false)]
        [TestCase(null, "Command is null or empty", false)]
        [TestCase("a", "Invalid command string", false)]
        public void MoveRoverPosition(string commands, string result, bool shouldPass)
        {
            // Arrange
            rover.SetPosition(10, 10, "N");

            // Act/Assert
            if (shouldPass)
            {
                rover.Move(commands);

                var position = rover.GetPosition();

                Assert.AreEqual(result, position);
            }
            else
            {
                var ex = Assert.Throws<Exception>(() => rover.Move(commands));

                Assert.AreEqual(result, ex.Message);
            }
        }
    }
}