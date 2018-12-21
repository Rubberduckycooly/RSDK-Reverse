using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
{
    /* Background Layout */
    public class BGLayout
    {

        public class BGLayer
        {
            public ushort[][] MapLayout { get; set; }

            public int width, height = 0;
            public byte Deform;
            public byte RelativeVSpeed;
            public byte ConstantVSpeed;

            public List<byte> LineIndexes = new List<byte>();

            public BGLayer()
            {
                width = height = 1;
                Deform = RelativeVSpeed = ConstantVSpeed = 0;

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
                Deform = RelativeVSpeed = ConstantVSpeed = 0;

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
                RelativeVSpeed = reader.ReadByte();
                ConstantVSpeed = reader.ReadByte();

                //Console.WriteLine("Width = " + width + " Height = " + height + " Unknown 1 = " + Unknown1 + " Unknown 2 = " + Unknown2 + " Unknown 3 = " + Unknown3);

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
                writer.Write(RelativeVSpeed);
                writer.Write(ConstantVSpeed);

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

        public class ParallaxValues
        {
            public byte RHSpeed; //Known as "Speed" in Taxman's Editor
            public byte CHSpeed; //Known as "Scroll" in Taxman's Editor
            public byte Deform; //Known as "Deform" in Taxman's Editor, Same here!

            public ParallaxValues()
            {
                RHSpeed = 0;
                CHSpeed = 0;
                Deform = 0;
            }

            public ParallaxValues(byte r, byte c, byte d)
            {
                RHSpeed = r;
                CHSpeed = c;
                Deform = d;
            }

            public ParallaxValues(Reader reader)
            {
                RHSpeed = reader.ReadByte();
                CHSpeed = reader.ReadByte();
                Deform = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(RHSpeed);
                writer.Write(CHSpeed);
                writer.Write(Deform);
            }

        }

        public List<ParallaxValues> HLines = new List<ParallaxValues>();

        public List<ParallaxValues> VLines = new List<ParallaxValues>();

        public List<BGLayer> Layers = new List<BGLayer>();

        List<byte> Unknown = new List<byte>();

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

            for (int i = 0; i < HLineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                HLines.Add(p);
            }

            byte VLineCount = reader.ReadByte();

            for (int i = 0; i < VLineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                VLines.Add(p);
            }

            for (int i = 0; i < layerCount; i++) //Read BG Layers
            {
                Layers.Add(new BGLayer(reader));
            }

            Console.WriteLine("Max Pos " + reader.BaseStream.Length + ", cur pos " + reader.Pos);
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
            // Save Data
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
