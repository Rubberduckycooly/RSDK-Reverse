using System.Collections.Generic;

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
            public short RelativeSpeed;
            /// <summary>
            /// how fast the layer moves while the player isn't moving
            /// </summary>
            public short ConstantSpeed;

            /// <summary>
            /// a list of Line positions
            /// </summary>
            public byte[] LineIndexes;

            public BGLayer()
            {
                width = height = 1;
                Deform = 0;
                RelativeSpeed = ConstantSpeed = 0;
                LineIndexes = new byte[height * 128];
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
                Deform = 0;
                RelativeSpeed = ConstantSpeed = 0;
                LineIndexes = new byte[height * 128];
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

                byte[] buf = new byte[3];
                bool finished = false;
                int cnt = 0;
                int loop = 0;

                LineIndexes = new byte[height * 128];

                while (!finished)
                {
                    buf[0] = reader.ReadByte();
                    if (buf[0] == 255)
                    {
                        buf[1] = reader.ReadByte();
                        if (buf[1] == 255)
                        {
                            finished = true;
                            break;
                        }
                        else
                        {
                            buf[2] = (byte)(reader.ReadByte() - 1);
                            loop = 0;

                            while (loop < buf[2] && !reader.IsEof)
                            {
                                LineIndexes[cnt++] = buf[1];
                                loop++;
                            }
                        }
                    }
                    else
                    {
                        LineIndexes[cnt++] = buf[0];
                    }
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
                writer.Write(width);
                writer.Write(height);
                writer.Write(Deform);
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);

                // Output data
                int l = 0;
                int cnt = 0;

                for (int x = 0; x < LineIndexes.Length; x++)
                {
                    if (LineIndexes[x] != l && x > 0)
                    {
                        rle_write(writer, l, cnt);
                        cnt = 0;
                    }
                    l = LineIndexes[x];
                    cnt++;
                }

                rle_write(writer, l, cnt);

                writer.Write((byte)0xFF);
                writer.Write((byte)0xFF);

                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        writer.Write((byte)(MapLayout[h][w]));
                    }
                }
            }

            private static void rle_write(Writer file, int pixel, int count)
            {
                if (count <= 2)
                {
                    for (int y = 0; y < count; y++)
                        file.Write((byte)pixel);
                }
                else
                {
                    while (count > 0)
                    {
                        file.Write((byte)0xFF);

                        file.Write((byte)pixel);

                        file.Write((byte)((count > 253) ? 254 : (count + 1)));
                        count -= 253;
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
