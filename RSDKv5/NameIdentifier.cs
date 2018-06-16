using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class NameIdentifier
    {
        private readonly byte[] Hash;
        public readonly String Name = null;

        public NameIdentifier(string name)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                Hash = md5.ComputeHash(new System.Text.ASCIIEncoding().GetBytes(name));
            }
            Name = name;
        }

        public NameIdentifier(byte[] hash)
        {
            Hash = hash;
        }

        internal NameIdentifier(Reader reader)
        {
             Hash = reader.ReadBytes(16);
        }

        internal void Write(Writer writer)
        {
             writer.Write(Hash);
        }

        public string HashString()
        {
            return BitConverter.ToString(Hash).Replace("-", string.Empty).ToLower();
        }

        public override string ToString()
        {
            if (Name != null) return Name;
            return HashString();
        }
    }
}
