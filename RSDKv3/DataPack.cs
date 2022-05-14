using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    public class DataPack
    {
        public class Directory
        {
            /// <summary>
            /// the directory path
            /// </summary>
            public string name = "Folder/";

            /// <summary>
            /// the file offset for the directory
            /// </summary>
            public int offset = 0;

            public Directory() { }

            public Directory(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name = "";
                byte stringLen = reader.ReadByte();
                for (int i = 0; i < stringLen; i++)
                    name += (char)(reader.ReadByte() ^ (0xFF - stringLen));

                offset = reader.ReadInt32();
            }

            public void Write(Writer writer)
            {
                name = name.Replace('\\', '/');
                writer.Write((byte)name.Length);
                for (int i = 0; i < name.Length; i++)
                    writer.Write((byte)(name[i] ^ (0xFF - name.Length)));

                writer.Write(offset);
            }
        }

        public class File
        {
            /// <summary>
            /// the filename of the file
            /// </summary>
            public string name = "File.bin";
            /// <summary>
            /// the combined filename and directory of the file
            /// </summary>
            public string fullName = "Folder/File.bin";

            /// <summary>
            /// an array of the bytes in the file, decrypted
            /// </summary>
            public byte[] data;

            /// <summary>
            /// what directory the file is in
            /// </summary>
            public ushort directoryID = 0;

            private int eKeyNo = 0;
            private int eKeyPosB = 0;
            private int eKeyPosA = 0;
            private int eNybbleSwap = 0;

            private static readonly string encryptionKeyA = "4RaS9D7KaEbxcp2o5r6t";
            private static readonly string encryptionKeyB = "3tRaUxLmEaSn";

            public File() { }

            public File(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name = "";
                byte stringLen = reader.ReadByte();
                for (int i = 0; i < stringLen; i++)
                    name += (char)(reader.ReadByte() ^ 0xFF);

                uint fileSize = reader.ReadUInt32();

                data = Decrypt(reader.ReadBytes((int)fileSize), false);
            }

            public void Write(Writer writer, bool standalone = false)
            {
                name = name.Replace('\\', '/');
                uint fileSize = (uint)data.Length;

                if (standalone)
                {
                    writer.Write(fileSize);
                    writer.Write(data);
                    writer.Close();
                }
                else
                {
                    writer.Write((byte)name.Length);
                    for (int i = 0; i < name.Length; i++)
                        writer.Write((byte)(name[i] ^ 0xFF));

                    writer.Write(fileSize);
                    writer.Write(Decrypt(data, true));
                }
            }

            private byte[] Decrypt(byte[] data, bool encrypting)
            {
                uint fileSize = (uint)data.Length;

                eKeyNo = ((int)fileSize & 0x1FC) >> 2;
                eKeyPosB = (eKeyNo % 9) + 1;
                eKeyPosA = (eKeyNo % eKeyPosB) + 1;
                eNybbleSwap = 0;

                byte[] outputData = new byte[data.Length];

                for (int i = 0; i < (int)fileSize; i++)
                {
                    int encByte = data[i];
                    if (encrypting)
                    {
                        encByte ^= encryptionKeyA[eKeyPosA++];

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = (encByte >> 4) | ((encByte & 0xF) << 4);

                        encByte ^= encryptionKeyB[eKeyPosB++] ^ eKeyNo;
                    }
                    else
                    {
                        encByte ^= encryptionKeyB[eKeyPosB++] ^ eKeyNo;

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = (encByte >> 4) | ((encByte & 0xF) << 4);

                        encByte ^= encryptionKeyA[eKeyPosA++];
                    }
                    outputData[i] = (byte)encByte;

                    if (eKeyPosA <= 19 || eKeyPosB <= 11)
                    {
                        if (eKeyPosA > 19)
                        {
                            eKeyPosA = 1;
                            eNybbleSwap ^= 1;
                        }
                        if (eKeyPosB > 11)
                        {
                            eKeyPosB = 1;
                            eNybbleSwap ^= 1;
                        }
                    }
                    else
                    {
                        eKeyNo++;
                        eKeyNo &= 0x7F;

                        if (eNybbleSwap != 0)
                        {
                            eKeyPosA = (eKeyNo % 12) + 6;
                            eKeyPosB = (eKeyNo % 5) + 4;
                            eNybbleSwap = 0;
                        }
                        else
                        {
                            eNybbleSwap = 1;
                            eKeyPosA = (eKeyNo % 15) + 3;
                            eKeyPosB = (eKeyNo % 7) + 1;
                        }
                    }
                }
                return outputData;
            }
        }

        /// <summary>
        /// a list of directories for the data file
        /// </summary>
        public List<Directory> directories = new List<Directory>();
        /// <summary>
        /// the list of files stored in the data file
        /// </summary>
        public List<File> files = new List<File>();

        public DataPack() { }
        public DataPack(string filepath) : this(new Reader(filepath)) { }
        public DataPack(System.IO.Stream stream) : this(new Reader(stream)) { }

        public DataPack(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            int headerSize = reader.ReadInt32();
            int dircount = reader.ReadUInt16();

            directories.Clear();
            for (int d = 0; d < dircount; d++)
                directories.Add(new Directory(reader));

            files.Clear();
            for (int d = 0; d < dircount; d++)
            {
                if ((d + 1) < directories.Count())
                {
                    while (reader.BaseStream.Position - headerSize < directories[d + 1].offset && reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        File f = new File(reader);
                        f.fullName = directories[d].name + f.name;
                        files.Add(f);
                    }
                }
                else
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        File f = new File(reader);
                        f.fullName = directories[d].name + f.name;
                        files.Add(f);
                    }
                }
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // Initial File Write
            int headerSize = 0;
            writer.Write(headerSize);
            writer.Write((ushort)directories.Count);

            foreach (Directory dir in directories)
                dir.Write(writer);

            headerSize = (int)writer.BaseStream.Position;

            // var orderedFiles = files.OrderBy(f => f.directoryID).ToList();

            int dirID = 0;

            directories[dirID].offset = 0;

            foreach (File file in files)
            {
                if (file.directoryID == dirID)
                {
                    file.Write(writer);
                }
                else
                {
                    dirID++;
                    directories[dirID].offset = (int)writer.BaseStream.Position - headerSize;
                    file.Write(writer);
                }
            }

            // Real File write
            writer.Seek(0, System.IO.SeekOrigin.Begin);

            writer.Write(headerSize);
            writer.Write((ushort)directories.Count);

            foreach (Directory dir in directories)
                dir.Write(writer);

            dirID = 0;

            foreach (File file in files)
            {
                if (file.directoryID == dirID)
                {
                    file.Write(writer);
                }
                else
                {
                    dirID++;
                    file.Write(writer);
                }
            }

            writer.Close();
        }
    }
}
