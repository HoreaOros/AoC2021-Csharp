using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            List<int> nums = new List<int>();
            string line = sr.ReadLine();
            string[] tokens = line.Split(',');
            foreach(string token in tokens)
                nums.Add(int.Parse(token));

            var cards = new List<(int Value, bool Marked)[,]>();  

            while (sr.ReadLine() != null)
            {
                var card = new (int Value, bool Marked)[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    line = sr.ReadLine();
                    tokens = line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0;j < 5; j++)
                        card[i, j] = (int.Parse(tokens[j]), false);
                }
                cards.Add(card);
            }
            sr.Close();

            Console.WriteLine($"Part1: {part1(cards, nums)}");
            Console.WriteLine($"Part2: {part2(cards, nums)}");
            


            
        }

        private static int part2(List<(int Value, bool Marked)[,]> cards, List<int> nums)
        {
            int lasCardToWin = 0, k = 0;
            UnMarkAll(cards);
            bool[] winners = new bool[cards.Count];
            while(winners.Count(c => c == true) != cards.Count - 1)
            {
                Console.WriteLine($"Marking {nums[k]}...");
                mark(cards, nums[k]);
                checkWinners(cards, winners);
                k++;
            }
            while (winners[lasCardToWin] == true)
                lasCardToWin++;

            Console.WriteLine($"Last card to win: {lasCardToWin + 1}...");

            while (!isWinner(cards[lasCardToWin]))
            {
                Console.WriteLine($"Marking {nums[k]}...");
                mark(cards[lasCardToWin], nums[k]);
                k++;
            }

            int sum = SumUnmarked(cards[lasCardToWin]);
            return  sum * nums[k - 1];
        }

        private static bool isWinner((int Value, bool Marked)[,] card)
        {
            for (int i = 0; i < 5; i++)
            {
                bool winning = true;
                for (int j = 0; winning && j < 5; j++)
                    winning = winning && card[i, j].Marked;
                if (winning)
                    return true;
            }


            for (int j = 0; j < 5; j++)
            {
                bool winning = true;
                for (int i = 0; winning && i < 5; i++)
                    winning = winning && card[i, j].Marked;
                if (winning)
                    return true;
            }
            return false;
        }

        private static void checkWinners(List<(int Value, bool Marked)[,]> cards, bool[] winners)
        {
            for (int k = 0; k < cards.Count; k++)
            {
                if (winners[k] == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        bool winning = true;
                        for (int j = 0; winning && j < 5; j++)
                            winning = winning && cards[k][i, j].Marked;
                        if (winning)
                            winners[k] = true;
                    }


                    for (int j = 0; j < 5; j++)
                    {
                        bool winning = true;
                        for (int i = 0; winning && i < 5; i++)
                            winning = winning && cards[k][i, j].Marked;
                        if (winning)
                            winners[k] = true;
                    }
                }
            }
        }

        private static void UnMarkAll(List<(int Value, bool Marked)[,]> cards)
        {
            foreach (var c in cards)
            {
                for(int i = 0; i < 5; i++)
                    for(int j = 0; j < 5; j++)
                        c[i, j].Marked = false;
            }
        }

        private static int SumUnmarked((int Value, bool Marked)[,] card)
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (card[i, j].Marked == false)
                        sum += card[i, j].Value;
            return sum;     
        }

        private static int part1(List<(int Value, bool Marked)[,]> cards, List<int> nums)
        {
            int winningCard;
            int k = 0;
            while ((winningCard = win(cards)) == -1)
            {
                mark(cards, nums[k]);
                k++;
            }

            
            return SumUnmarked(cards[winningCard]) * nums[k - 1];
        }

        private static int win(List<(int Value, bool Marked)[,]> cards)
        {
            for (int k = 0; k < cards.Count; k++)
            {
                for(int i = 0; i < 5; i++)
                {
                    bool winning = true;
                    for (int j = 0; j < 5; j++)
                        winning = winning && cards[k][i, j].Marked;
                    if (winning)
                        return k;
                }


                for (int j = 0; j < 5; j++)
                {
                    bool winning = true;
                    for (int i = 0; i < 5; i++)
                        winning = winning && cards[k][i, j].Marked;
                    if (winning)
                        return k;
                }
            }
            return -1;
        }

        private static void mark(List<(int Value, bool Marked)[,]> cards, int v)
        {
            for (int k = 0; k < cards.Count; k++)
                mark(cards[k], v);
        }
        private static void mark((int Value, bool Marked)[,] card, int v)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (card[i, j].Value == v)
                        card[i, j].Marked = true;
        }
    }
}
