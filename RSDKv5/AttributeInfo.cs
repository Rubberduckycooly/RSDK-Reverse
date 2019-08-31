using System;

namespace RSDKv5
{
    [Serializable]
    public enum AttributeTypes
    {
        UINT8 = 0,
        UINT16 = 1,
        UINT32 = 2,
        INT8 = 3,
        INT16 = 4,
        INT32 = 5,
        ENUM = 6,
        BOOL = 7,
        STRING = 8,
        VECTOR2 = 9,
        VECTOR3 = 10, //A wild guess, but nonetheless likely
        COLOR = 11,
    }

    [Serializable]
    public class AttributeInfo
    {
        /// <summary>
        /// the name of the attribute
        /// </summary>
        public NameIdentifier Name;
        /// <summary>
        /// the type of the attribute
        /// </summary>
        public AttributeTypes Type;

        public AttributeInfo()
        {
            this.Name = new NameIdentifier("attribute");
            this.Type = 0;
        }
        public AttributeInfo(NameIdentifier name, AttributeTypes type)
        {
            this.Name = name;
            this.Type = type;
        }

        public AttributeInfo(string name, AttributeTypes type) : this(new NameIdentifier(name), type) { }

        internal AttributeInfo(Reader reader)
        {
            Name = new NameIdentifier(reader);
            Type = (AttributeTypes)reader.ReadByte();
            string attribute = Objects.GetAttributeName(Name);
            if (attribute != null)
            {
                // Type mismatch
                //if (attribute.Type != Type) return;
                Name.Name = attribute;
            }
            else
            {
                var everyAttribute = Objects.AttributeNames;
                string hashString = Name.HashString();
                foreach (System.Collections.Generic.KeyValuePair<string, string> s in everyAttribute)
                {
                    NameIdentifier currentName = new NameIdentifier(s.Value);
                    String currentHashedName = currentName.HashString();
                    if (currentHashedName == hashString)
                    {
                        Name = currentName;
                        break;
                    }
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
