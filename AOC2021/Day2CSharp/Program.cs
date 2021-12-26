using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC21CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puzzleday = 4;
            int result_part1 = 0;
            int result_part2 = 0;
            switch (puzzleday)
            {
                case 2:
                    result_part1 = Day2.Day2_Part1();
                    result_part2 = Day2.Day2_Part2();
                    break;
                case 4:
                    result_part1 = Day4.Part1();  //Answer: 28082
                    result_part2 = Day4.Part2();  //Answer: 8224
                    break;
                default:
                    break;
            }

           
            System.Console.WriteLine(string.Format("Day{0} Part1: {1}", puzzleday, result_part1));
            System.Console.WriteLine(string.Format("Day{0} Part2: {1}", puzzleday, result_part2));
        }
    }
}
