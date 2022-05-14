using System;
using System.Linq;

namespace RSDKv2
{
    public class Gif
    {
        /// <summary>
        /// the width of the image
        /// </summary>
        public ushort width = 0;

        /// <summary>
        /// the height of the image
        /// </summary>
        public ushort height = 0;

        /// <summary>
        /// the Image's palette
        /// </summary>
        public Palette.Color[] palette = new Palette.Color[0x100];

        /// <summary>
        /// the pixel indices of the image 
        /// </summary>
        public byte[] pixels = new byte[0];

        public Gif()
        {
            for (int i = 0; i < 0x100; i++)
                palette[i] = new Palette.Color(0xFF, 0x00, 0xFF);
        }

        public Gif(string filename) : this(new Reader(filename)) { }

        public Gif(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Gif(Reader reader) : this()
        {
            Read(reader);
        }
        public void Read(Reader reader, bool skipHeader = false, int colorCount = 0x80)
        {
            #region Header
            if (!skipHeader)
            {
                reader.Seek(6, System.IO.SeekOrigin.Begin); // GIF89a

                width = reader.ReadByte();
                width |= (ushort)(reader.ReadByte() << 8);

                height = reader.ReadByte();
                height |= (ushort)(reader.ReadByte() << 8);

                byte info = reader.ReadByte(); // Palette Size
                colorCount = (info & 0x7) + 1;
                if (colorCount > 0)
                    colorCount = 1 << colorCount;
                reader.ReadByte(); // background color index
                reader.ReadByte(); // unused

                if (colorCount != 0x100)
                {
                    // RSDK-formatted GIF files *should* use 256 colors, but they don't *have* to.
                    Console.WriteLine($"RSDK-Formatted GIF files should use 256 colors!");
                    // throw new Exception("RSDK-Formatted GIF files must use 256 colors!");
                }
            }

            for (int c = 0; c < colorCount; ++c)
            {
                palette[c].r = reader.ReadByte();
                palette[c].g = reader.ReadByte();
                palette[c].b = reader.ReadByte();
            }
            #endregion

            #region Blocks
            byte blockType = reader.ReadByte();
            while (blockType != 0 && blockType != ';')
            {
                switch (blockType)
                {
                    default: // Unknown
                        Console.WriteLine($"Unknown Block Type ({blockType})");
                        break;

                    case (byte)'!': // Extension
                        {
                            byte extensionType = reader.ReadByte();
                            switch (extensionType)
                            {
                                case 0xF9: // Graphics Control Extension
                                    {
                                        int blockSize = reader.ReadByte();
                                        byte disposalFlag = reader.ReadByte();
                                        ushort frameDelay = reader.ReadUInt16();
                                        byte transparentIndex = reader.ReadByte();
                                        reader.ReadByte(); // terminator
                                    }
                                    break;

                                case 0x01: // Plain Text Extension
                                case 0xFE: // Comment Extension
                                case 0xFF: // Application Extension
                                    {
                                        int blockSize = reader.ReadByte();
                                        while (blockSize != 0)
                                        {
                                            // Read block
                                            reader.BaseStream.Position += blockSize;
                                            blockSize = reader.ReadByte(); // next block Size, if its 0 we know its the end of block
                                        }
                                    }
                                    break;

                                default: // Unknown
                                    Console.WriteLine($"Unknown Extension Type ({extensionType})");
                                    return;
                            }
                        }
                        break;

                    case (byte)',': // Image descriptor
                        {
                            int left = reader.ReadUInt16();
                            int top = reader.ReadUInt16();
                            int right = reader.ReadUInt16();
                            int bottom = reader.ReadUInt16();

                            byte info2 = reader.ReadByte();
                            bool interlaced = (info2 & 0x40) != 0;
                            if (info2 >> 7 == 1)
                            {
                                for (int c = 0x80; c < 0x100; ++c)
                                {
                                    palette[c].r = reader.ReadByte();
                                    palette[c].g = reader.ReadByte();
                                    palette[c].b = reader.ReadByte();
                                }
                            }

                            ReadPictureData(width, height, interlaced, reader);
                        }
                        break;
                }

                blockType = reader.ReadByte();
            }
            #endregion

            if (!skipHeader)
                reader.Close();
        }


        public void Write(string filename)
        {
            Write(new Writer(filename));
        }

        public void Write(System.IO.Stream s)
        {
            Write(new Writer(s));
        }

        public void Write(Writer writer, bool skipHeader = false, bool useLocal = false)
        {
            #region Header
            // [GIF HEADER]
            if (!skipHeader)
            {
                writer.Write("GIF".ToCharArray()); // File type
                writer.Write("89a".ToCharArray()); // File Version

                writer.Write(width);
                writer.Write(height);

                if (useLocal)
                    writer.Write((byte)((1 << 7) | (6 << 4) | 6)); // 1 == hasColors, 6 == paletteSize of 128, 6 == 7bpp
                else
                    writer.Write((byte)((1 << 7) | (7 << 4) | 7)); // 1 == hasColors, 7 == paletteSize of 256, 7 == 8bpp
                writer.Write((byte)0);
                writer.Write((byte)0);
            }

            // [GLOBAL PALETTE]
            for (int c = 0; c < (useLocal ? 0x80 : 0x100); ++c)
            {
                writer.Write(palette[c].r);
                writer.Write(palette[c].g);
                writer.Write(palette[c].b);
            }
            #endregion

            #region Extension Blocks
            // [EXTENSION BLOCKS]
            #endregion

            #region Image Descriptor Block
            // [IMAGE DESCRIPTOR HEADER]
            writer.Write(',');

            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((ushort)width);
            writer.Write((ushort)height);
            if (useLocal)
                writer.Write((byte)((1 << 7) | (0 << 6) | 6)); // 1 == useLocal, 0 == no interlacing, 6 == 7bpp
            else
                writer.Write((byte)((0 << 7) | (0 << 6) | 0)); // 0 == noLocal, 0 == no interlacing, no local palette, so we dont care

            // [LOCAL PALETTE]
            if (useLocal)
            {
                for (int c = 0x80; c < 0x100; ++c)
                {
                    writer.Write(palette[c].r);
                    writer.Write(palette[c].g);
                    writer.Write(palette[c].b);
                }
            }

            // [IMAGE DATA]
            WritePictureData(width, height, false, useLocal ? (byte)7 : (byte)8, writer);

            // [BLOCK END MARKER]
            writer.Write(';'); // ';' used for image descriptor, 0 would be used for other blocks
            #endregion

            writer.Close();
        }

        public System.Drawing.Image ToImage()
        {
            // Create image
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            System.Drawing.Imaging.ColorPalette cpal = img.Palette;

            for (int i = 0; i < 0x100; i++)
                cpal.Entries[i] = System.Drawing.Color.FromArgb(255, palette[i].r, palette[i].g, palette[i].b);

            img.Palette = cpal;

            // Write data to image
            System.Drawing.Imaging.BitmapData imgData = img.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, img.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, imgData.Scan0, pixels.Length);
            img.UnlockBits(imgData);

            return img;
        }

