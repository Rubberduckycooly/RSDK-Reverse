namespace RSDKv2
{
    public class Tiles128x128
    {
        public class Block
        {
            public class Tile
            {
                public enum VisualPlanes
                {
                    Low,
                    High,
                }
                public enum Directions
                {
                    FlipNone,
                    FlipX,
                    FlipY,
                    FlipXY,
                }
                public enum Solidities
                {
                    SolidAll,
                    SolidTop,
                    SolidAllButTop,
                    SolidNone,
                }

                /// <summary>
                /// if tile is on the high or low layer
                /// </summary>
                public VisualPlanes visualPlane = VisualPlanes.Low;
                /// <summary>
                /// the flip value of the tile
                /// </summary>
                public Directions direction = Directions.FlipNone;
                /// <summary>
                /// the Tile's index
                /// </summary>
                public ushort tileIndex = 0;
                /// <summary>
                /// the solidity for Collision Path A
                /// </summary>
                public Solidities solidityA = Solidities.SolidNone;
                /// <summary>
                /// the solidity for Collision Path B
                /// </summary>
                public Solidities solidityB = Solidities.SolidNone;
            }

            /// <summary>
            /// the list of tiles in this chunk
            /// </summary>
            public Tile[][] tiles;

            public Block()
            {
                tiles = new Tile[8][];
                for (int y = 0; y < 8; y++)
                {
                    tiles[y] = new Tile[8];
                    for (int x = 0; x < 8; x++)
                        tiles[y][x] = new Tile();
                }

            }
        }

        /// <summary>
        /// the list of chunks in the file
        /// </summary>
        public Block[] chunkList = new Block[512];

        public Tiles128x128()
        {
            for (int i = 0; i < chunkList.Length; i++)
                chunkList[i] = new Block();
        }

        public Tiles128x128(string filepath) : this(new Reader(filepath)) { }

        public Tiles128x128(System.IO.Stream strm) : this(new Reader(strm)) { }

        public Tiles128x128(Reader reader) : this()
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            byte[] tileBytes = new byte[3];

            for (int c = 0; c < 512; c++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        reader.Read(tileBytes, 0, tileBytes.Length);

                        tileBytes[0] = (byte)(tileBytes[0] - (tileBytes[0] >> 6 << 6));
                        chunkList[c].tiles[y][x].visualPlane = (Block.Tile.VisualPlanes)(tileBytes[0] >> 4);

                        tileBytes[0] = (byte)(tileBytes[0] - (tileBytes[0] >> 4 << 4));
                        chunkList[c].tiles[y][x].direction = (Block.Tile.Directions)(tileBytes[0] >> 2);

                        tileBytes[0] = (byte)(tileBytes[0] - (tileBytes[0] >> 2 << 2));
                        chunkList[c].tiles[y][x].tileIndex = (ushort)((tileBytes[0] << 8) + tileBytes[1]);

                        chunkList[c].tiles[y][x].solidityA = (Block.Tile.Solidities)(tileBytes[2] >> 4);
                        chunkList[c].tiles[y][x].solidityB = (Block.Tile.Solidities)(tileBytes[2] - (tileBytes[2] >> 4 << 4));
                    }
                }
            }
            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }


        public void write(Writer writer)
        {
            int[] tileBytes = new int[3];

            for (int c = 0; c < 512; c++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        tileBytes[0] = 0;

                        tileBytes[0] |= (byte)(chunkList[c].tiles[y][x].tileIndex >> 8); //Put the first bit onto buffer[0]
                        tileBytes[0] = (byte)(tileBytes[0] + (tileBytes[0] >> 2 << 2));
                        tileBytes[0] |= (((int)chunkList[c].tiles[y][x].direction) << 2); //Put the Flip of the tile two bits in
                        tileBytes[0] = (byte)(tileBytes[0] + (tileBytes[0] >> 4 << 4));
                        tileBytes[0] |= ((int)chunkList[c].tiles[y][x].visualPlane) << 4; //Put the Layer of the tile four bits in
                        tileBytes[0] = (byte)(tileBytes[0] + (tileBytes[0] >> 6 << 6));

                        tileBytes[1] = (byte)(chunkList[c].tiles[y][x].tileIndex); //Put the rest of the Tile16x16 Value into this buffer

                        tileBytes[2] = (int)chunkList[c].tiles[y][x].solidityB; //Colision Flag 1 is all bytes before bit 5
                        tileBytes[2] = tileBytes[2] | (int)chunkList[c].tiles[y][x].solidityA << 4; //Colision Flag 0 is all bytes after bit 4

                        writer.Write((byte)tileBytes[0]);
                        writer.Write((byte)tileBytes[1]);
                        writer.Write((byte)tileBytes[2]);
                    }
                }
            }
            writer.Close();
        }
    }
}
