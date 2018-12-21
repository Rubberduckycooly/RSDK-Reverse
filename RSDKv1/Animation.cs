using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv1
{
    [Serializable]
    public class Animation : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public readonly string PathMod = "..\\sprites";

        //Why Taxman, why
        public byte[] Unknown = new byte[5];
        byte EndTexFlag;

        public List<string> SpriteSheets = new List<string>();

        public List<sprHitbox> CollisionBoxes = new List<sprHitbox>();
        public List<sprAnimation> Animations = new List<sprAnimation>();

        [Serializable]
        public class sprAnimation : ICloneable
        {
            public object Clone()
            {
                return this.MemberwiseClone();
            }
            [Serializable]
            public class sprFrame : ICloneable
            {
                public object Clone()
                {
                    return this.MemberwiseClone();
                }
                public struct HitBox
                {
                    public short Left, Right, Top, Bottom;
                }

                public List<HitBox> HitBoxes = new List<HitBox>();
                public byte SpriteSheet = 0;
                public byte CollisionBox = 0;
                public readonly short Delay = 256;
                public byte X = 0;
                public byte Y = 0;
                public byte Width = 0;
                public byte Height = 0;
                public SByte PivotX = 0;
                public SByte PivotY = 0;

                public sprFrame()
                {

                }

                public sprFrame(Reader reader)
                {
                    SpriteSheet = reader.ReadByte();
                    CollisionBox = reader.ReadByte();
                    X = reader.ReadByte();
                    Y = reader.ReadByte();
                    Width = reader.ReadByte();
                    Height = reader.ReadByte();
                    PivotX = reader.ReadSByte();
                    PivotY = reader.ReadSByte();
                }

                public void Write(Writer writer)
                {
                    writer.Write(SpriteSheet);
                    writer.Write(CollisionBox);
                    writer.Write(X);
                    writer.Write(Y);
                    writer.Write(Width);
                    writer.Write(Height);
                    writer.Write(PivotX);
                    writer.Write(PivotY);
                }

            }

            public List<sprFrame> Frames = new List<sprFrame>();
            public byte LoopIndex;
            public byte SpeedMultiplyer;

            public sprAnimation()
            {

            }

            public sprAnimation(Reader reader)
            {
                byte frameCount = reader.ReadByte();
                SpeedMultiplyer = reader.ReadByte();
                LoopIndex = reader.ReadByte();
                for (int i = 0; i < frameCount; ++i)
                {
                    Frames.Add(new sprFrame(reader));
                }
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)Frames.Count);
                writer.Write(SpeedMultiplyer);
                writer.Write(LoopIndex);

                for (int i = 0; i < Frames.Count; ++i)
                {
                    Frames[i].Write(writer);
                }
            }

            public void NewFrame()
            {
                Frames.Add(new sprFrame());
            }

            public void CloneFrame(int frame)
            {
                Frames.Add((sprFrame)Frames[frame].Clone());
            }

            public void DeleteFrame(int frame)
            {
                if (Frames.Count > 0)
                {
                    Frames.RemoveAt(frame);
                }
            }

        }

        [Serializable]
        public class sprHitbox
        {
            public SByte Left, Right, Top, Bottom;

            public sprHitbox()
            {

            }

            public sprHitbox(Reader reader)
            {
                Left = reader.ReadSByte();
                Top = reader.ReadSByte();
                Right = reader.ReadSByte();
                Bottom = reader.ReadSByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(Left);
                writer.Write(Top);
                writer.Write(Right);
                writer.Write(Bottom);
            }
        }

        public Animation()
        {

        }

        public Animation(Reader reader)
        {
            Unknown = reader.ReadBytes(5);

            int spriteSheetCount = 3; //always 3

            for (int i = 0; i < spriteSheetCount; ++i)
                SpriteSheets.Add(reader.ReadString());

            EndTexFlag = reader.ReadByte(); //Seems to tell the RSDK's reader when to stop reading textures???

            // Read number of animations
            var animationsCount = reader.ReadByte();

            var animationCount = reader.ReadByte();
            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new sprAnimation(reader));

            int collisionBoxCount = reader.ReadByte();
            for (int i = 0; i < collisionBoxCount; ++i)
                CollisionBoxes.Add(new sprHitbox(reader));

        }

        public void Write(Writer writer)
        {
            writer.Write(Unknown);

            for (int i = 0; i < SpriteSheets.Count; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i]);
            }

            writer.Write(EndTexFlag);

            writer.Write((byte)Animations.Count);
            for (int i = 0; i < Animations.Count; ++i)
            {
                Write(writer);
            }

            writer.Write((byte)CollisionBoxes.Count);
            for (int i = 0; i < CollisionBoxes.Count; ++i)
            {
                CollisionBoxes[i].Write(writer);
            }

        }

        public void NewAnimation()
        {
            sprAnimation a = new sprAnimation();
            Animations.Add(a);
        }

        public void CloneAnimation(int anim)
        {
            sprAnimation a = new sprAnimation();

            byte FrameAmount = (byte)Animations[anim].Frames.Count;
            a.LoopIndex = Animations[anim].LoopIndex;
            a.SpeedMultiplyer = Animations[anim].SpeedMultiplyer;

            a.Frames.Clear();

            for (int i = 0; i < FrameAmount; i++)
            {
                a.Frames.Add((sprAnimation.sprFrame)Animations[anim].Frames[i].Clone());
            }

            Animations.Add(a);
        }

        public void DeleteAnimation(int frame)
        {
            Animations.RemoveAt(frame);
        }

        public void DeleteEndAnimation()
        {
            Animations.RemoveAt(Animations.Count - 1);
        }

    }
}
