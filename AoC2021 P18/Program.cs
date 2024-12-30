using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2021_P18
{
    internal class Program
    {
        static string fileName = "input.txt";
        static List<string> lines = new List<string>();

        static void Main(string[] args)
        {
            ReadData(fileName);

            part1();
            part2();
        }

        private static void part2()
        {
            HashSet<int> sums = new HashSet<int>();

            for(int i = 0; i < lines.Count; i++)
                for(int j = 0; j < lines.Count; j++)
                    if(i != j)
                    {
                        string sum = Reduce("[" + lines[i] + "," + lines[j] + "]");
                        Number sfn = GetSnailFishNumber(sum);
                        int s = sfn.Mangitude();
                        sums.Add(s);
                    }


            Console.WriteLine(sums.Max());
        }

        private static void part1()
        {
            string sum = lines[0];
            for(int i = 1; i < lines.Count; i++)
            {
                sum = "[" + sum + "," + lines[i] + "]";
                sum = Reduce(sum);
            }
            Console.WriteLine(sum);
            


            Number sfn = GetSnailFishNumber(sum);
            Console.WriteLine(sfn.Mangitude());
        }

        private static Number GetSnailFishNumber(string str)
        {
            if (str.Length == 1)
                return new RegularNumber() { Value = int.Parse(str) };

            int brackets = 0;
            int split = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == '[')
                    brackets++;
                else if (str[i] == ']')
                    brackets--;
                else if (str[i] == ',' &&  brackets == 1)
                {
                    split = i; break;
                }
            }
            string left = str.Substring(1, split - 1);
            string right = str.Substring(split + 1, str.Length - split - 2);


            return new SnailFishNumber() { Left = GetSnailFishNumber(left), Right = GetSnailFishNumber(right) };
        }

        private static string Reduce(string sum)
        {
            while(CanExplode(sum) || CanSplit(sum))
            {
                if (CanExplode(sum))
                    sum = Explode(sum);
                else
                    sum = Split(sum);
            }
            return sum;
        }

        private static string Split(string num)
        {
            Regex r = new Regex(@"\d{2,}");
            Match m = r.Match(num);
            int pos = m.Index;
            int len = m.Length;
            int n = int.Parse(num.Substring(pos, len));
            int left = n / 2;
            int right = n - left;
            string ins = "[" + left.ToString() + "," + right.ToString() +"]";

            num = num.Remove(pos, len);
            num = num.Insert(pos, ins);

            return num;
        }

        private static string Explode(string sum)
        {
            string result = "";
            Regex r = new Regex(@"\[\d+,\d+\]");
            MatchCollection matches = r.Matches(sum);
            bool found = false;
            int idx = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                int brackets = 0;
                for (int k = 0; k < matches[i].Index; k++)
                    if (sum[k] == '[')
                        brackets++;
                    else if (sum[k] == ']')
                        brackets--;
                if (brackets == 4)
                {
                    idx = i;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                int pos = matches[idx].Index;
                int len = matches[idx].Length;
                string explodingNumber = sum.Substring(pos, len);
                Regex r2 = new Regex(@"\d+");
                matches = r2.Matches(explodingNumber);
                int left = int.Parse(matches[0].Value);
                int right = int.Parse(matches[1].Value);

                // replace exploding number by 0;
                result = sum.Remove(pos, len);
                result = result.Insert(pos, "0");
                // add left to first regular number left if any
                string leftPart = result.Substring(0, pos);
                string rightPart = result.Substring(pos);

                // add right to first regular number right if any
                matches = r2.Matches(leftPart);
                if(matches.Count > 0)
                {
                    pos = matches[matches.Count - 1].Index;
                    len = matches[matches.Count - 1].Length;
                    leftPart = ReplaceAdd(leftPart, pos, len, left);
                }
                matches = r2.Matches(rightPart);
                if (matches.Count > 1)
                {
                    pos = matches[1].Index;
                    len = matches[1].Length;
                    rightPart = ReplaceAdd(rightPart, pos, len, right);
                }
                result = leftPart + rightPart;

            }
            else
                throw new ApplicationException("Cannot Explode");
            return result;
        }

        private static string ReplaceAdd(string str, int pos, int len, int value)
        {
            int nr = int.Parse(str.Substring(pos, len));
            string newNum = (nr + value).ToString();

            return str.Substring(0, pos) + newNum.ToString() + str.Substring(pos +len);
        }

        private static bool CanSplit(string line)
        {
            Regex r = new Regex(@"\d{2,}");
            Match m = r.Match(line);
            return m.Success;
        }

        private static bool CanExplode(string line)
        {
            Regex r = new Regex(@"\[\d+,\d+\]");
            MatchCollection matches = r.Matches(line);
            bool found = false;
            for(int i = 0; i < matches.Count; i++)
            {
                int brackets = 0;
                for (int k = 0; k < matches[i].Index; k++)
                    if (line[k] == '[')
                        brackets++;
                    else if (line[k] == ']')
                        brackets--;
                if (brackets == 4)
                {
                    found = true;
                    break;
                }
            }
            return found; 
        }

        private static void ReadData(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string line;
            while ((line = sr.ReadLine()) != null)
                lines.Add(line);
        }
    }

    internal abstract class Number
    {
        public abstract int Mangitude();
    }
    internal class RegularNumber: Number
    {
        public int Value { get; set; }

        public override int Mangitude()
        {
            return Value;
        }
    }
    internal class SnailFishNumber : Number
    {
        public Number Left { get; set; }
        public Number Right { get; set; }
        public override int Mangitude()
        {
            return 3 * Left.Mangitude() + 2 * Right.Mangitude();
        }
    }
}