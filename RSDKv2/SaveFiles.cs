using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKv2
{
    public class SaveFiles
    {
        public class SaveData
        {
            /// <summary>
            /// what character you are
            /// </summary>
            public int CharacterID;
            /// <summary>
            /// how many lives you have
            /// </summary>
            public int Lives;
            /// <summary>
            /// current scorre
            /// </summary>
            public byte[] Score = new byte[4];
            /// <summary>
            /// what level the player is upto
            /// </summary>
            public int LevelID;
            /// <summary>
            /// how many timestones are collected
            /// </summary>
            public int TimeStones = 127;
            /// <summary>
            /// I have no idea lma
            /// </summary>
            public int unknown2 = 0;
            /// <summary>
            /// what stages have good futures
            /// </summary>
            public byte[] GoodFutures = new byte[4];
            /// <summary>
            /// how many robo machines have been broken
            /// </summary>
            public ushort RoboMachines;
            /// <summary>
            /// how many holograms have been broken
            /// </summary>
            public ushort MSHolograms;

            public SaveData(Stream stream) : this(new Reader(stream))
            {
            }

            public SaveData(string file) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
            }

            internal SaveData(Reader reader)
            {
                //CharacterID = reader.ReadInt32();
                CharacterID = reader.ReadByte();
                reader.ReadBytes(3);
                Lives = reader.ReadInt32();
                Score = reader.ReadBytes(4);
                LevelID = reader.ReadInt32();
                TimeStones = reader.ReadInt32();
                unknown2 = reader.ReadInt32();
                GoodFutures = reader.ReadBytes(4);
                RoboMachines = reader.ReadUInt16();
                MSHolograms = reader.ReadUInt16();
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

            internal void Write(Writer writer)
            {
                writer.Write(CharacterID);//Characters
                writer.Write(Lives);//Lives
                writer.Write(Score); //Unknown 
                writer.Write(LevelID);//Levels
                writer.Write(TimeStones);
                writer.Write(unknown2);//RDC made me do it!
                writer.Write(GoodFutures);
                writer.Write(RoboMachines);
                writer.Write(MSHolograms);//Metal Sonic holograms
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

        }

        public SaveData[] Saves = new SaveData[4];

        //Global Vars
        public int unknown1;
        public int MusVolume;
        public int SFXVolume;
        public int SpindashStyle;
        public int unknown2;
        public int Filter;
        public int OSTStyle;
        public bool TailsUnlocked;

        public List<byte> UnknownGarbo = new List<byte>();

        public SaveFiles(Stream stream) : this(new Reader(stream))
        {
        }

        public SaveFiles(string file) : this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        internal SaveFiles(Reader reader)
        {
            for (int i = 0; i < 4; i++)
            {
                Saves[i] = new SaveData(reader);
            }

            unknown1 = reader.ReadInt32();
            MusVolume = reader.ReadInt32();
            SFXVolume = reader.ReadInt32();
            SpindashStyle = reader.ReadInt32();
            unknown2 = reader.ReadInt32();
            Filter = reader.ReadInt32();
            OSTStyle = reader.ReadInt32();
            int TA = reader.ReadInt32();
            if (TA == 7)
            {
                TailsUnlocked = true;
            }
            else if (TA != 7)
            {
                TailsUnlocked = false;
            }

            while (!reader.IsEof)
            {
                UnknownGarbo.Add(reader.ReadByte());
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

            internal void Write(Writer writer)
            {
            for (int i = 0; i < 4; i++)
            {
                Saves[i].Write(writer);
            }

            writer.Write(unknown1);
            writer.Write(MusVolume);
            writer.Write(SFXVolume);
            writer.Write(SpindashStyle);
            writer.Write(unknown2);
            writer.Write(Filter);
            writer.Write(OSTStyle);
            if (TailsUnlocked)
            {
                writer.Write(7);
            }
            else
            {
                writer.Write(0);
            }

            for (int i = 0; i < UnknownGarbo.Count; i++)
            {
                writer.Write(UnknownGarbo[i]);
            }

            writer.Close();
        }

    }
}
