using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RetroED.Tools.RSDKCollisionEditor.Classic
{
    class StageCollisions
    {
        public byte[] PathA = new byte[768];
        public byte[] PathB = new byte[768];

        public StageCollisions(BinaryReader reader)
        {
            for (int i = 0; i < 768; i++)
            {
                reader.ReadByte();
                PathA[i] = reader.ReadByte();
            }
            for (int i = 0; i < 768; i++)
            {
                reader.ReadByte();
                PathB[i] = reader.ReadByte();
            }
            reader.Close();
        }

        public void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 768; i++)
            {
                writer.Write(PathA[i]);
                writer.Write(PathB[i]);
            }
            writer.Close();
        }

    }
}
