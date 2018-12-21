using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class Palette
    {
        public const int PALETTE_COLUMNS = 0x10;
        public const int COLORS_PER_COLUMN = 0x10;
        public const int PALETTE_COLORS = 0x100;

        public PaletteColour[][] Colors = new PaletteColour[PALETTE_COLUMNS][];

        internal Palette(Reader reader)
        {
            ushort columns_bitmap = reader.ReadUInt16();
            for (int i = 0; i < PALETTE_COLUMNS; ++i)
            {
                if ((columns_bitmap & (1 << i)) != 0)
                {
                    Colors[i] = new PaletteColour[COLORS_PER_COLUMN];
                    for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                        Colors[i][j] = new PaletteColour(reader);
                }
            }
        }

        internal void Write(Writer writer)
        {
            ushort columns_bitmap = 0;
            for (int i = 0; i < PALETTE_COLUMNS; ++i)
                if (Colors[i] != null)
                    columns_bitmap |= (ushort)(1 << i);
            writer.Write(columns_bitmap);

            foreach (PaletteColour[] column in Colors)
                if (column != null)
                    foreach (PaletteColour color in column)
                        color.Write(writer);
        }
    }
}
