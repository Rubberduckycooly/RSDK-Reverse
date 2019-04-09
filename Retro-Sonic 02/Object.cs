using System.IO;

namespace RetroSonicV2
{
    public class Object
    {

        public byte Type;
        public byte ChunkPosX;
        public byte PosOffsetX;
        public byte ChunkPosY;
        public byte PosOffsetY;

        public Object()
        {
            Type = 0;
            ChunkPosX = 0;
            PosOffsetX = 0;
            ChunkPosY = 0;
            PosOffsetY = 0;
        }

        public Object(BinaryReader reader)
        {
            Type = reader.ReadByte();
            ChunkPosX = reader.ReadByte();
            PosOffsetX = reader.ReadByte();
            ChunkPosY = reader.ReadByte();
            PosOffsetY = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            writer.Write(ChunkPosX);
            writer.Write(PosOffsetX);
            writer.Write(ChunkPosY);
            writer.Write(PosOffsetY);
        }

    }
}
