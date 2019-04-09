using System.IO;

namespace RetroSonicV2
{
    class tds
    {
        public byte[] ChunkData;

        public tds(BinaryReader reader)
        {
            ChunkData = new byte[reader.BaseStream.Length];
            for (int l = 0; l < ChunkData.Length; ++l)
            {
                ChunkData[l] = reader.ReadByte();
            }
        }

    }
}
