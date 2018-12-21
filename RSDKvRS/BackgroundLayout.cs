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
            public ushort[][] MapLayout { get; set; }

            public int width, height = 0;
            public byte Deform; //1 for parallax, 2 for no parallax, 3 for "3D Sky", anything else for nothing
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
                HLines.Add(new ParallaxValues(reader));
            }

            byte VLineCount = reader.ReadByte();

            //Then the Vertical Parallax vallues
            for (int lc = 0; lc < VLineCount; lc++)
            {
                VLines.Add(new ParallaxValues(reader));
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
