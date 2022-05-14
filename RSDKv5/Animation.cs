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
                public class Hitbox
                {
                    public short left;
                    public short top;
                    public short right;
                    public short bottom;
                }

                /// <summary>
                /// the spritesheet ID
                /// </summary>
                public byte sheet = 0;
                /// <summary>
                /// the hitbox data for the frame
                /// </summary>
                public List<Hitbox> hitboxes = new List<Hitbox>();
                /// <summary>
                /// how many frames to wait before the next frame is shown
                /// </summary>
                public ushort duration = 0;
                /// <summary>
                /// represents a unicode character
                /// </summary>
                public ushort unicodeChar = '\0';
                /// <summary>
                /// the XPos on the sheet
                /// </summary>
                public ushort sprX = 0;
                /// <summary>
                /// the YPos on the sheet
                /// </summary>
                public ushort sprY = 0;
                /// <summary>
                /// the width of the frame
                /// </summary>
                public ushort width = 0;
                /// <summary>
                /// the height of the frame
                /// </summary>
                public ushort height = 0;
                /// <summary>
                /// the X Offset for the frame
                /// </summary>
                public short pivotX = 0;
                /// <summary>
                /// the Y Offset for the frame
                /// </summary>
                public short pivotY = 0;

                public Frame() { }

                public Frame(Reader reader, Animation anim = null)
                {
                    Read(reader, anim);
                }

                public void Read(Reader reader, Animation anim = null)
                {
                    sheet = reader.ReadByte();
                    duration = reader.ReadUInt16();
                    unicodeChar = reader.ReadUInt16();
                    sprX = reader.ReadUInt16();
                    sprY = reader.ReadUInt16();
                    width = reader.ReadUInt16();
                    height = reader.ReadUInt16();
                    pivotX = reader.ReadInt16();
                    pivotY = reader.ReadInt16();

                    for (int i = 0; anim != null && i < anim.hitboxTypes.Count; ++i)
                    {
                        var hitBox = new Hitbox();
                        hitBox.left = reader.ReadInt16();
                        hitBox.top = reader.ReadInt16();
                        hitBox.right = reader.ReadInt16();
                        hitBox.bottom = reader.ReadInt16();
                        hitboxes.Add(hitBox);
                    }
                }

                public void Write(Writer writer)
                {
                    writer.Write(sheet);
                    writer.Write(duration);
                    writer.Write(unicodeChar);
                    writer.Write(sprX);
                    writer.Write(sprY);
                    writer.Write(width);
                    writer.Write(height);
                    writer.Write(pivotX);
                    writer.Write(pivotY);

                    for (int c = 0; c < hitboxes.Count; ++c)
                    {
                        writer.Write(hitboxes[c].left);
                        writer.Write(hitboxes[c].top);
                        writer.Write(hitboxes[c].right);
                        writer.Write(hitboxes[c].bottom);
                    }
                }
            }

            public enum RotationStyles
            {
                None,
                Full,
                Snap_45Deg,
                Snap_90Deg,
                Snap_180Deg,
                StaticRotation,
            }

            /// <summary>
            /// the name of the animtion
            /// </summary>
            public string name = "New Animation";
            /// <summary>
            /// the list of frames in this animation
            /// </summary>
            public List<Frame> frames = new List<Frame>();
            /// <summary>
            /// the frame to loop back from
            /// </summary>
            public byte loopIndex = 0;
            /// <summary>
            /// the speed of the animation
            /// </summary>
            public short speed = 0;
            /// <summary>
            /// the rotation style of the animation
            /// </summary>
            public RotationStyles rotationStyle;

            public AnimationEntry() { }

            public AnimationEntry(Reader reader, Animation anim = null)
            {
                Read(reader, anim);
            }

            public void Read(Reader reader, Animation anim = null)
            {
                name = reader.ReadStringRSDK();
                short frameCount = reader.ReadInt16();
                speed = reader.ReadInt16();
                loopIndex = reader.ReadByte();
                rotationStyle = (RotationStyles)reader.ReadByte();

                frames.Clear();
                for (int f = 0; f < frameCount; ++f)
                    frames.Add(new Frame(reader, anim));
            }

            public void Write(Writer writer)
            {
                writer.WriteStringRSDK(name);
                writer.Write((short)frames.Count);
                writer.Write(speed);
                writer.Write(loopIndex);
                writer.Write((byte)rotationStyle);

                foreach (Frame frame in frames)
                    frame.Write(writer);
            }
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'S', (byte)'P', (byte)'R', 0 };

        private uint totalFrameCount
        {
            get
            {
                return (uint)animations.Take(animations.Count).Sum(x => x.frames.Count);
            }
        }

        /// <summary>
        /// a list of all the spritesheets to be loaded, each sheet path is relative to "Data/Sprites/"
        /// </summary>
        public List<string> spriteSheets = new List<string>();
        /// <summary>
        /// the list of Animations in the file
        /// </summary>
        public List<AnimationEntry> animations = new List<AnimationEntry>();
        /// <summary>
        /// the list of names for each hitbox to be used per-frame
        /// </summary>
        public List<string> hitboxTypes = new List<string>();

        public Animation() { }

        public Animation(string filename) : this(new Reader(filename)) { }

        public Animation(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Animation(Reader reader)
        {
            // Header
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid Animation v5 signature");
            }

            // used by RSDKv5 to allocate space for all the frames, automatically handled here
            uint totalFrameCount = reader.ReadUInt32();

            // SpriteSheets
            int spriteSheetCount = reader.ReadByte();
            spriteSheets.Clear();
            for (int s = 0; s < spriteSheetCount; ++s)
                spriteSheets.Add(reader.ReadStringRSDK());

            // Hitboxes
            int hitboxTypeCount = reader.ReadByte();
            hitboxTypes.Clear();
            for (int h = 0; h < hitboxTypeCount; ++h)
                hitboxTypes.Add(reader.ReadStringRSDK());

            // Animations
            int animationCount = reader.ReadInt16();
            animations.Clear();
            for (int a = 0; a < animationCount; ++a)
                animations.Add(new AnimationEntry(reader, this));

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            // Header
            writer.Write(signature);
            writer.Write(totalFrameCount);

            // SpriteSheets
            writer.Write((byte)spriteSheets.Count);
            foreach (string sheet in spriteSheets)
                writer.WriteStringRSDK(sheet);

            // Hitboxes
            writer.Write((byte)hitboxTypes.Count);
            foreach (string type in hitboxTypes)
                writer.WriteStringRSDK(type);

            // Animations
            writer.Write((ushort)animations.Count);
            foreach (AnimationEntry anim in animations)
                anim.Write(writer);

            writer.Close();
        }
    }
}
