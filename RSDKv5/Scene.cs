using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKv5
{
    #region Editor
    public class SceneEditorMetadata
    {
        /// <summary>
        /// Unknown value 1, is set to 2/3/4 by default
        /// </summary>
        private byte unknown1 = 3; //usually 2/3/4
        /// <summary>
        /// Background color 1
        /// </summary>
        public Color bgColor1 = new Color(0xFF, 0, 0xFF);
        /// <summary>
        /// Background color 2
        /// </summary>
        public Color bgColor2 = new Color(0, 0xFF, 0);
        /// <summary>
        /// A set of 7 bytes that (seemingly) always have the same value of "01010400010400"
        /// </summary>
        private byte[] unknownBytes; // Const: 01010400010400
        /// <summary>
        /// The name of the stamps file
        /// </summary>
        public string libraryName = "Library.bin";
        /// <summary>
        /// Another unknown Value
        /// </summary>
        private byte unknown2 = 0;

        public SceneEditorMetadata()
        {
            unknownBytes = new byte[] { 0x1, 0x1, 0x4, 0x0, 0x1, 0x4, 0x0 };
        }

        public SceneEditorMetadata(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            unknown1 = reader.ReadByte();
            bgColor1 = new Color(reader);
            bgColor2 = new Color(reader);
            unknownBytes = reader.ReadBytes(7);
            libraryName = reader.ReadStringRSDK();
            unknown2 = reader.ReadByte();
        }

        public void Write(Writer writer)
        {
            writer.Write(unknown1);
            bgColor1.Write(writer);
            bgColor2.Write(writer);
            writer.Write(unknownBytes);
            writer.WriteStringRSDK(libraryName);
            writer.Write(unknown2);
        }
    }
    #endregion

    #region Misc
    [Serializable]
    public class NameIdentifier
    {
        /// <summary>
        /// the MD5 hash of the name in bytes
        /// </summary>
        public byte[] hash;
        /// <summary>
        /// the name in plain text
        /// </summary>
        public string name = null;

        public bool usingHash = true;

        public NameIdentifier(string name)
        {
            hash = MD5Hasher.GetHash(new System.Text.ASCIIEncoding().GetBytes(name));
            this.name = name;
            usingHash = false;
        }

        public NameIdentifier(byte[] hash)
        {
            this.hash = hash;
        }

        public NameIdentifier(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            hash = reader.ReadBytes(16);
        }

        public void Write(Writer writer)
        {
            writer.Write(hash);
        }

        public string HashString()
        {
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }

        public override string ToString()
        {
            if (name != null) return name;
            return HashString();
        }
    }
    #endregion

    #region Tile Layers
    public class ScrollInfo
    {
        /// <summary>
        /// how fast the line moves while the player is moving, relative to 0x100.
        /// E.G: 0x100 == move 1 pixel per 1 pixel of camera movement, 0x80 = 1 pixel every 2 pixels of camera movement, etc
        /// </summary>
        public short parallaxFactor = 0x100;
        /// <summary>
        /// How fast the line moves without the player moving
        /// </summary>
        public short scrollSpeed = 0;
        /// <summary>
        /// determines if the scrollInfo allows deformation or not
        /// </summary>
        public bool deform = false;
        /// <summary>
        /// unknown value, appears unused in-engine
        /// </summary>
        private byte unknown = 0;

        /// <summary>
        /// parallaxFactor, represented as a floating point number relative to 1.0
        /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
        /// </summary>
        public float parallaxFactorF
        {
            get
            {
                return parallaxFactor * 0.00390625f; // 1 / 256
            }
            set
            {
                parallaxFactor = (byte)(value * 256.0f);
            }
        }

        /// <summary>
        /// scrollSpeed, represented as a floating point number relative to 1.0
        /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
        /// </summary>
        public float scrollSpeedF
        {
            get
            {
                return scrollSpeed * 0.00390625f; // 1 / 256
            }
            set
            {
                scrollSpeed = (byte)(value * 256.0f);
            }
        }

        public ScrollInfo(short parallaxFactor = 0x100, short scrollSpeed = 0, bool deform = false)
        {
            this.parallaxFactor = parallaxFactor;
            this.scrollSpeed = scrollSpeed;
            this.deform = deform;
        }

        public ScrollInfo(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            parallaxFactor = reader.ReadInt16();
            scrollSpeed = reader.ReadInt16();
            deform = reader.ReadBoolean();
            unknown = reader.ReadByte();
        }

        public void Write(Writer writer)
        {
            writer.Write(parallaxFactor);
            writer.Write(scrollSpeed);
            writer.Write(deform);
            writer.Write(unknown);
        }
    }

    [Serializable]
    public class SceneLayer
    {
        public class Tile
        {
            public enum Directions
            {
                FlipNone,
                FlipX,
                FlipY,
                FlipXY,
            }
            public enum Solidities
            {
                SolidNone,
                SolidTop,
                SolidAllButTop,
                SolidAll,
            }

            /// <summary>
            /// private, but can be accessed via regular operators: E.G (ushort tile = layout[y][x], or layout[y][x] = 0xFFFF)
            /// </summary>
            private ushort tile = 0xFFFF;

            public static implicit operator ushort(Tile t) => t.tile;
            public static implicit operator Tile(ushort t) => new Tile(t);

            public static implicit operator int(Tile t) => t.tile;
            public static implicit operator Tile(int t) => new Tile(t);

            public static implicit operator uint(Tile t) => t.tile;
            public static implicit operator Tile(uint t) => new Tile(t);

            /// <summary>
            /// if the tile is the transparent tile or not
            /// </summary>
            public bool isBlank
            {
                get { return tile == 0xFFFF; }
            }

            public static readonly Tile blank = new Tile(0xFFFF);

            /// <summary>
            /// the tile's index
            /// </summary>
            public ushort tileIndex
            {
                get { return (ushort)(tile & 0x3FF); }
                set
                {
                    uint store = (ushort)(tile >> 10);
                    tile = (ushort)(value | (store << 10));
                }
            }

            /// <summary>
            /// the flip value of the tile
            /// </summary>
            public Directions direction
            {
                get
                {
                    return (Directions)((tile >> 10) & 3);
                }
                set
                {
                    tile = (ushort)SetBit(10, ((int)value & 1) != 0, tile);
                    tile = (ushort)SetBit(11, ((int)value & 2) != 0, tile);
                }
            }

            /// <summary>
            /// the solidity for Collision Path A
            /// </summary>
            public Solidities solidityA
            {
                get
                {
                    return (Solidities)((tile >> 12) & 3);
                }
                set
                {
                    tile = (ushort)SetBit(12, ((int)value & 1) != 0, tile);
                    tile = (ushort)SetBit(13, ((int)value & 2) != 0, tile);
                }
            }
            /// <summary>
            /// the solidity for Collision Path B
            /// </summary>
            public Solidities solidityB
            {
                get
                {
                    return (Solidities)((tile >> 14) & 3);
                }
                set
                {
                    tile = (ushort)SetBit(14, ((int)value & 1) != 0, tile);
                    tile = (ushort)SetBit(15, ((int)value & 2) != 0, tile);
                }
            }

            public Tile() { }
            private Tile(ushort t) { tile = t; }
            private Tile(int t) { tile = (ushort)t; }
            private Tile(uint t) { tile = (ushort)t; }
        }

        public enum Types
        {
            /// <summary>
            /// self-explanitory, uses LineScroll to do horizontal parallax, while layer scrolls on Y axis
            /// </summary>
            HScroll,
            /// <summary>
            /// also self-explanitory, uses LineScroll to do vertical parallax, while layer scrolls on X axis
            /// </summary>
            VScroll,    // also self-explanitory
            /// <summary>
            /// special type that allows the game to go wild and create tons of custom FX with callbacks, pretty useless on its own without code that uses it though
            /// </summary>
            RotoZoom,
            /// <summary>
            /// basically zero parallax, entire layer moves as if it's a HScroll layer with no lineScroll
            /// </summary>
            Basic,
        }

        /// <summary>
        /// determines if the layer is visible or not, only used in-editor, set the layer's drawOrder to 16 if you wish for the layer to be invisible in-game
        /// </summary>
        public bool visible = false;

        /// <summary>
        /// the layer's name
        /// </summary>
        public string name = "New Layer";

        /// <summary>
        /// determines layer type
        /// </summary>
        public Types type = Types.HScroll;
        /// <summary>
        /// what drawlayer this layer is on, valid values are 0-15, everything else will be invisible in-game
        /// </summary>
        public byte drawOrder = 0;

        /// <summary>
        /// the layer's width (in tiles)
        /// </summary>
        private ushort width = 0;
        /// <summary>
        /// the layer's height (in tiles)
        /// </summary>
        private ushort height = 0;

        /// <summary>
        /// the Speed of the layer when the player is moving
        /// </summary>
        public short parallaxFactor = 0x100;
        /// <summary>
        /// the Speed of the layer when the player isn't moving
        /// </summary>
        public short scrollSpeed = 0;

        /// <summary>
        /// the line scroll data
        /// </summary>
        public List<ScrollInfo> scrollInfo = new List<ScrollInfo>();

        /// <summary>
        /// the line scroll indexes
        /// </summary>
        public byte[] lineScroll;

        /// <summary>
        /// the tile layout for the layer
        /// </summary>
        public Tile[][] layout;


        /// <summary>
        /// parallaxFactor, represented as a floating point number relative to 1.0
        /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
        /// </summary>
        public float parallaxFactorF
        {
            get
            {
                return parallaxFactor * 0.00390625f; // 1 / 256
            }
            set
            {
                parallaxFactor = (byte)(value * 256.0f);
            }
        }

        /// <summary>
        /// scrollSpeed, represented as a floating point number relative to 1.0
        /// E.G: 1.0 == move 1 pixel per 1 pixel of camera movement, 0.5 = 1 pixel every 2 pixels of camera movement, etc
        /// </summary>
        public float scrollSpeedF
        {
            get
            {
                return scrollSpeed * 0.00390625f; // 1 / 256
            }
            set
            {
                scrollSpeed = (byte)(value * 256.0f);
            }
        }

        public SceneLayer(string name = "New Layer", ushort width = 16, ushort height = 16)
        {
            this.name = name;
            this.width = width;
            this.height = height;

            scrollInfo.Add(new ScrollInfo());
            // Per pixel (of height or of width, depends if it scrolls horizontal or veritcal)
            lineScroll = new byte[height * 16];
            layout = new Tile[height][];
            for (int y = 0; y < height; ++y)
            {
                layout[y] = new Tile[width];
                for (int x = 0; x < width; ++x)
                    layout[y][x] = Tile.blank; // unlike earlier versions, v5 uses 0xFFFF to signify the "transparent" tile, meaning all 1024 tiles can be used, as opposed to 1023 previously
            }
        }

        public SceneLayer(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            visible = reader.ReadBoolean();

            name = reader.ReadStringRSDK();

            type = (Types)reader.ReadByte();
            drawOrder = reader.ReadByte();

            width = reader.ReadUInt16();
            height = reader.ReadUInt16();

            parallaxFactor = reader.ReadInt16();
            scrollSpeed = reader.ReadInt16();

            ushort scrollInfoCount = reader.ReadUInt16();
            scrollInfo.Clear();
            for (int i = 0; i < scrollInfoCount; ++i)
                scrollInfo.Add(new ScrollInfo(reader));

            // Read Line Scroll, its compressed using ZLib compression
            lineScroll = reader.ReadCompressed();

            // Read tile map, its compressed using ZLib compression
            layout = new Tile[height][];
            using (Reader creader = reader.GetCompressedStream())
            {
                for (int y = 0; y < height; ++y)
                {
                    layout[y] = new Tile[width];
                    for (int x = 0; x < width; ++x)
                        layout[y][x] = creader.ReadUInt16();
                }
            }
        }

        public void Write(Writer writer)
        {
            writer.Write(visible);

            writer.WriteStringRSDK(name);

            writer.Write((byte)type);
            writer.Write(drawOrder);

            writer.Write(width);
            writer.Write(height);

            writer.Write(parallaxFactor);
            writer.Write(scrollSpeed);

            writer.Write((ushort)scrollInfo.Count);
            foreach (ScrollInfo info in scrollInfo)
                info.Write(writer);

            writer.WriteCompressed(lineScroll);

            using (MemoryStream cmem = new MemoryStream())
            {
                using (Writer cwriter = new Writer(cmem))
                {
                    for (int y = 0; y < height; ++y)
                    {
                        for (int x = 0; x < width; ++x)
                            cwriter.Write(layout[y][x]);
                    }
                    cwriter.Close();
                    writer.WriteCompressed(cmem.ToArray());
                }
            }
        }

        /// <summary>
        /// Resizes a layer.
        /// </summary>
        /// <param name="width">The new Width</param>
        /// <param name="height">The new Height</param>
        public void Resize(ushort width, ushort height)
        {
            // first take a backup of the current dimensions
            // then update the internal dimensions
            ushort oldWidth = this.width;
            ushort oldHeight = this.height;
            this.width = width;
            this.height = height;

            // resize the various scrolling and tile arrays
            Array.Resize(ref lineScroll, this.height * 16);
            Array.Resize(ref layout, this.height);

            // fill the extended tile arrays with "empty" values

            // if we're actaully getting shorter, do nothing!
            for (ushort i = oldHeight; i < this.height; i++)
            {
                // first create arrays child arrays to the old width
                // a little inefficient, but at least they'll all be equal sized
                layout[i] = new Tile[oldWidth];
                for (int j = 0; j < oldWidth; ++j)
                    layout[i][j] = Tile.blank; // fill the new ones with blanks
            }

            for (ushort y = 0; y < this.height; y++)
            {
                // now resize all child arrays to the new width
                Array.Resize(ref layout[y], this.width);
                for (ushort x = oldWidth; x < this.width; x++)
                    layout[y][x] = Tile.blank; // and fill with blanks if wider
            }
        }

        private static int SetBit(int pos, bool set, int val)
        {
            if (set)
                val |= 1 << pos;

            if (!set)
                val &= ~(1 << pos);

            return val;
        }
    }
    #endregion

    #region Variables
    [Serializable]
    public enum VariableTypes
    {
        UInt8,
        UInt16,
        UInt32,
        Int8,
        Int16,
        Int32,
        Enum,
        Bool,
        String,
        Vector2,
        Float, // an assumption, but highly likely
        Color,
    }

    [Serializable]
    public class VariableInfo
    {
        /// <summary>
        /// the name of the variable
        /// </summary>
        public NameIdentifier name = new NameIdentifier("variable");

        /// <summary>
        /// the type of the variable
        /// </summary>
        public VariableTypes type = VariableTypes.UInt8;

        public VariableInfo() { }

        public VariableInfo(NameIdentifier name, VariableTypes type)
        {
            this.name = name;
            this.type = type;
        }

        public VariableInfo(string name, VariableTypes type) : this(new NameIdentifier(name), type) { }

        public VariableInfo(Reader reader, List<string> variableNames = null)
        {
            Read(reader, variableNames);
        }

        public void Read(Reader reader, List<string> variableNames = null)
        {
            name = new NameIdentifier(reader);
            type = (VariableTypes)reader.ReadByte();

            // if we have possible names, search em
            if (variableNames != null)
            {
                string hashString = name.HashString();
                foreach (string varName in variableNames)
                {
                    NameIdentifier currentName = new NameIdentifier(varName);
                    String currentHashedName = currentName.HashString();
                    if (currentHashedName == hashString)
                    {
                        name = currentName;
                        break;
                    }
                }
            }
        }

        public void Write(Writer writer)
        {
            name.Write(writer);
            writer.Write((byte)type);
        }
    }

    [Serializable]
    public class VariableValue
    {
        #region Definitions
        public class Vector2
        {
            public int x = 0;
            public int y = 0;

            public Vector2() { }
        }

        /// <summary>
        /// the uint8 value of the variable
        /// </summary>
        byte value_uint8 = 0;
        /// <summary>
        /// the uint16 value of the variable
        /// </summary>
        ushort value_uint16 = 0;
        /// <summary>
        /// the uint32 value of the variable
        /// </summary>
        uint value_uint32 = 0;
        /// <summary>
        /// the int8 value of the variable
        /// </summary>
        sbyte value_int8 = 0;
        /// <summary>
        /// the int16 value of the variable
        /// </summary>
        short value_int16 = 0;
        /// <summary>
        /// the int32 value of the variable
        /// </summary>
        int value_int32 = 0;
        /// <summary>
        /// the enum value of the variable
        /// </summary>
        int value_enum = 0;
        /// <summary>
        /// the bool value of the variable
        /// </summary>
        bool value_bool = false;
        /// <summary>
        /// the string value of the variable
        /// </summary>
        string value_string = string.Empty; // default to empty string, null causes many problems
        /// <summary>
        /// the vector2 value of the variable
        /// </summary>
        Vector2 value_vector2 = new Vector2();
        /// <summary>
        /// the float value of the variable
        /// </summary>
        float value_float = 0.0f;
        /// <summary>
        /// the color value of the variable
        /// </summary>
        Color value_color = new Color(0xFF, 0x00, 0xFF);

        public VariableTypes type = VariableTypes.UInt8;
        #endregion

        #region Accessors
        private void checkType(VariableTypes type)
        {
            if (type != this.type)
            {
                switch (type)
                {
                    case VariableTypes.UInt8:
                        value_uint8 = 0;
                        break;

                    case VariableTypes.UInt16:
                        value_uint16 = 0;
                        break;

                    case VariableTypes.UInt32:
                        value_uint32 = 0;
                        break;

                    case VariableTypes.Int8:
                        value_int8 = 0;
                        break;

                    case VariableTypes.Int16:
                        value_int16 = 0;
                        break;

                    case VariableTypes.Int32:
                        value_int32 = 0;
                        break;

                    case VariableTypes.Enum:
                        value_enum = 0;
                        break;

                    case VariableTypes.Bool:
                        value_bool = false;
                        break;

                    case VariableTypes.Color:
                        value_color = Color.EMPTY;
                        break;

                    case VariableTypes.Vector2:
                        value_vector2.x = 0;
                        value_vector2.y = 0;
                        break;

                    case VariableTypes.Float:
                        value_float = 0.0f;
                        break;

                    case VariableTypes.String:
                        value_string = string.Empty;
                        break;

                    default:
                        throw new Exception("Unexpected value type.");

                }
            }
        }
        public byte ValueUInt8
        {
            get { checkType(VariableTypes.UInt8); return value_uint8; }
            set { checkType(VariableTypes.UInt8); value_uint8 = value; }
        }
        public ushort ValueUInt16
        {
            get { checkType(VariableTypes.UInt16); return value_uint16; }
            set { checkType(VariableTypes.UInt16); value_uint16 = value; }
        }
        public uint ValueUInt32
        {
            get { checkType(VariableTypes.UInt32); return value_uint32; }
            set { checkType(VariableTypes.UInt32); value_uint32 = value; }
        }
        public sbyte ValueInt8
        {
            get { checkType(VariableTypes.Int8); return value_int8; }
            set { checkType(VariableTypes.Int8); value_int8 = value; }
        }
        public short ValueInt16
        {
            get { checkType(VariableTypes.Int16); return value_int16; }
            set { checkType(VariableTypes.Int16); value_int16 = value; }
        }
        public int ValueInt32
        {
            get { checkType(VariableTypes.Int32); return value_int32; }
            set { checkType(VariableTypes.Int32); value_int32 = value; }
        }
        public int ValueEnum
        {
            get { checkType(VariableTypes.Enum); return value_enum; }
            set { checkType(VariableTypes.Enum); value_enum = value; }
        }
        public bool ValueBool
        {
            get { checkType(VariableTypes.Bool); return value_bool; }
            set { checkType(VariableTypes.Bool); value_bool = value; }
        }
        public string ValueString
        {
            get { checkType(VariableTypes.String); return value_string; }
            set { checkType(VariableTypes.String); value_string = value; }
        }
        public Vector2 ValueVector2
        {
            get { checkType(VariableTypes.Vector2); return value_vector2; }
            set { checkType(VariableTypes.Vector2); value_vector2 = value; }
        }
        private float ValueFloat
        {
            get { checkType(VariableTypes.Float); return value_float; }
            set { checkType(VariableTypes.Float); value_float = value; }
        }
        public Color ValueColor
        {
            get { checkType(VariableTypes.Color); return value_color; }
            set { checkType(VariableTypes.Color); value_color = value; }
        }
        #endregion

        #region Init
        public VariableValue()
        {
            type = 0;
        }
        public VariableValue(VariableTypes type)
        {
            this.type = type;
        }
        public VariableValue Clone()
        {
            VariableValue n = new VariableValue(type);

            n.value_uint8 = value_uint8;
            n.value_uint16 = value_uint16;
            n.value_uint32 = value_uint32;
            n.value_int8 = value_int8;
            n.value_int16 = value_int16;
            n.value_int32 = value_int32;
            n.value_enum = value_enum;
            n.value_bool = value_bool;
            n.value_string = value_string;
            n.value_vector2 = value_vector2;
            n.value_float = value_float;
            n.value_color = value_color;

            return n;
        }
        public VariableValue(Reader reader, VariableTypes type)
        {
            this.type = type;
            Read(reader);
        }
        #endregion

        #region Read/Write
        public void Read(Reader reader)
        {
            switch (type)
            {
                case VariableTypes.UInt8:
                    value_uint8 = reader.ReadByte();
                    break;

                case VariableTypes.UInt16:
                    value_uint16 = reader.ReadUInt16();
                    break;

                case VariableTypes.UInt32:
                    value_uint32 = reader.ReadUInt32();
                    break;

                case VariableTypes.Int8:
                    value_int8 = reader.ReadSByte();
                    break;

                case VariableTypes.Int16:
                    value_int16 = reader.ReadInt16();
                    break;

                case VariableTypes.Int32:
                    value_int32 = reader.ReadInt32();
                    break;

                case VariableTypes.Enum:
                    value_enum = reader.ReadInt32();
                    break;

                case VariableTypes.Bool:
                    value_bool = reader.ReadBool32();
                    break;

                case VariableTypes.String:
                    value_string = reader.ReadStringRSDK_UTF16();
                    break;

                case VariableTypes.Vector2:
                    value_vector2.x = reader.ReadInt32();
                    value_vector2.y = reader.ReadInt32();
                    break;

                case VariableTypes.Float:
                    value_float = reader.ReadSingle();
                    break;

                case VariableTypes.Color:
                    value_color = new Color(reader);
                    break;
            }
        }
        public void Write(Writer writer)
        {
            switch (type)
            {
                case VariableTypes.UInt8:
                    writer.Write(value_uint8);
                    break;

                case VariableTypes.UInt16:
                    writer.Write(value_uint16);
                    break;

                case VariableTypes.UInt32:
                    writer.Write(value_uint32);
                    break;

                case VariableTypes.Int8:
                    writer.Write(value_int8);
                    break;

                case VariableTypes.Int16:
                    writer.Write(value_int16);
                    break;

                case VariableTypes.Int32:
                    writer.Write(value_int32);
                    break;

                case VariableTypes.Enum:
                    writer.Write(value_enum);
                    break;

                case VariableTypes.Bool:
                    writer.WriteBool32(value_bool);
                    break;

                case VariableTypes.String:
                    writer.WriteStringRSDK_UTF16(value_string);
                    break;

                case VariableTypes.Vector2:
                    writer.Write(value_vector2.x);
                    writer.Write(value_vector2.y);
                    break;

                case VariableTypes.Float:
                    writer.Write(value_float);
                    break;

                case VariableTypes.Color:
                    value_color.Write(writer);
                    break;
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            switch (type)
            {
                case VariableTypes.UInt8:
                    return value_uint8.ToString();

                case VariableTypes.UInt16:
                    return value_uint16.ToString();

                case VariableTypes.UInt32:
                    return value_uint32.ToString();

                case VariableTypes.Int8:
                    return value_int8.ToString();

                case VariableTypes.Int16:
                    return value_int16.ToString();

                case VariableTypes.Int32:
                    return value_int32.ToString();

                case VariableTypes.Enum:
                    return value_enum.ToString();

                case VariableTypes.Bool:
                    return value_bool.ToString();

                case VariableTypes.String:
                    return value_string.ToString();

                case VariableTypes.Vector2:
                    return value_vector2.ToString();

                case VariableTypes.Float:
                    return value_float.ToString();

                case VariableTypes.Color:
                    return value_color.ToString();

                default:
                    return "Unhandled Type for ToString()";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is VariableValue)
            {
                VariableValue compareValue = (obj as VariableValue);
                switch (compareValue.type)
                {
                    case VariableTypes.UInt8:
                        return compareValue.ValueUInt8 == ValueUInt8;

                    case VariableTypes.UInt16:
                        return compareValue.ValueUInt16 == ValueUInt16;

                    case VariableTypes.UInt32:
                        return compareValue.ValueUInt32 == ValueUInt32;

                    case VariableTypes.Int8:
                        return compareValue.ValueInt8 == ValueInt8;

                    case VariableTypes.Int16:
                        return compareValue.ValueInt16 == ValueInt16;

                    case VariableTypes.Int32:
                        return compareValue.ValueInt32 == ValueInt32;

                    case VariableTypes.Enum:
                        return compareValue.ValueEnum == ValueEnum;

                    case VariableTypes.Bool:
                        return compareValue.ValueBool == ValueBool;

                    case VariableTypes.String:
                        return compareValue.ValueString == ValueString;

                    case VariableTypes.Vector2:
                        return compareValue.ValueVector2.Equals(ValueVector2);

                    case VariableTypes.Float:
                        return compareValue.ValueFloat == ValueFloat;

                    case VariableTypes.Color:
                        return compareValue.ValueColor.Equals(ValueColor);

                    default:
                        break;
                }
            }

            return false;
        }
        #endregion
    }
    #endregion

    #region Objects & Entities
    [Serializable]
    public class SceneObject
    {
        /// <summary>
        /// the name of this type of object
        /// </summary>
        public NameIdentifier name = new NameIdentifier("NewObject");
        /// <summary>
        /// the names and types of each editable variable for this object
        /// </summary>
        public List<VariableInfo> variables = new List<VariableInfo>();
        /// <summary>
        /// a list of entities using this type
        /// </summary>
        public List<SceneEntity> entities = new List<SceneEntity>();

        public SceneObject() { }


        public SceneObject(NameIdentifier name, List<VariableInfo> variables)
        {
            this.name = name;
            this.variables = variables;
        }

        public SceneObject(Reader reader, List<string> objectNames = null, List<string> variableNames = null)
        {
            Read(reader, objectNames, variableNames);
        }

        public void Read(Reader reader, List<string> objectNames = null, List<string> variableNames = null)
        {
            name = new NameIdentifier(reader);

            byte variableCount = reader.ReadByte();
            for (int v = 1; v < variableCount; ++v)
                variables.Add(new VariableInfo(reader, variableNames));

            ushort entityCount = reader.ReadUInt16();
            for (int e = 0; e < entityCount; ++e)
                entities.Add(new SceneEntity(reader, this));

            // if we have possible names, search em
            if (objectNames != null)
            {
                string hashString = name.HashString();
                foreach (string varName in objectNames)
                {
                    NameIdentifier currentName = new NameIdentifier(varName);
                    string currentHashedName = currentName.HashString();
                    if (currentHashedName == hashString)
                    {
                        name = currentName;
                        break;
                    }
                }
            }
        }

        public void Write(Writer writer)
        {
            name.Write(writer);

            writer.Write((byte)(variables.Count + 1));
            foreach (VariableInfo variable in variables)
                variable.Write(writer);

            writer.Write((ushort)entities.Count);
            foreach (SceneEntity entity in entities)
                entity.Write(writer);
        }
    }

    [Serializable]
    public class SceneEntity : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        /// <summary>
        /// the slot this entity is in the entity list
        /// </summary>
        public ushort slotID;
        /// <summary>
        /// the x position (shifted 16-bit format, so 1.0 == (1 << 16))
        /// </summary>
        public int xpos = 0;
        /// <summary>
        /// the y position (shifted 16-bit format, so 1.0 == (1 << 16))
        /// </summary>
        public int ypos = 0;
        /// <summary>
        /// The type of object the entity is, this is basically equivalent to type from older engine versions
        /// </summary>
        public SceneObject type = null;

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


        /// <summary>
        /// a list of all the editable variable values for this entity, this list should match the definitions in the parent object type
        /// </summary>
        public List<VariableValue> variables = new List<VariableValue>();

        public SceneEntity(SceneObject obj, ushort slotID)
        {
            type = obj;
            this.slotID = slotID;
            foreach (VariableInfo variable in type.variables)
                variables.Add(new VariableValue(variable.type));
        }

        public SceneEntity(Reader reader, SceneObject type)
        {
            Read(reader, type);
        }

        public void Read(Reader reader, SceneObject type)
        {
            this.type = type;
            slotID = reader.ReadUInt16();
            //a position, made of 8 bytes, 4 for X, 4 for Y. shifted by 16 bits (0x10000 == 1.0)
            xpos = reader.ReadInt32();
            ypos = reader.ReadInt32();

            foreach (VariableInfo variable in this.type.variables)
                variables.Add(new VariableValue(reader, variable.type));
        }

        public void Write(Writer writer)
        {
            writer.Write(slotID);

            writer.Write(xpos);
            writer.Write(ypos);

            foreach (VariableValue variable in variables)
                variable.Write(writer);
        }
    }
    #endregion

    #region Scene
    [Serializable]
    public class Scene
    {
        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'S', (byte)'C', (byte)'N', 0 };

        /// <summary>
        /// metadata stuff for RSDK Scene editor
        /// </summary>
        public SceneEditorMetadata editorMetadata = new SceneEditorMetadata();

        /// <summary>
        /// the layers in this scene
        /// </summary>
        public List<SceneLayer> layers = new List<SceneLayer>();
        /// <summary>
        /// the object types in this scene
        /// </summary>
        public List<SceneObject> objects = new List<SceneObject>();

        /// <summary>
        /// the max amount of entities that can be in a single stage.
        /// NOTE: filters are taken into account, you can have more than the max if they aren't all loaded in one scene (See Mania vs Encore layouts)
        /// </summary>
        public const int ENTITY_LIST_SIZE = 2048;

        public Scene() { }

        public Scene(string filename, List<string> objectNames = null, List<string> variableNames = null) : this(new Reader(filename), objectNames, variableNames) { }

        public Scene(System.IO.Stream stream, List<string> objectNames = null, List<string> variableNames = null) : this(new Reader(stream), objectNames, variableNames) { }

        public Scene(Reader reader, List<string> objectNames = null, List<string> variableNames = null)
        {
            Read(reader, objectNames, variableNames);
        }

        public void Read(Reader reader, List<string> objectNames = null, List<string> variableNames = null)
        {
            // Load scene
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid Scene v5 signature");
            }

            editorMetadata = new SceneEditorMetadata(reader);

            byte layerCount = reader.ReadByte();
            layers.Clear();
            for (int i = 0; i < layerCount; ++i)
                layers.Add(new SceneLayer(reader));

            byte objectCount = reader.ReadByte();
            objects.Clear();
            for (int i = 0; i < objectCount; ++i)
                objects.Add(new SceneObject(reader, objectNames, variableNames));

            reader.Close();
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
            if (layers.Count >= 8)
            {
                writer.Close();
                throw new Exception("Invalid Scene v5 File! Layer Count exceeds maximum of 8!");
            }

            // Header
            writer.Write(signature);

            // Editor
            editorMetadata.Write(writer);

            // Layers
            writer.Write((byte)layers.Count);
            foreach (SceneLayer layer in layers)
                layer.Write(writer);

            // Objects
            writer.Write((byte)objects.Count);
            foreach (SceneObject obj in objects)
                obj.Write(writer);

            writer.Close();
        }
    }
    #endregion
}
