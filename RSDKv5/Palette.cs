namespace RSDKv5
{
    public class Palette
    {
        /// <summary>
        /// how many columns in the palette
        /// </summary>
        public const int PALETTE_ROWS = 0x10;
        /// <summary>
        /// how many colours per column
        /// </summary>
        public const int COLORS_PER_ROW = 0x10;

        /// <summary>
        /// our array of colours in the palette
        /// </summary>
        public Color[][] colors = new Color[PALETTE_ROWS][];

        bool[] activeRows = new bool[PALETTE_ROWS];

        public Palette(int palColumns = 16)
        {
            colors = new Color[palColumns][];
            activeRows = new bool[palColumns];

            for (int c = 0; c < palColumns; c++)
            {
                activeRows[c] = false;
                colors[c] = new Color[COLORS_PER_ROW];
                for (int r = 0; r < COLORS_PER_ROW; ++r)
                    colors[c][r] = new Color(0xFF, 0x00, 0xFF); 
            }
        }

        public Palette(Reader reader) : this()
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            ushort columns_bitmap = reader.ReadUInt16();
            for (int r = 0; r < PALETTE_ROWS; ++r)
            {
                activeRows[r] = false;
                if ((columns_bitmap & (1 << r)) != 0)
                {
                    activeRows[r] = true;
                    for (int c = 0; c < COLORS_PER_ROW; ++c)
                        colors[r][c] = new Color(reader, true);
                }
            }
        }

        public void write(Writer writer)
        {
            ushort columns_bitmap = 0;
            for (int r = 0; r < PALETTE_ROWS; ++r)
                if (activeRows[r])
                    columns_bitmap |= (ushort)(1 << r);
            writer.Write(columns_bitmap);

            int row = 0;
            foreach (Color[] column in colors)
            {
                if (activeRows[row])
                {
                    foreach (Color color in column)
                        color.write(writer, true);
                }
                ++row;
            }
        }
    }
}
