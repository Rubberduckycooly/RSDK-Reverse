using System;
using System.Collections.Generic;

namespace RSDKv3
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
                /// the stage's identifier (E.G Act1 or Act2)
                /// </summary>
                public string id = "1";
                /// <summary>
                /// Determines if the stage is highlighted on the dev menu
                /// </summary>
                public bool highlighted = false;

                public StageInfo() { }

                public StageInfo(Reader reader)
                {
                    read(reader);
                }

                public void read(Reader reader)
                {
                    folder = reader.readRSDKString();
                    id = reader.readRSDKString();
                    name = reader.readRSDKString();
                    highlighted = reader.ReadBoolean();
                }

                public void write(Writer writer)
                {
                    writer.writeRSDKString(folder);
                    writer.writeRSDKString(id);
                    writer.writeRSDKString(name);
                    writer.Write(highlighted);
                }
            }

            public StageList() { }

            public StageList(Reader reader)
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                list.Clear();
                byte stageCount = reader.ReadByte();
                for (int i = 0; i < stageCount; i++)
                    list.Add(new StageInfo(reader));
            }

            public void write(Writer writer)
            {
                writer.Write((byte)list.Count);
                foreach (StageInfo stage in list)
                    stage.write(writer);
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
                read(reader);
            }

            public void read(Reader reader)
            {
                name = reader.ReadString();
                byte[] bytes = reader.readBytes(4);
                value = (bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + (bytes[3] << 0);
            }

            public void write(Writer writer)
            {
                writer.writeRSDKString(name);
                // Value is Big-Endian in RSDKv3
                byte[] bytes = BitConverter.GetBytes(value);
                writer.Write(bytes[3]);
                writer.Write(bytes[2]);
                writer.Write(bytes[1]);
                writer.Write(bytes[0]);
            }
        }

        public class ObjectInfo
        {
            public ObjectInfo() { }

            public string name = "Object";
            public string script = "Folder/Script.txt";
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
        /// currently unknown, never used, not likely to be important or known
        /// </summary>
        private string unknown = "Data";

        /// <summary>
        /// the list of global objects
        /// </summary>
        public List<ObjectInfo> objects = new List<ObjectInfo>();
        /// <summary>
        /// the list of global SoundFX paths
        /// </summary>
        public List<string> soundFX = new List<string>();
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
            stageLists.Add(new StageList()); //Presentation Stages
            stageLists.Add(new StageList()); //Regular Stages
            stageLists.Add(new StageList()); //Special Stages
            stageLists.Add(new StageList()); //Bonus Stages
        }

        public GameConfig(string filename) : this(new Reader(filename)) { }
        public GameConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

        public GameConfig(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            // General
            gameTitle = reader.readRSDKString();
            unknown = reader.readRSDKString();
            gameDescription = reader.readRSDKString();

            // Objects
            objects.Clear();
            byte objectCount = reader.ReadByte();
            for (int i = 0; i < objectCount; ++i)
            {
                ObjectInfo info = new ObjectInfo();
                info.name = reader.readRSDKString();

                objects.Add(info);
            }

            foreach (ObjectInfo info in objects)
                info.script = reader.readRSDKString();

            // Global Variables
            globalVariables.Clear();
            byte globalVariableCount = reader.ReadByte();
            for (int i = 0; i < globalVariableCount; i++)
                globalVariables.Add(new GlobalVariable(reader));

            // SoundFX
            soundFX.Clear();
            byte sfxCount = reader.ReadByte();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(reader.readRSDKString());

            // Players
            players.Clear();
            byte playerCount = reader.ReadByte();
            for (int i = 0; i < playerCount; i++)
                players.Add(reader.readRSDKString());

            // Stages
            stageLists.Clear();
            stageLists.Add(new StageList(reader)); // Presentation Stages
            stageLists.Add(new StageList(reader)); // Regular Stages
            stageLists.Add(new StageList(reader)); // Special Stages
            stageLists.Add(new StageList(reader)); // Bonus Stages

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // General
            writer.writeRSDKString(gameTitle);
            writer.writeRSDKString(unknown);
            writer.writeRSDKString(gameDescription);

            // Objects
            writer.Write((byte)objects.Count);

            foreach (ObjectInfo info in objects)
                writer.writeRSDKString(info.name);

            foreach (ObjectInfo info in objects)
                writer.writeRSDKString(info.script);

            // Global Variables
            writer.Write((byte)globalVariables.Count);
            foreach (GlobalVariable variable in globalVariables)
                variable.write(writer);

            // SoundFX
            writer.Write((byte)soundFX.Count);

            foreach (string sfx in soundFX)
                writer.writeRSDKString(sfx);

            // Players
            writer.Write((byte)players.Count);
            foreach (string player in players)
                writer.Write(player);

            // Stages
            for (int s = 0; s < 4; s++)
                stageLists[s].write(writer);

            writer.Close();
        }

    }
}
