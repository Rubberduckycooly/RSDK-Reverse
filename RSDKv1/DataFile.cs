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
                directory = reader.readRSDKString();
                startOffset = reader.ReadInt32();
            }

            public void write(Writer writer)
            {
                writer.writeRSDKString(directory);
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
            public byte directoryID = 0;

            public FileInfo() { }

            public FileInfo(Reader reader)
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                fileName = reader.ReadString();
                fileData = reader.ReadBytes((int)reader.ReadUInt32());
            }

            public void write(Writer writer)
            {
                writer.Write(fileName);
                writer.Write((uint)fileData.Length);
                writer.Write(fileData);
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
            int dircount = reader.ReadByte();

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
            writer.Write((byte)directories.Count);

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
            writer.Write((byte)directories.Count);

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
