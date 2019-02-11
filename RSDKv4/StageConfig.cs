using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv4
{
    /* RSDKv3 and RSDKv4 have identical StageConfig file layouts, with RSDKv2 being very similar, 
    it just uses some of the unused bytes at the start of the file */
    public class StageConfig
    {

        public Palette palette = new Palette();

        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();
        public List<string> WAVnames = new List<string>();

        public List<string> ObjectsNames = new List<string>();
        public List<string> SourceTxtLocations = new List<string>();

        public StageConfig(string filename) : this(new Reader(filename))
        {

        }

        public StageConfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public StageConfig(Reader reader)
        {
            palette.Read(reader, 2);

            reader.ReadByte(); //A byte comes just after the palette but it's use is unknown

            this.ReadWAVConfiguration(reader);

            this.ReadObjectsNames(reader);

            reader.Close();
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();

            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString()); }
            for (int i = 0; i < objects_count; ++i)
            { SourceTxtLocations.Add(reader.ReadRSDKString());}
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
            foreach (string srcname in SourceTxtLocations)
                writer.WriteRSDKString(srcname);
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
            foreach (string wavname in WAVnames)
                writer.WriteRSDKString(wavname);
            foreach (WAVConfiguration wav in WAVs)
                wav.Write(writer);
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

            writer.Write((byte)0);

            WriteWAVConfiguration(writer);

            WriteObjectsNames(writer);

            writer.Close();

        }

    }
}
