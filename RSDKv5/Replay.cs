using System;
using System.Collections.Generic;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class Replay
    {

        public class ReplayEntry
        {
            public int[] Data = new int[7];

            public ReplayEntry()
            {

            }
        }


        public int[] Header = new int[14];
        public List<Replay> Entries = new List<Replay>();

        public Replay()
        {

        }

        public Replay(Reader reader)
        {
            Reader creader = reader.GetCompressedStreamRaw();
            //creader.BaseStream.Position = 0x84;

            int[] data = new int[creader.BaseStream.Length / 4];

            creader.BaseStream.Position = 0;
            for (int i = 0; i < creader.BaseStream.Length/4; i++)
            {
                data[i] = creader.ReadInt32();
            }
            creader.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < 14; i++)
            {
                Header[i] = creader.ReadInt32();
            }

            for (int i = 0; i < Header[4]; i++)
            {
                ReplayEntry entry = new ReplayEntry();
                entry.Data[0] = creader.ReadInt32(); //Inputs
                entry.Data[1] = creader.ReadInt32(); //Flags
                entry.Data[2] = creader.ReadInt32(); //Fuckery
                entry.Data[3] = creader.ReadInt32();
            }

            //"CurrentEntity->ObjectID" is actually "Scene.Mode"

            //data[0]: Header
            //data[1]: ???
            //data[2]: Packed Flag
            //data[3]: ActiveBuffer
            //data[4]: EntryCount
            //data[5]: FrameCount
            //data[6]: ZoneID (MenuParam[92])
            //data[7]: ActID (MenuParam[93])
            //data[8]: PlayerID (MenuParam[91])
            //data[9]: PlusLayout
            //data[10]: Oscillation
            //data[11]: ???
            //data[12]: ???
            //data[13]: ???

            //Entries Start: 14/0xE
            //Entry Size: 28 (bytes) (7 ints)

            //Entry[2]: Inputs
            //Input & 0x01 = ??? (Up)
            //Input & 0x02 = ??? (Down)
            //Input & 0x04 = ??? (Left)
            //Input & 0x08 = ??? (Right)
            //Input & 0x10 = ??? (JumpPress)
            //Input & 0x20 = ??? (JumpHold)

            creader.Close();

            //Writer writer = new Writer("Replay.bin");
            //writer.Write(data);
            //writer.Close();
        }

        public void Write(Writer writer)
        {

        }

    }
}
