using System;
using System.Collections.Generic;

namespace RSDKv2
{
    public class WAVConfiguration
    {
        public string Name;

        internal WAVConfiguration(Reader reader)
        {
            Name = reader.ReadRSDKString();
        }

        internal void Write(Writer writer)
        {
            writer.WriteRSDKString(Name);
        }
    }
}
