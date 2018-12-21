using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
{
    [Serializable]
    public class Animation : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public readonly string PathMod = "";

        
        public byte Unknown = 0;
        public byte PlayerType = 0;

        public List<string> SpriteSheets = new List<string>();

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

                public byte SpriteSheet = 0;
                public byte[] CollisionBox = new byte[4];
                public readonly short Delay = 256;
                public byte X = 0;
                public byte Y = 0;
                public byte Width = 0;
                public byte Height = 0;
                public byte PivotX = 0;
                public byte PivotY = 0;

                public sprFrame()
                {

                }

                public sprFrame(Reader reader, Animation anim = null)
                {
                    X = reader.ReadByte();
                    Y = reader.ReadByte();
                    Width = reader.ReadByte();
                    Height = reader.ReadByte();
                    SpriteSheet = reader.ReadByte();

                    for (int k = 0; k < 4; k++)
                    CollisionBox[k] = reader.ReadByte();

                    byte[] PivotVals = new byte[2];

                    PivotVals[0] = reader.ReadByte();
                    PivotVals[1] = reader.ReadByte();

                    PivotX = (byte)(CollisionBox[2] - PivotVals[0]); //PivotVal[0] is the true Value, this calculation is just done so the animation looks right upon playback
                    PivotY = (byte)(CollisionBox[3] - PivotVals[1]); //PivotVal[1] is the true Value, this calculation is just done so the animation looks right upon playback
                }

                public void Write(Writer writer)
                {
                    writer.Write(X);
                    writer.Write(Y);
                    writer.Write(Width);
                    writer.Write(Height);
                    writer.Write(SpriteSheet);
                    for (int c = 0; c < 4; ++c)
                    {
                        writer.Write(CollisionBox[c]);
                    }
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

        public Animation()
        {

        }

        public Animation(Reader reader, bool DreamcastVer = false)
        {
            Unknown = reader.ReadByte();
            PlayerType = reader.ReadByte();

            int spriteSheetCount = 3;
            if (DreamcastVer) //The Dreamcast Demo of retro-sonic only had 2 spritesheets per animation...
            {
                spriteSheetCount = 2;
            }
            else //But the PC demo has 3 spritesheets per animation! so we set that here!
            {
                spriteSheetCount = 3;
            }

            var animationCount = reader.ReadByte();

            for (int i = 0; i < spriteSheetCount; ++i)
                SpriteSheets.Add(reader.ReadString());

            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new sprAnimation(reader));

        }

        public void Write(Writer writer)
        {
            writer.Write(Unknown);
            writer.Write(PlayerType);

            writer.Write((byte)Animations.Count);

            for (int i = 0; i < SpriteSheets.Count; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i]);
            }

            for (int i = 0; i < Animations.Count; ++i)
            {
                Write(writer);
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
