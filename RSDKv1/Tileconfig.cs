namespace RSDKv1
{
    public class TileConfig
    {
        /// <summary>
        /// 1024 tiles, always
        /// </summary>
        private const int TILES_COUNT = 0x400;

        /// <summary>
        /// A list of all the mask values
        /// </summary>
        public CollisionMask[][] collisionMasks = new CollisionMask[2][];

        public enum CollisionSides
        {
            Floor,
            RWall,
            LWall,
            Roof,
            Max,
        }

        public class CollisionMask
        {
            public class HeightMask
            {
                public enum Solidities
                {
                    NotSolid,
                    Solid,
                    Solid2 // Idk what the hell this is, in every case I saw it behaves the same as solid
                }

                // Same As v2-v5
                public byte height = 0;

                public Solidities solid = Solidities.NotSolid;

                public HeightMask() { }
            }

            public enum CollisionModes
            {
                Floor,      // Set Player Collision Mode to Floor
                LWall,      // Set Player Collision Mode to LWall
                Roof,       // Set Player Collision Mode to Roof
                RWall,      // Set Player Collision Mode to RWall
                UseAngle,   // Use the calculated angle to determine when to change collision modes
            }

            public CollisionModes collisionMode = CollisionModes.UseAngle;

            public HeightMask[][] heightMasks = new HeightMask[4][];

            public CollisionMask()
            {
                heightMasks[0] = new HeightMask[16];
                heightMasks[1] = new HeightMask[16];
                heightMasks[2] = new HeightMask[16];
                heightMasks[3] = new HeightMask[16];

                for (int c = 0; c < 16; ++c)
                {
                    heightMasks[0][c] = new HeightMask();
                    heightMasks[1][c] = new HeightMask();
                    heightMasks[2][c] = new HeightMask();
                    heightMasks[3][c] = new HeightMask();
                }
            }
        }

        public TileConfig()
        {
            for (int p = 0; p < 2; ++p)
            {
                collisionMasks[p] = new CollisionMask[TILES_COUNT];
                for (int i = 0; i < TILES_COUNT; ++i)
                    collisionMasks[p][i] = new CollisionMask();
            }
        }

        public TileConfig(string filename, bool dcVer = false) : this(new Reader(filename), dcVer) { }

        public TileConfig(System.IO.Stream stream, bool dcVer = false) : this(new Reader(stream), dcVer) { }

        public TileConfig(Reader reader, bool dcVer = false) : this()
        {
            read(reader, dcVer);
        }

        public void read(Reader reader, bool dcVer = false)
        {
            for (int c = 0; c < TILES_COUNT; ++c)
            {
                if (!dcVer)
                {
                    byte buffer = reader.ReadByte();
                    collisionMasks[0][c].collisionMode = (CollisionMask.CollisionModes)((buffer & 0xF0) >> 4);
                    collisionMasks[1][c].collisionMode = (CollisionMask.CollisionModes)(buffer & 0x0F);
                }

                for (int p = 0; p < 2; ++p)
                {
                    for (int f = 0; f < (int)CollisionSides.Max; ++f)
                    {
                        for (int i = 0; i < 16; ++i)
                        {
                            byte buffer = reader.ReadByte();
                            collisionMasks[p][c].heightMasks[f][i].height = (byte)((buffer & 0xF0) >> 4);
                            collisionMasks[p][c].heightMasks[f][i].solid = (CollisionMask.HeightMask.Solidities)(buffer & 0x0F);
                        }
                    }
                }
            }

            reader.Close();
        }

        public void write(string filename, bool DCver = false)
        {
            using (Writer writer = new Writer(filename))
                write(writer, DCver);
        }

        public void write(System.IO.Stream stream, bool DCver = false)
        {
            using (Writer writer = new Writer(stream))
                write(writer, DCver);
        }

        public void write(Writer writer, bool dcVer = false)
        {
            for (int c = 0; c < TILES_COUNT; ++c)
            {
                if (!dcVer)
                    writer.Write(addNibbles((byte)collisionMasks[0][c].collisionMode, (byte)collisionMasks[1][c].collisionMode));

                for (int p = 0; p < 2; ++p)
                {
                    for (int f = 0; f < (int)CollisionSides.Max; ++f)
                    {
                        for (int i = 0; i < 16; ++i)
                            writer.Write(addNibbles(collisionMasks[p][c].heightMasks[f][i].height, (byte)collisionMasks[p][c].heightMasks[f][i].solid));
                    }
                }
            }
            writer.Close();
        }

        private byte addNibbles(byte a, byte b)
        {
            return (byte)((a & 0xF) << 4 | (b & 0xF));
        }

    }
}
