using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class WAVConfiguration
    {
        public string Name;
        public byte MaxConcurrentPlay;

        internal WAVConfiguration(Reader reader)
        {
            Name = reader.ReadRSDKString();
            MaxConcurrentPlay = reader.ReadByte();
        }

        internal void Write(Writer writer)
        {
            writer.WriteRSDKString(Name);
            writer.Write(MaxConcurrentPlay);
        }
    }
}
