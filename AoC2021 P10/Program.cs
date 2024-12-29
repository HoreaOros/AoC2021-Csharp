using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            char[] seps = {'\n', '\r' };
            var data = sr.ReadToEnd().Split(seps, StringSplitOptions.RemoveEmptyEntries) ;

            Dictionary<char, int> illegal = new Dictionary<char, int>();
            illegal.Add(')', 3);
            illegal.Add(']', 57);
            illegal.Add('}', 1197);
            illegal.Add('>', 25137);
            Dictionary<char, char> pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('[', ']');
            pairs.Add('{', '}');
            pairs.Add('<', '>');
            List<string> incomplete = new List<string>();
            Console.WriteLine($"Part1: {part1(data, illegal, pairs, incomplete)}");
            
            Console.WriteLine($"Part2: {part2(incomplete, pairs)}");


        }

        private static long part2(List<string> incomplete, Dictionary<char, char> pairs)
        {
            long result = 0;
            var points = new Dictionary<char, int> 
                { ['('] = 1,
                  ['['] = 2,
                  ['{'] = 3,
                  ['<'] = 4 };
            var scores = new List<long>();
            for(int i = 0; i <incomplete.Count; i++)
            {
                Stack<char> stack = new Stack<char>();
                for (int j = 0; j < incomplete[i].Length; j++)
                {
                    if (incomplete[i][j] == '(' || incomplete[i][j] == '[' ||
                        incomplete[i][j] == '{' || incomplete[i][j] == '<')
                        stack.Push(incomplete[i][j]);
                    else 
                        stack.Pop();
                }
                long score = 0;
                while (stack.Count > 0)
                {
                    score = score * 5 + points[stack.Peek()];
                    stack.Pop();
                }
                scores.Add(score);
            }
            scores.Sort();
            result = scores[scores.Count / 2];

            return result;
        }

        private static int part1(string[] data, Dictionary<char, int> illegal, Dictionary<char, char> pairs, List<string> incomplete)
        {
            int result = 0;
            
            
            for (int i = 0; i < data.Length; i++)
            {
                Stack<char> stack = new Stack<char>();
                bool corrupted = false;
                int j;
                for(j = 0; j < data[i].Length; j++)
                {
                    if (data[i][j] == '(' || data[i][j] == '[' || 
                        data[i][j] == '{' || data[i][j] == '<')
                        stack.Push(data[i][j]);

                    else if (stack.Count > 0 && pairs[stack.Peek()] == data[i][j])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        corrupted = true;
                        result += illegal[data[i][j]];
                        break;
                    }
                }
                if (!corrupted)
                    incomplete.Add(data[i]);

            }

            return result;
        }
    }
}
