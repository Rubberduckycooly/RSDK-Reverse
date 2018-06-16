using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class ObjectInfo
    {
        public readonly NameIdentifier Name;

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
    }
}
