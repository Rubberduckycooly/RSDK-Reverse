using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv1
{
    public class Video
    {
        byte curframe = 0; //Used for reading & writing files 

        public List<VideoFrame> Frames = new List<VideoFrame>();

        public ushort VideoInfo;
        public ushort Width;
        public ushort Height;

        public class VideoFrame
        {
            ushort Width;
            ushort Height;

            public List<Color> FramePalette = new List<Color>();

            public VideoFrame()
            {

            }

            public VideoFrame(Reader reader, ushort width, ushort height)
            {
                Width = width;
                Height = height;

                uint FilePos = reader.ReadUInt32();
                Console.WriteLine("FilePos = " + FilePos);

                for (int i = 0; i < 128; i++)
                {
                    Color c;
                    byte r = reader.ReadByte(); //r
                    byte g = reader.ReadByte(); //g
                    byte b = reader.ReadByte(); //b
                    c = Color.FromArgb(255, r, g, b);
                    Console.WriteLine("Colour " + i + " = " + c.R + " " + c.G + " " + c.B);
                    FramePalette.Add(c);
                }

                List<byte> idkMan = new List<byte>();

                bool Next = false;

                while (!Next)
                {
                    byte tmp = reader.ReadByte();
                    idkMan.Add(tmp);
                    Console.WriteLine("idk lol = " + tmp);
                    if (tmp == 44) { Next = true; }
                }

                reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();

                byte tmp2 = reader.ReadByte();

                Console.WriteLine("pp (very hard) = " + tmp2);

                uint v5 = (uint)tmp2 << 25 >> 31;

                Console.WriteLine("Nani = " + v5);

                Console.WriteLine("Use full Palette = " + (tmp2 >> 7));

                if (tmp2 >> 7 == 1) // Use extra colours?
                {
                    for (int i = 128; i < 256; i++)
                    {
                        Color c;
                        byte r = reader.ReadByte(); //r
                        byte g = reader.ReadByte(); //g
                        byte b = reader.ReadByte(); //b
                        c = Color.FromArgb(255, r, g, b);
                        Console.WriteLine("Colour " + i + " = " + c.R + " " + c.G + " " + c.B);
                        FramePalette.Add(c);
                    }
                }

                LoadGIFData(reader);

            }

            public void LoadGIFData(Reader reader)
            {
                //I have no idea how to load GIF data
            }

        }

        public Video(string filepath) : this(new Reader(filepath))
        {

        }

        public Video(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Video(Reader reader)
        {
            VideoInfo = reader.ReadUInt16();

            Width = reader.ReadUInt16();

            Height = reader.ReadUInt16();

            Console.WriteLine("VideoInfo = " + VideoInfo);
            Console.WriteLine("Width = " + Width);
            Console.WriteLine("Height = " + Height);

            //for (int f = 0; f < VideoInfo; f++)
            //{
                LoadVideoFrame(reader);
            //}

            Console.WriteLine("Reader Position = " + reader.Pos + " FileSize = " + reader.BaseStream.Length);
            reader.Close();
        }

        void LoadVideoFrame(Reader reader)
        {
            Frames.Add(new VideoFrame(reader,Width,Height));
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
