using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class GameConfig
    {

        public class Category
        {
            public byte SceneCount;
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            public class SceneInfo
            {
                public byte StageCount;
                public string SceneFolder;
                public string ActID;
                public string Name;

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

        public class GlobalVariable
        {

            public string Name;
            public byte Flag1;
            public byte Flag2;
            public byte Flag3;
            public byte Flag4;

            internal GlobalVariable(Reader reader)
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

        public string GameWindowText;
        public string DataFileName;
        public string GameDescriptionText;

        public List<string> ObjectsNames = new List<string>();
        public List<string> ScriptFilepaths = new List<string>();
        public List<string> SoundFX = new List<string>();
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
            DataFileName = reader.ReadRSDKString();
            GameDescriptionText = reader.ReadRSDKString();

            Console.WriteLine("Game Name: " + GameWindowText);
            Console.WriteLine("DataFile Name: " + DataFileName);
            Console.WriteLine("Game Description: " + GameDescriptionText);

            this.ReadObjectData(reader);

            byte Globals_Amount = reader.ReadByte();

            for (int i = 0; i < Globals_Amount; i++)
            {
                GlobalVariables.Add(new GlobalVariable(reader));
            }

            this.ReadSFXData(reader);

            byte playerCount = reader.ReadByte();
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(reader.ReadRSDKString());
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

            this.WriteObjectData(writer);

            writer.Write((byte)GlobalVariables.Count);
            for (int i = 0; i < GlobalVariables.Count; i++)
            {
                GlobalVariables[i].Write(writer);
            }

            this.WriteSFXData(writer);

            writer.Write(Players.Count);
            for (int i = 0; i < Players.Count; i++)
            {
                writer.Write(Players[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                Categories[i].Write(writer);
            }

            writer.Close();
        }

        internal void ReadObjectData(Reader reader)
        {
            byte objects_count = reader.ReadByte();
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString());}
            for (int i = 0; i < objects_count; ++i)
            { ScriptFilepaths.Add(reader.ReadRSDKString()); Console.WriteLine(ScriptFilepaths[i]); }
        }

        internal void WriteObjectData(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
            foreach (string name in ScriptFilepaths)
                writer.WriteRSDKString(name);
        }

        internal void ReadSFXData(Reader reader)
        {
            byte SoundFX_count = reader.ReadByte();
            for (int i = 0; i < SoundFX_count; ++i)
            { SoundFX.Add(reader.ReadString()); }
        }

        internal void WriteSFXData(Writer writer)
        {
            writer.Write((byte)SoundFX.Count);
            foreach (string wav in SoundFX)
                writer.Write(wav);
        }

        /*The Value For DevMenu is at: Line CA0, Column 0B*/
        public void SetDevMenu()
        {
            for (int i = 0; i < GlobalVariables.Count; i++)
            {
                if (GlobalVariables[i].Name == "Options.DevMenuFlag")
                {
                    if (GlobalVariables[i].Flag4 == 1)
                    {
                        GlobalVariables[i].Flag4 = 0;
                        Console.WriteLine("DevMenu Deactivated!");
                        return;
                    }
                    else if (GlobalVariables[i].Flag4 == 0)
                    {
                        GlobalVariables[i].Flag4 = 1;
                        Console.WriteLine("DevMenu Activated!");
                        return;
                    }
                }
            }
        }

    }
}
