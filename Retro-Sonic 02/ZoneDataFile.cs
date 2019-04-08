using System;
using System.Collections.Generic;
using System.IO;

namespace RetroSonicV2
{
    public class ZoneDataFile
    {
        //.zdf files
        public class ZoneData
        {
            public string ZoneFolder = "";
            public string ActID1 = "";
            public string ActID2 = "";
            public string ActID3 = "";

            public ZoneData()
            {
                ZoneFolder = "Stage";
                ActID1 = "1";
                ActID2 = "2";
                ActID3 = "3";
            }
        }

        public List<ZoneData> Zones = new List<ZoneData>();

        public ZoneDataFile()
        {

        }

        public ZoneDataFile(StreamReader reader)
        {

            while (!reader.EndOfStream)
            {
                ZoneData zd = new ZoneData();
                char buf = 'n';
                string ZoneFolder = "";
                string ActID = "";

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        ZoneFolder = ZoneFolder + buf;
                    }
                }
                zd.ZoneFolder = ZoneFolder;
                buf = 'n';

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        ActID = ActID + buf;
                    }
                }
                zd.ActID1 = ActID;
                buf = 'n';
                ActID = "";

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        ActID = ActID + buf;
                    }
                }
                zd.ActID2 = ActID;
                buf = 'n';
                ActID = "";

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        ActID = ActID + buf;
                    }
                }
                zd.ActID3 = ActID;
                buf = 'n';
                ActID = "";

                reader.Read();
                Zones.Add(zd);
                Console.WriteLine(zd.ZoneFolder + ", " + zd.ActID1 + ", " + zd.ActID2 + ", " + zd.ActID3 + Environment.NewLine);
            }
        }

        public void Write(StreamWriter writer)
        {
            foreach(ZoneData zoneData in Zones)
            {
                writer.Write(zoneData.ZoneFolder + "^" + zoneData.ActID1 + "^" + zoneData.ActID2 + "^" + zoneData.ActID3 + "^" + "_");
            }
        }

    }
}
