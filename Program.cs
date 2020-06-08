using System;
using RobotRover.Models;

namespace RobotRover
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hard coded here for the time being, but could be parsed in from file/console etc...
            string commands = "R1R3L2L1";
            //commands = Console.ReadLine();

            // NOTE: Multiple plateaus can be defined for individual rovers
            // Additional requirement
            Plateau plateau = new Plateau(40, 30);

            // Creating a new instance of the rover will set to default position of [0, 0, N]
            Rover rover = new Rover(plateau);
            // Requirement:
            rover.SetPosition(10, 10, "N");

            // Ability to implement additional Rovers:
            Rover rover2 = new Rover(plateau);
            rover2.SetPosition(8, 8, "S");
                        
            // Output current rover position and direction to console
            Console.WriteLine("Starting position:");
            Console.WriteLine(rover.GetPosition());

            // Requirement:
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Command string: {0}", commands);
            rover.Move(commands);

            Console.WriteLine();
            Console.WriteLine("Final position:");
            Console.WriteLine(rover.GetPosition());
        }
    }
}
