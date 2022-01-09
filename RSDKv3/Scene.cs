using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv3
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
                read(reader);
            }

            public void read(Reader reader)
            {
                // entity type, 1 byte, unsigned
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

            public void write(Writer writer)
            {
                writer.Write(type);
                writer.Write(propertyValue);

                writer.Write((byte)(xpos >> 8));
                writer.Write((byte)(xpos & 0xFF));

                writer.Write((byte)(ypos >> 8));
                writer.Write((byte)(ypos & 0xFF));
            }
        }

        public enum ActiveLayers
        {
            Foreground,
            Background1,
            Background2,
            Background3,
            Background4,
            Background5,
            Background6,
            Background7,
            Background8,
            None,
        }
        public enum LayerMidpoints
        {
            BeforeLayer0,
            AfterLayer0,
            AfterLayer1,
            AfterLayer2,
            AfterLayer3,
        }

        /// <summary>
        /// the Stage Name (what the titlecard displays)
        /// </summary>
        public string title = "STAGE";

        /// <summary>
        /// the chunk layout for the FG layer
        /// </summary>
        public ushort[][] layout;

        /// <summary>
        /// Active Layer 0
        /// </summary>
        public ActiveLayers activeLayer0 = ActiveLayers.Background1;
        /// <summary>
        /// Active Layer 1
        /// </summary>
        public ActiveLayers activeLayer1 = ActiveLayers.None;
        /// <summary>
        /// Active Layer 2
        /// </summary>
        public ActiveLayers activeLayer2 = ActiveLayers.Foreground;
        /// <summary>
        /// Active Layer 3
        /// </summary>
        public ActiveLayers activeLayer3 = ActiveLayers.Foreground;
        /// <summary>
        /// Determines what layers should draw using high visual plane, in an example of 2, active layers 2 & 3 would use high plane tiles, while 0 & 1 would use low plane
        /// </summary>
        public LayerMidpoints layerMidpoint = LayerMidpoints.AfterLayer2;

        /// <summary>
        /// the list of entities in the stage
        /// </summary>
        public List<Entity> entities = new List<Entity>();
        /// <summary>
        /// a list of names for each Object Type
        /// </summary>
        public List<string> objectTypeNames = new List<string>();

        /// <summary>
        /// stage width (in chunks)
        /// </summary>
        public byte width = 0;
        /// <summary>
        /// stage height (in chunks)
        /// </summary>
        public byte height = 0;

        /// <summary>
        /// the Max amount of entities that can be in a single stage
        /// </summary>
        public const int ENTITY_LIST_SIZE = 1024;

        public Scene()
        {
            layout = new ushort[1][];
            layout[0] = new ushort[1];
        }

        public Scene(string filename) : this(new Reader(filename)) { }

        public Scene(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Scene(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            title = reader.readRSDKString();

            activeLayer0  = (ActiveLayers)reader.ReadByte();
            activeLayer1  = (ActiveLayers)reader.ReadByte();
            activeLayer2  = (ActiveLayers)reader.ReadByte();
            activeLayer3  = (ActiveLayers)reader.ReadByte();
            layerMidpoint = (LayerMidpoints)reader.ReadByte();

            // Map width/height in 128 pixel units
            // In RSDKv3 it's one byte long

            width = reader.ReadByte();
            height = reader.ReadByte();

            layout = new ushort[height][];
            for (int i = 0; i < height; i++)
                layout[i] = new ushort[width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 128x128 Block number is 16-bit
                    // Big-Endian in RSDKv3
                    layout[y][x] = (ushort)(reader.ReadByte() << 8);
                    layout[y][x] |= reader.ReadByte();
                }
            }

            // Read number of object types
            int objectTypeCount = reader.ReadByte();

            objectTypeNames.Clear();
            for (int n = 0; n < objectTypeCount; n++)
                objectTypeNames.Add(reader.readRSDKString());

            // Read entities

            // 2 bytes, Big-Endian, unsigned
            int entityCount = reader.ReadByte() << 8;
            entityCount |= reader.ReadByte();

            for (int n = 0; n < entityCount; n++)
                entities.Add(new Entity(reader));

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            // Write zone name		
            writer.writeRSDKString(title);

            // Write the active layers & midpoint
            writer.Write((byte)activeLayer0);
            writer.Write((byte)activeLayer1);
            writer.Write((byte)activeLayer2);
            writer.Write((byte)activeLayer3);
            writer.Write((byte)layerMidpoint);

            // Write width and height
            writer.Write(width);
            writer.Write(height);

            // Write tile layout
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    writer.Write((byte)(layout[h][w] >> 8));
                    writer.Write((byte)(layout[h][w] & 0xff));
                }
            }

            // Write number of object types
            writer.Write((byte)objectTypeNames.Count);

            // Write object type names
            // Ignore first object type (Blank Object), it is not stored.
            foreach (string typeName in objectTypeNames)
                writer.writeRSDKString(typeName);

            // Write number of entities
            writer.Write((byte)(entities.Count >> 8));
            writer.Write((byte)(entities.Count & 0xFF));

            // Write entities
            foreach (Entity entity in entities)
                entity.write(writer);

            writer.Close();
        }

        /// <summary>
        /// Resizes a layer.
        /// </summary>
        /// <param name="width">The new Width</param>
        /// <param name="height">The new Height</param>
        public void resize(byte width, byte height)
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
            for (byte i = oldHeight; i < this.height; i++)
            {
                // first create arrays child arrays to the old width
                // a little inefficient, but at least they'll all be equal sized
                layout[i] = new ushort[oldWidth];
                for (int j = 0; j < oldWidth; ++j)
                    layout[i][j] = 0; // fill the new ones with blanks
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
