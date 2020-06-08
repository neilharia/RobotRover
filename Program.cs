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
            Rover rover1 = new Rover(plateau);
            // Requirement:
            rover1.SetPosition(10, 10, "N");

            // Ability to implement additional Rovers:
            Rover rover2 = new Rover(plateau);
            rover2.SetPosition(8, 8, "S");
                        
            // Output current rover position and direction to console
            Console.WriteLine("Rover 1 - starting position:");
            Console.WriteLine(rover1.GetPosition());

            Console.WriteLine("Rover 2 - starting position:");
            Console.WriteLine(rover2.GetPosition());

            // Requirement:
            Console.WriteLine();
            Console.WriteLine("Command string: {0}", commands);
            rover1.Move(commands);
            rover2.Move(commands);

            Console.WriteLine();
            Console.WriteLine("Rover 1 - final position:");
            Console.WriteLine(rover1.GetPosition());

            Console.WriteLine("Rover 2 - final position:");
            Console.WriteLine(rover2.GetPosition());
        }
    }
}
