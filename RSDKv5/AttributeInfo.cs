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

    [Serializable]
    public class AttributeInfo
    {
        /// <summary>
        /// the name of the attribute
        /// </summary>
        public readonly NameIdentifier Name;
        public readonly NameIdentifier ManiacEditorObject;
        /// <summary>
        /// the type of the attribute
        /// </summary>
        public readonly AttributeTypes Type;

        public AttributeInfo(NameIdentifier name, AttributeTypes type)
        {
            this.Name = name;
            this.Type = type;
        }

        public AttributeInfo(string name, AttributeTypes type) : this(new NameIdentifier(name), type) { }

        internal AttributeInfo(Reader reader, ObjectInfo info = null)
        {
            ManiacEditorObject = new NameIdentifier("ManiacEditorObject");
            Name = new NameIdentifier(reader);
            Type = (AttributeTypes)reader.ReadByte();
            if (info != null)
            {
                var attribute = info.GetAttributeInfo(Name);
                if (attribute != null)
                {
                    // Type mismatch
                    //if (attribute.Type != Type) return;
                    Name = attribute.Name;
                }
                else
                {
                    var everyAttribute = Objects.GetGlobalAttributes();
                    string hashString = Name.HashString();
                    for (int i = 0; i < everyAttribute.Count; i++)
                    {
                        NameIdentifier currentName = new NameIdentifier(everyAttribute[i]);
                        String currentHashedName = currentName.HashString();
                        if (currentHashedName == hashString)
                        {
                            Name = currentName;
                            i = everyAttribute.Count;
                        }
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
