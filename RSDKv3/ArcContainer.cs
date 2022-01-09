using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKv3
{
    public class ArcContainer
    {
        public class FileInfo
        {
            /// <summary>
            /// filename of the file
            /// </summary>
            public string fileName = "File.bin";
            /// <summary>
            /// the offset of the file data
            /// </summary>
            public uint fileOffset = 0;
            /// <summary>
            /// the filesize of the file (in bytes)
            /// </summary>
            public uint fileSize = 0;
            /// <summary>
            /// an array of the bytes in the file, decrypted
            /// </summary>
            public byte[] fileData;

            public FileInfo() { }
        }

        private static readonly byte[] signature = new byte[] { (byte)'A', (byte)'R', (byte)'C', (byte)'L' };

        private int decryptionKey = 0;

        public List<FileInfo> files = new List<FileInfo>();

        public ArcContainer() { }

        public ArcContainer(string filename) : this(new Reader(filename)) { }
        public ArcContainer(Stream stream) : this(new Reader(stream)) { }

        public ArcContainer(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            if (!reader.readBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid ARC Container v3 signature");
            }

            ushort fileCount = reader.ReadUInt16();

            byte[] fileData = reader.readBytes(0x28 * fileCount);

            decryptionKey = fileCount;

            for (int f = 0; f < fileCount; f++)
            {
                FileInfo file = new FileInfo();
                byte[] offsetBytes = new byte[4];
                byte[] sizeBytes = new byte[4];

                file.fileName = "";
                for (int i = 0; i < 0x28; i++)
                {
                    byte bufByte = fileData[i + (f * 0x28)];
                    byte decByte = (byte)(decryptByte() % 0xFF ^ bufByte);
                    if (i < 32)
                    {
                        file.fileName += (char)decByte;
                    }
                    else if (i >= 32 && i < 36)
                    {
                        offsetBytes[i - 32] = decByte;
                    }
                    else
                    {
                        sizeBytes[i - 36] = decByte;
                    }
                }
                file.fileName = file.fileName.Replace("\0", "");
                file.fileOffset = BitConverter.ToUInt32(offsetBytes, 0);
                file.fileSize = BitConverter.ToUInt32(sizeBytes, 0);

                long ReadPos = reader.BaseStream.Position;
                reader.seek(file.fileOffset, SeekOrigin.Begin);
                file.fileData = reader.readBytes(file.fileSize);
                reader.seek(ReadPos, SeekOrigin.Begin);
                files.Add(file);
            }

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // Header
            writer.Write(signature);

            writer.Write((ushort)files.Count);
            decryptionKey = (ushort)files.Count;

            // File Headers
            foreach (FileInfo file in files)
            {
                for (int i = 0; i < 0x20; ++i)
                    writer.Write((char)(i >= file.fileName.Length ? 0 : file.fileName[i]));
                writer.Write(0x00); // offset
                writer.Write(file.fileData.Length);
            }

            // File Data
            foreach (FileInfo file in files)
            {
                file.fileOffset = (uint)writer.BaseStream.Position;
                file.fileSize = (uint)file.fileData.Length;
                writer.Write(file.fileData);
            }

            writer.seek(0, SeekOrigin.Begin);

            writer.Write(signature);

            writer.Write((ushort)files.Count);
            decryptionKey = (ushort)files.Count;

            foreach (FileInfo file in files)
            {
                byte[] offsetBytes = BitConverter.GetBytes(file.fileOffset);
                byte[] sizeBytes = BitConverter.GetBytes(file.fileSize);

                for (int i = 0; i < 0x28; ++i)
                {
                    byte buffer = 0;
                    if (i < 0x20)
                        buffer = (byte)(i >= file.fileName.Length ? 0 : file.fileName[i]);
                    else if (i >= 0x20 && i < 0x24)
                        buffer = offsetBytes[i - 32];
                    else
                        buffer = sizeBytes[i - 36];
                    writer.Write((byte)(decryptByte() % 0xFF ^ buffer));
                }
            }

            for (int f = 0; f < (ushort)files.Count; ++f) writer.Write(files[f].fileData);

            writer.Close();
        }

        private int decryptByte()
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
