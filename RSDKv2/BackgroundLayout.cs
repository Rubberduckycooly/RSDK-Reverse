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

        public ushort[][] MapLayout { get; set; }

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

        byte layerCount;

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
            byte LineCount = reader.ReadByte();

            for (int i = 0; i < LineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                Lines.Add(p);
            }

            while (!reader.IsEof)
            {
                Unknown.Add(reader.ReadByte());
            }

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

            public ParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                OverallSpeed = reader.ReadByte();
                Value2 = reader.ReadByte();
                Console.WriteLine(LineNo + " " + OverallSpeed + " " + Value2);
            }

            public void Write(Writer writer)
            {
                writer.Write(LineNo);
                writer.Write(OverallSpeed);
                writer.Write(Value2);
            }

        }

    }
}
