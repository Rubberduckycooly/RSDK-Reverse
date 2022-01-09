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
            read(reader);
        }

        public void read(Reader reader)
        {
            // General
            loadGlobalObjects = reader.ReadBoolean();

            // Palettes
            stagePalette.read(reader, 2);

            // Objects
            objects.Clear();
            byte objectCount = reader.ReadByte();
            for (int i = 0; i < objectCount; ++i)
                objects.Add(reader.readRSDKString());

            // SoundFX
            soundFX.Clear();
            byte sfxCount = reader.ReadByte();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(reader.readRSDKString());

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // General
            writer.Write(loadGlobalObjects);

            // Palettes
            stagePalette.write(writer);

            // Objects
            writer.Write((byte)objects.Count);

            foreach (string script in objects)
                writer.writeRSDKString(script);

            // SoundFX
            writer.Write((byte)soundFX.Count);

            foreach (string path in soundFX)
                writer.writeRSDKString(path);

            writer.Close();

        }

    }
}
