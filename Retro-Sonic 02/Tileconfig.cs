using System.IO;

namespace RetroSonicV2
{
    public class Tileconfig
    {
        public byte[] CollisionMasks;
        public Tileconfig(BinaryReader reader)
        {
            CollisionMasks = new byte[reader.BaseStream.Length];

            for (int i = 0; i < CollisionMasks.Length; i++)
            {
                CollisionMasks[i] = reader.ReadByte();
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(CollisionMasks);
        }

    }
}
