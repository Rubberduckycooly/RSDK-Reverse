using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class GameConfig : CommonConfig
    {
        public String GameName;
        public String GameSubname;
        public String Version;

        bool _scenesHaveModeFilter;

        public byte StartSceneCategoryIndex;
        public ushort StartSceneIndex;

        public class SceneInfo
        {
            public string Name;
            public string Zone;
            public string SceneID;
            public byte ModeFilter;

            public SceneInfo()
            {
            }

            internal SceneInfo(Reader reader, bool scenesHaveModeFilter)
            {
                Name = reader.ReadRSDKString();
                Zone = reader.ReadRSDKString();
                SceneID = reader.ReadRSDKString();

                if (scenesHaveModeFilter) ModeFilter = reader.ReadByte();
            }

            internal void Write(Writer writer, bool scenesHaveModeFilter = false)
            {
                writer.WriteRSDKString(Name);
                writer.WriteRSDKString(Zone);
                writer.WriteRSDKString(SceneID);

                if (scenesHaveModeFilter) writer.Write(ModeFilter);
            }
        }

        public class Category
        {
            public string Name;
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            internal Category(Reader reader, bool scenesHaveModeFilter)
            {
                Name = reader.ReadRSDKString();

                byte scenes_count = reader.ReadByte();
                for (int i = 0; i < scenes_count; ++i)
                    Scenes.Add(new SceneInfo(reader, scenesHaveModeFilter));
            }

            internal void Write(Writer writer, bool scenesHaveModeFilter = false)
            {
                writer.WriteRSDKString(Name);

                writer.Write((byte)Scenes.Count);
                foreach (SceneInfo scene in Scenes)
                    scene.Write(writer, scenesHaveModeFilter);
            }
        }

        public class ConfigurableMemoryEntry
        {
            public uint Index;
            public int[] Data;

            internal ConfigurableMemoryEntry(Reader reader)
            {
                Index = reader.ReadUInt32();
                uint Count = reader.ReadUInt32();
                Data = new int[Count];
                for (int i = 0; i < Count; ++i)
                    Data[i] = reader.ReadInt32();
            }

            internal void Write(Writer writer)
            {
                writer.Write(Index);
                writer.Write((uint)Data.Length);
                foreach (uint val in Data)
                    writer.Write(val);
            }
        }

        public List<Category> Categories = new List<Category>();

        public List<ConfigurableMemoryEntry> ConfigMemory = new List<ConfigurableMemoryEntry>();

        public GameConfig(string filename) : this(new Reader(filename), true)
        {

        }

        public GameConfig(Stream stream) : this(new Reader(stream), false)
        {

        }

        private GameConfig(Reader reader, bool closeStream = false)
        {
            base.ReadMagic(reader);

            GameName = reader.ReadRSDKString();
            GameSubname = reader.ReadRSDKString();
            Version = reader.ReadRSDKString();

            InterpretVersion();

            StartSceneCategoryIndex = reader.ReadByte();
            StartSceneIndex = reader.ReadUInt16();

            base.ReadCommonConfig(reader);

            ushort TotalScenes = reader.ReadUInt16();

            byte categories_count = reader.ReadByte();
            for (int i = 0; i < categories_count; ++i)
                Categories.Add(new Category(reader, _scenesHaveModeFilter));

            byte config_memory_count = reader.ReadByte();
            for (int i = 0; i < config_memory_count; ++i)
                ConfigMemory.Add(new ConfigurableMemoryEntry(reader));

            if (closeStream)
                reader.Close();
        }

        private void InterpretVersion()
        {
            string[] versionParts = Version.Split('.');
            int midVersion = Int32.Parse(versionParts[1]);
            if (midVersion >= 5)
            {
                _scenesHaveModeFilter = true;
            }
        }

        public void Write(string filename, bool closeStream)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer,closeStream);
        }

        public void Write(Stream stream, bool closeStream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer, closeStream);
        }

        internal void Write(Writer writer, bool CloseStream)
        {
            base.WriteMagic(writer);

            writer.WriteRSDKString(GameName);
            writer.WriteRSDKString(GameSubname);
            writer.WriteRSDKString(Version);

            writer.Write(StartSceneCategoryIndex);
            writer.Write(StartSceneIndex);

            base.WriteCommonConfig(writer);

            writer.Write((ushort)Categories.Select(x => x.Scenes.Count).Sum());

            writer.Write((byte)Categories.Count);
            foreach (Category cat in Categories)
                cat.Write(writer, _scenesHaveModeFilter);

            writer.Write((byte)ConfigMemory.Count);
            foreach (ConfigurableMemoryEntry c in ConfigMemory)
                c.Write(writer);

            if (CloseStream)
            writer.Close();
        }
    }
}
