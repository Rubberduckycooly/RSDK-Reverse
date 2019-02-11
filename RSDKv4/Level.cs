using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
This Loader uses code from the programs: "Retro Engine Map Viewer" and TaxEd by -- and Nextvolume respectivley 
*/

namespace RSDKv4
{
    public class Level
    {
        public string Title { get; set; }
        public ushort[][] MapLayout { get; set; }

        /* Values for the "Display Bytes" */
        public byte ActiveLayer0 = 1; //Usually BG Layer
        public byte ActiveLayer1 = 9; //Unknown
        public byte ActiveLayer2 = 0; //Usually Foreground (Map) Layer
        public byte ActiveLayer3 = 0; //Usually Foreground (Map) Layer
        public byte Midpoint = 3;

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
            Title = reader.ReadRSDKString();
            Console.WriteLine(Title);
            byte[] buffer = new byte[5];

            ActiveLayer0 = reader.ReadByte();
            ActiveLayer1 = reader.ReadByte();
            ActiveLayer2 = reader.ReadByte();
            ActiveLayer3 = reader.ReadByte();
            Midpoint = reader.ReadByte();

            reader.Read(buffer, 0, 2); //Read Map Width

            width = 0; height = 0;

            // Map width in 128 pixel units
            // In Sonic 1 it's two bytes long, little-endian
            width = buffer[0] + (buffer[1] << 8);
            reader.Read(buffer, 0, 2); //Read Height
            height = buffer[0] + (buffer[1] << 8);

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
            //NOTE: Object data reading is wrong somehow,
            int ObjCount = 0;


            // 4 bytes, little-endian, unsigned
            ObjCount = reader.ReadByte();
            ObjCount |= reader.ReadByte() << 8;
            ObjCount |= reader.ReadByte() << 16;
            ObjCount |= reader.ReadByte() << 24;

            Console.WriteLine(ObjCount);

            ObjCount = ObjCount - 1;

            int obj_type = 0;
            int obj_subtype = 0;
            int obj_xPos = 0;
            int obj_yPos = 0;

            try
            {
            for (int n = 0; n < 0/*ObjCount*/; n++)
            {

                    // Object type, 1 byte, unsigned 
                    obj_type = reader.ReadByte();
                    obj_type|= reader.ReadByte() << 8;

                    // Object subtype, 1 byte, unsigned
                    obj_subtype = reader.ReadByte();
                    obj_subtype|= reader.ReadByte() << 8;

                    //Hm? What are these for?
                    //reader.ReadBytes(2);

                    // X Position, 4 bytes, little-endian, unsigned
                    obj_xPos = reader.ReadByte();
                    obj_xPos |= reader.ReadByte() << 8;
                    obj_xPos |= reader.ReadByte() << 16;
                    obj_xPos |= reader.ReadByte() << 24;

                    // Y Position, 4 bytes, little-endian, unsigned
                    obj_yPos = reader.ReadByte();
                    obj_yPos |= reader.ReadByte() << 8;
                    obj_yPos |= reader.ReadByte() << 16;
                    obj_yPos |= reader.ReadByte() << 24;


                    // Add object
                    //objects.Add(new Object(obj_type, obj_subtype, obj_xPos, obj_yPos));
                    //Console.WriteLine(reader.BaseStream.Position + " Object "+ n + " Obj Values: Type: " + obj_type + " Subtype: " + obj_subtype + " Xpos = " + obj_xPos + " Ypos = " + obj_yPos);
            }
            Console.WriteLine("Current Reader Position = " + reader.BaseStream.Position + " Current File Length = " + reader.BaseStream.Length);
            }
            catch(Exception ex)
            {
                if (reader.IsEof)
				throw ex;
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

            writer.Write((byte)(num_of_obj & 0xff));
            writer.Write((byte)((num_of_obj >> 8) & 0xff));
            writer.Write((byte)((num_of_obj >> 16) & 0xff));
            writer.Write((byte)((num_of_obj >> 24) & 0xff));

            // Write objects

            for (int n = 0; n < num_of_obj; n++)
            {
                Object obj = objects[n];
                int obj_type = obj.type;
                int obj_subtype = obj.subtype;
                int obj_xPos = obj.xPos;
                int obj_yPos = obj.yPos;

                // Most likely the type and subtypes are still one byte long in v2
                // and the two other bytes are for an empty field

                writer.Write((byte)(obj_type & 0xff));
                writer.Write(obj_type >> 8);

                writer.Write((byte)(obj_subtype & 0xff));
                writer.Write(obj_subtype >> 8);

                //writer.Write(0);
                //writer.Write(0);

                writer.Write((byte)(obj_xPos & 0xff));
                writer.Write((byte)((obj_xPos >> 8) & 0xff));
                writer.Write((byte)((obj_xPos >> 16) & 0xff));
                writer.Write((byte)((obj_xPos >> 24) & 0xff));

                writer.Write((byte)(obj_yPos & 0xff));
                writer.Write((byte)((obj_yPos >> 8) & 0xff));
                writer.Write((byte)((obj_yPos >> 16) & 0xff));
                writer.Write((byte)((obj_yPos >> 24) & 0xff));
            }

            writer.Close();

        }

    }
}
