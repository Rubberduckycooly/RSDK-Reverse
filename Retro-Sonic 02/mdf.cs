using System;
using System.Collections.Generic;
using System.IO;

namespace RetroSonicV2
{
    public class mdf
    {
        public List<string> ZoneNames = new List<string>();
        public mdf()
        {

        }

        public mdf(StreamReader reader)
        {
            while(!reader.EndOfStream)
            {
                string ZoneName = "";
                char buf = 'n';

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        ZoneName = ZoneName + buf;
                    }
                }
                ZoneNames.Add(ZoneName);
                Console.WriteLine(ZoneName);
            }
        }

        public void Write(StreamWriter writer)
        {
            foreach(string Zname in ZoneNames)
            {
                writer.Write(Zname + "^");
            }
        }

    }
}
