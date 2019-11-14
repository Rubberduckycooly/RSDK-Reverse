using System;
using System.Collections.Generic;

namespace RSDKv1
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
            "Bored",
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
            "Drowning",
            "Life Icon",
            "Fan Rotate",
            "Breathing",
            "Pushing",
            "Flailing Left",
            "Flailing Right",
            "Sliding",
            "Sonic Nexus Animation #23",
            "FinishPose",
            "Sonic Nexus Animation #24",
            "Sonic Nexus Animation #25",
            "Sonic Nexus Animation #26",
            "Sonic Nexus Animation #27",
            "Sonic Nexus Animation #28",
            "Sonic Nexus Animation #29",
            "Sonic Nexus Animation #30",
            "Sonic Nexus Animation #31",
            "Sonic Nexus Animation #32",
            "Sonic Nexus Animation #33",
            "Cork Screw",
            "Sonic Nexus Animation #35",
            "Sonic Nexus Animation #36",
            "Sonic Nexus Animation #37",
            "Sonic Nexus Animation #38",
            "Sonic Nexus Animation #39",
            "Sonic Nexus Animation #40",
            "Sonic Nexus Animation #41",
            "Sonic Nexus Animation #42",
            "Hanging",
            "Sonic Nexus Animation #44",
            "Sonic Nexus Animation #45",
            "Sonic Nexus Animation #46",
            "Sonic Nexus Animation #47",
            "Sonic Nexus Animation #48",
            "Sonic Nexus Animation #49",
            "Sonic Nexus Animation #50",
            "Sonic Nexus Animation #51",
};

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

        //Why Taxman, why
        /// <summary>
        /// Unknown Values, who knows what they do
        /// </summary>
        public byte[] Unknown = new byte[5];

        /// <summary>
        /// I don't really know tbh, might be a flag to stop loading textures?
        /// </summary>
        byte EndTexFlag;

        /// <summary>
        /// a List of paths to the spritesheets, relative to "Data/Sprites"
        /// </summary>
        public string[] SpriteSheets = new string[3];

        /// <summary>
        /// a list of the hitboxes that the animations can use
        /// </summary>
        public List<sprHitbox> CollisionBoxes = new List<sprHitbox>();
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

                public Frame()
                {

                }

                public Frame(Reader reader, bool bitFlipped = false)
                {
                    SpriteSheet = reader.ReadByte();
                    CollisionBox = reader.ReadByte();
                    X = reader.ReadByte();
                    Y = reader.ReadByte();
                    Width = reader.ReadByte();
                    Height = reader.ReadByte();
                    PivotX = reader.ReadSByte();
                    PivotY = reader.ReadSByte();
                    if (bitFlipped)
                    {
                        SpriteSheet ^= 255;
                        CollisionBox ^= 255;
                        X ^= 255;
                        Y ^= 255;
                        Width ^= 255;
                        Height ^= 255;
                        byte cx = (byte)PivotX;
                        byte cy = (byte)PivotY;
                        cx ^= 255;
                        cy ^= 255;
                        PivotX = (sbyte)cx;
                        PivotY = (sbyte)cy;
                    }
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
            /// the name of the animation (RSDKv1 doesn't have one, so we use a "Plain one")
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

            public AnimationEntry(Reader reader, bool bitflipped = false)
            {
                byte frameCount = reader.ReadByte();
                SpeedMultiplyer = reader.ReadByte();
                LoopIndex = reader.ReadByte();
                if (bitflipped)
                {
                    frameCount ^= 255;
                    SpeedMultiplyer ^= 255;
                    LoopIndex ^= 255;
                }
                for (int i = 0; i < frameCount; ++i)
                {
                    Frames.Add(new Frame(reader,bitflipped));
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

        public class sprHitbox
        {
            public struct HitboxInfo
            {
                public sbyte Left;
                public sbyte Top;
                public sbyte Right;
                public sbyte Bottom;
            }

            public HitboxInfo[] Hitboxes = new HitboxInfo[8];

            public sprHitbox()
            {

            }

            public sprHitbox(Reader reader, bool bitflipped = false)
            {
                for (int i = 0; i < 8; i++)
                {
                    Hitboxes[i].Left = reader.ReadSByte();
                    Hitboxes[i].Top = reader.ReadSByte();
                    Hitboxes[i].Right = reader.ReadSByte();
                    Hitboxes[i].Bottom = reader.ReadSByte();
                    if (bitflipped)
                    {
                        byte l = (byte)Hitboxes[i].Left;
                        byte r = (byte)Hitboxes[i].Right;
                        byte b = (byte)Hitboxes[i].Bottom;
                        byte t = (byte)Hitboxes[i].Top;
                        l ^= 255;
                        t ^= 255;
                        r ^= 255;
                        b ^= 255;
                        Hitboxes[i].Left = (sbyte)l;
                        Hitboxes[i].Right = (sbyte)r;
                        Hitboxes[i].Bottom = (sbyte)b;
                        Hitboxes[i].Top = (sbyte)t;
                    }
                    Console.WriteLine(Hitboxes[i].Left + "," + Hitboxes[i].Top + "," + Hitboxes[i].Right + "," + Hitboxes[i].Bottom);
                }
                Console.WriteLine();
            }

            public void Write(Writer writer)
            {
                for (int i = 0; i < 8; i++)
                {
                    writer.Write(Hitboxes[i].Left);
                    writer.Write(Hitboxes[i].Top);
                    writer.Write(Hitboxes[i].Right);
                    writer.Write(Hitboxes[i].Bottom);
                }
            }
        }

        public Animation()
        {

        }

        public Animation(Reader reader,bool BitFlipped = false)
        {
            Unknown = reader.ReadBytes(5);

            if (BitFlipped)
            {
                for (int i = 0; i < 5; i++)
                {
                    Unknown[i] ^= 255;
                }
            }

            int spriteSheetCount = 3; //always 3

            for (int i = 0; i < spriteSheetCount; ++i)
            {
                int sLen = reader.ReadByte();
                if (BitFlipped) sLen ^= 255;
                byte[] byteBuf = new byte[sLen];

                byteBuf = reader.ReadBytes(sLen);

                if (BitFlipped)
                {
                    for (int ii = 0; ii < sLen; ii++)
                    {
                        byteBuf[ii] ^= 255;
                    }
                }

                string result = System.Text.Encoding.UTF8.GetString(byteBuf);

                SpriteSheets[i] = result;
            }

            byte EndTexFlag = reader.ReadByte(); //Seems to tell the RSDK's reader when to stop reading textures???
            if (BitFlipped) EndTexFlag ^= 255;

            // Read number of animations
            var animationCount = reader.ReadByte();
            if (BitFlipped) animationCount ^= 255;
            for (int i = 0; i < animationCount; ++i)
                Animations.Add(new AnimationEntry(reader,BitFlipped));

            int collisionBoxCount = reader.ReadByte();
            for (int i = 0; i < collisionBoxCount; ++i)
                CollisionBoxes.Add(new sprHitbox(reader, BitFlipped));
            reader.Close();
        }

        public void Write(Writer writer)
        {
            writer.Write(Unknown); //No idea what these are chief

            byte SheetCnt = (byte)SpriteSheets.Length;

            for (int i = 0; i < SpriteSheets.Length; ++i)
            {
                writer.WriteRSDKString(SpriteSheets[i]);
            }

            writer.Write(EndTexFlag);

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
