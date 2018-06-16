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

        public PaletteColor[][] Colors = new PaletteColor[PALETTE_COLUMNS][];

        internal Palette(Reader reader)
        {
            ushort columns_bitmap = reader.ReadUInt16();
            for (int i = 0; i < PALETTE_COLUMNS; ++i)
            {
                if ((columns_bitmap & (1 << i)) != 0)
                {
                    Colors[i] = new PaletteColor[COLORS_PER_COLUMN];
                    for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                        Colors[i][j] = new PaletteColor(reader);
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

            foreach (PaletteColor[] column in Colors)
                if (column != null)
                    foreach (PaletteColor color in column)
                        color.Write(writer);
        }
    }
}
