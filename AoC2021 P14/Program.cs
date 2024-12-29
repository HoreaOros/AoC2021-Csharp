using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P14
{
    internal class Program
    {
        static string polymer;
        static string filename = "input.txt";
        static Dictionary<string, string> map = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            ReadData();
            Console.WriteLine($"Part 1: {part1(10)}");
            Console.WriteLine($"Part 2: {part2(40)}");
        }

        private static ulong part2(int steps)
        {
            ulong result = 0;
            string pol = string.Copy(polymer);

            Dictionary<string,  ulong> pairFrecv = new Dictionary<string, ulong>();
            foreach (var item in map.Keys)
                pairFrecv.Add(item, 0);
            for(int i = 0; i < pol.Length - 1; i++)
                pairFrecv[pol.Substring(i, 2)]++;


            for(int i = 0; i < steps; i++)
            {
                Dictionary<string, ulong> pairFrecvNew = new Dictionary<string, ulong>();
                foreach(var item in pairFrecv.Keys)
                    if (pairFrecv[item] != 0)
                    {
                        string pair1 = new string(new char[] { item[0], map[item][0] });
                        string pair2 = new string(new char[] { map[item][0], item[1] });


                        if(pairFrecvNew.ContainsKey(pair1))
                            pairFrecvNew[pair1] += pairFrecv[item];
                        else
                            pairFrecvNew[pair1] = pairFrecv[item];

                        if (pairFrecvNew.ContainsKey(pair2))
                            pairFrecvNew[pair2] += pairFrecv[item];
                        else
                            pairFrecvNew[pair2] = pairFrecv[item];

                    }
                pairFrecv = pairFrecvNew;
            }



            Dictionary<char, ulong> f = new Dictionary<char, ulong>();
            foreach (var item in pairFrecv.Keys)
            {
                char c1 = item[0];
                char c2 = item[1];
                if (f.ContainsKey(c1))
                    f[c1] += pairFrecv[item];
                else
                    f[c1] = pairFrecv[item];

                if (f.ContainsKey(c2))
                    f[c2] += pairFrecv[item];
                else
                    f[c2] = pairFrecv[item];

            }
            f[polymer.First()]++;
            f[polymer.Last()]++;
            Console.WriteLine(f.Values.Max() / 2);
            Console.WriteLine(f.Values.Min() / 2);

            result = f.Values.Max()  / 2- f.Values.Min() / 2;

            return result;
        }

        private static int part1(int steps)
        {
            int result = 0;
            string pol = string.Copy(polymer);

            for(int i = 0; i < steps; i++)
            {
                pol = Step(pol);
                Console.WriteLine($"Step {i + 1} - length = {pol.Length}");
            }

            Dictionary<char, int> f = new Dictionary<char, int>();
            foreach (var item in pol)
            {
                if (f.ContainsKey(item))
                    f[item]++;
                else
                    f[item] = 1;
            }
            result = f.Values.Max() - f.Values.Min();
            return result;
        }

        private static string Step(string pol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(pol[0]);
            for(int i = 0; i < pol.Length - 1; i++)
            {
                string s = pol.Substring(i, 2);
                sb.Append(map[s]);
                sb.Append(s[1]);
            }
            return sb.ToString();
        }

        private static void ReadData()
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            polymer = sr.ReadLine();
            sr.ReadLine();

            while((line = sr.ReadLine()) != null)
            {
                string[] t = line.Split(new char[] {' ', '-', '>'}, StringSplitOptions.RemoveEmptyEntries);
                map[t[0]] = t[1];
            }

        }
    }
}
