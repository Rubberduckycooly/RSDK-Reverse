using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKv3
{
    public class ArcContainer
    {
        public class File
        {
            /// <summary>
            /// filename of the file
            /// </summary>
            public string name = "File.bin";

            /// <summary>
            /// the offset of the file data
            /// </summary>
            public uint offset = 0;

            /// <summary>
            /// an array of the bytes in the file, decrypted
            /// </summary>
            public byte[] data;

            public File() { }
        }

        private static readonly byte[] signature = new byte[] { (byte)'A', (byte)'R', (byte)'C', (byte)'L' };

        private int decryptionKey = 0;

        public List<File> files = new List<File>();

        public ArcContainer() { }

        public ArcContainer(string filename) : this(new Reader(filename)) { }
        public ArcContainer(Stream stream) : this(new Reader(stream)) { }

        public ArcContainer(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid ARC Container v3 signature");
            }

            ushort fileCount = reader.ReadUInt16();

            byte[] fileData = reader.ReadBytes(0x28 * fileCount);

            decryptionKey = fileCount;

            for (int f = 0; f < fileCount; f++)
            {
                File file = new File();
                byte[] offsetBytes = new byte[4];
                byte[] sizeBytes = new byte[4];

                file.name = "";
                for (int i = 0; i < 0x28; i++)
                {
                    byte bufByte = fileData[i + (f * 0x28)];
                    byte decByte = (byte)(DecryptByte() % 0xFF ^ bufByte);
                    if (i < 32)
                        file.name += (char)decByte;
                    else if (i >= 32 && i < 36)
                        offsetBytes[i - 32] = decByte;
                    else
                        sizeBytes[i - 36] = decByte;
                }
                file.name = file.name.Replace("\0", "");
                file.offset = BitConverter.ToUInt32(offsetBytes, 0);
                uint fileSize = BitConverter.ToUInt32(sizeBytes, 0);

                long ReadPos = reader.BaseStream.Position;
                reader.Seek(file.offset, SeekOrigin.Begin);
                file.data = reader.ReadBytes(fileSize);
                reader.Seek(ReadPos, SeekOrigin.Begin);
                files.Add(file);
            }

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // Header
            writer.Write(signature);

            writer.Write((ushort)files.Count);
            decryptionKey = (ushort)files.Count;

            // File Headers
            foreach (File file in files)
            {
                for (int i = 0; i < 0x20; ++i)
                    writer.Write((char)(i >= file.name.Length ? 0 : file.name[i]));

                writer.Write(0x00); // offset
                writer.Write(file.data.Length);
            }

            // File Data
            foreach (File file in files)
            {
                file.offset = (uint)writer.BaseStream.Position;
                writer.Write(file.data);
            }

            writer.Seek(0, SeekOrigin.Begin);

            writer.Write(signature);

            writer.Write((ushort)files.Count);
            decryptionKey = (ushort)files.Count;

            foreach (File file in files)
            {
                byte[] offsetBytes = BitConverter.GetBytes(file.offset);
                byte[] sizeBytes = BitConverter.GetBytes(file.data.Length);

                for (int i = 0; i < 0x28; ++i)
                {
                    byte buffer = 0;
                    if (i < 0x20)
                        buffer = (byte)(i >= file.name.Length ? 0 : file.name[i]);
                    else if (i >= 0x20 && i < 0x24)
                        buffer = offsetBytes[i - 32];
                    else
                        buffer = sizeBytes[i - 36];

                    writer.Write((byte)(DecryptByte() % 0xFF ^ buffer));
                }
            }

            for (int f = 0; f < (ushort)files.Count; ++f) writer.Write(files[f].data);

            writer.Close();
        }

        private int DecryptByte()
        {
            int v1;

            v1 = 48271 * (decryptionKey % 44488) - 3399 * (decryptionKey / 44488);
            if (v1 <= 0)
                decryptionKey = v1 + 0x7FFFFFFF;
            else
                decryptionKey = 48271 * (decryptionKey % 44488) - 3399 * (decryptionKey / 44488);
            return decryptionKey;
        }
    }
}
