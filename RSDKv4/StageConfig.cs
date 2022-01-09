using System.Collections.Generic;

namespace RSDKv4
{
    public class StageConfig
    {
        /// <summary>
        /// the stageconfig palette (index 96-128)
        /// </summary>
        public Palette stagePalette = new Palette();
        /// <summary>
        /// the list of stage-specific objects
        /// </summary>
        public List<GameConfig.ObjectInfo> objects = new List<GameConfig.ObjectInfo>();
        /// <summary>
        /// the list of stage-specific SoundFX
        /// </summary>
        public List<GameConfig.SoundInfo> soundFX = new List<GameConfig.SoundInfo>();
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

            // SoundFX
            soundFX.Clear();
            byte sfxCount = reader.ReadByte();
            for (int i = 0; i < sfxCount; ++i)
            {
                GameConfig.SoundInfo info = new GameConfig.SoundInfo();
                info.name = reader.readRSDKString();

                soundFX.Add(info);
            }

            foreach (GameConfig.SoundInfo info in soundFX)
                info.path = reader.readRSDKString();

            // Objects
            objects.Clear();
            byte objectCount = reader.ReadByte();
            for (int i = 0; i < objectCount; ++i)
            {
                GameConfig.ObjectInfo info = new GameConfig.ObjectInfo();
                info.name = reader.readRSDKString();

                objects.Add(info);
            }

            foreach (GameConfig.ObjectInfo info in objects)
                info.script = reader.readRSDKString();

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

            // SoundFX
            writer.Write((byte)soundFX.Count);

            foreach (GameConfig.SoundInfo info in soundFX)
                writer.writeRSDKString(info.name);

            foreach (GameConfig.SoundInfo info in soundFX)
                writer.writeRSDKString(info.path);

            // Objects
            writer.Write((byte)objects.Count);

            foreach (GameConfig.ObjectInfo info in objects)
                writer.writeRSDKString(info.name);

            foreach (GameConfig.ObjectInfo info in objects)
                writer.writeRSDKString(info.script);

            writer.Close();
        }

    }
}
