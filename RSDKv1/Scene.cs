using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
{
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
        /// the Max amount of objects that can be in a single stage
        /// </summary>
        public int MaxObjectCount
        {
            get
            {
                return 1056;
            }
        }

        /* Values for the "Display Bytes" */
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer0 = 1; //Usually BG Layer
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer1 = 2; //Unknown
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer2 = 0; //Usually Foreground (Map) Layer
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer3 = 0; //Usually Foreground (Map) Layer
        /// <summary>
        /// The Midpoint Layer does ???
        /// </summary>
        public byte Midpoint = 3;

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
        /// the list of objects in the stage
        /// </summary>
        public List<Object> objects = new List<Object>();
        /// <summary>
        /// a list of names for each Object Type
        /// </summary>
        public List<string> objectTypeNames = new List<string>();

        /// <summary>
        /// stage width (in chunks)
        /// </summary>
        public ushort width;
        /// <summary>
        /// stage height (in chunks)
        /// </summary>
        public ushort height;

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
            Title = reader.ReadRSDKString();
            Console.WriteLine("Stage Name: " + Title);

            byte[] buffer = new byte[5];

            ActiveLayer0 = reader.ReadByte();
            ActiveLayer1 = reader.ReadByte();
            ActiveLayer2 = reader.ReadByte();
            ActiveLayer3 = reader.ReadByte();
            Midpoint = reader.ReadByte();

            reader.Read(buffer, 0, 2); //Read Width

            width = 0; height = 0;


            // Map width in 128 pixel units
            // In RSDKv1, it's one byte long
            width = buffer[0];
            height = buffer[1];

            MapLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                MapLayout[i] = new ushort[width];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 128x128 Block number is 16-bit
                    // Big-Endian in RSDKv1 and RSDKv2
                    reader.Read(buffer, 0, 2); //Read size
                    MapLayout[y][x] = (ushort)(buffer[1] + (buffer[0] << 8));
                }
            }


            // Read number of object types, Only RSDKv1 and RSDKv2 support this feature		
            int ObjTypeCount = reader.ReadByte();

            for (int n = 0; n < ObjTypeCount; n++)
            {
                string name = reader.ReadRSDKString();

                objectTypeNames.Add(name);
                Console.WriteLine(name);
            }

            // Read object data
            int ObjCount = 0;

            // 2 bytes, big-endian, unsigned
            ObjCount = reader.ReadByte() << 8;
            ObjCount |= reader.ReadByte();

            Object.cur_id = 0;

            for (int n = 0; n < ObjCount; n++)
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

            //Checks To Make Sure the Data Is Valid For Saving

            if (this.width > 255)
                throw new Exception("Cannot save as Type v1. Width in tiles > 255");

            if (this.height > 255)
                throw new Exception("Cannot save as Type v1. Height in tiles > 255");

            int num_of_objects = objects.Count;

            if (num_of_objects >= MaxObjectCount)
            {
                Console.WriteLine("Object Count > Max Objects!");
                return;
            }

            // Write zone name		
            writer.WriteRSDKString(Title);

            // Write the five "display" bytes
            writer.Write(ActiveLayer0);
            writer.Write(ActiveLayer1);
            writer.Write(ActiveLayer2);
            writer.Write(ActiveLayer3);
            writer.Write(Midpoint);

            // Write width and height
            writer.Write((byte)this.width);
            writer.Write((byte)this.height);

            // Write tile map

            for (int h = 0; h < this.height; h++)
            {
                for (int w = 0; w < this.width; w++)
                {
                    writer.Write((byte)(MapLayout[h][w] >> 8));
                    writer.Write((byte)(MapLayout[h][w] & 0xff));
                }
            }

            // Write number of object type names
            int num_of_objtype_names = this.objectTypeNames.Count;

            writer.Write((byte)(num_of_objtype_names));

            // Write object type names
            // Ignore first object type "Type zero", it is not stored.
            for (int n = 0; n < num_of_objtype_names; n++)
            {
                writer.WriteRSDKString(objectTypeNames[n]);
            }

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
