namespace RSDKv2
{
    public class TileConfig
    {
        public class CollisionMask
        {
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

            public CollisionMask(System.IO.Stream stream) : this(new Reader(stream)) { }

            public CollisionMask(Reader reader) : this()
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                byte flags = reader.ReadByte();
                flipY = (flags >> 4) != 0;
                this.flags = (byte)(flags & 0xF);
                floorAngle = reader.ReadByte();
                lWallAngle = reader.ReadByte();
                rWallAngle = reader.ReadByte();
                roofAngle = reader.ReadByte();

                byte[] collision = reader.ReadBytes(8);

                int collisionSolid = reader.ReadByte() << 8;
                collisionSolid |= reader.ReadByte();

                for (int c = 0; c < 8; c++)
                {
                    heightMasks[(c * 2) + 0].height = (byte)((collision[c] & 0xF0) >> 4);
                    heightMasks[(c * 2) + 1].height = (byte)(collision[c] & 0x0F);
                }

                for (int c = 0; c < 16; c++)
                    heightMasks[c].solid = GetBit(collisionSolid, c);
            }

            public void Write(Writer writer)
            {
                writer.Write(AddNybbles(flipY ? (byte)1 : (byte)0, flags));
                writer.Write(floorAngle);
                writer.Write(lWallAngle);
                writer.Write(rWallAngle);
                writer.Write(roofAngle);

                byte[] collision = new byte[8];

                for (int c = 0; c < 8; c++)
                    collision[c] = AddNybbles(heightMasks[(c * 2) + 0].height, heightMasks[(c * 2) + 1].height);

                int collisionSolid = 0;
                for (int c = 0; c < 16; c++)
                {
                    if (heightMasks[c].solid)
                        collisionSolid |= 1 << c;
                }

                writer.Write(collision); //Write Collision Data

                writer.Write((byte)((collisionSolid >> 8) & 0xFF)); // Write Collision Solidity byte 1
                writer.Write((byte)((collisionSolid >> 0) & 0xFF)); // Write Collision Solidity byte 2
            }

            private byte AddNybbles(byte a, byte b)
            {
                return (byte)((a & 0xF) << 4 | (b & 0xF));
            }

            private bool GetBit(int b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }
        }

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

        public TileConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

        public TileConfig(Reader reader)
        {
            Read(reader);
        }
        public void Read(Reader reader)
        {
            for (int p = 0; p < 2; ++p)
            {
                collisionMasks[p] = new CollisionMask[0x400];
                for (int c = 0; c < 0x400; ++c)
                    collisionMasks[p][c] = new CollisionMask();
            }

            for (int c = 0; c < 0x400; ++c)
            {
                collisionMasks[0][c].Read(reader);
                collisionMasks[1][c].Read(reader);
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
            for (int c = 0; c < 0x400; ++c)
            {
                collisionMasks[0][c].Write(writer);
                collisionMasks[1][c].Write(writer);
            }

            writer.Close();
        }

    }
}
