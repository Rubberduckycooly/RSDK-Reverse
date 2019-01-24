namespace RSDKv5
{
    public class ScrollInfo
    {
        short _relativeSpeed;
        short _constantspeed;
        byte _behaviour;
        byte _drawOrder;

        /// <summary>
        /// the Speed of the scroll info when the player is moving
        /// </summary>
        public short RelativeSpeed { get => _relativeSpeed; set => _relativeSpeed = value; }
        /// <summary>
        /// the Speed of the scroll info when the player is't moving
        /// </summary>
        public short ConstantSpeed { get => _constantspeed; set => _constantspeed = value; }
        /// <summary>
        /// a special byte that tells the game if this scroll info has any special properties
        /// </summary>
        public byte Behaviour { get => _behaviour; set => _behaviour = value; }
        /// <summary>
        /// what drawlayer this scroll info is on (maybe?)
        /// </summary>
        public byte DrawOrder { get => _drawOrder; set => _drawOrder = value; }

        public ScrollInfo(short rSpeed = 0x100, short cSpeed = 0, byte behav = 0, byte dlayer = 0)
        {
            _relativeSpeed = rSpeed;
            _constantspeed = cSpeed;

            _behaviour = behav;
            _drawOrder = dlayer;
        }

        public ScrollInfo(Reader reader)
        {
            _relativeSpeed = reader.ReadInt16();
            _constantspeed = reader.ReadInt16();

            _behaviour = reader.ReadByte();
            _drawOrder = reader.ReadByte();
        }

        public void Write(Writer writer)
        {
            writer.Write(_relativeSpeed);
            writer.Write(_constantspeed);

            writer.Write(_behaviour);
            writer.Write(_drawOrder);
        }
    }
}
