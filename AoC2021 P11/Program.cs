using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P11
{
    internal class Program
    {
        static readonly int rows = 10, cols = 10;
        static int[,] data = new int[rows, cols];
        static void Main(string[] args)
        {
            ReadData();
            PrintData("Before any steps:");
            //Console.WriteLine($"Part1: {part1()}");

            //part2 nu merge corect daca a rulat part1. 
            Console.WriteLine($"Part2: {part2()}");
        }

        private static int part2()
        {
            int step = 1;
            int flashes = 0;
            while (Step(ref flashes, step) != rows * cols)
                step++;
            return step;
        }

        private static void PrintData(string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                    Console.Write(data[i,j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void ReadData()
        {
            StreamReader sr = new StreamReader("input.txt");
            for (int i = 0; i < rows; i++)
            {
                string line = sr.ReadLine();
                for (int j = 0; j < cols; j++)
                    data[i, j] = line[j] - '0';
            }
        }
        private static int part1()
        {
            int flashes = 0;

            for (int step = 1; step <= 100; step++)
            {
                Step(ref flashes, step);
            }
            return flashes;
        }

        private static int Step(ref int flashes, int step)
        {
            int currentFlashes = 0;
            // First, the energy level of each octopus increases by 1.
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    data[i, j]++;
            //Then, any octopus with an energy level greater than 9 flashes.
            //This increases the energy level of all adjacent octopuses by 1,
            //including octopuses that are diagonally adjacent.
            //If this causes an octopus to have an energy level greater than 9, it also flashes.
            //This process continues as long as new octopuses keep having their energy level increased beyond 9.
            //(An octopus can only flash at most once per step.)

            bool flashing;
            do
            {
                flashing = false;
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        if (data[i, j] == 10)
                        {
                            //flashes++;
                            flashing = true;
                        }

                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        if (data[i, j] == 10)
                        {
                            flashes++;
                            currentFlashes++;
                            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
                            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };
                            for (int k = 0; k < dr.Length; k++)
                            {
                                int newr = i + dr[k];
                                int newc = j + dc[k];
                                if (newr >= 0 && newr < rows && newc >= 0 && newc < cols)
                                    if (data[newr, newc] < 10)
                                        data[newr, newc]++;
                            }
                            data[i, j] = 100;
                        }

            } while (flashing);


            // Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (data[i, j] > 9)
                        data[i, j] = 0;
            PrintData($"After step: {step}");
            return currentFlashes; // numarul de flashuri la pasul curent
        }
    }
 }
