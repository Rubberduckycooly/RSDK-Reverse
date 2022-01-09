using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKv5
{
    public class StageConfig
    {
        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'C', (byte)'F', (byte)'G', 0 };

        /// <summary>
        /// how many palettes are in the file
        /// </summary>
        private const int PALETTES_COUNT = 8;

        /// <summary>
        /// a list of all the object names
        /// </summary>
        public List<string> objects = new List<string>();
        /// <summary>
        /// the palettes in the file
        /// </summary>
        public Palette[] palettes = new Palette[PALETTES_COUNT];
        /// <summary>
        /// the list of stage-specific SoundFX
        /// </summary>
        public List<GameConfig.SoundInfo> soundFX = new List<GameConfig.SoundInfo>();
        /// <summary>
        /// whether or not to load the global objects in this stage
        /// </summary>
        public bool loadGlobalObjects = false;

        public StageConfig()
        {
            for (int i = 0; i < palettes.Length; i++)
                palettes[i] = new Palette();
        }

        public StageConfig(string filename) : this(new Reader(filename)) { }

        public StageConfig(Stream stream) : this(new Reader(stream)) { }

        public StageConfig(Reader reader) : this()
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            // General
            if (!reader.readBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid StageConfig v5 signature");
            }

            loadGlobalObjects = reader.ReadBoolean();

            // Objects
            byte objectCount = reader.ReadByte();
            objects.Clear();
            for (int i = 0; i < objectCount; ++i)
                objects.Add(reader.readRSDKString());

            // Palettes 
            for (int i = 0; i < PALETTES_COUNT; ++i)
                palettes[i] = new Palette(reader);

            // SoundFX
            byte sfxCount = reader.ReadByte();
            soundFX.Clear();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(new GameConfig.SoundInfo(reader));
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // General
            writer.Write(signature);

            writer.Write(loadGlobalObjects);

            // Objects
            writer.Write((byte)objects.Count);
            foreach (string name in objects)
                writer.writeRSDKString(name);

            // Palettes
            foreach (Palette palette in palettes)
                palette.write(writer);

            // SoundFX
            writer.Write((byte)soundFX.Count);
            foreach (GameConfig.SoundInfo sfx in soundFX)
                sfx.write(writer);
        }
    }
}
