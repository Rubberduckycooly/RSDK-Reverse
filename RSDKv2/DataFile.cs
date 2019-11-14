using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class DataFile
    {
        public class DirInfo
        {
            /// <summary>
            /// the directory path
            /// </summary>
            public string Directory;
            /// <summary>
            /// the file offset for the directory
            /// </summary>
            public int Address;

            public DirInfo()
            {

            }

            public DirInfo(Reader reader)
            {
                byte ss = reader.ReadByte();

                char buf = ',';
                string DecryptedString = "";

                for (int i = 0; i < ss; i++)
                {
                    byte b = reader.ReadByte();
                    int bufInt = (int)b;

                    bufInt ^= 0xFF - ss;

                    buf = (char)bufInt;
                    DecryptedString = DecryptedString + buf;
                }
                Directory = DecryptedString;
                //Console.WriteLine(Directory);
                Address = reader.ReadInt32();
            }

            public void Write(Writer writer, bool SingleFile = false)
            {
                Directory = Directory.Replace('\\', '/');
                int ss = Directory.Length;
                writer.Write((byte)ss);

                string str = Directory;

                for (int i = 0; i < ss; i++)
                {
                    int s = str[i];
                    writer.Write((byte)(s ^ (0xFF - ss)));
                }

                writer.Write(Address);
                if (SingleFile) writer.Close();
            }

            public void Write(string dataFolder)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Directory);
                if (!di.Exists) di.Create();
                Writer writer = new Writer(dataFolder);
                Directory = Directory.Replace('\\', '/');
                writer.Write(Directory);
                writer.Write(Address);
                writer.Close();
            }
        }

        public class FileInfo
        {
            /// <summary>
            /// the filename of the file
            /// </summary>
            public string FileName;
            /// <summary>
            /// the combined filename and directory of the file
            /// </summary>
            public string FullFileName;
            /// <summary>
            /// how many bytes the file contains
            /// </summary>
            public ulong fileSize;

            /// <summary>
            /// an array of bytes in the file
            /// </summary>
            public byte[] Filedata;

            /// <summary>
            /// what directory the file is in
            /// </summary>
            public ushort DirID = 0;

            int decryptKeyZ;
            int decryptKeyIndexZ;
            int decryptKeyIndex2;
            int decryptKeyIndex1;

            string decryptKey1 = "4RaS9D7KaEbxcp2o5r6t";
            string decryptKey2 = "3tRaUxLmEaSn";

            public FileInfo()
            {

            }

            public FileInfo(Reader reader)
            {
                byte ss = reader.ReadByte();

                char buf = ',';
                string DecryptedString = "";

                for (int i = 0; i < ss; i++)
                {
                    byte b = reader.ReadByte();
                    int bufInt = b;

                    bufInt ^= 0xFF;

                    buf = (char)bufInt;
                    DecryptedString = DecryptedString + buf;
                }

                FileName = DecryptedString;

                //Console.WriteLine(FileName);

                fileSize = reader.ReadUInt32();

                byte[] tmp = reader.ReadBytes(fileSize);
                int[] outbuf = new int[fileSize];

                for (int i = 0; i < (int)fileSize; i++)
                {
                    outbuf[i] = tmp[i];
                }

                decryptKeyZ = ((int)fileSize & 0x1fc) >> 2;
                decryptKeyIndex2 = (decryptKeyZ % 9) + 1;
                decryptKeyIndex1 = (decryptKeyZ % decryptKeyIndex2) + 1;

                decryptKeyIndexZ = 0;

                for (int i = 0; i < (int)fileSize; i++)
                {
                    outbuf[i] ^= decryptKey2[decryptKeyIndex2++] ^ decryptKeyZ;

                    if (decryptKeyIndexZ == 1) // swap nibbles
                        outbuf[i] = (outbuf[i] >> 4) | ((outbuf[i] & 0xf) << 4);

                    outbuf[i] ^= decryptKey1[decryptKeyIndex1++];

                    if ((decryptKeyIndex1 <= 19) || (decryptKeyIndex2 <= 11))
                    {
                        if (decryptKeyIndex1 > 19)
                        {
                            decryptKeyIndex1 = 1;
                            decryptKeyIndexZ ^= 1;
                        }
                        if (decryptKeyIndex2 > 11)
                        {
                            decryptKeyIndex2 = 1;
                            decryptKeyIndexZ ^= 1;
                        }
                    }
                    else
                    {
                        decryptKeyZ++;
                        decryptKeyZ &= 0x7F;

                        if (decryptKeyIndexZ != 0)
                        {
                            decryptKeyIndex1 = (decryptKeyZ % 12) + 6;
                            decryptKeyIndex2 = (decryptKeyZ % 5) + 4;
                            decryptKeyIndexZ = 0;
                        }
                        else
                        {
                            decryptKeyIndexZ = 1;
                            decryptKeyIndex1 = (decryptKeyZ % 15) + 3;
                            decryptKeyIndex2 = (decryptKeyZ % 7) + 1;
                        }
                    }
                }
                Filedata = new byte[outbuf.Length];
                for (int i = 0; i <outbuf.Length; i++)
                {
                    Filedata[i] = (byte)outbuf[i];
                }
                
            }

            public void Write(Writer writer, bool SingleFile = false)
            {
                FileName = FileName.Replace('\\', '/');

                int ss = FileName.Length;
                writer.Write((byte)ss);

                string str = FileName;

                fileSize = (uint)Filedata.Length;

                for (int i = 0; i < ss; i++)
                {
                    int s = str[i];
                    writer.Write((byte)(s ^ (0xFF)));
                }

                if (SingleFile)
                {
                    writer.Write(fileSize);
                    writer.Write(Filedata);
                    writer.Close();
                }
                else
                {
                    byte[] outfbuf = Filedata;

                    // Encrypt file
                    decryptKeyZ = (byte)(fileSize & 0x1fc) >> 2;
                    decryptKeyIndex2 = (decryptKeyZ % 9) + 1;
                    decryptKeyIndex1 = (decryptKeyZ % decryptKeyIndex2) + 1;

                    decryptKeyIndexZ = 0;

                    for (ulong i = 0; i < fileSize; i++)
                    {
                        outfbuf[i] ^= (byte)decryptKey1[decryptKeyIndex1++];

                        if (decryptKeyIndexZ == 1) // swap nibbles
                            outfbuf[i] = (byte)((outfbuf[i] >> 4) | ((outfbuf[i] & 0xf) << 4));

                        outfbuf[i] ^= (byte)(decryptKey2[decryptKeyIndex2++] ^ decryptKeyZ);

                        if ((decryptKeyIndex1 <= 19) || (decryptKeyIndex2 <= 11))
                        {
                            if (decryptKeyIndex1 > 19)
                            {
                                decryptKeyIndex1 = 1;
                                decryptKeyIndexZ ^= 1;
                            }
                            if (decryptKeyIndex2 > 11)
                            {
                                decryptKeyIndex2 = 1;
                                decryptKeyIndexZ ^= 1;
                            }
                        }
                        else
                        {
                            decryptKeyZ++;
                            decryptKeyZ &= 0x7F;

                            if (decryptKeyIndexZ != 0)
                            {
                                decryptKeyIndex1 = (decryptKeyZ % 12) + 6;
                                decryptKeyIndex2 = (decryptKeyZ % 5) + 4;
                                decryptKeyIndexZ = 0;
                            }
                            else
                            {
                                decryptKeyIndexZ = 1;
                                decryptKeyIndex1 = (decryptKeyZ % 15) + 3;
                                decryptKeyIndex2 = (decryptKeyZ % 7) + 1;
                            }
                        }
                    }

                    Filedata = outfbuf;

                    writer.Write((uint)fileSize);
                    writer.Write(Filedata);
                }
            }

            public void Write(string Datadirectory)
            {
                string tmp = FullFileName.Replace(System.IO.Path.GetFileName(FullFileName), "");
                string fullDir = Datadirectory + "\\" + tmp;
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fullDir);
                if (!di.Exists) di.Create();
                Writer writer = new Writer(fullDir + FileName);
                writer.Write(Filedata);
                writer.Close();
            }
        }

        /// <summary>
        /// the "offset" for file loading I think?
        /// </summary>
        public int headerSize;

        /// <summary>
        /// a list of directories for the datafile
        /// </summary>
        public List<DirInfo> Directories = new List<DirInfo>();
        /// <summary>
        /// the list of fileinfo data for the file
        /// </summary>
        public List<FileInfo> Files = new List<FileInfo>();
        /** Sequentially, a file description block for every file stored inside the data file. */

        public DataFile()
        { }

        public DataFile(string filepath) : this(new Reader(filepath))
        { }

        public DataFile(Reader reader)
        {

            headerSize = reader.ReadInt32();
            //Console.WriteLine("Header Size = " + headerSize);

            int dircount = reader.ReadUInt16();
            //Console.WriteLine("Directory Count = " + dircount);

            Directories = new List<DirInfo>();

            for (int d = 0; d < dircount; d++)
            {
                Directories.Add(new DirInfo(reader));
            }

            for (int d = 0; d < dircount; d++)
            {
                if ((d + 1) < Directories.Count())
                {
                    while (reader.Position - headerSize < Directories[d + 1].Address && !reader.IsEof)
                    {
                        FileInfo f = new FileInfo(reader);
                        f.FullFileName = Directories[d].Directory + f.FileName;
                        Files.Add(f);
                    }
                }
                else
                {
                    while (!reader.IsEof)
                    {
                        FileInfo f = new FileInfo(reader);
                        f.FullFileName = Directories[d].Directory + f.FileName;
                        Files.Add(f);
                    }
                }
            }
            reader.Close();
        }

        public void Write(Writer writer)
        {

            int DirHeaderSize = 0;

            writer.Write(DirHeaderSize);

            writer.Write((ushort)Directories.Count);

            for (int i = 0; i < Directories.Count; i++)
            {
                Directories[i].Write(writer);
            }

            DirHeaderSize = (int)writer.BaseStream.Position;

            var orderedFiles = Files.OrderBy(f => f.DirID).ToList();

            int Dir = 0;

            Directories[Dir].Address = 0;

            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].DirID == Dir)
                {
                    Files[i].Write(writer);
                }
                else
                {
                    Dir++;
                    Directories[Dir].Address = (int)writer.BaseStream.Position - DirHeaderSize;
                    Files[i].Write(writer);
                }
            }

            writer.BaseStream.Position = 0;

            writer.Write(DirHeaderSize);

            writer.Write((ushort)Directories.Count);

            for (int i = 0; i < Directories.Count; i++)
            {
                Directories[i].Write(writer);
            }

            Dir = 0;

            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].DirID == Dir)
                {
                    Files[i].Write(writer);
                }
                else
                {
                    Dir++;
                    Files[i].Write(writer);
                }
            }

            writer.Close();
        }

        public void WriteFile(int fileID)
        {
            Files[fileID].Write("");
        }

        public void WriteFile(string fileName, string NewFileName)
        {
            foreach (FileInfo f in Files)
            {
                if (f.FileName == fileName)
                {
                    f.Write(NewFileName);
                }
            }
        }

    }
}
