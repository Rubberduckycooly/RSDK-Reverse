using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
{
    public class Scene
    {
        public string Title { get; set; }
        public ushort[][] MapLayout { get; set; }

        /* Values for the "Display Bytes" */
        public byte ActiveLayer0 = 1; //Usually BG Layer
        public byte ActiveLayer1 = 2; //Unknown
        public byte ActiveLayer2 = 0; //Usually Foreground (Map) Layer
        public byte ActiveLayer3 = 0; //Usually Foreground (Map) Layer
        public byte Midpoint = 3;

        public List<Object> objects = new List<Object>();
        public List<string> objectTypeNames = new List<string>();

        public int width, height;

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


            // Read number of object types, Only RSDKv2 and RSDKv3 support this feature		
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

            int obj_type = 0;
            int obj_subtype = 0;
            int obj_xPos = 0;
            int obj_yPos = 0;

            for (int n = 0; n < ObjCount; n++)
            {
                // Object type, 1 byte, unsigned
                obj_type = reader.ReadByte();
                // Object subtype, 1 byte, unsigned
                obj_subtype = reader.ReadByte();

                // X Position, 2 bytes, big-endian, signed			
                obj_xPos = reader.ReadSByte() << 8;
                obj_xPos |= reader.ReadByte();

                // Y Position, 2 bytes, big-endian, signed
                obj_yPos = reader.ReadSByte() << 8;
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

            //Checks To Make Sure the Data Is Valid For Saving

            if (this.width > 255)
                throw new Exception("Cannot save as Type v1. Width in tiles > 255");

            if (this.height > 255)
                throw new Exception("Cannot save as Type v1. Height in tiles > 255");

            int num_of_objects = objects.Count;

            if (num_of_objects > 65535)
                throw new Exception("Cannot save as Type v1. Number of objects > 65535");

            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                int obj_type = obj.getType();
                int obj_subtype = obj.getSubtype();
                int obj_xPos = obj.getXPos();
                int obj_yPos = obj.getYPos();

                if (obj_type > 255)
                    throw new Exception("Cannot save as Type v1. Object type > 255");

                if (obj_subtype > 255)
                    throw new Exception("Cannot save as Type v1. Object subtype > 255");

                if (obj_xPos < -32768 || obj_xPos > 32767)
                    throw new Exception("Cannot save as Type v1. Object X Position can't fit in 16-bits");

                if (obj_yPos < -32768 || obj_yPos > 32767)
                    throw new Exception("Cannot save as Type v1. Object Y Position can't fit in 16-bits");
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

            // Write object data
            for (int n = 0; n < num_of_objects; n++)
            {
                Object obj = objects[n];

                int obj_type = obj.type;
                int obj_subtype = obj.subtype;
                int obj_xPos = obj.xPos;
                int obj_yPos = obj.yPos;

                writer.Write((byte)(obj_type));
                writer.Write((byte)(obj_subtype));

                writer.Write((byte)(obj_xPos >> 8));
                writer.Write((byte)(obj_xPos & 0xFF));

                writer.Write((byte)(obj_yPos >> 8));
                writer.Write((byte)(obj_yPos & 0xFF));
            }
            writer.Close();
        }

    }
}
