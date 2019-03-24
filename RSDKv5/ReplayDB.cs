using System;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class ReplayDB
    {

        public class Header
        {
            public Header()
            {

            }

            public Header(Reader reader)
            {

            }

            public void Write(Writer writer)
            {

            }
        }

        public ReplayDB()
        {

        }

        public ReplayDB(Reader reader)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            {
                using (ZOutputStream decompress = new ZOutputStream(outMemoryStream))
                {
                    decompress.Write(reader.ReadBytes(reader.BaseStream.Length), 0, (int)reader.BaseStream.Length);
                    decompress.finish();
                    reader.Close();
                }

               // Reader creader = new Reader(outMemoryStream);

                //creader.Close();
            }
        }

        public void Write(Writer writer)
        {

        }

    }
}
