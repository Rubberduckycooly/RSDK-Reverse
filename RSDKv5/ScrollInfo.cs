namespace RSDKv5
{
    public class ScrollInfo
    {
        short _unknownWord1;
        short _unknownWord2;
        byte _unknownByte1;
        byte _unknownByte2;

        public ScrollInfo(short word1 = 0x100, short word2 = 0, byte byte1 = 0, byte byte2 = 0)
        {
            _unknownWord1 = word1;
            _unknownWord2 = word2;

            _unknownByte1 = byte1;
            _unknownByte2 = byte2;
        }

        internal ScrollInfo(Reader reader)
        {
            _unknownWord1 = reader.ReadInt16();
            _unknownWord2 = reader.ReadInt16();

            _unknownByte1 = reader.ReadByte();
            _unknownByte2 = reader.ReadByte();
        }

        public short UnknownWord1 { get => _unknownWord1; set => _unknownWord1 = value; }
        public short UnknownWord2 { get => _unknownWord2; set => _unknownWord2 = value; }
        public byte UnknownByte1 { get => _unknownByte1; set => _unknownByte1 = value; }
        public byte UnknownByte2 { get => _unknownByte2; set => _unknownByte2 = value; }

        internal void Write(Writer writer)
        {
            writer.Write(_unknownWord1);
            writer.Write(_unknownWord2);

            writer.Write(_unknownByte1);
            writer.Write(_unknownByte2);
        }
    }
}
