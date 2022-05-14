using System.Collections.Generic;

namespace RSDKv1
{

    public class Backgrounds
    {
        public class ScrollInfo
        {
            /// <summary>
            /// how fast the line moves while the player is moving, relative to 0x80.
            /// E.G: 0x40 == move 1 pixel per 1 pixel of camera movement, 0x40 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public byte parallaxFactor = 0x80;

            /// <summary>
            /// How fast the line moves without the player moving
            /// E.G: 0x80 == move 1 pixel per 1 pixel of camera movement, 0x20 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public byte scrollSpeed = 0;

            /// <summary>
            /// determines if the scrollInfo allows deformation or not
            /// </summary>
            public bool deform = false;

            /// <summary>
            /// parallaxFactor, represented as a floating point number relative to 1.0
            /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public float parallaxFactorF
            {
                get
                {
                    return parallaxFactor * 0.0078125f; // 1 / 128
                }
                set
                {
                    parallaxFactor = (byte)(value * 128.0f);
                }
            }

            /// <summary>
            /// scrollSpeed, represented as a floating point number relative to 1.0
            /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public float scrollSpeedF
            {
                get
                {
                    return scrollSpeed * 0.015625f; // 1 / 64
                }
                set
                {
                    scrollSpeed = (byte)(value * 64.0f);
                }
            }

            public ScrollInfo() { }

            public ScrollInfo(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                // 1 byte, unsigned
                parallaxFactor = reader.ReadByte();
                scrollSpeed = reader.ReadByte();
                deform = reader.ReadBoolean();
            }

            public void Write(Writer writer)
            {
                writer.Write(parallaxFactor);
                writer.Write(scrollSpeed);
                writer.Write(deform);
            }

        }

        public class Layer
        {
            public enum LayerTypes
            {
                // 0 = none, not included here as an enum value because it should never be used lol

                /// <summary>
                /// self-explanitory, uses LineScroll to do horizontal parallax, while layer scrolls on Y axis
                /// </summary>
                HScroll = 1,
                /// <summary>
                /// also self-explanitory, uses LineScroll to do vertical parallax, while layer scrolls on X axis
                /// </summary>
                VScroll,
                /// <summary>
                /// layer behaves like a "Mode 7-like 3D Sky", pretty useless without code to properly manage it
                /// </summary>
                Sky3D,
                /// <summary>
                /// layer behaves like a "Mode 7-like 3D Floor", pretty useless without code to properly manage it
                /// </summary>
                Floor3D,
            }

            /// <summary>
            /// the chunk layout for the layer
            /// </summary>
            public byte[][] layout;

            /// <summary>
            /// Layer Width
            /// </summary>
            public byte width = 0;

            /// <summary>
            /// Layer Height
            /// </summary>
            public byte height = 0;

            /// <summary>
            /// determines layer type
            /// </summary>
            public LayerTypes type = LayerTypes.HScroll;

            /// <summary>
            /// how fast the Layer moves while the player is moving, relative to 0x80.
            /// E.G: 0x80 == move 1 pixel per 1 pixel of camera movement, 0x40 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public byte parallaxFactor = 0x80;

            /// <summary>
            /// how fast the layer moves while the player isn't moving
            /// </summary>
            public byte scrollSpeed = 0;

            /// <summary>
            /// a list of Line positions
            /// </summary>
            public byte[] lineScroll;


            /// <summary>
            /// parallaxFactor, represented as a floating point number relative to 1.0
            /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public float parallaxFactorF
            {
                get
                {
                    return parallaxFactor * 0.0078125f; // 1 / 128
                }
                set
                {
                    parallaxFactor = (byte)(value * 128.0f);
                }
            }

            /// <summary>
            /// scrollSpeed, represented as a floating point number relative to 1.0
            /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
            /// </summary>
            public float scrollSpeedF
            {
                get
                {
                    return scrollSpeed * 0.015625f; // 1 / 64
                }
                set
                {
                    scrollSpeed = (byte)(value * 64.0f);
                }
            }

            public Layer(byte w = 0, byte h = 0)
            {
                width = w;
                height = h;

                lineScroll = new byte[height * 128];
                layout = new byte[height][];
                for (int y = 0; y < height; y++)
                    layout[y] = new byte[width];
            }

            public Layer(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                width = reader.ReadByte();
                height = reader.ReadByte();
                type = (LayerTypes)reader.ReadByte();

                parallaxFactor = reader.ReadByte();
                scrollSpeed = reader.ReadByte();

                lineScroll = new byte[height * 128];

                // Read Line Scroll
                byte[] buf = new byte[3];
                int pos = 0;
                while (true)
                {
                    buf[0] = reader.ReadByte();
                    if (buf[0] == 0xFF)
                    {
                        buf[1] = reader.ReadByte();
                        if (buf[1] != 0xFF)
                        {
                            buf[2] = reader.ReadByte();
                            byte val = buf[1];
                            int cnt = buf[2] - 1;
                            for (int c = 0; c < cnt; ++c)
                            {
                                if (pos < lineScroll.Length)
                                    lineScroll[pos++] = val;
                            }
                        }
                        else
                            break;
                    }
                    else
                    {
                        if (pos < lineScroll.Length)
                            lineScroll[pos++] = buf[0];
                    }
                }

                layout = new byte[height][];
                for (int y = 0; y < height; y++)
                    layout[y] = new byte[width];

                for (int y = 0; y < height; y++)
                {
                    // 128x128 Block number is 8-bit in RSDKv1
                    for (int x = 0; x < width; x++)
                        layout[y][x] = reader.ReadByte();
                }
            }

