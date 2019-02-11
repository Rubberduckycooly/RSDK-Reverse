using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv2
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
            /// The Slope's angle
            /// </summary>
            public byte slopeAngle;
            /// <summary>
            /// How the player's physics react to the slope
            /// </summary>
            public byte physics;
            /// <summary>
            /// How much momentum is gained from the slope
            /// </summary>
            public byte momentum;
            /// <summary>
            /// Unknown
            /// </summary>
            public byte unknown;

            public CollisionMask()
            {
                Collision = new byte[16];
                HasCollision = new bool[16];
                slopeAngle = 0;
                physics = 0;
                momentum = 0;
                unknown = 0;
                isCeiling = false;
            }

            public CollisionMask(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal CollisionMask(Reader reader)
            {

                byte ic = reader.ReadByte();
                if (ic == 0) isCeiling = false;
                if (ic == 16) isCeiling = true;
                slopeAngle = reader.ReadByte();
                physics = reader.ReadByte();
                momentum = reader.ReadByte();
                unknown = reader.ReadByte();

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
                if (!isCeiling) writer.Write((byte)0);
                else if (isCeiling) writer.Write((byte)16);
                writer.Write(slopeAngle);
                writer.Write(physics);
                writer.Write(momentum);
                writer.Write(unknown);

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
                writer.Write((byte)(CollisionActive & 0xff)); //Write Collision Solidity byte 1
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

        private Tileconfig(Reader reader)
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
