using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class StageConfig
    {

        public Palette StagePalette = new Palette();

        public List<string> SoundFX = new List<string>();
        public List<string> SfxNames = new List<string>();

        public List<string> ObjectsNames = new List<string>();
        public List<string> ScriptFilepaths = new List<string>();

        public StageConfig()
        {

        }

        public StageConfig(string filename) : this(new Reader(filename))
        {

        }

        public StageConfig(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public StageConfig(Reader reader)
        {
            StagePalette.Read(reader, 2);

            this.ReadObjectsNames(reader);

            this.ReadWAVConfiguration(reader);

            reader.Close();

        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte unknownval = reader.ReadByte();
            byte objects_count = reader.ReadByte();

            Console.WriteLine(objects_count);
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString()); }
            for (int i = 0; i < objects_count; ++i)
            { ScriptFilepaths.Add(reader.ReadRSDKString()); }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)0);
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
            foreach (string srcname in ScriptFilepaths)
                writer.WriteRSDKString(srcname);
        }

        internal void ReadWAVConfiguration(Reader reader)
        {
            byte SoundFX_count = reader.ReadByte();

            for (int i = 0; i < SoundFX_count; ++i)
            { SoundFX.Add(reader.ReadString()); }
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)SoundFX.Count);
            foreach (string wav in SoundFX)
                writer.Write(wav);
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
            StagePalette.Write(writer);

            WriteObjectsNames(writer);

            WriteWAVConfiguration(writer);

            writer.Close();

        }

    }
}
