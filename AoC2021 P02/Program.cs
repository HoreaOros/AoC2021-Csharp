using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AoC2021_P02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string line;

            List<(string, int)> comms = new List<(string, int)> ();
            while ((line = sr.ReadLine()) != null)
            {
                string[] t = line.Split(' ');
                comms.Add((t[0], int.Parse(t[1])));
            }
            sr.Close();

            Console.WriteLine($"Part 1: {part1(comms)}");
            Console.WriteLine($"Part 2: {part2(comms)}");
        }

        private static int part2(List<(string, int)> comms)
        {
            int hPos = 0;
            int depth = 0;
            int aim = 0;

            foreach (var item in comms)
            {
                string comm;
                int value;
                (comm, value) = item;
                if (comm == "forward")
                {
                    hPos += value;
                    depth += aim * value;
                }
                else if (comm == "down")
                {
                    aim += value;
                }
                else
                {
                    aim -= value;
                }
            }

            return hPos * depth;
        }

        private static int part1(List<(string, int)> comms)
        {
            int hPos = 0;
            int depth = 0;

            foreach (var item in comms) 
            {
                string comm;
                int value; 
                (comm, value) = item;
                if (comm == "forward")
                    hPos += value;
                else if (comm == "down")
                    depth += value;
                else
                    depth -= value;
            }

            return hPos * depth;
        }
    }
}
