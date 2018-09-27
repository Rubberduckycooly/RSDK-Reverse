using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class DataFile
    {
        public class Header
        {
            //ulong MagicNumber;
            //ulong FormatVersion;
            ushort FileCount;
        }

        public class DirInfo
        {
            public string Directory;

            public long Address;

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
                Console.WriteLine(Directory);
                Address = reader.ReadInt32();
            }

            public void Write(string dataFolder)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Directory);
                if (!di.Exists) di.Create();
                Writer writer = new Writer(dataFolder);
                writer.Write(Directory);
                writer.Write(Address);
                writer.Close();
            }
        }

        public class FileInfo
        {
            public string FileName;
            public string FullFileName;

            public ulong fileSize;

            public byte[] Filedata;

            public FileInfo(Reader reader)
            {
                byte ss = reader.ReadByte();

                char buf = ',';
                string DecryptedString = "";

                for (int i = 0; i < ss; i++)
                {
                    byte b = reader.ReadByte();
                    int bufInt = (int)b;

                    bufInt ^= 0xFF;

                    buf = (char)bufInt;
                    DecryptedString = DecryptedString + buf;
                }

                FileName = DecryptedString;

                Console.WriteLine(FileName);

                fileSize = reader.ReadUInt32();
                Filedata = reader.ReadBytes(fileSize);
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

        public Header header = new Header();
        /** Header */

        public DirInfo[] Directories;

        public List<FileInfo> Files = new List<FileInfo>();
        /** Sequentially, a file description block for every file stored inside the data file. */

        public DataFile(string filepath) : this(new Reader(filepath))
        { }

        public DataFile(Reader reader)
        {
            int DataFileSize = (int)reader.BaseStream.Length;

            int headerSize = reader.ReadInt32();
            Console.WriteLine(headerSize);

            int dircount = reader.ReadUInt16();
            Console.WriteLine(dircount);

            Directories = new DirInfo[dircount];

            for (int d = 0; d < dircount; d++)
            {
                Directories[d] = new DirInfo(reader);
            }

            for (int d = 0; d < dircount; d++)
            {
                if ((d + 1) < Directories.Count())
                {
                    while (reader.Pos < Directories[d + 1].Address && !reader.IsEof)
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
        }

        public void Write(Writer writer)
        {

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
