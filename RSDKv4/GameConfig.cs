using System;
using System.Collections.Generic;

namespace RSDKv4
{
    public class GameConfig
    {
        public class StageList
        {
            /// <summary>
            /// the list of stages in this category
            /// </summary>
            public List<StageInfo> list = new List<StageInfo>();

            public class StageInfo
            {
                /// <summary>
                /// the stage name (shows up on the dev menu)
                /// </summary>
                public string name = "STAGE";

                /// <summary>
                /// the folder of the stage
                /// </summary>
                public string folder = "Folder";

                /// <summary>
                /// the stage's identifier (E.G Act'1' or Act'2')
                /// </summary>
                public string actID = "1";

                /// <summary>
                /// Determines if the stage is highlighted on the dev menu
                /// </summary>
                public bool highlighted = false;

                public StageInfo() { }

                public StageInfo(Reader reader)
                {
                    Read(reader);
                }

                public void Read(Reader reader)
                {
                    folder = reader.ReadStringRSDK();
                    actID = reader.ReadStringRSDK();
                    name = reader.ReadStringRSDK();
                    highlighted = reader.ReadBoolean();
                }

                public void Write(Writer writer)
                {
                    writer.WriteStringRSDK(folder);
                    writer.WriteStringRSDK(actID);
                    writer.WriteStringRSDK(name);
                    writer.Write(highlighted);
                }
            }

            public StageList() { }

            public StageList(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                list.Clear();
                byte stageCount = reader.ReadByte();
                for (int i = 0; i < stageCount; i++)
                    list.Add(new StageInfo(reader));
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)list.Count);
                foreach (StageInfo stage in list)
                    stage.Write(writer);
            }

        }

        public class GlobalVariable
        {
            /// <summary>
            /// the name of the variable
            /// </summary>
            public string name = "Variable";

            /// <summary>
            /// the variable's value
            /// </summary>
            public int value = 0;

            public GlobalVariable() { }

            public GlobalVariable(string name, int value = 0)
            {
                this.name = name;
                this.value = value;
            }

            public GlobalVariable(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                name = reader.ReadString();
                byte[] bytes = reader.ReadBytes(4);
                value = (bytes[3] << 24) + (bytes[2] << 16) + (bytes[1] << 8) + (bytes[0] << 0);
            }

            public void Write(Writer writer)
            {
                writer.WriteStringRSDK(name);
                // Value is Little-Endian in RSDKv4
                byte[] bytes = BitConverter.GetBytes(value);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);
                writer.Write(bytes[2]);
                writer.Write(bytes[3]);
            }
        }

        public class ObjectInfo
        {
            public ObjectInfo() { }

            public string name = "Object";
            public string script = "Folder/Script.txt";
        };

        public class SoundInfo
        {
            public SoundInfo() { }

            public string name = "Sound";
            public string path = "Folder/Sound.wav";
        };

        /// <summary>
        /// the game name, appears on the window
        /// </summary>
        public string gameTitle = "Retro-Engine";

        /// <summary>
        /// the string the appears in the about window
        /// </summary>
        public string gameDescription = "";

        /// <summary>
        /// a set of colors to be used as the masterpalette
        /// </summary>
        public Palette masterPalette = new Palette();

        /// <summary>
        /// the list of global objects
        /// </summary>
        public List<ObjectInfo> objects = new List<ObjectInfo>();

        /// <summary>
        /// the list of global SoundFX
        /// </summary>
        public List<SoundInfo> soundFX = new List<SoundInfo>();

        /// <summary>
        /// the list of global variable names and values
        /// </summary>
        public List<GlobalVariable> globalVariables = new List<GlobalVariable>();

        /// <summary>
        /// the list of player names
        /// </summary>
        public List<string> players = new List<string>();

        /// <summary>
        /// the category list (stage list)
        /// </summary>
        public List<StageList> stageLists = new List<StageList>();

        public GameConfig()
        {
            stageLists.Add(new StageList()); // Presentation Stages
            stageLists.Add(new StageList()); // Regular Stages
            stageLists.Add(new StageList()); // Special Stages
            stageLists.Add(new StageList()); // Bonus Stages
        }

        public GameConfig(string filename) : this(new Reader(filename)) { }
        public GameConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

        public GameConfig(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            // General
            gameTitle = reader.ReadStringRSDK();
            gameDescription = reader.ReadStringRSDK();

            // Palettes
            masterPalette = new Palette(reader, 6);

            // Objects
            objects.Clear();
            byte objectCount = reader.ReadByte();
            for (int i = 0; i < objectCount; ++i)
            {
                ObjectInfo info = new ObjectInfo();
                info.name = reader.ReadStringRSDK();

                objects.Add(info);
            }

            foreach (ObjectInfo info in objects)
                info.script = reader.ReadStringRSDK();

            // Global Variables
            globalVariables.Clear();
            byte globalVariableCount = reader.ReadByte();
            for (int i = 0; i < globalVariableCount; i++)
                globalVariables.Add(new GlobalVariable(reader));

            // SoundFX
            soundFX.Clear();
            byte sfxCount = reader.ReadByte();
            for (int i = 0; i < sfxCount; ++i)
            {
                SoundInfo info = new SoundInfo();
                info.name = reader.ReadStringRSDK();

                soundFX.Add(info);
            }

            foreach (SoundInfo info in soundFX)
                info.path = reader.ReadStringRSDK();

            // Players
            players.Clear();
            byte playerCount = reader.ReadByte();
            for (int i = 0; i < playerCount; i++)
                players.Add(reader.ReadStringRSDK());

            // Stages
            stageLists.Clear();
            stageLists.Add(new StageList(reader)); //Presentation Stages
            stageLists.Add(new StageList(reader)); //Regular Stages
            stageLists.Add(new StageList(reader)); //Special Stages
            stageLists.Add(new StageList(reader)); //Bonus Stages

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // General
            writer.WriteStringRSDK(gameTitle);
            writer.WriteStringRSDK(gameDescription);

            // Palettes
            masterPalette.Write(writer);

            // Objects
            writer.Write((byte)objects.Count);

            foreach (ObjectInfo info in objects)
                writer.WriteStringRSDK(info.name);

            foreach (ObjectInfo info in objects)
                writer.WriteStringRSDK(info.script);

            // Global Variables
            writer.Write((byte)globalVariables.Count);
            foreach (GlobalVariable variable in globalVariables)
                variable.Write(writer);

            // SoundFX
            writer.Write((byte)soundFX.Count);

            foreach (SoundInfo info in soundFX)
                writer.WriteStringRSDK(info.name);

            foreach (SoundInfo info in soundFX)
                writer.WriteStringRSDK(info.path);

            // Players
            writer.Write((byte)players.Count);
            foreach (string player in players)
                writer.Write(player);

            // Stages
            for (int i = 0; i < 4; i++)
                stageLists[i].Write(writer);

            writer.Close();
        }

    }
}
