using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace RSDKv5
{
    [Serializable]
    [XmlRoot("dictionary")]
    public class DictionaryWithDefault<TKey, TValue> : Dictionary<TKey, TValue>, ISerializable, IXmlSerializable
    {
        public TValue DefaultValue { get; set; }

        #region Constructors
        public DictionaryWithDefault() : base() 
        { 

        }
        public DictionaryWithDefault(TValue defaultValue) : base()
        {
            DefaultValue = defaultValue;
        }
        public DictionaryWithDefault(IDictionary<TKey, TValue> dictionary, TValue defaultValue) : base(dictionary) 
        {
            DefaultValue = defaultValue;
        }
        protected DictionaryWithDefault(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            //DefaultValue = (TValue)info.GetValue("DefaultValue", typeof(TValue));
        }
        #endregion

        public new TValue this[TKey key]
        {
            get
            {
                TValue t;
                return base.TryGetValue(key, out t) ? t : DefaultValue;
            }
            set { base[key] = value; }
        }

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
