using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace RSDKvB
{
    public class Palette
    {
        /// <summary>
        /// how many colours for each row (always 16)
        /// </summary>
        public int COLORS_PER_COLUMN = 0x10;

        /// <summary>
        /// an array of the colours
        /// </summary>
        public PaletteColour[][] Colors;

        public Palette(int pc = 2)
        {
            int palColumns = pc;

            Colors = new PaletteColour[palColumns][];
            for (int i = 0; i < palColumns; i++)
            {
                Colors[i] = new PaletteColour[COLORS_PER_COLUMN];
                for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                { Colors[i][j] = new PaletteColour(); }
            }
        }

        public Palette(Reader r)
        {
            Read(r);
        }

        public Palette(Reader r, int palcols)
        {
            Read(r, palcols);
        }

        public void Read(Reader reader, int Columns)
        {
            int palColumns = Columns;

            Colors = new PaletteColour[palColumns][];
            for (int i = 0; i < palColumns; i++)
            {
                Colors[i] = new PaletteColour[COLORS_PER_COLUMN];
                for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                {
                    Colors[i][j] = new PaletteColour(reader);
                }
            }
        }

        public void Read(Reader reader)
        {
            int palColumns = ((int)reader.BaseStream.Length / 8) / 6;

            Colors = new PaletteColour[palColumns][];
            for (int i = 0; i < palColumns; i++)
            {
                Colors[i] = new PaletteColour[COLORS_PER_COLUMN];
                for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                { Colors[i][j] = new PaletteColour(reader);}
            }
        }

        public void ReadSuperPalette(Reader reader, int offset)
        {

        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        internal void Write(Writer writer)
        {
            foreach (PaletteColour[] column in Colors)
                if (column != null)
                    foreach (PaletteColour color in column)
                    { color.Write(writer);}
        }

    }
}
