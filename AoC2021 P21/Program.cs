using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2021_P21
{
    internal class Program
    {
        static string fileName = "input.txt";
        static int p1, p2;
        static Dictionary<(int s1, int s2, int p1, int p2, int cp), (long w1, long w2)> cache = new Dictionary<(int s1, int s2, int p1, int p2, int cp), (long w1, long w2)>();
        static void Main(string[] args)
        {
            ReadData(fileName);
            part1(p1, p2);
            long w1 = 0, w2 = 0;

            (w1, w2) = solve(0, 0, p1 - 1, p2 - 1, 1);
            Console.WriteLine(Math.Max(w1, w2));
        }

        private static (long w1, long w2) solve(int s1, int s2, int p1, int p2, int currentPlayer)
        {
            if (cache.ContainsKey((s1, s2, p1, p2, currentPlayer)))
                return cache[(s1, s2, p1, p2, currentPlayer)];
            if(s1 >= 21)
            {
                cache[(s1, s2, p1, p2, currentPlayer)] = (1, 0);
                return (1, 0);
            }
            else if(s2 >= 21)
            {
                cache[(s1, s2, p1, p2, currentPlayer)] = (0, 1);
                return (0, 1);
            }


            long allW1 = 0, allW2 = 0;
            if(currentPlayer == 1)
            {
                for(int d1 = 1; d1 <= 3; d1++)
                    for(int d2 = 1; d2 <= 3; d2++)
                        for(int d3 = 1; d3 <= 3; d3++)
                        {
                            int newp1 = (p1 + d1 + d2 + d3) % 10;
                            int news1 = s1 + (newp1 + 1);
                            (long w1, long w2) = solve(news1, s2, newp1, p2, 2);
                            allW1 += w1;
                            allW2 += w2;
                            cache[(news1, s2, newp1, p2, 2)] = (w1, w2);
                        }
            }
            else
            {
                for (int d1 = 1; d1 <= 3; d1++)
                    for (int d2 = 1; d2 <= 3; d2++)
                        for (int d3 = 1; d3 <= 3; d3++)
                        {
                            int newp2 = (p2 + d1 + d2 + d3) % 10;
                            int news2 = s2 + (newp2 + 1);
                            (long w1, long w2) = solve(s1, news2, p1, newp2, 1);
                            allW1 += w1;
                            allW2 += w2;
                            cache[(s1, news2, p1, newp2, 1)] = (w1, w2);
                        }
            }
            cache[(s1, s2, p1, p2, currentPlayer)] = (allW1, allW2);
            return (allW1, allW2);
        }

       
        private static void part1(int p1, int p2)
        {
            long s1 = 0, s2 = 0;
            int d = 0, d1 = 0, d2, d3;
            long diceRolls = 0;
            // make positions 0 based;
            p1--;
            p2--; 
            while(true)
            {
                // Player 1
                d1 = NextDie(d);
                d2 = NextDie(d1);
                d3 = NextDie(d2);
                diceRolls += 3;
                p1 = (p1 + (d1 + d2 + d3)) % 10;
                s1 += (p1 + 1);

                //Console.WriteLine($"Player 1 rolls {d1} + {d2} + {d3} and moves to space {p1+1} for a total score of {s1}.");

                if (s1 >= 1000)
                    break;
                
                d = d3;


                // Player 2
                d1 = NextDie(d);
                d2 = NextDie(d1);
                d3 = NextDie(d2);
                diceRolls += 3;
                p2 = (p2 + (d1 + d2 + d3)) % 10;
                s2 += (p2 + 1);

                //Console.WriteLine($"Player 2 rolls {d1} + {d2} + {d3} and moves to space {p2 + 1} for a total score of {s2}.");

                if (s2 >= 1000)
                    break;

                d = d3;
            }
            Console.WriteLine(diceRolls * ((s1 >= 1000)?s2:s1));
        }

        private static int NextDie(int d)
        {
            d++;
            if (d == 101)
                d = 1;
            return d;
        }

        private static void ReadData(string fileName)
        {
            string text = File.ReadAllText(fileName);
            Regex r = new Regex(@"\d+");
            MatchCollection m = r.Matches(text);
            p1 = int.Parse(m[1].Value);
            p2 = int.Parse(m[3].Value);
        }
    }
}


// Test values:
// 444356092776315
// 341960390180808


// Mine:
// 14598271321389
// 37107702442602