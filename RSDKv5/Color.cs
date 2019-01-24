using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemColor = System.Drawing.Color;
using System.IO;

namespace RSDKv5
{
    [Serializable]
    public struct Color
    {

        /// <summary>
        /// Colour Red Value
        /// </summary>
        public byte R;
        /// <summary>
        /// Colour Green Value
        /// </summary>
        public byte G;
        /// <summary>
        /// Colour Blue Value
        /// </summary>
        public byte B;
        /// <summary>
        /// Colour Alpha Value
        /// </summary>
        public byte A;

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

        internal SystemColor ToSystemColors()
        {
            SystemColor returnColor = SystemColor.FromArgb(this.R, this.G, this.B);
            return returnColor;
        }

        internal Color FromSystemColor(SystemColor color)
        {
            Color returnColor = new Color();
            returnColor.R = color.R;
            returnColor.A = color.A;
            returnColor.B = color.B;
            returnColor.G = color.G;
            return returnColor;
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
