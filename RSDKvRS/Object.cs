using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
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
        /// The Object's X Position
        /// </summary>
        public short xPos;
        /// <summary>
        /// The Object's Y Position
        /// </summary>
        public short yPos;
        /// <summary>
        /// how to load the "attribute"?
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

            // Object type, 1 byte, unsigned
            type = reader.ReadByte();
            // Object subtype, 1 byte, unsigned
            subtype = reader.ReadByte();

            // X Position, 2 bytes, big-endian, signed			
            xPos = (short)(reader.ReadSByte() << 8);
            xPos |= (short)reader.ReadByte();

            // Y Position, 2 bytes, big-endian, signed
            yPos = (short)(reader.ReadSByte() << 8);
            yPos |= (short)reader.ReadByte();

            Console.WriteLine(id + " Obj Values: Type: " + type + ", Subtype: " + subtype + ", Xpos = " + xPos + ", Ypos = " + yPos);
        }

        public void Write(Writer writer)
        {
            if (type > 255)
                throw new Exception("Cannot save as Type vRS. Object type > 255");

            if (subtype > 255)
                throw new Exception("Cannot save as Type vRS. Object subtype > 255");

            if (xPos < -32768 || xPos > 32767)
                throw new Exception("Cannot save as Type vRS. Object X Position can't fit in 16-bits");

            if (yPos < -32768 || yPos > 32767)
                throw new Exception("Cannot save as Type vRS. Object Y Position can't fit in 16-bits");

            writer.Write(type);
            writer.Write(subtype);

            writer.Write((byte)(xPos >> 8));
            writer.Write((byte)(xPos & 0xFF));

            writer.Write((byte)(yPos >> 8));
            writer.Write((byte)(yPos & 0xFF));

            Console.WriteLine(id + " Obj Values: Type: " + type + ", Subtype: " + subtype + ", Xpos = " + xPos + ", Ypos = " + yPos);
        }
    }
}
