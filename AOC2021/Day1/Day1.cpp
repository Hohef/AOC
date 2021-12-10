#include <cstdio>
#include <fstream>
#include <iostream>
#include <string>

#include "Day3.h"

int main()
{
    int day3_part1 = 0;
    int day3_part2 = 0;
    Day3::Part1_2(day3_part1, day3_part2);
    std::cout << "Day3 Part1: " << day3_part1 << std::endl;
    std::cout << "Day3 Part2: " << day3_part2 << std::endl << std::endl;


    std::ifstream in("..\\input\\Day1_Input.txt");
    std::string line;
    int count = 0;

    //Prime the pump
    getline(in, line);
    int iline1 = atoi(line.c_str());
    getline(in, line);
    int iline2 = atoi(line.c_str());
    getline(in, line);
    int iline3 = atoi(line.c_str());

    int setsum2 = iline2 + iline3;
    int setsum1 = setsum2 + iline1;

    while (getline(in, line))
    {   
        //Complete the second set
        int iline = atoi(line.c_str());
        setsum2 += iline;
        
        //Check the count
        if (setsum2 > setsum1) count++;

        //Prep for next one
        setsum1 = setsum2;
        setsum2 = iline3 + iline;
        iline3 = iline;
    }

    std::cout << "Day1 Part2: " << count << std::endl;

    return 0;
}