using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
This Loader uses code from the programs: "Retro Engine Map Viewer" and TaxEd by -- and Nextvolume respectivley 
*/

namespace RSDKv2
{
    public class Scene
    {
        public string Title { get; set; }
        public ushort[][] MapLayout { get; set; }

        /* Values for the "Display Bytes" */
        public byte ActiveLayer0 = 1; //Usually BG Layer
        public byte ActiveLayer1 = 9; //Unknown
        public byte ActiveLayer2 = 0; //Usually Foreground (Map) Layer
        public byte ActiveLayer3 = 0; //Usually Foreground (Map) Layer
        public byte Midpoint = 3;

        //Byte 5: Stage.MidPoint
        //if it's 0 then nothing but the objects are drawn
        //if its 1 or 2 the tiles on high layer are drawn on the low layer
        // 3 is default
        // 4 or above draws tiles that are on the low layer on the high layer

        public List<Object> objects = new List<Object>();
        public List<string> objectTypeNames = new List<string>();

        public int width, height;

        public Scene()
        {

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
            //Console.WriteLine(Title);
            byte[] buffer = new byte[5];

            ActiveLayer0 = reader.ReadByte();
            ActiveLayer1 = reader.ReadByte();
            ActiveLayer2 = reader.ReadByte();
            ActiveLayer3 = reader.ReadByte();
            Midpoint = reader.ReadByte();

            reader.Read(buffer, 0, 2); //Read size

            width = 0; height = 0;


            // Map width in 128 pixel units
            // In RSDKv2, it's one byte long
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
                    // Big-Endian in RSDKv2 and RSDKv3
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
            }
            // Read object data

            int ObjCount = 0;

            // 2 bytes, big-endian, unsigned
            ObjCount = reader.ReadByte() << 8;
            ObjCount |= reader.ReadByte();

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
                Write(writer);
        }

        internal void Write(Writer writer)
        {
            //Checks To Make Sure the Data Is Valid For Saving

            if (width > 255)
                throw new Exception("Cannot save as Type v2. Width in tiles > 255");

            if (height > 255)
                throw new Exception("Cannot save as Type v2. Height in tiles > 255");

            int num_of_objects = objects.Count;

            if (num_of_objects > 65535)
                throw new Exception("Cannot save as Type v2. Number of objects > 65535");

            // Write zone name		
            writer.WriteRSDKString(Title);

            // Write the five "display" bytes
            writer.Write(ActiveLayer0);
            writer.Write(ActiveLayer1);
            writer.Write(ActiveLayer2);
            writer.Write(ActiveLayer3);
            writer.Write(Midpoint);

            // Write width and height
            writer.Write((byte)width);
            writer.Write((byte)height);

            // Write tile map

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    writer.Write((byte)(MapLayout[h][w] >> 8));
                    writer.Write((byte)(MapLayout[h][w] & 0xff));
                }
            }

            // Write number of object type names
            int num_of_objtype_names = objectTypeNames.Count;

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
