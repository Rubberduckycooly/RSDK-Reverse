using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKv1
{
    public class Scene
    {
        public class Entity
        {
            /// <summary>
            /// The type of object the entity is
            /// </summary>
            public byte type = 0;
            /// <summary>
            /// The entity's property value (aka subtype in classic sonic games)
            /// </summary>
            public byte propertyValue = 0;
            /// <summary>
            /// the x position (whole numbers)
            /// </summary>
            public short xpos = 0;
            /// <summary>
            /// the y position (whole numbers)
            /// </summary>
            public short ypos = 0;

            public Entity() { }

            public Entity(byte type, byte propertyValue, short xpos, short ypos) : this()
            {
                this.type = type;
                this.propertyValue = propertyValue;
                this.xpos = xpos;
                this.ypos = ypos;
            }

            public Entity(Reader reader) : this()
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                // Entity type, 1 byte, unsigned
                type = reader.ReadByte();

                // Property value, 1 byte, unsigned
                propertyValue = reader.ReadByte();

                // X Position, 2 bytes, big-endian, signed			
                xpos = (short)(reader.ReadSByte() << 8);
                xpos |= reader.ReadByte();

                // Y Position, 2 bytes, big-endian, signed
                ypos = (short)(reader.ReadSByte() << 8);
                ypos |= reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(type);
                writer.Write(propertyValue);

                writer.Write((byte)(xpos >> 8));
                writer.Write((byte)(xpos & 0xFF));

                writer.Write((byte)(ypos >> 8));
                writer.Write((byte)(ypos & 0xFF));
            }
        }

        public enum TrackIDs
        {
            StageTrack1,
            StageTrack2,
            StageTrack3,
            StageTrack4,
            StageTrack5,
            SpeedShoes,
            Invincibility,
            ActClear,
            GameOver,
            Title,
        }

        public enum BackgroundIDs
        {
            None,
            Background1,
            Background2,
            Background3,
            Background4,
            Background5,
            Background6,
            Background7,
            Background8,
        }

        /// <summary>
        /// the Stage Name (what the TitleCard displays)
        /// </summary>
        public string title = "Stage";

        /// <summary>
        /// the chunk layout for the FG layer
        /// </summary>
        public byte[][] layout;

        /// <summary>
        /// the starting Music ID for the stage
        /// </summary>
        public TrackIDs initialMusicID = TrackIDs.StageTrack1;

        /// <summary>
        /// the initial background ID
        /// </summary>
        public BackgroundIDs initialBackgroundID = BackgroundIDs.Background1;

        /// <summary>
        /// player's Spawn Xpos
        /// </summary>
        public short playerSpawnX;

        /// <summary>
        /// player's Spawn Ypos
        /// </summary>
        public short playerSpawnY;

        /// <summary>
        /// the list of entities in the stage
        /// </summary>
        public List<Entity> entities = new List<Entity>();

        /// <summary>
        /// stage width (in chunks)
        /// </summary>
        public byte width;

        /// <summary>
        /// stage height (in chunks)
        /// </summary>
        public byte height;

        /// <summary>
        /// the Max amount of entities that can be in a single stage
        /// </summary>
        public const int ENTITY_LIST_SIZE = 1000;

        public Scene()
        {
            layout = new byte[1][];
            layout[0] = new byte[1];
        }

        public Scene(string filename) : this(new Reader(filename)) { }

        public Scene(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Scene(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            var fileStream = reader.BaseStream as FileStream;
            string filename = fileStream.Name;

            Reader ITMreader = new Reader(Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".itm");

            // Tile Layout Loading
            width = reader.ReadByte();
            height = reader.ReadByte();

            layout = new byte[height][];
            for (int y = 0; y < height; y++)
                layout[y] = new byte[width];

            // Read map data from the map file
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    layout[y][x] = reader.ReadByte();
            }
            reader.Close();

            // Object Layout Loading
            title = ITMreader.ReadStringRSDK();

            initialMusicID = (TrackIDs)ITMreader.ReadByte();
            initialBackgroundID = (BackgroundIDs)ITMreader.ReadByte();
            playerSpawnX = (short)(ITMreader.ReadByte() << 8);
            playerSpawnX |= ITMreader.ReadByte();
            playerSpawnY = (short)(ITMreader.ReadByte() << 8);
            playerSpawnY |= ITMreader.ReadByte();

            // Read entities from the item file
            int entityCount = ITMreader.ReadByte() << 8;
            entityCount |= ITMreader.ReadByte();

            entities.Clear();
            for (int e = 0; e < entityCount; e++)
                entities.Add(new Entity(ITMreader));

            ITMreader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            var fileStream = writer.BaseStream as FileStream;
            string filename = fileStream.Name;

            // Write Tile Layout

            // Write width and height
            writer.Write(width);
            writer.Write(height);

            // Write tile layout
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    writer.Write(layout[y][x]);

            // Close map file
            writer.Close();

            // Write Object layout
            Writer ITMwriter = new Writer(Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".itm");

            // Save zone name
            ITMwriter.WriteStringRSDK(title);

            // Write the Stage Init Data
            ITMwriter.Write((byte)initialMusicID);
            ITMwriter.Write((byte)initialBackgroundID);
            ITMwriter.Write((byte)(playerSpawnX >> 8));
            ITMwriter.Write((byte)(playerSpawnX & 0xFF));
            ITMwriter.Write((byte)(playerSpawnY >> 8));
            ITMwriter.Write((byte)(playerSpawnY & 0xFF));

            // Write number of entities
            ITMwriter.Write((byte)(entities.Count >> 8));
            ITMwriter.Write((byte)(entities.Count & 0xFF));

            // Write entities
            foreach (Entity entity in entities)
                entity.Write(ITMwriter);

            ITMwriter.Close();
        }

        /// <summary>
        /// Resizes a layer.
        /// </summary>
        /// <param name="width">The new Width</param>
        /// <param name="height">The new Height</param>
        public void Resize(byte width, byte height)
        {
            // first take a backup of the current dimensions
            // then update the internal dimensions
            byte oldWidth = this.width;
            byte oldHeight = this.height;
            this.width = width;
            this.height = height;

            // resize the tile map
            System.Array.Resize(ref layout, this.height);

            // fill the extended tile arrays with "empty" values

            // if we're actaully getting shorter, do nothing!
            for (byte y = oldHeight; y < this.height; y++)
            {
                // first create arrays child arrays to the old width
                // a little inefficient, but at least they'll all be equal sized
                layout[y] = new byte[oldWidth];
                for (int x = 0; x < oldWidth; ++x)
                    layout[y][x] = 0; // fill the new ones with blanks
            }

            for (byte y = 0; y < this.height; y++)
            {
                // now resize all child arrays to the new width
                System.Array.Resize(ref layout[y], this.width);
                for (ushort x = oldWidth; x < this.width; x++)
                    layout[y][x] = 0; // and fill with blanks if wider
            }
        }
    }
}
