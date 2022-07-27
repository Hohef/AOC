#include "Day9.h"
#include <fstream>
#include <string>
#include <vector>

using namespace std;
void AdjustLeaderBoard(int& a, int& b, int& c, int newval);

void Day9::Part1_2(int &part1, int &part2)
{
	std::ifstream in("..\\input\\Day9_Input.txt");
	std::string line;

    std::vector<std::vector<char>> terrain;

    int y = 0;
	while (getline(in, line))
    {
        vector<char> newline;
        
        for( char c :line)
        {
            newline.push_back(c);
        }
        terrain.push_back(newline);
    }

    int top1 = 0;
    int top2 = 0;
    int top3 = 0;
    int numbasin;
    for (int y = 0; y < terrain.size();y++)
        for (int x = 0; x < terrain[y].size(); x++)
        {
            numbasin = CountBasin(x, y, 0, terrain);
            //Keep top 3 basins
            AdjustLeaderBoard(top1, top2, top3, numbasin);
        }

    //Result is top three basins
    part2 = top1 * top2 * top3;
}

/// <summary>
/// Quick and Dirty top three check.  
/// </summary>
/// <param name="a"></param>
/// <param name="b"></param>
/// <param name="c"></param>
/// <param name="newval"></param>
void AdjustLeaderBoard(int& a, int& b, int& c, int newval)
{
    if (newval < c) return;
    if (newval > a)
    {
        c = b;
        b = a;
        a = newval;
        return;
    }
    if (newval > b)
    {
        c = b;
        b = newval;
        return;
    }
    c = newval;
}

/// <summary>
/// Count all the consecutive spaces surrounded by 9s (exclusive of the 9s).
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <param name="count"></param>
/// <param name="terrain"></param>
/// <returns></returns>
int Day9::CountBasin(int x, int y, int count, std::vector<std::vector<char>> &terrain)
{
    //Check if slot to be counted;
    if (terrain[y][x] == '-') return count;
    if (terrain[y][x] == '9') return count;

    //Set as already counted;
    terrain[y][x] = '-';
    //Can we go left or right?
    if (x > 0)
        count = CountBasin(x-1, y, count, terrain);
    if (x < terrain[y].size()-1)
        count = CountBasin(x+1, y, count, terrain);
    //Can we go up or down
    if (y > 0)
        count = CountBasin(x, y - 1, count, terrain);
    if (y < terrain.size()-1)
        count = CountBasin(x, y + 1, count, terrain);

    //Include this space in current basin
    return count + 1;
}