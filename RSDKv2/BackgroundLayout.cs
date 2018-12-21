using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    /* Background Layout */
    public class BGLayout
    {
        public class ParallaxValues
        {
            public byte RelativeSpeed;
            public byte ConstantSpeed;
            public byte Unknown1;
            public byte Unknown; //Flips when walking?

            public ParallaxValues()
            {
                Unknown1 = 0;
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                Unknown = 0;
            }

            public ParallaxValues(byte r, byte c, byte u1, byte u2)
            {
                Unknown1 = u1;
                RelativeSpeed = r;
                ConstantSpeed = c;
                Unknown = u2;
            }

            public ParallaxValues(Reader reader)
            {
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Unknown1 = reader.ReadByte();
                Unknown = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(Unknown1);
                writer.Write(Unknown);
            }

        }

        public class BGLayer
        {

            public ushort[][] MapLayout { get; set; }

            public int width, height = 0;
            public byte Unknown1; //Has something to do with the layer shown, since most values cause a blank BG
            public byte Unknown2; //makes the background do weird things
            public byte RelativeVSpeed;
            public byte ConstantVSpeed;

            public List<byte> LineIndexes = new List<byte>();

            public BGLayer()
            {
                width = height = 1;
                Unknown1 = Unknown2 = RelativeVSpeed = ConstantVSpeed = 0;

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
                Unknown1 = Unknown2 = RelativeVSpeed = ConstantVSpeed = 0;

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
                RelativeVSpeed = reader.ReadByte();
                ConstantVSpeed = reader.ReadByte();
                Unknown1 = reader.ReadByte();
                Unknown2 = reader.ReadByte();

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
                writer.Write((byte)width);
                writer.Write((byte)height);
                writer.Write(RelativeVSpeed);
                writer.Write(ConstantVSpeed);
                writer.Write(Unknown1);
                writer.Write(Unknown2);

                for (int i = 0; i < LineIndexes.Count; i++)
                {
                    writer.Write(LineIndexes[i]);
                }
                writer.Write(0xFF);

                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        writer.Write((byte)(MapLayout[h][w] >> 8));
                        writer.Write((byte)(MapLayout[h][w] & 0xff));
                    }
                }

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
            byte LayerCount = reader.ReadByte();

            byte HLineCount = reader.ReadByte();
            byte VLineCount = reader.ReadByte();

            for (int i = 0; i < HLineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                HLines.Add(p);
            }

            for (int i = 0; i < VLineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
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
