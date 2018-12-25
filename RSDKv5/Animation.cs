using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    [Serializable]
    public class Animation : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static readonly byte[] MAGIC = new byte[] { (byte)'S', (byte)'P', (byte)'R', (byte)'\0' };

        public string PathMod
        {
            get
            {
                return "..//";
            }
        }

        //Why Taxman, why
        public int TotalFrameCount = 0;

        public List<string> SpriteSheets = new List<string>();
        public List<string> CollisionBoxes = new List<string>();

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
                public short Delay = 0;
                public short ID = 0;
                public short X = 0;
                public short Y = 0;
                public short Width = 0;
                public short Height = 0;
                public short PivotX = 0;
                public short PivotY = 0;

                public sprFrame()
                {

                }

                public sprFrame(Reader reader, Animation anim = null)
                {
                    SpriteSheet = reader.ReadByte();
                    CollisionBox = 0;
                    Delay = reader.ReadInt16();
                    ID = reader.ReadInt16();
                    X = reader.ReadInt16();
                    Y = reader.ReadInt16();
                    Width = reader.ReadInt16();
                    Height = reader.ReadInt16();
                    PivotX = reader.ReadInt16();
                    PivotY = reader.ReadInt16();

                    for (int i = 0; i < anim.CollisionBoxes.Count; ++i)
                    {
                        var hitBox = new HitBox();
                        hitBox.Left = reader.ReadInt16();
                        hitBox.Top = reader.ReadInt16();
                        hitBox.Right = reader.ReadInt16();
                        hitBox.Bottom = reader.ReadInt16();
                        HitBoxes.Add(hitBox);
                    }
                }

                public void Write(Writer writer)
                {
                    writer.Write(SpriteSheet);
                    writer.Write(Delay);
                    writer.Write(ID);
                    writer.Write(X);
                    writer.Write(Y);
                    writer.Write(Width);
                    writer.Write(Height);
                    writer.Write(PivotX);
                    writer.Write(PivotY);
                    for (int c = 0; c < HitBoxes.Count; ++c)
                    {
                        writer.Write(HitBoxes[c].Left);
                        writer.Write(HitBoxes[c].Top);
                        writer.Write(HitBoxes[c].Right);
                        writer.Write(HitBoxes[c].Bottom);
                    }
                }

            }

            public string AnimName;
            public List<sprFrame> Frames = new List<sprFrame>();
            public byte LoopIndex;
            public short SpeedMultiplyer;
            public byte RotationFlags;

            public sprAnimation()
            {

            }

            public sprAnimation(Reader reader, Animation anim = null)
            {
                AnimName = reader.ReadString();
                short frameCount = reader.ReadInt16();
                SpeedMultiplyer = reader.ReadInt16();
                LoopIndex = reader.ReadByte();
                RotationFlags = reader.ReadByte();
                for (int i = 0; i < frameCount; ++i)
                {
                    Frames.Add(new sprFrame(reader,anim));
                }
            }

            public void Write(Writer writer)
            {
                writer.Write(AnimName);
                writer.Write((ushort)Frames.Count);
                writer.Write(SpeedMultiplyer);
                writer.Write(LoopIndex);
                writer.Write(RotationFlags);
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

        public Animation(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid config file header magic");

            TotalFrameCount = reader.ReadInt32();

            int spriteSheetCount = reader.ReadByte();
            for (int i = 0; i < spriteSheetCount; ++i)
                SpriteSheets.Add(reader.ReadString());

            int collisionBoxCount = reader.ReadByte();
            for (int i = 0; i < collisionBoxCount; ++i)
                CollisionBoxes.Add(reader.ReadString());

            var animationCount = reader.ReadInt16();
            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new sprAnimation(reader, this));
            reader.Close();
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);
            writer.Write(TotalFrameCount);

            writer.Write((byte)SpriteSheets.Count);
            for (int i = 0; i < SpriteSheets.Count; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i]);
            }

            writer.Write((byte)CollisionBoxes.Count);
            for (int i = 0; i < CollisionBoxes.Count; ++i)
            {
                writer.WriteRSDKString(CollisionBoxes[i]);
            }

            writer.Write((ushort)Animations.Count);
            for (int i = 0; i < Animations.Count; ++i)
            {
                Animations[i].Write(writer);
            }
            writer.Close();
        }

        public void NewAnimation()
        {
            sprAnimation a = new sprAnimation();
            Animations.Add(a);
        }

        public void CloneAnimation(int anim)
        {
            sprAnimation a = new sprAnimation();

            a.AnimName = Animations[anim].AnimName;
            byte FrameAmount = (byte)Animations[anim].Frames.Count;
            a.LoopIndex = Animations[anim].LoopIndex;
            a.SpeedMultiplyer = Animations[anim].SpeedMultiplyer;
            a.RotationFlags = Animations[anim].RotationFlags;

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

        public int GetAnimByName(string name)
        {
            for (int i = 0; i < Animations.Count; i++)
            {
                if (Animations[i].AnimName == name)
                {
                    return i;
                }
            }
            Console.WriteLine("An anim with that name didn't exist! Name = " + name);
            return -1;
        }

        public void DeleteEndAnimation()
        {
            Animations.RemoveAt(Animations.Count - 1);
        }

    }
}
