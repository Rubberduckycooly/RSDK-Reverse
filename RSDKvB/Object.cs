using System;

namespace RSDKvB
{
    public class Object
    {

        public enum Attributes
        {
            ATTRIBUTE_STATE,
            ATTRIBUTE_DIRECTION,
            ATTRIBUTE_SCALE,
            ATTRIBUTE_ROTATION,
            ATTRIBUTE_DRAWORDER,
            ATTRIBUTE_PRIORITY,
            ATTRIBUTE_ALPHA,
            ATTRIBUTE_ANIMATION,
            ATTRIBUTE_ANIMATIONSPEED,
            ATTRIBUTE_FRAME,
            ATTRIBUTE_INKEFFECT,
            ATTRIBUTE_VALUE1,
            ATTRIBUTE_VALUE2,
            ATTRIBUTE_VALUE3,
            ATTRIBUTE_VALUE4,
        };


        public static string[] AttributeNames = new string[] {
            "State",
            "Direction",
            "Scale",
            "Rotation",
            "DrawOrder",
            "Priority",
            "Alpha",
            "Animation",
            "AnimationSpeed",
            "Frame",
            "InkEffect",
            "Value1",
            "Value2",
            "Value3",
            "Value4",
        };

        public static string[] AttributeTypes = new string[] {
            "int",
            "uint8",
            "int",
            "int",
            "uint8",
            "uint8",
            "uint8",
            "uint8",
            "int",
            "uint8",
            "uint8",
            "int",
            "int",
            "int",
            "int",
        };

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
        public int[] attributes = new int[15];
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
            Name = "Value Object";
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
                attributes[i] = int.MaxValue;
            }

            if ((AttributeType & 0x1) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_STATE] = reader.ReadInt32();
            }
            if ((AttributeType & 0x2) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_DIRECTION] = reader.ReadByte();
            }
            if ((AttributeType & 0x4) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_SCALE] = reader.ReadInt32();
            }
            if ((AttributeType & 0x8) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_ROTATION] = reader.ReadInt32();
            }
            if ((AttributeType & 0x10) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_DRAWORDER] = reader.ReadByte();
            }
            if ((AttributeType & 0x20) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_PRIORITY] = reader.ReadByte();
            }
            if ((AttributeType & 0x40) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_ALPHA] = reader.ReadByte();
            }
            if ((AttributeType & 0x80) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_ANIMATION] = reader.ReadByte();
            }
            if ((AttributeType & 0x100) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_ANIMATIONSPEED] = reader.ReadInt32();
            }
            if ((AttributeType & 0x200) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_FRAME] = reader.ReadByte();
            }
            if ((AttributeType & 0x400) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_INKEFFECT] = reader.ReadByte();
            }
            if ((AttributeType & 0x800) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_VALUE1] = reader.ReadInt32();
            }
            if ((AttributeType & 0x1000) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_VALUE2] = reader.ReadInt32();
            }
            if ((AttributeType & 0x2000) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_VALUE3] = reader.ReadInt32();
            }
            else if ((AttributeType & 0x4000) != 0)
            {
                attributes[(int)Attributes.ATTRIBUTE_VALUE4] = reader.ReadInt32();
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
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_STATE]);
            }
            if ((AttributeType & 0x2) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_DIRECTION]);
            }
            if ((AttributeType & 0x4) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_SCALE]);
            }
            if ((AttributeType & 0x8) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_ROTATION]);
            }
            if ((AttributeType & 0x10) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_DRAWORDER]);
            }
            if ((AttributeType & 0x20) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_PRIORITY]);
            }
            if ((AttributeType & 0x40) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_ALPHA]);
            }
            if ((AttributeType & 0x80) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_ANIMATION]);
            }
            if ((AttributeType & 0x100) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_ANIMATIONSPEED]);
            }
            if ((AttributeType & 0x200) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_FRAME]);
            }
            if ((AttributeType & 0x400) != 0)
            {
                writer.Write((byte)attributes[(int)Attributes.ATTRIBUTE_INKEFFECT]);
            }
            if ((AttributeType & 0x800) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_VALUE1]);
            }
            if ((AttributeType & 0x1000) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_VALUE2]);
            }
            if ((AttributeType & 0x2000) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_VALUE3]);
            }
            else if ((AttributeType & 0x4000) != 0)
            {
                writer.Write(attributes[(int)Attributes.ATTRIBUTE_VALUE4]);
            }
        }

    }
}
