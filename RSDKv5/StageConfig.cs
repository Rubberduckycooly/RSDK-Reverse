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
        /// a list of all the object names
        /// </summary>
        public List<string> objects = new List<string>();

        /// <summary>
        /// the palettes in the file
        /// </summary>
        public Palette[] palettes = new Palette[8];

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
            Read(reader);
        }

        public void Read(Reader reader)
        {
            // General
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid StageConfig v5 signature");
            }

            loadGlobalObjects = reader.ReadBoolean();

            // Objects
            byte objectCount = reader.ReadByte();
            objects.Clear();
            for (int i = 0; i < objectCount; ++i)
                objects.Add(reader.ReadStringRSDK());

            // Palettes 
            for (int i = 0; i < 8; ++i)
                palettes[i] = new Palette(reader);

            // SoundFX
            byte sfxCount = reader.ReadByte();
            soundFX.Clear();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(new GameConfig.SoundInfo(reader));
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // General
            writer.Write(signature);

            writer.Write(loadGlobalObjects);

            // Objects
            writer.Write((byte)objects.Count);
            foreach (string name in objects)
                writer.WriteStringRSDK(name);

            // Palettes
            foreach (Palette palette in palettes)
                palette.Write(writer);

            // SoundFX
            writer.Write((byte)soundFX.Count);
            foreach (GameConfig.SoundInfo sfx in soundFX)
                sfx.Write(writer);

            writer.Close();
        }
    }
}
