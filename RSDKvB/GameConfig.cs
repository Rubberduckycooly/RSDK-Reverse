using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
{
    public class GameConfig
    {

        public class Category
        {
            public class SceneInfo
            {
                public byte StageCount;
                public string SceneFolder = "Folder";
                public string ActID = "1";
                public string Name = "Scene";

                public SceneInfo()
                {
                }

                internal SceneInfo(Reader reader)
                {
                    SceneFolder = reader.ReadRSDKString();
                    ActID = reader.ReadRSDKString();
                    Name = reader.ReadRSDKString();
                    StageCount = reader.ReadByte();
                    Console.WriteLine("Name = " + Name + " ,Act ID = " + ActID + " ,Scene Folder = " + SceneFolder);
                }

                internal void Write(Writer writer)
                {
                    writer.WriteRSDKString(SceneFolder);
                    writer.WriteRSDKString(ActID);
                    writer.WriteRSDKString(Name);
                    writer.Write(StageCount);
                }
            }

            public byte SceneCount;
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            public Category()
            {

            }

            public Category(string filename) : this(new Reader(filename))
            {

            }

            public Category(System.IO.Stream stream) : this(new Reader(stream))
            {

            }

            internal Category(Reader reader)
            {
                SceneCount = reader.ReadByte();

                for (int i = 0; i < SceneCount; i++)
                {
                    Scenes.Add(new SceneInfo(reader));
                }
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

            internal void Write(Writer writer)
            {
                writer.Write(SceneCount);
                for (int i = 0; i < SceneCount; i++)
                {
                    Scenes[i].Write(writer);
                }
            }

        }

        public class GlobalVariable
        {
            public string Name;
            public byte UnknownValue1;
            public byte UnknownValue2;
            public byte UnknownValue3;
            public byte UnknownValue4;

            internal GlobalVariable(Reader reader)
            {
                Name = reader.ReadString();
                Console.WriteLine(Name);
                UnknownValue1 = reader.ReadByte();
                UnknownValue2 = reader.ReadByte();
                UnknownValue3 = reader.ReadByte();
                UnknownValue4 = reader.ReadByte();
            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);
                writer.Write(UnknownValue1);
                writer.Write(UnknownValue2);
                writer.Write(UnknownValue3);
                writer.Write(UnknownValue4);
            }
        }

        public string GameWindowText;
        public string GameDescriptionText;

        public Palette MasterPalette = new Palette();
        public List<string> ObjectsNames = new List<string>();
        public List<string> ScriptFilepaths = new List<string>();
        public List<string> SoundFX = new List<string>();
        public List<string> SfxNames = new List<string>();
        public List<GlobalVariable> GlobalVariables = new List<GlobalVariable>();
        public List<string> Players = new List<string>();
        public List<Category> Categories = new List<Category>();

        public GameConfig()
        {

        }

        public GameConfig(string filename) : this(new Reader(filename))
        {

        }

        public GameConfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private GameConfig(Reader reader)
        {
            GameWindowText = reader.ReadRSDKString();
            GameDescriptionText = reader.ReadRSDKString();

            Console.WriteLine("Game Title: " + GameWindowText);
            Console.WriteLine("Game Description: " + GameDescriptionText);

            this.ReadPalettes(reader);
            this.ReadObjectsNames(reader);

            byte GlobalVariables_Amount = reader.ReadByte();

            for (int i = 0; i < GlobalVariables_Amount; i++)
            {
                GlobalVariables.Add(new GlobalVariable(reader));
            }

            this.ReadWAVConfiguration(reader);

            byte playerCount = reader.ReadByte();
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(reader.ReadRSDKString());
            }

            Categories.Add(new Category(reader)); //Presentation Stages
            Categories.Add(new Category(reader)); //Regular Stages
            Categories.Add(new Category(reader)); //Special Stages
            Categories.Add(new Category(reader)); //Bonus Stages

            reader.Close();
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

        internal void Write(Writer writer)
        {
            writer.WriteRSDKString(GameWindowText);
            writer.WriteRSDKString(GameDescriptionText);

            this.WritePalettes(writer);
            this.WriteObjectsNames(writer);

            writer.Write((byte)GlobalVariables.Count);

            for (int i = 0; i < GlobalVariables.Count; i++)
            {
                GlobalVariables[i].Write(writer);
            }

            this.WriteWAVConfiguration(writer);

            writer.Write(Players.Count);
            for (int i = 0; i < Players.Count; i++)
            {
                writer.Write(Players[i]);
            }

            for (int i = 0; i < 4; i++)
            {
                Categories[i].Write(writer);
            }

            writer.Close();
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();

            for (int i = 0; i < objects_count; ++i)
            {
                ObjectsNames.Add(reader.ReadRSDKString());
            }

            for (int i = 0; i < objects_count; ++i)
            {
                ScriptFilepaths.Add(reader.ReadRSDKString());
                Console.WriteLine(ScriptFilepaths[i]);
            }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);

            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);

            foreach (string name in ScriptFilepaths)
                writer.WriteRSDKString(name);
        }

        internal void ReadPalettes(Reader reader)
        {
            MasterPalette = new Palette(reader, 6);
        }

        internal void WritePalettes(Writer writer)
        {
            MasterPalette.Write(writer);
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte SoundFX_count = reader.ReadByte();

            for (int i = 0; i < SoundFX_count; ++i)
            SfxNames.Add(reader.ReadRSDKString());

            for (int i = 0; i < SoundFX_count; ++i)
            SoundFX.Add(reader.ReadString());
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)SoundFX.Count);

            foreach (string wav in SfxNames)
                writer.WriteRSDKString(wav);

            foreach (string wav in SoundFX)
                writer.Write(wav);
        }

    }
}
