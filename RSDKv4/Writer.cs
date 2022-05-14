using System.Text;
using System.IO;

namespace RSDKv4
{
    public class Writer : BinaryWriter
    {
        public Writer(Stream stream) : base(stream) { }

        public Writer(string file) : base(File.Open(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) { }

        public void Seek(long position, SeekOrigin org)
        {
            BaseStream.Seek(position, org);
        }

        public void WriteStringRSDK(string str)
        {
            base.Write((byte)str.Length);
            base.Write(new UTF8Encoding().GetBytes(str));
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