        public Gfx ToGfx()
        {
            // Create image
            Gfx img = new Gfx();
            img.width = (ushort)width;
            img.height = (ushort)height;

            for (int i = 0; i < 0xFF; i++)
            {
                img.palette[i].r = palette[i].r;
                img.palette[i].g = palette[i].g;
                img.palette[i].b = palette[i].b;
            }
            img.palette[0xFF].r = 0xFF;
            img.palette[0xFF].g = 0x00;
            img.palette[0xFF].b = 0xFF;

            img.pixels = new byte[width * height];
            Array.Copy(pixels, img.pixels, pixels.Length);

            return img;
        }

        public Bitmap ToBitmap()
        {
            // Create image
            Bitmap img = new Bitmap();
            img.width = width;
            img.height = height;

            for (int c = 0; c < 0x100; c++)
            {
                img.palette[c].r = palette[c].r;
                img.palette[c].g = palette[c].g;
                img.palette[c].b = palette[c].b;
            }

            img.pixels = new byte[width * height];
            Array.Copy(pixels, img.pixels, pixels.Length);

            return img;
        }

        public void FromImage(System.Drawing.Bitmap img)
        {
            // Create image
            width = (ushort)img.Width;
            height = (ushort)img.Height;

            for (int c = 0; c < 0x100; c++)
            {
                palette[c].r = img.Palette.Entries[c].R;
                palette[c].g = img.Palette.Entries[c].G;
                palette[c].b = img.Palette.Entries[c].B;
            }
            pixels = new byte[width * height];

            System.Drawing.Imaging.BitmapData imgData = img.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Runtime.InteropServices.Marshal.Copy(imgData.Scan0, pixels, 0, pixels.Length);
            img.UnlockBits(imgData);
        }

