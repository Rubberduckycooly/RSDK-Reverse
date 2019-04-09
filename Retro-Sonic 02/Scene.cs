using System.Collections.Generic;
using System.IO;

namespace RetroSonicV2
{
    public class Scene
    {

        public ushort[][] MapLayout;
        public byte[] UnknownMapBytes = new byte[4]; 
        public byte UnknownByte1 = 0xff;
        public Object[] Objects = new Object[300];

        public byte width
        {
            get
            {
                return 78;
            }
        }
        public byte height
        {
            get
            {
                return 10;
            }
        }

        public Scene(BinaryReader reader)
        {
            MapLayout = new ushort[height][];
            for (int i = 0; i < height; i++)
            {
                MapLayout[i] = new ushort[width];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //MapLayout[y][x] |= reader.ReadByte();
                    //MapLayout[y][x] = (ushort)(reader.ReadByte() << 8);
                    MapLayout[y][x] = reader.ReadUInt16();
                }
            }
            UnknownMapBytes = reader.ReadBytes(4);

            var fileStream = reader.BaseStream as FileStream;
            string itmname = fileStream.Name;

            itmname = itmname.Replace(Path.GetExtension(itmname), ".itm");
            reader.Close();
            reader = new BinaryReader(File.Open(itmname, FileMode.Open));

            for (int i = 0; i < 301; i++)
            {
                Objects[i] = new Object(reader);
            }
            if (reader.BaseStream.Length == 1506) UnknownByte1 = reader.ReadByte();
            reader.Close();
        }

        public void Write(BinaryWriter writer)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    writer.Write(MapLayout[y][x]);
                }
            }

            writer.Write(UnknownMapBytes);

            var fileStream = writer.BaseStream as FileStream;
            string itmname = fileStream.Name;

            itmname = itmname.Replace(Path.GetExtension(itmname), ".itm");
            writer.Close();
            writer = new BinaryWriter(File.Create(itmname));

            for (int i = 0; i < 301; i++)
            {
                Objects[i].Write(writer);
            }
            if (UnknownByte1 != 0xff)
            {
                writer.Write(UnknownByte1);
            }
            writer.Close();
        }

    }

    public class MapLayout
    {
        public ushort[][] mapLayout;
        public byte[] UnknownMapBytes = new byte[4];
        public MapLayout(BinaryReader reader)
        {
            mapLayout = new ushort[10][];
            for (int i = 0; i < 10; i++)
            {
                mapLayout[i] = new ushort[78];
            }

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 78; x++)
                {
                    mapLayout[y][x] = (ushort)(reader.ReadByte() << 8);
                    mapLayout[y][x] |= reader.ReadByte();
                }
            }
            UnknownMapBytes = reader.ReadBytes(4);
        }

    }

    public class ObjectLayout
    {
        public byte UnknownByte1;
        public List<Object> Objects = new List<Object>();

        public ObjectLayout(BinaryReader reader)
        {
            for (int i = 0; i < 301; i++)
            {
                Objects.Add(new Object(reader));
            }
            if (reader.BaseStream.Length == 1506) UnknownByte1 = reader.ReadByte();
        }

    }
}
