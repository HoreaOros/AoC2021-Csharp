using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2021_P05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string line;
            
            Regex rx = new Regex("\\d+");
            int x1, y1, x2, y2;
            var lines = new List<(int x1, int y1, int x2, int y2)>();
            while ((line = sr.ReadLine()) != null)
            {
                MatchCollection matches = rx.Matches(line);
                x1 = int.Parse(matches[0].Value);
                y1 = int.Parse(matches[1].Value);
                x2 = int.Parse(matches[2].Value);
                y2 = int.Parse(matches[3].Value);
                lines.Add((x1, y1, x2, y2));
            }
            sr.Close();

            Console.WriteLine($"Part 1: {part1(lines)}");
            Console.WriteLine($"Part 2: {part2(lines)}");

        }

        private static object part2(List<(int x1, int y1, int x2, int y2)> lines)
        {
            int[,] diagram = new int[10000, 10000];
            int result = 0;
            int x1, y1, x2, y2;
            foreach (var item in lines)
            {
                (x1, y1, x2, y2) = item;
                if(x1 == x2 && y1 == y2)
                        Console.WriteLine($"Hey {x1}, {y1}, {x2}, {y2}");
                if (x1 == x2 || y1 == y2)
                {
                    if (x1 > x2)
                        (x1, x2) = (x2, x1);
                    if (y1 > y2)
                        (y1, y2) = (y2, y1);
                    if (x1 == x2) // vertical
                        for (int i = y1; i <= y2; i++)
                            diagram[i, x1]++;
                    else if (y1 == y2) // horizontal
                        for (int j = x1; j <= x2; j++)
                            diagram[y1, j]++;
                }
                else // diagonal
                {
                    int dx = -Math.Sign(x1 - x2);
                    int dy = -Math.Sign(y1 - y2);
                    while (x1 != x2)
                    {
                        diagram[y1, x1]++;
                        x1 += dx;
                        y1 += dy;
                    }
                    diagram[y1, x1]++;
                }
            }
            result = CountOverlap(diagram);
            return result;
        }

        private static int part1(List<(int x1, int y1, int x2, int y2)> lines)
        {
            int[,] diagram = new int[10000, 10000];
            int result = 0;
            int x1, y1, x2, y2;
            foreach (var item in lines)
            {
                (x1, y1, x2, y2) = item;
                if (x1 > x2)
                    (x1, x2) = (x2, x1);
                if (y1 > y2)
                    (y1, y2) = (y2, y1);
                if (x1 == x2 || y1 == y2)
                {
                    if (x1 == x2)
                        for (int i = y1; i <= y2; i++)
                            diagram[i, x1]++;
                    else if (y1 == y2)
                        for (int j = x1; j <= x2; j++)
                            diagram[y1, j]++;
                }

            }
            result = CountOverlap(diagram);
            return result;
        }

        private static int CountOverlap(int[,] diagram)
        {
            int result = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                    if (diagram[i, j] >= 2)
                        result++;
            }

            return result;
        }
    }
}
