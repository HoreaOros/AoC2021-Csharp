using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.IO.Ports;

namespace AoC2021_P03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string line;
            List<string> nums = new List<string>();
            while( (line = sr.ReadLine()) != null)
                nums.Add(line);
            sr.Close();


            Console.WriteLine($"Part1: {part1(nums)}");
            Console.WriteLine($"Part2: {part2(nums)}");
        }

        private static int part2(List<string> nums)
        {
            int lifeSupRate = 0;
            int OGenRate = 0, CO2ScrubRate = 0;

            List<string> oNums = new List<string>();
            List<string> co2Nums = new List<string>();
            foreach (var item in nums)
            {
                oNums.Add(item);
                co2Nums.Add(item);
            }
            

            int pos = 0;
            while (oNums.Count > 1)
            {
                (int zeroes, int ones) = Bits(pos, oNums);
                char mostCommon;
                if (zeroes > ones)
                    mostCommon = '0';
                else mostCommon = '1';
                //var filteredList = oNums.Where(v => v[pos] == mostCommon);

                List<string> filteredList = new List<string>();
                foreach (var item in oNums)
                {
                    if (item[pos] == mostCommon)
                        filteredList.Add(item);
                }
                oNums = filteredList;
                pos++;
            }


            pos = 0;
            while (co2Nums.Count > 1)
            {
                (int zeroes, int ones) = Bits(pos, co2Nums);
                char leastCommon;
                if (zeroes <= ones)
                    leastCommon = '0';
                else leastCommon = '1';
                List<string> filteredList = new List<string>();
                foreach (var item in co2Nums)
                {
                    if (item[pos] == leastCommon)
                        filteredList.Add(item);
                }
                co2Nums = filteredList;
                pos++;
            }

            for(int i = 0; i < oNums[0].Length; i++)
                OGenRate = OGenRate * 2 + (oNums[0][i] - '0');

            for (int i = 0; i < co2Nums[0].Length; i++)
                CO2ScrubRate = CO2ScrubRate * 2 + (co2Nums[0][i] - '0');

            lifeSupRate = OGenRate * CO2ScrubRate;
            return lifeSupRate;

        }

        private static (int zeroes, int ones) Bits(int pos, List<string> nums)
        {
            int n0 = 0, n1 = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i][pos] == '0')
                    n0++;
                else
                    n1++;
            }
            return (zeroes: n0, ones: n1);
        }

        private static int part1(List<string> nums)
        {
            int powerConsumption = 0;
            int gammaRate = 0, epsilonRate = 0;

            int cols = nums[0].Length;

            for (int i = 0; i < cols; i++)
            {
                int mostFrequentBit = MostFrequentBit(i, nums);
                int leastFrequntBit = 1 - mostFrequentBit;
                
                gammaRate = gammaRate * 2 + mostFrequentBit;
                epsilonRate = epsilonRate * 2 + leastFrequntBit;
            }


            powerConsumption = gammaRate * epsilonRate;
            return powerConsumption;
        }

        private static int MostFrequentBit(int pos, List<string> nums)
        {
            int zeroes = 0, ones = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i][pos] == '0')
                    zeroes++;
                else
                    ones++;
            }
            if (ones >= zeroes)
                return 1;
            else return 0;
        }
    }
}
