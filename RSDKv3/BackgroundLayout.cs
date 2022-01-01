﻿using System;
using System.Collections.Generic;

namespace RSDKv3
{
    /* Background Layout */
    public class BGLayout
    {
        public class ScrollInfo
        {
            /// <summary>
            /// how fast the line moves while the player is moving
            /// </summary>
            public short RelativeSpeed;
            /// <summary>
            /// How fast the line moves without the player moving
            /// </summary>
            public byte ConstantSpeed;
            /// <summary>
            /// a special byte that tells the game what "behaviour" property the layer has
            /// </summary>
            public byte Behaviour;

            public ScrollInfo()
            {
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                Behaviour = 0;
            }

            public ScrollInfo(short r, byte c, byte b)
            {
                RelativeSpeed = r;
                ConstantSpeed = c;
                Behaviour = b;
            }

            public ScrollInfo(Reader reader)
            {
                // 2 bytes, big-endian, unsigned
                RelativeSpeed = (short)(reader.ReadByte() << 8);
                RelativeSpeed |= (short)reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Behaviour = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)(RelativeSpeed >> 8));
                writer.Write((byte)(RelativeSpeed & 0xFF));
                writer.Write(ConstantSpeed);
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
            public byte width = 0;
            /// <summary>
            /// Layer Height
            /// </summary>
            public byte height = 0;
            /// <summary>
            /// a special byte that tells the game what "behaviour" property the layer has
            /// </summary>
            public byte Behaviour;
            /// <summary>
            /// how fast the Layer moves while the player is moving
            /// </summary>
            public short RelativeSpeed;
            /// <summary>
            /// how fast the layer moves while the player isn't moving
            /// </summary>
            public byte ConstantSpeed;
            /// <summary>
            /// a list of Line positions
            /// </summary>
            public byte[] LineIndexes;

            public BGLayer()
            {
                width = height = 1;
                Behaviour = 0;
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
                Behaviour = 0;
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
                Behaviour = reader.ReadByte();
                RelativeSpeed = (short)(reader.ReadByte() << 8);
                RelativeSpeed |= (short)reader.ReadByte();
                ConstantSpeed = reader.ReadByte();

                byte[] buf = new byte[3];
                bool finished = false;
                int cnt = 0;
                int loop = 0;

                LineIndexes = new byte[height * 128];

                while (!finished)
                {
                    buf[0] = reader.ReadByte();
                    if (buf[0] == 0xFF)
                    {
                        buf[1] = reader.ReadByte();
                        if (buf[1] == 0xFF)
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

                byte[] buffer = new byte[2];

                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        reader.Read(buffer, 0, 2); //Read size
                        MapLayout[y][x] = (ushort)(buffer[1] + (buffer[0] << 8));
                    }
                }
            }

            public void Write(Writer writer)
            {
                writer.Write(width);
                writer.Write(height);
                writer.Write(Behaviour);
                writer.Write((byte)(RelativeSpeed >> 8));
                writer.Write((byte)(RelativeSpeed & 0xFF));
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
                        writer.Write((byte)(MapLayout[h][w] >> 8));
                        writer.Write((byte)(MapLayout[h][w] & 0xFF));
                    }
                }

            }

            private static void rle_write(Writer file, int value, int count)
            {
                if (count <= 2)
                {
                    for (int y = 0; y < count; y++)
                    {
                        file.Write((byte)value);
                    }
                }
                else
                {
                    while (count > 0)
                    {
                        file.Write((byte)0xFF);
                        file.Write((byte)value);
                        file.Write((byte)((count > 253) ? 254 : (count + 1)));
                        count -= 253;
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
            writer.Write((byte)Layers.Count);
            writer.Write((byte)HLines.Count);
            writer.Write((byte)VLines.Count);

            for (int i = 0; i < HLines.Count; i++)
            {
                HLines[i].Write(writer);
            }

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