        public void FromImage(Gfx img)
        {
            // Create image
            width = img.width;
            height = img.height;

            for (int c = 0; c < 0xFF; c++)
            {
                palette[c].r = img.palette[c].r;
                palette[c].g = img.palette[c].g;
                palette[c].b = img.palette[c].b;
            }
            palette[0xFF].r = 0xFF;
            palette[0xFF].g = 0x00;
            palette[0xFF].b = 0xFF;

            pixels = new byte[width * height];
            Array.Copy(img.pixels, pixels, pixels.Length);
        }

        public void FromImage(Bitmap img)
        {
            // Create image
            width = (ushort)img.width;
            height = (ushort)img.height;

            for (int i = 0; i < 0x100; i++)
            {
                palette[i].r = img.palette[i].r;
                palette[i].g = img.palette[i].g;
                palette[i].b = img.palette[i].b;
            }

            pixels = new byte[width * height];
            Array.Copy(img.pixels, pixels, pixels.Length);
        }


        #region GIF PARSING

        #region DECLARATIONS
        private const int PARSING_IMAGE = 0;
        private const int PARSE_COMPLETE = 1;
        private const int LZ_MAX_CODE = 4095;
        private const int LZ_BITS = 12;
        private const int FIRST_CODE = 4097;
        private const int NO_SUCH_CODE = 4098;

        private const int HT_SIZE = 8192;
        private const int HT_KEY_MASK = 0x1FFF;

        private class Decoder
        {
            public int depth;
            public int clearCode;
            public int eofCode;
            public int runningCode;
            public int runningBits;
            public int prevCode;
            public int currentCode;
            public int maxCodePlusOne;
            public int stackPtr;
            public int shiftState;
            public int fileState;
            public int position;
            public int bufferSize;
            public uint shiftData;
            public uint pixelCount;
            public byte[] buffer = new byte[0x100];
            public byte[] stack = new byte[0x1000];
            public byte[] suffix = new byte[0x1000];
            public uint[] prefix = new uint[0x1000];
        };
        private class Encoder
        {
            public int depth;
            public int clearCode;
            public int eofCode;
            public int runningCode;
            public int runningBits;
            public int currentCode;
            public int maxCodePlusOne;
            public int shiftState;
            public int bufferSize;
            public uint shiftData;
            public int fileState;
            public byte[] buffer = new byte[0x100];
            public uint[] hashTable = new uint[HT_SIZE];
        };

        private int[] codeMasks = { 0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F, 0xFF, 0x1FF, 0x3FF, 0x7FF, 0xFFF };

        private Decoder decoder = new Decoder();
        private Encoder encoder = new Encoder();
        #endregion

