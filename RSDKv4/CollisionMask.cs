using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv4
{
    public class CollisionMask
    {

        const int TILES_COUNT = 1024;

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

            public TileConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal TileConfig(Reader reader)
            {
                Collision = reader.ReadBytes(10); // Collision, I think
                Config = reader.ReadBytes(5); // ???
            }
        }

        public CollisionMask(string filename) : this(new Reader(filename))
        {

        }

        public CollisionMask(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private CollisionMask(Reader reader)
        {
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath1[i] = new TileConfig(reader);
                for (int i = 0; i < TILES_COUNT; ++i)
                    CollisionPath2[i] = new TileConfig(reader);
            reader.Close();
        }
    }
}
