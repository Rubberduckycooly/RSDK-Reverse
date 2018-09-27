using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSDKv5
{
    public class DataFile
    {
        public class Header
        {
            public static readonly byte[] MAGIC = new byte[] { (byte)'R', (byte)'S', (byte)'D', (byte)'K',(byte)'v', (byte)'5'};
            public ushort FileCount;

            public Header(Reader reader)
            {
                if (!reader.ReadBytes(6).SequenceEqual(MAGIC))
                    throw new Exception("Invalid config file header magic");
                FileCount = reader.ReadUInt16();
            }

            public void Write(Writer writer)
            {
                writer.Write(MAGIC);
                writer.Write(FileCount);
            }
        }

        public class FileInfo
        {
            public string FileName;
            public string MD5FileName;
            byte[] md5Hash = new byte[16];

            public uint DataOffset;
            public uint fileSize;
            bool encrypted;

            public byte[] Filedata;

            public FileInfo(Reader reader, List<string> FileList, int cnt)
            {
                //FileName = reader.ReadString();

                for (int y = 0; y < 16; y += 4)
                {
                    md5Hash[y + 3] = reader.ReadByte();
                    md5Hash[y + 2] = reader.ReadByte();
                    md5Hash[y + 1] = reader.ReadByte();
                    md5Hash[y] = reader.ReadByte();
                    //Console.WriteLine(md5Hash[y] + " " + md5Hash[y + 1] + " " + md5Hash[y + 2] + " " + md5Hash[y + 3]);
                }
                MD5FileName = ConvertByteArrayToString(md5Hash);

                MD5 MD5Tool = MD5.Create();

                FileName = (cnt + 1) + ".bin"; //MD5FileName;

                for (int i = 0; i < FileList.Count; i++)
                {
                    //the string has to be in lowercase
                    string fp = FileList[i].ToLower();

                    int ok = 1;

                    for (int z = 0; z < 16; z++)
                    {
                        if (CalculateMD5Hash(fp)[z] != md5Hash[z])
                        {
                            ok = 0;
                            break;
                        }
                        
                    }

                    if (ok == 1)
                    {
                        FileName = FileList[i];
                        break;
                    }

                }
                DataOffset = reader.ReadUInt32();
                uint tmp = reader.ReadUInt32();

                if ((tmp & 0x80000000) == 0x80000000)
                {
                    encrypted = true;
                }

                fileSize = (tmp & 0x7FFFFFFF);

                //long tmp = reader.Pos;
                //reader.BaseStream.Position = DataOffset;
                //Filedata = reader.ReadBytes(fileSize);
                //reader.BaseStream.Position = tmp;
            }

            public void Write(string Datadirectory)
            {
                string tmpcheck = "";
                for (int i = 0; i < 4; i++)
                {
                    tmpcheck = tmpcheck + FileName[i];
                }
                System.IO.DirectoryInfo di;

                //Do we know the filename of the file?
                if (tmpcheck != "Data" && tmpcheck != "Byte")
                {
                    di = new System.IO.DirectoryInfo(Datadirectory + "//Unknown Files");
                    if (!di.Exists) di.Create();
                    Writer writer = new Writer(Datadirectory + "//Unknown Files//" + FileName);

                    if (Filedata != null)
                    writer.Write(Filedata);

                    writer.Close();
                }
                else //We do! now do normal stuff!
                {
                    string dir = FileName.Replace(Path.GetFileName(FileName), "");
                    di = new System.IO.DirectoryInfo(Datadirectory + "//" + dir);
                    if (!di.Exists) di.Create();
                    Writer writer = new Writer(Datadirectory + "//" + FileName);
                    if (Filedata != null)
                        writer.Write(Filedata);
                    writer.Close();
                }
            }

            private static string ConvertByteArrayToString(byte[] bytes)
            {
                var sb = new StringBuilder();
                foreach (var b in bytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }

            static string GetMd5Hash(MD5 md5Hash, string input)
            {

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }

            // Verify a hash against a string.
            static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(md5Hash, input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public byte[] CalculateMD5Hash(string input)
            {

                MD5 md5 = System.Security.Cryptography.MD5.Create();

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

                byte[] hash = md5.ComputeHash(inputBytes);

                return hash;

            }

        }

        public Header header;
        /** Header */

        public List<FileInfo> Files = new List<FileInfo>();
        /** Sequentially, a file description block for every file stored inside the data file. */

        public DataFile(string filepath, List<string> FileList) : this(new Reader(filepath), FileList)
        { }

        public DataFile(Reader reader, List<string> FileList)
        {
            header = new Header(reader);
            Console.WriteLine(header.FileCount);

            for (int i = 0; i < header.FileCount; i++)
            {
                Files.Add(new FileInfo(reader,FileList,i));
            }

            foreach (FileInfo f in Files)
            {
                reader.BaseStream.Position = f.DataOffset;
                f.Filedata = reader.ReadBytes(f.fileSize);
            }

        }

        public void Write(Writer writer)
        {

        }

        public void WriteFile(int fileID)
        {
            Files[fileID].Write("");
        }

        public void WriteFile(string fileName, string NewFileName)
        {
            foreach (FileInfo f in Files)
            {
                if (f.FileName == fileName)
                {
                    f.Write(NewFileName);
                }
            }
        }

    }
}
