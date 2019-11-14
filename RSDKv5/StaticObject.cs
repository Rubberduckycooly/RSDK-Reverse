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

        public int[] Data = new int[0x1];

        private uint DataPos = 0;
        private bool Debug = true;
        public StaticObject()
        {

        }

        public StaticObject(Reader reader)
        {
            int[] TmpData = new int[reader.BaseStream.Length];
            DataPos = 0;
            string filename = System.IO.Path.GetFileName(reader.GetFilename());
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC)) //"OBJ" Header
                throw new Exception("Invalid config file header magic");

            if (Debug)
            {
                Console.WriteLine("Viewing Info for " + filename);
            }

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
                            for (int i = 0; i < DataSize; i++)
                            {
                                TmpData[DataPos++] = reader.ReadByte();
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.UINT8 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 1) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 1).ToString("X"));
                                }
                            }
                            break;
                        case (int)AttributeTypes.INT8:
                            for (int i = 0; i < DataSize; i++)
                            {
                                TmpData[DataPos++] = reader.ReadSByte();
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Value: Type:" + AttributeTypes.INT8 + ", " + TmpData[DataPos-1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 1) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 1).ToString("X"));
                                }
                            }
                            break;
                            //IN16
                        case (int)AttributeTypes.UINT16:
                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                ushort Value = (ushort)(valA + (valB << 8));
                                TmpData[DataPos++] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.UINT16 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 2) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 2).ToString("X"));
                                }
                            }
                            break;
                        case (int)AttributeTypes.INT16:
                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                short Value = (short)(valA + (valB << 8));
                                TmpData[DataPos++] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.INT16 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 2) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 2).ToString("X"));
                                }
                            }
                            break;
                            //INT32
                        case (int)AttributeTypes.UINT32:
                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                uint Value = (uint)(valA + (valB << 8) + (valC << 16) + (valD << 24));
                                TmpData[DataPos++] = (int)Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.UINT32 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 4) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 4).ToString("X"));
                                }
                            }
                            break;
                        case (int)AttributeTypes.INT32:
                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                int Value = valA + (valB << 8) + (valC << 16) + (valD << 24);
                                TmpData[DataPos++] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.INT32 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 4) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 4).ToString("X"));
                                }
                            }
                            break;
                        case (int)AttributeTypes.ENUM:
                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                uint Value = (uint)(valA + (valB << 8) + (valC << 16) + (valD << 24));
                                TmpData[DataPos++] = (int)Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Value: Type:" + AttributeTypes.ENUM + ", " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 4) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 4).ToString("X"));
                                }
                            }
                            break;
                    }
                }
            }
            reader.Close();

            Data = new int[DataPos];

            for (int i = 0; i < DataPos; i++)
            {
                Data[i] = TmpData[i];
            }

            if (Debug)
            {
                Console.WriteLine(filename + " Has " + Data.Length + " Values");
            }
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);

            for (uint DataPos = 0; DataPos < Data.Length;)
            {
                uint offset = DataPos;
                byte FirstDataType = 0;
                byte DataTypeBuf = 0;
                byte DataType = 0;
                uint DataSize = 0;

                if (Data[offset] >= 0)
                {
                    if (Data[offset] <= byte.MaxValue)
                    {
                        FirstDataType = DataTypeBuf = (int)AttributeTypes.UINT8;
                    }
                    if (Data[offset] > byte.MaxValue && Data[offset] <= ushort.MaxValue)
                    {
                        FirstDataType = DataTypeBuf = (int)AttributeTypes.UINT16;
                    }
                    if (Data[offset] > ushort.MaxValue && Data[offset] <= uint.MaxValue)
                    {
                        FirstDataType = DataTypeBuf = (int)AttributeTypes.UINT32;
                    }
                }
                else
                {
                    if (Data[offset] > sbyte.MinValue)
                    {
                        FirstDataType = DataTypeBuf = (int)AttributeTypes.INT8;
                    }
                    if (Data[offset] <= sbyte.MinValue && Data[offset] >= short.MinValue)
                    {
                        FirstDataType = DataTypeBuf = (int)AttributeTypes.INT16;
                    }
                    if (Data[offset] <= short.MinValue && Data[offset] >= int.MinValue)
                    {
                        FirstDataType = DataTypeBuf = (int)AttributeTypes.INT32;
                    }
                }
                offset++;

                while (FirstDataType == DataTypeBuf && offset < Data.Length)
                {
                    if (Data[offset] >= 0)
                    {
                        if (Data[offset] <= byte.MaxValue)
                        {
                            DataTypeBuf = (int)AttributeTypes.UINT8;
                        }
                        if (Data[offset] > byte.MaxValue && Data[offset] <= ushort.MaxValue)
                        {
                            DataTypeBuf = (int)AttributeTypes.UINT16;
                        }
                        if (Data[offset] > ushort.MaxValue && Data[offset] <= uint.MaxValue)
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
                    else
                    {
                        if (Data[offset] > sbyte.MinValue)
                        {
                            DataTypeBuf = (int)AttributeTypes.INT8;
                        }
                        if (Data[offset] <= sbyte.MinValue && Data[offset] >= short.MinValue)
                        {
                            DataTypeBuf = (int)AttributeTypes.INT16;
                        }
                        if (Data[offset] <= short.MinValue && Data[offset] >= int.MinValue)
                        {
                            DataTypeBuf = (int)AttributeTypes.INT32;
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
                }

                if (offset == Data.Length)
                {
                    DataType = FirstDataType;
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
                        case (int)AttributeTypes.UINT8:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((byte)Data[DataPos++]);
                            }
                            break;
                        case (int)AttributeTypes.INT8:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((sbyte)Data[DataPos++]);
                            }
                            break;
                        //IN16
                        case (int)AttributeTypes.UINT16:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((ushort)Data[DataPos++]);
                            }
                            break;
                        case (int)AttributeTypes.INT16:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((short)Data[DataPos++]);
                            }
                            break;
                        //INT32
                        case (int)AttributeTypes.UINT32:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((uint)Data[DataPos++]);
                            }
                            break;
                        case (int)AttributeTypes.INT32:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((int)Data[DataPos++]);
                            }
                            break;
                        case (int)AttributeTypes.ENUM:
                            for (int i = 0; i < DataSize; i++)
                            {
                                writer.Write((uint)Data[DataPos++]);
                            }
                            break;
                    }
                }
            }

            writer.Close();
        }
    }
}
