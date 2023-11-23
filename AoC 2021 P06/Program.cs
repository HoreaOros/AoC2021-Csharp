using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_P06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string[] tokens = sr.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> fish = new List<int>();
            foreach (string token in tokens)
                fish.Add(int.Parse(token));
            sr.Close();

            Console.WriteLine($"Part 1: {part1(new List<int>(fish), 80)}");
            Console.WriteLine($"Part 2: {part2(new List<int>(fish), 256)}");
        }

        private static decimal part2(List<int> fish, int days)
        {
            decimal[] c = new decimal[9];
            foreach (var item in fish)
                c[item]++;
            for (int i = 0; i < days; i++)
            {
                decimal[] newc = new decimal[9];
                newc[0] = c[1]; 
                newc[1] = c[2];
                newc[2] = c[3];
                newc[3] = c[4];
                newc[4] = c[5];
                newc[5] = c[6];
                newc[6] = c[7] + c[0];
                newc[7] = c[8];
                newc[8] = c[0];
                c = newc;
            }
            return c.Sum(x => x);
        }

        private static int part1(List<int> fish, int days)
        {

            for (int i = 0; i < days; i++)
            {
                int count = fish.Count;
                for (int j = 0; j < count; j++)
                {
                    if (fish[j] > 0)
                        fish[j]--;
                    else
                    {
                        fish[j] = 6;
                        fish.Add(8);
                    }
                }
                
            }

            return fish.Count;
        }
    }

}
