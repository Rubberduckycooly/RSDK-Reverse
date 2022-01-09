using System.Text;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class Writer : BinaryWriter
    {
        public Writer(Stream stream) : base(stream) { }

        public Writer(string file) : base(File.Open(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) { }

        public void seek(long position, SeekOrigin org)
        {
            BaseStream.Seek(position, org);
        }

        public void writeUInt32BE(uint val)
        {
            val = ((val >> 24) & 0xff) | ((val << 8) & 0xff0000) | ((val >> 8) & 0xff00) | ((val << 24) & 0xff000000);
            base.Write(val);
        }

        public void writeBool32(bool val)
        {
            base.Write(val ? 0x01 : 0x00);
        }

        public void writeRSDKString(string val)
        {
            base.Write((byte)val.Length);
            base.Write(new UTF8Encoding().GetBytes(val));
        }

        public void writeRSDKUTF16String(string val)
        {
            base.Write((ushort)val.Length);
            base.Write(new UnicodeEncoding().GetBytes(val));
        }

        public void writeCompressed(byte[] bytes)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream compress = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION)) {
                compress.Write(bytes, 0, bytes.Length);
                compress.finish();

                byte[] data = outMemoryStream.ToArray();
                this.Write((uint)data.Length + sizeof(uint));
                this.writeUInt32BE((uint)bytes.Length);
                this.Write(data);
            }
        }

        public void writeCompressedRaw(byte[] bytes)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream compress = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION))
            {
                compress.Write(bytes, 0, bytes.Length);
                compress.finish();

                byte[] data = outMemoryStream.ToArray();
                Write(data);
            }
        }
    }
}
