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
        public NameIdentifier Name
        {
            get;
            private set;
        }
        public readonly List<AttributeInfo> Attributes = new List<AttributeInfo>();
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

        public void AddAttribute(AttributeInfo att)
        {
            Console.WriteLine("Attempted to add attribute of name \"" + att.Name + "\" to object \"" + Name + "\"");
            Attributes.Add(att);
            foreach (SceneEntity entity in Entities)
                entity.AddAttributeToEntity(att);
        }
    }
}
