namespace RSDKv5
{
    public class SceneEditorMetadata
    {
        /// <summary>
        /// Unknown value, is set to 2/3/4 by default
        /// </summary>
        public byte UnknownByte; // 2/3/4
        /// <summary>
        /// Background colour 1
        /// </summary>
        public Color BackgroundColor1;
        /// <summary>
        /// Background colour 2
        /// </summary>
        public Color BackgroundColor2;
        /// <summary>
        /// A set of 7 bytes that (seemingly) always have the same value of "01010400010400"
        /// </summary>
        public byte[] UnknownBytes; // Const: 01010400010400
        /// <summary>
        /// The name of the stamps file
        /// </summary>
        public string StampName;
        /// <summary>
        /// Another unknown Value
        /// </summary>
        public byte UnknownByte2;

        public SceneEditorMetadata()
        {
            UnknownByte = 3;
            BackgroundColor1 = new Color(0xFF, 0, 0xFF);
            BackgroundColor2 = new Color(0, 0xFF, 0);
            UnknownBytes = new byte[] { 0x1, 0x1, 0x4, 0x0, 0x1, 0x4, 0x0 };
            StampName = "Stamps.bin";
        }

        internal SceneEditorMetadata(Reader reader)
        {
            UnknownByte = reader.ReadByte();
            BackgroundColor1 = new Color(reader);
            BackgroundColor2 = new Color(reader);
            UnknownBytes = reader.ReadBytes(7);
            StampName = reader.ReadRSDKString();
            UnknownByte2 = reader.ReadByte();
        }

        internal void Write(Writer writer)
        {
            writer.Write(UnknownByte);
            BackgroundColor1.Write(writer);
            BackgroundColor2.Write(writer);
            writer.Write(UnknownBytes);
            writer.WriteRSDKString(StampName);
            writer.Write(UnknownByte2);
        }
    }
}
