using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AoC2021_P20
{
    internal class Program
    {
        static string fileName = "input.txt";
        static Dictionary<(int r, int c), char> inputImage = new Dictionary<(int r, int c), char>();
        static int ROWS, COLS;
        static string algo;

        static void Main(string[] args)
        {
            ReadData(fileName);
            //PrintImage(inputImage, 0, 0, ROWS - 1, COLS - 1);
            part1();

            part2();

            Console.WriteLine();
        }

        private static void part2()
        {
            Dictionary<(int r, int c), char> newImage = inputImage;
            for (int i = 0; i < 50; i++)
            {
                newImage = Enhance(newImage, 0 - i, 0 - i, ROWS - 1 + i, COLS - 1 + i, i % 2 == 0?'.':'#');
            }
            Console.WriteLine(newImage.Count(x => x.Value == '#'));

        }

        private static void PrintImage(Dictionary<(int r, int c), char> inputImage, int r1, int c1, int r2, int c2)
        {
            for(int r = r1; r <= r2; r++)
            {
                for(int c = c1; c <= c2; c++)
                    Console.Write(inputImage[(r,c)]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void part1()
        {
            Dictionary<(int r, int c), char> newImage = Enhance(inputImage, 0, 0, ROWS - 1, COLS - 1, '.');
            //PrintImage(newImage, -1, -1, ROWS, COLS);


            Dictionary<(int r, int c), char> newImage2 = Enhance(newImage, -1, -1, ROWS, COLS, '#');
            //PrintImage(newImage2, -2, -2, ROWS + 1, COLS + 1);

            
            Console.WriteLine(newImage2.Count(x => x.Value == '#'));
        }

        private static Dictionary<(int r, int c), char> Enhance(Dictionary<(int r, int c), char> image, int r1, int c1, int r2, int c2, char ch)
        {
            int[] dr = {-1, -1, -1, 0, 0, 0,   1, 1, 1};
            int[] dc = {-1, 0, 1,  -1, 0, 1,  -1, 0, 1 };
            Dictionary<(int r, int c), char> newImage = new Dictionary<(int r, int c), char>();

            for(int r = r1 - 1; r <= r2 + 1; r++)
                for(int c = c1 - 1; c <= c2 + 1; c++)
                {
                    StringBuilder sb = new StringBuilder();
                    for(int k = 0; k < dr.Length; k++)
                    {
                        int nr = r + dr[k];
                        int nc = c + dc[k];
                        if (image.ContainsKey((nr, nc)))
                            sb.Append(image[(nr, nc)]);
                        else
                            sb.Append(ch);
                    }
                    string str = sb.ToString();
                    newImage[(r, c)] = algo[ConvertFromHashToDecimal(str)];
                }
            return newImage;
        }

        private static int ConvertFromHashToDecimal(string str)
        {
            Debug.Assert(str.Length == 9);
            int result = 0;
            foreach (var c in str)
                result = result * 2 + (c == '#' ? 1 : 0);
            Debug.Assert(result >= 0 && result < 512);
            return result;
        }

        private static void ReadData(string fileName)
        {
            string text = File.ReadAllText(fileName);
            string[] lines = text.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
            ROWS = lines.Length - 1;
            COLS = lines[1].Length;
            algo = lines[0];
            for (int i = 1; i <= ROWS; i++)
                for (int j = 0; j < COLS; j++)
                    inputImage[(i - 1, j)] = lines[i][j];
            for (int i = 1; i <= 10; i++)
                for (int j = -10; j < COLS + 10; j++)
                {
                    inputImage[(-i, j)] = '.';
                    inputImage[(ROWS + i, j)] = '.';
                }
            for(int i = -10; i < ROWS + 10; i++)
                for(int j = 1; j <= 10; j++)
                {
                    inputImage[(i, -j)] = '.';
                    inputImage[(i, COLS + j)] = '.';
                }
        }
    }
}
