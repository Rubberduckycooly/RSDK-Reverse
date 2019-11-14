using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKv2
{
    public class ArcContainer
    {

        public class FileInfo
        {
            public string Name = "";
            public int FileSize = 0;
            public int Offset = 0;
            public byte[] FileData;

            public FileInfo()
            {

            }
        }

        public static readonly byte[] MAGIC = new byte[] { (byte)'A', (byte)'R', (byte)'C', (byte)'L' };

        int ARCKey = 0;

        public List<FileInfo> Files = new List<FileInfo>();

        public ArcContainer()
        {

        }

        public ArcContainer(Reader reader)
        {
            string FileName = System.IO.Path.GetFileNameWithoutExtension(reader.GetFilename());

            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
            {
                Console.WriteLine("This isn't an ARC file! aborting!");
                return;
            }

            ushort Filecount = reader.ReadUInt16();

            byte[] FileData = reader.ReadBytes(0x28 * Filecount);

            ARCKey = Filecount;

            for (int f = 0; f < Filecount; f++)
            {
                FileInfo file = new FileInfo();
                byte[] FilePtr = new byte[4];
                byte[] FilePtr2 = new byte[4];
                for (int i = 0; i < 0x28; i++)
                {
                    byte Buffer = FileData[i + (f * 0x28)];
                    byte DecryptedByte = (byte)(DecryptARCFileInfo() % 0xFF ^ Buffer);
                    if (i < 32)
                    {
                        file.Name += (char)DecryptedByte;
                    }
                    else if (i >= 32 && i < 36)
                    {
                        FilePtr[i - 32] = DecryptedByte;
                    }
                    else
                    {
                        FilePtr2[i - 36] = DecryptedByte;
                    }
                }
                file.Name = file.Name.Replace("\0", "");
                file.Offset = BitConverter.ToInt32(FilePtr, 0);
                file.FileSize = BitConverter.ToInt32(FilePtr2, 0);
                long ReadPos = reader.BaseStream.Position;
                reader.Seek(file.Offset, SeekOrigin.Begin);
                file.FileData = reader.ReadBytes(file.FileSize);
                reader.Seek(ReadPos, SeekOrigin.Begin);
                Files.Add(file);
            }

            reader.Close();

        }

        public void Unpack(string directory)
        {
            for (int i = 0; i < Files.Count; i++)
            {
                if (!Directory.Exists(directory + Files[i].Name.Replace(Path.GetFileName(Files[i].Name),"")))
                {
                    Directory.CreateDirectory(directory + Files[i].Name.Replace(Path.GetFileName(Files[i].Name), ""));
                }
                File.WriteAllBytes(directory + Files[i].Name, Files[i].FileData);
            }
        }

        int DecryptARCFileInfo()
        {
            int v1;

            v1 = 48271 * (ARCKey % 44488) - 3399 * (ARCKey / 44488);
            if (v1 <= 0)
                ARCKey = v1 + 0x7FFFFFFF;
            else
                ARCKey = 48271 * (ARCKey % 44488) - 3399 * (ARCKey / 44488);
            return ARCKey;
        }

        public void Write(Writer writer)
        {

        }

    }
}
