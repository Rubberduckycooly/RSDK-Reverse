using System;
using System.Collections.Generic;

namespace RSDKvRS
{
    [Serializable]
    public class Animation : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// the default names for the animations
        /// </summary>
        public string[] AnimNames = new string[]
{
            "Stopped",
            "Waiting",
            "Looking Up",
            "Looking Down",
            "Walking",
            "Running",
            "Skidding",
            "SuperPeelOut",
            "Spin Dash",
            "Jumping",
            "Bouncing",
            "Hurt",
            "Dying",
            "Life Icon",
            "Drowning",
            "Fan Rotate",
            "Breathing",
            "Pushing",
            "Flailing Left",
            "Flailing Right",
            "Sliding",
            "Hanging",
            "Dropping",
            "FinishPose",
            "Cork Screw",
            "Retro Sonic Animation #26",
            "Retro Sonic Animation #27",
            "Retro Sonic Animation #28",
            "Retro Sonic Animation #29",
            "Retro Sonic Animation #30",
            "BonusSpin",
            "SpecialStop",
            "SpecialWalk",
            "SpecialJump",
};

        /// <summary>
        /// a string to be added to the start of the path
        /// </summary>
        public string PathMod
        {
            get
            {
                return "";
            }
        }

        public bool DreamcastVer = false;

        /// <summary>
        /// Unknown Value
        /// </summary>
        public byte Unknown = 0;
        /// <summary>
        /// What Moves to give the player
        /// </summary>
        public byte PlayerType = 0;

        /// <summary>
        /// a List of paths to the spritesheets, relative to "Data/Sprites"
        /// </summary>
        public string[] SpriteSheets = new string[3];

        /// <summary>
        /// a list of Animations in the file
        /// </summary>
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
                    /// left offset
                    /// </summary>
                    public sbyte Left;
                    /// <summary>
                    /// top offset
                    /// </summary>
                    public sbyte Top;
                    /// <summary>
                    /// right offset
                    /// </summary>
                    public sbyte Right;
                    /// <summary>
                    /// bottom offset
                    /// </summary>
                    public sbyte Bottom;
                }

                /// <summary>
                /// the spritesheet index
                /// </summary>
                public byte SpriteSheet = 0;
                /// <summary>
                /// the collision box
                /// </summary>
                public HitBox CollisionBox = new HitBox();
                /// <summary>
                /// the delay of each frame before advancing to the next one in frames (always 256)
                /// </summary>
                public readonly short Delay = 256;
                /// <summary>
                /// the Xpos on the sheet
                /// </summary>
                public byte X = 0;
                /// <summary>
                /// the YPos on the sheet
                /// </summary>
                public byte Y = 0;
                /// <summary>
                /// the frame's width
                /// </summary>
                public byte Width = 0;
                /// <summary>
                /// the frame's height
                /// </summary>
                public byte Height = 0;
                /// <summary>
                /// the offsetX of the frame
                /// </summary>
                public SByte PivotX = 0;
                /// <summary>
                /// the offsetY of the frame
                /// </summary>
                public SByte PivotY = 0;

                public Frame()
                {

                }

                public Frame(Reader reader, Animation anim = null)
                {
                    X = reader.ReadByte();
                    Y = reader.ReadByte();
                    Width = reader.ReadByte();
                    Height = reader.ReadByte();
                    SpriteSheet = reader.ReadByte();

                    CollisionBox.Left = reader.ReadSByte();
                    CollisionBox.Top = reader.ReadSByte();
                    CollisionBox.Right = reader.ReadSByte();
                    CollisionBox.Bottom = reader.ReadSByte();

                    byte[] PivotVals = new byte[2];

                    PivotVals[0] = (byte)-reader.ReadByte();
                    PivotVals[1] = (byte)-reader.ReadByte();

                    PivotX = (sbyte)PivotVals[0];
                    PivotY = (sbyte)PivotVals[1];
                }

                public void Write(Writer writer)
                {
                    writer.Write(X);
                    writer.Write(Y);
                    writer.Write(Width);
                    writer.Write(Height);
                    writer.Write(SpriteSheet);

                    writer.Write(CollisionBox.Left);
                    writer.Write(CollisionBox.Top);
                    writer.Write(CollisionBox.Right);
                    writer.Write(CollisionBox.Bottom);
                    
                    byte px = (byte)PivotX;
                    byte py = (byte)PivotY;
                    writer.Write((byte)-px);
                    writer.Write((byte)-py);
                }

            }

            /// <summary>
            /// the name of the animation (RSDKvRS doesn't have one, so we use a "Plain one")
            /// </summary>
            public string AnimationName
            {
                get;
                set;
            }
            /// <summary>
            /// a list of frames in the animation
            /// </summary>
            public List<Frame> Frames = new List<Frame>();
            /// <summary>
            /// what frame to loop back from
            /// </summary>
            public byte LoopIndex;
            /// <summary>
            /// the speed multiplyer of the animation
            /// </summary>
            public byte SpeedMultiplyer;

            public AnimationEntry()
            {

            }

            public AnimationEntry(Reader reader)
            {
                byte frameCount = reader.ReadByte();
                SpeedMultiplyer = (byte)(reader.ReadByte() * 4);
                LoopIndex = reader.ReadByte();
                for (int i = 0; i < frameCount; ++i)
                {
                    Frames.Add(new Frame(reader));
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

        public Animation(Reader reader, bool DreamcastVer = false)
        {
            Unknown = reader.ReadByte();
            PlayerType = reader.ReadByte();

            this.DreamcastVer = DreamcastVer;

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

            for (int i = 0; i < 3; ++i)
            {
                if (i == 2 && DreamcastVer)
                {
                    SpriteSheets[i] = "<NULL>";
                }
                else
                {
                    SpriteSheets[i] = reader.ReadRSDKString();
                }
            }

            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new AnimationEntry(reader));
            reader.Close();
        }

        public void Write(Writer writer)
        {
            writer.Write(Unknown);
            writer.Write(PlayerType);

            writer.Write((byte)Animations.Count);

            byte SheetCnt = (byte)SpriteSheets.Length;

            for (int i = 0; i < SpriteSheets.Length; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i]);
            }

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

            byte FrameAmount = (byte)Animations[anim].Frames.Count;
            a.LoopIndex = Animations[anim].LoopIndex;
            a.SpeedMultiplyer = Animations[anim].SpeedMultiplyer;

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

        public void DeleteEndAnimation()
        {
            Animations.RemoveAt(Animations.Count - 1);
        }

    }
}
