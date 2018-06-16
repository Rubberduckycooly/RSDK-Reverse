using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    /* RSDKv3 and RSDKv4 have nearly identical StageConfig file layouts, with RSDKv2 being very similar, 
    it just uses some of the unused bytes at the start of the file */
    public class StageConfig
    {

        byte[] Unknown = new byte[97];

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

            Unknown = reader.ReadBytes(97); // there are 97 bytes that are always 0 so let's just ignore them
            //We read & store them so we can just write them back to the file
            this.ReadObjectsNames(reader);

            this.ReadWAVConfiguration(reader);

        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();
            Console.WriteLine(objects_count);
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString()); }
            for (int i = 0; i < objects_count; ++i)
            { SourceTxtLocations.Add(reader.ReadRSDKString()); Console.WriteLine(SourceTxtLocations[i]); }
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
            { WAVs.Add(new WAVConfiguration(reader)); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)WAVs.Count);
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
            writer.Write(Unknown);

            WriteObjectsNames(writer);

            WriteWAVConfiguration(writer);
        }

    }
}
