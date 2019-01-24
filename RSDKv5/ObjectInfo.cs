using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class ObjectInfo
    {
        /// <summary>
        /// the object name
        /// </summary>
        public readonly NameIdentifier Name;
        /// <summary>
        /// a list of all the attributes
        /// </summary>
        public readonly List<AttributeInfo> Attributes;
        Dictionary<string, AttributeInfo> hashToAttribute;

        internal ObjectInfo(NameIdentifier name, List<AttributeInfo> attributes)
        {
            Name = name;
            Attributes = attributes;
            hashToAttribute = Attributes.ToDictionary(x => x.Name.HashString());
        }

        public AttributeInfo GetAttributeInfo(NameIdentifier name)
        {
            AttributeInfo res = null;
            hashToAttribute.TryGetValue(name.HashString(), out res);
            return res;
        }

        public AttributeInfo GetAttributeInfoHashed(NameIdentifier name)
        {
            AttributeInfo res = null;
            hashToAttribute.TryGetValue(name.HashString(), out res);
            return res;
        }
    }
}
