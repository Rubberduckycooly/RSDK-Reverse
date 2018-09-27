using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv1
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
            VideoFrame vf = new VideoFrame();

            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();


            reader.ReadBytes(3);

            int tmp = reader.ReadByte();

            while (tmp != 44)
            {
                reader.ReadByte();
                reader.ReadBytes(2);
                reader.ReadBytes(2);
                reader.ReadBytes(2);
                reader.ReadBytes(2);
                reader.ReadByte();
            }

            Frames.Add(vf);
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
