using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RSDKvRS
{

    /* Full Map Layout */
    public class Scene
    {
        /// <summary>
        /// the Stage Name (what the titlecard displays)
        /// </summary>
        public string Title = "Stage";

        /// <summary>
        /// the array of Chunk IDs for the stage
        /// </summary>
        public ushort[][] MapLayout;

        /// <summary>
        /// the starting Music ID for the stage
        /// </summary>
        public byte Music; //This is usually Set to 0
        /// <summary>
        /// the displayed Background layer?
        /// </summary>
        public byte Background; //This is usually Set to 1 in PC, 0 in DC

        /// <summary>
        /// player's Spawn Xpos
        /// </summary>
        public short PlayerXpos;
        /// <summary>
        /// player's Spawn Ypos
        /// </summary>
        public short PlayerYPos;

        /// <summary>
        /// the list of objects in the stage
        /// </summary>
        public List<Object> objects = new List<Object>();

        /// <summary>
        /// stage width (in chunks)
        /// </summary>
        public ushort width;
        /// <summary>
        /// stage height (in chunks)
        /// </summary>
        public ushort height;

        /// <summary>
        /// the starting X Boundary, it's always 0 though
        /// </summary>
        public int xBoundary1
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// the starting Y Boundary, it's always 0 though
        /// </summary>
        public int yBoundary1
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// the ending X Boundary, it's the value (in pixels) for the stage width
        /// </summary>
        public int xBoundary2
        {
            get
            {
                return width << 7;
            }
        }
        /// <summary>
        /// the ending Y Boundary, it's the value (in pixels) for the stage height
        /// </summary>
        public int yBoundary2
        {
            get
            {
                return height << 7;
            }
        }
        /// <summary>
        /// The water level for the stage, by default it will be below the stage, so it's kinda useless lol
        /// </summary>
        public int WaterLevel
        {
            get
            {
                return yBoundary2 + 128;
            }
        }

        /// <summary>
        /// the Max amount of objects that can be in a single stage
        /// </summary>
        public int MaxObjectCount
        {
            get
            {
                return 1100;
            }
        }

        public Scene()
        {
            MapLayout = new ushort[1][];
            MapLayout[0] = new ushort[1];
        }

        public Scene(string filename) : this(new Reader(filename))
        {

        }

        public Scene(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public Scene(Reader reader)
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
            PlayerXpos = (short)(ITMreader.ReadByte() << 8);
            PlayerXpos |= ITMreader.ReadByte();
            PlayerYPos = (short)(ITMreader.ReadByte() << 8);
            PlayerYPos |= ITMreader.ReadByte();

            // Read objects from the item file
            int ObjCount = ITMreader.ReadByte() << 8;
            ObjCount |= ITMreader.ReadByte();

            Object.cur_id = 0;

            for (int i = 0; i < ObjCount; i++)
            {
                // Add object
                objects.Add(new Object(ITMreader));
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

            if (num_of_objects > MaxObjectCount)
                throw new Exception("Cannot save as Retro-Sonic map. Number of objects above 1100!");

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

                obj.Write(ITMwriter);
            }
            ITMwriter.Close();
        }

    }

    /* Tile Layout */
    public class MapLayout
    {

        public ushort[][] TileLayout { get; set; }

        public int width, height;

        public int xBoundary1
        {
            get
            {
                return 0;
            }
        }
        public int yBoundary1
        {
            get
            {
                return 0;
            }
        }
        public int xBoundary2
        {
            get
            {
                return width << 7;
            }
        }
        public int yBoundary2
        {
            get
            {
                return height << 7;
            }
        }
        public int WaterLevel
        {
            get
            {
                return yBoundary2 + 128;
            }
        }

        public MapLayout(string filename) : this(new Reader(filename))
        {

        }

        public MapLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public MapLayout(Reader reader)
        {

            width = reader.ReadByte();
            height = reader.ReadByte();
            TileLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                TileLayout[i] = new ushort[width];
            }

            // Read map data from the map file
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    TileLayout[y][x] = reader.ReadByte();
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
                    writer.Write((byte)TileLayout[y][x]);
            writer.Close();
        }

    }

    /* Object Layout */
    public class ObjectLayout
    {

        string Title;

        public byte Music; //This is usually Set to 0
        public byte Background; //This is usually Set to 1 in PC, 0 in DC

        public ushort PlayerXpos;
        public ushort PlayerYPos;

        public int MaxObjectCount
        {
            get
            {
                return 1100;
            }
        }

        List<Object> objects = new List<Object>();

        public ObjectLayout(string filename) : this(new Reader(filename))
        {

        }

        public ObjectLayout(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public ObjectLayout(Reader reader)
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

            Object.cur_id = 0;

            for (int i = 0; i < ObjCount; i++)
            {
                // Add object
                objects.Add(new Object(reader));
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

            if (num_of_objects > MaxObjectCount)
                throw new Exception("Cannot save as Retro-Sonic map. Number of objects above 1100!");

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

            objects = objects.OrderBy(o => o.id).ToList();

            // Write object data
            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                obj.Write(writer);
            }
            writer.Close();
        }
    }
}
