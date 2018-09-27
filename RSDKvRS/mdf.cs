using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
{
    public class mdf
    {
        public class Level
        {
            public string StageName;
            public string StageFolder;
            public string ActNo;
            public string Unknown;
            public byte Unknown1;
            public byte Unknown2;

            public Level(Reader reader)
            {
                char buf = 'n';

                while (buf != '^')
                {
                    buf = reader.ReadChar();
                    if (buf == '^') { break; }
                    else
                    {
                        StageName = StageName + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = reader.ReadChar();
                    if (buf == '^') { break; }
                    else
                    {
                        StageFolder = StageFolder + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = reader.ReadChar();
                    if (buf == '^') { break; }
                    else
                    {
                        ActNo = ActNo + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = reader.ReadChar();
                    if (buf == '^') { break; }
                    else
                    {
                        Unknown = Unknown + buf;
                    }
                }
                buf = 'n';
                Unknown1 = reader.ReadByte();
                Unknown2 = reader.ReadByte();

            }

            public void write(Writer writer)
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

                writer.Write(Unknown1);
                writer.Write(Unknown2);

            }

        }

        public List<Level> Stages = new List<Level>();

        public mdf(string filename) : this(new Reader(filename))
        {

        }

        public mdf(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public mdf(Reader reader)
        {
            while (!reader.IsEof)
            {
                Stages.Add(new Level(reader));
            }
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
            for (int i = 0; i < Stages.Count; i++)
            {
                Stages[i].write(writer);
            }
        }
    }
}
