namespace RSDKv5
{
    public class Palette
    {
        /// <summary>
        /// our array of colors in the palette
        /// </summary>
        public Color[][] colors = new Color[16][];

        public bool[] activeRows = new bool[16];

        public Palette(int palColumns = 16)
        {
            colors = new Color[palColumns][];
            activeRows = new bool[palColumns];

            for (int c = 0; c < palColumns; c++)
            {
                activeRows[c] = false;
                colors[c] = new Color[16];
                for (int r = 0; r < 16; ++r)
                    colors[c][r] = new Color(0xFF, 0x00, 0xFF); 
            }
        }

        public Palette(Reader reader) : this()
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            ushort activeRowMasks = reader.ReadUInt16();

            for (int r = 0; r < 16; ++r)
            {
                activeRows[r] = false;
                if ((activeRowMasks & (1 << r)) != 0)
                {
                    activeRows[r] = true;
                    for (int c = 0; c < 16; ++c)
                        colors[r][c] = new Color(reader, true);
                }
            }
        }

        public void Write(Writer writer)
        {
            ushort activeRowMasks = 0;
            for (int r = 0; r < 16; ++r)
            {
                if (activeRows[r])
                    activeRowMasks |= (ushort)(1 << r);
            }

            writer.Write(activeRowMasks);

            int rowID = 0;
            foreach (Color[] row in colors)
            {
                if (activeRows[rowID])
                {
                    foreach (Color color in row)
                        color.Write(writer, true);
                }
                ++rowID;
            }
        }
    }
}
