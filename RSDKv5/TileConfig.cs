using System;
using System.Linq;
using System.IO;

namespace RSDKv5
{
    public class TileConfig
    {
        public class CollisionMask : ICloneable
        {
            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public class HeightMask
            {
                public HeightMask() { }

                public byte height = 0;
                public bool solid = false;
            }

            /// <summary>
            /// collision values for each column
            /// </summary>
            public HeightMask[] heightMasks = new HeightMask[16];

            /// <summary>
            /// is the mask yFlipped
            /// </summary>
            public bool flipY = false;
            /// <summary>
            /// generic flags value, can be used by scripts for any purpose
            /// </summary>
            public byte flags = 0;
            /// <summary>
            /// Angle value when walking on the floor
            /// </summary>
            public byte floorAngle = 0x00;
            /// <summary>
            /// Angle value when walking on LWall
            /// </summary>
            public byte lWallAngle = 0xC0;
            /// <summary>
            /// Angle value when walking on the ceiling
            /// </summary>
            public byte roofAngle = 0x80;
            /// <summary>
            /// Angle value when walking on RWall
            /// </summary>
            public byte rWallAngle = 0x40;

            public CollisionMask()
            {
                for (int c = 0; c < 16; ++c)
                    heightMasks[c] = new HeightMask();
            }

            public CollisionMask(Reader reader) : this()
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                byte[] collision = reader.ReadBytes(16);
                bool[] collisionSolid = reader.ReadBytes(16).Select(x => x != 0).ToArray();

                for (int c = 0; c < 16; c++)
                {
                    heightMasks[c].height = collision[c];
                    heightMasks[c].solid = collisionSolid[c];
                }

                flipY = reader.ReadBoolean();
                floorAngle = reader.ReadByte();
                lWallAngle = reader.ReadByte();
                rWallAngle = reader.ReadByte();
                roofAngle = reader.ReadByte();
                flags = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                for (int c = 0; c < 16; c++)
                    writer.Write(heightMasks[c].height);
                for (int c = 0; c < 16; c++)
                    writer.Write(heightMasks[c].solid);

                writer.Write(flipY);
                writer.Write(floorAngle);
                writer.Write(lWallAngle);
                writer.Write(rWallAngle);
                writer.Write(roofAngle);
                writer.Write(flags);
            }
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'T', (byte)'I', (byte)'L', 0 };

        /// <summary>
        /// A list of all the mask values
        /// </summary>
        public CollisionMask[][] collisionMasks = new CollisionMask[2][];

        public TileConfig()
        {
            for (int p = 0; p < 2; ++p)
            {
                collisionMasks[p] = new CollisionMask[0x400];
                for (int i = 0; i < 0x400; ++i)
                    collisionMasks[p][i] = new CollisionMask();
            }
        }

        public TileConfig(string filename) : this(new Reader(filename)) { }

        public TileConfig(Stream stream) : this(new Reader(stream)) { }

        public TileConfig(Reader reader) : this()
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid TileConfig signature");
            }

            using (Reader creader = reader.GetCompressedStream())
            {
                for (int i = 0; i < 0x400; ++i)
                    collisionMasks[0][i].Read(creader);

                for (int i = 0; i < 0x400; ++i)
                    collisionMasks[1][i].Read(creader);
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

        public void Write(Writer writer)
        {
            writer.Write(signature);
            using (var stream = new MemoryStream())
            {
                using (var cwriter = new Writer(stream))
                {
                    for (int i = 0; i < 0x400; ++i)
                        collisionMasks[0][i].Write(cwriter);
                    for (int i = 0; i < 0x400; ++i)
                        collisionMasks[1][i].Write(cwriter);
                }

                writer.WriteCompressed(stream.ToArray());
            }

            writer.Close();
        }
    }
}
