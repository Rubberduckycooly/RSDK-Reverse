using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace RSDKv4
{
    public class Palette
    {
        public int COLORS_PER_COLUMN = 0x10;

        public PaletteColor[][] Colors;

        public Palette()
        {
            
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

            Colors = new PaletteColor[palColumns][];
            for (int i = 0; i < palColumns; i++)
            {
                Colors[i] = new PaletteColor[COLORS_PER_COLUMN];
                for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                { Colors[i][j] = new PaletteColor(reader);  Console.WriteLine("R = " + Colors[i][j].R + " G = " + Colors[i][j].G + "B = " + Colors[i][j].B); }
            }
        }

        public void Read(Reader reader)
        {
            int palColumns = ((int)reader.BaseStream.Length / 8) / 6;

            Colors = new PaletteColor[palColumns][];
            for (int i = 0; i < palColumns; i++)
            {
                Colors[i] = new PaletteColor[COLORS_PER_COLUMN];
                for (int j = 0; j < COLORS_PER_COLUMN; ++j)
                { Colors[i][j] = new PaletteColor(reader);}
            }
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
            foreach (PaletteColor[] column in Colors)
                if (column != null)
                    foreach (PaletteColor color in column)
                    { color.Write(writer);}
        }

    }
}
