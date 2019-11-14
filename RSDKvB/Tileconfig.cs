using System.Drawing;

namespace RSDKvB
{
    public class Tileconfig
    {
        /// <summary>
        /// 1024, one for each tile
        /// </summary>
        const int TILES_COUNT = 1024;

        /// <summary>
        /// A list of all the mask values on plane A
        /// </summary>
        public CollisionMask[] CollisionPath1 = new CollisionMask[TILES_COUNT];
        /// <summary>
        /// A list of all the mask values on plane B
        /// </summary>
        public CollisionMask[] CollisionPath2 = new CollisionMask[TILES_COUNT];

        public class CollisionMask
        {
            /// <summary>
            /// Collision position for each pixel
            /// </summary>
            public byte[] Collision = new byte[16]; //two Collision Values per read byte

            /// <summary>
            /// the collision flags for each "column"
            /// </summary>
            public bool[] HasCollision = new bool[16];

            /// <summary>
            /// is the Mask A ceiling mask?
            /// </summary>
            public bool isCeiling;
            /// <summary>
            /// is the Mask A ceiling mask?
            /// </summary>
            public byte Behaviour;
            /// <summary>
            /// Angle value when walking on the floor
            /// </summary>
            public byte FloorAngle;
            /// <summary>
            /// Angle value when walking on RWall
            /// </summary>
            public byte RWallAngle;
            /// <summary>
            /// Angle value when walking on LWall
            /// </summary>
            public byte LWallAngle;
            /// <summary>
            /// Angle value when walking on the ceiling
            /// </summary>
            public byte CeilingAngle;

            public CollisionMask()
            {
                Collision = new byte[16];
                HasCollision = new bool[16];
                FloorAngle = 0x00;
                RWallAngle = 0xC0;
                LWallAngle = 0x40;
                CeilingAngle = 0x80;
                isCeiling = false;
            }

            public CollisionMask(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal CollisionMask(Reader reader)
            {

                byte flags = reader.ReadByte();
                isCeiling = (flags >> 4) != 0;
                Behaviour = (byte)(flags & 0xF);
                FloorAngle = reader.ReadByte();
                RWallAngle = reader.ReadByte();
                LWallAngle = reader.ReadByte();
                CeilingAngle = reader.ReadByte();

                byte[] collision = reader.ReadBytes(8);

                int ActiveCollision = reader.ReadByte() << 8;
                ActiveCollision |= reader.ReadByte();

                int i = 0;
                int i2 = 1;

                for (int c = 0; c < 8; c++)
                {
                    Collision[i] = (byte)((collision[c] & 0xF0) >> 4);
                    Collision[i2] = (byte)(collision[c] & 0x0F);
                    i += 2;
                    i2 += 2;
                }

                int b = 0;

                for (int ii = 0; ii < 16; ii++)
                {
                    HasCollision[ii] = IsBitSet(ActiveCollision, b);
                    b++;
                }

            }

            public void Write(Writer writer)
            {
                writer.Write(AddNibbles(isCeiling ? (byte)1 : (byte)0, Behaviour));
                writer.Write(FloorAngle);
                writer.Write(RWallAngle);
                writer.Write(LWallAngle);
                writer.Write(CeilingAngle);

                byte[] collision = new byte[8];
                int CollisionActive = 0;

                int c = 0;

                for (int i = 0; i < 8; i++)
                {
                    collision[i] = AddNibbles(Collision[c++], Collision[c++]);
                }

                for (int i = 0; i < 16; i++)
                {
                    if (HasCollision[i])
                    {
                        CollisionActive |= 1 << i;
                    }
                    if (!HasCollision[i])
                    {
                        CollisionActive |= 0 << i;
                    }
                }

                writer.Write(collision); //Write Collision Data

                writer.Write((byte)(CollisionActive >> 8)); //Write Collision Solidity byte 1
                writer.Write((byte)(CollisionActive & 0xff)); //Write Collision Solidity byte 2
            }

            public byte AddNibbles(byte a, byte b)
            {
                return (byte)((a & 0xF) << 4 | (b & 0xF));
            }

            public bool IsBitSet(int b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }

            public Bitmap DrawCMask(System.Drawing.Color bg, System.Drawing.Color fg, Bitmap tile = null)
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

                if (!isCeiling)
                {
                    for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                    {
                        for (int h = 0; h < 16; h++)
                        {
                            if (Collision[w] <= h && HasCollision[w])
                            {
                                b.SetPixel(w, h, fg);
                            }
                        }
                    }
                }

                if (isCeiling)
                {
                    for (int y = 0; y < 16; y++) //Set the Active/Main (FG) colour
                    {
                        for (int x = 0; x < 16; x++) //Set the Active/Main (FG) colour
                        {
                            b.SetPixel(x, y, bg);
                        }
                    }

                    for (int w = 15; w > -1; w--) //Set the Active/Main (FG) colour
                    {
                        for (int h = 15; h > -1; h--)
                        {
                            if (Collision[w] >= h && HasCollision[w])
                            {
                                b.SetPixel(w, h, fg);
                            }
                        }
                    }
                }
                return b;
            }

        }

        public Tileconfig()
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                CollisionPath1[i] = new CollisionMask();
                CollisionPath2[i] = new CollisionMask();
            }
        }

        public Tileconfig(string filename) : this(new Reader(filename))
        {

        }

        public Tileconfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Tileconfig(Reader reader)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                CollisionPath1[i] = new CollisionMask(reader);
                CollisionPath2[i] = new CollisionMask(reader);
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            Write(new Writer(filename));
        }

        public void Write(System.IO.Stream s)
        {
            Write(new Writer(s));
        }

        public void Write(Writer writer)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                CollisionPath1[i].Write(writer);
                CollisionPath2[i].Write(writer);
            }
            writer.Close();
        }

    }
}
