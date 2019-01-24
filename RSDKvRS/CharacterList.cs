using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvRS
{
    //*.mdf Files
    public class CharacterList
    {
        public class Character
        {
            /// <summary>
            /// The Display name of the "team"
            /// </summary>
            public string DisplayName = "";
            /// <summary>
            /// the String that's used for the GOT THROUGH screen
            /// </summary>
            public string MainCharacter = "";
            /// <summary>
            /// How many characters are in the "team"
            /// </summary>
            public string CharacterCount = "";
            /// <summary>
            /// Player 1's sprite mappings
            /// </summary>
            public string Character1Anim = "";
            /// <summary>
            /// Player 2's sprite mappings (or NULL for no player 2)
            /// </summary>
            public string Character2Anim = "";

            public Character()
            {

            }

            public Character(StreamReader reader)
            {
                char buf = 'n';

                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        DisplayName = DisplayName + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        MainCharacter = MainCharacter + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        CharacterCount = CharacterCount + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        Character1Anim = Character1Anim + buf;
                    }
                }
                buf = 'n';
                while (buf != '^')
                {
                    buf = (char)reader.Read();
                    if (buf == '^') { break; }
                    else
                    {
                        Character2Anim = Character2Anim + buf;
                    }
                }
                buf = 'n';
                reader.ReadLine();

            }

            public void write(StreamWriter writer)
            {
                for (int i = 0; i < DisplayName.Length; i++)
                {
                    writer.Write(DisplayName[i]);
                }
                writer.Write('^');
                for (int i = 0; i < MainCharacter.Length; i++)
                {
                    writer.Write(MainCharacter[i]);
                }
                writer.Write('^');
                for (int i = 0; i < CharacterCount.Length; i++)
                {
                    writer.Write(CharacterCount[i]);
                }
                writer.Write('^');
                for (int i = 0; i < Character1Anim.Length; i++)
                {
                    writer.Write(Character1Anim[i]);
                }
                writer.Write('^');
                for (int i = 0; i < Character2Anim.Length; i++)
                {
                    writer.Write(Character2Anim[i]);
                }
                writer.Write('^');
                writer.WriteLine();

            }

        }

        public List<Character> Characters = new List<Character>();

        public CharacterList()
        {
            Characters.Add(new Character());
        }

        public CharacterList(string filename) : this(new StreamReader(File.OpenRead(filename)))
        {

        }

        public CharacterList(System.IO.Stream stream) : this(new StreamReader(stream))
        {

        }

        public CharacterList(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                Characters.Add(new Character(reader));
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filename)))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            this.Write(stream);
        }

        internal void Write(StreamWriter writer)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].write(writer);
            }
            writer.Close();
        }
    }
}
