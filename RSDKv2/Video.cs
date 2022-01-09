using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RSDKv2
{
    public class Video
    {
        // GifWriter from: https://stackoverflow.com/questions/1196322/how-to-create-an-animated-gif-in-net

        /// <summary>
        /// Creates a GIF using .Net GIF encoding and additional animation headers.
        /// </summary>
        public class GifWriter : IDisposable
        {
            #region Fields
            const long SourceGlobalColorInfoPosition = 10,
                SourceImageBlockPosition = 789;

            readonly Writer _writer;
            bool _firstFrame = true;
            readonly object _syncLock = new object();
            #endregion

            /// <summary>
            /// Creates a new instance of GifWriter.
            /// </summary>
            /// <param name="OutStream">The <see cref="Stream"/> to output the Gif to.</param>
            /// <param name="DefaultFrameDelay">Default Delay between consecutive frames... FrameRate = 1000 / DefaultFrameDelay.</param>
            /// <param name="Repeat">No of times the Gif should repeat... -1 not to repeat, 0 to repeat indefinitely.</param>
            public GifWriter(Stream OutStream, int DefaultFrameDelay = 500, int Repeat = -1)
            {
                if (OutStream == null)
                    throw new ArgumentNullException(nameof(OutStream));

                if (DefaultFrameDelay <= 0)
                    throw new ArgumentOutOfRangeException(nameof(DefaultFrameDelay));

                if (Repeat < -1)
                    throw new ArgumentOutOfRangeException(nameof(Repeat));

                _writer = new Writer(OutStream);
                this.DefaultFrameDelay = DefaultFrameDelay;
                this.Repeat = Repeat;
            }

            /// <summary>
            /// Creates a new instance of GifWriter.
            /// </summary>
            /// <param name="FileName">The path to the file to output the Gif to.</param>
            /// <param name="DefaultFrameDelay">Default Delay between consecutive frames... FrameRate = 1000 / DefaultFrameDelay.</param>
            /// <param name="Repeat">No of times the Gif should repeat... -1 not to repeat, 0 to repeat indefinitely.</param>
            public GifWriter(string FileName, int DefaultFrameDelay = 500, int Repeat = -1)
                : this(new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read), DefaultFrameDelay, Repeat) { }

            #region Properties
            /// <summary>
            /// Gets or Sets the Default Width of a Frame. Used when unspecified.
            /// </summary>
            public int DefaultWidth { get; set; }

            /// <summary>
            /// Gets or Sets the Default Height of a Frame. Used when unspecified.
            /// </summary>
            public int DefaultHeight { get; set; }

            /// <summary>
            /// Gets or Sets the Default Delay in Milliseconds.
            /// </summary>
            public int DefaultFrameDelay { get; set; }

            /// <summary>
            /// The Number of Times the Animation must repeat.
            /// -1 indicates no repeat. 0 indicates repeat indefinitely
            /// </summary>
            public int Repeat { get; }
            #endregion

            /// <summary>
            /// Adds a frame to this animation.
            /// </summary>
            /// <param name="Image">The image to add</param>
            /// <param name="Delay">Delay in Milliseconds between this and last frame... 0 = <see cref="DefaultFrameDelay"/></param>
            public void WriteFrame(Image Image, int Delay = 0)
            {
                lock (_syncLock)
                    using (var gifStream = new MemoryStream())
                    {
                        Image.Save(gifStream, ImageFormat.Gif);

                        // Steal the global color table info
                        if (_firstFrame)
                            InitHeader(gifStream, _writer, Image.Width, Image.Height);

                        WriteGraphicControlBlock(gifStream, _writer, Delay == 0 ? DefaultFrameDelay : Delay);
                        WriteImageBlock(gifStream, _writer, !_firstFrame, 0, 0, Image.Width, Image.Height);
                    }

                if (_firstFrame)
                    _firstFrame = false;
            }

#region Write
            void InitHeader(Stream SourceGif, Writer Writer, int Width, int Height)
            {
                // File Header
                Writer.Write("GIF".ToCharArray()); // File type
                Writer.Write("89a".ToCharArray()); // File Version

                Writer.Write((short)(DefaultWidth == 0 ? Width : DefaultWidth)); // Initial Logical Width
                Writer.Write((short)(DefaultHeight == 0 ? Height : DefaultHeight)); // Initial Logical Height

                SourceGif.Position = SourceGlobalColorInfoPosition;
                Writer.Write((byte)SourceGif.ReadByte()); // Global Color Table Info
                Writer.Write((byte)0); // Background Color Index
                Writer.Write((byte)0); // Pixel aspect ratio
                WriteColorTable(SourceGif, Writer);

                // App Extension Header for Repeating
                if (Repeat == -1)
                    return;

                Writer.Write(unchecked((short)0xFF21)); // Application Extension Block Identifier
                Writer.Write((byte)0x0b); // Application Block Size
                Writer.Write("NETSCAPE2.0".ToCharArray()); // Application Identifier
                Writer.Write((byte)3); // Application block length
                Writer.Write((byte)1);
                Writer.Write((short)Repeat); // Repeat count for images.
                Writer.Write((byte)0); // terminator
            }

            static void WriteColorTable(Stream SourceGif, BinaryWriter Writer, byte paletteStartPos = 0)
            {
                SourceGif.Position = 13 + (paletteStartPos * 3); // Locating the image color table
                var colorTable = new byte[0x80 * 3];
                SourceGif.Read(colorTable, 0, colorTable.Length);
                Writer.Write(colorTable, 0, colorTable.Length);
            }

            static void WriteGraphicControlBlock(Stream SourceGif, BinaryWriter Writer, int FrameDelay)
            {
                SourceGif.Position = 781; // Locating the source GCE
                var blockhead = new byte[8];
                SourceGif.Read(blockhead, 0, blockhead.Length); // Reading source GCE

                Writer.Write(unchecked((short)0xF921)); // Identifier
                Writer.Write((byte)0x04); // Block Size
                Writer.Write((byte)(blockhead[3] & 0xF7 | 0x08)); // Setting disposal flag
                Writer.Write((short)(FrameDelay / 10)); // Setting frame delay
                Writer.Write(blockhead[6]); // Transparent color index
                Writer.Write((byte)0); // Terminator
            }

            static void WriteImageBlock(Stream SourceGif, BinaryWriter Writer, bool IncludeColorTable, int X, int Y, int Width, int Height)
            {
                SourceGif.Position = SourceImageBlockPosition; // Locating the image block
                var header = new byte[11];
                SourceGif.Read(header, 0, header.Length);
                Writer.Write(header[0]); // Separator
                Writer.Write((short)X); // Position X
                Writer.Write((short)Y); // Position Y
                Writer.Write((short)Width); // Width
                Writer.Write((short)Height); // Height

                if (IncludeColorTable) // If first frame, use global color table - else use local
                {
                    SourceGif.Position = SourceGlobalColorInfoPosition;
                    Writer.Write((byte)(SourceGif.ReadByte() & 0x3F | 0x80)); // Enabling local color table
                    WriteColorTable(SourceGif, Writer);
                }
                else Writer.Write((byte)(header[9] & 0x07 | 0x07)); // Disabling local color table

                Writer.Write(header[10]); // LZW Min Code Size

                // Read/Write image data
                SourceGif.Position = SourceImageBlockPosition + header.Length;

                var dataLength = SourceGif.ReadByte();
                while (dataLength > 0)
                {
                    var imgData = new byte[dataLength];
                    SourceGif.Read(imgData, 0, dataLength);

                    Writer.Write((byte)dataLength);
                    Writer.Write(imgData, 0, dataLength);
                    dataLength = SourceGif.ReadByte();
                }

                Writer.Write((byte)0); // Terminator
            }
            #endregion

            /// <summary>
            /// Frees all resources used by this object.
            /// </summary>
            public void Dispose()
            {
                // Complete File
                _writer.Write((byte)0x3b); // File Trailer

                _writer.BaseStream.Dispose();
                _writer.Dispose();
            }
        }

        /// <summary>
        /// the list of every frame
        /// </summary>
        public List<VideoFrame> frames = new List<VideoFrame>();

        /// <summary>
        /// How Wide the frames are (in pixels)
        /// </summary>
        public ushort width;
        /// <summary>
        /// how Tall the frames are (in pixels)
        /// </summary>
        public ushort height;

        public class VideoFrame
        {
            public static int videoFilePos = 0;

            /// <summary>
            /// the frame image
            /// </summary>
            public Image image;

            public VideoFrame() { }

            public VideoFrame(Reader reader, ushort width, ushort height)
            {
                read(reader, width, height);
            }

            public void read(Reader reader, ushort width, ushort height)
            {
                int frameSize = reader.ReadInt32();

                byte[] gifData = new byte[frameSize + 13];

                // Write Header
                gifData[0] = (byte)'G';
                gifData[1] = (byte)'I';
                gifData[2] = (byte)'F';
                gifData[3] = (byte)'8';
                gifData[4] = (byte)'9';
                gifData[5] = (byte)'a';

                byte[] bytes = BitConverter.GetBytes(width);
                gifData[6] = bytes[0];
                gifData[7] = bytes[1];

                bytes = BitConverter.GetBytes(height);
                gifData[8] = bytes[0];
                gifData[9] = bytes[1];

                // 1 == hasColours, 6 == paletteSize of 128, 7 == 8bpp
                gifData[10] = (1 << 7) | (6 << 4) | 6;
                gifData[11] = 0;
                gifData[12] = 0;

                // Read Data
                reader.Read(gifData, 13, (int)frameSize);

                List<Color> globalPalette = new List<Color>();
                List<Color> localPalette = new List<Color>();
                using (var gifStream = new System.IO.MemoryStream(gifData))
                {
                    using (var gifReader = new Reader(gifStream))
                    {
                        reader.BaseStream.Position = 13;
                        for (int i = 0; i < 0x80; ++i)
                        {
                            byte r = reader.ReadByte();
                            byte g = reader.ReadByte();
                            byte b = reader.ReadByte();
                            globalPalette.Add(Color.FromArgb(r, g, b));
                        }

                        byte buffer = reader.ReadByte();
                        while (buffer != (byte)',') buffer = reader.ReadByte();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        reader.ReadUInt16();
                        byte info = reader.ReadByte();
                        if (info >> 7 == 1)
                        {
                            for (int i = 0; i < 0x80; ++i)
                            {
                                byte r = reader.ReadByte();
                                byte g = reader.ReadByte();
                                byte b = reader.ReadByte();
                                localPalette.Add(Color.FromArgb(r, g, b));
                            }
                        }
                    }
                }

                using (var gifStream = new System.IO.MemoryStream(gifData))
                    image = Image.FromStream(gifStream);

                // ColorPalette pal = new ColorPalette();
                // pal.Entries[0]]

                // image.Palette = pal;

                videoFilePos += frameSize;
                reader.BaseStream.Position = videoFilePos;
            }

            public void write(Writer writer)
            {

                byte[] gifData;
                using (var gifStream = new System.IO.MemoryStream())
                {
                    GifWriter gWriter = new GifWriter(gifStream);
                    gWriter.WriteFrame(image);
                    // image.Save(gifStream, ImageFormat.Gif);
                    gifData = gifStream.ToArray();
                }
                gifData = gifData.Skip(13).ToArray();

                writer.Write((uint)gifData.Length + 4);
                writer.Write(gifData);
            }

            public void export(string path)
            {
                image.Save(path);
            }

        }

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

            VideoFrame.videoFilePos = (int)reader.BaseStream.Position;
            for (int f = 0; f < frameCount; f++)
            {
                frames.Add(new VideoFrame(reader, width, height));
                frames[f].export($"Frames/Frame{f}.gif");
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

            foreach (VideoFrame frame in frames)
                frame.write(writer);

            writer.Close();
        }

    }
}
