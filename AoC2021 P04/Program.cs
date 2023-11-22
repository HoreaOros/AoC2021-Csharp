using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

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
                    tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0;j < 5; j++)
                        card[i, j] = (int.Parse(tokens[j]), false);
                }
                cards.Add(card);
            }
            sr.Close();
            int winningCard;
            int k = 0;
            while((winningCard = win(cards)) == -1)
            {
                mark(cards, nums[k]);
                k++;
            }

            int sum = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (cards[winningCard][i, j].Marked == false)
                        sum += cards[winningCard][i, j].Value;


            Console.WriteLine($"Part 1: {sum * nums[k - 1]}");
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
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (cards[k][i, j].Value == v)
                            cards[k][i, j].Marked = true;
        }
    }
}
