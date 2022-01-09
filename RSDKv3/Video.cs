using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RSDKv3
{
    public class Video
    {
        /// <summary>
        /// the list of every frame
        /// </summary>
        public List<Gif> frames = new List<Gif>();

        /// <summary>
        /// How Wide the frames are (in pixels)
        /// </summary>
        public ushort width;
        /// <summary>
        /// how Tall the frames are (in pixels)
        /// </summary>
        public ushort height;

        public Video() { }

        public Video(string filepath) : this(new Reader(filepath)) { }

        public Video(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Video(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            ushort frameCount = reader.ReadUInt16();
            width = reader.ReadUInt16();
            height = reader.ReadUInt16();

            int videoFilePos = (int)reader.BaseStream.Position;
            frames.Clear();
            for (int f = 0; f < frameCount; f++)
            {
                int frameSize = reader.ReadInt32();

                Gif frame = new Gif();
                frame.width = width;
                frame.height = height;
                frame.read(reader, true, 0x80);

                frames.Add(frame);

                videoFilePos += frameSize;
                reader.BaseStream.Position = videoFilePos;
            }

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream reader)
        {
            using (Writer writer = new Writer(reader))
                write(writer);
        }

        public void write(Writer writer)
        {
            writer.Write((ushort)frames.Count);
            writer.Write(width);
            writer.Write(height);

            foreach (Gif frame in frames)
            {
                byte[] gifData;
                using (var gifStream = new System.IO.MemoryStream())
                {
                    Writer swriter = new Writer(gifStream);
                    frame.write(swriter, true, true);
                    gifData = gifStream.ToArray();
                }

                writer.Write((uint)gifData.Length + 4);
                writer.Write(gifData);
            }

            writer.Close();
        }

        public void import(string inputFolder)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(inputFolder);
            if (!dir.Exists)
                return;

            frames.Clear();
            int w = -1, h = -1;
            foreach (System.IO.FileInfo f in dir.GetFiles())
            {
                if (f.Extension == ".gif")
                {
                    Gif frame = new Gif(f.FullName);
                    if (w == -1)
                        w = width = frame.width;
                    else if (w != frame.width)
                        throw new Exception($"Frame sizes have to match! {f.Name} width did not match!");

                    if (h == -1)
                        h = height = frame.height;
                    else if (h != frame.height)
                        throw new Exception($"Frame sizes have to match! {f.Name} height did not match!");

                    frames.Add(frame);
                }
            }
        }

        public void export(string outputFolder)
        {
            int frameID = 0;
            foreach (Gif frame in frames)
                frame.write(outputFolder + $"Frame {(frameID++).ToString().PadLeft(6, '0')}.gif");
        }
    }
}
