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

            public class UnknownValues2
            {
                public byte Value1;
                public byte Value2;
                public byte Value3;
            }

            public ushort[][] MapLayout { get; set; }

            public int width, height = 0;
            public byte Deform;
            public byte RelativeVSpeed;
            public byte ConstantVSpeed;

            public List<UnknownValues2> UnknownVals2 = new List<UnknownValues2>();

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
                Console.WriteLine(reader.Pos);
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
                        //Console.WriteLine("Map = " + MapLayout[y][x]);
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
                        writer.Write((byte)(MapLayout[h][w]));
                    }
                }

            }

        }

        public class HorizontalParallaxValues
        {
            public byte RHSpeed; //Known as "Speed" in Taxman's Editor
            public byte CHSpeed; //Known as "Scroll" in Taxman's Editor
            public byte Deform; //Known as "Deform" in Taxman's Editor, Same here!

            public HorizontalParallaxValues()
            {
                RHSpeed = 0;
                CHSpeed = 0;
                Deform = 0;
            }

            public HorizontalParallaxValues(Reader reader)
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

        public class VerticalParallaxValues
        {
            public byte RHSpeed; //Known as "Speed" in Taxman's Editor
            public byte CHSpeed; //Known as "Scroll" in Taxman's Editor
            public byte Deform; //Known as "Deform" in Taxman's Editor, Same here!

            public VerticalParallaxValues()
            {
                RHSpeed = 0;
                CHSpeed = 0;
                Deform = 0;
            }

            public VerticalParallaxValues(Reader reader)
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

        public byte layerCount;

        public List<HorizontalParallaxValues> HLines = new List<HorizontalParallaxValues>();

        public List<VerticalParallaxValues> VLines = new List<VerticalParallaxValues>();

        public List<BGLayer> Layers = new List<BGLayer>();

        List<byte> Unknown = new List<byte>();

        //NOTES:
        //Byte 1 are the number of layers
        //byte 2 is the amount of lines
        //then there seem to be line values
        //then IDK

        public BGLayout(string filename) : this(new Reader(filename))
        {

        }

        public BGLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public BGLayout(Reader reader)
        {
            layerCount = reader.ReadByte();

            byte HLineCount = reader.ReadByte();

            for (int i = 0; i < HLineCount; i++)
            {
                HorizontalParallaxValues p = new HorizontalParallaxValues(reader);
                HLines.Add(p);
            }

            byte VLineCount = reader.ReadByte();

            for (int i = 0; i < VLineCount; i++)
            {
                VerticalParallaxValues p = new VerticalParallaxValues(reader);
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

            for (int i = 0; i < layerCount; i++)
            {
                Layers[i].Write(writer);
            }

            writer.Close();

        }
    }
}
