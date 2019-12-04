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

            for (int i = 0; i < creader.BaseStream.Length; i++)
            {
                data[i] = creader.ReadByte();
            }

            creader.Close();

            Writer writer = new Writer("Replay.bin");
            writer.Write(data);
            writer.Close();
        }

        public void Write(Writer writer)
        {

        }

    }
}
