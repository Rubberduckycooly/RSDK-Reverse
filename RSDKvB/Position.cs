using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
{
    [Serializable]
    public struct Position
    {
        [Serializable]
        public struct Value
        {
            public Value(short high = 0, ushort low = 0)
            {
                Low = low;
                High = high;
            }
            /// <summary>
            /// High value
            /// </summary>
            public short High;
            /// <summary>
            /// Low value
            /// </summary>
            public ushort Low;
        };

        /// <summary>
        /// Xpos values
        /// </summary>
        public Value X;
        /// <summary>
        /// Ypos values
        /// </summary>
        public Value Y;

        public Position(short x = 0, short y = 0)
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("X: ");
            sb.Append(X.High);
            if (0 != X.Low) sb.Append($"[{X.Low}]");
            sb.Append(", Y: ");
            sb.Append(Y.High);
            if (0 != Y.Low) sb.Append($"[{Y.Low}]");
            return sb.ToString();
        }
    }
}
