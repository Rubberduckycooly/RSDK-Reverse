using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace RSDKvRS
{

    /* Full Map Layout */
    public class Scene
    {
        public string Title { get; set; }
        public ushort[][] MapLayout { get; set; }

        public byte Music; //This is usually Set to 0
        public byte Background; //This is usually Set to 1 in PC, 0 in DC

        public ushort PlayerXpos;
        public ushort PlayerYPos;

        public List<Object> objects = new List<Object>();

        public int width, height;

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
    public class MapLayout
    {

        public ushort[][] TileLayout { get; set; }

        public int width, height;

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
}
