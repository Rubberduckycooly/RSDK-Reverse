using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv2
{
    class rsv
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

        public rsv(string filepath) : this(new Reader(filepath))
        {

        }

        public rsv(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public rsv(Reader reader)
        {
            int v4;

            v4 = reader.ReadByte();
            FrameCount = (byte)v4;
            v4 = reader.ReadByte();
            FrameCount += (byte)v4 << 8;
            v4 = reader.ReadByte();
            Width = (byte)v4;
            v4 = reader.ReadByte();
            Width += (byte)v4 << 8;
            v4 = reader.ReadByte();
            Height = (byte)v4;
            v4 = reader.ReadByte();
            Height += (byte)v4 << 8;

            for (int i = 0; i < FrameCount; i++)
            {
                LoadVideoFrame(reader);
            }
            reader.Close();
        }

        void LoadVideoFrame(Reader reader)
        {
            int v4;

            int Unknown1;

            v4 = reader.ReadByte();
            Unknown1 = (byte)v4;
            v4 = reader.ReadByte();
            Unknown1 += (byte)v4 << 8;
            v4 = reader.ReadByte();
            Unknown1 += (byte)v4 << 16;
            v4 = reader.ReadByte();
            Unknown1 += (byte)v4 << 24;

            for (int u = 0; u < 128; ++u)
            {
                UnknownFrameVals ufv = new UnknownFrameVals();
                v4 = reader.ReadByte();
                //ufv.Unknown1 = (byte)v4;
                //ufv.Unknown2 = (byte)(v4 >> 8 << 8);
                //ufv.Unknown3 = (byte)(v4 >> 8);
                //ufv.Unknown4 = (byte)v4 + (BYTE1(v4) << 8) + ((byte)v4 << 16);
                //ufv.Unknown5 = ((int)BYTE2(v4) >> 3) | 32 * ((int)BYTE1(v4) >> 2) | ((ushort)((byte)v4 >> 3) << 11);
                Frames[curframe].Unknown.Add(ufv);
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
