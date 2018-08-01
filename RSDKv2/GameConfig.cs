using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class GameConfig
    {
        public String GameName;
        public String DataString;
        public String Disclaimer;


        //public List<string> ObjectsNames = new List<string>();
        List<string> SourceTxtLocations = new List<string>();
        List<string> AniLocations = new List<string>();
        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();
        public List<Global> Globals = new List<Global>();
        public List<string> WAVnames = new List<string>();
        public List<PlayerData> Players = new List<PlayerData>();
        public StageList Scenes;

        public class SceneInfo
        {
            public byte StageCount;
            public string SceneFolder;
            public string Zone;
            public string Name;

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

        public class StageList
        {
            public List<SceneGroup> Scenes = new List<SceneGroup>();

            public StageList()
            {

            }

            public StageList(string filename) : this(new Reader(filename))
            {

            }

            public StageList(System.IO.Stream stream) : this(new Reader(stream))
            {

            }

            internal StageList(Reader reader)
            {

                Scenes.Add(new SceneGroup(reader)); //Menus
                Scenes.Add(new SceneGroup(reader)); //Stages              
                if (!reader.IsEof)
                {
                    Scenes.Add(new SceneGroup(reader)); //Special Stages
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
                for (int i = 0; i < 3; i++)
                {
                    Scenes[i].Write(writer);
                }
            }
        }

        public class Global
        {

            string Name;
            byte UnknownValue1;
            byte UnknownValue2;
            byte UnknownValue3;
            byte UnknownValue4;

            internal Global(Reader reader)
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
            GameName = reader.ReadRSDKString();
            DataString = reader.ReadRSDKString();
            Disclaimer = reader.ReadRSDKString();

            Console.WriteLine(GameName);
            Console.WriteLine(DataString);
            Console.WriteLine(Disclaimer);

            this.ReadObjectsNames(reader);

            byte Globals_Amount = reader.ReadByte();

            for (int i = 0; i < Globals_Amount; i++)
            {
                Globals.Add(new Global(reader));
            }

            this.ReadWAVConfiguration(reader);

            byte PLR_data_count = reader.ReadByte();

            for (int i = 0; i < PLR_data_count; ++i)
            {
                Players.Add(new PlayerData(reader));
            }

            Scenes = new StageList(reader);

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
            writer.WriteRSDKString(GameName);
            writer.WriteRSDKString(DataString);
            writer.WriteRSDKString(Disclaimer);

            this.WriteObjectsNames(writer);

            writer.Write((byte)Globals.Count);

            for (int i = 0; i < Globals.Count; i++)
            {
                Globals[i].Write(writer);
            }

            this.WriteWAVConfiguration(writer);

            writer.Write((byte)Players.Count);

            for (int i = 0; i < Players.Count; ++i)
            {
                Players[i].Write(writer);
            }

            Scenes.Write(writer);
            writer.Write((byte)0);

            writer.Close();

        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte srctxt_count = reader.ReadByte();
            for (int i = 0; i < srctxt_count; ++i)
            { SourceTxtLocations.Add(reader.ReadRSDKString()); Console.WriteLine(SourceTxtLocations[i]); }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)SourceTxtLocations.Count);
            foreach (string name in SourceTxtLocations)
                writer.WriteRSDKString(name);
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte wavs_count = reader.ReadByte();

            for (int i = 0; i < wavs_count; ++i)
            { WAVs.Add(new WAVConfiguration(reader)); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)WAVs.Count);
            foreach (WAVConfiguration wav in WAVs)
                wav.Write(writer);
        }

    }
}
