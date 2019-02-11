using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

/* Taxman sure loves leaving support for fileformats in his code */

namespace RSDKv2
{
    public class Video
    {

        /// <summary>
        /// the list of every frame
        /// </summary>
        public List<VideoFrame> Frames = new List<VideoFrame>();

        /// <summary>
        /// How many frames the video contains
        /// </summary>
        public ushort VideoInfo;
        /// <summary>
        /// How Wide the frames are (in pixels)
        /// </summary>
        public ushort Width;
        /// <summary>
        /// how Tall the frames are (in pixels)
        /// </summary>
        public ushort Height;

        public class VideoFrame
        {
            /// <summary>
            /// How Wide this frame is (in pixels)
            /// </summary>
            ushort Width;
            /// <summary>
            /// how Tall this frame is (in pixels)
            /// </summary>
            ushort Height;

            /// <summary>
            /// the file offset to the next file(?)
            /// </summary>
            uint FilePos;

            /// <summary>
            /// extra bytes until "," is found
            /// </summary>
            List<byte> idkMan = new List<byte>();

            //GIF VALUES
            /// <summary>
            /// ImageLeft value (from the gif format, ignored by RSDK)
            /// </summary>
            ushort ImageLeft;
            /// <summary>
            /// ImageTop value (from the gif format, ignored by RSDK)
            /// </summary>
            ushort ImageTop;
            /// <summary>
            /// ImageWidth value, should be the same as Main Width (from the gif format, ignored by RSDK)
            /// </summary>
            ushort ImageWidth;
            /// <summary>
            /// ImageHeight value, should be the same as Main Height (from the gif format, ignored by RSDK)
            /// </summary>
            ushort ImageHeight;
            /// <summary>
            /// whether the image is loaded via interlacing or not
            /// </summary>
            uint isInterlaced;
            /// <summary>
            /// various image flags
            /// </summary>
            byte PaletteType;
            /// <summary>
            /// whether or not the image has 128 or 256 colours
            /// </summary>
            bool FullPallete;

            /// <summary>
            /// the raw image data
            /// </summary>
            public byte[] ImageData;
            /// <summary>
            /// the raw (compressed) image data
            /// </summary>
            public byte[] CompressedImageData;
            int dataptr = 0;

            bool ExtendedCodeTable = false;

            /// <summary>
            /// the image codesize (used for image loading)
            /// </summary>
            int codesize
            {
                get
                {
                    if (ExtendedCodeTable)
                    {
                        return 8;
                    }
                    else
                    {
                        return 7;
                    }
                }
            }
            /// <summary>
            /// the image clearcode (used for image loading)
            /// </summary>
            int ClearCode
            {
                get
                {
                    if (ExtendedCodeTable)
                    {
                        return 256;
                    }
                    else
                    {
                        return 128;
                    }
                }
            }
            /// <summary>
            /// the image endcode (used for image loading)
            /// </summary>
            int EndCode
            {
                get
                {
                    if (ExtendedCodeTable)
                    {
                        return 257;
                    }
                    else
                    {
                        return 129;
                    }
                }
            }

            /// <summary>
            /// the palette for this frame
            /// </summary>
            public List<Color> FramePalette = new List<Color>();

            int startOffset = 0;
            int endOffset = 0;

            public VideoFrame()
            {

            }

            public VideoFrame(ushort width, ushort height)
            {
                Width = width;
                Height = height;

                ImageData = new byte[Width * Height];
            }

            public VideoFrame(Reader reader, ushort width, ushort height)
            {
                Width = width;
                Height = height;
                startOffset = (int)reader.Pos;
                ImageData = new byte[Width * Height];

                FilePos = (uint)(reader.ReadByte() + (reader.ReadByte() << 8) + (reader.ReadByte() << 16) + (reader.ReadByte() << 24));//reader.ReadUInt32();

                for (int i = 0; i < 128; i++)
                {
                    Color c;
                    byte r = reader.ReadByte(); //r
                    byte g = reader.ReadByte(); //g
                    byte b = reader.ReadByte(); //b
                    c = Color.FromArgb(255, r, g, b);
                    //Console.WriteLine("Colour " + i + " = " + c.R + " " + c.G + " " + c.B);
                    FramePalette.Add(c);
                }

                bool Next = false;

                while (!Next)
                {
                    byte tmp = reader.ReadByte();
                    idkMan.Add(tmp);
                    //Console.WriteLine("idk lol = " + tmp);
                    if (tmp == 44) { Next = true; } // AKA ','
                }

                ImageLeft = reader.ReadUInt16();
                ImageTop = reader.ReadUInt16();
                ImageWidth = reader.ReadUInt16();
                ImageHeight = reader.ReadUInt16();

                PaletteType = reader.ReadByte();

                //Console.WriteLine("Palette Type = " + PaletteType);

                isInterlaced = (uint)PaletteType << 25 >> 31;

                //Console.WriteLine("Interlaced? = " + isInterlaced);

                //Console.WriteLine("Use full Palette = " + (PaletteType >> 7));

                FullPallete = PaletteType >> 7 == 1;

                if (FullPallete) // Use extra colours?
                {
                    for (int i = 128; i < 256; i++)
                    {
                        Color c;
                        byte r = reader.ReadByte(); //r
                        byte g = reader.ReadByte(); //g
                        byte b = reader.ReadByte(); //b
                        c = Color.FromArgb(255, r, g, b);
                        //Console.WriteLine("(Extra) Colour " + i + " = " + c.R + "," + c.G + "," + c.B);
                        FramePalette.Add(c);
                    }
                }

                ReadGIFData(reader);
                Console.WriteLine("Loaded Video Frame!");
                //reader.BaseStream.Position = FilePos + 6;
                endOffset = (int)reader.Pos;
            }


            void outLine(byte[] buff)
            {
                for (int i = 0; i < Width; i++)
                {
                    ImageData[dataptr++] = buff[i];
                }
            }

            public void ReadGIFData(Reader reader)
            {
                //I have no idea how to load GIF data

                CompressedImageData = new byte[Width * Height + 1];

                byte bitSize = reader.ReadByte();
                ExtendedCodeTable = bitSize == 8;

                bool notEnd = true;

                int c = 0;

                while (notEnd)
                {
                    byte BlockSize = reader.ReadByte();
                    byte clearCode = reader.ReadByte(); //just read the clearcode

                    if (BlockSize == 0)
                    {
                        break;
                    }

                    if (!ExtendedCodeTable)
                    {

                        while ((c = reader.ReadByte()) != EndCode)
                        {

                            if (c == ClearCode)
                            {
                                //do clearcode shit

                                while ((c = reader.ReadByte()) == ClearCode)
                                {
                                    //skip
                                }

                                if (c == EndCode)
                                {
                                    notEnd = false;
                                    break;
                                }

                            }
                            else
                            {
                                //Process data
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("wtf no");
                        break;
                        //gonna haveta do special stuff for 256 colour images
                    }
                }
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

            //GET IMAGE DATA LOADING
            for (int f = 0; f < VideoInfo; f++)
            {
                LoadVideoFrame(reader);
            }

            Console.WriteLine("Reader Position = " + reader.Pos + " FileSize = " + reader.BaseStream.Length + " Data Left = " + (reader.BaseStream.Length - reader.Pos));
            reader.Close();
        }

        void LoadVideoFrame(Reader reader)
        {
            Frames.Add(new VideoFrame(reader, Width, Height));
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