            public void Write(Writer writer)
            {
                writer.Write(width);
                writer.Write(height);
                writer.Write((byte)type);
                writer.Write(parallaxFactor);
                writer.Write(scrollSpeed);

                // Write Line Scroll
                int l = 0;
                int cnt = 0;

                for (int line = 0; line < height * 128; line++)
                {
                    int index = 0;
                    if (line < lineScroll.Length)
                        index = lineScroll[line];

                    if (index != l && line > 0)
                    {
                        WriteRLE(writer, l, cnt);
                        cnt = 0;
                    }
                    l = index;
                    cnt++;
                }

                WriteRLE(writer, l, cnt);

                writer.Write((byte)0xFF);
                writer.Write((byte)0xFF);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                        writer.Write(layout[y][x]);
                }
            }

            private static void WriteRLE(Writer file, int value, int count)
            {
                if (count <= 2)
                {
                    for (int y = 0; y < count; y++)
                        file.Write((byte)value);
                }
                else
                {
                    while (count > 0)
                    {
                        file.Write((byte)0xFF);
                        file.Write((byte)value);
                        file.Write((byte)((count > 254) ? 255 : (count + 1)));
                        count -= 254;
                    }
                }
            }

            /// <summary>
            /// Resizes a layer.
            /// </summary>
            /// <param name="width">The new Width</param>
            /// <param name="height">The new Height</param>
            public void Resize(byte width, byte height)
            {
                // first take a backup of the current dimensions
                // then update the internal dimensions
                byte oldWidth = this.width;
                byte oldHeight = this.height;
                this.width = width;
                this.height = height;

                // resize the various scrolling and tile arrays
                System.Array.Resize(ref lineScroll, this.height * 128);
                System.Array.Resize(ref layout, this.height);

                // fill the extended tile arrays with "empty" values

                // if we're actaully getting shorter, do nothing!
                for (byte y = oldHeight; y < this.height; y++)
                {
                    // first create arrays child arrays to the old width
                    // a little inefficient, but at least they'll all be equal sized
                    layout[y] = new byte[oldWidth];
                    for (int x = 0; x < oldWidth; ++x)
                        layout[y][x] = 0; // fill the new ones with blanks
                }

                for (byte y = 0; y < this.height; y++)
                {
                    // now resize all child arrays to the new width
                    System.Array.Resize(ref layout[y], this.width);
                    for (ushort x = oldWidth; x < this.width; x++)
                        layout[y][x] = 0; // and fill with blanks if wider
                }
            }

        }

        /// <summary>
        /// A list of Horizontal Line Scroll Values
        /// </summary>
        public List<ScrollInfo> hScroll = new List<ScrollInfo>();
        /// <summary>
        /// A list of Vertical Line Scroll Values
        /// </summary>
        public List<ScrollInfo> vScroll = new List<ScrollInfo>();
        /// <summary>
        /// A list of Background layers
        /// </summary>
        public Layer[] layers = new Layer[8];

        public Backgrounds()
        {
            for (int i = 0; i < 8; ++i)
                layers[i] = new Layer();
        }

        public Backgrounds(string filename) : this(new Reader(filename)) { }

        public Backgrounds(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Backgrounds(Reader reader) : this()
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            byte layerCount = reader.ReadByte();
            byte hScrollCount = reader.ReadByte();

            vScroll.Clear();
            for (int i = 0; i < hScrollCount; i++)
                hScroll.Add(new ScrollInfo(reader));

            byte vScrollCount = reader.ReadByte();
            vScroll.Clear();
            for (int i = 0; i < vScrollCount; i++)
                vScroll.Add(new ScrollInfo(reader));

            for (int i = 0; i < 8; i++)
                layers[i] = new Layer();

            for (int i = 0; i < layerCount; i++)
                layers[i].Read(reader);

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // Too Lazy to do this properly, have 8 layers no matter what
            writer.Write((byte)8);

            writer.Write((byte)hScroll.Count);
            foreach (ScrollInfo hScrollInfo in hScroll)
                hScrollInfo.Write(writer);

            writer.Write((byte)vScroll.Count);
            foreach (ScrollInfo vScrollInfo in vScroll)
                vScrollInfo.Write(writer);

            foreach (Layer layer in layers)
                layer.Write(writer);

            writer.Close();
        }
    }
}
