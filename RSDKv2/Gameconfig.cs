using System;
using System.Collections.Generic;

namespace RSDKv2
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
                    Read(reader);
                }

                public void Read(Reader reader)
                {
                    folder = reader.ReadStringRSDK();
                    id = reader.ReadStringRSDK();
                    name = reader.ReadStringRSDK();
                    highlighted = reader.ReadBoolean();
                }

                public void Write(Writer writer)
                {
                    writer.WriteStringRSDK(folder);
                    writer.WriteStringRSDK(id);
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
                value = (bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + (bytes[3] << 0);
            }

            public void Write(Writer writer)
            {
                writer.WriteStringRSDK(name);
                // Value is Big-Endian in RSDKv2
                byte[] bytes = BitConverter.GetBytes(value);
                writer.Write(bytes[3]);
                writer.Write(bytes[2]);
                writer.Write(bytes[1]);
                writer.Write(bytes[0]);
            }
        }

        public class PlayerInfo
        {
            /// <summary>
            /// The location of the player animation file
            /// </summary>
            public string animation = "Folder/Player.bin";
            /// <summary>
            /// the location of the player script
            /// </summary>
            public string script = "Folder/Player.txt";
            /// <summary>
            /// the name of the player
            /// </summary>
            public string name = "PLAYER";

            public PlayerInfo() { }

            public PlayerInfo(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                animation = reader.ReadStringRSDK();
                script = reader.ReadStringRSDK();
                name = reader.ReadStringRSDK();
            }

            public void Write(Writer writer)
            {
                writer.WriteStringRSDK(animation);
                writer.WriteStringRSDK(script);
                writer.WriteStringRSDK(name);
            }

        }

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
        /// the list of global object script paths
        /// </summary>
        public List<string> objects = new List<string>();

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
        public List<PlayerInfo> players = new List<PlayerInfo>();

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
            unknown = reader.ReadStringRSDK();
            gameDescription = reader.ReadStringRSDK();

            // Objects
            objects.Clear();
            byte objectCount = reader.ReadByte();
            for (int i = 0; i < objectCount; ++i)
                objects.Add(reader.ReadStringRSDK());

            // Global Variables
            globalVariables.Clear();
            byte globalVariableCount = reader.ReadByte();
            for (int i = 0; i < globalVariableCount; i++)
                globalVariables.Add(new GlobalVariable(reader));

            // SoundFX
            soundFX.Clear();
            byte sfxCount = reader.ReadByte();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(reader.ReadStringRSDK());

            // Players
            players.Clear();
            byte playerCount = reader.ReadByte();
            for (int i = 0; i < playerCount; i++)
                players.Add(new PlayerInfo(reader));

            // Stages
            stageLists.Clear();
            stageLists.Add(new StageList(reader)); // Presentation Stages
            stageLists.Add(new StageList(reader)); // Regular Stages
            stageLists.Add(new StageList(reader)); // Special Stages
            stageLists.Add(new StageList(reader)); // Bonus Stages

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
            writer.WriteStringRSDK(unknown);
            writer.WriteStringRSDK(gameDescription);

            // Objects
            writer.Write((byte)objects.Count);

            foreach (string script in objects)
                writer.WriteStringRSDK(script);

            // Global Variables
            writer.Write((byte)globalVariables.Count);
            foreach (GlobalVariable variable in globalVariables)
                variable.Write(writer);

            // SoundFX
            writer.Write((byte)soundFX.Count);

            foreach (string sfx in soundFX)
                writer.WriteStringRSDK(sfx);

            // Players
            writer.Write((byte)players.Count);
            foreach (PlayerInfo player in players)
                player.Write(writer);

            // Stages
            for (int s = 0; s < 4; s++)
                stageLists[s].Write(writer);

            writer.Close();
        }
    }
}
