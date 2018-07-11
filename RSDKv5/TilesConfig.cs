using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class TilesConfig
    {
        public static readonly byte[] MAGIC = new byte[] { (byte)'T', (byte)'I', (byte)'L', (byte)'\0' };

        const int TILES_COUNT = 0x400;

        TileConfig[] CollisionPath1 = new TileConfig[TILES_COUNT];
        TileConfig[] CollisionPath2 = new TileConfig[TILES_COUNT];

        public class TileConfig
        {
            // Collision position for each pixel
            public byte[] Collision;

            // If has collision
            public bool[] HasCollision;

            // If is celing, the collision is from below
            public bool IsCeiling;

            // Unknown 5 bytes config
            public byte[] Config;
            byte slopeAngle;
            byte somethingThatChangesPhysics;
            byte probablyMomentumGain;
            byte unknown;
            byte someBool;

            public TileConfig(Stream stream) : this(new Reader(stream)) { }

            internal TileConfig(Reader reader)
            {
                Collision = reader.ReadBytes(16);
                HasCollision = reader.ReadBytes(16).Select(x => x != 0).ToArray();
                IsCeiling = reader.ReadBoolean();
                Config = reader.ReadBytes(5);
            }
        }

        public TilesConfig(string filename) : this(new Reader(filename))
        {

        }

        public TilesConfig(Stream stream) : this(new Reader(stream))
        {

        }

        private TilesConfig(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid tiles config file header magic");

            using (Reader creader = reader.GetCompressedStream())
            {
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath1[i] = new TileConfig(creader);
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath2[i] = new TileConfig(creader);
            }
            reader.ReadByte();
        }
    }
}
