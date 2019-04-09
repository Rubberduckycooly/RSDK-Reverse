using System.IO;

namespace RetroSonicV2
{
    public class Backgrounds
    {

        public byte LayerCount;

        public Backgrounds(BinaryReader reader)
        {
            LayerCount = reader.ReadByte();

            for (int i = 0; i < LayerCount; i++)
            {
                byte LinePosCnt = reader.ReadByte();

                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();

                for (int t = 0; t < LinePosCnt; t++)
                {
                    reader.ReadByte();
                }
            }
        }

        public void Write(BinaryWriter writer)
        {

        }

    }
}
