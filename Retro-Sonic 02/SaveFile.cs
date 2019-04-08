using System.IO;

namespace RetroSonicV2
{
    public class SaveFile
    {

        public class Save
        {
            public byte ZoneID;
            public byte ChaosEmeralds;
            public byte Lives;

            public Save(BinaryReader reader)
            {
                ZoneID = reader.ReadByte();
                ChaosEmeralds = reader.ReadByte();
                Lives = reader.ReadByte();
            }

            public void Write(BinaryWriter writer)
            {
                writer.Write(ZoneID);
                writer.Write(ChaosEmeralds);
                writer.Write(Lives);
            }

        }

        public Save SonicSave;
        public Save MilesSave;

        public SaveFile(BinaryReader reader)
        {
            SonicSave = new Save(reader);
            MilesSave = new Save(reader);
        }

        public void Write(BinaryWriter writer)
        {
            SonicSave.Write(writer);
            MilesSave.Write(writer);
        }

    }
}
