using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv5
{
    public class StaticObject
    {

        public class ArrayInfo
        {
            public byte type = 0x00;
            public int size = 0;
            public int valueCount = 0;
            public int[] values;
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'O', (byte)'B', (byte)'J', 0 };


        /// <summary>
        /// the list of arrays in the file
        /// </summary>
        public List<ArrayInfo> arrays = new List<ArrayInfo>();

        public StaticObject() { }

        public StaticObject(string filename) : this(new Reader(filename)) { }

        public StaticObject(System.IO.Stream stream) : this(new Reader(stream)) { }

        public StaticObject(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            if (!reader.readBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid Static Object v5 signature");
            }

            int memPos = 0;
            while (!reader.isEof)
            {
                int type = reader.ReadByte();
                int arraySize = reader.ReadInt32();

                if ((type & 0x80) != 0)
                {
                    uint count = reader.ReadUInt32();

                    type &= 0x7F;

                    ArrayInfo array = new ArrayInfo();
                    array.type = (byte)type;
                    array.size = arraySize;
                    array.valueCount = (int)count;
                    array.values = new int[(int)count];

                    switch (type)
                    {
                        default:
                            Console.WriteLine($"ERROR: Encountered unexpected array type ({type})!");
                            break;
                        //INT8
                        case (int)VariableTypes.UINT8:
                            for (int i = 0; i < count; ++i)
                                array.values[i] = reader.ReadByte();
                            memPos += arraySize;
                            break;
                        case (int)VariableTypes.INT8:
                            for (int i = 0; i < count; ++i)
                                array.values[i] = reader.ReadSByte();
                            memPos += arraySize;
                            break;
                        //IN16
                        case (int)VariableTypes.UINT16:
                            int tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos;

                            for (int i = 0; i < count; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                array.values[i] = (ushort)(valA + (valB << 8));
                            }

                            memPos += 2 * arraySize;
                            break;
                        case (int)VariableTypes.INT16:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos;

                            for (int i = 0; i < count; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                array.values[i] = (short)(valA + (valB << 8));
                            }
                            memPos += 2 * arraySize;
                            break;
                        //INT32
                        case (int)VariableTypes.UINT32:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos;

                            for (int i = 0; i < count; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                array.values[i] = (int)(uint)(valA + (valB << 8) + (valC << 16) + (valD << 24));
                            }
                            memPos += 4 * arraySize;
                            break;
                        case (int)VariableTypes.INT32:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos;

                            for (int i = 0; i < count; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                array.values[i] = valA + (valB << 8) + (valC << 16) + (valD << 24);
                            }
                            memPos += 4 * arraySize;
                            break;
                        case (int)VariableTypes.ENUM: // bool
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos;

                            for (int i = 0; i < count; ++i)
                            {
                                byte valA = reader.ReadByte();
                                byte valB = reader.ReadByte();
                                byte valC = reader.ReadByte();
                                byte valD = reader.ReadByte();
                                array.values[i] = valA + (valB << 8) + (valC << 16) + (valD << 24);
                            }
                            memPos += 4 * arraySize;
                            break;
                    }
                    arrays.Add(array);
                }
                else
                {
                    ArrayInfo array = new ArrayInfo();
                    array.type = (byte)type;
                    array.size = arraySize;
                    array.valueCount = 0;
                    array.values = new int[0];
                    arrays.Add(array);

                    int tmpMemPos = 0;
                    switch (type)
                    {
                        //INT8
                        case (int)VariableTypes.UINT8:
                        case (int)VariableTypes.INT8:
                            memPos += arraySize;
                            break;
                        //IN16
                        case (int)VariableTypes.UINT16:
                        case (int)VariableTypes.INT16:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 2 * arraySize;
                            break;
                        //INT32
                        case (int)VariableTypes.UINT32:
                        case (int)VariableTypes.INT32:
                        case 6: //bool
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 4 * arraySize;
                            break;
                        case 7: // Pointer
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 4 * arraySize;
                            break;
                        case 8: // Vector2
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 8 * arraySize;
                            break;
                        case 9: // Text
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 8 * arraySize;
                            break;
                        case 10: // Animator
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 24 * arraySize;
                            break;
                        case 11: // Hitbox
                            tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 8 * arraySize;
                            break;
                        case 12: // Unknown
                            tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 18 * arraySize;
                            break;
                        default:
                            break;
                    }
                }
            }
            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            writer.Write(signature);

            foreach (ArrayInfo array in arrays)
            {
                writer.Write((byte)(array.valueCount > 0 ? (array.type | 0x80) : array.type));
                writer.Write(array.size);

                if (array.valueCount > 0)
                {
                    writer.Write(array.valueCount);

                    switch (array.type)
                    {
                        //INT8
                        case (int)VariableTypes.UINT8:
                        case (int)VariableTypes.INT8:
                            for (int i = 0; i < array.valueCount; ++i)
                                writer.Write(array.values[i]);
                            break;
                        //IN16
                        case (int)VariableTypes.UINT16:
                        case (int)VariableTypes.INT16:
                            for (int i = 0; i < array.valueCount; ++i)
                            {
                                byte[] bytes = BitConverter.GetBytes(array.values[i]);
                                writer.Write(bytes[0]);
                                writer.Write(bytes[1]);
                            }
                            break;
                        //INT32
                        case (int)VariableTypes.UINT32:
                        case (int)VariableTypes.INT32:
                        case (int)VariableTypes.ENUM: // bool
                            for (int i = 0; i < array.valueCount; ++i)
                            {
                                byte[] bytes = BitConverter.GetBytes(array.values[i]);
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
    }
}
