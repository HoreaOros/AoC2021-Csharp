using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2021_P19
{
    internal class Program
    {
        static string fileName = "input.txt";
        static List<Scanner> scanners = new List<Scanner>();
        static List<(int x, int y, int z)> allBeacons = new List<(int x, int y, int z)> ();
        static void Main(string[] args)
        {
            ReadData(fileName);
            foreach (Scanner scanner in scanners)
            {
                Console.WriteLine (scanner.Beacons.Count);
            }
            part1();
            Console.WriteLine();
        }

        private static void part1()
        {
            GetOverlap(allBeacons, scanners[1]);
        }

        private static List<(int x, int y, int z)> GetOverlap(List<(int x, int y, int z)> allb, Scanner s)
        {
            for(int i = 0; i < s.AllBeaconsRotations.Count; i++)
            {
                var b = s.AllBeaconsRotations[i];
                var res = IsOverlapBeacons(allb, b);
                if(res.r == true)
                {
                    (int dx, int dy, int dz) = res.diff;
                    foreach(var item in s.AllBeaconsRotations[i])
                    {
                        (int x, int y, int z) = item;
                        allBeacons.Add((x + dx, y + dy, z + dz));
                    }

                    break;
                }
            }

            return null;
        }

        private static (bool r, (int, int, int) diff) IsOverlapBeacons(List<(int x, int y, int z)> allb, List<(int x, int y, int z)> b)
        {
            int t = allb.Count - b.Count + 1;
            for(int k = 0; k < t; k++)
            {
                var diff = new Dictionary<(int, int, int), int>();
                for (int i = 0; i < b.Count; i++)
                {
                    var key = (allb[i + k].x - b[i].x, allb[i + k].y - b[i].y, allb[i + k].z - b[i].z); 
                    if(diff.ContainsKey(key))
                    {
                        diff[key]++;
                        if (diff[key] >= 12)
                            return (true, key);
                    }
                    else
                        diff[key] = 1;
                }
            }
            return (false, (0, 0, 0));
        }

        private static void ReadData(string fileName)
        {
            string text = File.ReadAllText(fileName);
            string[] t = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            

            for (int i = 0; i < t.Length; i++)
            {
                string[] lines = t[i].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                Scanner scanner = new Scanner(i);

                Regex r = new Regex(@"(-?\d+),(-?\d+),(-?\d+)");
                for(int k = 1; k < lines.Length; k++)
                {
                    Match match = r.Match(lines[k]);
                    scanner.Beacons.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)));
                }
                scanner.Beacons.Sort(new TupleCompare());
                if (i > 0)
                    scanner.GetRotations();
                else
                    scanner.AllBeaconsRotations.Add(scanner.Beacons);
                scanners.Add(scanner);
                Console.WriteLine(scanner);
            }
            allBeacons.AddRange(scanners[0].Beacons);
        }
    }

    internal class TupleCompare : IComparer<(int x, int y, int z)>
    {
        public int Compare((int x, int y, int z) t1, (int x, int y, int z) t2)
        {
            if (t1.x < t2.x)
                return -1;
            else if (t1.x > t2.x)
                return 1;
            else if (t1.y < t2.y)
                return -1;
            else if (t1.y > t2.y)
                return 1;
            else if (t1.z < t2.z)
                return -1;
            else if(t1.z > t2.z)
                return 1;
            else return 0;
        }
    }

    class Scanner
    {
        public List<(int x, int y, int z)> Beacons { get; set; }
        public List<List<(int x, int y, int z)>> AllBeaconsRotations = new List<List<(int x, int y, int z)>>();  
        public Scanner(int i)
        {
            Index = i;
            Beacons = new List<(int x, int y, int z)>();
        }

        public int Index { get; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in Beacons)
            {
                sb.Append(item);sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        internal void GetRotations()
        {
            List<(int x, int y, int z)> lst = Beacons;
            for (int i = 0; i < 4; i++)
            {
                lst = GetRotation(lst);
                AllBeaconsRotations.Add(lst);
            }
            
            lst = new List<(int x, int y, int z)>();
            foreach(var item in Beacons)
                lst.Add((-item.x, item.y, item.z));
            for (int i = 0; i < 4; i++)
            {
                lst = GetRotation(lst);
                AllBeaconsRotations.Add(lst);
            }


            lst = new List<(int x, int y, int z)>();
            foreach (var item in Beacons)
                lst.Add((item.y, item.x, item.z));
            for (int i = 0; i < 4; i++)
            {
                lst = GetRotation(lst);
                AllBeaconsRotations.Add(lst);
            }


            lst = new List<(int x, int y, int z)>();
            foreach (var item in Beacons)
                lst.Add((-item.y, item.x, item.z));
            for (int i = 0; i < 4; i++)
            {
                lst = GetRotation(lst);
                AllBeaconsRotations.Add(lst);
            }


            lst = new List<(int x, int y, int z)>();
            foreach (var item in Beacons)
                lst.Add((item.z, item.x, item.y));
            for (int i = 0; i < 4; i++)
            {
                lst = GetRotation(lst);
                AllBeaconsRotations.Add(lst);
            }

            lst = new List<(int x, int y, int z)>();
            foreach (var item in Beacons)
                lst.Add((-item.z, item.x, item.y));
            for (int i = 0; i < 4; i++)
            {
                lst = GetRotation(lst);
                AllBeaconsRotations.Add(lst);
            }
        }

        private List<(int x, int y, int z)> GetRotation(List<(int x, int y, int z)> beacons)
        {
            List<(int x, int y, int z)> lst = new List<(int x, int y, int z)>();
            foreach(var item in Beacons)
            {
                lst.Add((item.x, -item.z, item.y));
            }
            lst.Sort(new TupleCompare());
            return lst;
        }
    }
}

