using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace RSDKv5
{
    public class GameConfig
    {
        public class SceneCategory
        {
            public class SceneInfo
            {
                /// <summary>
                /// the name of this scene (used in the dev menu)
                /// </summary>
                public string name = "SCENE";
                /// <summary>
                /// the folder this scene is located in
                /// </summary>
                public string folder = "Folder";
                /// <summary>
                /// the scene's ID (e.g. Scene'1' or Scene'A' or Scene'3')
                /// </summary>
                public string id = "1";
                /// <summary>
                /// the scene's object filter, a collection of bits. For example: mania uses bit 0 for common objects, bit 1 for mania mode objects & bit 2 for encore objects.
                /// </summary>
                public byte filter = 0;

                public SceneInfo() { }

                public SceneInfo(Reader reader, bool readFilter = true)
                {
                    Read(reader, readFilter);
                }

                public void Read(Reader reader, bool readFilter = true)
                {
                    name = reader.ReadStringRSDK();
                    folder = reader.ReadStringRSDK();
                    id = reader.ReadStringRSDK();

                    if (readFilter)
                        filter = reader.ReadByte();
                }

                public void Write(Writer writer, bool writeFilter = true)
                {
                    writer.WriteStringRSDK(name);
                    writer.WriteStringRSDK(folder);
                    writer.WriteStringRSDK(id);

                    if (writeFilter)
                        writer.Write(filter);
                }
            }

            /// <summary>
            /// the category name (used for dev menu)
            /// </summary>
            public string name = "New Category";
            /// <summary>
            /// a list of the scenes in the category
            /// </summary>
            public List<SceneInfo> list = new List<SceneInfo>();

            public SceneCategory() { }

            public SceneCategory(Reader reader, bool readFilter = true)
            {
                Read(reader, readFilter);
            }

            public void Read(Reader reader, bool readFilter = true)
            {
                name = reader.ReadStringRSDK();

                list.Clear();
                byte sceneCount = reader.ReadByte();
                for (int i = 0; i < sceneCount; ++i)
                    list.Add(new SceneInfo(reader, readFilter));
            }

            public void Write(Writer writer, bool writeFilter = true)
            {
                writer.WriteStringRSDK(name);

                writer.Write((byte)list.Count);
                foreach (SceneInfo scene in list)
                    scene.Write(writer, writeFilter);
            }
        }

        public class GlobalVariable
        {
            /// <summary>
            /// the offset of the global variable, relative to the start of the struct (in sets of ints, so variable 2 would be offset 1, assuming variable 1 is a single value)
            /// </summary>
            public int offset = 0;
            /// <summary>
            /// a list of the default values for the global variable
            /// </summary>
            public List<int> values = new List<int>();

            public GlobalVariable() { }

            public GlobalVariable(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                offset = reader.ReadInt32();

                values.Clear();
                int valueCount = reader.ReadInt32();
                for (int i = 0; i < valueCount; ++i)
                    values.Add(reader.ReadInt32());
            }

            public void Write(Writer writer)
            {
                writer.Write(offset);

                writer.Write(values.Count);
                foreach (int value in values)
                    writer.Write(value);
            }
        }

        public class SoundInfo
        {
            /// <summary>
            /// the path to the sfx
            /// </summary>
            public string name = "Folder/SoundFX.wav";

            /// <summary>
            /// how many instances of the sfx can play at once
            /// </summary>
            public byte maxConcurrentPlay = 1;

            public SoundInfo() { }

            public SoundInfo(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name = reader.ReadStringRSDK();
                maxConcurrentPlay = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.WriteStringRSDK(name);
                writer.Write(maxConcurrentPlay);
            }
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'C', (byte)'F', (byte)'G', 0 };

        /// <summary>
        /// the name of the game (also window name)
        /// </summary>
        public string gameTitle = "Retro-Engine";
        /// <summary>
        /// the game's subname (used for dev menu)
        /// </summary>
        public string gameSubtitle = "";
        /// <summary>
        /// what version the game is on
        /// </summary>
        public string gameVersion = "1.00";

        /// <summary>
        /// the starting category to load from
        /// </summary>
        public byte startingActiveList = 0;
        /// <summary>
        /// the starting scene to load from
        /// </summary>
        public ushort startingListPos = 0;

        /// <summary>
        /// a list of all the object names
        /// </summary>
        public List<string> objects = new List<string>();
        /// <summary>
        /// the palettes in the file
        /// </summary>
        public Palette[] palettes = new Palette[8];
        /// <summary>
        /// the list of global SoundFX
        /// </summary>
        public List<SoundInfo> soundFX = new List<SoundInfo>();

        /// <summary>
        /// a list of all the categories
        /// </summary>
        public List<SceneCategory> categories = new List<SceneCategory>();

        /// <summary>
        /// the list of global variable values, see RSDKConfig for names, types & other editor-only data
        /// </summary>
        public List<GlobalVariable> globalVariables = new List<GlobalVariable>();

        public GameConfig()
        {
            for (int i = 0; i < palettes.Length; i++)
                palettes[i] = new Palette();
        }

        public GameConfig(string filename)
        {
            using (var reader = new Reader(filename))
                Read(reader);
        }

        public GameConfig(Stream stream)
        {
            using (var reader = new Reader(stream))
                Read(reader);
        }

        public GameConfig(Reader reader, bool usePlusFormat = true)
        {
            Read(reader, usePlusFormat);
        }

        public void Read(Reader reader, bool usePlusFormat = true)
        {
            // Header
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid GameConfig v5 signature");
            }

            // General
            gameTitle = reader.ReadStringRSDK();
            gameSubtitle = reader.ReadStringRSDK();
            gameVersion = reader.ReadStringRSDK();

            startingActiveList = reader.ReadByte();
            startingListPos = reader.ReadUInt16();

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
                soundFX.Add(new SoundInfo(reader));

            // Scenes
            // total scene count, used by RSDKv5 to allocate scenes before they're read, its managed automatically here
            ushort totalSceneCount = reader.ReadUInt16();

            byte categoryCount = reader.ReadByte();

            categories.Clear();
            for (int i = 0; i < categoryCount; ++i)
                categories.Add(new SceneCategory(reader, usePlusFormat));

            // Global Variables
            byte globalVariableCount = reader.ReadByte();
            globalVariables.Clear();
            for (int i = 0; i < globalVariableCount; ++i)
                globalVariables.Add(new GlobalVariable(reader));

            reader.Close();
        }

        public void Write(string filename, bool usePlusFormat = true)
        {
            using (Writer writer = new Writer(filename))
                Write(writer, usePlusFormat);
        }

        public void Write(Stream stream, bool usePlusFormat = true)
        {
            using (Writer writer = new Writer(stream))
                Write(writer, usePlusFormat);
        }

        public void Write(Writer writer, bool usePlusFormat = true)
        {
            // Header
            writer.Write(signature);

            // General
            writer.WriteStringRSDK(gameTitle);
            writer.WriteStringRSDK(gameSubtitle);
            writer.WriteStringRSDK(gameVersion);

            writer.Write(startingActiveList);
            writer.Write(startingListPos);

            // Objects
            writer.Write((byte)objects.Count);
            foreach (string name in objects)
                writer.WriteStringRSDK(name);

            // Palettes
            foreach (Palette palette in palettes)
                palette.Write(writer);

            // SoundFX
            writer.Write((byte)soundFX.Count);
            foreach (SoundInfo sfx in soundFX)
                sfx.Write(writer);

            // Total Scene Count
            writer.Write((ushort)categories.Select(x => x.list.Count).Sum());

            // Scenes
            writer.Write((byte)categories.Count);
            foreach (SceneCategory cat in categories)
                cat.Write(writer, usePlusFormat);

            // Global Variables
            writer.Write((byte)globalVariables.Count);
            foreach (GlobalVariable c in globalVariables)
                c.Write(writer);
        }
    }
}
