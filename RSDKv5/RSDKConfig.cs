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
            /// <summary>
            /// the name of the variable
            /// </summary>
            public string name  = "Variable";
            /// <summary>
            /// the variable's type, should always be "int" or "int[array_size_goes_here]"
            /// </summary>
            public string type  = "int"; // type of the variable
            /// <summary>
            /// the variable's default value, '-' signifies no default value
            /// </summary>
            public string value = "-";

            public Variable(string name = "", string type = "", string value = "")
            {
                this.name   = name;
                this.type   = type;
                this.value  = value;
            }

            public Variable(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name    = reader.ReadString();
                type    = reader.ReadString();
                value   = reader.ReadString();
            }

            public void Write(Writer writer)
            {
                writer.Write(name);
                writer.Write(type);
                writer.Write(value);
            }
        }

        public class Constant
        {
            /// <summary>
            /// the name of the constant variable
            /// </summary>
            public string name = "CONST_VAR";
            /// <summary>
            /// the value of the constant variable, should be an integer value
            /// </summary>
            public string value = "0";

            public Constant() { }

            public Constant(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name    = reader.ReadString();
                value   = reader.ReadString();
            }

            public void Write(Writer writer)
            {
                writer.Write(name);
                writer.Write(value);
            }
        }

        /// <summary>
        /// the list of editor info for global variables, see GameConfig for in-game info
        /// </summary>
        public List<Variable> variables = new List<Variable>();
        /// <summary>
        /// the list of global constant variables & their values
        /// </summary>
        public List<Constant> constants = new List<Constant>();

        public RSDKConfig() { }

        public RSDKConfig(string filename) : this(new Reader(filename)) { }

        public RSDKConfig(System.IO.Stream reader) : this(new Reader(reader)) { }

        public RSDKConfig(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            // Variables
            byte varCount = reader.ReadByte();
            variables.Count();
            for (int i = 0; i < varCount; i++)
                variables.Add(new Variable(reader));

            // Constants
            byte constCount = reader.ReadByte();
            constants.Clear();
            for (int i = 0; i < constCount; i++)
                constants.Add(new Constant(reader));

            reader.Close();
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
            // Variables
            writer.Write((byte)variables.Count);
            foreach (Variable variable in variables)
                variable.Write(writer);

            // Constants
            writer.Write((byte)constants.Count);
            foreach (Constant constant in constants)
                constant.Write(writer);

            writer.Close();
        }

    }
}
