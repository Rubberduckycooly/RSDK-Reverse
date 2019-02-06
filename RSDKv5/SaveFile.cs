using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class SaveFiles
    {
        public static int FileSize
        {
            get
            {
                return 0x10000;
            }
        }

        public static bool IsLittleEndian;

        public int[] SaveRAM = new int[FileSize / 4];

        public int SaveFile = 0;
        public int EncoreSaveFile = 0;
        public int SaveFilePos
        {
            get
            {
                return (0x400 * SaveFile);
            }
        }
        public int EncoreSaveFilePos
        {
            get
            {
                return 0x2800 + (0x400 * EncoreSaveFile);
            }
        }

        public int BSSID;
        public int curEmerald;
        public int curEncoreEmerald;
        /// <summary>
        /// what save slot we're using
        /// </summary>
        public int SlotID
        {
            get
            {
                return GetValue(SaveFilePos + 0x58);
            }
            set
            {
                SetValue(SaveFilePos + 0x58, value);
            }
        }
        /// <summary>
        /// what character you are
        /// </summary>
        public int CharacterID
        {
            get
            {
                return GetValue(SaveFilePos + 0x5C);
            }
            set
            {
                SetValue(SaveFilePos + 0x5C, value);
            }
        }
        /// <summary>
        /// what Zone ID the player is upto
        /// </summary>
        public int CurrentZoneID
        {
            get
            {
                return GetValue(SaveFilePos + 0x60);
            }
            set
            {
                SetValue(SaveFilePos + 0x60, value);
            }
        }
        /// <summary>
        /// the save's life count
        /// </summary>
        public int Lives
        {
            get
            {
                return GetValue(SaveFilePos + 0x64);
            }
            set
            {
                SetValue(SaveFilePos + 0x64, value);
            }
        }
        /// <summary>
        /// the Save's Score
        /// </summary>
        public int Score
        {
            get
            {
                return GetValue(SaveFilePos + 0x68);
            }
            set
            {
                SetValue(SaveFilePos + 0x68, value);
            }
        }
        /// <summary>
        /// the target score
        /// </summary>
        public int TargetScore
        {
            get
            {
                return GetValue(SaveFilePos + 0x6C);
            }
            set
            {
                SetValue(SaveFilePos + 0x6C, value);
            }
        }
        /// <summary>
        /// what emeralds have been collected?
        /// </summary>
        public bool EmeraldState
        {
            get
            {
                int emeralds = SaveRAM[(SaveFilePos + 0x70)/4];
                int emeraldActive = 1 << curEmerald;
                return (emeralds & emeraldActive) != 0;
            }
            set
            {
                int emeraldActive = 1 << curEmerald;
                if (value)
                    SaveRAM[(SaveFilePos + 0x70) / 4] |= (byte)emeraldActive;
                else
                    SaveRAM[(SaveFilePos + 0x70) / 4] &= (byte)~(emeraldActive);
            }
        }
        /// <summary>
        /// what's the next special stage that the player goes to?
        /// </summary>
        public int NextSpecialStage
        {
            get
            {
                return GetValue(SaveFilePos + 0x7C);
            }
            set
            {
                SetValue(SaveFilePos + 0x7C, value);
            }
        }
        /// <summary>
        /// was a giant ring used in this zone
        /// </summary>
        public bool GiantRingUsedInZone
        {
            get
            {
                return GetValue(SaveFilePos + 0x80) != 0;
            }
            set
            {
                int i = 0;
                if (value) i = 1;
                SetValue(SaveFilePos + 0x80, i);
            }
        }
        /// <summary>
        /// how many silver medals have been collected
        /// </summary>
        public int SilverMedalCount
        {
            get
            {
                return GetValue(0x2520);
            }
            set
            {
                SetValue(0x2520, value);
            }
        }
        /// <summary>
        /// how many gold medals have been collected
        /// </summary>
        public int GoldMedalCount
        {
            get
            {
                return GetValue(0x251C);
            }
            set
            {
                SetValue(0x251C, value);
            }
        }
        /// <summary>
        /// what BSSstages have been completed
        /// </summary>
        public int BSSstatus
        {
            get
            {
                return GetValue(BSSID + 0x2456);
            }
            set
            {
                SetValue(BSSID + 0x2456, value);
            }
        }

        public int GetValue(int location)
        {
            //get int value from the array, (I put in byte pointers)
            location /= 4;

            //get our bytes
            int response = SaveRAM[location];
            if (IsLittleEndian) //if PC ver, then just return it
            { return response; }
            byte[] tempArray = BitConverter.GetBytes(response);
            Array.Reverse(tempArray);
            return BitConverter.ToInt32(tempArray, 0);
            //else, swap the bytes and return it
        }


        //ENCORE
        public int EncoreZoneID
        {
            get
            {
                return GetValue(EncoreSaveFilePos + 0x60);
            }
            set
            {
                SetValue(EncoreSaveFilePos + 0x60, value);
            }
        }
        public int EncoreUnknown1
        {
            get
            {
                return GetValue(EncoreSaveFilePos + 0x64);
            }
            set
            {
                SetValue(EncoreSaveFilePos + 0x64, value);
            }
        }
        /// <summary>
        /// the Save's Score
        /// </summary>
        public int EncoreScore
        {
            get
            {
                return GetValue(EncoreSaveFilePos + 0x68);
            }
            set
            {
                SetValue(EncoreSaveFilePos + 0x68, value);
            }
        }
        /// <summary>
        /// the target score
        /// </summary>
        public int EncoreTargetScore
        {
            get
            {
                return GetValue(EncoreSaveFilePos + 0x6C);
            }
            set
            {
                SetValue(EncoreSaveFilePos + 0x6C, value);
            }
        }
        /// <summary>
        /// what emeralds have been collected?
        /// </summary>
        public bool EncoreEmeraldState
        {
            get
            {
                int emeralds = SaveRAM[(EncoreSaveFilePos + 0x70) / 4];
                int emeraldActive = 1 << curEmerald;
                return (emeralds & emeraldActive) != 0;
            }
            set
            {
                int emeraldActive = 1 << curEmerald;
                if (value)
                    SaveRAM[(EncoreSaveFilePos + 0x70) / 4] |= (byte)emeraldActive;
                else
                    SaveRAM[(EncoreSaveFilePos + 0x70) / 4] &= (byte)~(emeraldActive);
            }
        }
        /// <summary>
        /// what's the next special stage that the player goes to?
        /// </summary>
        public int EncoreNextSpecialStage
        {
            get
            {
                return GetValue(EncoreSaveFilePos + 0x7C);
            }
            set
            {
                SetValue(EncoreSaveFilePos + 0x7C, value);
            }
        }
        /// <summary>
        /// was a giant ring used in this zone
        /// </summary>
        public bool EncoreGiantRingUsedInZone
        {
            get
            {
                return GetValue(EncoreSaveFilePos + 0x80) != 0;
            }
            set
            {
                int i = 0;
                if (value) i = 1;
                SetValue(EncoreSaveFilePos + 0x80, i);
            }
        }
        public int EncoreMainChar
        {
            get
            {
                return (GetValue(EncoreSaveFilePos + 0x110) >> (8 * 0)) & 0xff;
            }
            set
            {
                SaveRAM[(EncoreSaveFilePos + 0x110) / 4] = (int)((value & 0xFFFFFF00) | 01);
            }
        }
        public int EncoreBuddyChar
        {
            get
            {
                return (GetValue(EncoreSaveFilePos + 0x110) >> (8 * 1)) & 0xff;
            }
            set
            {
                SaveRAM[(EncoreSaveFilePos + 0x110) / 4] = (int)((value & 0xFFFFFF) | 01);
            }
        }

        public int SetValue(int location, int value)
        {
            //get int value from the array, (I put in byte pointers)
            //AKA I'm hella lazy
            location /= 4;

            //get our bytes
            byte[] bytes = BitConverter.GetBytes(value);
            if (!IsLittleEndian)
            {
                //if PC ver, swap bytes
                Array.Reverse(bytes);
                value = BitConverter.ToInt32(bytes, 0);
            }
            //save our data
            SaveRAM[location] = value;
            return SaveRAM[location];
        }

        public SaveFiles(Stream stream, bool isPCVer = true) : this(new Reader(stream), isPCVer)
        {
        }

        public SaveFiles(string file, bool isPCVer = true) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read), isPCVer)
        {
        }

        public SaveFiles(Reader reader, bool isPCVer = true)
        {
            IsLittleEndian = isPCVer;

            for (int i = 0; i < FileSize / 4; i++)
            {
                SaveRAM[i] = reader.ReadInt32();
            }

            reader.Close();
        }

        public void Write(string filename, bool isPCVer = true)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer, isPCVer);
        }

        public void Write(System.IO.Stream stream, bool isPCVer = true)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer, isPCVer);
        }

        public void Write(Writer writer, bool isPCVer = true)
        {

            IsLittleEndian = isPCVer;

            for (int i = 0; i < SaveRAM.Length; i++)
            {
                writer.Write(SaveRAM[i]);
            }
            writer.Close();
        }

    }
}
