using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public struct Position
    {

        public struct Value 
        {
            public Value(short high = 0, ushort low = 0)
            {
                Low = low;
                High = high;
            }

            public short High;
            public ushort Low;
        };

        public Value X;
        public Value Y;

        public Position(short x=0, short y=0)
        {
            X = new Value(x);
            Y = new Value(y);
        }

        internal Position(Reader reader) : this()
        {
            Read(reader);
        }

        internal void Read(Reader reader)
        {
            X.Low = reader.ReadUInt16();
            X.High = reader.ReadInt16();

            Y.Low = reader.ReadUInt16();
            Y.High = reader.ReadInt16();
        }

        internal void Write(Writer writer)
        {
            writer.Write(X.Low);
            writer.Write(X.High);

            writer.Write(Y.Low);
            writer.Write(Y.High);
        }
    }
}