        #region DECODING
        private void InitDecoder(Reader reader)
        {
            byte initCodeSize = reader.ReadByte();
            decoder.fileState = PARSING_IMAGE;
            decoder.position = 0;
            decoder.bufferSize = 0;
            decoder.buffer[0] = 0;
            decoder.depth = initCodeSize;
            decoder.clearCode = 1 << initCodeSize;
            decoder.eofCode = decoder.clearCode + 1;
            decoder.runningCode = decoder.eofCode + 1;
            decoder.runningBits = initCodeSize + 1;
            decoder.maxCodePlusOne = 1 << decoder.runningBits;
            decoder.stackPtr = 0;
            decoder.prevCode = NO_SUCH_CODE;
            decoder.shiftState = 0;
            decoder.shiftData = 0;
            for (int i = 0; i <= LZ_MAX_CODE; ++i) decoder.prefix[i] = NO_SUCH_CODE;
        }
        private void ReadLine(Reader reader, int length, int offset)
        {
            int i = 0;
            int stackPtr = decoder.stackPtr;
            int eofCode = decoder.eofCode;
            int clearCode = decoder.clearCode;
            int prevCode = decoder.prevCode;
            if (stackPtr != 0)
            {
                while (stackPtr != 0)
                {
                    if (i >= length)
                        break;

                    pixels[offset++] = decoder.stack[--stackPtr];
                    i++;
                }
            }

            while (i < length)
            {
                int gifCode = ReadCode(reader);
                if (gifCode == eofCode)
                {
                    if (i != length - 1 || decoder.pixelCount != 0)
                        return;

                    i++;
                }
                else
                {
                    if (gifCode == clearCode)
                    {
                        for (int p = 0; p <= LZ_MAX_CODE; p++)
                            decoder.prefix[p] = NO_SUCH_CODE;

                        decoder.runningCode = decoder.eofCode + 1;
                        decoder.runningBits = decoder.depth + 1;
                        decoder.maxCodePlusOne = 1 << decoder.runningBits;
                        prevCode = decoder.prevCode = NO_SUCH_CODE;
                    }
                    else
                    {
                        if (gifCode < clearCode)
                        {
                            pixels[offset] = (byte)gifCode;
                            offset++;
                            i++;
                        }
                        else
                        {
                            if (gifCode < 0 || gifCode > LZ_MAX_CODE)
                                return;

                            int code;
                            if (decoder.prefix[gifCode] == NO_SUCH_CODE)
                            {
                                if (gifCode != decoder.runningCode - 2)
                                    return;

                                code = prevCode;
                                decoder.suffix[decoder.runningCode - 2] = decoder.stack[stackPtr++] = TracePrefix(prevCode, clearCode);
                            }
                            else
                            {
                                code = gifCode;
                            }
                            int c = 0;
                            while (c++ <= LZ_MAX_CODE && code > clearCode && code <= LZ_MAX_CODE)
                            {
                                decoder.stack[stackPtr++] = decoder.suffix[code];
                                code = (int)decoder.prefix[code];
                            }
                            if (c >= LZ_MAX_CODE | code > LZ_MAX_CODE)
                                return;

                            decoder.stack[stackPtr++] = (byte)code;
                            while (stackPtr != 0 && i++ < length)
                                pixels[offset++] = decoder.stack[--stackPtr];
                        }

                        if (prevCode != NO_SUCH_CODE)
                        {
                            if (decoder.runningCode < 2 || decoder.runningCode > FIRST_CODE)
                                return;

                            decoder.prefix[decoder.runningCode - 2] = (uint)prevCode;
                            if (gifCode == decoder.runningCode - 2)
                                decoder.suffix[decoder.runningCode - 2] = TracePrefix(prevCode, clearCode);
                            else
                                decoder.suffix[decoder.runningCode - 2] = TracePrefix(gifCode, clearCode);
                        }
                        prevCode = gifCode;
                    }
                }
            }
            decoder.prevCode = prevCode;
            decoder.stackPtr = stackPtr;
        }

        private int ReadCode(Reader reader)
        {
            while (decoder.shiftState < decoder.runningBits)
            {
                byte b = ReadByte(reader);
                decoder.shiftData |= (uint)b << decoder.shiftState;
                decoder.shiftState += 8;
            }
            int result = (int)(decoder.shiftData & (uint)codeMasks[decoder.runningBits]);
            decoder.shiftData >>= decoder.runningBits;
            decoder.shiftState -= decoder.runningBits;
            if (++decoder.runningCode > decoder.maxCodePlusOne && decoder.runningBits < LZ_BITS)
            {
                decoder.maxCodePlusOne <<= 1;
                decoder.runningBits++;
            }
            return result;
        }

