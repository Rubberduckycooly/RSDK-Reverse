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

        byte[] SixBytes_RS; //These Bytes' functions are unknown, so we just save them here so we can write them back

        List<Object> objects = new List<Object>();

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
            Console.WriteLine("Width " + width + " Height " + height);
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
                    Console.WriteLine(MapLayout[y][x]);
                }
            }

            SixBytes_RS = new byte[6];
            ITMreader.Read(SixBytes_RS,0, 6);

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
                Console.WriteLine(i + " Obj Values: Type: " + obj_type + ", Subtype: " + obj_subtype + ", Xpos = " + obj_xPos + ", Ypos = " + obj_yPos);
                objects.Add(new Object(obj_type, obj_subtype, obj_xPos, obj_yPos));
            }


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

            // Write the six bytes we kept
            ITMwriter.Write(SixBytes_RS);

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
            Console.WriteLine("Width " + width + " Height " + height);
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
                    Console.WriteLine(MapLayout[y][x]);
                }
            }

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
        }

    }

    /* Object Layout */
    public class itm
    {

        string Title;

        byte[] SixBytes_RS;

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

            SixBytes_RS = new byte[6];
            reader.Read(SixBytes_RS, 0, 6);

            // Read objects from the item file
            int ObjCount = reader.ReadByte() << 8;
            ObjCount |= reader.ReadByte();
            Console.WriteLine("Object Count = " + ObjCount + " Also the reader pos = " + reader.Pos);
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
                Console.WriteLine(i + " Obj Values: Type: " + obj_type + ", Subtype: " + obj_subtype + ", Xpos = " + obj_xPos + ", Ypos = " + obj_yPos);
                objects.Add(new Object(obj_type, obj_subtype, obj_xPos, obj_yPos));
            }
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

        }
    }

    /* Background Layout */
    public class BGLayout
    {

        int width, height = 0;

        public ushort[][] MapLayout { get; set; }

        List<LineLayer> LineLayers = new List<LineLayer>();

        byte layerCount;

        List<byte> Unknown = new List<byte>();

        //NOTES:
        //Byte 1 are the number of layers
        //byte 2 is the amount of lines
        //then there seem to be line values
        //then IDK

        public BGLayout(string filename) : this(new Reader(filename))
        {
            
        }

        public BGLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public BGLayout(Reader reader)
        {
            layerCount = reader.ReadByte();

            for (int lc = 0; lc < layerCount + 1; lc++)
            {
                LineLayer l = new LineLayer(reader);
                LineLayers.Add(l);
            }

            while(!reader.IsEof)
            {
                Unknown.Add(reader.ReadByte());
            }

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
            //writer.Write(layerCount);
            //writer.Write((byte)Lines.Count);

            //for (int i = 0; i < Lines.Count; i++)
            //{
                //Lines[i].Write(writer);
            //}

            for (int i = 0; i < Unknown.Count; i++)
            {
                writer.Write(Unknown[i]);
            }

        }

        public class LineLayer
        {

        public List<ParallaxValues> Lines = new List<ParallaxValues>();

            public LineLayer(Reader reader)
            {
                byte LineCount = reader.ReadByte();
                for (int i = 0; i < LineCount; i++)
                {
                    ParallaxValues p = new ParallaxValues(reader);
                    Lines.Add(p);
                }
            }

        public class ParallaxValues
        {
            byte LineNo;
            byte OverallSpeed;
            byte Value2;

            public ParallaxValues(Reader reader)
            {
                LineNo = reader.ReadByte();
                OverallSpeed = reader.ReadByte();
                Value2 = reader.ReadByte();
                Console.WriteLine(LineNo + " " + OverallSpeed + " " + Value2);
            }

            public void Write(Writer writer)
            {
                writer.Write(LineNo);
                writer.Write(OverallSpeed);
                writer.Write(Value2);
            }

        }
        }



    }

    /* TileConfig Equivelent */
    public class tcf
    {
        public tcf(string filename) : this(new Reader(filename))
        {

        }

        public tcf(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public tcf(Reader reader)
        {

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

            //for (int i = 0; i < BlockList.Count; i++)
            //{
                //BlockList[i].Render(new Bitmap("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EHZ\\Zone.png"), "C:\\Users\\owner\\Downloads\\RS-EHZ Chunks\\" + i + ".png");
            //}

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

        public byte[] Unknown = new byte[26];

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

            Unknown = reader.ReadBytes(26); //unknown values

            this.ReadWAVConfiguration(reader);

            this.ReadOGGConfiguration(reader);
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

                for (int i = 0; i < objects_count; ++i)
                { ObjectsNames.Add(reader.ReadRSDKString()); }
            }

            internal void WriteObjectsNames(Writer writer)
            {
                writer.Write((byte)ObjectsNames.Count);
                foreach (string name in ObjectsNames)
                    writer.WriteRSDKString(name);
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

            writer.Write(Unknown); //unknown values

            WriteWAVConfiguration(writer);

            WriteOGGConfiguration(writer);

            //writer.Write(Unknown);

                //WriteObjectsNames(writer);

                //WriteWAVConfiguration(writer);
            }

    }
}
