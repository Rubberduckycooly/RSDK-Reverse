using System.Collections.Generic;

namespace RSDKv1
{
    public class StageConfig
    {
        public enum GfxSlotIDs
        {
            TitleCard,
            Unused1,
            Shields,
            Unused2,
            General,
            General2,
            CustomSheet1,
            CustomSheet2,
            CustomSheet3,
            CustomSheet4,
            CustomSheet5,
            Player1_Sheet1,
            Player1_Sheet2,
            Player1_Sheet3,
            Player2_Sheet1,
            Player2_Sheet2,
            Player2_Sheet3,
        };

        public class ObjectInfo
        {
            /// <summary>
            /// the filepath to the script
            /// </summary>
            public string script = "Folder/Script.rsf";

            /// <summary>
            /// the spritesheet ID for the object
            /// </summary>
            public GfxSlotIDs sheetID = GfxSlotIDs.CustomSheet1;

            public ObjectInfo() { }
        }

        /// <summary>
        /// the stageconfig palette (index 96-128)
        /// </summary>
        public Palette stagePalette = new Palette();

        /// <summary>
        /// a list of sheets to add to the global list
        /// </summary>
        public List<string> spriteSheets = new List<string>();

        /// <summary>
        /// the list of stage objects
        /// </summary>
        public List<ObjectInfo> objects = new List<ObjectInfo>();

        /// <summary>
        /// the list of the stage music tracks
        /// </summary>
        public List<string> musicTracks = new List<string>();

        /// <summary>
        /// the list of stage-specific SoundFX
        /// </summary>
        public List<string> soundFX = new List<string>();

        public StageConfig() { }

        public StageConfig(string filename) : this(new Reader(filename)) { }

        public StageConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

        public StageConfig(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            // Palettes
            stagePalette.Read(reader, 2);

            // SpriteSheets
            byte sheetCount = reader.ReadByte();
            spriteSheets.Clear();
            for (int i = 0; i < sheetCount; ++i)
                spriteSheets.Add(reader.ReadStringRSDK());

            // Objects
            byte objectCount = reader.ReadByte();
            objects.Clear();
            for (int i = 0; i < objectCount; ++i)
            {
                ObjectInfo info = new ObjectInfo();
                info.script = reader.ReadStringRSDK();
                objects.Add(info);
            }

            foreach (ObjectInfo info in objects)
                info.sheetID = (GfxSlotIDs)reader.ReadByte();

            // SoundFX
            byte sfxCount = reader.ReadByte();
            soundFX.Clear();
            for (int i = 0; i < sfxCount; ++i)
                soundFX.Add(reader.ReadStringRSDK());

            // Music
            byte trackCount = reader.ReadByte();
            musicTracks.Clear();
            for (int i = 0; i < trackCount; ++i)
                musicTracks.Add(reader.ReadStringRSDK());

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // Palettes
            stagePalette.Write(writer);

            // SpriteSheets
            writer.Write((byte)spriteSheets.Count);
            foreach (string sheet in spriteSheets)
                writer.WriteStringRSDK(sheet);

            // Objects
            writer.Write((byte)objects.Count);
            foreach (ObjectInfo info in objects)
                writer.Write(info.script);

            foreach (ObjectInfo info in objects)
                writer.Write((byte)info.sheetID);

            // SoundFX
            writer.Write((byte)soundFX.Count);
            foreach (string path in soundFX)
                writer.WriteStringRSDK(path);

            // Music
            writer.Write((byte)musicTracks.Count);
            foreach (string track in musicTracks)
                writer.WriteStringRSDK(track);

            writer.Close();
        }

    }
}
