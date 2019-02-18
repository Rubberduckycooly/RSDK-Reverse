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
		public enum LoadState : byte
		{
			NoLoad = 0,
			Fail = 1,
			Success = 2,
			Upgrade = 3
			
		}

		public  LoadState loadstate = LoadState.NoLoad;

        public static readonly byte[] MAGIC = new byte[] { (byte)'C', (byte)'N', (byte)'K', (byte)1 };
		public static readonly byte[] MAGIC_r1 = new byte[] { (byte)'C', (byte)'N', (byte)'K', (byte)'\0' };

		public class TileChunk
        {
			/// <summary>
			/// the layout of the stamp (Layer A)
			/// </summary>
			public ushort[,] TileMapA;
			/// <summary>
			/// the layout of the stamp (Layer B)
			/// </summary>
			public ushort[,] TileMapB;
			/// <summary>
			/// how big the stamp is (value * 16)
			/// </summary>
			public ushort ChunkSize = 8;

            public TileChunk(Dictionary<Point, ushort> points)
            {
                TileMapA = new ushort[ChunkSize, ChunkSize];
                for (int x = 0; x < ChunkSize; x++)
                {
                    for (int y = 0; y < ChunkSize; y++)
                    {
                        Point p = new Point(x, y);
                        if (points.ContainsKey(p)) TileMapA[x, y] = points[p];
                        else TileMapA[x, y] = 0xffff;
                    }
                }
				TileMapB = new ushort[ChunkSize, ChunkSize];
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						TileMapB[x, y] = 0xffff;
					}
				}
			}

			public TileChunk(Dictionary<Point, ushort> pointsA, Dictionary<Point, ushort> pointsB)
			{
				TileMapA = new ushort[ChunkSize, ChunkSize];
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						Point p = new Point(x, y);
						if (pointsA.ContainsKey(p)) TileMapA[x, y] = pointsA[p];
						else TileMapA[x, y] = 0xffff;
					}
				}
				TileMapB = new ushort[ChunkSize, ChunkSize];
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						Point p = new Point(x, y);
						if (pointsB.ContainsKey(p)) TileMapB[x, y] = pointsB[p];
						else TileMapB[x, y] = 0xffff;
					}
				}
			}

			public TileChunk(Reader reader)
			{
				ChunkSize = reader.ReadUInt16();
				TileMapA = new ushort[ChunkSize, ChunkSize];
				TileMapB = new ushort[ChunkSize, ChunkSize];
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						TileMapA[x, y] = reader.ReadUInt16();
					}
				}
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						TileMapB[x, y] = reader.ReadUInt16();
					}
				}
			}

			public TileChunk(Reader reader, int OldFormat = 0)
			{
				if (OldFormat == 0) TileChunkR0(reader);
			}

			public void TileChunkR0(Reader reader)
			{
				ChunkSize = reader.ReadUInt16();
				TileMapA = new ushort[ChunkSize, ChunkSize];
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						TileMapA[x, y] = reader.ReadUInt16();
					}
				}
				TileMapB = new ushort[ChunkSize, ChunkSize];
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						TileMapB[x, y] = 0xffff;
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
                        writer.Write(TileMapA[x, y]);
                    }
                }
				for (int x = 0; x < ChunkSize; x++)
				{
					for (int y = 0; y < ChunkSize; y++)
					{
						writer.Write(TileMapB[x, y]);
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
			byte[] header = reader.ReadBytes(4);

			if (!header.SequenceEqual(MAGIC))
			{
				if (header.SequenceEqual(MAGIC_r1))
				{
					// Revision 0 Reader Code
					Console.WriteLine("Header MAGIC matches an older version of this format. It must be upgraded!");
					loadstate = LoadState.Upgrade;

					int StampCount = reader.ReadUInt16();

					for (int s = 0; s < StampCount; ++s)
						StampList.Add(new TileChunk(reader, 0));
				}
				else
				{
					Console.WriteLine("Header MAGIC was not the same as the stamp header! aborting!");
					loadstate = LoadState.Fail;
					return;
				}

			}
			else
			{
				loadstate = LoadState.Success;

				int StampCount = reader.ReadUInt16();

				for (int s = 0; s < StampCount; ++s)
					StampList.Add(new TileChunk(reader));
			}
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