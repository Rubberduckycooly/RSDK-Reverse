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
        public List<Category> Categories = new List<Category>();

        public class SceneInfo
        {
            public string Name;
            public string Zone;
            public string SceneID;

            internal SceneInfo(Reader reader)
            {
                Name = reader.ReadRSDKString();
                Zone = reader.ReadRSDKString();
                SceneID = reader.ReadRSDKString();
                Console.WriteLine("Name = " + Name + " ,Zone = " + Zone + " ,SceneID = " + SceneID);
            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);
                writer.WriteRSDKString(Zone);
                writer.WriteRSDKString(SceneID);
            }
        }

        public class Category
        {
            public string Name;
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            internal Category(Reader reader)
            {
                byte scenes_count = reader.ReadByte();
                //Name = reader.ReadRSDKString();

                for (int i = 0; i < scenes_count; ++i)
                    Scenes.Add(new SceneInfo(reader));

            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);

                writer.Write((byte)Scenes.Count);
                foreach (SceneInfo scene in Scenes)
                    scene.Write(writer);
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

            //Stage Order Loading Goes Here

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
            this.WriteObjectsNames(writer);
            this.WriteWAVConfiguration(writer);
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
