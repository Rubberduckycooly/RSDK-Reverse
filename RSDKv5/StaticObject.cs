using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv5
{
    public class StaticObject
    {

        public class ArrayInfo
        {
            public int Size;
            public byte Type;
            public int[] Data;
        }

        /// <summary>
        /// the file's signtature
        /// </summary>
        public static readonly byte[] MAGIC = new byte[] { (byte)'O', (byte)'B', (byte)'J', (byte)'\0' };

        public int[] Data = new int[0x1];
        public List<ArrayInfo> Arrays = new List<ArrayInfo>();

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

            int MemPos = 0; // I think?

            while (!reader.IsEof)
            {
                int DataType = reader.ReadByte();
                int Unknown = reader.ReadInt32(); //Unknown

                if ((DataType & 0x80) != 0)
                {
                    uint DataSize = reader.ReadUInt32();

                    DataType &= 0x7F;

                    ArrayInfo array = new ArrayInfo();
                    array.Type = (byte)DataType;
                    array.Size = (int)DataSize;
                    array.Data = new int[(int)DataSize];

                    switch (DataType)
                    {
                        //INT8
                        case (int)AttributeTypes.UINT8:

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: UINT8");
                            }
                            

                            for (int i = 0; i < DataSize; i++)
                            {
                                TmpData[DataPos++] = reader.ReadByte();
                                array.Data[i] = TmpData[DataPos - 1];
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.UINT8 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 1) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 1).ToString("X"));
                                }
                            }
                            MemPos += Unknown;
                            break;
                        case (int)AttributeTypes.INT8:

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: INT8");
                            }

                            for (int i = 0; i < DataSize; i++)
                            {
                                TmpData[DataPos++] = reader.ReadSByte();
                                array.Data[i] = TmpData[DataPos - 1];
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Value: Type:" + AttributeTypes.INT8 + ", " + TmpData[DataPos-1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 1) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 1).ToString("X"));
                                }
                            }
                            MemPos += Unknown;
                            break;
                            //IN16
                        case (int)AttributeTypes.UINT16:
                            int TmpDataOffset = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: UINT16");
                            }

                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                ushort Value = (ushort)(valA + (valB << 8));
                                TmpData[DataPos++] = Value;
                                array.Data[i] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.UINT16 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 2) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 2).ToString("X"));
                                }
                            }

                            MemPos += 2 * Unknown;
                            break;
                        case (int)AttributeTypes.INT16:
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: INT16");
                            }

                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                short Value = (short)(valA + (valB << 8));
                                TmpData[DataPos++] = Value;
                                array.Data[i] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.INT16 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 2) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 2).ToString("X"));
                                }
                            }
                            MemPos += 2 * Unknown;
                            break;
                            //INT32
                        case (int)AttributeTypes.UINT32:
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: UINT32");
                            }

                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                uint Value = (uint)(valA + (valB << 8) + (valC << 16) + (valD << 24));
                                TmpData[DataPos++] = (int)Value;
                                array.Data[i] = (int)Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.UINT32 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 4) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 4).ToString("X"));
                                }
                            }
                            MemPos += 4 * Unknown;
                            break;
                        case (int)AttributeTypes.INT32:
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: INT32");
                            }

                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                int Value = valA + (valB << 8) + (valC << 16) + (valD << 24);
                                TmpData[DataPos++] = Value;
                                array.Data[i] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Type:" + AttributeTypes.INT32 + ", Value: " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 4) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 4).ToString("X"));
                                }
                            }
                            MemPos += 4 * Unknown;
                            break;
                        case (int)AttributeTypes.ENUM:
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            if (Debug)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                                Console.WriteLine("Array Size: " + DataSize);
                                Console.WriteLine("Array Type: VAR");
                            }

                            for (int i = 0; i < DataSize; i++)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                int Value = (valA + (valB << 8) + (valC << 16) + (valD << 24));
                                TmpData[DataPos++] = (int)Value;
                                array.Data[i] = Value;
                                if (Debug)
                                {
                                    Console.WriteLine("Value Info: Value: Type:" + AttributeTypes.ENUM + ", " + TmpData[DataPos - 1] + ", Value (Hex) 0x" + TmpData[DataPos - 1].ToString("X") + ", Offset: " + (reader.BaseStream.Position - 4) + ", Offset (Hex): 0x" + (reader.BaseStream.Position - 4).ToString("X"));
                                }
                            }
                            MemPos += 4 * Unknown;
                            break;
                    }
                }
                else
                {
                    switch (DataType)
                    {
                        //INT8
                        case (int)AttributeTypes.UINT8:
                        case (int)AttributeTypes.INT8:
                            MemPos += Unknown;
                            break;
                        //IN16
                        case (int)AttributeTypes.UINT16:
                        case (int)AttributeTypes.INT16:
                            bool v45 = (MemPos & 0xFFFFFFFE) < MemPos;
                            int v46 = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if (!v45)
                                v46 = MemPos;
                            MemPos = v46 + 2 * Unknown;
                            break;
                        //INT32
                        case (int)AttributeTypes.UINT32:
                        case (int)AttributeTypes.INT32:
                        case (int)AttributeTypes.ENUM:
                        case (int)AttributeTypes.BOOL:
                            int v48 = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                v48 = MemPos;
                            MemPos = v48 + 4 * Unknown;
                            break;
                        case (int)AttributeTypes.STRING:
                        case (int)AttributeTypes.VECTOR2:
                            v48 = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                v48 = MemPos;
                            MemPos = v48 + 8 * Unknown;
                            break;
                        case (int)AttributeTypes.VECTOR3:
                            v48 = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                v48 = MemPos;
                            MemPos = v48 + 24 * Unknown;
                            break;
                        case (int)AttributeTypes.COLOR:
                            v48 = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                v48 = MemPos;
                            MemPos = v48 + 8 * Unknown;
                            break;
                        default:
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
