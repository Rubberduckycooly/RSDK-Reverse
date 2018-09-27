using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

/* Taxman sure loves leaving support for fileformats in his code */

namespace RSDKv2
{
    public class Video
    {

        class UnknownFrameVals
        {
            public byte Unknown1;
            public byte Unknown2;
            public byte Unknown3;
            public int Unknown4;
            public short Unknown5;
        }
        class VideoFrame
        {

            public List<Bitmap> Display = new List<Bitmap>();
            public List<UnknownFrameVals> Unknown = new List<UnknownFrameVals>();
        }

        byte curframe = 0; //Used for reading & writing files 

        List<VideoFrame> Frames = new List<VideoFrame>();

        public int Width, Height, FrameCount;

        public Video(string filepath) : this(new Reader(filepath))
        {

        }

        public Video(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Video(Reader reader)
        {
            int v4;

            byte[] buffer = new byte[2];

            reader.Read(buffer, 0, 2);
            FrameCount = buffer[0] + (buffer[1] << 8);

            reader.Read(buffer, 0, 2);
            Width = buffer[0] + (buffer[1] << 8);

            reader.Read(buffer, 0, 2);
            Height = buffer[0] + (buffer[1] << 8);

            Console.WriteLine("Frame Count = " + FrameCount);
            Console.WriteLine("Video Width = " + Width);
            Console.WriteLine("Video Height = " + Height);

            for (int i = 0; i < FrameCount; i++)
            {
                LoadVideoFrame(reader,curframe);
            }
            Console.WriteLine(reader.Pos + " " + reader.BaseStream.Length);
            reader.Close();
        }

        void LoadVideoFrame(Reader reader,int curFrame)
        {
            Frames.Add(new VideoFrame());
            int v4;

            int Unknown1;

            byte[] buffer = new byte[5];

            reader.Read(buffer, 0, 4);

            Unknown1 = buffer[0] + (buffer[1] << 8) + (buffer[2] << 16) + (buffer[3] << 24);

            //Console.WriteLine(Unknown1);

            for (int u = 0; u < 128; ++u)
            {
                UnknownFrameVals ufv = new UnknownFrameVals();
                buffer = reader.ReadBytes(3);
                ufv.Unknown1 = buffer[0];
                ufv.Unknown2 = (byte)(buffer[1] >> 8 << 8);
                ufv.Unknown3 = (byte)(buffer[1] >> 8);
                ufv.Unknown4 = buffer[1] + (buffer[1] << 8) + (buffer[2] << 16);
                ufv.Unknown5 = (short)((buffer[0] >> 3) | 32 * ((int)buffer[0] >> 2) | ((ushort)((byte)buffer[0] >> 3) << 11));
                Frames[curFrame].Unknown.Add(ufv);
                //Console.WriteLine("Unknown 1 = " + ufv.Unknown1 + ", Unknown 2 = " + ufv.Unknown2 + ", Unknown 3 = " + ufv.Unknown3 + ", Unknown 4 = " + ufv.Unknown4 + ", Unknown 5 = " + ufv.Unknown5);
            }

            v4 = reader.ReadByte();

            while (v4 != 44)
            {
                v4 = reader.ReadByte();
            }

            if (v4 == 44)
            {
                v4 = reader.ReadByte();
                v4 = reader.ReadByte();
                v4 = reader.ReadByte();
                v4 = reader.ReadByte();
                v4 = reader.ReadByte();

                int v1 = (v4 & 0x40) >> 6;
                int v3 = v1;
                if ((v4 & 0x80) >> 7 == 1)
                {
                    for (int i = 128; i < 256; ++i)
                        reader.ReadBytes(3);
                }
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

            writer.Close();
        }

    }
}
