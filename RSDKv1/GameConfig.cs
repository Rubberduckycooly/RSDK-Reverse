using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
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

                internal SceneInfo()
                {
                    SceneFolder = "";
                    ActID = "";
                    Name = "";
                    StageCount = 0;
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
                Scenes = new List<SceneInfo>();
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

            string Name;
            byte UnknownValue1;
            byte UnknownValue2;
            byte UnknownValue3;
            byte UnknownValue4;

            public GlobalVariable()
            {
                Name = "";
                UnknownValue1 = 0;
                UnknownValue2 = 0;
                UnknownValue3 = 0;
                UnknownValue4 = 0;
            }

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

        public class PlayerData
        {

            public string PlayerAnimLocation;
            public string PlayerScriptLocation;
            public string PlayerName;

            public PlayerData()
            {
                PlayerAnimLocation = "";
                PlayerScriptLocation = "";
                PlayerName = "";
            }

            public PlayerData(Reader reader)
            {
                PlayerAnimLocation = reader.ReadRSDKString();
                PlayerScriptLocation = reader.ReadRSDKString();
                PlayerName = reader.ReadRSDKString();
            }

            public void Write(Writer writer)
            {
                writer.WriteRSDKString(PlayerAnimLocation);
                writer.WriteRSDKString(PlayerScriptLocation);
                writer.WriteRSDKString(PlayerName);
            }

        }

        public string GameWindowText;
        public string DataFileName;
        public string GameDescriptionText;

        public List<string> ScriptFilepaths = new List<string>();
        public List<string> SoundFX = new List<string>();
        public List<GlobalVariable> GlobalVariables = new List<GlobalVariable>();
        public List<PlayerData> playerData = new List<PlayerData>();
        public List<Category> Categories = new List<Category>();

        public GameConfig(string filename) : this(new Reader(filename))
        {
        }

        public GameConfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private GameConfig(Reader reader)
        {
            GameWindowText = reader.ReadRSDKString();
            DataFileName = reader.ReadRSDKString();
            GameDescriptionText = reader.ReadRSDKString();

            Console.WriteLine("Game Title: " + GameWindowText);
            Console.WriteLine("DataFile Name: " + DataFileName);
            Console.WriteLine("Game Description: " + GameDescriptionText);

            this.ReadObjectsNames(reader);

            byte Globals_Amount = reader.ReadByte();

            for (int i = 0; i < Globals_Amount; i++)
            {
                GlobalVariables.Add(new GlobalVariable(reader));
            }

            this.ReadWAVConfiguration(reader);

            byte PLR_data_count = reader.ReadByte();

            for (int i = 0; i < PLR_data_count; ++i)
            {
                playerData.Add(new PlayerData(reader));
            }

            Categories.Add(new Category(reader)); //Menus
            Categories.Add(new Category(reader)); //Stages              
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
            writer.WriteRSDKString(DataFileName);
            writer.WriteRSDKString(GameDescriptionText);

            this.WriteObjectsNames(writer);

            writer.Write((byte)GlobalVariables.Count);

            for (int i = 0; i < GlobalVariables.Count; i++)
            {
                GlobalVariables[i].Write(writer);
            }

            this.WriteWAVConfiguration(writer);

            writer.Write((byte)playerData.Count);

            for (int i = 0; i < playerData.Count; ++i)
            {
                playerData[i].Write(writer);
            }

            for (int i = 0; i < 3; i++)
            {
                Categories[i].Write(writer);
            }

            writer.Close();

        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte srctxt_count = reader.ReadByte();
            for (int i = 0; i < srctxt_count; ++i)
            { ScriptFilepaths.Add(reader.ReadRSDKString()); Console.WriteLine(ScriptFilepaths[i]); }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ScriptFilepaths.Count);
            foreach (string name in ScriptFilepaths)
                writer.WriteRSDKString(name);
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte wavs_count = reader.ReadByte();

            for (int i = 0; i < wavs_count; ++i)
            { SoundFX.Add(reader.ReadString()); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)SoundFX.Count);
            foreach (string wav in SoundFX)
                writer.Write(wav);
        }

    }
}
