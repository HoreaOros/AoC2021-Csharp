using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P25
{
    internal class Program
    {
        static string fileName = "input.txt";
        static char[,] map;
        static int ROWS, COLS;
        static void Main(string[] args)
        {
            ReadData(fileName);
            part1(map, ROWS, COLS);
        }

        private static void part1(char[,] map, int rOWS, int cOLS)
        {
            int rounds = 0;
            while(true)
            {
                HashSet<(int r, int c)> east = GetMovers(map, ROWS, COLS, (0, 1), '>');
                if (east.Count > 0)
                    Move(east, map, ROWS, COLS, (0, 1));


                HashSet<(int r, int c)> south = GetMovers(map, ROWS, COLS, (1, 0), 'v');
                if (south.Count > 0)
                    Move(south, map, ROWS, COLS, (1, 0));

                rounds++;
                if (east.Count == 0 && south.Count == 0)
                    break;
            }
            Console.WriteLine(rounds);
        }

        private static void Move(HashSet<(int r, int c)> movers, char[,] map, int rOWS, int cOLS, (int r, int c) delta)
        {
            foreach(var item in movers)
            {
                int nr = (item.r + delta.r) % rOWS;
                int nc = (item.c + delta.c) % cOLS;

                map[nr, nc] = map[item.r, item.c];
                map[item.r, item.c] = '.';
            }
        }

        private static HashSet<(int r, int c)> GetMovers(char[,] map, int rOWS, int cOLS, (int r, int c) delta, char ch)
        {
            HashSet<(int r, int c)> movers = new HashSet<(int r, int c)> ();
            for (int i = 0; i < ROWS; i++)
                for (int j = 0; j < COLS; j++)
                    if (map[i, j] == ch && map[(i + delta.r) % rOWS, (j + delta.c) % cOLS] == '.')
                        movers.Add((i, j));
            return movers;
        }

        private static void ReadData(string fileName)
        {
            string text = File.ReadAllText(fileName);
            string[] lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ROWS = lines.Length;
            COLS = lines[0].Length;
            map = new char[ROWS, COLS];
            for(int i = 0; i < ROWS; i++)
                for(int j = 0; j < COLS; j++)
                    map[i, j] = lines[i][j];
        }
    }
}
