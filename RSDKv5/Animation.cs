using System;
using System.Collections.Generic;
using System.Linq;

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
        public int TotalFrameCount
        {
            get
            {
                return Animations.Take(Animations.Count).Sum(x => x.Frames.Count);
            }
        }

        public List<string> SpriteSheets = new List<string>();
        public List<string> CollisionBoxes = new List<string>();

        public List<AnimationEntry> Animations = new List<AnimationEntry>();

        [Serializable]
        public class AnimationEntry : ICloneable
        {
            public object Clone()
            {
                return this.MemberwiseClone();
            }
            [Serializable]
            public class Frame : ICloneable
            {
                public object Clone()
                {
                    return this.MemberwiseClone();
                }
                public struct HitBox
                {
                    /// <summary>
                    /// the Xpos of the hitbox
                    /// </summary>
                    public short Left;
                    /// <summary>
                    /// the Width of the hitbox
                    /// </summary>
                    public short Top;
                    /// <summary>
                    /// the Ypos of the hitbox
                    /// </summary>
                    public short Right;
                    /// <summary>
                    /// the height of the hitbox
                    /// </summary>
                    public short Bottom;
                }

                /// <summary>
                /// the hitbox data for the frame
                /// </summary>
                public List<HitBox> HitBoxes = new List<HitBox>();
                /// <summary>
                /// the spritesheet ID
                /// </summary>
                public byte SpriteSheet = 0;
                /// <summary>
                /// the collisionBox ID
                /// </summary>
                public byte CollisionBox = 0;
                /// <summary>
                /// how many frames to wait before the next frame is shown
                /// </summary>
                public short Delay = 0;
                /// <summary>
                /// special value, used for things like the title card letters (and strangely, mighty's victory anim)
                /// </summary>
                public short ID = 0;
                /// <summary>
                /// the Xpos on the sheet
                /// </summary>
                public short X = 0;
                /// <summary>
                /// the Ypos on the sheet
                /// </summary>
                public short Y = 0;
                /// <summary>
                /// the width of the frame
                /// </summary>
                public short Width = 0;
                /// <summary>
                /// the height of the frame
                /// </summary>
                public short Height = 0;
                /// <summary>
                /// the X Offset for the frame
                /// </summary>
                public short PivotX = 0;
                /// <summary>
                /// the Y Offset for the frame
                /// </summary>
                public short PivotY = 0;

                public Frame()
                {

                }

                public Frame(Reader reader, Animation anim = null)
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

                /// <summary>
                /// Retrieves the PivotX value for the frame relative to its horrizontal flipping.
                /// </summary>
                /// <param name="fliph">Horizontal flip</param>
                public int RelCenterX(bool fliph)
                {
                    return (fliph ? -(Width + PivotX) : PivotX);
                }

                /// <summary>
                /// Retrieves the PivotY value for the frame relative to its vertical flipping.
                /// </summary>
                /// <param name="flipv">Vertical flip</param>
                public int RelCenterY(bool flipv)
                {
                    return (flipv ? -(Height + PivotY) : PivotY);
                }


            }

            /// <summary>
            /// the name of the animtion
            /// </summary>
            public string AnimName
            {
                get;
                set;
            }
            /// <summary>
            /// the list of frames in this animation
            /// </summary>
            public List<Frame> Frames = new List<Frame>();
            /// <summary>
            /// the frame to loop back from
            /// </summary>
            public byte LoopIndex;
            /// <summary>
            /// the amount to multiply each frame's "Delay" value
            /// </summary>
            public short SpeedMultiplyer;
            /// <summary>
            /// the rotation style of the animation
            /// </summary>
            public byte RotationFlags;

            public AnimationEntry()
            {

            }

            public AnimationEntry(Reader reader, Animation anim = null)
            {
                AnimName = reader.ReadRSDKString().Replace("" + '\0', "");
                short frameCount = reader.ReadInt16();
                SpeedMultiplyer = reader.ReadInt16();
                LoopIndex = reader.ReadByte();
                RotationFlags = reader.ReadByte();
                for (int i = 0; i < frameCount; ++i)
                {
                    Frames.Add(new Frame(reader,anim));
                }
            }

            public void Write(Writer writer)
            {
                writer.WriteRSDKString(AnimName + '\0');
                writer.Write((short)Frames.Count);
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
                Frames.Add(new Frame());
            }

            public void CloneFrame(int frame)
            {
                Frames.Add((Frame)Frames[frame].Clone());
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

            int TotalFrameCount = reader.ReadInt32();

            int spriteSheetCount = reader.ReadByte();
            for (int i = 0; i < spriteSheetCount; ++i)
            {
                SpriteSheets.Add(reader.ReadRSDKString().Replace("" + '\0', ""));
            }

            int collisionBoxCount = reader.ReadByte();
            for (int i = 0; i < collisionBoxCount; ++i)
            {
                CollisionBoxes.Add(reader.ReadRSDKString().Replace("" + '\0', ""));
                string tmp = "";
                for (int ii = 0; ii < CollisionBoxes[i].Length - 1; ii++) //Fixes a crash when using the string to load (by trimming the null char off)
                {
                    tmp += CollisionBoxes[i][ii];
                }
                CollisionBoxes[i] = tmp;
            }

            var animationCount = reader.ReadInt16();
            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new AnimationEntry(reader, this));
            reader.Close();
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);
            writer.Write(TotalFrameCount);

            writer.Write((byte)SpriteSheets.Count);
            for (int i = 0; i < SpriteSheets.Count; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i] + '\0');
            }

            writer.Write((byte)CollisionBoxes.Count);
            for (int i = 0; i < CollisionBoxes.Count; ++i)
            {
                writer.WriteRSDKString(CollisionBoxes[i] + '\0');
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
            AnimationEntry a = new AnimationEntry();
            Animations.Add(a);
        }

        public void CloneAnimation(int anim)
        {
            AnimationEntry a = new AnimationEntry();

            a.AnimName = Animations[anim].AnimName;
            byte FrameAmount = (byte)Animations[anim].Frames.Count;
            a.LoopIndex = Animations[anim].LoopIndex;
            a.SpeedMultiplyer = Animations[anim].SpeedMultiplyer;
            a.RotationFlags = Animations[anim].RotationFlags;

            a.Frames.Clear();

            for (int i = 0; i < FrameAmount; i++)
            {
                a.Frames.Add((AnimationEntry.Frame)Animations[anim].Frames[i].Clone());
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
