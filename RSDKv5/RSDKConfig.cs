using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class RSDKConfig
    {

        public class Variable
        {
            public string Name;
            public string Type;
            public string Value;

            public Variable(Reader reader)
            {
                Name = reader.ReadString();
                Type = reader.ReadString();
                Value = reader.ReadString();
            }

            public void Write(Writer writer)
            {
                writer.Write(Name);
                writer.Write(Type);
                writer.Write(Value);
            }
        }

        public class Alias
        {
            public string Name;
            public string Value;

            public Alias(Reader reader)
            {
                Name = reader.ReadString();
                Value = reader.ReadString();
            }

            public void Write(Writer writer)
            {
                writer.Write(Name);
                writer.Write(Value);
            }
        }

        public List<Variable> Variables = new List<Variable>();
        public List<Alias> Aliases = new List<Alias>();

        public RSDKConfig()
        {

        }

        public RSDKConfig(string filename) : this(new Reader(filename))
        {

        }

        public RSDKConfig(System.IO.Stream reader) : this(new Reader(reader))
        {

        }

        public RSDKConfig(Reader reader)
        {
            byte vcount = reader.ReadByte();

            for (int i = 0; i < vcount; i++)
            {
                Variables.Add(new Variable(reader));
                Console.WriteLine(Variables[i].Name + ", " + Variables[i].Type + ", " + Variables[i].Value);
            }

            byte acount = reader.ReadByte();

            for (int i = 0; i < acount; i++)
            {
                Aliases.Add(new Alias(reader));
                Console.WriteLine(Aliases[i].Name + ", " + Aliases[i].Value);
            }
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            writer.Write((byte)Variables.Count);

            for (int i = 0; i < Variables.Count; i++)
            {
                Variables[i].Write(writer);
            }

            writer.Write((byte)Aliases.Count);

            for (int i = 0; i < Aliases.Count; i++)
            {
                Aliases[i].Write(writer);
            }
        }

    }
}
