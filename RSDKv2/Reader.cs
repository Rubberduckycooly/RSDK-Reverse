using System;
using System.Text;
using System.IO;

namespace RSDKv2
{
    public class Reader : BinaryReader
    {
        public Reader(Stream stream) : base(stream) { }

        public Reader(string file) : base(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read)) { }

        public byte[] ReadBytes(long count)
        {
            if (count < 0 || count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException($"requested {count} bytes, while only non-negative int32 amount of bytes possible");

            byte[] bytes = base.ReadBytes((int)count);

            if (bytes.Length < count)
                throw new EndOfStreamException($"requested {count} bytes, but got only {bytes.Length} bytes");

            return bytes;
        }

        public bool isEof
        {
            get { return BaseStream.Position >= BaseStream.Length; }
        }

        public string ReadStringRSDK()
        {
            return new UTF8Encoding().GetString(ReadBytes(this.ReadByte()));
        }
        public void Seek(long position, SeekOrigin org)
        {
            BaseStream.Seek(position, org);
        }
    }
}
