using System;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv1
{
    public class Animation : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// the default names for the animations
        /// </summary>
        private static readonly string[] animationNames = {
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
            "Fly Tired",
            "Climbing",
            "Ledge Pull Up",
            "Glide Slide",
            "BonusSpin",
            "SpecialStop",
            "SpecialWalk",
            "SpecialJump",
        };

        public enum PlayerIDs
        {
            Sonic,
            Tails,
            Knuckles,
        }

        public class AnimationEntry : ICloneable
        {
            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public class Frame : ICloneable
            {
                public object Clone()
                {
                    return this.MemberwiseClone();
                }

                public struct Hitbox
                {
                    public sbyte left;
                    public sbyte top;
                    public sbyte right;
                    public sbyte bottom;
                }

                /// <summary>
                /// the spritesheet index
                /// </summary>
                public byte sheet = 0;
                /// <summary>
                /// the collision box
                /// </summary>
                public Hitbox hitbox = new Hitbox();
                /// <summary>
                /// the XPos on the sheet
                /// </summary>
                public byte sprX = 0;
                /// <summary>
                /// the YPos on the sheet
                /// </summary>
                public byte sprY = 0;
                /// <summary>
                /// the frame's width
                /// </summary>
                public byte width = 0;
                /// <summary>
                /// the frame's height
                /// </summary>
                public byte height = 0;
                /// <summary>
                /// the X offset of the frame
                /// </summary>
                public sbyte pivotX = 0;
                /// <summary>
                /// the Y offset of the frame
                /// </summary>
                public sbyte pivotY = 0;

                public Frame() { }

                public Frame(Reader reader)
                {
                    read(reader);
                }

                public void read(Reader reader)
                {
                    sprX = reader.ReadByte();
                    sprY = reader.ReadByte();
                    width = reader.ReadByte();
                    height = reader.ReadByte();
                    sheet = reader.ReadByte();

                    hitbox.left = readSByte(reader);
                    hitbox.top = readSByte(reader);
                    hitbox.right = readSByte(reader);
                    hitbox.bottom = readSByte(reader);

                    pivotX = (sbyte)-reader.ReadByte();
                    pivotY = (sbyte)-reader.ReadByte();
                }

                public void write(Writer writer)
                {
                    writer.Write(sprX);
                    writer.Write(sprY);
                    writer.Write(width);
                    writer.Write(height);
                    writer.Write(sheet);

                    writeSByte(writer, hitbox.left);
                    writeSByte(writer, hitbox.top);
                    writeSByte(writer, hitbox.right);
                    writeSByte(writer, hitbox.bottom);

                    writer.Write((byte)-pivotX);
                    writer.Write((byte)-pivotY);
                }

                private sbyte readSByte(Reader reader)
                {
                    int val = reader.ReadByte();
                    if (val >= 0x80)
                        return (sbyte)(0x80 - val);
                    else
                        return (sbyte)val;
                }
                private void writeSByte(Writer writer, int val)
                {
                    if (val < 0)
                        writer.Write((byte)(0x80 - val));
                    else
                        writer.Write((byte)val);
                }

            }

            /// <summary>
            /// the name of the animation
            /// </summary>
            public string name { get; private set; }
            /// <summary>
            /// a list of frames in the animation
            /// </summary>
            public List<Frame> frames = new List<Frame>();
            /// <summary>
            /// the speed of the animation
            /// </summary>
            public byte speed = 0;
            /// <summary>
            /// what frame to loop from
            /// </summary>
            public byte loopIndex = 0;

            /// <summary>
            /// the speed of the animation, represented as a float, with 0 being the minimum speed and 1.0 being the maximum speed
            /// the speed value represents how much to incriment the anim timer per frame
            /// E.G: 1.0 will change the anim frame every in-game frame, while 0.5 will change it every 2 in-game frames
            /// </summary>
            public float speedF
            {
                get { return speed / 60.0f; }
                set { speed = (byte)(value < 0 ? 0 : value > 1.0 ? 60 : (value * 60)); }
            }

            public AnimationEntry(string name = "New Animation") { this.name = name; }

            public AnimationEntry(Reader reader, string name = "New Animation") : this(name)
            {
                read(reader);
            }

            public void read(Reader reader)
            {
                short frameCount = reader.ReadByte();
                speed = reader.ReadByte();
                loopIndex = reader.ReadByte();

                frames.Clear();
                for (int f = 0; f < frameCount; ++f)
                    frames.Add(new Frame(reader));
            }

            public void write(Writer writer)
            {
                writer.Write((byte)frames.Count);
                writer.Write(speed);
                writer.Write(loopIndex);

                foreach (Frame frame in frames)
                    frame.write(writer);
            }
        }

        /// <summary>
        /// Unknown Value, no clue what it does, not used in-engine
        /// </summary>
        private byte unknown = 0;
        /// <summary>
        /// What moveset to give the player
        /// </summary>
        public PlayerIDs playerType = PlayerIDs.Sonic;

        /// <summary>
        /// a list of all the spritesheets to be loaded, each sheet path is relative to "Data/Characters/"
        /// </summary>
        public string[] spriteSheets = new string[3];
        /// <summary>
        /// the list of Animations in the file
        /// </summary>
        public List<AnimationEntry> animations = new List<AnimationEntry>();

        public Animation()
        {
            for (int i = 0; i < 3; ++i)
                spriteSheets[i] = "<NULL>";
        }

        public Animation(string filename) : this(new Reader(filename)) { }

        public Animation(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Animation(Reader reader, bool dcVer = false) : this()
        {
            read(reader, dcVer);
        }

        public void read(Reader reader, bool dcVer = false)
        {
            unknown = reader.ReadByte();
            playerType = (PlayerIDs)reader.ReadByte();
            byte animationCount = reader.ReadByte();

            // SpriteSheets
            for (int s = 0; s < (dcVer ? 2 : 3); ++s)
                spriteSheets[s] = reader.readRSDKString();

            // Animations
            animations.Clear();
            for (int a = 0; a < animationCount; ++a)
                animations.Add(new AnimationEntry(reader, a < animationNames.Length ? animationNames[a] : $"Retro Sonic Animation #{a + 1}"));

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer, bool dcVer = false)
        {
            writer.Write(unknown); //No idea what this is
            writer.Write((byte)playerType);
            writer.Write((byte)animations.Count);

            // SpriteSheets
            for (int s = 0; s < (dcVer ? 2 : 3); ++s)
                writer.writeRSDKString(spriteSheets[s]);

            // Animations
            foreach (AnimationEntry anim in animations)
                anim.write(writer);

            writer.Close();
        }
    }
}
