using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class CommonConfig
    {
        public static readonly byte[] MAGIC = new byte[] { (byte)'C', (byte)'F', (byte)'G', (byte)'\0' };

        const int PALETTES_COUNT = 8;

        public List<string> ObjectsNames = new List<string>();
        public Palette[] Palettes = new Palette[PALETTES_COUNT];
        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();

        internal void ReadMagic(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid config file header magic");
        }

        internal void WriteMagic(Writer writer)
        {
            writer.Write(MAGIC);
        }

        internal void ReadCommonConfig(Reader reader)
        {
            this.ReadObjectsNames(reader);
            this.ReadPalettes(reader);
            this.ReadWAVConfiguration(reader);
        }

        internal void WriteCommonConfig(Writer writer)
        {
            this.WriteObjectsNames(writer);
            this.WritePalettes(writer);
            this.WriteWAVConfiguration(writer);
        }

        internal void ReadObjectsNames(Reader reader)
        {
            byte objects_count = reader.ReadByte();
            for (int i = 0; i < objects_count; ++i)
                ObjectsNames.Add(reader.ReadRSDKString());
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
                writer.WriteRSDKString(name);
        }

        internal void ReadPalettes(Reader reader)
        {
            for (int i = 0; i < PALETTES_COUNT; ++i)
                Palettes[i] = new Palette(reader);
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
                WAVs.Add(new WAVConfiguration(reader));
        }

        internal void WriteWAVConfiguration(Writer writer)
        {
            writer.Write((byte)WAVs.Count);
            foreach (WAVConfiguration wav in WAVs)
                wav.Write(writer);
        }
    }
}
