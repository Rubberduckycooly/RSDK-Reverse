using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class ArcContainer
    {

        public class FileInfo
        {
            public byte[] Header = new byte[1];
            public byte[] FileData = new byte[1];

            public FileInfo()
            {

            }

            public FileInfo(Reader reader)
            {

            }

            public FileInfo(Writer writer)
            {

            }
        }

        public static readonly byte[] MAGIC = new byte[] { (byte)'A', (byte)'R', (byte)'C', (byte)'L' };

        string FileName = "File";

        public List<FileInfo> header = new List<FileInfo>();

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

            ushort Filecount = reader.ReadUInt16();

            for (int i = 0; i < Filecount; i++)
            {
                FileInfo f = new FileInfo();
                byte[] data = reader.ReadBytes(0x28);
                f.Header = data;
                header.Add(f);
            }

        }

        public void Write(Writer writer)
        {

        }

    }
}