        private byte ReadByte(Reader reader)
        {
            byte c = 0;
            if (decoder.fileState == PARSE_COMPLETE)
                return c;

            byte b;
            if (decoder.position == decoder.bufferSize)
            {
                b = reader.ReadByte();
                decoder.bufferSize = (int)b;
                if (decoder.bufferSize == 0)
                {
                    decoder.fileState = PARSE_COMPLETE;
                    return c;
                }
                decoder.buffer = reader.ReadBytes(decoder.bufferSize);
                b = decoder.buffer[0];
                decoder.position = 1;
            }
            else
            {
                b = decoder.buffer[decoder.position++];
            }
            return b;
        }

        private byte TracePrefix(int code, int clearCode)
        {
            int i = 0;
            while (code > clearCode && i++ <= LZ_MAX_CODE) code = (int)decoder.prefix[code];

            return (byte)code;
        }

        private void ReadPictureData(int width, int height, bool interlaced, Reader reader)
        {
            pixels = new byte[width * height];

            InitDecoder(reader);
            if (interlaced)
            {
                int[] initialRows = { 0, 4, 2, 1 };
                int[] rowInc = { 8, 8, 4, 2 };
                for (int p = 0; p < 4; ++p)
                {
                    for (int y = initialRows[p]; y < height; y += rowInc[p])
                        ReadLine(reader, width, y * width);
                }
            }
            else
            {
                for (int h = 0; h < height; ++h) ReadLine(reader, width, h * width);
            }
        }
        #endregion

        #region ENCODING

        private void InsertHashTable(uint key, int code)
        {
            uint hKey = ((key >> 12) ^ key) & HT_KEY_MASK;

            while ((encoder.hashTable[hKey] >> 12) != 0xFFFFF)
                hKey = (hKey + 1) & HT_KEY_MASK;

            encoder.hashTable[hKey] = (uint)((key << 12) | (code & 0x0FFF));
        }

        private int ExistsHashTable(uint key)
        {
            uint hKey = ((key >> 12) ^ key) & HT_KEY_MASK;
            uint tableKey = 0;

            while ((tableKey = encoder.hashTable[hKey] >> 12) != 0xFFFFF)
            {
                if (key == tableKey)
                    return (int)(encoder.hashTable[hKey] & 0x0FFF);
                hKey = (hKey + 1) & HT_KEY_MASK;
            }

            return -1;
        }

        private void InitEncoder(Writer writer, byte bitsPerPixel)
        {
            byte initCodeSize = bitsPerPixel < 2 ? (byte)2 : bitsPerPixel;
            writer.Write(initCodeSize);

            encoder.fileState = PARSING_IMAGE;
            encoder.bufferSize = 0;
            encoder.buffer[0] = 0;
            encoder.depth = initCodeSize;
            encoder.clearCode = 1 << initCodeSize;
            encoder.eofCode = encoder.clearCode + 1;
            encoder.runningCode = encoder.eofCode + 1;
            encoder.runningBits = initCodeSize + 1;
            encoder.maxCodePlusOne = 1 << encoder.runningBits;
            encoder.currentCode = FIRST_CODE;
            encoder.shiftState = 0;
            encoder.shiftData = 0;
            for (int i = 0; i < HT_SIZE; ++i) encoder.hashTable[i] = 0xFFFFFFFF;

            WriteCode(writer, encoder.clearCode);
        }

