using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace RSDKv5
{
    public class Gameconfig : CommonConfig
    {
        /// <summary>
        /// the name of the game (also window name)
        /// </summary>
        public String GameName = "RSDKv5 Game";
        /// <summary>
        /// the game's subname (used for dev menu)
        /// </summary>
        public String GameSubname = "Powered By the Retro Engine!";
        /// <summary>
        /// what version the game is on
        /// </summary>
        public String Version = "1.05.0";
        public String FilePath;

        /// <summary>
        /// is this file a plus file?
        /// </summary>
        public bool _scenesHaveModeFilter = true;

        /// <summary>
        /// the starting category to load from
        /// </summary>
        public byte StartSceneCategoryIndex = 0;
        /// <summary>
        /// the starting scene to load from
        /// </summary>
        public ushort StartSceneIndex = 0;
        public static int CurrentLevelID = 0;


        public void ResetLevelID()
        {
            CurrentLevelID = 0;
        }


        public class SceneInfo
        {
            /// <summary>
            /// the name of this scene (used for dev menu)
            /// </summary>
            public string Name = "Scene";
            /// <summary>
            /// the folder this scene is located in
            /// </summary>
            public string Zone = "";
            /// <summary>
            /// the SceneID (e.g. Scene1 or SceneA or Scene3)
            /// </summary>
            public string SceneID = "";
            /// <summary>
            /// what "mode" of the stage is (normal, encore, etc)
            /// </summary>
            public byte ModeFilter = 0;
            /// <summary>
            /// For GameConfig Position; Used for Auto Booting
            /// </summary>
            public int LevelID;
            /// <summary>
            /// For GameConfig Position; Used for Auto Booting
            /// </summary>
            public int Index;

            public SceneInfo()
            {
            }

            public SceneInfo(Reader reader, bool scenesHaveModeFilter, int index, bool levelIDMode = false)
            {
                Name = reader.ReadRSDKString();
                Zone = reader.ReadRSDKString();
                SceneID = reader.ReadRSDKString();
                if (levelIDMode)
                {
                    LevelID = CurrentLevelID; //For GameConfig Position; Used for Auto Booting
                }
                Index = index; //For Getting the Index of Categories

                if (scenesHaveModeFilter) ModeFilter = reader.ReadByte();
            }

            public string GetFilePath(string DataFolder)
            {
                return Path.Combine(DataFolder, "Stages", Zone, "Scene" + SceneID + ".bin");
            }

            public void Write(Writer writer, bool scenesHaveModeFilter = false)
            {
                writer.WriteRSDKString(Name);
                writer.WriteRSDKString(Zone);
                writer.WriteRSDKString(SceneID);

                if (scenesHaveModeFilter) writer.Write(ModeFilter);
            }
        }

        public class Category
        {
            /// <summary>
            /// the category name (used for dev menu)
            /// </summary>
            public string Name = "New Category";
            /// <summary>
            /// a list of the scenes in the category
            /// </summary>
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            public Category(bool scenemodeFilter = true)
            {
                Name = "New Category";
            }

            public Category(Reader reader, bool scenesHaveModeFilter)
            {
                Name = reader.ReadRSDKString();

                byte scenes_count = reader.ReadByte();

                int index = 0;
                for (int i = 0; i < scenes_count; ++i)
                {
                    Scenes.Add(new SceneInfo(reader, scenesHaveModeFilter, index, true));
                    CurrentLevelID++;
                    index++;
                }

            }

            public void Write(Writer writer, bool scenesHaveModeFilter = false)
            {
                writer.WriteRSDKString(Name);

                writer.Write((byte)Scenes.Count);
                foreach (SceneInfo scene in Scenes)
                    scene.Write(writer, scenesHaveModeFilter);
            }
        }

        public class ConfigurableMemoryEntry
        {
            /// <summary>
            /// the index of the memory entry
            /// </summary>
            public uint Index;
            /// <summary>
            /// the data in the memory entry
            /// </summary>
            public List<uint> Data = new List<uint>();

            public ConfigurableMemoryEntry()
            {

            }

            internal ConfigurableMemoryEntry(Reader reader)
            {
                Index = reader.ReadUInt32();
                uint Count = reader.ReadUInt32();
                Data = new List<uint>();
                for (int i = 0; i < Count; ++i)
                {
                    Data.Add(reader.ReadUInt32());
                }
            }

            internal void Write(Writer writer)
            {
                writer.Write(Index);
                writer.Write((uint)Data.Count);
                foreach (uint val in Data)
                    writer.Write(val);
            }
        }

        /// <summary>
        /// a list of all the categories
        /// </summary>
        public List<Category> Categories = new List<Category>();

        /// <summary>
        /// a list of all the config memory data
        /// </summary>
        public List<ConfigurableMemoryEntry> ConfigMemory = new List<ConfigurableMemoryEntry>();

        public Gameconfig()
        {
        }

        public Gameconfig(string filename)
        {
            FilePath = filename;
            using (var reader = new Reader(filename))
                Read(reader);
        }

        public Gameconfig(Stream stream)
        {
            using (var reader = new Reader(stream))
                Read(reader);
        }

        public Gameconfig(Reader reader, bool closeReader = false)
        {
            ReadConfig(reader, closeReader);
        }

        public void Read(Reader reader, bool closeReader = false)
        {
            ReadConfig(reader, closeReader);
        }


        public void ReadConfig(Reader reader, bool closeReader = false)
        {
            ReadMagic(reader);

            GameName = reader.ReadRSDKString();
            GameSubname = reader.ReadRSDKString();
            Version = reader.ReadRSDKString();

            InterpretVersion();

            StartSceneCategoryIndex = reader.ReadByte();
            StartSceneIndex = reader.ReadUInt16();

            ReadCommonConfig(reader);

            ushort TotalScenes = reader.ReadUInt16();
            byte categories_count = reader.ReadByte();

            CurrentLevelID = 0;
            for (int i = 0; i < categories_count; ++i)
            {
                Categories.Add(new Category(reader, _scenesHaveModeFilter));
            }
            CurrentLevelID = 0;

            try
            {
                byte config_memory_count = reader.ReadByte();

                for (int i = 0; i < config_memory_count; ++i)
                    ConfigMemory.Add(new ConfigurableMemoryEntry(reader));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading config memory! you potentially have a bad gameconfig!");
                Console.WriteLine("Error: " + ex.Message);
            }
            if (closeReader) reader.Close();
        }

        private void InterpretVersion()
        {
            if (Version.Contains("1.03."))
            {
                _scenesHaveModeFilter = false;
            }
        }

        public void Write(string filename)
        {
            if (File.Exists(filename))
            {
                using (Writer writer = new Writer(filename))
                    Write(writer);
            }
            else
            {
                System.Diagnostics.Debug.Print(string.Format("Unable to Save to the following File: {0}", filename));
            }

        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            WriteMagic(writer);

            writer.WriteRSDKString(GameName);
            writer.WriteRSDKString(GameSubname);
            writer.WriteRSDKString(Version);

            writer.Write(StartSceneCategoryIndex);
            writer.Write(StartSceneIndex);

            WriteCommonConfig(writer);

            writer.Write((ushort)Categories.Select(x => x.Scenes.Count).Sum());

            writer.Write((byte)Categories.Count);
            foreach (Category cat in Categories)
                cat.Write(writer, _scenesHaveModeFilter);

            writer.Write((byte)ConfigMemory.Count);
            foreach (ConfigurableMemoryEntry c in ConfigMemory)
                c.Write(writer);

        }
    }
}
