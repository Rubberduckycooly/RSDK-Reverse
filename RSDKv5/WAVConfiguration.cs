using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class WAVConfiguration
    {
        /// <summary>
        /// the name of the sound effect
        /// </summary>
        public string Name;

        public byte MaxConcurrentPlay;

        public WAVConfiguration()
        {
        }

        public WAVConfiguration(Reader reader)
        {
            Name = reader.ReadRSDKString();
            MaxConcurrentPlay = reader.ReadByte();
        }

        public void Write(Writer writer)
        {
            writer.WriteRSDKString(Name);
            writer.Write(MaxConcurrentPlay);
        }
    }
}
