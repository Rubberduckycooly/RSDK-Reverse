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

                /// <summary>
                /// the spritesheet index
                /// </summary>
                public byte SpriteSheet = 0;
                /// <summary>
                /// the collision box
                /// </summary>
                public byte[] CollisionBox = new byte[4];
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
                public byte PivotX = 0;
                /// <summary>
                /// the offsetY of the frame
                /// </summary>
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

            /// <summary>
            /// the name of the animation (RSDKvRS doesn't have one, so we use a "Plain one")
            /// </summary>
            public string AnimationName
            {
                get
                {
                    return "RSDKvRS Animation ";
                }
            }
            /// <summary>
            /// a list of frames in the animation
            /// </summary>
            public List<sprFrame> Frames = new List<sprFrame>();
            /// <summary>
            /// what frame to loop back from
            /// </summary>
            public byte LoopIndex;
            /// <summary>
            /// the speed multiplyer of the animation
            /// </summary>
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
                SpriteSheets[i] = reader.ReadString();

            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new sprAnimation(reader));
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
