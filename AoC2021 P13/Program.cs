using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace AoC2021_P13
{
    internal class Program
    {
        static bool[,] data;
        static int lines = 0, cols = 0;
        static List<(int x, int y)> points = new List<(int x, int y)> ();
        static List<(string, int)> folds = new List<(string, int)> ();
        static void Main(string[] args)
        {
            ReadData();
            data = CreateMap();

            Console.WriteLine($"Part 1: {part1()}");
            part2();
        }

        private static void part2()
        {
            for (int i = 1; i < folds.Count; i++)
                (lines, cols) = fold(lines, cols, folds[i]);

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < cols; j++)
                    if (data[i, j])
                        Console.Write('0');
                    else
                        Console.Write('.');

                Console.WriteLine();
            }
        }

        private static bool[,] CreateMap()
        {
            data = new bool[lines, cols];
            foreach (var p in points) 
            {
                (int col, int row) = p;
                data[row, col] = true;
            }
            return data;
        }

        private static int part1()
        {
            (int r, int c) dj = fold(lines, cols, folds[0]);
            (lines, cols) = dj;
            return CountVisible(dj);
            
        }

        private static (int r, int c) fold(int lines, int cols, (string ax, int n) value)
        {
            (int r, int c) result;
            if (value.ax == "y")
                result = foldUp(lines, cols, value.n);
            else
                result = foldLeft(lines, cols, value.n);
            return result;
        }

        private static (int r, int c) foldLeft(int lines, int cols, int n)
        {
            for (int i = 0; i < lines; i++)
                for (int j = n + 1; j < cols; j++)
                {
                    data[i, n - (j-n)] |= data[i, j];
                }
            return (lines, n);
        }

        private static (int r, int c) foldUp(int lines, int cols, int n)
        {
            for(int i = n + 1; i < lines; i++)
                for(int j = 0; j < cols; j++)
                {
                    data[n - (i - n), j] |= data[i, j];
                }
            return (n, cols);
        }

        private static int CountVisible((int r, int c) dj)
        {
            int result = 0;
            for (int i = 0; i < dj.r; i++)
                for (int j = 0; j < dj.c; j++)
                    if (data[i, j])
                        result++;

            return result;
        }

        private static void ReadData()
        {
            string line;
            Regex r = new Regex("(\\d+),(\\d+)");
            StreamReader sr = new StreamReader("input.txt");
            while((line = sr.ReadLine()) != "") 
            { 
                Match m = r.Match(line);
                int x = (int.Parse(m.Groups[1].Value)); 
                int y = (int.Parse(m.Groups[2].Value)); 

                if(y > lines)
                    lines = y;
                if(x > cols) 
                    cols = x;

                points.Add((x, y));
                Console.WriteLine(m);
            }

            lines++; cols++;
            //fold along x = 655
            //fold along y = 447
            r = new Regex("fold along (x|y)=(\\d+)");
            while ((line = sr.ReadLine()) != null)
            {
                Match m = r.Match(line);
                folds.Add((m.Groups[1].Value, int.Parse(m.Groups[2].Value)));
            }
        }
    }
}
