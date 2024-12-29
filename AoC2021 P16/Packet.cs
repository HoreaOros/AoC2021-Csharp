using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021_P16
{
    internal abstract class Packet
    {
        public int Version { get; set; }
        public int TypeID { get; set; } // 4 literal, <> 4 operator
        public abstract int VersionSum();
        public abstract long PacketValue();

    }
    internal class Literal: Packet
    {
        public long Value {  get; set; }

        public override long PacketValue()
        {
            return Value;
        }

        public override int VersionSum()
        {
            return Version;
        }
    }
    internal class Operator: Packet
    {
        public List<Packet> Packets { get; set; }

        public override long PacketValue()
        {
            long result  = 0;
            switch(TypeID)
            {
                case 0:
                    foreach (Packet p in Packets)
                        result += p.PacketValue();
                    break;
                case 1:
                    result = 1;
                    foreach(Packet p in Packets)
                        result *= p.PacketValue();
                    break;
                case 2:
                    result = Packets.Min(x => x.PacketValue());
                    break;
                case 3:
                    result = Packets.Max(x => x.PacketValue());
                    break;
                case 5:
                    result = (Packets[0].PacketValue() > Packets[1].PacketValue()) ? 1 : 0;
                    break;
                case 6:
                    result = (Packets[0].PacketValue() < Packets[1].PacketValue()) ? 1 : 0;
                    break;
                case 7:
                    result = (Packets[0].PacketValue() == Packets[1].PacketValue()) ? 1 : 0;
                    break;
            }

            return result;
        }

        public override int VersionSum()
        {
            return Version + Packets.Sum(x => x.VersionSum());
        }
    }
}
