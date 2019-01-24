using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
{
    /* Background Layout */
    public class BGLayout
    {
        public class BGLayer
        {
            /// <summary>
            /// the array of Chunks IDs for the Layer
            /// </summary>
            public ushort[][] MapLayout { get; set; }

            /// <summary>
            /// Layer Width
            /// </summary>
            public byte width = 0;
            /// <summary>
            /// Layer Height
            /// </summary>
            public byte height = 0;
            /// <summary>
            /// the deform value
            /// set it to 1 for Horizontal parallax, 2 for Vertical parallax, 3 for "3D Sky", anything else for nothing
            /// </summary>
            public byte Deform;
            /// <summary>
            /// how fast the Layer moves while the player is moving
            /// </summary>
            public byte RelativeSpeed;
            /// <summary>
            /// how fast the layer moves while the player isn't moving
            /// </summary>
            public byte ConstantSpeed;

            /// <summary>
            /// a list of Line positions
            /// </summary>
            public List<byte> LineIndexes = new List<byte>();

            public BGLayer()
            {
                width = height = 1;
                Deform = RelativeSpeed = ConstantSpeed = 0;

                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
            }

            public BGLayer(byte w, byte h)
            {
                width = w;
                height = h;
                Deform = RelativeSpeed = ConstantSpeed = 0;

                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
            }

            public BGLayer(Reader reader)
            {
                width = reader.ReadByte();
                height = reader.ReadByte();
                Deform = reader.ReadByte();
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();

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
                        MapLayout[y][x] = reader.ReadByte();
                    }
                }
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)width);
                writer.Write((byte)height);
                writer.Write(Deform);
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);

                for (int i = 0; i < LineIndexes.Count; i++)
                {
                    writer.Write(LineIndexes[i]);
                }
                writer.Write(0xFF);

                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        writer.Write((byte)(MapLayout[h][w]));
                    }
                }
            }

        }

        public class ScrollInfo
        {
            /// <summary>
            /// how fast the line moves while the player is moving
            /// (Known as "Speed" in Taxman's Editor)
            /// </summary>
            public byte RelativeSpeed;
            /// <summary>
            /// How fast the line moves without the player moving
            /// (Known as "Scroll" in Taxman's Editor)
            /// </summary>
            public byte ConstantSpeed;
            /// <summary>
            /// Behaviour Value, controls special line FX
            /// (aka behaviour, Known as "Deform" in Taxman's Editor)
            /// </summary>
            public byte Deform;

            public ScrollInfo()
            {
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                Deform = 0;
            }

            public ScrollInfo(byte r, byte c, byte d)
            {
                RelativeSpeed = r;
                ConstantSpeed = c;
                Deform = d;
            }

            public ScrollInfo(Reader reader)
            {
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Deform = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(Deform);
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
            byte layerCount = reader.ReadByte();

            byte HLineCount = reader.ReadByte();

            //First up is certainly the Horizontal Parallax vallues
            for (int lc = 0; lc < HLineCount; lc++)
            {
                HLines.Add(new ScrollInfo(reader));
            }

            byte VLineCount = reader.ReadByte();

            //Then the Vertical Parallax vallues
            for (int lc = 0; lc < VLineCount; lc++)
            {
                VLines.Add(new ScrollInfo(reader));
            }

            for (int i = 0; i < layerCount; i++) //Next, Read BG Layers
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
            writer.Write((byte)Layers.Count);
            writer.Write((byte)HLines.Count);

            for (int lc = 0; lc < HLines.Count; lc++)
            {
                HLines[lc].Write(writer);
            }

            writer.Write((byte)VLines.Count);

            for (int lc = 0; lc < VLines.Count; lc++)
            {
                VLines[lc].Write(writer);
            }

            for (int i = 0; i < Layers.Count; i++) //Read BG Layers
            {
                Layers[i].Write(writer);
            }

            writer.Close();
        }
    }
}
