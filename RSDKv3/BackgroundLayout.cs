using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    /* Background Layout */
    public class BGLayout
    {
        public int width, height = 0;

        byte Unknown1; //Has something to do with the layer shown, since most values cause a blank BG
        byte Unknown2; //makes the background do weird things
        byte RelativeVSpeed;
        byte ConstantVSpeed;

        public ushort[][] MapLayout { get; set; }

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

        public List<byte> UnknownValues = new List<byte>();

        byte UnknownValue;
        byte UnknownValue2;

        byte[] unknown = new byte[363];

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
            UnknownValue = reader.ReadByte();//destroys the background if above 9
            byte LineCount = reader.ReadByte();
            UnknownValue2 = reader.ReadByte();// No idea what it does, honestly...

            for (int i = 0; i < LineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                Lines.Add(p);
            }

            //These Bytes seem to tell the engine how to display the layer(s)
            width = reader.ReadByte();
            height = reader.ReadByte();
            Unknown1 = reader.ReadByte(); //Has something to do with the layer shown, since most values cause a blank BG
            Unknown2 = reader.ReadByte(); //makes the background do weird things
            RelativeVSpeed = reader.ReadByte();
            ConstantVSpeed = reader.ReadByte();

            //Read LineInfo

            unknown = reader.ReadBytes(363); //Dont know how to get this value yet :/

            //reader.ReadBytes(2); //always ends with "FF FF"

            MapLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                MapLayout[i] = new ushort[width];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 128x128 Block number is 16-bit
                    // Big-Endian in RSDKv2 and RSDKv3
                    byte[] buffer = new byte[5];
                    reader.Read(buffer, 0, 2); //Read size
                    MapLayout[y][x] = (ushort)(buffer[1] + (buffer[0] << 8));
                    //Console.WriteLine(MapLayout[y][x]);
                }
            }

            while (!reader.IsEof)
            {
                UnknownValues.Add(reader.ReadByte()); //I have no idea what these are for, so lets just store them so we can write them back later :)
            }

            //Read Line Positions

            //Read Tiles

            //Read Unknown Flags
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
            writer.Write(UnknownValue);
            writer.Write((byte)Lines.Count);
            writer.Write(UnknownValue2);

            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Write(writer);
            }

            writer.Write((byte)width);
            writer.Write((byte)height);
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(RelativeVSpeed);
            writer.Write(ConstantVSpeed);

            writer.Write(unknown);

            for (int h = 0; h < this.height; h++)
            {
                for (int w = 0; w < this.width; w++)
                {
                    writer.Write((byte)(MapLayout[h][w] >> 8));
                    writer.Write((byte)(MapLayout[h][w] & 0xff));
                }
            }

            for (int i = 0; i < UnknownValues.Count; i++)
            {
                writer.Write(UnknownValues[i]);
            }
            writer.Close();
        }

        public class ParallaxValues
        {
            public byte LineNo;
            public byte RelativeSpeed;
            public byte ConstantSpeed;
            public byte Unknown; //Flips when walking?
            /*
            CD has 3 Bytes called: 
            Parallax Factor,
            Scroll Speed and
            Scroll Pos
            */
            public ParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Unknown = reader.ReadByte();
                Console.WriteLine(LineNo + " " + RelativeSpeed + " " + ConstantSpeed + " " + Unknown);
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
