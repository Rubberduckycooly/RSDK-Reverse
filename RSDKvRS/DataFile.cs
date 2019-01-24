using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
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

            public void Write(Writer writer)
            {
                writer.Write(Directory);
                writer.Write(Address);
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
            public byte DirID = 0;

            public FileInfo()
            {
            }

            public FileInfo(Reader reader)
            {
                FileName = reader.ReadString();
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

            public void Write(Writer writer)
            {
                writer.Write(FileName);
                writer.Write((uint)Filedata.Length);
                writer.Write(Filedata);
            }
        }

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
        {

        }

        public DataFile(string filepath): this(new Reader(filepath))
        {}

        public DataFile(Reader reader)
        {

            int DirBlockSize = reader.ReadInt32();
            Console.WriteLine("Directory Size: " + DirBlockSize);

            int dircount = reader.ReadByte();
            Console.WriteLine("Directory Count: " + dircount);

            Directories = new List<DirInfo>();

            for (int d = 0; d < dircount; d++)
            {
                Directories.Add(new DirInfo(reader));
            }

            Console.WriteLine("Pos: " + reader.BaseStream.Position);

            for (int d = 0; d < dircount; d++)
            {
                if ((d+1) < Directories.Count())
                {
                    while (reader.Pos - DirBlockSize < Directories[d + 1].Address && !reader.IsEof)
                    {
                        FileInfo f = new FileInfo(reader);
                        f.DirID = (byte)d;
                        f.FullFileName = Directories[d].Directory + f.FileName;
                        Files.Add(f);
                    }
                }
                else
                {
                    while (!reader.IsEof)
                    {
                        FileInfo f = new FileInfo(reader);
                        f.DirID = (byte)d;
                        f.FullFileName = Directories[d].Directory + f.FileName;
                        Files.Add(f);
                    }
                }
            }

            Console.WriteLine("File count: " + Files.Count);
        }

        public void Write(Writer writer)
        {

            int DirHeaderSize = 0;

            writer.Write(DirHeaderSize);

            writer.Write((byte)Directories.Count);

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

            writer.Write((byte)Directories.Count);

            for (int i = 0; i < Directories.Count; i++)
            {
                Directories[i].Write(writer);
            }

            writer.Close();
        }

        public void WriteFile(int fileID)
        {
            Files[fileID].Write("");
        }

        public void WriteFile(string fileName, string NewFileName)
        {
            foreach(FileInfo f in Files)
            {
                if (f.FileName == fileName)
                {
                    f.Write(NewFileName);
                }
            }
        }

    }
}
