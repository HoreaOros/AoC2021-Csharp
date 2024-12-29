using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P16
{
    internal class Program
    {
        static string filename = "input.txt";
        static string data;
        static Dictionary<char, string> map = new Dictionary<char, string>()
        {
            ['0'] = "0000",
            ['1'] = "0001",
            ['2'] = "0010",
            ['3'] = "0011",
            ['4'] = "0100",
            ['5'] = "0101",
            ['6'] = "0110",
            ['7'] = "0111",
            ['8'] = "1000",
            ['9'] = "1001",
            ['A'] = "1010",
            ['B'] = "1011",
            ['C'] = "1100",
            ['D'] = "1101",
            ['E'] = "1110",
            ['F'] = "1111",

        };
        static void Main(string[] args)
        {
            ReadData();

            solve();
        

        }

        

        private static void solve()
        {
            int i = 0;
            (Packet outerPacket, int r) = ProcessPacket(i);
            Console.WriteLine(outerPacket.VersionSum());
            Console.WriteLine(outerPacket.PacketValue());
        }

        private static (Packet, int) ProcessPacket(int i)
        {
            int version = Convert.ToInt32(data.Substring(i, 3), 2);
            int type = Convert.ToInt32(data.Substring(i + 3, 3), 2);

            i += 6;
            if(type == 4) // literal
            {
                StringBuilder sb = new StringBuilder();
                while (data[i] == '1')
                {
                    sb.Append(data.Substring(i + 1, 4));
                    i += 5;
                }
                sb.Append(data.Substring(i + 1, 4));
                i += 5;
                long value = Convert.ToInt64(sb.ToString(), 2);
                Packet packet= new Literal() { Version = version, TypeID = type, Value = value };
                return (packet, i);
            }
            else // operator
            {
                char lengthTypeID = data[i];
                int totalLength = -1;
                int numberSubPackets = -1;
                i++;
                if(lengthTypeID == '0') 
                {
                    totalLength = Convert.ToInt32(data.Substring(i, 15), 2);
                    i += 15;
                }
                else
                {
                    numberSubPackets = Convert.ToInt32(data.Substring(i, 11), 2);
                    i += 11;
                }
                Operator packet = new Operator() { Version = version, TypeID = type, Packets = new List<Packet>() };

                if(numberSubPackets > 0)
                    while(numberSubPackets > 0)
                    {
                        (Packet p, int newi) = ProcessPacket(i);
                        packet.Packets.Add(p);
                        numberSubPackets--;
                        i = newi;
                    }
                else if(totalLength > 0)
                {
                    while(totalLength > 0)
                    {
                        (Packet p, int newi) = ProcessPacket(i);
                        packet.Packets.Add(p);
                        totalLength -= (newi - i);
                        i = newi;
                    }
                }

                return (packet, i);
            }
        }

        private static void ReadData()
        {
            StreamReader sr = new StreamReader(filename);
            string line = sr.ReadLine();
            StringBuilder sb = new StringBuilder();
            foreach (char c in line)
                sb.Append(map[c]);
            
            data = sb.ToString();
        }
    }
}
