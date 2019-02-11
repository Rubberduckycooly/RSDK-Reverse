using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvRS
{
    //*.mdf Files
    public class ZoneList
    {
        public class Level
        {
            /// <summary>
            /// the Stage's Name
            /// </summary>
            public string StageName = "";
            /// <summary>
            /// the folder of the Stage
            /// </summary>
            public string StageFolder = "";
            /// <summary>
            /// the Identifier after "Act" (like Act1 or Act2)
            /// </summary>
            public string ActNo = "";
            /// <summary>
            /// I have actually no idea, it's called unknown for a reason lol
            /// </summary>
            public string Unknown = "";

            public Level()
            {

            }

            public Level(StreamReader reader)
            {
                char buf = 'n';

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        StageName = StageName + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        StageFolder = StageFolder + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        ActNo = ActNo + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        Unknown = Unknown + buf;
                    }
                }
                buf = 'n';
                reader.ReadLine();

            }

            public void write(StreamWriter writer)
            {
                for (int i = 0; i < StageName.Length; i++)
                {
                    writer.Write(StageName[i]);
                }
                writer.Write('^');
                for (int i = 0; i < StageFolder.Length; i++)
                {
                    writer.Write(StageFolder[i]);
                }
                writer.Write('^');
                for (int i = 0; i < ActNo.Length; i++)
                {
                    writer.Write(ActNo[i]);
                }
                writer.Write('^');
                for (int i = 0; i < Unknown.Length; i++)
                {
                    writer.Write(Unknown[i]);
                }
                writer.Write('^');
                writer.WriteLine();

            }

        }

        public List<Level> Stages = new List<Level>();

        public ZoneList()
        {
            Stages.Add(new Level());
        }

        public ZoneList(string filename) : this(new StreamReader(File.OpenRead(filename)))
        {

        }

        public ZoneList(System.IO.Stream stream) : this(new StreamReader(stream))
        {

        }

        public ZoneList(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                Stages.Add(new Level(reader));
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filename)))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            this.Write(stream);
        }

        internal void Write(StreamWriter writer)
        {
            for (int i = 0; i < Stages.Count; i++)
            {
                Stages[i].write(writer);
            }
            writer.Close();
        }
    }
}
