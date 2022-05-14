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
            /// the stage's identifier (E.G Act'1' or Act'2')
            /// </summary>
            public string id = "1";

            /// <summary>
            /// Determines if the stage is highlighted on the menu
            /// </summary>
            public bool highlighted = false;

            public StageInfo() { }

            public StageInfo(StreamReader reader)
            {
                Read(reader);
            }

            public void Read(StreamReader reader)
            {
                name = ReadString(reader);
                folder = ReadString(reader);
                id = ReadString(reader);
                highlighted = ReadString(reader) != "0";

                reader.ReadLine();
            }

            public void Write(StreamWriter writer)
            {
                WriteString(writer, name);
                WriteString(writer, folder);
                WriteString(writer, id);
                WriteString(writer, highlighted ? "1" : "0");

                writer.Write('\r');
                writer.Write('\n');
            }

            private string ReadString(StreamReader reader)
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

            private void WriteString(StreamWriter writer, string str)
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
            Read(reader);
        }

        public void Read(StreamReader reader)
        {
            list.Clear();
            while (!reader.EndOfStream)
                list.Add(new StageInfo(reader));

            reader.Close();
        }

        public void Write(string filename)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filename)))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
                Write(stream);
        }

        public void Write(StreamWriter writer)
        {
            foreach (StageInfo stage in list)
                stage.Write(writer);

            writer.Close();
        }
    }
}
