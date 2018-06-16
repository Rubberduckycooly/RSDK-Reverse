using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class SceneEditorMetadata
    {
        byte UnknownByte; // 2/3/4
        public Color BackgroundColor1;
        public Color BackgroundColor2;
        byte[] UnknownBytes; // Const: 01010400010400
        public string BinName;
        byte UnknownByte2;

        public SceneEditorMetadata()
        {
            UnknownByte = 3;
            BackgroundColor1 = Color.EMPTY;
            BackgroundColor2 = Color.EMPTY;
            UnknownBytes = new byte[] { 0x1, 0x1, 0x4, 0x0, 0x1, 0x4, 0x0 };
            UnknownByte2 = new byte();
            BinName = "Scene1.bin";
        }

        internal SceneEditorMetadata(Reader reader)
        {
            UnknownByte = reader.ReadByte();
            BackgroundColor1 = new Color(reader);
            BackgroundColor2 = new Color(reader);
            UnknownBytes = reader.ReadBytes(7);
            BinName = reader.ReadRSDKString();
            UnknownByte2 = reader.ReadByte();
        }

        internal void Write(Writer writer)
        {
            writer.Write(UnknownByte);
            BackgroundColor1.Write(writer);
            BackgroundColor2.Write(writer);
            writer.Write(UnknownBytes);
            writer.WriteRSDKString(BinName);
            writer.Write(UnknownByte2);
        }
    }
}
