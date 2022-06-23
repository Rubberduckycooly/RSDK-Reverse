using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv5
{
    public class StaticVariables
    {
        enum StaticVariableTypes
        {
            UInt8,
            UInt16,
            UInt32,
            Int8,
            Int16,
            Int32,
            Bool,
            Pointer,
            Vector2,
            String,
            Animator,
            Hitbox,
            SpriteFrame,
        };

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

        public StaticVariables() { }

        public StaticVariables(string filename) : this(new Reader(filename)) { }

        public StaticVariables(System.IO.Stream stream) : this(new Reader(stream)) { }

        public StaticVariables(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(signature))
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

                        case (int)StaticVariableTypes.UInt8:
                            for (int i = 0; i < count; ++i)
                                array.values[i] = reader.ReadByte();
                            memPos += arraySize;
                            break;

                        case (int)StaticVariableTypes.Int8:
                            for (int i = 0; i < count; ++i)
                                array.values[i] = reader.ReadSByte();
                            memPos += arraySize;
                            break;

                        case (int)StaticVariableTypes.UInt16:
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

                        case (int)StaticVariableTypes.Int16:
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

                        case (int)StaticVariableTypes.UInt32:
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

                        case (int)StaticVariableTypes.Int32:
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

                        case (int)StaticVariableTypes.Bool:
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

                    // NOTE:
                    // These values assume 32bit alignment (aka a pointer is 4 bytes)
                    // so the mem positions won't be accurate for 64-bit compiled static vars (all consoles)

                    int tmpMemPos = 0;
                    switch (type)
                    {
                        case (int)StaticVariableTypes.UInt8:
                        case (int)StaticVariableTypes.Int8:
                            memPos += arraySize;
                            break;

                        case (int)StaticVariableTypes.UInt16:
                        case (int)StaticVariableTypes.Int16:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 2 * arraySize;
                            break;

                        case (int)StaticVariableTypes.UInt32:
                        case (int)StaticVariableTypes.Int32:
                        case (int)StaticVariableTypes.Bool:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 4 * arraySize;
                            break;

                        case (int)StaticVariableTypes.Pointer:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 4 * arraySize;
                            break;

                        case (int)StaticVariableTypes.Vector2:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 8 * arraySize;
                            break;

                        case (int)StaticVariableTypes.String:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 8 * arraySize;
                            break;

                        case (int)StaticVariableTypes.Animator:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFC) + 4);
                            if ((memPos & 0xFFFFFFFC) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 24 * arraySize;
                            break;

                        case (int)StaticVariableTypes.Hitbox:
                            tmpMemPos = (int)((memPos & 0xFFFFFFFE) + 2);
                            if ((memPos & 0xFFFFFFFE) >= memPos)
                                tmpMemPos = memPos;
                            memPos = tmpMemPos + 8 * arraySize;
                            break;

                        case (int)StaticVariableTypes.SpriteFrame:
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

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
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
                        case (int)StaticVariableTypes.UInt8:
                        case (int)StaticVariableTypes.Int8:
                            for (int i = 0; i < array.valueCount; ++i)
                                writer.Write(array.values[i]);
                            break;

                        case (int)StaticVariableTypes.UInt16:
                        case (int)StaticVariableTypes.Int16:
                            for (int i = 0; i < array.valueCount; ++i)
                            {
                                byte[] bytes = BitConverter.GetBytes(array.values[i]);
                                writer.Write(bytes[0]);
                                writer.Write(bytes[1]);
                            }
                            break;

                        case (int)StaticVariableTypes.UInt32:
                        case (int)StaticVariableTypes.Int32:
                        case (int)StaticVariableTypes.Bool:
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
