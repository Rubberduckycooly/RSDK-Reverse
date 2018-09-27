using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using zlib;

namespace RSDKv2
{
    public class Reader : BinaryReader
    {
        public Reader(Stream stream) : base(stream)
        {
        }

        public Reader(string file) : base(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        public byte[] ReadBytes(long count)
        {
            if (count < 0 || count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("requested " + count + " bytes, while only non-negative int32 amount of bytes possible");
            byte[] bytes = base.ReadBytes((int)count);
            if (bytes.Length < count)
                throw new EndOfStreamException("requested " + count + " bytes, but got only " + bytes.Length + " bytes");
            return bytes;
        }

        public byte[] ReadBytes(ulong count)
        {
            if (count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("requested " + count + " bytes, while only non-negative int32 amount of bytes possible");
            int cnt = (int)count;
            byte[] bytes = base.ReadBytes(cnt);
            if (bytes.Length < cnt)
                throw new EndOfStreamException("requested " + count + " bytes, but got only " + bytes.Length + " bytes");
            return bytes;
        }

        public bool IsEof
        {
            get { return BaseStream.Position >= BaseStream.Length; }
        }
        
        public void Seek(long position, SeekOrigin org)
        {
            BaseStream.Seek(position, org);
        }
        
        public long Pos
        {
            get { return BaseStream.Position; }
        }
        
        public long Size
        {
            get { return BaseStream.Length; }
        }
        
        public uint ReadUInt32BE()
        {
            byte[] bytes = ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public string GetFilename()
        {
            var fileStream = BaseStream as FileStream;
            return fileStream.Name;
        }

        public string ReadRSDKString()
        {
            return new UTF8Encoding().GetString(ReadBytes(this.ReadByte()));
        }

        public string ReadRSDKUnicodeString()
        {
            return new UnicodeEncoding().GetString(ReadBytes(this.ReadUInt16() * 2));
        }

        public byte[] ReadCompressed()
        {
            uint compresed_size = this.ReadUInt32();
            uint uncompressed_size = this.ReadUInt32BE();
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream decompress = new ZOutputStream(outMemoryStream))
            {
                decompress.Write(this.ReadBytes(compresed_size - 4), 0, (int)compresed_size - 4);
                decompress.finish();
                return outMemoryStream.ToArray();
            }
        }

        public Reader GetCompressedStream()
        {
            return new Reader(new MemoryStream(this.ReadCompressed()));
        }
    }
}
