namespace RSDKv2
{
    public class Palette
    {
        public class Color
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

            public Color(byte R = 0, byte G = 0, byte B = 0)
            {
                this.R = R;
                this.G = G;
                this.B = B;
            }

            public Color(Reader reader)
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                R = reader.ReadByte();
                G = reader.ReadByte();
                B = reader.ReadByte();
            }

            public void write(Writer writer)
            {
                writer.Write(R);
                writer.Write(G);
                writer.Write(B);
            }
        }

        /// <summary>
        /// how many colours for each row (always 16)
        /// </summary>
        public int COLORS_PER_ROW = 0x10;

        /// <summary>
        /// an array of the colours
        /// </summary>
        public Palette.Color[][] colors;

        public Palette(int rows = 2)
        {
            int palRows = rows;

            colors = new Palette.Color[palRows][];
            for (int r = 0; r < palRows; r++)
            {
                colors[r] = new Palette.Color[COLORS_PER_ROW];
                for (int c = 0; c < COLORS_PER_ROW; ++c)
                    colors[r][c] = new Palette.Color();
            }
        }

        public Palette(Reader r)
        {
            read(r, 2);
        }

        public Palette(Reader r, int rows = 2)
        {
            read(r, rows);
        }

        public void read(Reader reader, int rows = 2)
        {
            int palRows = rows > 0 ? rows : (((int)reader.BaseStream.Length / 8) / 6);

            colors = new Palette.Color[palRows][];
            for (int r = 0; r < palRows; r++)
            {
                colors[r] = new Palette.Color[COLORS_PER_ROW];
                for (int c = 0; c < COLORS_PER_ROW; ++c)
                    colors[r][c] = new Palette.Color(reader);
            }
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            foreach (Palette.Color[] row in colors)
            {
                if (row != null)
                {
                    foreach (Palette.Color color in row)
                        color.write(writer);
                }
            }
        }

    }
}
