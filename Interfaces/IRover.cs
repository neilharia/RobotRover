using System;

namespace RobotRover.Interfaces
{
    public interface IRover
    {
        public void SetPosition(int x, int y, string direction);

        public void Move(string commands);
    }
}
