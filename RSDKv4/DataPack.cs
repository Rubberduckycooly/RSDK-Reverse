using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSDKv4
{
    public class DataPack
    {
        public class NameIdentifier
        {
            /// <summary>
            /// the MD5 hash of the name in bytes
            /// </summary>
            public byte[] hash;

            /// <summary>
            /// the name in plain text
            /// </summary>
            public string name = null;

            public bool usingHash = true;

            public NameIdentifier(string name)
            {
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    hash = md5.ComputeHash(new System.Text.ASCIIEncoding().GetBytes(name));
                }
                this.name = name;
                usingHash = false;
            }

            public NameIdentifier(byte[] hash)
            {
                this.hash = hash;
            }

            public NameIdentifier(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                hash = reader.ReadBytes(16);
            }

            public void Write(Writer writer)
            {
                writer.Write(hash);
            }

            public string HashString()
            {
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }

            public override string ToString()
            {
                if (name != null) return name;
                return HashString();
            }
        }

        public class File
        {
            /// <summary>
            /// A list of extension types (used if the filename is unknown)
            /// </summary>
            public enum ExtensionTypes
            {
                Unknown,
                Ogg,
                Wav,
                Mdl,
                Png,
                Gif,
            }

            /// <summary>
            /// filename of the file
            /// </summary>
            public NameIdentifier name = new NameIdentifier("File.bin");

            /// <summary>
            /// whether the file is encrypted or not
            /// </summary>
            public bool encrypted = false;

            /// <summary>
            /// an array of the bytes in the file, decrypted
            /// </summary>
            public byte[] data;

            /// <summary>
            /// the extension of the file
            /// </summary>
            public ExtensionTypes extension = ExtensionTypes.Unknown;

            /// <summary>
            /// a string of bytes used for decryption/encryption
            /// </summary>
            private static byte[] encryptionKeyA = new byte[16];

            /// <summary>
            /// another string of bytes used for decryption/encryption
            /// </summary>
            private static byte[] encryptionKeyB = new byte[16];

            private static int eKeyNo;
            private static int eKeyPosA;
            private static int eKeyPosB;
            private static int eNybbleSwap;

            public uint offset = 0;

            public File() { }

            public File(Reader reader, List<string> fileNames = null, int fileID = 0)
            {
                Read(reader, fileNames, fileID);
            }

            public void Read(Reader reader, List<string> fileNames = null, int fileID = 0)
            {
                for (int y = 0; y < 16; y += 4)
                {
                    name.hash[y + 3] = reader.ReadByte();
                    name.hash[y + 2] = reader.ReadByte();
                    name.hash[y + 1] = reader.ReadByte();
                    name.hash[y + 0] = reader.ReadByte();
                }
                name.usingHash = true;

                var md5 = MD5.Create();

                name.name = (fileID + 1) + ".bin"; //Make a base name

                for (int i = 0; fileNames != null && i < fileNames.Count; i++)
                {
                    // RSDKv4 Hashes all Strings at Lower Case
                    string fp = fileNames[i].ToLower();

                    bool match = true;

                    for (int z = 0; z < 16; z++)
                    {
                        if (CalculateMD5Hash(fp)[z] != name.hash[z])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        name = new NameIdentifier(fileNames[i]);
                        break;
                    }
                }

                uint fileOffset = reader.ReadUInt32();
                uint tmp = reader.ReadUInt32();

                encrypted = (tmp & 0x80000000) != 0;
                uint fileSize = (tmp & 0x7FFFFFFF);

                long tmp2 = reader.BaseStream.Position;
                reader.BaseStream.Position = fileOffset;

                // Decrypt File if Encrypted
                if (encrypted && !name.usingHash)
                    data = Decrypt(reader.ReadBytes(fileSize), false);
                else
                    data = reader.ReadBytes(fileSize);


                reader.BaseStream.Position = tmp2;

                extension = GetExtensionFromData();

                if (name.usingHash)
                {
                    switch (extension)
                    {
                        case ExtensionTypes.Gif:
                            name.name = "Sprite" + (fileID + 1) + ".gif";
                            break;

                        case ExtensionTypes.Mdl:
                            name.name = "Model" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Ogg:
                            name.name = "Music" + (fileID + 1) + ".ogg";
                            break;

                        case ExtensionTypes.Png:
                            name.name = "Image" + (fileID + 1) + ".png";
                            break;

                        case ExtensionTypes.Wav:
                            name.name = "SoundEffect" + (fileID + 1) + ".wav";
                            break;

                        case ExtensionTypes.Unknown:
                            name.name = "UnknownFileType" + (fileID + 1) + ".bin";
                            break;
                    }
                }
                md5.Dispose();
            }

            public void WriteFileHeader(Writer writer, uint offset = 0)
            {
                NameIdentifier name = this.name;
                if (!this.name.usingHash)
                    name = new NameIdentifier(this.name.name.Replace('\\', '/').ToLower());

                for (int y = 0; y < 16; y += 4)
                {
                    writer.Write(name.hash[y + 3]);
                    writer.Write(name.hash[y + 2]);
                    writer.Write(name.hash[y + 1]);
                    writer.Write(name.hash[y + 0]);
                }
                writer.Write(offset);
                writer.Write((uint)(data.Length) | (encrypted ? 0x80000000 : 0));
            }

            public void WriteFileData(Writer writer)
            {
                if (encrypted && !name.usingHash)
                    writer.Write(Decrypt(data, true));
                else
                    writer.Write(data);
            }

            private byte[] CalculateMD5Hash(string input)
            {
                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

                md5.Dispose();
                return hash;
            }

            private void GenerateKey(out byte[] keyBuffer, uint Value)
            {
                string strbuf = Value.ToString();

                byte[] md5Buf = CalculateMD5Hash(strbuf);
                keyBuffer = new byte[16];

                for (int y = 0; y < 16; y += 4)
                {
                    // convert every 32-bit word to Little Endian
                    keyBuffer[y + 3] = md5Buf[y + 0];
                    keyBuffer[y + 2] = md5Buf[y + 1];
                    keyBuffer[y + 1] = md5Buf[y + 2];
                    keyBuffer[y + 0] = md5Buf[y + 3];
                }
            }

            private void GenerateKeys(uint key1, uint key2, uint size)
            {
                GenerateKey(out encryptionKeyA, key1);
                GenerateKey(out encryptionKeyB, key2);

                eKeyNo = (int)(size / 4) & 0x7F;
                eKeyPosA = 0;
                eKeyPosB = 8;
                eNybbleSwap = 0;
            }

            private byte[] Decrypt(byte[] data, bool encrypting)
            {
                uint fileSize = (uint)(data.Length);
                GenerateKeys(fileSize, (fileSize >> 1) + 1, fileSize);

                const uint ENC_KEY_2 = 0x24924925;
                const uint ENC_KEY_1 = 0xAAAAAAAB;

                byte[] outputData = new byte[data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    int encByte = data[i];
                    if (encrypting)
                    {
                        encByte ^= encryptionKeyA[eKeyPosA++];

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = ((encByte << 4) + (encByte >> 4)) & 0xFF;

                        encByte ^= eKeyNo ^ encryptionKeyB[eKeyPosB++];
                    }
                    else
                    {
                        encByte ^= eKeyNo ^ encryptionKeyB[eKeyPosB++];

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = ((encByte << 4) + (encByte >> 4)) & 0xFF;

                        encByte ^= encryptionKeyA[eKeyPosA++];
                    }
                    outputData[i] = (byte)encByte;

                    if (eKeyPosA <= 0x0F)
                    {
                        if (eKeyPosB > 0x0C)
                        {
                            eKeyPosB = 0;
                            eNybbleSwap ^= 0x01;
                        }
                    }
                    else if (eKeyPosB <= 0x08)
                    {
                        eKeyPosA = 0;
                        eNybbleSwap ^= 0x01;
                    }
                    else
                    {
                        eKeyNo += 2;
                        eKeyNo &= 0x7F;

                        if (eNybbleSwap != 0)
                        {
                            int key1 = MulUnsignedHigh(ENC_KEY_1, eKeyNo);
                            int key2 = MulUnsignedHigh(ENC_KEY_2, eKeyNo);
                            eNybbleSwap = 0;

                            int temp1 = key2 + (eKeyNo - key2) / 2;
                            int temp2 = key1 / 8 * 3;

                            eKeyPosA = eKeyNo - temp1 / 4 * 7;
                            eKeyPosB = eKeyNo - temp2 * 4 + 2;
                        }
                        else
                        {
                            int key1 = MulUnsignedHigh(ENC_KEY_1, eKeyNo);
                            int key2 = MulUnsignedHigh(ENC_KEY_2, eKeyNo);
                            eNybbleSwap = 1;

                            int temp1 = key2 + (eKeyNo - key2) / 2;
                            int temp2 = key1 / 8 * 3;

                            eKeyPosB = eKeyNo - temp1 / 4 * 7;
                            eKeyPosA = eKeyNo - temp2 * 4 + 3;
                        }
                    }
                }
                return outputData;
            }

            private int MulUnsignedHigh(uint arg1, int arg2)
            {
                return (int)(((ulong)arg1 * (ulong)arg2) >> 32);
            }

            private ExtensionTypes GetExtensionFromData()
            {
                byte[] header = new byte[5];

                for (int i = 0; i < header.Length; i++)
                    header[i] = data[i];

                byte[] signature_ogg = new byte[] { (byte)'O', (byte)'g', (byte)'g', (byte)'s' };
                byte[] signature_gif = new byte[] { (byte)'G', (byte)'I', (byte)'F' };
                byte[] signature_mdl = new byte[] { (byte)'R', (byte)'3', (byte)'D', 0 };
                byte[] signature_png = new byte[] { (byte)'P', (byte)'N', (byte)'G' };
                byte[] signature_wav = new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' };

                if (header.Take(4).SequenceEqual(signature_ogg))
                    return ExtensionTypes.Ogg;

                if (header.Take(3).SequenceEqual(signature_gif))
                    return ExtensionTypes.Gif;

                if (header.Take(4).SequenceEqual(signature_mdl))
                    return ExtensionTypes.Mdl;

                if (header.Take(3).SequenceEqual(signature_png))
                    return ExtensionTypes.Png;

                if (header.Take(4).SequenceEqual(signature_wav))
                    return ExtensionTypes.Wav;

                return ExtensionTypes.Unknown;
            }
        }

        public static readonly byte[] signature = new byte[] { (byte)'R', (byte)'S', (byte)'D', (byte)'K' };

        private byte version = (byte)'B';

        public List<File> files = new List<File>();

        public DataPack() { }

        public DataPack(string filepath, List<string> fileNames = null) : this(new Reader(filepath), fileNames) { }
        public DataPack(Stream stream, List<string> fileNames = null) : this(new Reader(stream), fileNames) { }

        public DataPack(Reader reader, List<string> fileNames = null)
        {
            Read(reader, fileNames);
        }

        public void Read(Reader reader, List<string> fileNames = null)
        {
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid DataFile v4 signature");
            }

            reader.ReadByte(); // 'v'
            version = reader.ReadByte();

            ushort fileCount = reader.ReadUInt16();
            files.Clear();
            for (int i = 0; i < fileCount; i++)
                files.Add(new File(reader, fileNames, i));

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // firstly we setup the file
            // write a bunch of blanks

            writer.Write(signature);
            writer.Write('v');
            writer.Write(version);

            writer.Write((ushort)files.Count); // write the header

            foreach (File f in files)  // write each file's header
                f.WriteFileHeader(writer); // write our file header data

            foreach (File f in files) // write "Filler Data"
            {
                f.offset = (uint)writer.BaseStream.Position;    // get our file data offset
                byte[] b = new byte[f.data.Length];         // load up a set of blanks with the same size as the original set
                writer.Write(b);                                // fill the file up with blank data
            }

            // now we really write our data

            writer.Seek(0, SeekOrigin.Begin); // jump back to the start of the file

            writer.Write(signature);
            writer.Write('v');
            writer.Write(version);

            writer.Write((ushort)files.Count); // re-write our header

            foreach (File f in files) // for each file
            {
                f.WriteFileHeader(writer, f.offset);        // write our header
                long pos = writer.BaseStream.Position;      // get our writer pos for later
                writer.BaseStream.Position = f.offset;      // jump to our saved offset
                f.WriteFileData(writer);                    // write our file data
                writer.BaseStream.Position = pos;           // jump back ready to write the next file!
            }

        }
    }
}
