using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string[] tokens = sr.ReadLine().Split(new char[] { ',', });
            List<int> nums = new List<int>();
            foreach (var item in tokens)
                nums.Add(int.Parse(item));
            nums.Sort();
            Console.WriteLine($"Part 1: {part1(nums)}");
            Console.WriteLine($"Part 2: {part2(nums)}");




        }


        // Brute force
        private static object part2(List<int> nums)
        {
            int minim = nums.Min();
            int maxim = nums.Max();
            int result = int.MaxValue;
            for (int d = minim; d <= maxim; d++)
            {
                int fuel = nums.Sum(x =>
                            {
                                int diff = Math.Abs(d - x);
                                return diff * (diff + 1) / 2;
                            });
                result = (fuel < result)? fuel : result;
            }
            return result;
        }


        // Median
        private static int part1(List<int> nums)
        {
            nums.Sort();
            int median = nums[nums.Count / 2];
            return nums.Sum(n => Math.Abs(median - n));
        }
    }
}
