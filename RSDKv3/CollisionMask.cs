using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    public class CollisionMask
    {

        const int TILES_COUNT = 1024;

        public TileConfig[] Collision = new TileConfig[TILES_COUNT];

        public class TileConfig
        {
            // Collision position for each pixel
            public byte[] Collision;

            // If has collision
            //public bool[] HasCollision;

            // Unknown 5 bytes config
            public byte[] Config; //Some of these affect collision

            //Slope is one value
            //Momentum is another

            //Unknown Byte Values
            public byte[] Unknown;

            public TileConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal TileConfig(Reader reader)
            {
                Config = reader.ReadBytes(5);
                //Byte 1: Unknown
                //Byte 2: Slope
                //Byte 3: Physics
                //Byte 4: Unknown (may be momentum)
                //Byte 5: Unknown
                Collision = reader.ReadBytes(10); //Seems to be only for path A, IDK about path B
                Unknown = reader.ReadBytes(15); // No Idea Right now
            }

            public void Write(Writer writer)
            {
                writer.Write(Config);
                writer.Write(Collision);
                writer.Write(Unknown);
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
            for (int i = 0; i < TILES_COUNT; ++i)
                Collision[i].Write(writer);
        }

    }
}
