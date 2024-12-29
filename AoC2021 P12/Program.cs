using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AoC2021_P12
{
    internal class Program
    {
        static string filename = "input.txt";
        static int result1 = 0;
        static int result2 = 0;
        static Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
            ReadData(filename);
            part1();
            Console.WriteLine($"Part 1: {result1}");

            part2();
            Console.WriteLine($"Part 2: {result2}");

            Console.WriteLine();
        }

        private static void part2()
        {
            List<string> path = new List<string>();
            walk2("start", path);
        }

        private static void walk2(string node, List<string> path)
        {
            if (node == "end")
            {
                result2++;
                path.Add(node);
                //Print(path);
            }
            else
            {
                path.Add(node);
                //Print(path);
                foreach (string vecin in data[node])
                {
                    if(vecin != "start")
                        if (char.IsUpper(vecin[0]) ||
                            (char.IsLower(vecin[0]) && NoLowerDuplicate(path)) ||
                            (char.IsLower(vecin[0]) && !NoLowerDuplicate(path)) && !path.Contains(vecin))
                        {
                            walk2(vecin, new List<string>(path));
                        }
                }
            }
        }

        private static void Print(List<string> path)
        {
            foreach (string item in path)
                Console.Write($"{item},");
            Console.WriteLine();
        }

        private static bool NoLowerDuplicate(List<string> path)
        {
            List<string> visited = new List<string>();
            foreach (string node in path)
            {
                if (char.IsLower(node[0]))
                    if (visited.Contains(node))
                        return false;
                    else
                        visited.Add(node);
            }
            return true;
        }

        private static void part1()
        {
            List<string> path = new List<string>();
            walk("start", path);
        }

        private static void walk(string node, List<string> path)
        {
            if (node == "end")
                result1++;
            else
            {
                path.Add(node);
                foreach (string vecin in data[node])
                {
                    if(char.IsUpper(vecin[0]) || 
                        (char.IsLower(vecin[0]) && !path.Contains(vecin)))
                    {
                        walk(vecin, new List<string>(path));
                    }
                }
            }
        }

        private static void ReadData(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] t = line.Split(new char[] { '-' });
                string nod1 = t[0];
                string nod2 = t[1];
                if(!data.ContainsKey(nod1))
                    data[nod1] = new List<string>();
                data[nod1].Add(nod2);

                if (!data.ContainsKey(nod2))
                    data[nod2] = new List<string>();
                data[nod2].Add(nod1);
            }
        }
    }
}
