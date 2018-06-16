using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using System.IO;


namespace RSDKv5
{
    public class Objects
    {
        static List<ObjectInfo> objects = new List<ObjectInfo>();
        static Dictionary<string, ObjectInfo> hashToObject = new Dictionary<string, ObjectInfo>();

        public static void InitObjects(Stream stream)
        {
            var parser = new IniParser.StreamIniDataParser();
            IniData data = parser.ReadData(new StreamReader(stream));
            foreach (var section in data.Sections)
            {
                List<AttributeInfo> attributes = new List<AttributeInfo>();
                foreach (var key in section.Keys)
                {
                    AttributeTypes type;
                    if(!Enum.TryParse(key.Value, out type))
                    {
                        // unknown attribute, what to do?
                        Debug.WriteLine($"Unknown type! [{key.Value}]");
                    }
                    
                    attributes.Add(new AttributeInfo(new NameIdentifier(key.KeyName), type));
                }
                objects.Add(new ObjectInfo(new NameIdentifier(section.SectionName), attributes));
            }
            hashToObject = objects.ToDictionary(x => x.Name.HashString());
        }

        public static ObjectInfo GetObjectInfo(NameIdentifier name)
        {
            ObjectInfo res = null;
            hashToObject.TryGetValue(name.HashString(), out res);
            return res;
        }
    }
}
