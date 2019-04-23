using System;

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
        /// determines what "attributes" to load
        /// </summary>
        public ushort AttributeType;
        /// <summary>
        /// the "attributes"
        /// </summary>
        public uint[] attributes = new uint[15];
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

            //Attribute bits, 2 bytes, unsigned
            byte t1 = reader.ReadByte();
            byte t2 = reader.ReadByte();

            AttributeType = (ushort)((t2 << 8) + t1);

            // Object type, 1 byte, unsigned
            type = reader.ReadByte(); //Type

            // Object subtype, 1 byte, unsigned
            subtype = reader.ReadByte(); //SubType/PropertyValue

            //a position, made of 8 bytes, 4 for X, 4 for Y
            position = new Position(reader);

            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i] = uint.MaxValue;
            }

            if ((AttributeType & 0x1) != 0)
            {
                attributes[0] = reader.ReadUInt32();
            }
            if ((AttributeType & 0x2) != 0)
            {
                attributes[1] = reader.ReadByte();
            }
            if ((AttributeType & 0x4) != 0)
            {
                attributes[2] = reader.ReadUInt32();
            }
            if ((AttributeType & 0x8) != 0)
            {
                attributes[3] = reader.ReadUInt32();
            }
            if ((AttributeType & 0x10) != 0)
            {
                attributes[4] = reader.ReadByte();
            }
            if ((AttributeType & 0x20) != 0)
            {
                attributes[5] = reader.ReadByte();
            }
            if ((AttributeType & 0x40) != 0)
            {
                attributes[6] = reader.ReadByte();
            }
            if ((AttributeType & 0x80) != 0)
            {
                attributes[7] = reader.ReadByte();
            }
            if ((AttributeType & 0x100) != 0)
            {
                attributes[8] = reader.ReadUInt32();
            }
            if ((AttributeType & 0x200) != 0)
            {
                attributes[9] = reader.ReadByte();
            }
            if ((AttributeType & 0x400) != 0)
            {
                attributes[10] = reader.ReadByte();
            }
            if ((AttributeType & 0x800) != 0)
            {
                attributes[11] = reader.ReadUInt32();
            }
            if ((AttributeType & 0x1000) != 0)
            {
                attributes[12] = reader.ReadUInt32();
            }
            if ((AttributeType & 0x2000) != 0)
            {
                attributes[13] = reader.ReadUInt32();
            }
            else if ((AttributeType & 0x4000) != 0)
            {
                attributes[14] = reader.ReadUInt32();
            }
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

            if ((AttributeType & 0x1) != 0)
            {
                writer.Write(attributes[0]);
            }
            if ((AttributeType & 0x2) != 0)
            {
                writer.Write((byte)attributes[1]);
            }
            if ((AttributeType & 0x4) != 0)
            {
                writer.Write(attributes[2]);
            }
            if ((AttributeType & 0x8) != 0)
            {
                writer.Write(attributes[3]);
            }
            if ((AttributeType & 0x10) != 0)
            {
                writer.Write((byte)attributes[4]);
            }
            if ((AttributeType & 0x20) != 0)
            {
                writer.Write((byte)attributes[5]);
            }
            if ((AttributeType & 0x40) != 0)
            {
                writer.Write((byte)attributes[6]);
            }
            if ((AttributeType & 0x80) != 0)
            {
                writer.Write((byte)attributes[7]);
            }
            if ((AttributeType & 0x100) != 0)
            {
                writer.Write(attributes[8]);
            }
            if ((AttributeType & 0x200) != 0)
            {
                writer.Write((byte)attributes[9]);
            }
            if ((AttributeType & 0x400) != 0)
            {
                writer.Write((byte)attributes[10]);
            }
            if ((AttributeType & 0x800) != 0)
            {
                writer.Write(attributes[11]);
            }
            if ((AttributeType & 0x1000) != 0)
            {
                writer.Write(attributes[12]);
            }
            if ((AttributeType & 0x2000) != 0)
            {
                writer.Write(attributes[13]);
            }
            else if ((AttributeType & 0x4000) != 0)
            {
                writer.Write(attributes[14]);
            }
        }

    }
}
