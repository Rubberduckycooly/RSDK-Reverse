using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv4
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
            public ushort Unknown1; //Has something to do with the layer shown, since most values cause a blank BG
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
                byte[] buffer = new byte[2];

                reader.Read(buffer, 0, 2); //Read size
                width = (buffer[0] + (buffer[1] << 8));

                reader.Read(buffer, 0, 2); //Read size
                height = (buffer[0] + (buffer[1] << 8));

                Unknown1 = reader.ReadByte();

                Unknown2 = reader.ReadByte();

                RelativeVSpeed = reader.ReadByte();

                ConstantVSpeed = reader.ReadByte();

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
                    //else if (u2.Value1 != 255)
                    //{
                    //    u2.Value3 = reader.ReadByte();
                    //}

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

                writer.Write(Unknown1);
                writer.Write(Unknown2);
                writer.Write(RelativeVSpeed);
                writer.Write(ConstantVSpeed);

                Console.WriteLine(UnknownVals2.Count);

                for (int i = 0; i < UnknownVals2.Count; i++)
                {
                    writer.Write(UnknownVals2[i].Value1);

                    if (UnknownVals2[i].Value1 == 255)
                    {
                        writer.Write(UnknownVals2[i].Value2);

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
                        writer.Write((byte)(this.MapLayout[h][w] & 0xff));
                        writer.Write((byte)(this.MapLayout[h][w] >> 8));
                    }
                }

            }

        }

        public byte LayerCount;

        public List<HorizontalParallaxValues> HLines = new List<HorizontalParallaxValues>();

        public List<VerticalParallaxValues> VLines = new List<VerticalParallaxValues>();

        public List<BGLayer> Layers = new List<BGLayer>();

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

            for (int i = 0; i < LayerCount; i++)
            {
                Layers[i].Write(writer);
            }
            writer.Close();
        }

    }
}
