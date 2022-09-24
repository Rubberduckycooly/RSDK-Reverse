using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSDKv5
{
    public class DataPack
    {
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
                Cfg,
                Png,
                Mdl,
                Obj,
                Spr,
                Gif,
                Scn,
                Til,
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

            public File(Reader reader, List<NameIdentifier> fileNames = null, int fileID = 0)
            {
                Read(reader, fileNames, fileID);
            }

            public void Read(Reader reader, List<NameIdentifier> fileNames = null, int fileID = 0)
            {
                for (int y = 0; y < 16; y += 4)
                {
                    name.hash[y + 3] = reader.ReadByte();
                    name.hash[y + 2] = reader.ReadByte();
                    name.hash[y + 1] = reader.ReadByte();
                    name.hash[y + 0] = reader.ReadByte();
                }
                name.usingHash = true;

                name.name = (fileID + 1) + ".bin"; //Make a base name

                if (fileNames != null)
                    foreach (var fn in fileNames)
                    {
                        bool match = true;

                        for (int z = 0; z < 16; z++)
                        {
                            if (fn.hash[z] != name.hash[z])
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                        {
                            name = fn;
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
                        default: break;

                        case ExtensionTypes.Cfg:
                            if (encrypted)
                                name.name = "Config[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "Config" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Gif:
                            if (encrypted)
                                name.name = "SpriteSheet[Encrypted]" + (fileID + 1) + ".gif";
                            else
                                name.name = "SpriteSheet" + (fileID + 1) + ".gif";
                            break;

                        case ExtensionTypes.Mdl:
                            if (encrypted)
                                name.name = "Model[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "Model" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Obj:
                            if (encrypted)
                                name.name = "StaticObject[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "StaticObject" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Ogg:
                            if (encrypted)
                                name.name = "Music[Encrypted]" + (fileID + 1) + ".ogg";
                            else
                                name.name = "Music" + (fileID + 1) + ".ogg";
                            break;

                        case ExtensionTypes.Png:
                            if (encrypted)
                                name.name = "Image[Encrypted]" + (fileID + 1) + ".png";
                            else
                                name.name = "Image" + (fileID + 1) + ".png";
                            break;

                        case ExtensionTypes.Scn:
                            if (encrypted)
                                name.name = "Scene[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "Scene" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Spr:
                            if (encrypted)
                                name.name = "Animation[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "Animation" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Til:
                            if (encrypted)
                                name.name = "TileConfig[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "TileConfig" + (fileID + 1) + ".bin";
                            break;

                        case ExtensionTypes.Wav:
                            if (encrypted)
                                name.name = "SoundFX[Encrypted]" + (fileID + 1) + ".wav";
                            else
                                name.name = "SoundFX" + (fileID + 1) + ".wav";
                            break;

                        case ExtensionTypes.Unknown:
                            if (encrypted)
                                name.name = "UnknownFileType[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                name.name = "UnknownFileType" + (fileID + 1) + ".bin";
                            break;
                    }
                }
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

			private byte[] CalculateMD5Hash(string input) => MD5Hasher.GetHash(Encoding.ASCII.GetBytes(input));

			private void GenerateKeys(string fileName, uint fileSize)
            {
                string filenameUpper = fileName.ToUpper();
                byte[] md5Buf = md5Buf = CalculateMD5Hash(filenameUpper);

                for (int y = 0; y < 16; y += 4)
                {
                    // convert every 32-bit word to Little Endian
                    encryptionKeyA[y + 3] = md5Buf[y + 0];
                    encryptionKeyA[y + 2] = md5Buf[y + 1];
                    encryptionKeyA[y + 1] = md5Buf[y + 2];
                    encryptionKeyA[y + 0] = md5Buf[y + 3];
                }

                string fsize = fileSize.ToString();
                md5Buf = CalculateMD5Hash(fsize);

                for (int y = 0; y < 16; y += 4)
                {
                    // convert every 32-bit word to Little Endian
                    encryptionKeyB[y + 3] = md5Buf[y + 0];
                    encryptionKeyB[y + 2] = md5Buf[y + 1];
                    encryptionKeyB[y + 1] = md5Buf[y + 2];
                    encryptionKeyB[y + 0] = md5Buf[y + 3];
                }

                eKeyNo = (int)(fileSize / 4) & 0x7F;
                eKeyPosA = 0;
                eKeyPosB = 8;
                eNybbleSwap = 0;
            }

            private byte[] Decrypt(byte[] data, bool encrypting)
            {
                // Note: Since only XOr is used, this function does both,
                //       decryption and encryption.

                uint fileSize = (uint)data.Length;
                GenerateKeys(name.name, fileSize);

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
                            eNybbleSwap = 0;

                            eKeyPosA = eKeyNo % 7;
                            eKeyPosB = (eKeyNo % 0xC) + 2;
                        }
                        else
                        {
                            eNybbleSwap = 1;

                            eKeyPosA = (eKeyNo % 0xC) + 3;
                            eKeyPosB = eKeyNo % 7;
                        }
                    }
                }
                return outputData;
            }

            private ExtensionTypes GetExtensionFromData()
            {
                if (data.Length < 4)
                    return ExtensionTypes.Unknown;

                byte[] header = new byte[4];

                for (int i = 0; i < header.Length; i++)
                    header[i] = data[i];

                byte[] signature_ogg = new byte[] { (byte)'O', (byte)'g', (byte)'g', (byte)'s' };
                byte[] signature_gif = new byte[] { (byte)'G', (byte)'I', (byte)'F' };
                byte[] signature_mdl = new byte[] { (byte)'M', (byte)'D', (byte)'L', 0 };
                byte[] signature_png = new byte[] { (byte)'P', (byte)'N', (byte)'G' };
                byte[] signature_wav = new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' };
                byte[] signature_scn = new byte[] { (byte)'S', (byte)'C', (byte)'N', 0 };
                byte[] signature_til = new byte[] { (byte)'T', (byte)'I', (byte)'L', 0 };
                byte[] signature_spr = new byte[] { (byte)'S', (byte)'P', (byte)'R', 0 };
                byte[] signature_cfg = new byte[] { (byte)'C', (byte)'C', (byte)'G', 0 };
                byte[] signature_obj = new byte[] { (byte)'O', (byte)'B', (byte)'J', 0 };

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

                if (header.Take(4).SequenceEqual(signature_scn))
                    return ExtensionTypes.Scn;

                if (header.Take(4).SequenceEqual(signature_til))
                    return ExtensionTypes.Til;

                if (header.Take(4).SequenceEqual(signature_spr))
                    return ExtensionTypes.Spr;

                if (header.Take(4).SequenceEqual(signature_cfg))
                    return ExtensionTypes.Cfg;

                if (header.Take(4).SequenceEqual(signature_obj))
                    return ExtensionTypes.Obj;

                return ExtensionTypes.Unknown;
            }

        }

        public static readonly byte[] signature = new byte[] { (byte)'R', (byte)'S', (byte)'D', (byte)'K' };

        public byte version = (byte)'5';

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
                throw new Exception("Invalid DataFile v5 signature");
            }

            reader.ReadByte(); // 'v'
            version = reader.ReadByte();

            List<NameIdentifier> names = new List<NameIdentifier>();
            if (fileNames != null)
                foreach (var fn in fileNames)
                    names.Add(new NameIdentifier(fn.ToLowerInvariant()) { name = fn });

            ushort fileCount = reader.ReadUInt16();
            for (int f = 0; f < fileCount; f++)
                files.Add(new File(reader, names, f));

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

            writer.Write((ushort)files.Count);

            foreach (File f in files)     // write each file's header
                f.WriteFileHeader(writer, 0); // write our file header data

            foreach (File f in files) // write "Filler Data"
            {
                f.offset = (uint)writer.BaseStream.Position;
                byte[] b = new byte[f.data.Length]; // load up a set of blanks with the same size as the original set
                writer.Write(b);                        // fill the file up with blank data
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
