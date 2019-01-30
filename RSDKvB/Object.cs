using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
{
    public class Object
    {
        /// <summary>
        /// the Object's Name (used for entity list)
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// The Type of the object
        /// </summary>
        public byte type;
        /// <summary>
        /// The Object's SubType/PropertyValue
        /// </summary>
        public byte subtype;
        /// <summary>
        /// a quick way to get the X position
        /// </summary>
        public short xPos
        {
            get
            {
                return position.X.High;
            }
            set
            {
                position.X.High = (short)value;
            }
        }
        /// <summary>
        /// a quick way to get the Y position
        /// </summary>
        public short yPos
        {
            get
            {
                return position.Y.High;
            }
            set
            {
                position.Y.High = (short)value;
            }
        }
        /// <summary>
        /// how to load the "attribute"?
        /// </summary>
        public ushort AttributeType;
        /// <summary>
        /// the attribute?
        /// </summary>
        public int attribute;
        /// <summary>
        /// the raw position values
        /// </summary>
        public Position position = new Position();

        /// <summary>
        /// How Many Objects have been loaded
        /// </summary>
        public static int cur_id = 0;
        /// <summary>
        /// the Index of the object in the loaded Object List
        /// </summary>
        public int id;

        public Object()
        {
        }

        public Object(byte type, byte subtype, short xPos, short yPos) : this(type, subtype, xPos, yPos, cur_id++)
        {
        }

        public Object(byte type, byte subtype, short xPos, short yPos, int id)
        {
            Name = "Unknown Object";
            this.type = type;
            this.subtype = subtype;
            this.xPos = xPos;
            this.yPos = yPos;
            this.id = id;
        }

        public Object(byte type, byte subtype, short xPos, short yPos, int id, string name)
        {
            this.Name = name;
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

            AttributeType = reader.ReadUInt16();

            // Object type, 1 byte, unsigned
            type = reader.ReadByte(); //Type

            // Object subtype, 1 byte, unsigned
            subtype = reader.ReadByte(); //SubType/PropertyValue

            //a position, made of 8 bytes, 4 for X, 4 for Y
            position = new Position(reader);

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

            writer.Write(AttributeType);

            writer.Write(type);
            writer.Write(subtype);

            position.Write(writer);

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
                    attribute = reader.ReadInt32();
                    break;
                case 1:
                    attribute = reader.ReadInt32();
                    break;
                case 2:
                    attribute = reader.ReadInt32();
                    break;
                case 3:
                    attribute = reader.ReadByte();
                    break;
                case 4:
                    attribute = reader.ReadByte();
                    break;
                case 5:
                    attribute = reader.ReadByte();
                    break;
                case 6:
                    attribute = reader.ReadByte();
                    break;
                case 7:
                    attribute = reader.ReadInt32();
                    break;
                case 8:
                    attribute = reader.ReadByte();
                    break;
                case 9:
                    attribute = reader.ReadByte();
                    break;
                case 10:
                    attribute = reader.ReadInt32();
                    break;
                case 11:
                    attribute = reader.ReadInt32();
                    break;
                case 12:
                    attribute = reader.ReadInt32();
                    break;
                case 13:
                    attribute = reader.ReadInt32();
                    break;
            }
        }

    }
}
