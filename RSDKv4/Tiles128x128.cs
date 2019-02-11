using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv4
{
    public class Tiles128x128
    {
        //The file is always 96kb in size

        public List<Tile128> BlockList { get; set; }

        public Tiles128x128(string filepath) : this(new Reader(filepath))
        {

        }

        public Tiles128x128(System.IO.Stream strm) : this(new Reader(strm))
        {

        }

        public Tiles128x128(Reader strm)
        {
            BlockList = new List<Tile128>();
            byte[] mappingEntry = new byte[3];
            Tile128 currentBlock = new Tile128();

            for (int c = 0; c < 512; c++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        strm.Read(mappingEntry, 0, mappingEntry.Length);
                        mappingEntry[0] = (byte)(mappingEntry[0] - (mappingEntry[0] >> 6 << 6));
                        currentBlock.Mapping[y][x].VisualPlane = (byte)(mappingEntry[0] >> 4);
                        mappingEntry[0] = (byte)(mappingEntry[0] - (mappingEntry[0] >> 4 << 4));
                        currentBlock.Mapping[y][x].Direction = (byte)(mappingEntry[0] >> 2);
                        mappingEntry[0] = (byte)(mappingEntry[0] - (mappingEntry[0] >> 2 << 2));
                        currentBlock.Mapping[y][x].Tile16x16 = (ushort)((mappingEntry[0] << 8) + mappingEntry[1]);
                        currentBlock.Mapping[y][x].CollisionFlag0 = (byte)(mappingEntry[2] >> 4);
                        currentBlock.Mapping[y][x].CollisionFlag1 = (byte)(mappingEntry[2] - (mappingEntry[2] >> 4 << 4));
                    }
                }
                BlockList.Add(currentBlock);
                currentBlock = new Tile128();
            }
            strm.Close();
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
            int[] mappingEntry = new int[3];

            mappingEntry = new int[3];

            for (int c = 0; c < 512; c++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        mappingEntry = new int[3];
                        mappingEntry[0] |= (byte)(BlockList[c].Mapping[y][x].Tile16x16 >> 8); //Put the first bit onto buffer[0]
                        mappingEntry[0] = (byte)(mappingEntry[0] + (mappingEntry[0] >> 2 << 2));
                        mappingEntry[0] |= (BlockList[c].Mapping[y][x].Direction) << 2; //Put the Flip of the tile two bits in
                        mappingEntry[0] = (byte)(mappingEntry[0] + (mappingEntry[0] >> 4 << 4));
                        mappingEntry[0] |= (BlockList[c].Mapping[y][x].VisualPlane) << 4; //Put the Layer of the tile four bits in
                        mappingEntry[0] = (byte)(mappingEntry[0] + (mappingEntry[0] >> 6 << 6));

                        mappingEntry[1] = (byte)(BlockList[c].Mapping[y][x].Tile16x16); //Put the rest of the Tile16x16 Value into this buffer

                        mappingEntry[2] = BlockList[c].Mapping[y][x].CollisionFlag1; //Colision Flag 1 is all bytes before bit 5
                        mappingEntry[2] = mappingEntry[2] | BlockList[c].Mapping[y][x].CollisionFlag0 << 4; //Colision Flag 0 is all bytes after bit 4

                        writer.Write((byte)mappingEntry[0]);
                        writer.Write((byte)mappingEntry[1]);
                        writer.Write((byte)mappingEntry[2]);
                    }
                }
            }
            writer.Close();
        }

        public Bitmap RenderChunk(int ChunkID, Bitmap Tiles)
        {
            Bitmap chunk = new Bitmap(128, 128);
            chunk = BlockList[ChunkID].Render(Tiles);
            return chunk;
        }

        public Tile128 Clone(int ChunkID)
        {
            Tile128 Copy = new Tile128();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Copy.Mapping[y][x].VisualPlane = BlockList[ChunkID].Mapping[y][x].VisualPlane;
                    Copy.Mapping[y][x].Direction = BlockList[ChunkID].Mapping[y][x].Direction;
                    Copy.Mapping[y][x].Tile16x16 = BlockList[ChunkID].Mapping[y][x].Tile16x16;
                    Copy.Mapping[y][x].CollisionFlag0 = BlockList[ChunkID].Mapping[y][x].CollisionFlag0;
                    Copy.Mapping[y][x].CollisionFlag1 = BlockList[ChunkID].Mapping[y][x].CollisionFlag1;
                }
            }
            return Copy;
        }

    }

    public class Tile128
    {
        public Tile16[][] Mapping;
        public Tile128()
        {
            Mapping = new Tile16[8][];
            for (int i = 0; i < 8; i++)
            {
                Mapping[i] = new Tile16[8];
            }

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Mapping[y][x] = new Tile16();
                }
            }

        }

        public Bitmap Render(Image tiles)
        {
            Bitmap retval = new Bitmap(128, 128);
            using (Graphics rg = Graphics.FromImage(retval))
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Rectangle destRect = new Rectangle(x * 16, y * 16, 16, 16);
                        Rectangle srcRect = new Rectangle(0, Mapping[y][x].Tile16x16 * 16, 16, 16);
                        using (Bitmap tile = new Bitmap(16, 16))
                        {
                            using (Graphics tg = Graphics.FromImage(tile))
                            {
                                tg.DrawImage(tiles, 0, 0, srcRect, GraphicsUnit.Pixel);
                            }
                            if (Mapping[y][x].Direction == 1)
                            {
                                tile.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            }
                            else if (Mapping[y][x].Direction == 2)
                            {
                                tile.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            }
                            else if (Mapping[y][x].Direction == 3)
                            {
                                tile.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                            }
                            rg.DrawImage(tile, destRect);
                        }
                    }
                }
            }
            return retval;
        }
    }

    public class Tile16
    {
        public byte VisualPlane { get; set; }
        public byte Direction { get; set; }
        public ushort Tile16x16 { get; set; }
        public byte CollisionFlag0 { get; set; }
        public byte CollisionFlag1 { get; set; }
    }
}
