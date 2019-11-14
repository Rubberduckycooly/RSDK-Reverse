using System;
using System.Collections.Generic;

namespace RSDKv2
{
    public class Gameconfig
    {

        public class Category
        {
            /// <summary>
            /// the list of stages in this category
            /// </summary>
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            public class SceneInfo
            {
                /// <summary>
                /// Scene Mode
                /// </summary>
                public byte SceneMode;
                /// <summary>
                /// the folder of the scene
                /// </summary>
                public string SceneFolder = "Folder";
                /// <summary>
                /// the scene's identifier (E.G Act1 or Act2)
                /// </summary>
                public string ActID = "1";
                /// <summary>
                /// the scene name (shows up on the dev menu)
                /// </summary>
                public string Name = "Scene";

                public SceneInfo()
                {
                    SceneFolder = "Folder";
                    ActID = "1";
                    Name = "Stage";
                    SceneMode = 0;
                }

                public SceneInfo(Reader reader)
                {
                    SceneFolder = reader.ReadRSDKString();
                    ActID = reader.ReadRSDKString();
                    Name = reader.ReadRSDKString();
                    SceneMode = reader.ReadByte();
                    //Console.WriteLine("Name = " + Name + " ,Act ID = " + ActID + " ,Scene Folder = " + SceneFolder, " ,SceneMode = " + SceneMode);
                }

                public void Write(Writer writer)
                {
                    writer.WriteRSDKString(SceneFolder);
                    writer.WriteRSDKString(ActID);
                    writer.WriteRSDKString(Name);
                    writer.Write(SceneMode);
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

            public Category(Reader reader)
            {
                byte SceneCount = reader.ReadByte();
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

            public void Write(Writer writer)
            {
                writer.Write((byte)Scenes.Count);
                for (int i = 0; i < Scenes.Count; i++)
                {
                    Scenes[i].Write(writer);
                }
            }

        }

        public class GlobalVariable
        {
            /// <summary>
            /// the name of the variable
            /// </summary>
            public string Name;
            /// <summary>
            /// the variable's value
            /// </summary>
            public int Value = 0;

            public GlobalVariable()
            {

            }

            public GlobalVariable(string name)
            {
                Name = name;
            }

            public GlobalVariable(Reader reader)
            {
                Name = reader.ReadString();
                byte[] bytes = new byte[4];
                bytes = reader.ReadBytes(4);
                Value = (bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + (bytes[3] << 0);
            }

            public void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);
                writer.Write((byte)(Value >> 24));
                writer.Write((byte)(Value >> 16));
                writer.Write((byte)(Value >> 8));
                writer.Write((byte)(Value & 0xff));
            }
        }

        /// <summary>
        /// the game name, appears on the window
        /// </summary>
        public string GameWindowText;
        /// <summary>
        /// i have no idea
        /// </summary>
        public string DataFileName;
        /// <summary>
        /// the string the appears in the about window
        /// </summary>
        public string GameDescriptionText;

        /// <summary>
        /// a unique name for each object in the script list
        /// </summary>
        public List<string> ObjectsNames = new List<string>();
        /// <summary>
        /// the list of filepaths for the global objects
        /// </summary>
        public List<string> ScriptPaths = new List<string>();
        /// <summary>
        /// the list of global SoundFX
        /// </summary>
        public List<string> SoundFX = new List<string>();
        /// <summary>
        /// the list of global variable names and values
        /// </summary>
        public List<GlobalVariable> GlobalVariables = new List<GlobalVariable>();
        /// <summary>
        /// the list of playerdata needed for players
        /// </summary>
        public List<string> Players = new List<string>();
        /// <summary>
        /// the category list (stage list)
        /// </summary>
        public List<Category> Categories = new List<Category>();

        public Gameconfig()
        {
            Categories.Add(new Category()); //Menus
            Categories.Add(new Category()); //Stages
            Categories.Add(new Category()); //Special Stages
            Categories.Add(new Category()); //Bonus Stages
        }

        public Gameconfig(string filename) : this(new Reader(filename))
        {

        }

        public Gameconfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Gameconfig(Reader reader)
        {
            GameWindowText = reader.ReadRSDKString();
            DataFileName = reader.ReadRSDKString();
            GameDescriptionText = reader.ReadRSDKString();

            //Console.WriteLine("Game Name: " + GameWindowText);
            //Console.WriteLine("???: " + DataFileName);
            //Console.WriteLine("Game Description: " + GameDescriptionText);

            this.ReadObjectData(reader);

            byte GlobalsAmount = reader.ReadByte();

            for (int i = 0; i < GlobalsAmount; i++)
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

        public void Write(Writer writer)
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

            writer.Write((byte)Players.Count);
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

        internal void ReadObjectData(Reader reader)
        {
            byte objects_count = reader.ReadByte();
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString());}
            for (int i = 0; i < objects_count; ++i)
            { ScriptPaths.Add(reader.ReadRSDKString()); /*Console.WriteLine(ScriptPaths[i]);*/ }
        }

        internal void WriteObjectData(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
            foreach (string name in ScriptPaths)
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

        uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }

        /*The Value For DevMenu is at: Line CA0, Column 0B*/
        public void SetDevMenu()
        {
            for (int i = 0; i < GlobalVariables.Count; i++)
            {
                if (GlobalVariables[i].Name == "Options.DevMenuFlag")
                {
                    if (GlobalVariables[i].Value == 1)
                    {
                        GlobalVariables[i].Value = 0;
                        Console.WriteLine("DevMenu Deactivated!");
                        return;
                    }
                    else if (GlobalVariables[i].Value == 0)
                    {
                        GlobalVariables[i].Value = 1;
                        Console.WriteLine("DevMenu Activated!");
                        return;
                    }
                }
            }
        }

    }
}
