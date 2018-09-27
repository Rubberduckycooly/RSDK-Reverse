using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    [Serializable]
    public struct Color
    {

        public byte R, G, B, A;

        public static Color EMPTY = new Color(0, 0, 0, 0);

        public Color(byte R=0, byte G=0, byte B=0, byte A=255)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        internal Color(Reader reader) : this()
        {
            Read(reader);
        }

        internal void Read(Reader reader)
        {
            B = reader.ReadByte();
            G = reader.ReadByte();
            R = reader.ReadByte();
            A = reader.ReadByte();
        }

        internal void Write(Writer writer)
        {
            writer.Write(B);
            writer.Write(G);
            writer.Write(R);
            writer.Write(A);
        }
    }
}
