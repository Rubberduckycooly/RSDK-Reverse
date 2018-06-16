using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv4
{
    public class GameConfig
    {

        public String GameName;
        public String GameSubname;
        //public String GameSubName2;

        public Palette[] Palettes = new Palette[1];
        public List<string> ObjectsNames = new List<string>();
        List<string> SourceTxtLocations = new List<string>();
        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();
        public List<Global> Globals = new List<Global>();
        public List<string> WAVnames = new List<string>();
        public List<Category> Categories = new List<Category>();

        public class SceneInfo
        {
            public string Name;
            public string Zone;
            public string SceneID;

            internal SceneInfo(Reader reader)
            {
                Name = reader.ReadRSDKString();
                Zone = reader.ReadRSDKString();
                SceneID = reader.ReadRSDKString();
                Console.WriteLine("Name = " + Name + " ,Zone = " + Zone + " ,SceneID = " + SceneID);
            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);
                writer.WriteRSDKString(Zone);
                writer.WriteRSDKString(SceneID);
            }
        }

        public class Category
        {
            public string Name;
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            internal Category(Reader reader)
            {
                byte scenes_count = reader.ReadByte();
                //Name = reader.ReadRSDKString();

                for (int i = 0; i < scenes_count; ++i)
                    Scenes.Add(new SceneInfo(reader));
            }

            internal void Write(Writer writer)
            {
                writer.WriteRSDKString(Name);

                writer.Write((byte)Scenes.Count);
                foreach (SceneInfo scene in Scenes)
                    scene.Write(writer);
            }
        }

        public class Global
        {

            string Name;
            byte UnknownValue1;
            byte UnknownValue2;
            byte UnknownValue3;
            byte UnknownValue4;

            internal Global(Reader reader)
            {
                Name = reader.ReadString();
                Console.WriteLine(Name);
                UnknownValue1 = reader.ReadByte();
                UnknownValue2 = reader.ReadByte();
                UnknownValue3 = reader.ReadByte();
                UnknownValue4 = reader.ReadByte();
            }

            internal void Write(Writer writer)
            { 

            }
        }

        public GameConfig(string filename) : this(new Reader(filename))
        {

        }

        public GameConfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private GameConfig(Reader reader)
        {
            GameName = reader.ReadRSDKString();
            GameSubname = reader.ReadRSDKString();

            Console.WriteLine(GameName + " \nSN = " + GameSubname);

            this.ReadPalettes(reader);
            this.ReadObjectsNames(reader);

            byte Globals_Amount = reader.ReadByte();

            for (int i = 0; i < Globals_Amount; i++)
            {
                Globals.Add(new Global(reader));
            }

            this.ReadWAVConfiguration(reader);

            //Stage Order Loading Goes Here!

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
            this.WritePalettes(writer);
            this.WriteObjectsNames(writer);
            this.WriteWAVConfiguration(writer);
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString());}
            for (int i = 0; i < objects_count; ++i)
            { SourceTxtLocations.Add(reader.ReadRSDKString()); Console.WriteLine(SourceTxtLocations[i]); }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
        }

        internal void ReadPalettes(Reader reader)
        {
            for (int i = 0; i < 1; ++i)
            { Palettes[i] = new Palette(reader, 6);}
        }

        internal void WritePalettes(Writer writer)
        {
            foreach (Palette palette in Palettes)
                palette.Write(writer);
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte wavs_count = reader.ReadByte();
            for (int i = 0; i < wavs_count; ++i)
            { WAVnames.Add(reader.ReadRSDKString()); }
            for (int i = 0; i < wavs_count; ++i)
            { WAVs.Add(new WAVConfiguration(reader)); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)WAVs.Count);
            foreach (WAVConfiguration wav in WAVs)
                wav.Write(writer);
        }

    }
}
