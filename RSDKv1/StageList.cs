using System;
using System.Collections.Generic;
using System.IO;

namespace RSDKv1
{
    public class StageList
    {
        public List<StageInfo> list = new List<StageInfo>();

        public class StageInfo
        {
            /// <summary>
            /// the stage name
            /// </summary>
            public string name = "STAGE";
            /// <summary>
            /// the folder of the stage
            /// </summary>
            public string folder = "Folder";
            /// <summary>
            /// the stage's identifier (E.G Act1 or Act2)
            /// </summary>
            public string id = "1";
            /// <summary>
            /// Determines if the stage is highlighted on the menu
            /// </summary>
            public bool highlighted = false;

            public StageInfo() { }

            public StageInfo(StreamReader reader)
            {
                name = readString(reader);
                folder = readString(reader);
                id = readString(reader);
                highlighted = readString(reader) != "0";

                reader.ReadLine();
            }

            public void write(StreamWriter writer)
            {
                writeString(writer, name);
                writeString(writer, folder);
                writeString(writer, id);
                writeString(writer, highlighted ? "1" : "0");

                writer.Write('\r');
                writer.Write('\n');
            }

            private string readString(StreamReader reader)
            {
                string str = "";

                while (true)
                {
                    char buf = (char)reader.Read();
                    if (buf == '^')
                        break;
                    else
                        str += buf;
                }

                return str;
            }

            private void writeString(StreamWriter writer, string str)
            {
                for (int i = 0; i < str.Length; i++)
                    writer.Write(str[i]);
                writer.Write('^');
            }

        }

        public StageList() { }

        public StageList(string filename) : this(new StreamReader(File.OpenRead(filename))) { }

        public StageList(System.IO.Stream stream) : this(new StreamReader(stream)) { }

        public StageList(StreamReader reader)
        {
            read(reader);
        }

        public void read(StreamReader reader)
        {
            list.Clear();
            while (!reader.EndOfStream)
                list.Add(new StageInfo(reader));
            reader.Close();
        }

        public void write(string filename)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filename)))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
                write(stream);
        }

        public void write(StreamWriter writer)
        {
            foreach(StageInfo stage in list)
                stage.write(writer);

            writer.Close();
        }
    }
}
