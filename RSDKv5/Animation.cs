using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    // Thanks to Xeeynamo for the Animation information
    [Serializable]
    public class Animation
    {

        public const uint Sig = 0x00525053;
        public int FrameCount = 0;
        public List<string> SpriteSheets = new List<string>();
        public List<string> CollisionBoxes = new List<string>();
        public List<AnimationEntry> Animations = new List<AnimationEntry>();


        public void Load(BinaryReader reader)
        {
            if (reader.ReadUInt32() != Sig)
                throw new Exception("Invalid Signature!");
            FrameCount = reader.ReadInt32();
            int spriteSheetCount = reader.ReadByte();
            for (int i = 0; i < spriteSheetCount; ++i)
                SpriteSheets.Add(ReadString(reader));

            int collisionBoxCount = reader.ReadByte();
            for (int i = 0; i < collisionBoxCount; ++i)
                CollisionBoxes.Add(ReadString(reader));

            var animationCount = reader.ReadInt16();
            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new AnimationEntry().Load(reader, this));

        }

        public static string ReadString(BinaryReader reader)
        {
            byte length = reader.ReadByte();
            string result = Encoding.ASCII.GetString(reader.ReadBytes(length), 0, length - 1);
            return result;
        }

        public class AnimationEntry
        {
            public string AnimName;
            public List<Frame> Frames = new List<Frame>();
            public int FrameLoop;
            public int FrameSpeed;
            public byte Unknown;

            public AnimationEntry Load(BinaryReader reader, Animation anim)
            {
                AnimName = ReadString(reader);
                short frameCount = reader.ReadInt16();
                FrameSpeed = reader.ReadInt16();
                FrameLoop = reader.ReadByte();
                Unknown = reader.ReadByte();
                for (int i = 0; i < frameCount; ++i)
                {
                    Frames.Add(Frame.ReadFrame(reader, anim));
                }
                return this;
            }
        }

        public class Frame
        {

            public List<HitBox> HitBoxes = new List<HitBox>();
            public int SpriteSheet = 0;
            public int CollisionBox = 0;
            public int Duration = 0;
            public int ID = 0;
            public int X = 0;
            public int Y = 0;
            public int Width = 0;
            public int Height = 0;
            public int CenterX = 0;
            public int CenterY = 0;

            public static Frame ReadFrame(BinaryReader reader, Animation anim)
            {
                var frame = new Frame();
                frame.SpriteSheet = reader.ReadByte();
                frame.CollisionBox = 0;
                frame.Duration = reader.ReadInt16();
                frame.ID = reader.ReadInt16();
                frame.X = reader.ReadInt16();
                frame.Y = reader.ReadInt16();
                frame.Width = reader.ReadInt16();
                frame.Height = reader.ReadInt16();
                frame.CenterX = reader.ReadInt16();
                frame.CenterY = reader.ReadInt16();
                for (int i = 0; i <  anim.CollisionBoxes.Count; ++i)
                {
                    var hitBox = new HitBox();
                    hitBox.Left = reader.ReadInt16();
                    hitBox.Top = reader.ReadInt16();
                    hitBox.Right = reader.ReadInt16();
                    hitBox.Bottom = reader.ReadInt16();
                    frame.HitBoxes.Add(hitBox);
                }
                return frame;
            }

            public struct HitBox
            {
                public int Left, Right, Top, Bottom;
            }
        }
    }
}
