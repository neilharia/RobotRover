using System;
using System.Linq;
using System.Text;
using RobotRover.Interfaces;

namespace RobotRover.Models
{
    public class Rover : IRover
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public string Direction { get; private set; }

        private readonly Plateau plateau;

        public Rover(Plateau plateau)
        {
            this.plateau = plateau;

            // Set default position of the rover at the origin, facing North
            //SetPosition(0, 0, "N");
        }

        public void SetPosition(int x, int y, string direction)
        {
            X = x;
            Y = y;

            if(string.IsNullOrEmpty(direction) || direction.Length > 1)
            {
                throw new Exception("Invalid direction string");
            }

            if(direction.All(c=> c == 'N' || c == 'E' || c=='S' || c == 'W'))
            {
                if (x >= Plateau.MinX && x <= plateau.MaxX && y >= Plateau.MinY && y <= plateau.MaxY)
                {
                    Direction = direction;
                }
                else
                {
                    throw new Exception("Cannot set initial position of rover, out of plateau");
                }
            }
            else
            {
                throw new Exception("Invalid characters in direction string");
            }
        }

        public string GetPosition()
        {
            return $"[{X}, {Y}, {Direction}]";
        }

        public void Move(string commands)
        {
            // Check that the command string is not null or empty
            if (string.IsNullOrEmpty(commands))
            {
                throw new Exception("Command is null or empty");
            }

            // Check that the command string only contains either digits or rotations (L/R)
            var isValidCommand = commands.All( c=> Char.IsDigit(c) || c == 'L' || c == 'R');

            // We assume that the first character of the string is either L or R
            if (isValidCommand && (commands[0] == 'L' || commands[0] == 'R'))
            {
                var unitsToMove = new StringBuilder();

                // Start parsing in the command string
                for (int i = 0; i < commands.Length; i++)
                {
                    char c = commands[i];

                    // L = 90deg rotation to the left, R = 90deg rotation to the right
                    if (c == 'L' || c == 'R')
                    {
                        // check to make sure the string does not contain consecutive L/R
                        if (i > 0)
                        {
                            if (commands[i - 1] == 'L' || commands[i - 1] == 'R')
                            {
                                throw new Exception("Invalid command string");
                            }
                        }

                        // for the first command in the string, unitsToMove should be empty
                        // however, upon finding the next L or R, unitsToMove will contain the amount to move for the previous command
                        if (unitsToMove.Length > 0)
                        {
                            // parse the amount to move by
                            int amount = int.Parse(unitsToMove.ToString());

                            // move this amount
                            if (!GetNewLocation(amount))
                            {
                                throw new Exception("Rover has fallen off the plateau!");
                            }

                            // clear the amount for the next command
                            unitsToMove.Clear();
                        }

                        // find the new direction to face
                        GetNewDirection(c);
                    }
                    else
                    {
                        // append the amount to move by here, as the amount to move could theoretically be any number of digits, not just single digits
                        unitsToMove.Append(c);
                    }
                }

                // move by the final amount parsed from the command string
                int finalAmount = int.Parse(unitsToMove.ToString());
                if (!GetNewLocation(finalAmount))
                {
                    throw new Exception("Rover has fallen off the plateau!");
                }
            }
            else
            {
                throw new Exception("Invalid command string");
            }
        }

        private void GetNewDirection(char rotation)
        {
            switch (Direction)
            {
                case "N":
                    if (rotation == 'L')
                    {
                        Direction = "W";
                    }
                    else if(rotation == 'R')
                    {
                        Direction = "E";
                    }
                    break;
                case "E":
                    if (rotation == 'L')
                    {
                        Direction = "N";
                    }
                    else if (rotation == 'R')
                    {
                        Direction = "S";
                    }
                    break;
                case "W":
                    if (rotation == 'L')
                    {
                        Direction = "S";
                    }
                    else if (rotation == 'R')
                    {
                        Direction = "N";
                    }
                    break;
                case "S":
                    if (rotation == 'L')
                    {
                        Direction = "E";
                    }
                    else if (rotation == 'R')
                    {
                        Direction = "W";
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool GetNewLocation(int amount)
        {
            string sign = null;
            string direction = null;
            bool moved = false;

            // get the sign of the amount the rover is meant to move
            switch (Direction)
            {
                case "N":
                case "E":
                    sign = "positive";
                    break;
                case "W":
                case "S":
                    sign = "negative";
                    break;
            }

            // which cartesian coordinate the rover should move in
            switch (Direction)
            {
                case "N":
                case "S":
                    direction = "y";
                    break;
                case "E":
                case "W":
                    direction = "x";
                    break;
            }

            // depending on cartesian axis, check if the rover would be in the bounds of the plateau after moving, only move if true
            if(direction == "y")
            {
                if (sign == "positive")
                {
                    if (Y + amount <= plateau.MaxY)
                    {
                        Y += amount;
                        moved = true;
                    }
                }
                else
                {
                    if (Y - amount >= Plateau.MinY)
                    {
                        Y -= amount;
                        moved = true;
                    }
                }       
            }
            else
            {
                if (sign == "positive")
                {
                    if (X + amount <= plateau.MaxX)
                    { 
                        X += amount;
                        moved = true;
                    }
                }
                else
                {
                    if (X - amount >= Plateau.MinX)
                    {
                        X -= amount;
                        moved = true;
                    }
                }
            }

            return moved;
        }
    }
}
