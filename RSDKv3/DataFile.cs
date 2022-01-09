using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    public class DataFile
    {
        public class DirInfo
        {
            /// <summary>
            /// the directory path
            /// </summary>
            public string directory = "Folder/";
            /// <summary>
            /// the file offset for the directory
            /// </summary>
            public int startOffset = 0;

            public DirInfo() { }

            public DirInfo(Reader reader)
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                directory = "";
                byte stringLen = reader.ReadByte();
                for (int i = 0; i < stringLen; i++)
                    directory += (char)(reader.ReadByte() ^ (0xFF - stringLen));

                startOffset = reader.ReadInt32();
            }

            public void write(Writer writer)
            {
                directory = directory.Replace('\\', '/');
                writer.Write((byte)directory.Length);
                for (int i = 0; i < directory.Length; i++)
                    writer.Write((byte)(directory[i] ^ (0xFF - directory.Length)));

                writer.Write(startOffset);
            }
        }

        public class FileInfo
        {
            /// <summary>
            /// the filename of the file
            /// </summary>
            public string fileName = "File.bin";
            /// <summary>
            /// the combined filename and directory of the file
            /// </summary>
            public string fullFilename = "Folder/File.bin";

            /// <summary>
            /// an array of the bytes in the file, decrypted
            /// </summary>
            public byte[] fileData;

            /// <summary>
            /// what directory the file is in
            /// </summary>
            public ushort directoryID = 0;

            private int eStringNo = 0;
            private int eNybbleSwap = 0;
            private int eStringPosB = 0;
            private int eStringPosA = 0;

            private static readonly string encryptionStringA = "4RaS9D7KaEbxcp2o5r6t";
            private static readonly string encryptionStringB = "3tRaUxLmEaSn";

            public FileInfo() { }

            public FileInfo(Reader reader)
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                fileName = "";
                byte stringLen = reader.ReadByte();
                for (int i = 0; i < stringLen; i++)
                    fileName += (char)(reader.ReadByte() ^ 0xFF);

                uint fileSize = reader.ReadUInt32();

                fileData = decrypt(reader.ReadBytes((int)fileSize), false);
            }

            public void write(Writer writer, bool standalone = false)
            {
                fileName = fileName.Replace('\\', '/');
                uint fileSize = (uint)fileData.Length;

                if (standalone)
                {
                    writer.Write(fileSize);
                    writer.Write(fileData);
                    writer.Close();
                }
                else
                {
                    writer.Write((byte)fileName.Length);
                    for (int i = 0; i < fileName.Length; i++)
                        writer.Write((byte)(fileName[i] ^ 0xFF));

                    writer.Write(fileSize);
                    writer.Write(decrypt(fileData, true));
                }
            }

            private byte[] decrypt(byte[] data, bool encrypting)
            {
                uint fileSize = (uint)data.Length;

                eStringNo = ((int)fileSize & 0x1FC) >> 2;
                eStringPosB = (eStringNo % 9) + 1;
                eStringPosA = (eStringNo % eStringPosB) + 1;
                eNybbleSwap = 0;

                byte[] outputData = new byte[data.Length];

                for (int i = 0; i < (int)fileSize; i++)
                {
                    int encByte = data[i];
                    if (encrypting)
                    {
                        encByte ^= encryptionStringA[eStringPosA++];

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = (encByte >> 4) | ((encByte & 0xF) << 4);

                        encByte ^= encryptionStringB[eStringPosB++] ^ eStringNo;
                    }
                    else
                    {
                        encByte ^= encryptionStringB[eStringPosB++] ^ eStringNo;

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = (encByte >> 4) | ((encByte & 0xF) << 4);

                        encByte ^= encryptionStringA[eStringPosA++];
                    }
                    outputData[i] = (byte)encByte;

                    if (eStringPosA <= 19 || eStringPosB <= 11)
                    {
                        if (eStringPosA > 19)
                        {
                            eStringPosA = 1;
                            eNybbleSwap ^= 1;
                        }
                        if (eStringPosB > 11)
                        {
                            eStringPosB = 1;
                            eNybbleSwap ^= 1;
                        }
                    }
                    else
                    {
                        eStringNo++;
                        eStringNo &= 0x7F;

                        if (eNybbleSwap != 0)
                        {
                            eStringPosA = (eStringNo % 12) + 6;
                            eStringPosB = (eStringNo % 5) + 4;
                            eNybbleSwap = 0;
                        }
                        else
                        {
                            eNybbleSwap = 1;
                            eStringPosA = (eStringNo % 15) + 3;
                            eStringPosB = (eStringNo % 7) + 1;
                        }
                    }
                }
                return outputData;
            }
        }

        /// <summary>
        /// a list of directories for the data file
        /// </summary>
        public List<DirInfo> directories = new List<DirInfo>();
        /// <summary>
        /// the list of files stored in the data file
        /// </summary>
        public List<FileInfo> files = new List<FileInfo>();

        public DataFile() { }
        public DataFile(string filepath) : this(new Reader(filepath)) { }
        public DataFile(System.IO.Stream stream) : this(new Reader(stream)) { }

        public DataFile(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            int headerSize = reader.ReadInt32();
            int dircount = reader.ReadUInt16();

            directories.Clear();
            for (int d = 0; d < dircount; d++)
                directories.Add(new DirInfo(reader));

            files.Clear();
            for (int d = 0; d < dircount; d++)
            {
                if ((d + 1) < directories.Count())
                {
                    while (reader.BaseStream.Position - headerSize < directories[d + 1].startOffset && reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        FileInfo f = new FileInfo(reader);
                        f.fullFilename = directories[d].directory + f.fileName;
                        files.Add(f);
                    }
                }
                else
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        FileInfo f = new FileInfo(reader);
                        f.fullFilename = directories[d].directory + f.fileName;
                        files.Add(f);
                    }
                }
            }
            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // Initial File Write
            int headerSize = 0;
            writer.Write(headerSize);
            writer.Write((ushort)directories.Count);

            foreach (DirInfo dir in directories)
                dir.write(writer);

            headerSize = (int)writer.BaseStream.Position;

            // var orderedFiles = files.OrderBy(f => f.directoryID).ToList();

            int dirID = 0;

            directories[dirID].startOffset = 0;

            foreach (FileInfo file in files)
            {
                if (file.directoryID == dirID)
                {
                    file.write(writer);
                }
                else
                {
                    dirID++;
                    directories[dirID].startOffset = (int)writer.BaseStream.Position - headerSize;
                    file.write(writer);
                }
            }

            // Real File write
            writer.seek(0, System.IO.SeekOrigin.Begin);

            writer.Write(headerSize);
            writer.Write((ushort)directories.Count);

            foreach (DirInfo dir in directories)
                dir.write(writer);

            dirID = 0;

            foreach (FileInfo file in files)
            {
                if (file.directoryID == dirID)
                {
                    file.write(writer);
                }
                else
                {
                    dirID++;
                    file.write(writer);
                }
            }

            writer.Close();
        }
    }
}
