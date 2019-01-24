using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvB
{
    public class StringList
    {

        public class SingleString
        {
            /// <summary>
            /// what language the string is for
            /// </summary>
            public string Language = "en";
            /// <summary>
            /// the string identifier (what the games uses to call it)
            /// </summary>
            public string StringName = "RetroEngine";
            /// <summary>
            /// what the string actually says
            /// </summary>
            public string StringContents = "RSDK";
            /// <summary>
            /// if the string is a comment or not
            /// </summary>
            bool Comment = false;

            public SingleString()
            {

            }

            public SingleString(StreamReader reader)
            {
                Language = "";
                StringName = "";
                StringContents = "";

                char buf = 'n';
                while(buf != ':')
                {
                    buf = (char)reader.Read();
                    if (buf == ':') break;
                    if (buf == '/') { Comment = true; StringContents = StringContents + buf; break; }
                    Language = Language + buf;
                }

                if (Comment)
                {
                    string line = reader.ReadLine();
                    StringContents = StringContents + line;
                    if (!reader.EndOfStream) reader.ReadLine();
                    return;
                }

                buf = 'n';
                while (buf != ':')
                {
                    buf = (char)reader.Read();
                    if (buf == ':') break;
                    StringName = StringName + buf;
                }
                reader.ReadLine();
                reader.Read();

                bool Break = false;

                while(!Break)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("end string")) { Break = true; break; }
                    StringContents += line;
                    StringContents += Environment.NewLine;
                }
                if (!reader.EndOfStream) reader.ReadLine();
            }

            public void Write(StreamWriter writer, bool startWithNewLine = false)
            {
                if (!Comment)
                {
                    if (startWithNewLine) writer.WriteLine();
                    writer.Write(Language);
                    writer.Write(":");
                    writer.Write(StringName);
                    writer.Write(":");
                    writer.WriteLine();
                    writer.Write("\t");
                    writer.Write(StringContents);
                    writer.Write("end string");
                }
                else
                {
                    if (startWithNewLine) writer.WriteLine();
                    writer.Write(StringContents);
                }
            }
        }

        public List<SingleString> Strings = new List<SingleString>();

        public StringList()
        {

        }

        public StringList(StreamReader reader)
        {
            while (!reader.EndOfStream) Strings.Add(new SingleString(reader));
        }

        public void Write(StreamWriter writer)
        {
            for (int i = 0; i < Strings.Count; i++)
            {
                Strings[i].Write(writer, i != 0);
            }
        }

    }
}
