using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P15
{
    internal class Program
    {
        static int[,] data;
        static string filename = "input.txt";
        static List<string> lines = new List<string>();
        static void Main(string[] args)
        {
            ReadData();
            Console.WriteLine($"Part 1: {part1()}");
            Console.WriteLine($"Part 1: {part11()}");
            Console.WriteLine($"Part 2: {part2()}");
            Console.WriteLine();

        }

        private static int part11()
        {
            //Bellman-Ford

            int result;
            List<(int r, int c)> vertices = new List<(int r, int c)>();
            List<((int r, int c) a, (int r, int c) b, int w)> edges =
                new List<((int r, int c) a, (int r, int c) b, int w)>();

            int[] dc = { 0, 0, -1, 1 };
            int[] dr = { -1, 1, 0, 0 };

            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    vertices.Add((i, j));
                    for (int k = 0; k < dr.Length; k++)
                    {
                        int ni = i + dr[k], nj = j + dc[k];
                        if (ni >= 0 && nj >= 0 && ni < data.GetLength(0) && nj < data.GetLength(1))
                            edges.Add(((i, j), (ni, nj), data[ni, nj]));
                    }
                }

            var distance = new Dictionary<(int r, int c), int>();
            var predecessor = new Dictionary<(int r, int c), (int r, int c)>();
            foreach (var item in vertices)
                distance[item] = 1000000000;
            distance[(0, 0)] = 0;


            for (int i = 0; i < vertices.Count - 1; i++)
                foreach (var item in edges)
                {
                    if (distance[item.a] + item.w < distance[item.b])
                    {
                        distance[item.b] = distance[item.a] + item.w;
                        predecessor[item.b] = item.a;
                    }
                }

            result = distance[(data.GetLength(0) - 1, data.GetLength(1) - 1)];
            return result;
        }

        private static int part2()
        {
            int result = 0;
            int[,] dataL = new int[data.GetLength(0) * 5, data.GetLength(1) * 5];

            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    for (int i = 0; i < data.GetLength(0); i++)
                        for (int j = 0; j < data.GetLength(1); j++)
                        {
                            int v = (data[i, j] + r + c) % 9;
                            if (v == 0)
                                v = 9;
                            dataL[r * data.GetLength(0) + i, c * data.GetLength(1) + j] = v;
                        }

            //Bellman-Ford

            
            List<(int r, int c)> vertices = new List<(int r, int c)> ();
            List<((int r, int c) a, (int r, int c) b, int w)> edges =
                new List<((int r, int c) a, (int r, int c) b, int w)>();

            int[] dc = {0, 0, -1, 1 };
            int[] dr = {-1, 1, 0, 0 };

            for (int i = 0; i < dataL.GetLength(0); i++)
                for(int j = 0; j < dataL.GetLength(1); j++)
                {
                    vertices.Add((i, j));
                    for(int k = 0; k < dr.Length; k++)
                    {
                        int ni = i + dr[k],  nj = j + dc[k];
                        if (ni >= 0 && nj >= 0 && ni < dataL.GetLength(0) && nj < dataL.GetLength(1))
                            edges.Add(((i, j), (ni, nj), dataL[ni, nj]));
                    }
                }

            var distance = new Dictionary<(int r, int c), int> ();
            var predecessor = new Dictionary<(int r, int c), (int r, int c)>();
            foreach (var item in vertices)
                distance[item] = 1000000000;
            distance[(0, 0)] = 0;


            for(int i = 0; i < vertices.Count - 1; i++)
            {
                if(i % 1000 == 0)
                    Console.Write($"{i} ");
                foreach (var item in edges)
                {
                    if (distance[item.a] + item.w < distance[item.b])
                    {
                        distance[item.b] = distance[item.a] + item.w;
                        predecessor[item.b] = item.a;
                    }
                }
            }

            result = distance[(dataL.GetLength(0) - 1, dataL.GetLength(1) - 1)];
            return result;

        }

        static void solve(int[,] sums, int[,] d)
        {
            sums[0, 1] = d[0, 1];
            sums[1, 0] = d[1, 0];
            for (int i = 2; i < d.GetLength(0); i++)
                sums[i, 0] = d[i, 0] + sums[i - 1, 0];

            for (int j = 2; j < d.GetLength(1); j++)
                sums[0, j] = d[0, j] + sums[0, j - 1];

            for (int i = 1; i < d.GetLength(0); i++)
                for (int j = 1; j < d.GetLongLength(1); j++)
                    if (sums[i - 1, j] < sums[i, j - 1])
                        sums[i, j] += d[i, j] + sums[i - 1, j];
                    else
                        sums[i, j] += d[i, j] + sums[i, j - 1];
        }
        private static int part1()
        {
            int result;
            int[,] sums = new int[data.GetLength(0), data.GetLength(1)];


            solve(sums, data);

            result = sums[data.GetLength(0) - 1, data.GetLength(1) - 1];
            return result;

        }

        private static void ReadData()
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            while ((line = sr.ReadLine()) != null)
                lines.Add(line);
            sr.Close();
            data = new int[lines.Count, lines[0].Length];

            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    data[i, j] = lines[i][j] - '0';
        }
    }
}
