using System.Collections.Generic;

namespace RSDKv2
{
    public class StageConfig
    {
        /// <summary>
        /// the stageconfig palette (index 96-128)
        /// </summary>
        public Palette stagePalette = new Palette();

        /// <summary>
        /// the list of stage-specific objects scripts
        /// </summary>
        public List<string> objects = new List<string>();

        /// <summary>
        /// the list of stage-specific SoundFX paths
        /// </summary>
        public List<string> soundFX = new List<string>();

        /// <summary>
        /// whether or not to load the global objects in this stage
        /// </summary>
        public bool loadGlobalObjects = false;

        public StageConfig() { }

        public StageConfig(string filename) : this(new Reader(filename)) { }

        public StageConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

        public StageConfig(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            // General
            loadGlobalObjects = reader.ReadBoolean();

            // Palettes
            stagePalette.Read(reader, 2);

            // Objects
            objects.Clear();
            byte objectCount = reader.ReadByte();
            for (int i = 0; i < objectCount; ++i)
                objects.Add(reader.ReadStringRSDK());

            // SoundFX
            soundFX.Clear();
            byte sfxCount = reader.ReadByte();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(reader.ReadStringRSDK());

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
            // General
            writer.Write(loadGlobalObjects);

            // Palettes
            stagePalette.Write(writer);

            // Objects
            writer.Write((byte)objects.Count);

            foreach (string script in objects)
                writer.WriteStringRSDK(script);

            // SoundFX
            writer.Write((byte)soundFX.Count);

            foreach (string path in soundFX)
                writer.WriteStringRSDK(path);

            writer.Close();
        }
    }
}
