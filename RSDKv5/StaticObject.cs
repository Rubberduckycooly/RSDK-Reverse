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
                int DataType = reader.ReadByte();
                uint Unknown = reader.ReadUInt32(); //Unknown

                if ((DataType & 0x80) != 0)
                {
                    uint DataSize = reader.ReadUInt32();

                    DataType &= 0x7F;

                    switch(DataType)
                    {
                        //INT8
                        case (int)AttributeTypes.UINT8:
                        case (int)AttributeTypes.INT8:
                            for (int i = 0; i < DataSize; i++)
                            {
                                TmpData[DataPos++] = reader.ReadByte();
                            }
                            break;
                            //IN16
                        case (int)AttributeTypes.UINT16:
                        case (int)AttributeTypes.INT16:
                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                int Value = valA + (valB << 8);
                                TmpData[DataPos++] = (uint)Value;
                            }
                            break;
                            //INT32
                        case (int)AttributeTypes.UINT32:
                        case (int)AttributeTypes.INT32:
                        case (int)AttributeTypes.VAR:
                            for (int i = 0; i < DataSize; i++)
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
                uint offset = DataPos;
                byte FirstDataType = 0;
                byte DataTypeBuf = 0;
                byte DataType = 0;
                uint DataSize = 0;

                if (Data[offset] < 0x100)
                {
                    FirstDataType = DataTypeBuf = (int)AttributeTypes.UINT8;
                }
                if (Data[offset] >= 0x100 && Data[offset] < 0x10000)
                {
                    FirstDataType = DataTypeBuf = (int)AttributeTypes.UINT16;
                }
                if (Data[offset] >= 0x10000 && Data[offset] <= 0xFFFFFFFF)
                {
                    FirstDataType = DataTypeBuf = (int)AttributeTypes.UINT32;
                }
                offset++;

                while (FirstDataType == DataTypeBuf && offset < Data.Length)
                {
                    if (Data[offset] < 0x100)
                    {
                        DataTypeBuf = (int)AttributeTypes.UINT8;
                    }
                    if (Data[offset] >= 0x100 && Data[offset] < 0x10000)
                    {
                        DataTypeBuf = (int)AttributeTypes.UINT16;
                    }
                    if (Data[offset] >= 0x10000 && Data[offset] <= 0xFFFFFFFF)
                    {
                        DataTypeBuf = (int)AttributeTypes.UINT32;
                    }
                    if (FirstDataType == DataTypeBuf)
                    {
                        offset++;
                    }
                    else
                    {
                        DataType = FirstDataType;
                    }
                }

                FirstDataType = DataTypeBuf;

                DataSize = offset - DataPos;
                //DataPos = offset;
                DataType |= 0x80;

                writer.Write(DataType);
                writer.Write(DataSize);

                if ((DataType & 0x80) != 0)
                {
                    writer.Write(DataSize);

                    switch (DataType & 0x7F)
                    {
                        //INT8
                        case 0:
                        case 3:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((byte)Data[DataPos++]);
                            }
                            break;
                        //IN16
                        case 1:
                        case 4:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((ushort)Data[DataPos++]);
                            }
                            break;
                        //INT32
                        case 2:
                        case 5:
                        case 6:
                            for (int i = 0; i < DataSize; i++)
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
