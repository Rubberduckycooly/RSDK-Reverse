using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace RSDKvB
{
    public class SaveFiles
    {
        public static int FileSize
        {
            get
            {
                return 0x8000;
            }
        }

        public int[] SaveRAM = new int[FileSize / 4];

        public int SaveFile = 0;
        public int SaveFilePos
        {
            get
            {
                return (SaveFile * 0x20);
            }
        }

        /// <summary>
        /// what character you are
        /// </summary>
        public int CharacterID
        {
            get
            {
                return SaveRAM[(SaveFilePos + 0x00) / 4];
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x00) / 4] = value;
            }
        }
        /// <summary>
        /// how many lives you have
        /// </summary>
        public int Lives
        {
            get
            {
                return SaveRAM[(SaveFilePos + 0x04) / 4];
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x04) / 4] = value;
            }
        }
        /// <summary>
        /// current score
        /// </summary>
        public int Score
        {
            get
            {
                byte[] intBytes = BitConverter.GetBytes(SaveRAM[(SaveFilePos + 0x08) / 4]);
                return intBytes[0] + (intBytes[1] << 8) + (intBytes[2] << 16);
            }
            set
            {
                byte[] intBytes = BitConverter.GetBytes(value);
                SaveRAM[(SaveFilePos + 0x08) / 4] = intBytes[0] + (intBytes[1] << 8) + (intBytes[2] << 16);
            }
        }

        public int ScoreBonus
        {
            get
            {
                return SaveRAM[(SaveFilePos + 0x0C) / 4];
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x0C) / 4] = value;
            }
        }
        /// <summary>
        /// what level the player is upto
        /// </summary>
        public int ZoneID
        {
            get
            {
                return SaveRAM[(SaveFilePos + 0x10) / 4];
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x10) / 4] = value;
            }
        }
        /// <summary>
        /// how many timestones are collected
        /// </summary>
        public int Emeralds
        {
            get
            {
                return SaveRAM[(SaveFilePos + 0x14) / 4];
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x14) / 4] = value;
            }
        }

        public int SpecialZoneID
        {
            get
            {
                return SaveRAM[(SaveFilePos + 0x18) / 4];
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x18) / 4] = value;
            }
        }

        //Global Vars

        public int NewSave
        {
            get
            {
                return SaveRAM[0x80 / 4];
            }
            set
            {
                SaveRAM[0x80 / 4] = value;
            }
        }

        /// <summary>
        /// how loud the music is
        /// </summary>
        public int MusVolume
        {
            get
            {
                return SaveRAM[0x84 / 4];
            }
            set
            {
                SaveRAM[0x84 / 4] = value;
            }
        }
        /// <summary>
        /// how loud the SoundFX is
        /// </summary>
        public int SFXVolume
        {
            get
            {
                return SaveRAM[0x88 / 4];
            }
            set
            {
                SaveRAM[0x88 / 4] = value;
            }
        }
        /// <summary>
        /// using S1 control style
        /// </summary>
        public int ControlStyle
        {
            get
            {
                return SaveRAM[0x8C / 4];
            }
            set
            {
                SaveRAM[0x8C / 4] = value;
            }
        }
        /// <summary>
        /// what region the game is from
        /// </summary>
        public int Region
        {
            get
            {
                return SaveRAM[0x90 / 4];
            }
            set
            {
                SaveRAM[0x90 / 4] = value;
            }
        }

        public int TouchRectW
        {
            get
            {
                return SaveRAM[0x94 / 4];
            }
            set
            {
                SaveRAM[0x94 / 4] = value;
            }
        }
        public int TouchRectH
        {
            get
            {
                return SaveRAM[0x98 / 4];
            }
            set
            {
                SaveRAM[0x98 / 4] = value;
            }
        }
        public int TouchRectJumpX
        {
            get
            {
                return SaveRAM[0x9C / 4];
            }
            set
            {
                SaveRAM[0x9C / 4] = value;
            }
        }
        public int TouchRectJumpY
        {
            get
            {
                return SaveRAM[0xA0 / 4];
            }
            set
            {
                SaveRAM[0xA0 / 4] = value;
            }
        }
        public int TouchRectMoveX
        {
            get
            {
                return SaveRAM[0xA4 / 4];
            }
            set
            {
                SaveRAM[0xA4 / 4] = value;
            }
        }
        public int TouchRectY
        {
            get
            {
                return SaveRAM[0xA8 / 4];
            }
            set
            {
                SaveRAM[0xA8 / 4] = value;
            }
        }

        /// <summary>
        /// the screen filter
        /// </summary>
        public int Filter
        {
            get
            {
                return SaveRAM[0xAC / 4];
            }
            set
            {
                SaveRAM[0xAC / 4] = value;
            }
        }
        /// <summary>
        /// something to do with display but idk what
        /// </summary>
        public int DisplayValue
        {
            get
            {
                return SaveRAM[0xB0 / 4];
            }
            set
            {
                SaveRAM[0xB0 / 4] = value;
            }
        }
        /// <summary>
        /// unknown
        /// </summary>
        public int Unknown
        {
            get
            {
                return SaveRAM[0xB4 / 4];
            }
            set
            {
                SaveRAM[0xB4 / 4] = value;
            }
        }

        public void SetEmeraldState(int pos, bool Set)
        {
            if (Set)
            {
                Emeralds |= 1 << pos;
            }
            if (!Set)
            {
                Emeralds &= ~(1 << pos);
            }
        }

        public SaveFiles(Stream stream) : this(new Reader(stream))
        {
        }

        public SaveFiles(string file) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        internal SaveFiles(Reader reader)
        {
            for (int i = 0; i < FileSize / 4; i++)
            {
                SaveRAM[i] = reader.ReadInt32();
            }

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
            for (int i = 0; i < SaveRAM.Length; i++)
            {
                writer.Write(SaveRAM[i]);
            }
            writer.Close();
        }

    }
}
