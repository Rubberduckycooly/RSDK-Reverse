using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
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

        /* Values for the "Display Bytes" */
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer0 = 1; //Usually BG Layer
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer1 = 9; //Unknown
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
        /// stage width (in chunks)
        /// </summary>
        public ushort width;
        /// <summary>
        /// stage height (in chunks)
        /// </summary>
        public ushort height;

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
            Console.WriteLine(Title);
            byte[] buffer = new byte[5];

            ActiveLayer0 = reader.ReadByte();
            ActiveLayer1 = reader.ReadByte();
            ActiveLayer2 = reader.ReadByte();
            ActiveLayer3 = reader.ReadByte();
            Midpoint = reader.ReadByte();

            width = 0; height = 0;

            // Map width in 128 pixel units
            // In RSDKvB it's two bytes long, little-endian

            reader.Read(buffer, 0, 2); //Read Map Width
            width = (ushort)(buffer[0] + (buffer[1] << 8));

            reader.Read(buffer, 0, 2); //Read Height
            height = (ushort)(buffer[0] + (buffer[1] << 8));

            Console.WriteLine("Width " + width + " Height " + height);

            MapLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                MapLayout[i] = new ushort[width];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //128x128 Block number is 16-bit
                    //Little-Endian in RSDKv4	
                    reader.Read(buffer, 0, 2); //Read size
                    MapLayout[y][x] = (ushort)(buffer[0] + (buffer[1] << 8));
                }
            }

            // Read object data
            int ObjCount = 0;

            // 4 bytes, little-endian, unsigned
            byte t1 = reader.ReadByte();
            byte t2 = reader.ReadByte();

            ObjCount = (t2 << 8) + t1;

            Console.WriteLine("Object Count = " + ObjCount);

            Object.cur_id = 0;

            int n = 0;

            try
            {
                for (n = 0; n < ObjCount; n++)
                {
                    // Add object
                    objects.Add(new Object(reader));
                }
                //Console.WriteLine("Current Reader Position = " + reader.BaseStream.Position + " Current File Length = " + reader.BaseStream.Length + " Data Left = " + (reader.BaseStream.Length - reader.BaseStream.Position));
            }
            catch (Exception ex)
            {
                if (reader.IsEof)
                {
                    Console.WriteLine("Fuck, not the end! Objects Left: " + (ObjCount-n));
                    Console.WriteLine("Current Reader Position = " + reader.BaseStream.Position + " Current File Length = " + reader.BaseStream.Length + " Data Left = " + (reader.BaseStream.Length - reader.BaseStream.Position));
                    reader.Close();
                    return;
                }
            }

            Console.WriteLine("Current Reader Position = " + reader.BaseStream.Position + " Current File Length = " + reader.BaseStream.Length + " Data Left = " + (reader.BaseStream.Length - reader.BaseStream.Position));
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
            // Write zone name		
            writer.WriteRSDKString(Title);

            // Write the five "display" bytes
            writer.Write(ActiveLayer0);
            writer.Write(ActiveLayer1);
            writer.Write(ActiveLayer2);
            writer.Write(ActiveLayer3);
            writer.Write(Midpoint);

            // Write width
            writer.Write((byte)(this.width & 0xff));
            writer.Write((byte)(this.width >> 8));

            // Write height
            writer.Write((byte)(this.height & 0xff));
            writer.Write((byte)(this.height >> 8));

            // Write tilemap
            for (int h = 0; h < this.height; h++)
            {
                for (int w = 0; w < this.width; w++)
                {
                    writer.Write((byte)(this.MapLayout[h][w] & 0xff));
                    writer.Write((byte)(this.MapLayout[h][w] >> 8));
                }
            }

            // Write number of objects
            int num_of_obj = objects.Count;

            if (num_of_obj >= MaxObjectCount)
            {
                Console.WriteLine("Object Count > Max Objects!");
                return;
            }

            writer.Write((byte)(num_of_obj & 0xff));
            writer.Write((byte)((num_of_obj >> 8) & 0xff));
            writer.Write((byte)((num_of_obj >> 16) & 0xff));
            writer.Write((byte)((num_of_obj >> 24) & 0xff));

            // Write objects

            objects = objects.OrderBy(o => o.id).ToList();

            for (int n = 0; n < num_of_obj; n++)
            {
                Object obj = objects[n];

                obj.Write(writer);
            }

            writer.Close();

        }

    }
}
