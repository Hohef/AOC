using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC21CSharp
{
    class Day2
    {
        public static int Day2_Part1()
        {
            System.IO.StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day2_Input.txt");

            int depth = 0; int distance = 0;
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] movement = line.Split(' ');

                int val = Int32.Parse(movement[1]);

                if (movement[0] == "forward") distance += val;
                else if (movement[0] == "down") depth += val;
                else if (movement[0] == "up") depth -= val;
            }

            return distance * depth;
        }

        public static int Day2_Part2()
        {
            System.IO.StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day2_Input.txt");

            int depth = 0; int distance = 0; int aim = 0;
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] movement = line.Split(' ');

                int val = Int32.Parse(movement[1]);

                if (movement[0] == "forward")
                {
                    distance += val;
                    depth += (val * aim);
                }
                else if (movement[0] == "down")
                {
                    //depth += val;
                    aim += val;
                }
                else if (movement[0] == "up")
                {
                    //depth -= val;
                    aim -= val;
                }
            }

            return distance * depth;
        }
    }
}
