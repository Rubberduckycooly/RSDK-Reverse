using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class CollisionMask
    {

        const int TILES_COUNT = 1024;

        TileConfig[] Collision = new TileConfig[TILES_COUNT];

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

            //Unknown Byte Values
            public byte[] Unknown;

            public TileConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal TileConfig(Reader reader)
            {
                //IsCeiling = reader.ReadBoolean();
                Config = reader.ReadBytes(5);
                Collision = reader.ReadBytes(16);
                Unknown = reader.ReadBytes(9);
                //HasCollision = Collision.Select(x => x != 0).ToArray();
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
                Collision[i] = new TileConfig(reader);
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        internal void Write(Writer writer)
        {

        }

    }
}
