using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;

namespace AoC2021
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            List<int> nums = new List<int>();
            string line;
            while((line = sr.ReadLine()) != null)
                nums.Add(int.Parse(line));
            sr.Close();

            Console.WriteLine($"Part 1: {part1(nums)}");
            Console.WriteLine($"Part 2: {part2(nums)}");

        }

        private static int part2(List<int> nums)
        {
            int prevWindow = nums[0] + nums[1] + nums[2];
            int count = 0;

            for (int i = 3; i < nums.Count; i++)
            {
                int currentWindow = prevWindow + nums[i] - nums[i - 3];
                if (currentWindow > prevWindow)
                    count++;
            }

            return count;
        }

        private static int part1(List<int> nums)
        {
            int count = 0;
            for (int i = 1; i < nums.Count; i++)
                if (nums[i] > nums[i - 1])
                    count++;
            return count;
        }
    }
}
