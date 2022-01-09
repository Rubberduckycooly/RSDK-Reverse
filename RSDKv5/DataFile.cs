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
                CFG,
                PNG,
                MDL,
                OBJ,
                SPR,
                GIF,
                SCN,
                TIL,
            }

            /// <summary>
            /// filename of the file
            /// </summary>
            public NameIdentifier fileName = new NameIdentifier("File.bin");

            /// <summary>
            /// whether the file is encrypted or not
            /// </summary>
            public bool encrypted = false;

            /// <summary>
            /// an array of the bytes in the file, decrypted
            /// </summary>
            public byte[] fileData;

            /// <summary>
            /// the extension of the file
            /// </summary>
            public ExtensionTypes extension = ExtensionTypes.UNKNOWN;

            /// <summary>
            /// a string of bytes used for decryption/encryption
            /// </summary>
            private static byte[] encryptionStringA = new byte[16];
            /// <summary>
            /// another string of bytes used for decryption/encryption
            /// </summary>
            private static byte[] encryptionStringB = new byte[16];

            private static int eStringNo;
            private static int eStringPosA;
            private static int eStringPosB;
            private static int eNybbleSwap;

            public uint offset = 0;

            public FileInfo() { }

            public FileInfo(Reader reader, List<string> fileNames = null, int fileID = 0)
            {
                read(reader, fileNames, fileID);
            }

            public void read(Reader reader, List<string> fileNames = null, int fileID = 0)
            {
                for (int y = 0; y < 16; y += 4)
                {
                    fileName.hash[y + 3] = reader.ReadByte();
                    fileName.hash[y + 2] = reader.ReadByte();
                    fileName.hash[y + 1] = reader.ReadByte();
                    fileName.hash[y + 0] = reader.ReadByte();
                }
                fileName.usingHash = true;

                var md5 = MD5.Create();

                fileName.name = (fileID + 1) + ".bin"; //Make a base name

                for (int i = 0; fileNames != null && i < fileNames.Count; i++)
                {
                    // Mania Hashes all Strings at Lower Case
                    string fp = fileNames[i].ToLower();

                    bool match = true;

                    for (int z = 0; z < 16; z++)
                    {
                        if (calculateMD5Hash(fp)[z] != fileName.hash[z])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        fileName = new NameIdentifier(fileNames[i]);
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
                if (encrypted && !fileName.usingHash)
                    fileData = decrypt(reader.readBytes(fileSize), false);
                else
                    fileData = reader.readBytes(fileSize);

                reader.BaseStream.Position = tmp2;

                extension = getExtensionFromData();

                if (fileName.usingHash)
                {
                    switch (extension)
                    {
                        case ExtensionTypes.CFG:
                            if (encrypted)
                                fileName.name = "Config[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "Config" + (fileID + 1) + ".bin";
                            break;
                        case ExtensionTypes.GIF:
                            if (encrypted)
                                fileName.name = "SpriteSheet[Encrypted]" + (fileID + 1) + ".gif";
                            else
                                fileName.name = "SpriteSheet" + (fileID + 1) + ".gif";
                            break;
                        case ExtensionTypes.MDL:
                            if (encrypted)
                                fileName.name = "Model[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "Model" + (fileID + 1) + ".bin";
                            break;
                        case ExtensionTypes.OBJ:
                            if (encrypted)
                                fileName.name = "StaticObject[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "StaticObject" + (fileID + 1) + ".bin";
                            break;
                        case ExtensionTypes.OGG:
                            if (encrypted)
                                fileName.name = "Music[Encrypted]" + (fileID + 1) + ".ogg";
                            else
                                fileName.name = "Music" + (fileID + 1) + ".ogg";
                            break;
                        case ExtensionTypes.PNG:
                            if (encrypted)
                                fileName.name = "Image[Encrypted]" + (fileID + 1) + ".png";
                            else
                                fileName.name = "Image" + (fileID + 1) + ".png";
                            break;
                        case ExtensionTypes.SCN:
                            if (encrypted)
                                fileName.name = "Scene[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "Scene" + (fileID + 1) + ".bin";
                            break;
                        case ExtensionTypes.SPR:
                            if (encrypted)
                                fileName.name = "Animation[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "Animation" + (fileID + 1) + ".bin";
                            break;
                        case ExtensionTypes.TIL:
                            if (encrypted)
                                fileName.name = "TileConfig[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "TileConfig" + (fileID + 1) + ".bin";
                            break;
                        case ExtensionTypes.WAV:
                            if (encrypted)
                                fileName.name = "SoundFX[Encrypted]" + (fileID + 1) + ".wav";
                            else
                                fileName.name = "SoundFX" + (fileID + 1) + ".wav";
                            break;
                        case ExtensionTypes.UNKNOWN:
                            if (encrypted)
                                fileName.name = "UnknownFileType[Encrypted]" + (fileID + 1) + ".bin";
                            else
                                fileName.name = "UnknownFileType" + (fileID + 1) + ".bin";
                            break;
                    }
                }
                md5.Dispose();
            }

            public void writeFileHeader(Writer writer, uint offset = 0)
            {
                NameIdentifier name = fileName;
                if (!fileName.usingHash)
                    name = new NameIdentifier(fileName.name.Replace('\\', '/').ToLower());

                for (int y = 0; y < 16; y += 4)
                {
                    writer.Write(name.hash[y + 3]);
                    writer.Write(name.hash[y + 2]);
                    writer.Write(name.hash[y + 1]);
                    writer.Write(name.hash[y + 0]);
                }
                writer.Write(offset);
                writer.Write((uint)(fileData.Length) | (encrypted ? 0x80000000 : 0));
            }

            public void writeFileData(Writer writer)
            {
                if (encrypted && !fileName.usingHash)
                    writer.Write(decrypt(fileData, true));
                else
                    writer.Write(fileData);
            }

            private byte[] calculateMD5Hash(string input)
            {
                byte[] hash;
                using (var md5 = MD5.Create())
                    hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return hash;
            }

            private void generateELoadKeys(string FileName, uint VSize)
            {
                string filenameUpper = FileName.ToUpper();
                byte[] md5Buf = md5Buf = calculateMD5Hash(filenameUpper);

                for (int y = 0; y < 16; y += 4)
                {
                    // convert every 32-bit word to Little Endian
                    encryptionStringA[y + 3] = md5Buf[y + 0];
                    encryptionStringA[y + 2] = md5Buf[y + 1];
                    encryptionStringA[y + 1] = md5Buf[y + 2];
                    encryptionStringA[y + 0] = md5Buf[y + 3];
                }

                string fsize = VSize.ToString();
                md5Buf = calculateMD5Hash(fsize);

                for (int y = 0; y < 16; y += 4)
                {
                    // convert every 32-bit word to Little Endian
                    encryptionStringB[y + 3] = md5Buf[y + 0];
                    encryptionStringB[y + 2] = md5Buf[y + 1];
                    encryptionStringB[y + 1] = md5Buf[y + 2];
                    encryptionStringB[y + 0] = md5Buf[y + 3];
                }

                eStringNo = (int)(VSize / 4) & 0x7F;
                eStringPosA = 0;
                eStringPosB = 8;
                eNybbleSwap = 0;
            }

            private byte[] decrypt(byte[] data, bool encrypting)
            {
                // Note: Since only XOr is used, this function does both,
                //       decryption and encryption.

                uint fileSize = (uint)data.Length;
                generateELoadKeys(fileName.name, fileSize);

                byte[] outputData = new byte[data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    int encByte = data[i];
                    if (encrypting)
                    {
                        encByte ^= encryptionStringA[eStringPosA++];

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = ((encByte << 4) + (encByte >> 4)) & 0xFF;

                        encByte ^= eStringNo ^ encryptionStringB[eStringPosB++];
                    }
                    else
                    {
                        encByte ^= eStringNo ^ encryptionStringB[eStringPosB++];

                        if (eNybbleSwap == 1)   // swap nibbles: 0xAB <-> 0xBA
                            encByte = ((encByte << 4) + (encByte >> 4)) & 0xFF;

                        encByte ^= encryptionStringA[eStringPosA++];
                    }

                    outputData[i] = (byte)encByte;

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
                            eNybbleSwap = 0;

                            eStringPosA = eStringNo % 7;
                            eStringPosB = (eStringNo % 0xC) + 2;
                        }
                        else
                        {
                            eNybbleSwap = 1;

                            eStringPosA = (eStringNo % 0xC) + 3;
                            eStringPosB = eStringNo % 7;
                        }
                    }
                }
                return outputData;
            }

            private ExtensionTypes getExtensionFromData()
            {
                byte[] header = new byte[5];

                for (int i = 0; i < header.Length; i++)
                    header[i] = fileData[i];

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
                    return ExtensionTypes.OGG;

                if (header.Take(3).SequenceEqual(signature_gif))
                    return ExtensionTypes.GIF;

                if (header.Take(4).SequenceEqual(signature_mdl))
                    return ExtensionTypes.MDL;

                if (header.Take(3).SequenceEqual(signature_png))
                    return ExtensionTypes.PNG;

                if (header.Take(4).SequenceEqual(signature_wav))
                    return ExtensionTypes.WAV;

                if (header.Take(4).SequenceEqual(signature_scn))
                    return ExtensionTypes.SCN;

                if (header.Take(4).SequenceEqual(signature_til))
                    return ExtensionTypes.TIL;

                if (header.Take(4).SequenceEqual(signature_spr))
                    return ExtensionTypes.SPR;

                if (header.Take(4).SequenceEqual(signature_cfg))
                    return ExtensionTypes.CFG;

                if (header.Take(4).SequenceEqual(signature_obj))
                    return ExtensionTypes.OBJ;

                return ExtensionTypes.UNKNOWN;
            }

        }

        public static readonly byte[] signature = new byte[] { (byte)'R', (byte)'S', (byte)'D', (byte)'K', (byte)'v', (byte)'5' };

        public List<FileInfo> files = new List<FileInfo>();

        public DataFile() { }
        public DataFile(string filepath, List<string> fileNames = null) : this(new Reader(filepath), fileNames) { }
        public DataFile(Stream stream, List<string> fileNames = null) : this(new Reader(stream), fileNames) { }

        public DataFile(Reader reader, List<string> fileNames = null)
        {
            read(reader, fileNames);
        }

        public void read(Reader reader, List<string> fileNames = null)
        {
            if (!reader.readBytes(6).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid DataFile v5 signature");
            }

            ushort fileCount = reader.ReadUInt16();
            for (int i = 0; i < fileCount; i++)
                files.Add(new FileInfo(reader, fileNames, i));

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // firstly we setout the file
            // write a bunch of blanks

            writer.Write(signature);
            writer.Write((ushort)files.Count);

            foreach (FileInfo f in files)     // write each file's header
                f.writeFileHeader(writer, 0); // write our file header data

            foreach (FileInfo f in files) // write "Filler Data"
            {
                f.offset = (uint)writer.BaseStream.Position;
                byte[] b = new byte[f.fileData.Length]; // load up a set of blanks with the same size as the original set
                writer.Write(b);                        // fill the file up with blank data
            }

            // now we really write our data

            writer.seek(0, SeekOrigin.Begin); // jump back to the start of the file

            writer.Write(signature);
            writer.Write((ushort)files.Count); // re-write our header

            foreach (FileInfo f in files) // for each file
            {
                f.writeFileHeader(writer, f.offset);        // write our header
                long pos = writer.BaseStream.Position;      // get our writer pos for later
                writer.BaseStream.Position = f.offset;      // jump to our saved offset
                f.writeFileData(writer);                    // write our file data
                writer.BaseStream.Position = pos;           // jump back ready to write the next file!
            }

        }
    }
}