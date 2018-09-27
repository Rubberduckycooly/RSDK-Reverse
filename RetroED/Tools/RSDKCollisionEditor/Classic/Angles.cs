using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RetroED.Tools.RSDKCollisionEditor.Classic
{
    class Angles
    {

        public byte[] angles = new byte[256];

        public Angles(BinaryReader reader)
        {
            for (int i = 0; i < 256; i++)
            {
                angles[i] = reader.ReadByte();
            }
            reader.Close();
        }

    }
}
