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
        //int width, height = 0;

        public ushort[][] MapLayout { get; set; }

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

        public List<ParallaxValues> Lines2 = new List<ParallaxValues>();

        byte UnknownValue;

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
            reader.ReadByte();// No idea what it does, honestly...

            for (int i = 0; i < LineCount; i++)
            {
                ParallaxValues p = new ParallaxValues(reader);
                Lines.Add(p);
            }

            //These Bytes seem to tell the engine how to display the layer(s)
            byte LineValCount = reader.ReadByte(); //Seems to load incorrect chunks when the value is changed, Might be the number of upcoming line values
            byte Unknown1 = reader.ReadByte(); //Seems to offset the camera (and lock it)...
            byte Unknown2 = reader.ReadByte(); //Has something to do with the layer shown, since most values cause a blank BG
            byte Unknown3 = reader.ReadByte(); //makes the background do weird things
            byte RelativeVSpeed = reader.ReadByte();
            byte ConstantVSpeed = reader.ReadByte();

            //Read LineInfo

            //Read Line Positions

            //Read Tiles

            //Read Unknown Flags

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

            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Write(writer);
            }

        }

        public class ParallaxValues
        {
            byte LineNo;
            byte RelativeSpeed;
            byte ConstantSpeed;
            byte Flips; //Flips when walking?

            public ParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                RelativeSpeed = reader.ReadByte();
                ConstantSpeed = reader.ReadByte();
                Flips = reader.ReadByte();
                Console.WriteLine(LineNo + " " + RelativeSpeed + " " + ConstantSpeed + " " + Flips);
            }

            public void Write(Writer writer)
            {
                writer.Write(LineNo);
                writer.Write(RelativeSpeed);
                writer.Write(ConstantSpeed);
                writer.Write(Flips);
            }

        }

        public class LineInfo
        {

        }



    }
}
