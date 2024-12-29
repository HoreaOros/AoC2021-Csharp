using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2021_P08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            Regex r = new Regex("[a-z]+");
            var data = new List<(List<string> wires, List<string> digits)>();
            string line;
            while ((line = sr.ReadLine()) != null ) 
            { 
                MatchCollection col = r.Matches(line);
                List<string> wires = new List<string>();
                for(int i = 0; i < 10; i++)
                {
                    string s = String.Concat(col[i].Value.OrderBy(c => c));
                    wires.Add(s);
                }

                List<string> digits = new List<string>();
                for (int i = 10; i < 14; i++)
                {
                    string s = String.Concat(col[i].Value.OrderBy(c => c));
                    digits.Add(s);
                }

                data.Add((wires, digits));
            }
            sr.Close();

            Console.WriteLine($"Part 1: {part1(data)}");
            Console.WriteLine($"Part 2: {part2(data)}");

        }
        static IEnumerable<IEnumerable<T>>  GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        private static int part2(List<(List<string> wires, List<string> digits)> data)
        {
            int result = 0;

            string litere = "abcdefg";
            IEnumerable<IEnumerable<char>> permutations = GetPermutations(litere, 7);
            foreach (var item in data)
            {
                (List<string> w, List<string> d) = item;


                foreach (var perm in permutations)
                {
                    List<char> c0 = new List<char>();
                    List<char> c1 = new List<char>(); 
                    List<char> c2= new List<char>(); 
                    List<char> c3 = new List<char>();
                    List<char> c4 = new List<char>();
                    List<char> c5 = new List<char>();
                    List<char> c6 = new List<char>();
                    List<char> c7 = new List<char>();
                    List<char> c8 = new List<char>();
                    List<char> c9 = new List<char>();
                    List<char> p = perm.ToList();

                    c0.Add(p[0]); c0.Add(p[1]); c0.Add(p[2]); c0.Add(p[4]); c0.Add(p[5]); c0.Add(p[6]); c0.Sort();
                    c1.Add(p[2]); c1.Add(p[5]);  c1.Sort();
                    c2.Add(p[0]); c2.Add(p[2]); c2.Add(p[3]); c2.Add(p[4]); c2.Add(p[6]);   c2.Sort();
                    c3.Add(p[0]); c3.Add(p[2]); c3.Add(p[3]); c3.Add(p[5]); c3.Add(p[6]);   c3.Sort();
                    c4.Add(p[1]); c4.Add(p[2]); c4.Add(p[3]); c4.Add(p[5]);       c4.Sort();
                    c5.Add(p[0]); c5.Add(p[1]); c5.Add(p[3]); c5.Add(p[5]); c5.Add(p[6]);         c5.Sort();
                    c6.Add(p[0]); c6.Add(p[1]); c6.Add(p[3]); c6.Add(p[4]); c6.Add(p[5]); c6.Add(p[6]);     c6.Sort();
                    c7.Add(p[0]); c7.Add(p[2]); c7.Add(p[5]);    c7.Sort();
                    c8.Add(p[0]); c8.Add(p[1]); c8.Add(p[2]); c8.Add(p[3]); c8.Add(p[4]); c8.Add(p[5]); c8.Add(p[6]);   c8.Sort();
                    c9.Add(p[0]); c9.Add(p[1]); c9.Add(p[2]); c9.Add(p[3]); c9.Add(p[5]); c9.Add(p[6]);         c9.Sort();

                    List<string> c09 = new List<string>();
                    c09.Add(new string(c0.ToArray()));    c09.Add(new string(c1.ToArray()));   c09.Add(new string(c2.ToArray()));
                    c09.Add(new string(c3.ToArray()));    c09.Add(new string(c4.ToArray()));   c09.Add(new string(c5.ToArray()));
                    c09.Add(new string(c6.ToArray()));    c09.Add(new string(c7.ToArray()));   c09.Add(new string(c8.ToArray()));
                    c09.Add(new string(c9.ToArray()));

                    //Verific daca c09 e indentic cu w

                    var a = w.All(c09.Contains) && w.Count == c09.Count;
                    if(a)
                    {
                        // am gasit permutarea
                        // decodific 
                        int d1, d2, d3, d4;
                        d1 = c09.IndexOf(d[0]);
                        d2 = c09.IndexOf(d[1]);
                        d3 = c09.IndexOf(d[2]);
                        d4 = c09.IndexOf(d[3]);
                        result += 1000 * d1 + 100 * d2 + 10 * d3 + d4;
                        break;
                    }
                }
            }
            return result;
        }

        private static int part1(List<(List<string> wires, List<string> digits)> data)
        {
            int count = 0;
            int[] d = { 2, 3, 4, 7 };
            foreach (var item in data)
            {
                foreach (var w in item.digits) 
                    if(d.Contains(w.Length))
                        count++; 
            }

            return count;
        }
    }
}
