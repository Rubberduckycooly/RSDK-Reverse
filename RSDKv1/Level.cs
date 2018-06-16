using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv1
{

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
            Console.WriteLine("Object Count = " + ObjCount + " Also the reader pos = " + reader.Pos);
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

    }

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
    }

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
    }

    public class til
    {
        public til(string filename) : this(new Reader(filename))
        {

        }

        public til(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public til(Reader reader)
        {

        }
    }

    public class zcf
    {
        public zcf(string filename) : this(new Reader(filename))
        {

        }

        public zcf(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public zcf(Reader reader)
        {

        }
    }
}
