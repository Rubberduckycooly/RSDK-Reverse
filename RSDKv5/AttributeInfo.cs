using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public enum AttributeTypes
    {
        UINT8 = 0,
        UINT16 = 1,
        UINT32 = 2,
        INT8 = 3,
        INT16 = 4,
        INT32 = 5,
        VAR = 6,
        BOOL = 7,
        STRING = 8,
        POSITION = 9,
        COLOR = 11,
    }

    public class AttributeInfo
    {
        public readonly NameIdentifier Name;
        public readonly AttributeTypes Type;

        public AttributeInfo(NameIdentifier name, AttributeTypes type)
        {
            this.Name = name;
            this.Type = type;
        }

        public AttributeInfo(string name, AttributeTypes type) : this(new NameIdentifier(name), type) { }

        internal AttributeInfo(Reader reader, ObjectInfo info = null)
        {
            Name = new NameIdentifier(reader);
            Type = (AttributeTypes)reader.ReadByte();
            if (info != null)
            {
                var attribute = info.GetAttributeInfo(Name);
                if (attribute != null)
                {
                    // Type mismatch
                    if (attribute.Type != Type) return;
                    Name = attribute.Name;
                }
            }
        }

        internal void Write(Writer writer)
        {
            Name.Write(writer);
            writer.Write((byte)Type);
        }
    }
}
