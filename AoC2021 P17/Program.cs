using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2021_P17
{
    internal class Program
    {
        static string filename = "input.txt";
        static int x1, x2, y1, y2;
        static void Main(string[] args)
        {
            ReadData(filename);
            Console.WriteLine($"{x1} {x2} {y1} {y2}");
            part1();
        }

        private static void part1()
        {
            List<(int x, int step)> xs = new List<(int x, int step)>();
            for(int x = x2; x > 0; x--)
            {
                int c_x = x;
                int current_x = 0;
                int steps = 0;
                bool add = false;
                while(current_x <= x2)
                {
                    steps++;
                    current_x += c_x;
                    if (current_x >= x1 && current_x <= x2)
                    {
                        xs.Add((x, steps));
                        add = true;
                    }
                    if(c_x > 0)
                        c_x--;
                    if (c_x == 0)
                        break;
                }
                if(current_x >= x1 && current_x <= x2 && c_x == 0)
                {
                    for (int i = 0; i < 3000; i++) // 3000 ???? valoare din "burta"
                        xs.Add((x, ++steps));
                }
            }
            List<(int x, int y, int step)> r = new List<(int x, int y, int step)>();
            foreach(var item in xs)
            {
                int steps = item.step;
                for(int y = -1000; y <= 1000; y++)
                {
                    int pozy = (2 * y - steps + 1) * (steps) / 2;
                    if (pozy >= y1 && pozy <= y2)
                        r.Add((item.x, y, steps));
                }
            }

            //part 1 answer
            Console.WriteLine(r.Max(x => y1 * (y1 + 1) / 2));

            HashSet<(int, int)> hs = new HashSet<(int, int)>();
            foreach (var item in r)
                hs.Add((item.x, item.y));
            //part 2 answer
            Console.WriteLine(hs.Count);
        }

        private static void ReadData(string filename)
        {
            string text = File.ReadAllText(filename);
            Regex r = new Regex(@"-?\d+");
            MatchCollection match = r.Matches(text);
            x1 = int.Parse(match[0].Value);
            x2 = int.Parse(match[1].Value);
            y1 = int.Parse(match[2].Value);
            y2 = int.Parse(match[3].Value);
        }
    }
}
