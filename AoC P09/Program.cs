using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AoC_P09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            int lins = 100, cols = 100;
            string line;
            int[,] map = new int[lins, cols];
            for(int i = 0; i < lins;  i++)
            {
                line = sr.ReadLine();
                for(int j = 0; j < cols; j++)
                    map[i, j] = line[j] - '0';
            }
            sr.Close();

            int result1;
            List<(int row, int col)> lowPoints;
            (result1, lowPoints) = part1(map);
            Console.WriteLine($"Part 1: {result1}");

            Console.WriteLine($"Part 2: {part2(map, lowPoints)}");
        }

        private static int part2(int[,] map, List<(int row, int col)> lowPoints)
        {
            int result;
            int max1 = 0, max2 = 0, max3 = 0;

            for(int i = 0; i < lowPoints.Count; i++)
            {
                int size = walk(lowPoints[i], map);
                if (size >= max1)
                {
                    max3 = max2;
                    max2 = max1;
                    max1 = size;
                }
                else if (size >= max2)
                {
                    max3 = max2;
                    max2 = size;
                }
                else if (size > max3)
                    max3 = size;
            }
            Console.WriteLine($"{max1} {max2} {max3}");
            result = max1 * max2 * max3;
            return result;
        }

        private static int walk((int row, int col) value, int[,] map)
        {
            int[] dr = { -1, 1, 0, 0 };
            int[] dc = { 0, 0, -1, 1 };
            var Q = new Queue<(int row, int col)> ();
            Q.Enqueue(value);
            int count = 0;
            while(Q.Count > 0)
            {
                var current = Q.Dequeue();
                count++;
                for(int k = 0; k < dr.Length; k++)
                {
                    int newr = current.row + dr[k];
                    int newc = current.col + dc[k];
                    if (newr >= 0 && newr < map.GetLength(0) && newc >= 0 && newc < map.GetLength(1))
                    {
                        if (!Q.Contains((newr, newc)) && map[newr, newc] < 9 && (map[newr, newc] > map[current.row, current.col]))
                            Q.Enqueue((newr, newc));
                    }
                }
                map[current.row, current.col] = 9;
            }

            return count;
        }

        private static (int, List<(int row, int col)>) part1(int[,] map)
        {
            int[] dr = { -1, 1, 0, 0 };
            int[] dc = { 0, 0, -1, 1 };
            int result = 0;
            var lowPoints = new List<(int row, int col)>();
            for(int i = 0; i < map.GetLength(0); i++)
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    int newr, newc;
                    bool ok = true;
                    for(int k = 0; k < dr.Length; k++)
                    {
                        newr = i + dr[k];
                        newc = j + dc[k];
                        if (newr >= 0 && newr < map.GetLength(0) && newc >= 0 && newc < map.GetLength(1))
                            if (map[newr, newc] <= map[i, j])
                                ok = false;
                    }
                    if (ok)
                    { 
                        result += map[i,j] + 1;
                        lowPoints.Add((i, j));
                    }
                }
            return (result, lowPoints);
        }
    }
}
