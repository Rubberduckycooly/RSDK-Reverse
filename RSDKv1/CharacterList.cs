using System.Collections.Generic;
using System.IO;

namespace RSDKv1
{
    public class CharacterList
    {
        public class PlayerInfo
        {
            /// <summary>
            /// The display name of the character on the menu
            /// </summary>
            public string displayName = "";
            /// <summary>
            /// the name that's used for the [Player] GOT THROUGH screen
            /// </summary>
            public string playerName = "";
            /// <summary>
            /// if P2 is enabled on this character
            /// </summary>
            public bool hasP2 = false;
            /// <summary>
            /// Player 1's animation file (Relative to Data/Characters/)
            /// </summary>
            public string player1Anim = "Player.Ani";
            /// <summary>
            /// Player 2's animation file (Relative to Data/Characters/)
            /// </summary>
            public string player2Anim = "NULL";

            public PlayerInfo() { }

            public PlayerInfo(StreamReader reader)
            {
                Read(reader);
            }

            public void Read(StreamReader reader)
            {
                displayName = ReadString(reader);
                playerName = ReadString(reader);
                hasP2 = ReadString(reader) == "2";
                player1Anim = ReadString(reader);
                player2Anim = ReadString(reader);
                if (!hasP2)
                    player2Anim = "NULL";

                reader.ReadLine();
            }

            public void Write(StreamWriter writer)
            {
                WriteString(writer, displayName);
                WriteString(writer, playerName);
                WriteString(writer, hasP2 ? "2" : "1");
                WriteString(writer, player1Anim);
                WriteString(writer, hasP2 ? player2Anim : "NULL");

                writer.Write('\r');
                writer.Write('\n');
            }

            private string ReadString(StreamReader reader)
            {
                string str = "";

                while (true)
                {
                    char buf = (char)reader.Read();
                    if (buf == '^')
                        break;
                    else
                        str += buf;
                }

                return str;
            }

            private void WriteString(StreamWriter writer, string str)
            {
                for (int i = 0; i < str.Length; i++)
                    writer.Write(str[i]);
                writer.Write('^');
            }
        }

        public List<PlayerInfo> players = new List<PlayerInfo>();

        public CharacterList() { }

        public CharacterList(string filename) : this(new StreamReader(File.OpenRead(filename))) { }

        public CharacterList(System.IO.Stream stream) : this(new StreamReader(stream)) { }

        public CharacterList(StreamReader reader)
        {
            Read(reader);
        }

        public void Read(StreamReader reader)
        {
            players.Clear();
            while (!reader.EndOfStream)
                players.Add(new PlayerInfo(reader));

            reader.Close();
        }

        public void Write(string filename)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filename)))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
                Write(writer);
        }

        public void Write(StreamWriter writer)
        {
            foreach (PlayerInfo player in players)
                player.Write(writer);

            writer.Close();
        }
    }
}
