using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC21CSharp
{
    class Day3
    {
        public static int Part1()
        {
            int gamma = 0;
            int epsilon = 0;

            System.IO.StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day3_Input.txt");

            string line;

            int[] array = new int[12];
            int readings = 0;
            while ((line = input.ReadLine()) != null)
            {
                readings++;
                for (int i = 0; i < 12; ++i)
                {
                    if (line[i] == '1')
                        array[i]++;
                }            
            }

            input.Close();

            readings /= 2;


            for (int i = 0; i < 12; ++i)
            {
                int shift = 1 << (11 - i);
                if (array[i] > readings)
                    gamma += shift;
                else
                    epsilon += shift;
            }

            return gamma * epsilon;
        }

        public static int Part2()
        {
  

            return 0;
        }
    }

    class Bucket
    {
        public List<Bucket> ones = new List<Bucket>();
        public List<Bucket> zero = new List<Bucket>();
    }
}
