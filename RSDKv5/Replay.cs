using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zlib;

namespace RSDKv5
{
    public class Replay
    {
        public class ReplayEntry
        {
            public enum InfoTypes
            {
                None,
                StateChange,
                UseFlags,
                PassedTimeAttackGate,
            }
            public enum FlagTypes
            {
                None = 0,
                InputChange = 1 << 0,
                PositionChange = 1 << 1,
                VelocityChange = 1 << 2,
                GimmickChange = 1 << 3,
                DirectionChange = 1 << 4,
                RotationChange = 1 << 5,
                AnimationChange = 1 << 6,
                FrameChange = 1 << 7,
            }
            public enum Directions
            {
                FlipNone,
                FlipX,
                FlipY,
                FlipXY,
            }

            public InfoTypes info = InfoTypes.None;
            public FlagTypes flags = FlagTypes.None;
            private byte inputs = 0;

            public bool upHold
            {
                get { return (inputs & 0x01) != 0; }
                set { inputs = (byte)setBit(0, value, inputs); }
            }

            public bool downHold
            {
                get { return (inputs & 0x02) != 0; }
                set { inputs = (byte)setBit(1, value, inputs); }
            }

            public bool leftHold
            {
                get { return (inputs & 0x04) != 0; }
                set { inputs = (byte)setBit(2, value, inputs); }
            }

            public bool rightHold
            {
                get { return (inputs & 0x08) != 0; }
                set { inputs = (byte)setBit(3, value, inputs); }
            }

            public bool jumpPress
            {
                get { return (inputs & 0x10) != 0; }
                set { inputs = (byte)setBit(4, value, inputs); }
            }

            public bool jumpHold
            {
                get { return (inputs & 0x20) != 0; }
                set { inputs = (byte)setBit(5, value, inputs); }
            }

            public Directions direction = Directions.FlipNone;
            public VariableValue.Vector2 position = new VariableValue.Vector2();
            public VariableValue.Vector2 velocity = new VariableValue.Vector2();
            public int rotation = 0;
            public byte anim = 0;
            public byte frame = 0;

            private static int setBit(int pos, bool set, int val)
            {
                if (set)
                    val |= 1 << pos;
                if (!set)
                    val &= ~(1 << pos);
                return val;
            }

            public ReplayEntry() { }

            public int unpack(Reader reader, bool isPacked)
            {
                int pos = (int)reader.BaseStream.Position;

                info = (InfoTypes)reader.ReadByte();
                byte flags = reader.ReadByte();
                if (isPacked)
                {
                    bool flag = info == InfoTypes.StateChange || info == InfoTypes.PassedTimeAttackGate;

                    if ((flags & (byte)FlagTypes.InputChange) != 0 || flag)
                        inputs = reader.ReadByte();

                    if ((flags & (byte)FlagTypes.PositionChange) != 0 || flag)
                    {
                        position.x = reader.ReadInt32();
                        position.y = reader.ReadInt32();
                    }
                    if ((flags & (byte)FlagTypes.VelocityChange) != 0 || flag)
                    {
                        velocity.x = reader.ReadInt32();
                        velocity.y = reader.ReadInt32();
                    }

                    if ((flags & (byte)FlagTypes.RotationChange) != 0 || flag)
                        rotation = reader.ReadByte() << 1;

                    if ((flags & (byte)FlagTypes.DirectionChange) != 0 || flag)
                        direction = (Directions)reader.ReadByte();

                    if ((flags & (byte)FlagTypes.AnimationChange) != 0 || flag)
                        anim = reader.ReadByte();

                    if ((flags & (byte)FlagTypes.FrameChange) != 0 || flag)
                        frame = reader.ReadByte();
                }
                else
                {
                    inputs = reader.ReadByte();
                    position.x = reader.ReadInt32();
                    position.y = reader.ReadInt32();
                    velocity.x = reader.ReadInt32();
                    velocity.y = reader.ReadInt32();
                    rotation = reader.ReadInt32();
                    direction = (Directions)reader.ReadByte();
                    anim = reader.ReadByte();
                    frame = reader.ReadByte();
                }

                this.flags = (FlagTypes)flags;

                return (int)reader.BaseStream.Position - pos;
            }
            public int pack(Writer writer, bool isPacked)
            {
                int pos = (int)writer.BaseStream.Position;

                writer.Write((byte)info);

                bool flag = info == InfoTypes.StateChange || info == InfoTypes.PassedTimeAttackGate;
                writer.Write((byte)this.flags);

                byte flags = (byte)this.flags;
                if (isPacked)
                {
                    if (flag || (flags & (byte)FlagTypes.InputChange) != 0)
                        writer.Write(inputs);

                    if (flag || (flags & (byte)FlagTypes.PositionChange) != 0)
                    {
                        writer.Write(position.x);
                        writer.Write(position.y);
                    }

                    if (flag || (flags & (byte)FlagTypes.VelocityChange) != 0)
                    {
                        writer.Write(velocity.x);
                        writer.Write(velocity.y);
                    }

                    if (flag || (flags & (byte)FlagTypes.RotationChange) != 0)
                        writer.Write((byte)(rotation >> 1));

                    if (flag || (flags & (byte)FlagTypes.DirectionChange) != 0)
                        writer.Write((byte)direction);

                    if (flag || (flags & (byte)FlagTypes.AnimationChange) != 0)
                        writer.Write(anim);

                    if (flag || (flags & (byte)FlagTypes.FrameChange) != 0)
                        writer.Write(frame);
                }
                else
                {
                    writer.Write(inputs);
                    writer.Write(position.x);
                    writer.Write(position.y);
                    writer.Write(velocity.x);
                    writer.Write(velocity.y);
                    writer.Write(rotation);
                    writer.Write((byte)direction);
                    writer.Write(anim);
                    writer.Write(frame);
                }

                return (int)writer.BaseStream.Position - pos;
            }
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly uint signature = 0xF6057BED;

