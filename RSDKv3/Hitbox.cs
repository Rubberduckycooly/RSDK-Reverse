using System.IO;

namespace RSDKv3
{
    public class Hitbox : IHitbox
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public Hitbox() { }

        public Hitbox(BinaryReader reader)
        {
            Read(reader);
        }

        public void SaveChanges(BinaryWriter writer)
        {
            Write(writer);
        }

        public void Read(BinaryReader reader)
        {
            Left = reader.ReadSByte();
            Top = reader.ReadSByte();
            Right = reader.ReadSByte();
            Bottom = reader.ReadSByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((sbyte)Left);
            writer.Write((sbyte)Top);
            writer.Write((sbyte)Right);
            writer.Write((sbyte)Bottom);
        }

        public object Clone()
        {
            return new Hitbox()
            {
                Left = Left,
                Top = Top,
                Right = Right,
                Bottom = Bottom
            };
        }
    }
}
