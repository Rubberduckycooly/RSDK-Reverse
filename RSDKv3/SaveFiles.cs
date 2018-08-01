using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKv3
{
    class SaveFiles
    {
        class SaveData
        {
            byte CurrentLevel;
            byte Unknown;
            byte Lives;

            public SaveData(Stream stream) : this(new Reader(stream))
            {
            }

            public SaveData(string file) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
            }

            internal SaveData(Reader reader)
            {
                CurrentLevel = reader.ReadByte();
                Unknown = reader.ReadByte();
                Lives = reader.ReadByte();
            }

            public void Write(string filename)
            {
                using (Writer writer = new Writer(filename))
                    this.Write(writer);
            }

            public void Write(System.IO.Stream stream)
            {
                using (Writer writer = new Writer(stream))
                    this.Write(writer);
            }

            internal void Write(Writer writer)
            {
                writer.Write(CurrentLevel);
                writer.Write(Unknown);
                writer.Write(Lives);
            }
        }

        SaveData[] Saves = new SaveData[4];

        public SaveFiles(Stream stream) : this(new Reader(stream))
        {
        }

        public SaveFiles(string file) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        internal SaveFiles(Reader reader)
        {
            for (int i = 0; i < 4; i++)
            {
                Saves[i] = new SaveData(reader);
            }
            reader.Close();
        }

                    public void Write(string filename)
            {
                using (Writer writer = new Writer(filename))
                    this.Write(writer);
            }

            public void Write(System.IO.Stream stream)
            {
                using (Writer writer = new Writer(stream))
                    this.Write(writer);
            }

            internal void Write(Writer writer)
            {
            for (int i = 0; i < 4; i++)
            {
                Saves[i].Write(writer);
            }
            writer.Close();
        }
    }
}
