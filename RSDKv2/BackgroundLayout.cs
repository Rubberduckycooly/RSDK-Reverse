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
        public class HorizontalParallaxValues
        {
            public byte LineNo;
            public byte RelativeSpeed;
            public byte ConstantSpeed;
            public byte Unknown; //Flips when walking?

            public HorizontalParallaxValues()
            {
                LineNo = 0;
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                Unknown = 0;
            }

            public HorizontalParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Unknown = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(LineNo);
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(Unknown);
            }

        }

        public class VerticalParallaxValues
        {
            public byte LineNo;
            public byte RelativeSpeed;
            public byte ConstantSpeed;
            public byte Unknown; //Flips when walking?

            public VerticalParallaxValues()
            {
                LineNo = 0;
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                Unknown = 0;
            }

            public VerticalParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Unknown = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(LineNo);
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(Unknown);
            }

        }

        public class BGLayer
        {

            public class UnknownValues2
            {
                public byte Value1;
                public byte Value2;
                public byte Value3;
            }

            public ushort[][] MapLayout { get; set; }

            public int width, height = 0;
            public byte Unknown1; //Has something to do with the layer shown, since most values cause a blank BG
            public byte Unknown2; //makes the background do weird things
            public byte RelativeVSpeed;
            public byte ConstantVSpeed;

            public List<UnknownValues2> UnknownVals2 = new List<UnknownValues2>();

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
                    UnknownValues2 u2 = new UnknownValues2();
                    u2.Value1 = reader.ReadByte();

                    if (u2.Value1 == 255)
                    {
                        u2.Value2 = reader.ReadByte();

                        if (u2.Value2 == 255)
                        {
                            j = 1;
                        }
                        else
                        {
                            u2.Value3 = reader.ReadByte();
                        }
                    }
                    else if (u2.Value1 != 255)
                    {
                        u2.Value3 = reader.ReadByte();
                    }
                    UnknownVals2.Add(u2);
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

                Console.WriteLine(UnknownVals2.Count);

                for (int i = 0; i < UnknownVals2.Count; i++)
                {
                    writer.Write(UnknownVals2[i].Value1);
                    Console.WriteLine(UnknownVals2[i].Value1);

                    if (UnknownVals2[i].Value1 == 255)
                    {
                        writer.Write(UnknownVals2[i].Value2);
                        Console.WriteLine(UnknownVals2[i].Value2);

                        if (UnknownVals2[i].Value1 == 255 && UnknownVals2[i].Value2 == 255)
                        {
                            break;
                        }

                        if (UnknownVals2[i].Value2 != 255)
                        {
                            writer.Write(UnknownVals2[i].Value3);
                        }
                    }
                    else
                    {
                        writer.Write(UnknownVals2[i].Value3);
                    }
                }

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

        byte LayerCount;

        public List<HorizontalParallaxValues> HLines = new List<HorizontalParallaxValues>();

        public List<VerticalParallaxValues> VLines = new List<VerticalParallaxValues>();

        public List<BGLayer> Layers = new List<BGLayer>();

        //RSDKv2 & RSDKvB have different background layouts
        //NOTES:
        //Byte 1 are the number of layers
        //byte 2 is the amount of lines
        //then there seem to be line values
        //then there are some "Display bytes" that control how the layers are shown
        //then some kind of flags that control things like deformations
        //then Line Positions
        //then tiles
        //then there are more Unknown flags at the end...

        public BGLayout(string filename) : this(new Reader(filename))
        {

        }

        public BGLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public BGLayout(Reader reader)
        {
            LayerCount = reader.ReadByte();

            byte HLineCount = reader.ReadByte();
            byte VLineCount = reader.ReadByte();

            for (int i = 0; i < HLineCount; i++)
            {
                HorizontalParallaxValues p = new HorizontalParallaxValues(reader);
                HLines.Add(p);
            }

            for (int i = 0; i < VLineCount; i++)
            {
                VerticalParallaxValues p = new VerticalParallaxValues(reader);
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
