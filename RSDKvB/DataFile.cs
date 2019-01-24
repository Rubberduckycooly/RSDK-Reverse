using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSDKvB
{
    public class DataFile
    {
        public class Header
        {
            public static readonly byte[] MAGIC = new byte[] { (byte)'R', (byte)'S', (byte)'D', (byte)'K',(byte)'v', (byte)'B'};
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

            /// <summary>
            /// A list of extension types (used if the filename is unknown)
            /// </summary>
            public enum ExtensionTypes
            {
                UNKNOWN,
                OGG,
                WAV,
                R3D,
                PNG,
                GIF,
            }

            /// <summary>
            /// filename of the file
            /// </summary>
            public string FileName;
            /// <summary>
            /// filename of the file (but as an MD5 string)
            /// </summary>
            public string MD5FileName;
            /// <summary>
            /// stored MD5 hash
            /// </summary>
            byte[] md5Hash = new byte[16];

            /// <summary>
            /// the offset of the file data
            /// </summary>
            public uint DataOffset;
            /// <summary>
            /// the filesize of the file (in bytes)
            /// </summary>
            public uint FileSize;
            /// <summary>
            /// whether the file is encrypted or not
            /// </summary>
            public bool Encrypted;

            /// <summary>
            /// an array of the filedata
            /// </summary>
            public byte[] Filedata;

            /// <summary>
            /// the extension of the file
            /// </summary>
            public ExtensionTypes Extension = ExtensionTypes.UNKNOWN;

            /// <summary>
            /// a string  of bytes used for decryption/encryption
            /// </summary>
            static byte[] encryptionStringA = new byte[16];
            /// <summary>
            /// another string  of bytes used for decryption/encryption
            /// </summary>
            static byte[] encryptionStringB = new byte[16];

            static int eStringNo;
            static int eStringPosA;
            static int eStringPosB;
            static int eNybbleSwap;

            public FileInfo()
            {

            }

            public FileInfo(Reader reader, List<string> FileList, int cnt)
            {
                for (int y = 0; y < 16; y += 4)
                {
                    md5Hash[y + 3] = reader.ReadByte();
                    md5Hash[y + 2] = reader.ReadByte();
                    md5Hash[y + 1] = reader.ReadByte();
                    md5Hash[y] = reader.ReadByte();
                }
                MD5FileName = ConvertByteArrayToString(md5Hash);

                var md5 = MD5.Create();

                FileName = (cnt + 1) + ".bin"; //Make a base name

                for (int i = 0; i < FileList.Count; i++)
                {
                    // Mania Hashes all Strings at Lower Case
                    string fp = FileList[i].ToLower();

                    bool match = true;

                    for (int z = 0; z < 16; z++)
                    {
                        if (CalculateMD5Hash(fp)[z] != md5Hash[z])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        FileName = FileList[i];
                        break;
                    }

                }

                DataOffset = reader.ReadUInt32();
                uint tmp = reader.ReadUInt32();

                Encrypted = (tmp & 0x80000000) != 0;
                FileSize = (tmp & 0x7FFFFFFF);

                long tmp2 = reader.BaseStream.Position;
                reader.BaseStream.Position = DataOffset;

                Filedata = reader.ReadBytes(FileSize);

                // Decrypt File if Encrypted
                if (Encrypted)
                    Filedata = Decrypt(Filedata);

                reader.BaseStream.Position = tmp2;

                Extension = GetExtensionFromData();

                if (FileName == (cnt + 1) + ".bin")
                {
                    switch (Extension)
                    {
                        case ExtensionTypes.GIF:
                            FileName = "Sprite" + (cnt + 1) + ".gif";
                            break;
                        case ExtensionTypes.R3D:
                            FileName = "Model" + (cnt + 1) + ".bin";
                            break;
                        case ExtensionTypes.OGG:
                            FileName = "Music" + (cnt + 1) + ".ogg";
                            break;
                        case ExtensionTypes.PNG:
                            FileName = "Image" + (cnt + 1) + ".png";
                            break;
                        case ExtensionTypes.WAV:
                            FileName = "SoundEffect" + (cnt + 1) + ".wav";
                            break;
                        case ExtensionTypes.UNKNOWN:
                            FileName = "UnknownFileType" + (cnt + 1) + ".bin";
                            break;
                    }
                }
                md5.Dispose();
            }

            public void WriteFileHeader(Writer writer)
            {
                writer.Write(CalculateMD5Hash(FileName.ToLower()));
                writer.Write(DataOffset);
                writer.Write(FileSize | (Encrypted ? 0x80000000 : 0));
            }

            public void WriteFileData(Writer writer)
            {
                if (Encrypted)
                    writer.Write(Decrypt(Filedata));
                else
                    writer.Write(Filedata);
            }

            public void Write(string Datadirectory)
            {
                string tmpcheck = "";
                for (int i = 0; i < 4; i++)
                {
                    tmpcheck = tmpcheck + FileName[i];
                }

                //Do we know the filename of the file?
                if (tmpcheck != "Data" && tmpcheck != "Byte")
                {
                    string directory = Path.Combine(Datadirectory, "Unknown Files");
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    File.WriteAllBytes(Path.Combine(directory, FileName), Filedata);
                }
                else //We do! now do normal stuff!
                {

                    string dir = Path.Combine(Datadirectory, FileName.Replace(Path.GetFileName(FileName), ""));
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    File.WriteAllBytes(Path.Combine(Datadirectory, FileName), Filedata);
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

            public void GenerateKey(out byte[] Buffer, uint Value)
            {
                string strbuf;
                byte[] md5Buf = new byte[16];

                strbuf = Value.ToString();

                md5Buf = CalculateMD5Hash(strbuf);

                Buffer = new byte[16];

                for (int y = 0; y < 16; y += 4)
                {
                    // convert every 32-bit word to Little Endian
                    Buffer[y + 3] = md5Buf[y + 0];
                    Buffer[y + 2] = md5Buf[y + 1];
                    Buffer[y + 1] = md5Buf[y + 2];
                    Buffer[y + 0] = md5Buf[y + 3];
                }
                
                return;
            }

            void GenerateELoadKeys(uint Arg1, uint Arg2, uint VSize)
            {
                GenerateKey(out encryptionStringA, Arg1);
                GenerateKey(out encryptionStringB, Arg2);

                eStringNo = (int)(VSize / 4) & 0x7F;
                eStringPosA = 0;
                eStringPosB = 8;
                eNybbleSwap = 0;
            }
            byte[] Decrypt(byte[] data)
            {
                // Note: Since only XOr is used, this function does both,
                //       decryption and encryption.

                GenerateELoadKeys(FileSize, (FileSize >> 1) + 1, FileSize);

                const uint ENC_KEY_2 = 0x24924925;
                const uint ENC_KEY_1 = 0xAAAAAAAB;

                int TempByt;
                int Key1;
                int Key2;
                int Temp1;
                int Temp2;

                byte[] ReturnData = new byte[data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    TempByt = eStringNo ^ encryptionStringB[eStringPosB];
                    TempByt ^= data[i];
                    if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                    {
                        TempByt = ((TempByt << 4) + (TempByt >> 4)) & 0xFF;
                    }
                    TempByt ^= encryptionStringA[eStringPosA];
                    ReturnData[i] = (byte)TempByt;

                    eStringPosA++;
                    eStringPosB++;

                    if (eStringPosA <= 0x0F)
                    {
                        if (eStringPosB > 0x0C)
                        {
                            eStringPosB = 0;
                            eNybbleSwap ^= 0x01;
                        }
                    }
                    else if (eStringPosB <= 0x08)
                    {
                        eStringPosA = 0;
                        eNybbleSwap ^= 0x01;
                    }
                    else
                    {
                        eStringNo += 2;
                        eStringNo &= 0x7F;

                        if (eNybbleSwap != 0)
                        {
                            Key1 = MulUnsignedHigh(ENC_KEY_1, eStringNo);
                            Key2 = MulUnsignedHigh(ENC_KEY_2, eStringNo);
                            eNybbleSwap = 0;

                            Temp1 = Key2 + (eStringNo - Key2) / 2;
                            Temp2 = Key1 / 8 * 3;

                            eStringPosA = eStringNo - Temp1 / 4 * 7;
                            eStringPosB = eStringNo - Temp2 * 4 + 2;
                        }
                        else
                        {
                            Key1 = MulUnsignedHigh(ENC_KEY_1, eStringNo);
                            Key2 = MulUnsignedHigh(ENC_KEY_2, eStringNo);
                            eNybbleSwap = 1;

                            Temp1 = Key2 + (eStringNo - Key2) / 2;
                            Temp2 = Key1 / 8 * 3;

                            eStringPosB = eStringNo - Temp1 / 4 * 7;
                            eStringPosA = eStringNo - Temp2 * 4 + 3;
                        }
                    }
                }
                return ReturnData;
            }

            public int MulUnsignedHigh(uint arg1, int arg2)
            {
                return (int)(((ulong)arg1 * (ulong)arg2) >> 32);
            }

            public ExtensionTypes GetExtensionFromData()
            {
                byte[] header = new byte[5];

                for (int i = 0; i < header.Length; i++)
                {
                    header[i] = Filedata[i];
                }

                if (header[0] == (byte)'O' && header[1] == (byte)'g' && header[2] == (byte)'g' && header[3] == (byte)'S')
                {
                    return ExtensionTypes.OGG;
                }

                if (header[0] == (byte)'G' && header[1] == (byte)'I' && header[2] == (byte)'F')
                {
                    return ExtensionTypes.GIF;
                }

                if (header[0] == (byte)'R' && header[1] == (byte)'3' && header[2] == (byte)'D' && header[3] == (byte)'\0')
                {
                    return ExtensionTypes.R3D;
                }

                if (header[1] == (byte)'P' && header[2] == (byte)'N' && header[3] == (byte)'G')
                {
                    return ExtensionTypes.PNG;
                }

                if (header[0] == (byte)'R' && header[1] == (byte)'I' && header[2] == (byte)'F' && header[3] == (byte)'F')
                {
                    return ExtensionTypes.WAV;
                }

                return ExtensionTypes.UNKNOWN;
            }
        }

        public Header header;
        /** Header */

        public List<FileInfo> Files = new List<FileInfo>();
        /** Sequentially, a file description block for every file stored inside the data file. */

        public DataFile()
        {
        }

        public DataFile(string filepath, List<string> FileList) : this(new Reader(filepath), FileList)
        { }

        public DataFile(Reader reader, List<string> FileList)
        {
            header = new Header(reader); //read the header data

            Console.WriteLine("File Count = " + header.FileCount);

            for (int i = 0; i < header.FileCount; i++)
            {
                Files.Add(new FileInfo(reader,FileList,i)); //read each file's header
            }

        }

        public void Write(Writer writer)
        {
            header.FileCount = (ushort)Files.Count; //get the file count

            //firstly we setout the file
            //write a bunch of blanks

            header.Write(writer); //write the header

            foreach (FileInfo f in Files) //write each file's header
            {
                f.WriteFileHeader(writer); //write our file header data
            }

            foreach (FileInfo f in Files) //Write "Filler Data"
            {
                f.DataOffset = (uint)writer.BaseStream.Position; //get our file data offset
                byte[] b = new byte[f.FileSize]; //load up a set of blanks with the same size as the original set
                writer.Write(b); //fill the file up with blank data
            }

            //now we really write our data!

            writer.BaseStream.Position = 0; //jump back to the start of the file

            header.Write(writer); //re-write our header

            foreach (FileInfo f in Files) //for each file
            {
                f.WriteFileHeader(writer); //write our header
                long Tmp = writer.BaseStream.Position; //get our writer pos for later
                writer.BaseStream.Position = f.DataOffset; //jump to our saved offset
                f.WriteFileData(writer); //write our file data
                writer.BaseStream.Position = Tmp; //jump back ready to write the next file!
            }

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
