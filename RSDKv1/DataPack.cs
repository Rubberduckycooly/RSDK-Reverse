using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv1
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
                name = reader.ReadStringRSDK();
                offset = reader.ReadInt32();
            }

            public void Write(Writer writer)
            {
                writer.WriteStringRSDK(name);
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
            public byte directoryID = 0;

            public File() { }

            public File(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name = reader.ReadString();
                data = reader.ReadBytes((int)reader.ReadUInt32());
            }

            public void Write(Writer writer)
            {
                writer.Write(name);
                writer.Write((uint)data.Length);
                writer.Write(data);
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
            int dircount = reader.ReadByte();

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
            writer.Write((byte)directories.Count);

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
            writer.Write((byte)directories.Count);

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
