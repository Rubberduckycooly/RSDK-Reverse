using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class SceneLayer
    {
        byte IgnoredByte;
        private string _name;

        public byte IsScrollingVertical;
        public byte UnknownByte2;

        private ushort _width;
        private ushort _height;

        public ushort UnknownWord1;
        public ushort UnknownWord2;

        public class ScrollInfo
        {
            ushort UnknownWord1;
            ushort UnknownWord2;
            byte UnknownByte1;
            byte UnknownByte2;

            public ScrollInfo(ushort word1 = 0x100, ushort word2 = 0, byte byte1 = 0, byte byte2 = 0)
            {
                this.UnknownWord1 = word1;
                this.UnknownWord2 = word2;

                this.UnknownByte1 = byte1;
                this.UnknownByte2 = byte2;
            }

            internal ScrollInfo(Reader reader)
            {
                UnknownWord1 = reader.ReadUInt16();
                UnknownWord2 = reader.ReadUInt16();

                UnknownByte1 = reader.ReadByte();
                UnknownByte2 = reader.ReadByte();
            }

            internal void Write(Writer writer)
            {
                writer.Write(UnknownWord1);
                writer.Write(UnknownWord2);

                writer.Write(UnknownByte1);
                writer.Write(UnknownByte2);
            }
        }

        public List<ScrollInfo> ScrollingInfo = new List<ScrollInfo>();

        public byte[] ScrollIndexes;

        public ushort[][] Tiles;

        public string Name { get => _name; set => _name = value; }
        public ushort Width { get => _width; set => _width = value; }
        public ushort Height { get => _height; set => _height = value; }

        public SceneLayer(string name, ushort width, ushort height)
        {
            Name = name;
            Width = width;
            Height = height;

            ScrollingInfo.Add(new ScrollInfo());
            // Per pixel (of height or of width, depends if it scrolls horizontal or veritcal)
            ScrollIndexes = new byte[Height * 16];
            Tiles = new ushort[Height][];
            for (int i = 0; i < Height; ++i)
            {
                Tiles[i] = new ushort[Width];
                for (int j = 0; j < Width; ++j)
                { Tiles[i][j] = 0xffff; }
            }
        }

        internal SceneLayer(Reader reader)
        {
            IgnoredByte = reader.ReadByte();

            Name = reader.ReadRSDKString();

            IsScrollingVertical = reader.ReadByte();
            UnknownByte2 = reader.ReadByte();

            Width = reader.ReadUInt16();
            Height = reader.ReadUInt16();

            UnknownWord1 = reader.ReadUInt16();
            UnknownWord2 = reader.ReadUInt16();

            ushort scrolling_info_count = reader.ReadUInt16();
            for (int i = 0; i < scrolling_info_count; ++i)
                ScrollingInfo.Add(new ScrollInfo(reader));

            ScrollIndexes = reader.ReadCompressed();

            Tiles = new ushort[Height][];
            using (Reader creader = reader.GetCompressedStream())
            {
                for (int i = 0; i < Height; ++i)
                {
                    Tiles[i] = new ushort[Width];
                    for (int j = 0; j < Width; ++j)
                    { Tiles[i][j] = creader.ReadUInt16(); Console.WriteLine("Hex: {0:X}", Tiles[i][j]); }
                }
            }
        }

        internal void Write(Writer writer)
        {
            writer.Write(IgnoredByte);

            writer.WriteRSDKString(Name);

            writer.Write(IsScrollingVertical);
            writer.Write(UnknownByte2);

            writer.Write(Width);
            writer.Write(Height);

            writer.Write(UnknownWord1);
            writer.Write(UnknownWord2);

            writer.Write((ushort)ScrollingInfo.Count);
            foreach (ScrollInfo info in ScrollingInfo)
                info.Write(writer);

            writer.WriteCompressed(ScrollIndexes);

            using (MemoryStream cmem = new MemoryStream())
            using (Writer cwriter = new Writer(cmem))
            {
                    for (int i = 0; i < Height; ++i)
                        for (int j = 0; j < Width; ++j)
                        { cwriter.Write(Tiles[i][j]); Console.WriteLine(Tiles[i][j]); }
                cwriter.Close();
                writer.WriteCompressed(cmem.ToArray());
            }
        }

        /// <summary>
        /// Resizes a layer.
        /// </summary>
        /// <param name="width">The new Width</param>
        /// <param name="height">The new Height</param>
        public void Resize(ushort width, ushort height)
        {
            // first take a backup of the current dimensions
            // then update the internal dimensions
            ushort oldWidth = Width;
            ushort oldHeight = Height;
            Width = width;
            Height = height;

            // resize the various scrolling and tile arrays
            Array.Resize(ref ScrollIndexes, Height * 16);
            Array.Resize(ref Tiles, Height);

            // fill the extended tile arrays with "empty" values

            // if we're actaully getting shorter, do nothing!
            for (ushort i = oldHeight; i < Height; i++)
            {
                // first create arrays child arrays to the old width
                // a little inefficient, but at least they'll all be equal sized
                Tiles[i] = new ushort[oldWidth];
                for (int j = 0; j < oldWidth; ++j)
                    Tiles[i][j] = 0xffff; // fill the new ones with blanks
            }

            for (ushort i = 0; i < Height; i++)
            {
                // now resize all child arrays to the new width
                Array.Resize(ref Tiles[i], Width);
                for (ushort j = oldWidth; j < Width; j++)
                    Tiles[i][j] = 0xffff; // and fill with blanks if wider
            }
        }
    }
}
