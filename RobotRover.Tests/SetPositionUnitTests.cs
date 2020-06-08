using System;
using NUnit.Framework;

namespace RobotRover.Tests
{
    [TestFixture]
    public class SetPositionUnitTests : RoverTestsBase
    {
        [TestCase(8, 15, "N", true)]
        [TestCase(3, 7, "E", true)]
        [TestCase(16, 19, "S", true)]
        [TestCase(12, 10, "W", true)]
        [TestCase(-120, -100, "W", false, "Cannot set initial position of rover, out of plateau")]
        [TestCase(120, 100, "W", false, "Cannot set initial position of rover, out of plateau")]
        [TestCase(3, 7, null, false, "Invalid direction string")]
        [TestCase(3, 7, "", false, "Invalid direction string")]
        [TestCase(3, 7, "asdfasdf", false, "Invalid direction string")]
        [TestCase(3, 7, "a", false, "Invalid characters in direction string")]
        [TestCase(3, 7, "9", false, "Invalid characters in direction string")]
        public void SetRoverPosition(int x, int y, string direction, bool shouldPass, string exceptionMessage = null)
        {
            if (shouldPass)
            {
                rover.SetPosition(x, y, direction);

                var position = rover.GetPosition();

                Assert.AreEqual(position, $"[{x}, {y}, {direction}]");
                Assert.AreEqual(x, rover.X);
                Assert.AreEqual(y, rover.Y);
                Assert.AreEqual(direction, rover.Direction);
            }
            else
            {
                var ex = Assert.Throws<Exception>(() => rover.SetPosition(x, y, direction));

                Assert.AreEqual(exceptionMessage, ex.Message);
            }
        }
    }
}