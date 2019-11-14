using System;
using System.Linq;
using System.IO;
using System.Drawing;

namespace RSDKv5
{
    public class Tileconfig
    {
        /// <summary>
        /// the signature of the file
        /// </summary>
        public static readonly byte[] MAGIC = new byte[] { (byte)'T', (byte)'I', (byte)'L', (byte)'\0' };

        /// <summary>
        /// how many tiles we can store values for (1024)
        /// </summary>
        const int TILES_COUNT = 0x400;

        /// <summary>
        /// the collison data for Path A
        /// </summary>
        public CollisionMask[] CollisionPath1 = new CollisionMask[TILES_COUNT];
        /// <summary>
        /// the collison data for Path B
        /// </summary>
        public CollisionMask[] CollisionPath2 = new CollisionMask[TILES_COUNT];



        public class CollisionMask : ICloneable
        {
            public object Clone()
            {
                return this.MemberwiseClone();
            }

            /// <summary>
            /// Collision position for each pixel
            /// </summary>
            public byte[] Collision;

            /// <summary>
            /// If has collision
            /// </summary>
            public bool[] HasCollision;

            // Collision Mask config

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
            /// <summary>
            /// for Behaviour occasions, like conveyor belts
            /// </summary>
            public byte Behaviour;
            /// <summary>
            /// If is ceiling, the collision is from below
            /// </summary>
            public bool IsCeiling;

            public CollisionMask()
            {
                Collision = new byte[16];
                HasCollision = new bool[16];
                IsCeiling = false;
                FloorAngle = 0x00;
                RWallAngle = 0xC0;
                LWallAngle = 0x40;
                CeilingAngle = 0x80;
                Behaviour = 0;
            }

            public CollisionMask(Reader reader)
            {
                Collision = reader.ReadBytes(16);
                HasCollision = reader.ReadBytes(16).Select(x => x != 0).ToArray();
                IsCeiling = reader.ReadBoolean();
                FloorAngle = reader.ReadByte();
                RWallAngle = reader.ReadByte();
                LWallAngle = reader.ReadByte();
                CeilingAngle = reader.ReadByte();
                Behaviour = reader.ReadByte();
            }

            public CollisionMask(Stream stream) : this(new Reader(stream))
            {

            }

            public void Write(System.IO.BinaryWriter writer)
            {

            }

            public void WriteUnc(System.IO.BinaryWriter writer)
            {
                writer.Write(Collision);
                for (int i = 0; i < HasCollision.Length; i++)
                { writer.Write(HasCollision[i]); }
                writer.Write(IsCeiling);
                writer.Write(FloorAngle);
                writer.Write(RWallAngle);
                writer.Write(LWallAngle);
                writer.Write(CeilingAngle);
                writer.Write(Behaviour);
            }

            public Bitmap DrawCMask(System.Drawing.Color bg, System.Drawing.Color fg, Bitmap tile = null)
            {
                Bitmap b;
                bool HasTile = false;
                if (tile == null)
                {
                    b = new Bitmap(16, 16);
                }
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

                if (!IsCeiling)
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

                if (IsCeiling)
                {
                    for (int y = 0; y < 16; y++) //Set the Active/Main (FG) colour
                    {
                        for (int x = 0; x < 16; x++) //Set the Active/Main (FG) colour
                        {
                            if (!HasTile)
                            {
                                b.SetPixel(x, y, bg);
                            }
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
                CollisionPath1[i] = new CollisionMask();
            for (int i = 0; i < TILES_COUNT; ++i)
                CollisionPath2[i] = new CollisionMask();
        }

        public Tileconfig(string filename) : this(new Reader(filename))
        {

        }

        public Tileconfig(Stream stream) : this(new Reader(stream))
        {

        }

        public Tileconfig(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid tiles config file header magic");

            using (Reader creader = reader.GetCompressedStream())
            {
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath1[i] = new CollisionMask(creader);
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath2[i] = new CollisionMask(creader);
            }
            reader.Close();
        }

        public Tileconfig(string filename, bool unc) : this(new Reader(filename), unc)
        {

        }

        public Tileconfig(Stream stream, bool unc) : this(new Reader(stream), unc)
        {

        }

        private Tileconfig(Reader reader, bool unc)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid tiles config file header magic");
            if (!unc)
            {
                using (Reader creader = reader.GetCompressedStream())
                {
                    for (int i = 0; i < TILES_COUNT; ++i)
                        CollisionPath1[i] = new CollisionMask(creader);
                    for (int i = 0; i < TILES_COUNT; ++i)
                        CollisionPath2[i] = new CollisionMask(creader);
                }
            }
            else
            {
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath1[i] = new CollisionMask(reader);
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath2[i] = new CollisionMask(reader);
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            Write(new Writer(filename));
        }

        public void Write(Stream s)
        {
            Write(new Writer(s));
        }

        // Write Compressed
        public void Write(Writer writer)
        {
            writer.Write(MAGIC);
            using (var stream = new MemoryStream())
            {
                using (var writerUncompressed = new BinaryWriter(stream))
                {
                    for (int i = 0; i < TILES_COUNT; ++i)
                        CollisionPath1[i].WriteUnc(writerUncompressed);
                    for (int i = 0; i < TILES_COUNT; ++i)
                        CollisionPath2[i].WriteUnc(writerUncompressed);
                }
                writer.WriteCompressed(stream.ToArray());
            }
            writer.Close();
        }

        public void WriteUnc(string filename)
        {
            WriteUnc(new Writer(filename));
        }

        public void WriteUnc(Stream s)
        {
            WriteUnc(new Writer(s));
        }

        // Write Uncompressed
        public void WriteUnc(Writer writer)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
                CollisionPath1[i].WriteUnc(writer);
            for (int i = 0; i < TILES_COUNT; ++i)
                CollisionPath2[i].WriteUnc(writer);
            writer.Close();
        }
    }
}
