using System.IO;

namespace RSDKv5
{
    public class StageConfig : CommonConfig
    {
        /// <summary>
        /// the path to this file
        /// </summary>
        public string FilePath;

        /// <summary>
        /// whether or not we use the global objects in this stage
        /// </summary>
        public bool LoadGlobalObjects;

        public StageConfig(string filename) : this(new Reader(filename))
        {
            FilePath = filename;
        }

        public StageConfig()
        {
            for (int i = 0; i < Palettes.Length; i++)
            {
                Palettes[i] = new Palette();
            }
        }

        public StageConfig(Stream stream) : this(new Reader(stream))
        {

        }

        public StageConfig(Reader reader)
        {
            base.ReadMagic(reader);

            LoadGlobalObjects = reader.ReadBoolean();

            base.ReadCommonConfig(reader);

            foreach (string Name in ObjectsNames)
            {
                string Hash = RSDKv5.Objects.GetMd5HashString(Name);
                if (!RSDKv5.Objects.ObjectNames.ContainsKey(Hash))
                {
                    RSDKv5.Objects.AddObjectName(Name);
                }
            }
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