        private void WriteByte(Writer writer, int b, bool flush = false)
        {
            if (flush)
            {
                // write everything we've got
                writer.Write((byte)encoder.bufferSize);
                writer.Write(encoder.buffer, 0, encoder.bufferSize);
                encoder.bufferSize = 0;
            }
            else
            {
                if (encoder.bufferSize == 0xFF)
                {
                    // buffer is full, write it to file
                    writer.Write((byte)encoder.bufferSize);
                    writer.Write(encoder.buffer, 0, encoder.bufferSize);
                    encoder.bufferSize = 0;
                }
                encoder.buffer[encoder.bufferSize++] = (byte)b;
            }
        }

        private void WriteCode(Writer writer, int code, bool flush = false)
        {
            if (flush)
            {
                // write remaining data
                while (encoder.shiftState > 0)
                {
                    WriteByte(writer, (int)(encoder.shiftData & 0xFF));
                    encoder.shiftData >>= 8;
                    encoder.shiftState -= 8;
                }
                //clear & reset
                encoder.shiftState = 0;
                WriteByte(writer, 0, true);
            }
            else
            {
                encoder.shiftData |= ((uint)code) << encoder.shiftState;
                encoder.shiftState += encoder.runningBits;

                // write any full bytes we have
                while (encoder.shiftState >= 8)
                {
                    WriteByte(writer, (int)(encoder.shiftData & 0xFF));
                    encoder.shiftData >>= 8;
                    encoder.shiftState -= 8;
                }
            }

            // add more bits for the code if needed
            if (encoder.runningCode >= encoder.maxCodePlusOne && code <= LZ_MAX_CODE)
                encoder.maxCodePlusOne = 1 << ++encoder.runningBits;
        }

        private void WriteLine(Writer writer, byte[] line)
        {
            int mask = codeMasks[encoder.depth];
            for (int l = 0; l < line.Length; l++)
                line[l] = (byte)(line[l] & mask);

            int pxPos = 0, curCode = 0;
            if (encoder.currentCode == FIRST_CODE)
                curCode = line[pxPos++];
            else
                curCode = encoder.currentCode;

            while (pxPos < line.Length)
            {
                byte pixel = line[pxPos++];

                // create a key based on our code & the next pixel
                int newCode = 0;
                uint newKey = (((uint)curCode) << 8) + pixel;
                if ((newCode = ExistsHashTable(newKey)) >= 0)
                {
                    curCode = newCode;
                }
                else
                {
                    WriteCode(writer, curCode);
                    curCode = pixel;

                    // handle clear codes if the hash table is full
                    if (encoder.runningCode >= LZ_MAX_CODE)
                    {
                        WriteCode(writer, encoder.clearCode);
                        encoder.runningCode = encoder.eofCode + 1;
                        encoder.runningBits = encoder.depth + 1;
                        encoder.maxCodePlusOne = 1 << encoder.runningBits;

                        //clear hash table
                        for (int h = 0; h < HT_SIZE; ++h) encoder.hashTable[h] = 0xFFFFFFFF;
                    }
                    else
                    {
                        // add this to the hash table
                        InsertHashTable(newKey, encoder.runningCode++);
                    }
                }

            }

            // keep this, it'll be needed later or at the end
            encoder.currentCode = curCode;
        }

        private void WritePictureData(int width, int height, bool interlaced, byte bitsPerPixel, Writer writer)
        {
            InitEncoder(writer, bitsPerPixel);

            if (interlaced)
            {
                int[] initialRows = { 0, 4, 2, 1 };
                int[] rowInc = { 8, 8, 4, 2 };
                for (int p = 0; p < 4; ++p)
                {
                    for (int y = initialRows[p]; y < height; y += rowInc[p])
                        WriteLine(writer, pixels.Skip(y * width).Take(width).ToArray());
                }
            }
            else
            {
                for (int y = 0; y < height; ++y) WriteLine(writer, pixels.Skip(y * width).Take(width).ToArray());
            }

            // write extra data
            WriteCode(writer, encoder.currentCode);
            WriteCode(writer, encoder.eofCode);
            WriteCode(writer, 0, true);

            writer.Write((byte)0); //block terminator

            encoder.fileState = PARSE_COMPLETE;
        }
        #endregion
        #endregion
    }
}
