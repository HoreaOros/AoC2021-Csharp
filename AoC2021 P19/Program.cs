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
        static string fileName = "inputTest.txt";
        static Queue<Scanner> scanners = new Queue<Scanner>();
        static HashSet<(int x, int y, int z)> allBeacons = new HashSet<(int x, int y, int z)> ();
        static void Main(string[] args)
        {
            ReadData(fileName);
            
            part1();
            Console.WriteLine();
        }

        private static void part1()
        {
            while(scanners.Count > 0)
            {
                Scanner s = scanners.Dequeue ();
                HashSet<(int x, int y, int z)> k = calc_intersect(allBeacons, s);
                if(k != null)
                    allBeacons.UnionWith (k);
                else
                    scanners.Enqueue (s);
                Console.WriteLine(scanners.Count);
            }
            Console.WriteLine(allBeacons.Count);
        }

        private static HashSet<(int x, int y, int z)> calc_intersect(HashSet<(int x, int y, int z)> allBeacons, Scanner s)
        {
            (int x, int y, int z) off;
            foreach(var s2 in s.AllBeaconsRotations)
                foreach(var a in allBeacons)
                    foreach(var b in s2)
                    {
                        off = (a.x - b.x, a.y - b.y, a.z - b.z);
                        HashSet<(int x, int y, int z)> c = new HashSet<(int x, int y, int z)>();
                        foreach (var item in s2)
                            c.Add((item.x + off.x, item.y + off.y, item.z + off.z));

                        HashSet<(int x, int y, int z)> intersection = new HashSet<(int x, int y, int z)>(allBeacons.Intersect (c));
                        if (intersection.Count >= 12)
                            return c;
                    }
            return null;
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
                if (i > 0)
                    scanner.GetRotations();
                else
                    scanner.AllBeaconsRotations.Add(scanner.Beacons);
                scanners.Enqueue(scanner);
                Console.WriteLine(scanner);
            }
            allBeacons = new HashSet<(int x, int y, int z)>(scanners.Dequeue().Beacons);
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
            foreach(var item in beacons)
                lst.Add((item.x, -item.z, item.y));
            return lst;
        }
    }
}

