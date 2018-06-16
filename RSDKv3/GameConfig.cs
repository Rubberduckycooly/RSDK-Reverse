using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    public class GameConfig
    {

        public String GameName;
        public String DataString;
        public String GameSubname;


        public List<string> ObjectsNames = new List<string>();
        List<string> SourceTxtLocations = new List<string>();
        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();
        public List<Global> Globals = new List<Global>();

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

            List<string> PlayerNames = new List<string>();

            int playerCount = reader.ReadByte();

            for (int i = 0; i < playerCount; i++)
            {
            string character = reader.ReadRSDKString();
            PlayerNames.Add(character);
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
            this.WriteObjectsNames(writer);
            this.WriteWAVConfiguration(writer);
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
