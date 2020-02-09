using System.IO;

namespace RSDKv5
{
    public class Stageconfig : CommonConfig
    {
        /// <summary>
        /// the path to this file
        /// </summary>
        public string FilePath;

        /// <summary>
        /// whether or not we use the global objects in this stage
        /// </summary>
        public bool LoadGlobalObjects;

        public Stageconfig(string filename) : this(new Reader(filename))
        {
            FilePath = filename;
        }

        public Stageconfig()
        {
            for (int i = 0; i < Palettes.Length; i++)
            {
                Palettes[i] = new Palette();
            }
        }

        public Stageconfig(Stream stream) : this(new Reader(stream))
        {

        }

        public Stageconfig(Reader reader)
        {
            base.ReadMagic(reader);

            LoadGlobalObjects = reader.ReadBoolean();

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

        public void Write(Writer writer)
        {
            base.WriteMagic(writer);

            writer.Write(LoadGlobalObjects);
            base.WriteCommonConfig(writer);

        }
    }
}
