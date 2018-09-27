using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
{
    public class DataFile
    {
        public class DirInfo
        {
            public string Directory;

            public long Address;

            public DirInfo(Reader reader)
            {
                Directory = reader.ReadString();
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
                bool DontFlip = false;
                FileName = reader.ReadString();

                string ext = System.IO.Path.GetExtension(FileName);

                if (ext == ".ogg" || ext == ".wav")
                {
                    DontFlip = true;
                }
                Console.WriteLine(FileName + " Test " + DontFlip);
                if (DontFlip)
                {
                    fileSize = reader.ReadUInt32();
                    Filedata = reader.ReadBytes(fileSize);
                }
                else if (!DontFlip)
                {
                    fileSize = reader.ReadUInt32();
                    Filedata = reader.ReadBytes(fileSize);
                    for (int i = 0; i < Filedata.Length; i++)
                    {
                        byte b = (byte)~Filedata[i];
                        Filedata[i] = b;
                    }
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

            int dircount = reader.ReadByte();
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
