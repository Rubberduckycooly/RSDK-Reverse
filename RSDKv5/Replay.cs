using System;
using System.Collections.Generic;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class Replay
    {
        public Replay()
        {

        }

        public Replay(Reader reader)
        {
            Reader creader = reader.GetCompressedStreamRaw();
            //creader.BaseStream.Position = 0x84;

            byte[] data = new byte[creader.BaseStream.Length];
            int[] data2 = new int[creader.BaseStream.Length / 4];

            for (int i = 0; i < creader.BaseStream.Length; i++)
            {
                data[i] = creader.ReadByte();
            }

            creader.BaseStream.Position = 0;
            for (int i = 0; i < creader.BaseStream.Length/4; i++)
            {
                data2[i] = creader.ReadInt32();
            }

            creader.Close();

            //data[0]: Header
            //data[1]: Header
            //data[2]: Packed Flag
            //data[3]: Header
            //data[5]: EntryCount

            //data[11]: ???

            //Entries Start: 14/0xE
            //Entry Size: 28/0x1C (bytes?)

            Writer writer = new Writer("Replay.bin");
            writer.Write(data);
            writer.Close();
        }

        public void Write(Writer writer)
        {

        }

    }
}
