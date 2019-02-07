using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKv2
{
    public class SaveFiles
    {

        public class TimeAttackStageData
        {
            public class TimeAttackData
            {
                public int Minutes;
                public int Seconds;
                public int Milliseconds;

                public TimeAttackData()
                {

                }

                public TimeAttackData(int Offset)
                {
                    //SaveRAM[Offset];
                }
            }

            TimeAttackData[] Times = new TimeAttackData[3];

            public TimeAttackStageData()
            {
                Times[0] = new TimeAttackData();
                Times[1] = new TimeAttackData();
                Times[2] = new TimeAttackData();
            }

            public TimeAttackStageData(int Offset)
            {
                int BaseOffset = Offset;
                for (int i = Offset; Offset < BaseOffset + 3; Offset++)
                {
                    Times[Offset - BaseOffset] = new TimeAttackData(Offset);
                }
            }
        }

        public static int FileSize
        {
            get
            {
                return 0x8000;
            }
        }

        public static int[] SaveRAM = new int[FileSize / 4];

        public int SaveFile = 0;
        public int TimeAttackStage = 0;
        public int TimeAttackPlace = 0; //0 to 2
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
        /// <summary>
        /// what zone the player is upto
        /// </summary>
        public int ZoneID
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
        /// how many timestones are collected
        /// </summary>
        public int TimeStones
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
        /// what special stage the user is upto
        /// </summary>
        public int SpecialZoneID
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
        /// <summary>
        /// what stages have good futures
        /// </summary>
        public int GoodFutures
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
        /// <summary>
        /// how many robo machines have been broken
        /// </summary>
        public int FuturesSaved
        {
            get
            {
                byte[] intBytes = BitConverter.GetBytes(SaveRAM[(SaveFilePos + 0x1C) / 4]);
                return intBytes[2] + (intBytes[3] << 8);
            }
            set
            {
                SaveRAM[(SaveFilePos + 0x1C) / 4] = value;
            }
        }

        //Global Vars
        /// <summary>
        /// if set to 0 the engine resets everything
        /// </summary>
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
        /// using CD or MD spindash style
        /// </summary>
        public int SpindashStyle
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
        /// no idea
        /// </summary>
        public int unknown3
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
        /// <summary>
        /// the screen filter
        /// </summary>
        public int Filter
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
        /// <summary>
        /// JP or US OST?
        /// </summary>
        public int OSTStyle
        {
            get
            {
                byte[] intBytes = BitConverter.GetBytes(SaveRAM[0x98 / 4]);
                return intBytes[0];
            }
            set
            {
                SaveRAM[0x98 / 4] = value;
            }
        }
        /// <summary>
        /// do we have tails unlocked?
        /// </summary>
        public bool TailsUnlocked
        {
            get
            {
                if (SaveRAM[0x9C / 4] == 7)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    SaveRAM[0x9C / 4] = 7;
                }
                else
                {
                    SaveRAM[0x9C / 4] = 0;
                }
            }
        }
        /// <summary>
        /// what zones are unlocked in time attack?
        /// </summary>
        public int TimeAttackUnlocks
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


        public void SetTimeStone(int pos, bool Set)
        {
            if (Set)
            {
                TimeStones |= 1 << pos;
            }
            if (!Set)
            {
                TimeStones &= ~(1 << pos);
            }
        }

        public SaveFiles()
        {
            SaveFile = 1;
        }

        public SaveFiles(Stream stream) : this(new Reader(stream))
        {
        }

        public SaveFiles(string file) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        internal SaveFiles(Reader reader)
        {
            SaveFile = 1;
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
