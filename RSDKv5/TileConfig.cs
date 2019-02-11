using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Drawing;

namespace RSDKv5
{
    public class TileConfig
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
            /// <summary>
            /// for special occasions, like conveyor belts
            /// </summary>
            public byte special;
            /// <summary>
            /// If is ceiling, the collision is from below
            /// </summary>
            public bool IsCeiling;

            public CollisionMask()
            {
                Collision = new byte[16];
                HasCollision = new bool[16];
                IsCeiling = false;
                slopeAngle = 0;
                physics = 0;
                momentum = 0;
                unknown = 0;
                special = 0;
            }

            public CollisionMask(Reader reader)
            {
                Collision = reader.ReadBytes(16);
                HasCollision = reader.ReadBytes(16).Select(x => x != 0).ToArray();
                IsCeiling = reader.ReadBoolean();
                slopeAngle = reader.ReadByte();
                physics = reader.ReadByte();
                momentum = reader.ReadByte();
                unknown = reader.ReadByte();
                special = reader.ReadByte();
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
                writer.Write(slopeAngle);
                writer.Write(physics);
                writer.Write(momentum);
                writer.Write(unknown);
                writer.Write(special);
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

        public TileConfig()
        {
            for (int i = 0; i < TILES_COUNT; ++i)
                CollisionPath1[i] = new CollisionMask();
            for (int i = 0; i < TILES_COUNT; ++i)
                CollisionPath2[i] = new CollisionMask();
        }

        public TileConfig(string filename) : this(new Reader(filename))
        {

        }

        public TileConfig(Stream stream) : this(new Reader(stream))
        {

        }

        private TileConfig(Reader reader)
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

        public TileConfig(string filename, bool unc) : this(new Reader(filename), unc)
        {

        }

        public TileConfig(Stream stream, bool unc) : this(new Reader(stream), unc)
        {

        }

        private TileConfig(Reader reader, bool unc)
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
