using System;
using SystemColor = System.Drawing.Color;

namespace RSDKv5
{
    [Serializable]
    public class Color
    {
        /// <summary>
        /// Colour Red Value
        /// </summary>
        public byte R = 0x00;
        /// <summary>
        /// Colour Green Value
        /// </summary>
        public byte G = 0x00;
        /// <summary>
        /// Colour Blue Value
        /// </summary>
        public byte B = 0x00;
        /// <summary>
        /// Colour Alpha Value
        /// </summary>
        public byte A = 0xFF;

        public static Color EMPTY = new Color(0, 0, 0, 0);

        public Color(byte R = 0x00, byte G = 0x00, byte B = 0x00, byte A = 0xFF)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        public Color(Reader reader, bool paletteClr = false) : this()
        {
            read(reader, paletteClr);
        }

        public void read(Reader reader, bool paletteClr = false)
        {
            if (paletteClr)
            {
                R = reader.ReadByte();
                G = reader.ReadByte();
                B = reader.ReadByte();
            }
            else
            {
                B = reader.ReadByte();
                G = reader.ReadByte();
                R = reader.ReadByte();
                A = reader.ReadByte();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                Color compareValue = (Color)obj;
                bool isEqual = true;

                if (isEqual && compareValue.R == this.R) isEqual = true;
                else if (!isEqual) isEqual = false;

                if (isEqual && compareValue.G == this.G) isEqual = true;
                else if (!isEqual) isEqual = false;

                if (isEqual && compareValue.B == this.B) isEqual = true;
                else if (!isEqual) isEqual = false;

                if (isEqual && compareValue.A == this.A) isEqual = true;
                else if (!isEqual) isEqual = false;

                return isEqual;
            }
            else return false;
        }

        public SystemColor toSystemColors()
        {
            SystemColor returnColor = SystemColor.FromArgb(R, G, B);
            return returnColor;
        }

        public Color fromSystemColor(SystemColor color)
        {
            Color returnColor = new Color();
            returnColor.R = color.R;
            returnColor.A = color.A;
            returnColor.B = color.B;
            returnColor.G = color.G;
            return returnColor;
        }

        public void write(Writer writer, bool paletteClr = false)
        {
            if (paletteClr)
            {
                writer.Write(R);
                writer.Write(G);
                writer.Write(B);
            }
            else
            {
                writer.Write(B);
                writer.Write(G);
                writer.Write(R);
                writer.Write(A);
            }
        }
    }
}
