using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace RSDKv1
{

    /* Full Map Layout */
    public class Level
    {
        public string Title { get; set; }
        public ushort[][] MapLayout { get; set; }

        public byte Music; //This is usually Set to 0
        public byte Background; //This is usually Set to 1 in PC, 0 in DC

        public ushort PlayerXpos;
        public ushort PlayerYPos;

        public List<Object> objects = new List<Object>();

        public int width, height;

        public Level(string filename) : this(new Reader(filename))
        {

        }

        public Level(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Level(Reader reader)
        {
            // Separate path components			
            String dirname = Path.GetDirectoryName(reader.GetFilename());
            String basename = "\\" + Path.GetFileNameWithoutExtension(reader.GetFilename());

            String itmPath = dirname + basename + ".itm";

            Reader ITMreader = new Reader(itmPath);

            Title = ITMreader.ReadRSDKString();

            width = reader.ReadByte();
            height = reader.ReadByte();
            MapLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                MapLayout[i] = new ushort[width];
            }

            // Read map data from the map file
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MapLayout[y][x] = reader.ReadByte();
                }
            }

            Music = ITMreader.ReadByte();
            Background = ITMreader.ReadByte();
            PlayerXpos = (ushort)(ITMreader.ReadByte() << 8);
            PlayerXpos |= ITMreader.ReadByte();
            PlayerYPos = (ushort)(ITMreader.ReadByte() << 8);
            PlayerYPos |= ITMreader.ReadByte();

            // Read objects from the item file
            int ObjCount = ITMreader.ReadByte() << 8;
            ObjCount |= ITMreader.ReadByte();

            for (int i = 0; i < ObjCount; i++)
            {
                // Object type, 1 byte, unsigned
                int obj_type = ITMreader.ReadByte();
                // Object subtype, 1 byte, unsigned
                int obj_subtype = ITMreader.ReadByte();

                // X Position, 2 bytes, big-endian, signed			
                int obj_xPos = ITMreader.ReadSByte() << 8;
                obj_xPos |= ITMreader.ReadByte();

                // Y Position, 2 bytes, big-endian, signed
                int obj_yPos = ITMreader.ReadSByte() << 8;
                obj_yPos |= ITMreader.ReadByte();

                // Add object
                objects.Add(new Object(obj_type, obj_subtype, obj_xPos, obj_yPos));
            }
            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        internal void Write(Writer writer)
        {

            //Checks To make sure that the file can be saved correctly

            if (width >= 256)
                throw new Exception("Cannot save as Retro-Sonic map. Level width in tiles > 255.");

            if (height >= 256)
                throw new Exception("Cannot save as Retro-Sonic map. Level height in tiles > 255.");

            int num_of_objects = objects.Count;

            if (num_of_objects > 65535)
                throw new Exception("Cannot save as Retro-Sonic map. Number of objects > 65535");

            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                int obj_type = obj.getType();
                int obj_subtype = obj.getSubtype();
                int obj_xPos = obj.getXPos();
                int obj_yPos = obj.getYPos();

                if (obj_type > 255)
                    throw new Exception("Cannot save as Retro-Sonic map. Object type > 255");

                if (obj_subtype > 255)
                    throw new Exception("Cannot save as Retro-Sonic map. Object subtype > 255");

                if (obj_xPos < -32768 || obj_xPos > 32767)
                    throw new Exception("Cannot save as Retro-Sonic. Object X Position can't fit in 16-bits");

                if (obj_yPos < -32768 || obj_yPos > 32767)
                    throw new Exception("Cannot save as Retro-Sonic. Object Y Position can't fit in 16-bits");
            }

            // Separate path components			
            String dirname = Path.GetDirectoryName(writer.GetFilename());
            String basename = "\\" + Path.GetFileNameWithoutExtension(writer.GetFilename());

            String itmPath = dirname + basename + ".itm";

            // Create item file
            Writer ITMwriter = new Writer(itmPath);

            // Save width and height
            writer.Write((byte)width);
            writer.Write((byte)height);

            // Save map data
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    writer.Write((byte)MapLayout[y][x]);

            // Close map file
            writer.Close();

            // Save zone name
            ITMwriter.WriteRSDKString(Title);

            // Write the Stage Init Data
            ITMwriter.Write(Music);
            ITMwriter.Write(Background);
            ITMwriter.Write((byte)(PlayerXpos >> 8));
            ITMwriter.Write((byte)(PlayerXpos & 0xFF));
            ITMwriter.Write((byte)(PlayerYPos >> 8));
            ITMwriter.Write((byte)(PlayerYPos & 0xFF));

            // Write number of objects
            ITMwriter.Write((byte)(num_of_objects >> 8));
            ITMwriter.Write((byte)(num_of_objects & 0xFF));

            // Write object data
            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                int obj_type = obj.type;
                int obj_subtype = obj.subtype;
                int obj_xPos = obj.xPos;
                int obj_yPos = obj.yPos;

                ITMwriter.Write((byte)obj_type);
                ITMwriter.Write((byte)obj_subtype);

                ITMwriter.Write((byte)(obj_xPos >> 8));
                ITMwriter.Write((byte)(obj_xPos & 0xFF));

                ITMwriter.Write((byte)(obj_yPos >> 8));
                ITMwriter.Write((byte)(obj_yPos & 0xFF));
            }
            ITMwriter.Close();
        }

    }

    /* Tile Layout */
    public class Map
    {

        public ushort[][] MapLayout { get; set; }

        public int width, height;

        public Map(string filename) : this(new Reader(filename))
        {

        }

        public Map(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Map(Reader reader)
        {

            width = reader.ReadByte();
            height = reader.ReadByte();
            MapLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                MapLayout[i] = new ushort[width];
            }

            // Read map data from the map file
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MapLayout[y][x] = reader.ReadByte();
                }
            }
            reader.Close();
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
            // Save width and height
            writer.Write((byte)width);
            writer.Write((byte)height);

            // Save map data
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    writer.Write((byte)MapLayout[y][x]);
            writer.Close();
        }

    }

    /* Object Layout */
    public class itm
    {

        string Title;

        public byte Music; //This is usually Set to 0
        public byte Background; //This is usually Set to 1 in PC, 0 in DC

        public ushort PlayerXpos;
        public ushort PlayerYPos;

        List<Object> objects = new List<Object>();

        public itm(string filename) : this(new Reader(filename))
        {

        }

        public itm(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public itm(Reader reader)
        {

            Title = reader.ReadRSDKString();

            Music = reader.ReadByte();
            Background = reader.ReadByte();
            PlayerXpos = (ushort)(reader.ReadByte() << 8);
            PlayerXpos |= reader.ReadByte();
            PlayerYPos = (ushort)(reader.ReadByte() << 8);
            PlayerYPos |= reader.ReadByte();

            // Read objects from the item file
            int ObjCount = reader.ReadByte() << 8;
            ObjCount |= reader.ReadByte();
            for (int i = 0; i < ObjCount; i++)
            {
                // Object type, 1 byte, unsigned
                int obj_type = reader.ReadByte();
                // Object subtype, 1 byte, unsigned
                int obj_subtype = reader.ReadByte();

                // X Position, 2 bytes, big-endian, signed			
                int obj_xPos = reader.ReadSByte() << 8;
                obj_xPos |= reader.ReadByte();

                // Y Position, 2 bytes, big-endian, signed
                int obj_yPos = reader.ReadSByte() << 8;
                obj_yPos |= reader.ReadByte();

                // Add object
                objects.Add(new Object(obj_type, obj_subtype, obj_xPos, obj_yPos));
            }
            reader.Close();
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
            //Checks To make sure that the file can be saved correctly

            int num_of_objects = objects.Count;

            if (num_of_objects > 65535)
                throw new Exception("Cannot save as Retro-Sonic map. Number of objects > 65535");

            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                int obj_type = obj.getType();
                int obj_subtype = obj.getSubtype();
                int obj_xPos = obj.getXPos();
                int obj_yPos = obj.getYPos();

                if (obj_type > 255)
                    throw new Exception("Cannot save as Retro-Sonic map. Object type > 255");

                if (obj_subtype > 255)
                    throw new Exception("Cannot save as Retro-Sonic map. Object subtype > 255");

                if (obj_xPos < -32768 || obj_xPos > 32767)
                    throw new Exception("Cannot save as Retro-Sonic. Object X Position can't fit in 16-bits");

                if (obj_yPos < -32768 || obj_yPos > 32767)
                    throw new Exception("Cannot save as Retro-Sonic. Object Y Position can't fit in 16-bits");
            }

            // Save zone name
            writer.WriteRSDKString(Title);

            // Write the Stage Init Data
            writer.Write(Music);
            writer.Write(Background);
            writer.Write((byte)(PlayerXpos >> 8));
            writer.Write((byte)(PlayerXpos & 0xFF));
            writer.Write((byte)(PlayerYPos >> 8));
            writer.Write((byte)(PlayerYPos & 0xFF));

            // Write number of objects
            writer.Write((byte)(num_of_objects >> 8));
            writer.Write((byte)(num_of_objects & 0xFF));

            // Write object data
            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                int obj_type = obj.type;
                int obj_subtype = obj.subtype;
                int obj_xPos = obj.xPos;
                int obj_yPos = obj.yPos;

                writer.Write((byte)obj_type);
                writer.Write((byte)obj_subtype);

                writer.Write((byte)(obj_xPos >> 8));
                writer.Write((byte)(obj_xPos & 0xFF));

                writer.Write((byte)(obj_yPos >> 8));
                writer.Write((byte)(obj_yPos & 0xFF));
            }
            writer.Close();
        }
    }

    /* Background Layout */
    public class BGLayout
    {

        public class BGLayer
        {

            public class UnknownValues2
            {
                public byte Value1;
                public byte Value2;
                public byte Value3;
            }

            public ushort[][] MapLayout { get; set; }

            public int width, height = 0;
            public byte Deform; //1 for parallax, 2 for no parallax, 3 for "3D Sky", anything else for nothing
            public byte RelativeVSpeed;
            public byte ConstantVSpeed;

            public List<UnknownValues2> UnknownVals2 = new List<UnknownValues2>();

            public BGLayer(Reader reader)
            {
                width = reader.ReadByte();
                height = reader.ReadByte();
                Deform = reader.ReadByte();
                RelativeVSpeed = reader.ReadByte();
                ConstantVSpeed = reader.ReadByte();

                //Console.WriteLine("Width = " + width + " Height = " + height + " Unknown 1 = " + Unknown1 + " Unknown 2 = " + Unknown2 + " Unknown 3 = " + Unknown3);

                int j = 0;
                while (j < 1)
                {
                    UnknownValues2 u2 = new UnknownValues2();
                    u2.Value1 = reader.ReadByte();

                    if (u2.Value1 == 255)
                    {
                        u2.Value2 = reader.ReadByte();

                        if (u2.Value2 == 255)
                        {
                            j = 1;
                        }
                        else
                        {
                            u2.Value3 = reader.ReadByte();
                        }
                    }
                    else if (u2.Value1 != 255)
                    {
                        u2.Value3 = reader.ReadByte();
                    }
                    UnknownVals2.Add(u2);
                }
                Console.WriteLine(reader.Pos);
                MapLayout = new ushort[height][];
                for (int m = 0; m < height; m++)
                {
                    MapLayout[m] = new ushort[width];
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        MapLayout[y][x] = reader.ReadByte();
                        //Console.WriteLine("Map = " + MapLayout[y][x]);
                    }
                }
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)width);
                writer.Write((byte)height);
                writer.Write(Deform);
                writer.Write(RelativeVSpeed);
                writer.Write(ConstantVSpeed);

                Console.WriteLine(UnknownVals2.Count);

                for (int i = 0; i < UnknownVals2.Count; i++)
                {
                    writer.Write(UnknownVals2[i].Value1);
                    Console.WriteLine(UnknownVals2[i].Value1);

                    if (UnknownVals2[i].Value1 == 255)
                    {
                        writer.Write(UnknownVals2[i].Value2);
                        Console.WriteLine(UnknownVals2[i].Value2);

                        if (UnknownVals2[i].Value1 == 255 && UnknownVals2[i].Value2 == 255)
                        {
                            break;
                        }

                        if (UnknownVals2[i].Value2 != 255)
                        {
                            writer.Write(UnknownVals2[i].Value3);
                        }
                    }
                    else
                    {
                        writer.Write(UnknownVals2[i].Value3);
                    }
                }

                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        writer.Write((byte)(MapLayout[h][w]));
                    }
                }
            }

        }

        public class UnknownValues
        {
            public byte Value1;
            public byte Value2;
            public byte Value3;

            public UnknownValues(Reader reader)
            {
                Value1 = reader.ReadByte();
                Value2 = reader.ReadByte();
                Value3 = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(Value1);
                writer.Write(Value2);
                writer.Write(Value3);
            }

        }

        public byte layerCount;
        public byte LinesPerLayer;

        public byte Unknown1;

        public List<UnknownValues> UnknownVals = new List<UnknownValues>();

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

        public List<BGLayer> Layers = new List<BGLayer>();

        //NOTES:
        //Byte 1 are the number of layers
        //byte 2 is the amount of lines
        //then there are the line values
        //then IDK
        //

        public BGLayout(string filename) : this(new Reader(filename))
        {

        }

        public BGLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public BGLayout(Reader reader)
        {
            layerCount = reader.ReadByte();
            LinesPerLayer = reader.ReadByte();

            //First up is certainly the Parallax vallues
            for (int lc = 0; lc < LinesPerLayer; lc++)
            {
                Lines.Add(new ParallaxValues(reader));
            }

            Unknown1 = reader.ReadByte(); //Used for reading these unknown values 

            for (int i = 0; i < Unknown1; i++)
            {
                UnknownValues u = new UnknownValues(reader);
                UnknownVals.Add(u);
            }

            for (int i = 0; i < layerCount; i++) //Read BG Layers
            {
                Layers.Add(new BGLayer(reader));
            }

            reader.Close();
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
            writer.Write(layerCount);
            writer.Write((byte)Lines.Count);

            for (int lc = 0; lc < Lines.Count; lc++)
            {
                Lines[lc].Write(writer);
            }

            writer.Write(Unknown1);

            for (int i = 0; i < Unknown1; i++)
            {
                UnknownVals[i].Write(writer);
            }

            for (int i = 0; i < layerCount; i++) //Read BG Layers
            {
                Layers[i].Write(writer);
            }

            writer.Close();
        }

            public class ParallaxValues
            {
            public byte RHSpeed; //Known as "Speed" in Taxman's Editor
            public byte CHSpeed; //Known as "Scroll" in Taxman's Editor
            public byte Deform; //Known as "Deform" in Taxman's Editor, Same here!

            public ParallaxValues()
            {
                RHSpeed = 0;
                CHSpeed = 0;
                Deform = 0;
            }

            public ParallaxValues(Reader reader)
            {
                RHSpeed = reader.ReadByte();
                CHSpeed = reader.ReadByte();
                Deform = reader.ReadByte();
            }

                public void Write(Writer writer)
                {
                    writer.Write(RHSpeed);
                    writer.Write(CHSpeed);
                    writer.Write(Deform);
                }

            }
    }

    /* TileConfig Equivelent */
    public class tcf
    {
        public const int TILES_COUNT = 0x400; //1024

        public TileConfig[] Collision = new TileConfig[TILES_COUNT];

        public tcf(string filename, bool DCver) : this(new Reader(filename), DCver)
        {

        }

        public tcf(System.IO.Stream stream, bool DCver) : this(new Reader(stream), DCver)
        {

        }

        public tcf(Reader reader, bool DCver)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                Collision[i] = new TileConfig(reader, DCver);
            }
            reader.Close();
        }

        public void Write(string filename, bool DCver)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer, DCver);
        }

        public void Write(System.IO.Stream stream, bool DCver)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer, DCver);
        }

        internal void Write(Writer writer, bool DCver)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                Collision[i].Write(writer, DCver);
            }
            writer.Close();
        }

        public class TileConfig
        {

            //DC version doesnt have this unknown value
            public byte PCunknown; //if it's Value is FF it causes the player's collision & gravity to be disabled until jumping, 00 causes the player to be unstuck from the tile
            //May be a "Tile Stickiness Value"
            public int[] CollisionP1 = new int[16]; //Collision Values for Path A
            public int[] unknownP1 = new int[16];
            public int[] unknown2P1 = new int[16];
            public int[] unknown3P1 = new int[16];

            public int[] CollisionP2 = new int[16]; //Collision Values for Path A
            public int[] unknownP2 = new int[16];
            public int[] unknown2P2 = new int[16];
            public int[] unknown3P2 = new int[16];

            public TileConfig(string filename, bool DCver) : this(new Reader(filename), DCver)
            {
            }

            public TileConfig(System.IO.Stream stream, bool DCver) : this(new Reader(stream), DCver)
            {
            }

            public TileConfig(Reader reader, bool DCver)
            {
                if (DCver)
                {
                    //CollisionP1 = reader.ReadBytes(32);
                    //CollisionP1 = reader.ReadBytes(32); //First 16 bytes are for Collision Angle, Next 16 control how the tile behaves somehow (Path A)
                    //unknownP1 = reader.ReadBytes(32); //Unknown, Seems to be for path A
                    //CollisionP2 = reader.ReadBytes(32); //First 16 bytes are for Collision Angle, Next 16 control how the tile behaves somehow (Path B)
                    //unknownP2 = reader.ReadBytes(32); //Unknown, Seems to be for path B
                }
                else if (!DCver)
                {
                    PCunknown = reader.ReadByte(); // Single Byte
                    for (int i = 0; i < 16; i++)
                    {
                        CollisionP1[i] = reader.ReadByte();
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        unknownP1[i] = reader.ReadByte();
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        unknown2P1[i] = reader.ReadByte();
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        unknown3P1[i] = reader.ReadByte();
                    }



                    for (int i = 0; i < 16; i++)
                    {
                        CollisionP2[i] = reader.ReadByte();
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        unknownP2[i] = reader.ReadByte();
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        unknown2P2[i] = reader.ReadByte();
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        unknown3P2[i] = reader.ReadByte();
                    }
                }
            }

            public void Write(string filename, bool DCver)
            {
                using (Writer writer = new Writer(filename))
                    this.Write(writer,DCver);
            }

            public void Write(System.IO.Stream stream, bool DCver)
            {
                using (Writer writer = new Writer(stream))
                    this.Write(writer, DCver);
            }

            internal void Write(Writer writer, bool DCver)
            {
                if (DCver)
                {
                    //writer.Write(CollisionP1);
                    //writer.Write(unknownP1);
                    //writer.Write(CollisionP2);
                    //writer.Write(unknownP2);
                }
                else if (!DCver)
                {
                    writer.Write(PCunknown);
                    for (int i = 0; i < CollisionP1.Length; i++)
                    {
                        writer.Write((byte)CollisionP1[i]);
                    }
                    for (int i = 0; i < unknownP1.Length; i++)
                    {
                        writer.Write((byte)unknownP1[i]);
                    }
                    for (int i = 0; i < unknown2P1.Length; i++)
                    {
                        writer.Write((byte)unknown2P1[i]);
                    }
                    for (int i = 0; i < unknown3P1.Length; i++)
                    {
                        writer.Write((byte)unknown3P1[i]);
                    }



                    for (int i = 0; i < CollisionP2.Length; i++)
                    {
                        writer.Write((byte)CollisionP2[i]);
                    }
                    for (int i = 0; i < unknownP2.Length; i++)
                    {
                        writer.Write((byte)unknownP2[i]);
                    }
                    for (int i = 0; i < unknown2P2.Length; i++)
                    {
                        writer.Write((byte)unknown2P2[i]);
                    }
                    for (int i = 0; i < unknown3P2.Length; i++)
                    {
                        writer.Write((byte)unknown3P2[i]);
                    }

                }
            }
        }

    }

    /* 128x128Tiles Equivelent */
    public class til
    {

        public List<Tile128> BlockList { get; set; }

        public til(string filepath) : this(new Reader(filepath))
        {

        }

        public til(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public til(Reader strm)
        {
            BlockList = new List<Tile128>();
            byte[] mappingEntry = new byte[3];
            Tile128 currentBlock = new Tile128();
            int tileIndex = 0;
            while (strm.Read(mappingEntry, 0, mappingEntry.Length) > 0)
            {
                if (tileIndex >= currentBlock.Mapping.Length)
                {
                    tileIndex = 0;
                    BlockList.Add(currentBlock);
                    currentBlock = new Tile128();
                }
                mappingEntry[0] = (byte)(mappingEntry[0] - (mappingEntry[0] >> 6 << 6));
                currentBlock.Mapping[tileIndex].VisualPlane = (byte)(mappingEntry[0] >> 4);
                mappingEntry[0] = (byte)(mappingEntry[0] - (mappingEntry[0] >> 4 << 4));
                currentBlock.Mapping[tileIndex].Orientation = (byte)(mappingEntry[0] >> 2);
                mappingEntry[0] = (byte)(mappingEntry[0] - (mappingEntry[0] >> 2 << 2));
                currentBlock.Mapping[tileIndex].Tile16x16 = (ushort)((mappingEntry[0] << 8) + mappingEntry[1]);
                currentBlock.Mapping[tileIndex].CollisionFlag0 = (byte)(mappingEntry[2] >> 4);
                currentBlock.Mapping[tileIndex].CollisionFlag1 = (byte)(mappingEntry[2] - (mappingEntry[2] >> 4 << 4));
                tileIndex++;
            }
            if (tileIndex >= currentBlock.Mapping.Length)
            {
                tileIndex = 0;
                BlockList.Add(currentBlock);
                currentBlock = new Tile128();
            }
            strm.Close();
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
            int[] mappingEntry = new int[3];
            int tileIndex = 0;
            int chunkIndex = 0;

            while (chunkIndex < 256)
            {
                if (tileIndex >= BlockList[chunkIndex].Mapping.Length)
                {
                    tileIndex = 0;
                    chunkIndex++;
                }
                mappingEntry = new int[3];
                if (chunkIndex > 255) break;

                mappingEntry[0] |= (byte)(BlockList[chunkIndex].Mapping[tileIndex].Tile16x16 >> 8); //Put the first bit onto buffer[0]
                mappingEntry[0] = (byte)(mappingEntry[0] + (mappingEntry[0] >> 2 << 2));
                mappingEntry[0] |= (BlockList[chunkIndex].Mapping[tileIndex].Orientation) << 2; //Put the Flip of the tile two bits in
                mappingEntry[0] = (byte)(mappingEntry[0] + (mappingEntry[0] >> 4 << 4));
                mappingEntry[0] |= (BlockList[chunkIndex].Mapping[tileIndex].VisualPlane) << 4; //Put the Layer of the tile four bits in
                mappingEntry[0] = (byte)(mappingEntry[0] + (mappingEntry[0] >> 6 << 6));

                mappingEntry[1] = (byte)(BlockList[chunkIndex].Mapping[tileIndex].Tile16x16); //Put the rest of the Tile16x16 Value into this buffer

                mappingEntry[2] = BlockList[chunkIndex].Mapping[tileIndex].CollisionFlag1; //Colision Flag 1 is all bytes before bit 5
                mappingEntry[2] = mappingEntry[2] | BlockList[chunkIndex].Mapping[tileIndex].CollisionFlag0 << 4; //Colision Flag 0 is all bytes after bit 4

                writer.Write((byte)mappingEntry[0]);
                writer.Write((byte)mappingEntry[1]);
                writer.Write((byte)mappingEntry[2]);
                tileIndex++;
            }

            if (chunkIndex < 256 && tileIndex >= BlockList[chunkIndex].Mapping.Length)
            {
                tileIndex = 0;
                chunkIndex++;
            }
            writer.Close();
        }

        public Tile128 Clone(int ChunkID)
        {
            Tile128 Copy = new Tile128();
            for (int i = 0; i < 64; i++)
            {
                Copy.Mapping[i].VisualPlane = BlockList[ChunkID].Mapping[i].VisualPlane;
                Copy.Mapping[i].Orientation = BlockList[ChunkID].Mapping[i].Orientation;
                Copy.Mapping[i].Tile16x16 = BlockList[ChunkID].Mapping[i].Tile16x16;
                Copy.Mapping[i].CollisionFlag0 = BlockList[ChunkID].Mapping[i].CollisionFlag0;
                Copy.Mapping[i].CollisionFlag1 = BlockList[ChunkID].Mapping[i].CollisionFlag1;
            }
            return Copy;
        }

        public Bitmap RenderChunk(int ChunkID, Bitmap Tiles)
        {
            Bitmap chunk = new Bitmap(128, 128);
            chunk = BlockList[ChunkID].Render(Tiles);
            return chunk;
        }

        public class Tile128
        {
            public Tile16[] Mapping;
            public Tile128()
            {
                Mapping = new Tile16[8 * 8];
                for (int i = 0; i < Mapping.Length; i++)
                {
                    Mapping[i] = new Tile16();
                }
            }

            public Bitmap Render(Image tiles)
            {
                Bitmap retval = new Bitmap(128, 128);
                using (Graphics rg = Graphics.FromImage(retval))
                {
                    int i = 0;
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            Rectangle destRect = new Rectangle(x * 16, y * 16, 16, 16);
                            Rectangle srcRect = new Rectangle(0, Mapping[i].Tile16x16 * 16, 16, 16);
                            using (Bitmap tile = new Bitmap(16, 16))
                            {
                                using (Graphics tg = Graphics.FromImage(tile))
                                {
                                    tg.DrawImage(tiles, 0, 0, srcRect, GraphicsUnit.Pixel);
                                }
                                if (Mapping[i].Orientation == 1)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                }
                                else if (Mapping[i].Orientation == 2)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                }
                                else if (Mapping[i].Orientation == 3)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                }
                                rg.DrawImage(tile, destRect);
                            }
                            i++;
                        }
                    }
                }
                return retval;
            }

            public Bitmap Render(Image tiles, string outpath)
            {
                Bitmap retval = new Bitmap(128, 128);
                using (Graphics rg = Graphics.FromImage(retval))
                {
                    int i = 0;
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            Rectangle destRect = new Rectangle(x * 16, y * 16, 16, 16);
                            Rectangle srcRect = new Rectangle(0, Mapping[i].Tile16x16 * 16, 16, 16);
                            using (Bitmap tile = new Bitmap(16, 16))
                            {
                                using (Graphics tg = Graphics.FromImage(tile))
                                {
                                    tg.DrawImage(tiles, 0, 0, srcRect, GraphicsUnit.Pixel);
                                }
                                if (Mapping[i].Orientation == 1)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                }
                                else if (Mapping[i].Orientation == 2)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                }
                                else if (Mapping[i].Orientation == 3)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                }
                                rg.DrawImage(tile, destRect);
                            }
                            i++;
                        }
                        retval.Save(outpath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                return retval;
            }
        }

        public class Tile16
        {
            public byte VisualPlane { get; set; }
            public byte Orientation { get; set; }
            public ushort Tile16x16 { get; set; }
            public byte CollisionFlag0 { get; set; }
            public byte CollisionFlag1 { get; set; }
        }
    }

    /* StageConfig Equivelent */
    public class zcf
    {

        public byte[] Unknown;

        public Palette palette = new Palette();

        public List<WAVConfiguration> WAVs = new List<WAVConfiguration>();

        public List<WAVConfiguration> Music = new List<WAVConfiguration>();

        public List<string> ObjectsSheets = new List<string>();

        public List<string> ObjectsNames = new List<string>();

        public zcf(string filename) : this(new Reader(filename))
        {

        }

        public zcf(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public zcf(Reader reader)
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
            Unknown = new byte[objects_count];
            for (int i = 0; i < objects_count; ++i)
            { ObjectsNames.Add(reader.ReadRSDKString()); }
            for (int i = 0; i < objects_count; i++)
            {
                Unknown[i] = reader.ReadByte();
            }
        }

        internal void WriteObjectsNames(Writer writer)
        {
            writer.Write((byte)ObjectsNames.Count);
            foreach (string name in ObjectsNames)
            { writer.WriteRSDKString(name); }
            writer.Write(Unknown);
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
