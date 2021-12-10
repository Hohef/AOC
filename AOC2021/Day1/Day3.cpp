#include "Day3.h"

#include <fstream>
#include <string>

using namespace std;

class Bucket
{
	public:
		~Bucket() 
		{
			if (pOnes != nullptr) delete pOnes;
			if (pZero != nullptr) delete pZero;
		}
		int ones = 0;
		int zero = 0;
		Bucket* pOnes;
		Bucket* pZero;
};


void Day3::Part1_2(int &part1, int &part2)
{
	Bucket* bucket = new Bucket();
	int readingOnes[12];
	int readingzeros[12];
	int numReadings = 0;
	memset(readingOnes, 0, sizeof(readingOnes));
	memset(readingzeros, 0, sizeof(readingzeros));

	std::ifstream in("..\\input\\Day3_Input.txt");
	std::string line;

	while (getline(in, line))
	{
		Bucket* pBucket = bucket;

		for (int i = 0; i < 12; ++i)
		{
			if (line[i] == '1')
			{
				readingOnes[i]++;
				pBucket->ones++;
				if (pBucket->pOnes == nullptr)
					pBucket->pOnes = new Bucket();
				pBucket = pBucket->pOnes;
			}
			else
			{
				readingzeros[i]++;
				pBucket->zero++;
				if (pBucket->pZero == nullptr)
					pBucket->pZero = new Bucket();
				pBucket = pBucket->pZero;
			}
		}
	}

	int gamma = 0; int epsilon = 0;
	for (int i = 0; i < 12; i++)
	{
		int shift = 1 << (11 - i);
		if (readingOnes[i] >= readingzeros[i])
			gamma += shift;
		else
			epsilon += shift;
	}

	part1 = gamma * epsilon;

	//*************************************************
	//Calculate Oxygen Generator and CO2 Srubber ratings
	//*************************************************
	Bucket *pOxygen = bucket;
	Bucket* pScrub = bucket;

	int oxygen = 0;
	int scrubber = 0;
	for (int i = 0; i < 12; ++i)
	{
		int shift = 1 << (11 - i);
		//Oxygen Generator
		if (pOxygen->ones >= pOxygen->zero && pOxygen->ones != 0)
		{
			oxygen += shift;
			pOxygen = pOxygen->pOnes;
		}
		else
		{
			pOxygen = pOxygen->pZero;
		}

		//CO2 Scrubber
		if (((pScrub->zero <= pScrub->ones) && pScrub->zero != 0) || pScrub->ones == 0)
		{
			pScrub = pScrub->pZero;
		}
		else
		{
			scrubber += shift;
			pScrub = pScrub->pOnes;
		}
	}

	part2 = oxygen * scrubber;
}