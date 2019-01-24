using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class ArcContainer
    {

        public static readonly byte[] MAGIC = new byte[] { (byte)'A', (byte)'R', (byte)'C', (byte)'L' };

        string FileName = "File";

        public ArcContainer()
        {

        }

        public ArcContainer(Reader reader)
        {
            FileName = System.IO.Path.GetFileNameWithoutExtension(reader.GetFilename());

            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
            {
                Console.WriteLine("This isn't an ARC file! aborting!");
                return;
            }

            ushort unknown = reader.ReadUInt16();



        }

        public void Write(Writer writer)
        {

        }

    }
}
