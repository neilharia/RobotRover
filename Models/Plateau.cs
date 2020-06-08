using System;
namespace RobotRover.Models
{
    public class Plateau
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public const int MinX = 0;
        public const int MinY = 0;

        public Plateau(int x, int y)
        {
            MaxX = x;
            MaxY = y;
        }
    }
}
