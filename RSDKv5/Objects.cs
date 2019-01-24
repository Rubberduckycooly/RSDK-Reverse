using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace RSDKv5
{
    public class Objects
    {
        static List<ObjectInfo> objects = new List<ObjectInfo>();
        static List<String> ObjectNames = new List<String>();
        static List<String> AttributeNames = new List<String>();
        static Dictionary<string, ObjectInfo> hashToObject = new Dictionary<string, ObjectInfo>();

        public static void InitObjects(Stream stream, bool skipHash = false)
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
                        Debug.WriteLine($"Unknown type in object_attributes.ini! [{key.Value}]");
                    }
                    
                    attributes.Add(new AttributeInfo(new NameIdentifier(key.KeyName), type));
                    if (!AttributeNames.Contains(key.KeyName))
                    {
                        AttributeNames.Add(key.KeyName);
                    }
                }
                objects.Add(new ObjectInfo(new NameIdentifier(section.SectionName), attributes));
                if (!ObjectNames.Contains(section.SectionName))
                {
                    ObjectNames.Add(section.SectionName);
                }
            }
            if (skipHash != true)
            {
                hashToObject = objects.ToDictionary(x => x.Name.HashString());
            }


        }

        public static ObjectInfo GetObjectInfo(NameIdentifier name)
        {
            ObjectInfo res = null;
            hashToObject.TryGetValue(name.HashString(), out res);
            return res;
        }

        public static List<String> GetGlobalAttributes()
        {
            return AttributeNames;
        }

        public static List<String> GetGlobalNames()
        {
            return ObjectNames;
        }
    }
}
