using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv5
{
    public class StaticObject
    {
        /// <summary>
        /// the file's signtature
        /// </summary>
        public static readonly byte[] MAGIC = new byte[] { (byte)'O', (byte)'B', (byte)'J', (byte)'\0' };

        public uint[] Data = new uint[0x1];

        private uint DataPos = 0;

        public StaticObject()
        {

        }

        public StaticObject(Reader reader)
        {
            uint[] TmpData = new uint[reader.BaseStream.Length];
            DataPos = 0;

            if (!reader.ReadBytes(4).SequenceEqual(MAGIC)) //"OBJ" Header
                throw new Exception("Invalid config file header magic");

            while (!reader.IsEof)
            {
                int Unknown1 = reader.ReadByte();
                reader.ReadUInt32(); //Unknown

                if ((Unknown1 & 0x80) != 0)
                {
                    uint Unknown3 = reader.ReadUInt32();

                    int Variable1 = Unknown1 & 0x7F;

                    switch(Variable1)
                    {
                        //INT8
                        case 0:
                        case 3:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                TmpData[DataPos++] = reader.ReadByte();
                            }
                            break;
                            //IN16
                        case 1:
                        case 4:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                int Value = valA + (valB << 8);
                                TmpData[DataPos++] = (uint)Value;
                            }
                            break;
                            //INT32
                        case 2:
                        case 5:
                        case 6:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                int Value = valA + (valB << 8) + (valC << 16) + (valD << 24);
                                TmpData[DataPos++] = (uint)Value;
                            }
                            break;
                    }
                }
            }
            reader.Close();

            Data = new uint[DataPos];

            for (int i = 0; i < DataPos; i++)
            {
                Data[i] = TmpData[i];
            }
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);

            for (DataPos = 0; DataPos < Data.Length;)
            {
                byte Unknown1 = 0;
                writer.Write(Unknown1);
                writer.Write(Data[DataPos++]);

                if ((Unknown1 & 0x80) != 0)
                {
                    uint Unknown3 = 0;
                    writer.Write(Unknown3);

                    int Variable1 = Unknown1 & 0x7F;

                    switch (Variable1)
                    {
                        //INT8
                        case 0:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write((byte)Data[DataPos++]);
                            }
                            break;
                        case 3:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write((byte)Data[DataPos++]);
                            }
                            break;
                        //IN16
                        case 1:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write((ushort)Data[DataPos++]);
                            }
                            break;
                        case 4:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write((ushort)Data[DataPos++]);
                            }
                            break;
                        //INT32
                        case 2:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write(Data[DataPos++]);
                            }
                            break;
                        case 5:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write(Data[DataPos++]);
                            }
                            break;
                        case 6:
                            for (int i = 0; i < Unknown3; i++)
                            {
                                writer.Write(Data[DataPos++]);
                            }
                            break;
                    }
                }
            }

            writer.Close();
        }
    }
}
