using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
{
    /* StageConfig Equivelent */
    public class Zoneconfig
    {
        public class ObjectData
        {
            public string FilePath;
            public byte SpriteSheetID;
        }

        public Palette palette = new Palette();

        public List<string> ObjectsSheets = new List<string>();

        public List<ObjectData> Objects = new List<ObjectData>();

        public List<WAVConfiguration> Music = new List<WAVConfiguration>();

        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();

        public Zoneconfig(string filename) : this(new Reader(filename))
        {

        }

        public Zoneconfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Zoneconfig(Reader reader)
        {
            palette.Read(reader, 2);

            this.ReadObjectsSpriteSheets(reader);

            this.ReadObjectsNames(reader);

            this.ReadWAVConfiguration(reader);

            this.ReadOGGConfiguration(reader);

            reader.Close();
        }

        internal void ReadObjectsSpriteSheets(Reader reader)
        {
            byte objectSheets_count = reader.ReadByte();

            for (int i = 0; i < objectSheets_count; ++i)
            { ObjectsSheets.Add(reader.ReadRSDKString()); }
        }

        internal void WriteObjectsSpriteSheets(Writer writer)
        {
            writer.Write((byte)ObjectsSheets.Count);
            foreach (string name in ObjectsSheets)
                writer.WriteRSDKString(name);
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();

            for (int i = 0; i < objects_count; ++i)
            {
                ObjectData OD = new ObjectData();
                OD.FilePath = reader.ReadRSDKString();
                Objects.Add(OD);
            }

            for (int i = 0; i < objects_count; i++)
            {
                Objects[i].SpriteSheetID = reader.ReadByte();
            }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)Objects.Count);

            for (int i = 0; i < Objects.Count; ++i)
            {
                writer.WriteRSDKString(Objects[i].FilePath);
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                writer.Write(Objects[i].SpriteSheetID);
            }
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte wavs_count = reader.ReadByte();

            for (int i = 0; i < wavs_count; ++i)
            { WAVs.Add(new WAVConfiguration(reader)); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)WAVs.Count);
            foreach (WAVConfiguration wav in WAVs)
                wav.Write(writer);
        }

        internal void ReadOGGConfiguration(Reader reader)
        {
            byte Music_count = reader.ReadByte();

            for (int i = 0; i < Music_count; ++i)
            { Music.Add(new WAVConfiguration(reader)); }
        }

        internal void WriteOGGConfiguration(Writer writer)
        {
            writer.Write((byte)Music.Count);
            foreach (WAVConfiguration mus in Music)
                mus.Write(writer);
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

            palette.Write(writer);

            WriteObjectsSpriteSheets(writer);

            WriteObjectsNames(writer);

            WriteWAVConfiguration(writer);

            WriteOGGConfiguration(writer);

            writer.Close();
        }

    }
}
