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
        public int width, height = 0;
        public ushort[][] MapLayout { get; set; }

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

        byte layerCount;

        List<byte> Unknown = new List<byte>();
        List<PaletteColor> pal = new List<PaletteColor>();

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
            byte LineCount = reader.ReadByte();

            for (int i = 0; i < LineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                Lines.Add(p);
            }

            byte[] buffer = new byte[5];
            //reader.Read(displayBytes, 0, 5); //Waste 5 bytes, I don't care about them right now.
            //The first 4 bytes are loaded into StageSystem.ActiveTileLayers. 5th byte is tLayerMidPoint.
            //If you want to know the values then look at the values for "DisplayBytes"
            reader.Read(buffer, 0, 2); //Read BGMap size Width

            width = 0; height = 0;

            // Map width in 128 pixel units
            // In Sonic 1 it's two bytes long, little-endian
            width = buffer[0] + (buffer[1] << 8);
            reader.Read(buffer, 0, 2); //Read Height
            height = buffer[0] + (buffer[1] << 8);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //128x128 Block number is 16-bit
                    //Little-Endian in RSDKv4	
                    reader.Read(buffer, 0, 2); //Read size
                    MapLayout[y][x] = (ushort)(buffer[0] + (buffer[1] << 8));
                    Console.WriteLine(MapLayout[y][x]);
                }
            }

            /*while (!reader.IsEof)
            {
            }*/

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
            writer.Write(layerCount);
            writer.Write((byte)Lines.Count);

            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Write(writer);
            }

            for (int i = 0; i < Unknown.Count; i++)
            {
                writer.Write(Unknown[i]);
            }

        }

        public class ParallaxValues
        {
            byte LineNo;
            byte OverallSpeed;
            byte Value2;
            byte Value3;

            public ParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                OverallSpeed = reader.ReadByte();
                Value2 = reader.ReadByte();
                Value3 = reader.ReadByte();
                Console.WriteLine(LineNo + " " + OverallSpeed + " " + Value2 + " " + Value3);
            }

            public void Write(Writer writer)
            {
                writer.Write(LineNo);
                writer.Write(OverallSpeed);
                writer.Write(Value2);
                writer.Write(Value3);
            }

        }

    }
}
