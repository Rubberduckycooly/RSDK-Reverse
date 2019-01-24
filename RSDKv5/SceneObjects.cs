using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    [Serializable]
    public class SceneObject
    {
        /// <summary>
        /// the name of this type of object
        /// </summary>
        public NameIdentifier Name
        {
            get;
            private set;
        }
        /// <summary>
        /// the names and types of each attribute of this object
        /// </summary>
        public readonly List<AttributeInfo> Attributes = new List<AttributeInfo>();
        /// <summary>
        /// a list of entities using this type
        /// </summary>
        public List<SceneEntity> Entities = new List<SceneEntity>();


        public SceneObject(NameIdentifier name, List<AttributeInfo> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        /*public SceneObjects(string name, List<AttributeInfo> attributes) : this(new NameIdentifier(name), attributes) { }*/

        internal SceneObject(Reader reader)
        {
            Name = new NameIdentifier(reader);
            var info = Objects.GetObjectInfo(Name);
            if (info != null) Name = info.Name;

            byte attributes_count = reader.ReadByte();
            for (int i = 1; i < attributes_count; ++i)
                Attributes.Add(new AttributeInfo(reader, info));

            ushort entities_count = reader.ReadUInt16();
            for (int i = 0; i < entities_count; ++i)
                Entities.Add(new SceneEntity(reader, this));
                
        }

        internal void Write(Writer writer)
        {
            Name.Write(writer);

            writer.Write((byte)(Attributes.Count + 1));
            foreach (AttributeInfo attribute in Attributes)
                attribute.Write(writer);

            writer.Write((ushort)Entities.Count);
            foreach (SceneEntity entity in Entities)
                entity.Write(writer);
        }

        public void AddAttribute(string attName, AttributeTypes attType)
        {
            AttributeInfo att = new AttributeInfo(attName, attType);
            Console.WriteLine("Attempting to add attribute of name \"" + attName + "\" to object \"" + Name + "\"");
            Attributes.Add(att);
            foreach (SceneEntity entity in Entities)
            {
                Console.WriteLine(Name + " at slot " + entity.SlotID + " recieved attribute \"" + att.Name + "\"");
                entity.Attributes.Add(new AttributeValue(att.Type));
                entity.attributesMap[att.Name.ToString()] = entity.Attributes.Last();
            }
        }

        public void RemoveAttribute(string attName)
        {
            Console.WriteLine("Attempting to remove attribute of name \"" + attName + "\" from object \"" + Name + "\"");
            for (int i = 0; i < Attributes.Count; i++)
            {
                if (Attributes[i].Name.Name == attName)
                {
                    Console.WriteLine("Attribute \"" + attName + "\" found, attempting removal...");
                    Attributes.RemoveAt(i);
                    foreach (SceneEntity entity in Entities)
                    {
                        Console.WriteLine(Name + " at slot " + entity.SlotID + " lost attribute \"" + attName + "\"");
                        entity.Attributes.RemoveAt(i);
                        entity.attributesMap.Remove(attName);
                    }
                    return;
                }
            }
            Console.WriteLine("Removal failed because attribute \"" + attName + "\" wasn't found!!");
        }

        public bool HasAttributeOfName(string name)
        {
            foreach (AttributeInfo att in Attributes)
                if (att.Name.Name == name)
                    return true;
            return false;
        }
    }
}
