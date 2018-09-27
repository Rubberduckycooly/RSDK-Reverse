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
        public String GameSubname;

        public List<string> ObjectsNames = new List<string>();
        public List<string> SourceTxtLocations = new List<string>();
        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();
        public List<Global> Globals = new List<Global>();
        public StageList Stages;

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
                Console.WriteLine(SceneCount);
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
            public byte playerCount;
            public List<string> Players = new List<string>();
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
                playerCount = reader.ReadByte();
                for (int i = 0; i < playerCount; i++)
                {
                    Players.Add(reader.ReadRSDKString());
                }

                Scenes.Add(new SceneGroup(reader)); //Menus
                Scenes.Add(new SceneGroup(reader)); //Stages
                Scenes.Add(new SceneGroup(reader)); //Special Stages
                Scenes.Add(new SceneGroup(reader)); //Bonus Stages
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
                writer.Write(playerCount);
                for (int i = 0; i < playerCount; i++)
                {
                    writer.Write(Players[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    Scenes[i].Write(writer);
                }
            }
        }

        public class Global
        {

            public string Name;
            public byte Flag1;
            public byte Flag2;
            public byte Flag3;
            public byte Flag4;

            internal Global(Reader reader)
            {
                Name = reader.ReadString();
                Console.WriteLine(Name);
                Flag1 = reader.ReadByte();
                Flag2 = reader.ReadByte();
                Flag3 = reader.ReadByte();
                Flag4 = reader.ReadByte();
            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);
                writer.Write(Flag1);
                writer.Write(Flag2);
                writer.Write(Flag3);
                writer.Write(Flag4);
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
            GameSubname = reader.ReadRSDKString();

            Console.WriteLine("Name = " + GameName);
            Console.WriteLine("Data = " + DataString);
            Console.WriteLine("SubName = " + GameSubname);

            this.ReadObjectsNames(reader);

            byte Globals_Amount = reader.ReadByte();

            for (int i = 0; i < Globals_Amount; i++)
            {
                Globals.Add(new Global(reader));
            }

            this.ReadWAVConfiguration(reader);

            Stages = new StageList(reader);

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
            writer.WriteRSDKString(GameSubname);

            this.WriteObjectsNames(writer);

            writer.Write((byte)Globals.Count);
            for (int i = 0; i < Globals.Count; i++)
            {
                Globals[i].Write(writer);
            }

            this.WriteWAVConfiguration(writer);

            Stages.Write(writer);
            writer.Write((byte)0);

            writer.Close();
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString());}
            for (int i = 0; i < objects_count; ++i)
            { SourceTxtLocations.Add(reader.ReadRSDKString()); Console.WriteLine(SourceTxtLocations[i]); }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
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

        /*The Value For DevMenu is at: Line CA0, Column 0B*/
        public void SetDevMenu()
        {
            for (int i = 0; i < Globals.Count; i++)
            {
                if (Globals[i].Name == "Options.DevMenuFlag")
                {
                    if (Globals[i].Flag4 == 1)
                    {
                        Globals[i].Flag4 = 0;
                        Console.WriteLine("DevMenu Deactivated!");
                        return;
                    }
                    else if (Globals[i].Flag4 == 0)
                    {
                        Globals[i].Flag4 = 1;
                        Console.WriteLine("DevMenu Activated!");
                        return;
                    }
                }
            }
        }

    }
}
