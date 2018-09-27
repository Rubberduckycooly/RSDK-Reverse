using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
{
    public class GameConfig
    {
        public string GameWindowText;
        public string DataString;
        public string GameDescriptionText;


        public List<string> ScriptFilepaths = new List<string>();
        public List<WAVConfiguration> SoundFX = new List<WAVConfiguration>();
        public List<GlobalVariables> globalVariables = new List<GlobalVariables>();
        public List<PlayerData> playerData = new List<PlayerData>();
        public List<SceneGroup> SceneCategories = new List<SceneGroup>();

        public class SceneInfo
        {
            public byte StageCount;
            public string SceneFolder;
            public string Zone;
            public string Name;

            internal SceneInfo()
            {
                SceneFolder = "";
                Zone = "";
                Name = "";
                StageCount = 0;
            }

            internal SceneInfo(Reader reader)
            {
                SceneFolder = reader.ReadRSDKString();
                Zone = reader.ReadRSDKString();
                Name = reader.ReadRSDKString();
                StageCount = reader.ReadByte();
                Console.WriteLine("Name = " + Name + " ,Zone = " + Zone + " ,SceneFolder = " + SceneFolder);
            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(SceneFolder);
                writer.WriteRSDKString(Zone);
                writer.WriteRSDKString(Name);
                writer.Write(StageCount);
            }
        }

        public class SceneGroup
        {
            public byte SceneCount;
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            public SceneGroup()
            {
                Scenes = new List<SceneInfo>();
            }

            public SceneGroup(string filename) : this(new Reader(filename))
            {

            }

            public SceneGroup(System.IO.Stream stream) : this(new Reader(stream))
            {

            }

            internal SceneGroup(Reader reader)
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

        public class GlobalVariables
        {

            string Name;
            byte UnknownValue1;
            byte UnknownValue2;
            byte UnknownValue3;
            byte UnknownValue4;

            public GlobalVariables()
            {
                Name = "";
                UnknownValue1 = 0;
                UnknownValue2 = 0;
                UnknownValue3 = 0;
                UnknownValue4 = 0;
            }

            internal GlobalVariables(Reader reader)
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

        public GameConfig(string filename) : this(new Reader(filename))
        {
        }

        public GameConfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private GameConfig(Reader reader)
        {
            GameWindowText = reader.ReadRSDKString();
            DataString = reader.ReadRSDKString();
            GameDescriptionText = reader.ReadRSDKString();

            Console.WriteLine(GameWindowText);
            Console.WriteLine(DataString);
            Console.WriteLine(GameDescriptionText);

            this.ReadObjectsNames(reader);

            byte Globals_Amount = reader.ReadByte();

            for (int i = 0; i < Globals_Amount; i++)
            {
                globalVariables.Add(new GlobalVariables(reader));
            }

            this.ReadWAVConfiguration(reader);

            byte PLR_data_count = reader.ReadByte();

            for (int i = 0; i < PLR_data_count; ++i)
            {
                playerData.Add(new PlayerData(reader));
            }

            SceneCategories.Add(new SceneGroup(reader)); //Menus
            SceneCategories.Add(new SceneGroup(reader)); //Stages              
            SceneCategories.Add(new SceneGroup(reader)); //Special Stages
            SceneCategories.Add(new SceneGroup(reader)); //Bonus Stages

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
            writer.WriteRSDKString(DataString);
            writer.WriteRSDKString(GameDescriptionText);

            this.WriteObjectsNames(writer);

            writer.Write((byte)globalVariables.Count);

            for (int i = 0; i < globalVariables.Count; i++)
            {
                globalVariables[i].Write(writer);
            }

            this.WriteWAVConfiguration(writer);

            writer.Write((byte)playerData.Count);

            for (int i = 0; i < playerData.Count; ++i)
            {
                playerData[i].Write(writer);
            }

            for (int i = 0; i < 3; i++)
            {
                SceneCategories[i].Write(writer);
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
            { SoundFX.Add(new WAVConfiguration(reader)); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)SoundFX.Count);
            foreach (WAVConfiguration wav in SoundFX)
                wav.Write(writer);
        }

    }
}
