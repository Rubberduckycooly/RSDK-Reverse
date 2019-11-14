using System;
using System.Collections.Generic;

namespace RSDKvB
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
                Value = (bytes[3] << 24) + (bytes[2] << 16) + (bytes[1] << 8) + (bytes[0] << 0);
            }

            public void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);
                writer.Write((Value));
            }
        }

        /// <summary>
        /// the game name, appears on the window
        /// </summary>
        public string GameWindowText;
        /// <summary>
        /// the string the appears in the about window
        /// </summary>
        public string GameDescriptionText;

        /// <summary>
        /// a set of colours to be used as the masterpalette
        /// </summary>
        public Palette MasterPalette = new Palette();
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
        /// a list of names for each SFX file
        /// </summary>
        public List<string> SfxNames = new List<string>();
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
            Categories.Add(new Category()); //Presentation Stages
            Categories.Add(new Category()); //Regular Stages
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
            GameDescriptionText = reader.ReadRSDKString();

            //Console.WriteLine("Game Title: " + GameWindowText);

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

        public void Write(Writer writer)
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

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();

            for (int i = 0; i < objects_count; ++i)
            {
                ObjectsNames.Add(reader.ReadRSDKString());
            }

            for (int i = 0; i < objects_count; ++i)
            {
                ScriptPaths.Add(reader.ReadRSDKString());
                Console.WriteLine(ScriptPaths[i]);
            }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);

            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);

            foreach (string name in ScriptPaths)
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
