using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv5
{
    public class StaticObject
    {

        public class ArrayInfo
        {
            public byte Type = 0x80;
            public int Size = 0;
            public int DataSize = 0;
            public int[] Data;
        }

        /// <summary>
        /// the file's signtature
        /// </summary>
        public static readonly byte[] MAGIC = new byte[] { (byte)'O', (byte)'B', (byte)'J', (byte)'\0' };

        public List<ArrayInfo> Arrays = new List<ArrayInfo>();
        public StaticObject(bool PrintDebugInfo = false)
        {

        }

        public StaticObject(Reader reader, bool PrintDebugInfo = false)
        {
            bool Debug = PrintDebugInfo;

            int[] TmpData = new int[reader.BaseStream.Length];
            string filename = System.IO.Path.GetFileName(reader.GetFilename());
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC)) //"OBJ" Header
                throw new Exception("Invalid config file header magic");

            if (Debug)
                Console.WriteLine("Viewing Info for " + filename);


            int MemPos = 0; // I think?

            while (!reader.IsEof)
            {
                int DataType = reader.ReadByte();
                int ArraySize = reader.ReadInt32();

                if ((DataType & 0x80) != 0)
                {
                    uint DataSize = reader.ReadUInt32();

                    DataType &= 0x7F;

                    ArrayInfo array = new ArrayInfo();
                    array.Type = (byte)DataType;
                    array.Size = (int)ArraySize;
                    array.DataSize = (int)DataSize;
                    array.Data = new int[(int)DataSize];
                    if (Debug)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Array Info:");
                        Console.WriteLine("Struct Offset: " + MemPos + "(0x" + MemPos.ToString("X") + ")");
                        Console.WriteLine("Array Size: " + ArraySize);
                        Console.WriteLine("Array DataSize: " + DataSize);
                    }

                    switch (DataType)
                    {
                        //INT8
                        case (int)AttributeTypes.UINT8:
                            if (Debug)
                                Console.WriteLine("Array Type: UINT8");
                            for (int i = 0; i < DataSize; ++i)
                            {
                                array.Data[i] = reader.ReadByte();
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 1), Debug);
                            }
                            MemPos += ArraySize;
                            break;
                        case (int)AttributeTypes.INT8:
                            if (Debug)
                                Console.WriteLine("Array Type: INT8");
                            for (int i = 0; i < DataSize; ++i)
                            {
                                array.Data[i] = reader.ReadSByte();
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 1), Debug);
                            }
                            MemPos += ArraySize;
                            break;
                        //IN16
                        case (int)AttributeTypes.UINT16:
                            if (Debug)
                                Console.WriteLine("Array Type: UINT16");
                            int TmpDataOffset = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            for (int i = 0; i < DataSize; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                array.Data[i] = (ushort)(valA + (valB << 8));
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 2), Debug);
                            }

                            MemPos += 2 * ArraySize;
                            break;
                        case (int)AttributeTypes.INT16:
                            if (Debug)
                                Console.WriteLine("Array Type: INT16");
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            for (int i = 0; i < DataSize; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                array.Data[i] = (short)(valA + (valB << 8));
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 2), Debug);
                            }
                            MemPos += 2 * ArraySize;
                            break;
                        //INT32
                        case (int)AttributeTypes.UINT32:
                            if (Debug)
                                Console.WriteLine("Array Type: UINT32");
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            for (int i = 0; i < DataSize; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                array.Data[i] = (int)(uint)(valA + (valB << 8) + (valC << 16) + (valD << 24));
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 4), Debug);
                            }
                            MemPos += 4 * ArraySize;
                            break;
                        case (int)AttributeTypes.INT32:
                            if (Debug)
                                Console.WriteLine("Array Type: INT32");
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            for (int i = 0; i < DataSize; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                array.Data[i] = valA + (valB << 8) + (valC << 16) + (valD << 24);
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 4), Debug);
                            }
                            MemPos += 4 * ArraySize;
                            break;
                        case (int)AttributeTypes.ENUM:
                            if (Debug)
                                Console.WriteLine("Array Type: VAR/ENUM");
                            TmpDataOffset = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                TmpDataOffset = MemPos;
                            MemPos = TmpDataOffset;

                            for (int i = 0; i < DataSize; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                array.Data[i] = valA + (valB << 8) + (valC << 16) + (valD << 24);
                                printValInfo(array.Data[i], (int)(reader.BaseStream.Position - 4), Debug);
                            }
                            MemPos += 4 * ArraySize;
                            break;
                    }
                }
                else
                {
                    int Buffer = 0;
                    switch (DataType)
                    {
                        //INT8
                        case (int)AttributeTypes.UINT8:
                        case (int)AttributeTypes.INT8:
                            MemPos += ArraySize;
                            break;
                        //IN16
                        case (int)AttributeTypes.UINT16:
                        case (int)AttributeTypes.INT16:
                            Buffer = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                Buffer = MemPos;
                            MemPos = Buffer + 2 * ArraySize;
                            break;
                        //INT32
                        case (int)AttributeTypes.UINT32:
                        case (int)AttributeTypes.INT32:
                        case (int)AttributeTypes.ENUM:
                        case (int)AttributeTypes.BOOL:
                            Buffer = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                Buffer = MemPos;
                            MemPos = Buffer + 4 * ArraySize;
                            break;
                        case (int)AttributeTypes.STRING:
                        case (int)AttributeTypes.VECTOR2:
                            Buffer = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                Buffer = MemPos;
                            MemPos = Buffer + 8 * ArraySize;
                            break;
                        case (int)AttributeTypes.VECTOR3:
                            Buffer = (int)((MemPos & 0xFFFFFFFC) + 4);
                            if ((MemPos & 0xFFFFFFFC) >= MemPos)
                                Buffer = MemPos;
                            MemPos = Buffer + 24 * ArraySize;
                            break;
                        case (int)AttributeTypes.COLOR:
                            Buffer = (int)((MemPos & 0xFFFFFFFE) + 2);
                            if ((MemPos & 0xFFFFFFFE) >= MemPos)
                                Buffer = MemPos;
                            MemPos = Buffer + 8 * ArraySize;
                            break;
                        default:
                            break;
                    }
                }
            }
            reader.Close();

            if (Debug)
                Console.WriteLine(filename + " Has " + Arrays.Count + " Arrays");

        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);

            foreach (ArrayInfo array in Arrays)
            {
                writer.Write(array.Type);
                writer.Write(array.Size);

                if ((array.Type & 0x80) != 0)
                {
                    writer.Write(array.DataSize);

                    switch (array.Type & 0x7F)
                    {
                        //INT8
                        case (int)AttributeTypes.UINT8:
                        case (int)AttributeTypes.INT8:
                            for (int i = 0; i < array.DataSize; ++i)
                                writer.Write(array.Data[i]);
                            break;
                        //IN16
                        case (int)AttributeTypes.UINT16:
                        case (int)AttributeTypes.INT16:
                            for (int i = 0; i < array.DataSize; ++i)
                            {
                                byte[] bytes = BitConverter.GetBytes(array.Data[i]);
                                writer.Write(bytes[0]);
                                writer.Write(bytes[1]);
                            }
                            break;
                        //INT32
                        case (int)AttributeTypes.UINT32:
                        case (int)AttributeTypes.INT32:
                        case (int)AttributeTypes.ENUM:
                            for (int i = 0; i < array.DataSize; ++i)
                            {
                                byte[] bytes = BitConverter.GetBytes(array.Data[i]);
                                writer.Write(bytes[0]);
                                writer.Write(bytes[1]);
                                writer.Write(bytes[2]);
                                writer.Write(bytes[3]);
                            }
                            break;
                    }
                }
            }

            writer.Close();
        }

        private void printValInfo(int data, int pos, bool debug)
        {
            if (debug)
                Console.WriteLine($"Value Info: Value: Type:{AttributeTypes.ENUM}, {data}, Value (Hex) 0x{data.ToString("X")}, Offset: {pos}, Offset (Hex): 0x{pos.ToString("X")}");
        }
    }
}
