using System.Text;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class Writer : BinaryWriter
    {
        public Writer(Stream stream) : base(stream) { }

        public Writer(string file) : base(File.Open(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) { }

        public void Seek(long position, SeekOrigin org)
        {
            BaseStream.Seek(position, org);
        }

        public void WriteUInt32BE(uint val)
        {
            val = ((val >> 24) & 0xff) | ((val << 8) & 0xff0000) | ((val >> 8) & 0xff00) | ((val << 24) & 0xff000000);
            base.Write(val);
        }

        public void WriteBool32(bool val)
        {
            base.Write(val ? 0x01 : 0x00);
        }

        public void WriteStringRSDK(string val)
        {
            base.Write((byte)val.Length);
            base.Write(new UTF8Encoding().GetBytes(val));
        }

        public void WriteStringRSDK_UTF16(string val)
        {
            base.Write((ushort)val.Length);
            base.Write(new UnicodeEncoding().GetBytes(val));
        }

        public void WriteCompressed(byte[] bytes)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream compress = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION)) {
                compress.Write(bytes, 0, bytes.Length);
                compress.finish();

                byte[] data = outMemoryStream.ToArray();
                this.Write((uint)data.Length + sizeof(uint));
                this.WriteUInt32BE((uint)bytes.Length);
                this.Write(data);
            }
        }

        public void WriteCompressedRaw(byte[] bytes)
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

        public void WriteText(string str = "")
        {
            for (int i = 0; i < str.Length; ++i) Write((byte)str[i]);
        }

        public void WriteLine(string str = "", byte eolFormat = 0)
        {
            for (int i = 0; i < str.Length; ++i) Write((byte)str[i]);

            switch (eolFormat)
            {
                default:
                case 0: Write((byte)'\n'); break;

                case 1:
                    Write((byte)'\r');
                    Write((byte)'\n');
                    break;

                case 2: Write((byte)'\r'); break;
            }
        }
    }
}
