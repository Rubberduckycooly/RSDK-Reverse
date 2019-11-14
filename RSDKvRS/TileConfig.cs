using System.Drawing;

namespace RSDKvRS
{
    public class Tileconfig
    {
        /// <summary>
        /// 1024, one for each tile
        /// </summary>
        public const int TILES_COUNT = 0x400; //1024

        /// <summary>
        /// A list of 1024 collision masks (one per tile)
        /// </summary>
        public CollisionMask[] Collision = new CollisionMask[TILES_COUNT];

        public Tileconfig()
        {
            Collision = new CollisionMask[TILES_COUNT];
            for (int i = 0; i < Collision.Length; i++)
            {
                Collision[i] = new CollisionMask();
            }
        }

        public Tileconfig(string filename, bool DCver = false) : this(new Reader(filename), DCver)
        {

        }

        public Tileconfig(System.IO.Stream stream, bool DCver = false) : this(new Reader(stream), DCver)
        {

        }

        public Tileconfig(Reader reader, bool DCver = false)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                Collision[i] = new CollisionMask(reader, DCver);
            }
            reader.Close();
        }

        public void Write(string filename, bool DCver = false)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer, DCver);
        }

        public void Write(System.IO.Stream stream, bool DCver = false)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer, DCver);
        }

        public void Write(Writer writer, bool DCver = false)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                Collision[i].Write(writer, DCver);
            }
            writer.Close();
        }

        public enum CollisionSides
        {
            Floor,
            LWall,
            RWall,
            Roof,
            Max,
        }

        public class CollisionMask
        {

            public class HeightMask
            {
                public byte Height = 0;
                public byte Solidity = 0;

                public HeightMask()
                {

                }
            }

            //Tile Angle
            //DC version doesnt have this unknown value
            public byte Angle; 
            public byte Behaviour; 


            public HeightMask[][][] Collision = new HeightMask[2][][];

            public CollisionMask(bool DCver = false)
            {
            }

            public CollisionMask(string filename, bool DCver = false) : this(new Reader(filename), DCver)
            {
            }

            public CollisionMask(System.IO.Stream stream, bool DCver = false) : this(new Reader(stream), DCver)
            {
            }

            public CollisionMask(Reader reader, bool DCver = false)
            {

                for (int p = 0; p < 2; p++)
                {
                    for (int f = 0; f < (int)CollisionSides.Max; f++)
                    {
                        Collision[f] = new HeightMask[(int)CollisionSides.Max][];
                        for (int c = 0; c < 16; c++)
                        {
                            Collision[p][f][c] = new HeightMask();
                        }
                    }
                }

                if (!DCver)
                {
                    byte buffer = reader.ReadByte();
                    Angle = (byte)((buffer & 0xF0) >> 4);
                    Behaviour= (byte)(buffer & 0x0F);
                }

                for (int p = 0; p < 2; p++)
                {
                    for (int f = 0; f < (int)CollisionSides.Max; f++)
                    {
                        for (int c = 0; c < 16; c++)
                        {
                            byte buffer = reader.ReadByte();
                            Collision[p][f][c].Height = (byte)((c & 0xF0) >> 4);
                            Collision[p][f][c].Solidity = (byte)(c & 0x0F);
                        }
                    }
                }
            }

            public void Write(string filename, bool DCver = false)
            {
                using (Writer writer = new Writer(filename))
                    this.Write(writer, DCver);
            }

            public void Write(System.IO.Stream stream, bool DCver = false)
            {
                using (Writer writer = new Writer(stream))
                    this.Write(writer, DCver);
            }

            internal void Write(Writer writer, bool DCver = false)
            {

                if (!DCver)
                {
                    writer.Write(AddNibbles(Angle, Behaviour));
                }

                for (int p = 0; p < 2; p++)
                {
                    for (int f = 0; f < (int)CollisionSides.Max; f++)
                    {
                        for (int c = 0; c < 16; c++)
                        {
                            byte buffer = AddNibbles(Collision[p][f][c].Height, Collision[p][f][c].Solidity);
                            writer.Write(buffer);
                        }
                    }
                }
            }

            public byte AddNibbles(byte a, byte b)
            {
                return (byte)((a & 0xF) << 4 | (b & 0xF));
            }

            public bool IsBitSet(ushort b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }

            public Bitmap DrawCMask(System.Drawing.Color bg, System.Drawing.Color fg, int CollisionDir, bool PathB, Bitmap tile = null)
            {
                Bitmap b;
                bool HasTile = false;
                if (tile == null)
                { b = new Bitmap(16, 16); }
                else
                {
                    b = tile.Clone(new Rectangle(0, 0, tile.Width, tile.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    HasTile = true;
                }

                if (!HasTile)
                {
                    for (int h = 0; h < 16; h++) //Set the BG colour
                    {
                        for (int w = 0; w < 16; w++)
                        {
                            b.SetPixel(w, h, bg);
                        }
                    }
                }

                int p = PathB ? 1 : 0;

                for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                {
                    if (Collision[p][CollisionDir][w].Height <= 15 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 15, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 14 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 14, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 13 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 13, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 12 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 12, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 11 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 11, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 10 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 10, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 9 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 9, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 8 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 8, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 7 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 7, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 6 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 6, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 5 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 5, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 4 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 4, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 3 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 3, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 2 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 2, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 1 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 1, fg);
                    }
                    if (Collision[p][CollisionDir][w].Height <= 0 && Collision[p][CollisionDir][w].Solidity > 0)
                    {
                        b.SetPixel(w, 0, fg);
                    }
                }

                return b;
            }
        }

    }
}
