using SystemColor = System.Drawing.Color;

namespace RSDKv1
{
    public class Palette
    {
        public class Color
        {
            /// <summary>
            /// Color Red Value
            /// </summary>
            public byte r;

            /// <summary>
            /// Color Green Value
            /// </summary>
            public byte g;

            /// <summary>
            /// Color Blue Value
            /// </summary>
            public byte b;

            public Color(byte r = 0, byte g = 0, byte b = 0)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public Color(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                r = reader.ReadByte();
                g = reader.ReadByte();
                b = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(r);
                writer.Write(g);
                writer.Write(b);
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
                sysColor.b = color.B;
                sysColor.g = color.G;
                return sysColor;
            }
        }

        /// <summary>
        /// an array of the colors
        /// </summary>
        public Color[][] colors;

        public Palette(int rows = 2)
        {
            int palRows = rows;

            colors = new Color[palRows][];
            for (int r = 0; r < palRows; r++)
            {
                colors[r] = new Color[16];
                for (int c = 0; c < 16; ++c)
                    colors[r][c] = new Color();
            }
        }

        public Palette(Reader r)
        {
            Read(r, 2);
        }

        public Palette(Reader r, int rows = 2)
        {
            Read(r, rows);
        }

        public void Read(Reader reader, int rows = 2)
        {
            int palRows = rows > 0 ? rows : (((int)reader.BaseStream.Length / 8) / 6);

            colors = new Color[palRows][];
            for (int r = 0; r < palRows; r++)
            {
                colors[r] = new Color[16];
                for (int c = 0; c < 16; ++c)
                    colors[r][c] = new Color(reader);
            }
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            foreach (Color[] row in colors)
            {
                if (row != null)
                {
                    foreach (Color color in row)
                        color.Write(writer);
                }
            }
        }

    }
}
