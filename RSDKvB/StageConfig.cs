using System.Collections.Generic;

namespace RSDKvB
{
    public class Stageconfig
    {
        /// <summary>
        /// the stageconfig palette (index 96-128)
        /// </summary>
        public Palette StagePalette = new Palette();
        /// <summary>
        /// the list of Stage SoundFX paths
        /// </summary>
        public List<string> SoundFX = new List<string>();
        /// <summary>
        /// a list of names for each SFX file
        /// </summary>
        public List<string> SfxNames = new List<string>();
        /// <summary>
        /// a list of names for each script
        /// </summary>
        public List<string> ObjectsNames = new List<string>();
        /// <summary>
        /// A list of the script filepaths for the stage-specific objects
        /// </summary>
        public List<string> ScriptPaths = new List<string>();
        /// <summary>
        /// whether or not to load the global objects in this stage
        /// </summary>
        public bool LoadGlobalScripts = false;

        public Stageconfig()
        {

        }

        public Stageconfig(string filename) : this(new Reader(filename))
        {

        }

        public Stageconfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Stageconfig(Reader reader)
        {
            LoadGlobalScripts = reader.ReadBoolean();

            StagePalette.Read(reader, 2);

            this.ReadWAVConfiguration(reader);

            this.ReadObjectsNames(reader);

            reader.Close();
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();

            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString()); }

            for (int i = 0; i < objects_count; ++i)
            { ScriptPaths.Add(reader.ReadRSDKString());}
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);

            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);

            foreach (string srcname in ScriptPaths)
                writer.WriteRSDKString(srcname);
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte SoundFX_count = reader.ReadByte();

            for (int i = 0; i < SoundFX_count; ++i)
            { SfxNames.Add(reader.ReadRSDKString()); }

            for (int i = 0; i < SoundFX_count; ++i)
            { SoundFX.Add(reader.ReadString()); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)SoundFX.Count);

            foreach (string wavname in SfxNames)
                writer.WriteRSDKString(wavname);

            foreach (string wav in SoundFX)
                writer.Write(wav);
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        public void Write(Writer writer)
        {
            writer.Write(LoadGlobalScripts);

            StagePalette.Write(writer);

            WriteWAVConfiguration(writer);

            WriteObjectsNames(writer);

            writer.Close();

        }

    }
}
