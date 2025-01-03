using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2021_P22
{
    internal class Program
    {
        static string fileName = "input.txt";
        static List<(string chg, int x1, int x2, int y1, int y2, int z1, int z2)> comms = new List<(string chg, int x1, int x2, int y1, int y2, int z1, int z2)>();
        static void Main(string[] args)
        {
            ReadData(fileName);
            part1();
        }

        private static void part1()
        {
            Dictionary<(int x, int y, int z), bool> map = new Dictionary<(int x, int y, int z), bool>();
            foreach(var comm in comms)
            {
                Console.WriteLine(comm);
                (string chg, int x1, int x2, int y1, int y2, int z1, int z2) = comm;
                if(x1 >= -50 && x2 <= 50 &&  y1 >= -50 && y2 <= 50 && z1 >= -50 && z2 <= 50)
                for(int x = x1; x <= x2; x++)
                    for(int y = y1; y <= y2; y++)
                        for(int z = z1; z <= z2; z++)
                            map[(x, y, z)] = (chg == "on"?true:false);
            }
            Console.WriteLine(map.Count(x => x.Value == true));
        }

        private static void ReadData(string fileName)
        {
            string text = File.ReadAllText(fileName);
            string[] lines = text.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            //on x = -54112..-39298, y = -85059..-49293, z = -27449..7877
            Regex r = new Regex(@"(on|off) x=(-?\d+)..(-?\d+),y=(-?\d+)..(-?\d+),z=(-?\d+)..(-?\d+)");
            foreach (string line in lines)
            {
                Match m = r.Match(line);
                comms.Add((m.Groups[1].Value, 
                    int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), 
                    int.Parse(m.Groups[4].Value), int.Parse(m.Groups[5].Value), 
                    int.Parse(m.Groups[6].Value), int.Parse(m.Groups[7].Value)));

            }
            Console.WriteLine();
        }
    }
}
