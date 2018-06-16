using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv3
{
    public class PaletteColor
    {

        public byte R, G, B;

        public PaletteColor(byte R = 0, byte G = 0, byte B = 0)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        internal PaletteColor(BinaryReader reader)
        {
            this.Read(reader);
        }

        internal void Read(BinaryReader reader)
        {
            R = reader.ReadByte();
            G = reader.ReadByte();
            B = reader.ReadByte();
        }

        internal void Write(BinaryWriter writer)
        {
            writer.Write(R);
            writer.Write(G);
            writer.Write(B);
        }
    }
}
