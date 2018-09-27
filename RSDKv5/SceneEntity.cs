using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    [Serializable]
    public class SceneEntity
    {
        public ushort SlotID;
        public Position Position;
        public SceneObject Object;
        public List<AttributeValue> Attributes = new List<AttributeValue>();
        public Dictionary<string, AttributeValue> attributesMap = new Dictionary<string, AttributeValue>();

        public SceneEntity(SceneObject obj, ushort slotID)
        {
            Object = obj;
            SlotID = slotID;

            foreach (AttributeInfo attribute in Object.Attributes)
            {
                Attributes.Add(new AttributeValue(attribute.Type));
                attributesMap[attribute.Name.ToString()] = Attributes.Last();
            }
        }

        public SceneEntity(SceneEntity other, ushort slotID)
        {
            Object = other.Object;
            SlotID = slotID;
            Position = other.Position;

            foreach (AttributeInfo attribute in Object.Attributes)
            {
                Attributes.Add(other.GetAttribute(attribute.Name).Clone());
                attributesMap[attribute.Name.ToString()] = Attributes.Last();
            }
        }

        internal SceneEntity(Reader reader, SceneObject obj)
        {
            Object = obj;
            SlotID = reader.ReadUInt16();
            Position = new Position(reader);

            foreach (AttributeInfo attribute in Object.Attributes)
            {
                Attributes.Add(new AttributeValue(reader, attribute.Type));
                attributesMap[attribute.Name.ToString()] = Attributes.Last();
            }
        }

        public AttributeValue GetAttribute(string name)
        {
            return attributesMap[name];
        }

        public AttributeValue GetAttribute(NameIdentifier name)
        {
            return GetAttribute(name.ToString());
        }

        internal void Write(Writer writer)
        {
            writer.Write(SlotID);
            Position.Write(writer);

            foreach (AttributeValue attribute in Attributes)
                attribute.Write(writer);
        }

        public void AddAttributeToObject(string name, AttributeTypes type)
        {
            Console.WriteLine("Attempted to add attribute of name \"" + name + "\" to entity \"" + Object.Name + "\"");
            Object.AddAttribute(new AttributeInfo(name, type));
        }

        public void AddAttributeToEntity(AttributeInfo att)
        {
            Console.WriteLine(Object.Name + " at slot " + SlotID + " recieved attribute \"" + att.Name + "\"");
            Attributes.Add(new AttributeValue(att.Type));
            attributesMap[att.Name.ToString()] = Attributes.Last();

            // Default filter to 1 for now
            // TODO: Make customizable via Object/Entity Manager
            attributesMap["filter"].ValueUInt8 = 1;
        }
    }
}
