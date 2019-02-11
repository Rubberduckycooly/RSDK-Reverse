using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
{
    /* Background Layout */
    public class BGLayout
    {
        public class ScrollInfo
        {
            /// <summary>
            /// how fast the line moves while the player is moving
            /// </summary>
            public byte RelativeSpeed;
            /// <summary>
            /// How fast the line moves without the player moving
            /// </summary>
            public byte ConstantSpeed;
            /// <summary>
            /// the draw order of the layer
            /// </summary>
            public byte DrawLayer;
            /// <summary>
            /// a special byte that tells the game what "behaviour" property the layer has
            /// </summary>
            public byte Behaviour;

            public ScrollInfo()
            {
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                DrawLayer = 0;
                Behaviour = 0;
            }

            public ScrollInfo(byte r, byte c, byte d, byte b)
            {
                RelativeSpeed = r;
                ConstantSpeed = c;
                DrawLayer = d;
                Behaviour = b;
            }

            public ScrollInfo(Reader reader)
            {
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                DrawLayer = reader.ReadByte();
                Behaviour = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(DrawLayer);
                writer.Write(Behaviour);
            }

        }

        public class BGLayer
        {
            /// <summary>
            /// the array of Chunks IDs for the Layer
            /// </summary>
            public ushort[][] MapLayout { get; set; }

            /// <summary>
            /// Layer Width
            /// </summary>
            public ushort width = 0;
            /// <summary>
            /// Layer Height
            /// </summary>
            public ushort height = 0;
            /// <summary>
            /// the draw order of the layer
            /// </summary>
            public byte DrawLayer;
            /// <summary>
            /// a special byte that tells the game what "behaviour" property the layer has
            /// </summary>
            public byte Behaviour;
            /// <summary>
            /// how fast the Layer moves while the player is moving
            /// </summary>
            public byte RelativeSpeed;
            /// <summary>
            /// how fast the layer moves while the player isn't moving
            /// </summary>
            public byte ConstantSpeed;

            /// <summary>
            /// indexes to HLine values
            /// </summary>
            public List<byte> LineIndexes = new List<byte>();

            public BGLayer()
            {
                width = height = 1;
                DrawLayer = Behaviour = RelativeSpeed = ConstantSpeed = 0;

                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
            }

            public BGLayer(ushort w, ushort h)
            {
                width = w;
                height = h;
                DrawLayer = Behaviour = RelativeSpeed = ConstantSpeed = 0;

                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
            }

            public BGLayer(Reader reader)
            {
                byte[] buffer = new byte[2];

                reader.Read(buffer, 0, 2); //Read size
                width = (ushort)(buffer[0] + (buffer[1] << 8));

                reader.Read(buffer, 0, 2); //Read size
                height = (ushort)(buffer[0] + (buffer[1] << 8));

                ConstantSpeed = reader.ReadByte();
                RelativeSpeed = reader.ReadByte();
                DrawLayer = reader.ReadByte();
                Behaviour = reader.ReadByte();

                int j = 0;
                while (j < 1)
                {
                    byte b;

                    b = reader.ReadByte();

                    if (b == 255)
                    {
                        byte tmp2 = reader.ReadByte();

                        if (tmp2 == 255)
                        {
                            j = 1;
                        }
                        else
                        {
                            b = reader.ReadByte();
                        }
                    }

                    LineIndexes.Add(b);
                }

                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //128x128 Block number is 16-bit
                        //Little-Endian in RSDKv4	
                        reader.Read(buffer, 0, 2); //Read size
                        MapLayout[y][x] = (ushort)(buffer[0] + (buffer[1] << 8));
                    }
                }
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)(width & 0xff));
                writer.Write((byte)(width >> 8));

                writer.Write((byte)(height & 0xff));
                writer.Write((byte)(height >> 8));

                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(DrawLayer);
                writer.Write(Behaviour);

                for (int i = 0; i < LineIndexes.Count; i++)
                {
                    writer.Write(LineIndexes[i]);
                }
                writer.Write(0xFF);

                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        writer.Write((byte)(MapLayout[h][w] & 0xff));
                        writer.Write((byte)(MapLayout[h][w] >> 8));
                    }
                }

            }

        }

        /// <summary>
        /// A list of Horizontal Line Scroll Values
        /// </summary>
        public List<ScrollInfo> HLines = new List<ScrollInfo>();
        /// <summary>
        /// A list of Vertical Line Scroll Values
        /// </summary>
        public List<ScrollInfo> VLines = new List<ScrollInfo>();
        /// <summary>
        /// A list of Background layers
        /// </summary>
        public List<BGLayer> Layers = new List<BGLayer>();

        public BGLayout()
        {

        }

        public BGLayout(string filename) : this(new Reader(filename))
        {

        }

        public BGLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public BGLayout(Reader reader)
        {
            byte LayerCount = reader.ReadByte();

            byte HLineCount = reader.ReadByte();

            for (int i = 0; i < HLineCount; i++)
            {
                ScrollInfo p = new ScrollInfo(reader);
                HLines.Add(p);
            }

            byte VLineCount = reader.ReadByte();

            for (int i = 0; i < VLineCount; i++)
            {
                ScrollInfo p = new ScrollInfo(reader);
                VLines.Add(p);
            }

            for (int i = 0; i < LayerCount; i++)
            {
                Layers.Add(new BGLayer(reader));
            }

            reader.Close();
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
            // Save width and height
            writer.Write((byte)Layers.Count);

            writer.Write((byte)HLines.Count);

            for (int i = 0; i < HLines.Count; i++)
            {
                HLines[i].Write(writer);
            }

            writer.Write((byte)VLines.Count);

            for (int i = 0; i < VLines.Count; i++)
            {
                VLines[i].Write(writer);
            }

            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Write(writer);
            }
            writer.Close();
        }

    }
}
