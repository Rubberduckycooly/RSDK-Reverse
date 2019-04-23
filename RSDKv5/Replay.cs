using System;
using System.Collections.Generic;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class Replay
    {
        public List<Position> positions = new List<Position>();
        public Replay()
        {

        }

        public Replay(Reader reader)
        {
            Reader creader = reader.GetCompressedStreamRaw();
            //creader.BaseStream.Position = 0x84;

            while (creader.BaseStream.Position + 8 < creader.BaseStream.Length)
            {
                positions.Add(new Position(creader));
            }

            creader.Close();
        }

        public void Write(Writer writer)
        {

        }

    }
}
