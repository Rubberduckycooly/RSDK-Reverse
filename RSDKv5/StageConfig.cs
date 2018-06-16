using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class StageConfig : CommonConfig
    {
        bool UseGameObjects;

        public StageConfig(string filename) : this(new Reader(filename))
        {

        }

        public StageConfig(Stream stream) : this(new Reader(stream))
        {

        }

        internal StageConfig(Reader reader)
        {
            base.ReadMagic(reader);

            UseGameObjects = reader.ReadBoolean();

            base.ReadCommonConfig(reader);
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        internal void Write(Writer writer)
        {
            base.WriteMagic(writer);

            writer.Write(UseGameObjects);
            base.WriteCommonConfig(writer);

        }
    }
}
