using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
{
    public class Object
    {
        public byte type;
        public byte subtype;
        public int xPos;
        public int yPos;
        public ushort AttributeType;
        public int attribute;


        static int cur_id = 0;
        public int id;

        public Object(byte type, byte subtype, int xPos, int yPos) : this(type, subtype, xPos, yPos, cur_id++)
        {
        }

        private Object(byte type, byte subtype, int xPos, int yPos, int id)
        {
            this.type = type;
            this.subtype = subtype;
            this.xPos = xPos;
            this.yPos = yPos;
            this.id = id;
        }

        public Object(Reader reader)
        {
            cur_id++;
            id = cur_id;

            /*
            type = reader.ReadByte();
            subtype = reader.ReadByte();

		
            xPos = reader.ReadSByte() << 8;
            xPos |= reader.ReadByte();

            yPos = reader.ReadSByte() << 8;
            yPos |= reader.ReadByte();*/

            AttributeType = reader.ReadUInt16();

            // Object type, 1 byte, unsigned
            type = reader.ReadByte(); //Type

            // Object subtype, 1 byte, unsigned
            subtype = reader.ReadByte(); //SubType

            //4 Position "Buffer" Bytes
            byte v15;
            byte v16;
            byte v17;
            byte v18;

            v15 = reader.ReadByte();
            v16 = reader.ReadByte();
            v17 = reader.ReadByte();
            v18 = reader.ReadByte();

            // X Position, 4 bytes, big-endian, unsigned	
            xPos = (v17 << 16) + (v16 << 8) + v15 + (v18 << 24);

            v15 = reader.ReadByte();
            v16 = reader.ReadByte();
            v17 = reader.ReadByte();
            v18 = reader.ReadByte();

            // Y Position, 4 bytes, big-endian, unsigned
            yPos = (v17 << 16) + (v16 << 8) + v15 + (v18 << 24);

            //Console.WriteLine("ATTRIBUTE COUNT:" + Pad(AttributeType,16));

            string Pad(int b, int length)
            {
                return Convert.ToString(b, 2).PadLeft(length, '0');
            }

            if ((AttributeType & 1) != 0)
            {
                if ((AttributeType & 2) == 0)
                {
                    if ((AttributeType & 4) == 0)
                    {
                        if ((AttributeType & 8) == 0)
                        {
                            if ((AttributeType & 0x10) == 0)
                            {
                                if ((AttributeType & 0x20) == 0)
                                {
                                    if ((AttributeType & 0x40) == 0)
                                    {
                                        if ((AttributeType & 0x80) == 0)
                                        {
                                            if ((AttributeType & 0x100) == 0)
                                            {
                                                if ((AttributeType & 0x200) == 0)
                                                {
                                                    if ((AttributeType & 0x400) == 0)
                                                    {
                                                        if ((AttributeType & 0x800) == 0)
                                                        {
                                                            if ((AttributeType & 0x1000) == 0)
                                                            {
                                                                if ((AttributeType & 0x2000) == 0)
                                                                {
                                                                    if ((AttributeType & 0x4000) != 0)
                                                                    {
                                                                        ReadAttribute(reader, 13);
                                                                    }
                                                                    goto END_LOOP;
                                                                }
                                                                ReadAttribute(reader, 12);
                                                                goto END_LOOP;
                                                            }
                                                            ReadAttribute(reader, 11);
                                                            goto END_LOOP;
                                                        }
                                                        ReadAttribute(reader, 10);
                                                        goto END_LOOP;
                                                    }
                                                    ReadAttribute(reader, 9);
                                                    goto END_LOOP;
                                                }
                                                ReadAttribute(reader, 8);
                                                goto END_LOOP;
                                            }
                                            ReadAttribute(reader, 7);
                                            goto END_LOOP;
                                        }
                                        ReadAttribute(reader, 6);
                                        goto END_LOOP;
                                    }
                                    ReadAttribute(reader, 5);
                                    goto END_LOOP;
                                }
                                ReadAttribute(reader, 4);
                                goto END_LOOP;
                            }
                            ReadAttribute(reader, 3);
                            goto END_LOOP;
                        }
                        ReadAttribute(reader, 2);
                        goto END_LOOP;
                    }
                    ReadAttribute(reader,1);
                    goto END_LOOP;
                }
                ReadAttribute(reader, 0);
                goto END_LOOP;
            }

            END_LOOP:

            Console.WriteLine(id + " Obj Values: Type: " + type + ", Subtype: " + subtype + ", Xpos = " + xPos + ", Ypos = " + yPos + ", Attribute Type = " + AttributeType + ", Attribute = " + attribute);
        }

        public void Write(Writer writer)
        {
            if (type > 255)
            {
                //throw new Exception("Cannot save as Type vB. Object type > 255");
                Console.WriteLine("Cannot save as Type vB. Object type > 255");
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write(0);
                writer.Write(0);
                return;
            }

            if (subtype > 255)
            {
                //throw new Exception("Cannot save as Type vB. Object subtype > 255");
                Console.WriteLine("Cannot save as Type vB. Object subtype > 255");
                writer.Write(type);
                writer.Write((byte)0);
                writer.Write(0);
                writer.Write(0);
                return;
            }

            if (xPos < Int32.MinValue || xPos > Int32.MaxValue)
            {
                //throw new Exception("Cannot save as Type vB. Object X Position can't fit in 32-bits");
                Console.WriteLine("Cannot save as Type vB. Object X Position can't fit in 32-bits");
                writer.Write(type);
                writer.Write(subtype);
                writer.Write(0);
                writer.Write(0);
                return;
            }

            if (yPos < Int32.MinValue || yPos > Int32.MaxValue)
            {
                //throw new Exception("Cannot save as Type vB. Object Y Position can't fit in 32-bits");
                Console.WriteLine("Cannot save as Type vB. Object Y Position can't fit in 32-bits");
                writer.Write(type);
                writer.Write(subtype);
                writer.Write(xPos);
                writer.Write(0);
                return;
            }

            writer.Write(AttributeType);

            writer.Write(type);
            writer.Write(subtype);

            //writer.Write((byte)(xPos >> 8));
            //writer.Write((byte)(xPos & 0xFF));
            writer.Write(xPos);

            //writer.Write((byte)(yPos >> 8));
            //writer.Write((byte)(yPos & 0xFF));
            writer.Write(yPos);

            if ((AttributeType & 1) != 0)
            {
                if ((AttributeType & 2) != 0)
                {
                    if ((AttributeType & 4) != 0)
                    {
                        if ((AttributeType & 8) != 0)
                        {
                            if ((AttributeType & 0x10) != 0)
                            {
                                if ((AttributeType & 0x20) != 0)
                                {
                                    if ((AttributeType & 0x40) != 0)
                                    {
                                        if ((AttributeType & 0x80) != 0)
                                        {
                                            if ((AttributeType & 0x100) != 0)
                                            {
                                                if ((AttributeType & 0x200) != 0)
                                                {
                                                    if ((AttributeType & 0x400) != 0)
                                                    {
                                                        if ((AttributeType & 0x800) != 0)
                                                        {
                                                            if ((AttributeType & 0x1000) != 0)
                                                            {
                                                                if ((AttributeType & 0x2000) != 0)
                                                                {
                                                                    if ((AttributeType & 0x4000) != 0)
                                                                    {
                                                                        writer.Write(attribute);
                                                                    }
                                                                    goto END_LOOP;
                                                                }
                                                                writer.Write(attribute);
                                                                goto END_LOOP;
                                                            }
                                                            writer.Write(attribute);
                                                            goto END_LOOP;
                                                        }
                                                        writer.Write(attribute);
                                                        goto END_LOOP;
                                                    }
                                                    writer.Write((byte)attribute);
                                                    goto END_LOOP;
                                                }
                                                writer.Write((byte)attribute);
                                                goto END_LOOP;
                                            }
                                            writer.Write(attribute);
                                            goto END_LOOP;
                                        }
                                        writer.Write((byte)attribute);
                                        goto END_LOOP;
                                    }
                                    writer.Write((byte)attribute);
                                    goto END_LOOP;
                                }
                                writer.Write((byte)attribute);
                                goto END_LOOP;
                            }
                            writer.Write((byte)attribute);
                            goto END_LOOP;
                        }
                        writer.Write(attribute);
                        goto END_LOOP;
                    }
                    writer.Write(attribute);
                    goto END_LOOP;
                }
                writer.Write(attribute);
                goto END_LOOP;
            }
            else
            {
                writer.Write((byte)attribute);
            }

            END_LOOP:

            Console.WriteLine(id + " Obj Values: Type: " + type + ", Subtype: " + subtype + ", Xpos = " + xPos + ", Ypos = " + yPos + ", Attribute Type = " + AttributeType + ", Attribute = " + attribute);
        }
        

        private void ReadAttribute(Reader reader, int ID)
        {
            switch(ID)
            {
                case 0:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 1:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 2:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 3:
                    attribute = (int)reader.ReadByte();
                    break;
                case 4:
                    attribute = (int)reader.ReadByte();
                    break;
                case 5:
                    attribute = (int)reader.ReadByte();
                    break;
                case 6:
                    attribute = (int)reader.ReadByte();
                    break;
                case 7:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 8:
                    attribute = (int)reader.ReadByte();
                    break;
                case 9:
                    attribute = (int)reader.ReadByte();
                    break;
                case 10:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 11:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 12:
                    attribute = (int)reader.ReadInt32();
                    break;
                case 13:
                    attribute = (int)reader.ReadInt32();
                    break;
            }
        }

    }
}
