using System;
using SystemColor = System.Drawing.Color;

namespace RSDKv5
{
    [Serializable]
    public class Color
    {
        /// <summary>
        /// Color Red Value
        /// </summary>
        public byte r = 0x00;

        /// <summary>
        /// Color Green Value
        /// </summary>
        public byte g = 0x00;

        /// <summary>
        /// Color Blue Value
        /// </summary>
        public byte b = 0x00;
        
        /// <summary>
        /// Color Alpha Value
        /// </summary>
        public byte a = 0xFF;

        public static Color EMPTY = new Color(0, 0, 0, 0);

        public Color(byte R = 0x00, byte G = 0x00, byte B = 0x00, byte A = 0xFF)
        {
            this.r = R;
            this.g = G;
            this.b = B;
            this.a = A;
        }

        public Color(Reader reader, bool paletteClr = false) : this()
        {
            Read(reader, paletteClr);
        }

        public void Read(Reader reader, bool paletteClr = false)
        {
            if (paletteClr)
            {
                r = reader.ReadByte();
                g = reader.ReadByte();
                b = reader.ReadByte();
            }
            else
            {
                b = reader.ReadByte();
                g = reader.ReadByte();
                r = reader.ReadByte();
                a = reader.ReadByte();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                Color compareValue = (Color)obj;
                bool isEqual = true;

                if (isEqual && compareValue.r == this.r) isEqual = true;
                else if (!isEqual) isEqual = false;

                if (isEqual && compareValue.g == this.g) isEqual = true;
                else if (!isEqual) isEqual = false;

                if (isEqual && compareValue.b == this.b) isEqual = true;
                else if (!isEqual) isEqual = false;

                if (isEqual && compareValue.a == this.a) isEqual = true;
                else if (!isEqual) isEqual = false;

                return isEqual;
            }

            return false;
        }

        public SystemColor ToSystemColor()
        {
            return SystemColor.FromArgb(r, g, b);
        }

        public Color FromSystemColor(SystemColor color)
        {
            Color sysColor = new Color();
            sysColor.r = color.R;
            sysColor.a = color.A;
            sysColor.b = color.B;
            sysColor.g = color.G;
            return sysColor;
        }

        public void Write(Writer writer, bool paletteClr = false)
        {
            if (paletteClr)
            {
                writer.Write(r);
                writer.Write(g);
                writer.Write(b);
            }
            else
            {
                writer.Write(b);
                writer.Write(g);
                writer.Write(r);
                writer.Write(a);
            }
        }
    }
}
