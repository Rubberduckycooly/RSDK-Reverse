using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
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
                return "..\\sprites\\";
            }
        }

        /// <summary>
        /// if this is set then only allow one sheet (meaning it'll be used for Objects and not the player)
        /// </summary>
        public bool isObjectAni = false;

        /// <summary>
        /// a List of paths to the spritesheets, relative to "Data/Sprites"
        /// </summary>
        public List<string> SpriteSheets = new List<string>();

        /// <summary>
        /// a list of the hitboxes that the animations can use
        /// </summary>
        public List<sprHitbox> CollisionBoxes = new List<sprHitbox>();
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
                public struct HitBox
                {
                    public short Left, Right, Top, Bottom;
                }

                public List<HitBox> HitBoxes = new List<HitBox>();
                /// <summary>
                /// the spritesheet index
                /// </summary>
                public byte SpriteSheet = 0;
                /// <summary>
                /// the collision box
                /// </summary>
                public byte CollisionBox = 0;
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

                public sprFrame()
                {

                }

                public sprFrame(Reader reader, Animation anim = null)
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

            /// <summary>
            /// the name of the animation
            /// </summary>
            public string AnimName;
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
            /// <summary>
            /// the rotation style of the animation
            /// </summary>
            public byte RotationFlags;

            public sprAnimation()
            {

            }

            public sprAnimation(Reader reader, Animation anim = null)
            {
                AnimName = reader.ReadString();
                short frameCount = reader.ReadByte();
                SpeedMultiplyer = reader.ReadByte();
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
                writer.Write((byte)Frames.Count);
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
            int spriteSheetCount = reader.ReadByte();
            for (int i = 0; i < spriteSheetCount; ++i)
                SpriteSheets.Add(reader.ReadString());

            var animationCount = reader.ReadInt16();
            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new sprAnimation(reader));

            int collisionBoxCount = reader.ReadByte();
            for (int i = 0; i < collisionBoxCount; ++i)
                CollisionBoxes.Add(new sprHitbox(reader));
            reader.Close();
        }

        public void Write(Writer writer)
        {
            writer.Write((byte)SpriteSheets.Count);
            for (int i = 0; i < SpriteSheets.Count; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i]);
            }

            writer.Write((byte)Animations.Count);
            for (int i = 0; i < Animations.Count; ++i)
            {
                Animations[i].Write(writer);
            }

            writer.Write((byte)CollisionBoxes.Count);
            for (int i = 0; i < CollisionBoxes.Count; ++i)
            {
                CollisionBoxes[i].Write(writer);
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
