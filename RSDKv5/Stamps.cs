using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

//------------------RSDKv5 Stamps format---------------------//
//--------first format revision by: Carjem Generations-------//
//----implementation and finalisation by Rubberduckycooly----//

namespace RSDKv5
{
    public class Stamps
    {

        public static readonly byte[] MAGIC = new byte[] { (byte)'C', (byte)'N', (byte)'K', (byte)'\0' };

        public class TileChunk
        {
            /// <summary>
            /// the layout of the stamp
            /// </summary>
            public ushort[,] TileMap;
            /// <summary>
            /// how big the stamp is (value * 16)
            /// </summary>
            public ushort ChunkSize = 8;

            public TileChunk(Dictionary<Point, ushort> points)
            {
                TileMap = new ushort[ChunkSize, ChunkSize];
                for (int x = 0; x < ChunkSize; x++)
                {
                    for (int y = 0; y < ChunkSize; y++)
                    {
                        Point p = new Point(x, y);
                        if (points.ContainsKey(p)) TileMap[x, y] = points[p];
                        else TileMap[x, y] = 0xffff;
                    }
                }
            }

            public TileChunk(Reader reader)
            {
                ChunkSize = reader.ReadUInt16();
                TileMap = new ushort[ChunkSize, ChunkSize];
                for (int x = 0; x < ChunkSize; x++)
                {
                    for (int y = 0; y < ChunkSize; y++)
                    {
                        TileMap[x, y] = reader.ReadUInt16();
                    }
                }
            }

            public void Write(Writer writer)
            {
                writer.Write(ChunkSize);
                for (int x = 0; x < ChunkSize; x++)
                {
                    for (int y = 0; y < ChunkSize; y++)
                    {
                        writer.Write(TileMap[x, y]);
                    }
                }
            }

        }

        public List<TileChunk> StampList = new List<TileChunk>();

        public Stamps(string filename) : this(new Reader(filename))
        {

        }

        public Stamps()
        {

        }

        public Stamps(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
            {
                Console.WriteLine("Header MAGIC was not the same as the stamp header! aborting!");
                return;
            }

            int StampCount = reader.ReadUInt16();

            for (int s = 0; s < StampCount; ++s)
                StampList.Add(new TileChunk(reader));
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);

            writer.Write((ushort)StampList.Count);

            for (int s = 0; s < (ushort)StampList.Count; ++s)
                StampList[s].Write(writer);
        }

    }
}