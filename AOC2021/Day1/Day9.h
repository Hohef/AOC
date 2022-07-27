#pragma once
#include <vector>

class Day9
{
	public:
		static void Part1_2(int& part1, int& part2);
	private:
		static int CountBasin(int x, int y, int count, std::vector<std::vector<char>>& terrain);
};