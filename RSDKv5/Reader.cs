using System;
using System.Text;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class Reader : BinaryReader
    {
        public Reader(Stream stream) : base(stream) { }

        public Reader(string file) : base(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read)) { }

        public byte[] readBytes(long count)
        {
            if (count < 0 || count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("requested " + count + " bytes, while only non-negative int32 amount of bytes possible");
            byte[] bytes = base.ReadBytes((int)count);
            if (bytes.Length < count)
                throw new EndOfStreamException("requested " + count + " bytes, but got only " + bytes.Length + " bytes");
            return bytes;
        }

        public byte[] readBytes(ulong count)
        {
            if (count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("requested " + count + " bytes, while only non-negative int32 amount of bytes possible");
            int cnt = (int)count;
            byte[] bytes = base.ReadBytes(cnt);
            if (bytes.Length < cnt)
                throw new EndOfStreamException("requested " + count + " bytes, but got only " + bytes.Length + " bytes");
            return bytes;
        }

        public bool isEof
        {
            get { return BaseStream.Position >= BaseStream.Length; }
        }
        public void seek(long position, SeekOrigin org)
        {
            BaseStream.Seek(position, org);
        }

        public uint readUInt32BE()
        {
            byte[] bytes = readBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public bool readBool32()
        {
            return base.ReadInt32() != 0;
        }

        public string readRSDKString()
        {
            return new UTF8Encoding().GetString(readBytes(this.ReadByte())).Replace("\0", "");
        }

        public string readRSDKUTF16String()
        {
            return new UnicodeEncoding().GetString(readBytes(this.ReadUInt16() * 2));
        }

        public byte[] readCompressed()
        {
            uint compresed_size = this.ReadUInt32();
            uint uncompressed_size = this.readUInt32BE();
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream decompress = new ZOutputStream(outMemoryStream))
            {
                decompress.Write(this.readBytes(compresed_size - 4), 0, (int)compresed_size - 4);
                decompress.finish();
                return outMemoryStream.ToArray();
            }
        }

        public byte[] readCompressedRaw()
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream decompress = new ZOutputStream(outMemoryStream))
            {
                decompress.Write(readBytes(BaseStream.Length), 0, (int)BaseStream.Length);
                decompress.finish();
                return outMemoryStream.ToArray();
            }
        }

        public Reader getCompressedStream()
        {
            return new Reader(new MemoryStream(this.readCompressed()));
        }

        public Reader getCompressedStreamRaw()
        {
            return new Reader(new MemoryStream(this.readCompressedRaw()));
        }
    }
}
