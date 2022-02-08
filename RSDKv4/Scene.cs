using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv4
{
    public class Scene
    {
        public class Entity
        {
            public class VariableInfo
            {
                public VariableInfo() { }

                /// <summary>
                /// the variable's value
                /// </summary>
                public int value = 0;
                /// <summary>
                /// determines if the variable is active or not
                /// </summary>
                public bool active = false;
            };

            public static List<string> variableNames = new List<string>() {
                "State",
                "Direction",
                "Scale",
                "Rotation",
                "DrawOrder",
                "Priority",
                "Alpha",
                "Animation",
                "AnimationSpeed",
                "Frame",
                "InkEffect",
                "Value0",
                "Value1",
                "Value2",
                "Value3"
            };

            public static List<string> variableTypes = new List<string>()  {
                "int",
                "uint8",
                "int",
                "int",
                "uint8",
                "uint8",
                "uint8",
                "uint8",
                "int",
                "uint8",
                "uint8",
                "int",
                "int",
                "int",
                "int"
            };

            /// <summary>
            /// The type of object the entity is
            /// </summary>
            public byte type = 0;
            /// <summary>
            /// The entity's property value (aka subtype in classic sonic games)
            /// </summary>
            public byte propertyValue = 0;
            /// <summary>
            /// the x position (shifted 16-bit format, so 1.0 == (1 << 16))
            /// </summary>
            public int xpos = 0;
            /// <summary>
            /// the y position (shifted 16-bit format, so 1.0 == (1 << 16))
            /// </summary>
            public int ypos = 0;
            /// <summary>
            /// info for all the entity's variables 
            /// </summary>
            public VariableInfo[] variables = new VariableInfo[15];

            /// <summary>
            /// XPos but represented as a floating point number rather than a fixed point one
            /// </summary>
            public float xposF
            {
                get { return xpos / 65536.0f; }
                set { xpos = (int)(value / 65536); }
            }

            /// <summary>
            /// YPos but represented as a floating point number rather than a fixed point one
            /// </summary>
            public float yposF
            {
                get { return ypos / 65536.0f; }
                set { ypos = (int)(value / 65536); }
            }

            public Entity()
            {
                for (int v = 0; v < 15; ++v)
                    variables[v] = new VariableInfo();
            }

            public Entity(byte type, byte propertyValue, int xpos, int ypos) : this()
            {
                this.type = type;
                this.propertyValue = propertyValue;
                this.xpos = xpos;
                this.ypos = ypos;
            }

            public Entity(byte type, byte propertyValue, float xposF, float yposF) : this()
            {
                this.type = type;
                this.propertyValue = propertyValue;
                this.xposF = xposF;
                this.yposF = yposF;
            }

            public Entity(Reader reader) : this()
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                //Variable flags, 2 bytes, unsigned
                ushort flags = reader.ReadUInt16();
                for (int i = 0; i < 15; i++)
                    variables[i].active = (flags & (1 << i)) != 0;

                // entity type, 1 byte, unsigned
                type = reader.ReadByte();

                // PropertyValue, 1 byte, unsigned
                propertyValue = reader.ReadByte();

                //a position, made of 8 bytes, 4 for X, 4 for Y
                xpos = reader.ReadInt32();
                ypos = reader.ReadInt32();

                for (int i = 0; i < 15; i++)
                {
                    if (variables[i].active)
                    {
                        if (variableTypes[i] == "uint8")
                            variables[i].value = reader.ReadByte();
                        else
                            variables[i].value = reader.ReadInt32();
                    }
                }
            }

            public void write(Writer writer)
            {
                int flags = 0;
                for (int i = 0; i < 15; i++)
                {
                    if (variables[i].active)
                        flags |= 1 << i;
                    else
                        flags &= ~(1 << i);
                }
                writer.Write((ushort)flags);

                writer.Write(type);
                writer.Write(propertyValue);

                writer.Write(xpos);
                writer.Write(ypos);

                for (int i = 0; i < 15; i++)
                {
                    if (variables[i].active)
                    {
                        if (variableTypes[i] == "uint8")
                            writer.Write((byte)variables[i].value);
                        else
                            writer.Write(variables[i].value);
                    }
                }
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
        /// the stage's name (what the titlecard displays)
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

            // Map width in 128 pixel units
            // In RSDKv4 it's one byte long (with an unused byte after each one), little-endian

            width = reader.ReadByte();
            reader.ReadByte(); // Unused

            height = reader.ReadByte();
            reader.ReadByte(); // Unused

            layout = new ushort[height][];
            for (int i = 0; i < height; i++)
                layout[i] = new ushort[width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 128x128 Block number is 16-bit
                    // Little-Endian in RSDKv4	
                    layout[y][x] = reader.ReadByte();
                    layout[y][x] |= (ushort)(reader.ReadByte() << 8);
                }
            }

            // Read entities

            // 2 bytes, little-endian, unsigned
            int entityCount = reader.ReadByte();
            entityCount |= reader.ReadByte() << 8;

            entities.Clear();
            for (int o = 0; o < entityCount; o++)
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

            // Write width
            writer.Write(width);
            writer.Write((byte)0);

            // Write height
            writer.Write(height);
            writer.Write((byte)0);

            // Write tile layout
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    writer.Write((byte)(layout[h][w] & 0xFF));
                    writer.Write((byte)(layout[h][w] >> 8));
                }
            }

            // Write number of entities

            writer.Write((byte)(entities.Count & 0xFF));
            writer.Write((byte)(entities.Count >> 8));

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
