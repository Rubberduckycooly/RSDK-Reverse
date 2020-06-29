using System;

namespace RSDKvB
{
    public class Object
    {

        public enum AttributesIDs
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
                return Position.X.High;
            }
            set
            {
                Position.X.High = value;
            }
        }
        /// <summary>
        /// a quick way to get the Y position
        /// </summary>
        public short yPos
        {
            get
            {
                return Position.Y.High;
            }
            set
            {
                Position.Y.High = value;
            }
        }
        /// <summary>
        /// determines what "attributes" to load
        /// </summary>
        public bool[] activeAttributes = new bool[15];
        /// <summary>
        /// the "attributes"
        /// </summary>
        public int[] attributes = new int[15];
        /// <summary>
        /// the raw position values
        /// </summary>
        private Position Position = new Position();
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

            ushort flags = (ushort)((t2 << 8) + t1);
            for (int i = 0; i < 15; i++)
            {
                activeAttributes[i] = (flags & (1 << i)) != 0;
            }

            // Object type, 1 byte, unsigned
            type = reader.ReadByte(); //Type

            // Object subtype, 1 byte, unsigned
            subtype = reader.ReadByte(); //SubType/PropertyValue

            //a position, made of 8 bytes, 4 for X, 4 for Y
            Position = new Position(reader);

            bool[] attribType = new bool[] {
                true,
                false,
                true,
                true,
                false,
                false,
                false,
                false,
                true,
                false,
                false,
                true,
                true,
                true,
                true,
            };

            for (int i = 0; i < attributes.Length; i++)
            {
                if (activeAttributes[i])
                {
                    if (attribType[i])
                        attributes[i] = reader.ReadByte();
                    else
                        attributes[i] = reader.ReadInt32();
                }
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

            int flags = 0;
            for (int i = 0; i < 15; i++)
            {
                if (activeAttributes[i])
                    flags |= 1 << i;
                else
                    flags &= ~(1 << i);
            }
            writer.Write((ushort)flags);

            writer.Write(type);
            writer.Write(subtype);

            Position.Write(writer);

            bool[] attribType = new bool[] {
                true,
                false,
                true,
                true,
                false,
                false,
                false,
                false,
                true,
                false,
                false,
                true,
                true,
                true,
                true,
            };

            for (int i = 0; i < attributes.Length; i++)
            {
                if (activeAttributes[i])
                {
                    if (attribType[i])
                        writer.Write((byte)attributes[i]);
                    else
                        writer.Write((int)attributes[i]);
                }
            }
        }

    }
}
