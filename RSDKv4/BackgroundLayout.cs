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
        public class UnknownValues
        {
            public byte Value1;
            public byte Value2;
            public byte Value3;

            public UnknownValues(Reader reader)
            {
                Value1 = reader.ReadByte();
                Value2 = reader.ReadByte();
                Value3 = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(Value1);
                writer.Write(Value2);
                writer.Write(Value3);
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

            public BGLayer(Reader reader)
            {
                byte[] buffer = new byte[2];
                Console.WriteLine(reader.Pos);
                reader.Read(buffer, 0, 2); //Read size
                width = (ushort)(buffer[0] + (buffer[1] << 8));

                reader.Read(buffer, 0, 2); //Read size
                height = (ushort)(buffer[0] + (buffer[1] << 8));

                reader.Read(buffer, 0, 2); //Read size
                Unknown1 = (ushort)(buffer[0] + (buffer[1] << 8));

                Unknown2 = reader.ReadByte();
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
                        MapLayout[y][x] = (ushort)(buffer[0] + (buffer[1] << 8));
                    }
                }
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)width);
                writer.Write((byte)height);
                writer.Write(Unknown1);
                writer.Write(Unknown2);
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
                        writer.Write((byte)(this.MapLayout[h][w] & 0xff));
                        writer.Write((byte)(this.MapLayout[h][w] >> 8));
                    }
                }

            }

        }

        public byte LayerCount;

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

        public List<BGLayer> Layers = new List<BGLayer>();

        public List<byte> Unknown = new List<byte>();

        public BGLayout(string filename) : this(new Reader(filename))
        {

        }

        public BGLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public BGLayout(Reader reader)
        {
            LayerCount = reader.ReadByte();
            byte LineCount = reader.ReadByte();

            for (int i = 0; i < LineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                Lines.Add(p);
            }

            for (int i = 0; i < LayerCount; i++)
            {
                Layers.Add(new BGLayer(reader));
            }

            //Read Unknown Flags
            while (!reader.IsEof)
            {
                Unknown.Add(reader.ReadByte()); //I have no idea what these are for, so lets just store them so we can write them back later :)
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
            writer.Write(LayerCount);
            writer.Write((byte)Lines.Count);

            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Write(writer);
            }

            for (int i = 0; i < LayerCount; i++)
            {
                Layers[i].Write(writer);
            }

            for (int i = 0; i < Unknown.Count; i++)
            {
                writer.Write(Unknown[i]);
            }
            writer.Close();
        }

        public class ParallaxValues
        {
            public byte LineNo;
            public byte RelativeSpeed;
            public byte ConstantSpeed;
            public byte Unknown; //Flips when walking?

            public ParallaxValues()
            {
                LineNo = 0;
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                Unknown = 0;
            }

            public ParallaxValues(Reader reader)
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

    }
}