        // Header
        public int gameVer = 0;
        public bool isPacked = false;
        public int startingFrame = 1;
        public int zoneID = 0;
        public int act = 0;
        public int characterID = 0;
        public bool isPlusLayout = false;
        public int oscillation = 0;
        private int unknown = 0;

        List<ReplayEntry> frames = new List<ReplayEntry>();

        public Replay() { }

        public Replay(string filename) : this(new Reader(filename)) { }
        public Replay(Stream stream) : this(new Reader(stream)) { }

        public Replay(Reader reader)
        {
            Reader creader = reader.getCompressedStreamRaw();
            reader.Close();

            // Signature
            if (creader.ReadUInt32() != signature)
            {
                creader.Close();
                throw new Exception("Invalid Replay v5 signature");
            }

            gameVer = creader.ReadInt32();
            isPacked = creader.readBool32();
            bool isNotEmpty = creader.readBool32();
            int frameCount = creader.ReadInt32();
            startingFrame = creader.ReadInt32();
            zoneID = creader.ReadInt32();
            act = creader.ReadInt32();
            characterID = creader.ReadInt32();
            isPlusLayout = creader.readBool32();
            oscillation = creader.ReadInt32();
            int bufferSize = creader.ReadInt32();
            float avgSize = creader.ReadSingle();
            unknown = creader.ReadInt32();

            frames.Clear();
            for (int f = 0; f < frameCount; ++f)
            {
                ReplayEntry frame = new ReplayEntry();
                frame.unpack(creader, isPacked);
                frames.Add(frame);
            }

            creader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            using (var stream = new MemoryStream())
            {
                using (var cwriter = new Writer(stream))
                {
                    int bufferSize = 0;
                    byte[] frameBuffer = new byte[1];
                    float averageSize = 0;
                    int packedFrameCount = 0;

                    using (var frameStream = new MemoryStream())
                    {
                        using (var fwriter = new Writer(frameStream))
                        {
                            foreach (ReplayEntry frame in frames)
                            {
                                int frameSize = frame.pack(fwriter, isPacked);
                                bufferSize += frameSize;

                                if (frames.Count > 1)
                                {
                                    uint frameCount = (uint)frames.Count;
                                    float avg = averageSize;
                                    float sizef = frameSize;
                                    averageSize = ((avg * packedFrameCount) + sizef) / (packedFrameCount + 1);
                                }
                                else
                                {
                                    averageSize = frameSize;
                                }
                                packedFrameCount++;
                            }
                        }
                        frameBuffer = frameStream.ToArray();
                    }

                    if (!isPacked)
                        bufferSize = 28 * (frames.Count + 2);

                    cwriter.Write(signature);
                    cwriter.Write(gameVer);
                    cwriter.writeBool32(isPacked ? true : false);
                    cwriter.writeBool32(frames.Count >= 1 ? true : false);
                    cwriter.Write(frames.Count);
                    cwriter.Write(startingFrame);
                    cwriter.Write(zoneID);
                    cwriter.Write(act);
                    cwriter.Write(characterID);
                    cwriter.writeBool32(isPlusLayout ? true : false);
                    cwriter.Write(oscillation);
                    cwriter.Write(bufferSize);
                    cwriter.Write(averageSize);
                    cwriter.Write(unknown);

                    cwriter.Write(frameBuffer);
                }
                writer.writeCompressedRaw(stream.ToArray());
            }

            writer.Close();
        }

    }
}
