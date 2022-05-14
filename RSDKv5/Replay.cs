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
            public enum ValueChanges
            {
                None = 0,
                Input = 1 << 0,
                Position = 1 << 1,
                Velocity = 1 << 2,
                Gimmick = 1 << 3,
                Direction = 1 << 4,
                Rotation = 1 << 5,
                Animation = 1 << 6,
                Frame = 1 << 7,
            }
            public enum Directions
            {
                FlipNone,
                FlipX,
                FlipY,
                FlipXY,
            }

            public InfoTypes info = InfoTypes.None;
            public ValueChanges changedValues = ValueChanges.None;
            private byte inputs = 0;

            public bool upHold
            {
                get { return (inputs & 0x01) != 0; }
                set { inputs = (byte)SetBit(0, value, inputs); }
            }

            public bool downHold
            {
                get { return (inputs & 0x02) != 0; }
                set { inputs = (byte)SetBit(1, value, inputs); }
            }

            public bool leftHold
            {
                get { return (inputs & 0x04) != 0; }
                set { inputs = (byte)SetBit(2, value, inputs); }
            }

            public bool rightHold
            {
                get { return (inputs & 0x08) != 0; }
                set { inputs = (byte)SetBit(3, value, inputs); }
            }

            public bool jumpPress
            {
                get { return (inputs & 0x10) != 0; }
                set { inputs = (byte)SetBit(4, value, inputs); }
            }

            public bool jumpHold
            {
                get { return (inputs & 0x20) != 0; }
                set { inputs = (byte)SetBit(5, value, inputs); }
            }

            public Directions direction = Directions.FlipNone;
            public VariableValue.Vector2 position = new VariableValue.Vector2();
            public VariableValue.Vector2 velocity = new VariableValue.Vector2();
            public int rotation = 0;
            public byte anim = 0;
            public byte frame = 0;

            private static int SetBit(int pos, bool set, int val)
            {
                if (set)
                    val |= 1 << pos;

                if (!set)
                    val &= ~(1 << pos);

                return val;
            }

            public ReplayEntry() { }

            public int Unpack(Reader reader, bool isPacked)
            {
                int pos = (int)reader.BaseStream.Position;

                info = (InfoTypes)reader.ReadByte();
                byte changedValues = reader.ReadByte();
                if (isPacked)
                {
                    bool forceUnpack = info == InfoTypes.StateChange || info == InfoTypes.PassedTimeAttackGate;

                    if ((changedValues & (byte)ValueChanges.Input) != 0 || forceUnpack)
                        inputs = reader.ReadByte();

                    if ((changedValues & (byte)ValueChanges.Position) != 0 || forceUnpack)
                    {
                        position.x = reader.ReadInt32();
                        position.y = reader.ReadInt32();
                    }
                    if ((changedValues & (byte)ValueChanges.Velocity) != 0 || forceUnpack)
                    {
                        velocity.x = reader.ReadInt32();
                        velocity.y = reader.ReadInt32();
                    }

                    if ((changedValues & (byte)ValueChanges.Rotation) != 0 || forceUnpack)
                        rotation = reader.ReadByte() << 1;

                    if ((changedValues & (byte)ValueChanges.Direction) != 0 || forceUnpack)
                        direction = (Directions)reader.ReadByte();

                    if ((changedValues & (byte)ValueChanges.Animation) != 0 || forceUnpack)
                        anim = reader.ReadByte();

                    if ((changedValues & (byte)ValueChanges.Frame) != 0 || forceUnpack)
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

                this.changedValues = (ValueChanges)changedValues;

                return (int)reader.BaseStream.Position - pos;
            }
            public int Pack(Writer writer, bool isPacked)
            {
                int pos = (int)writer.BaseStream.Position;

                writer.Write((byte)info);

                writer.Write((byte)this.changedValues);

                byte flags = (byte)this.changedValues;
                bool forcePack = info == InfoTypes.StateChange || info == InfoTypes.PassedTimeAttackGate;
                if (isPacked)
                {
                    if (forcePack || (flags & (byte)ValueChanges.Input) != 0)
                        writer.Write(inputs);

                    if (forcePack || (flags & (byte)ValueChanges.Position) != 0)
                    {
                        writer.Write(position.x);
                        writer.Write(position.y);
                    }

                    if (forcePack || (flags & (byte)ValueChanges.Velocity) != 0)
                    {
                        writer.Write(velocity.x);
                        writer.Write(velocity.y);
                    }

                    if (forcePack || (flags & (byte)ValueChanges.Rotation) != 0)
                        writer.Write((byte)(rotation >> 1));

                    if (forcePack || (flags & (byte)ValueChanges.Direction) != 0)
                        writer.Write((byte)direction);

                    if (forcePack || (flags & (byte)ValueChanges.Animation) != 0)
                        writer.Write(anim);

                    if (forcePack || (flags & (byte)ValueChanges.Frame) != 0)
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
        public int gameVer = 6; // 1.06 (also applies to EGS/Origin releases)
        public bool isPacked = false;
        public int startingFrame = 1;
        public int zoneID = 0;
        public int act = 0;
        public int characterID = 0;
        public bool isPlusLayout = false;
        public int oscillation = 0;
        private int unused = 0;

        List<ReplayEntry> frames = new List<ReplayEntry>();

        public Replay() { }

        public Replay(string filename) : this(new Reader(filename)) { }
        public Replay(Stream stream) : this(new Reader(stream)) { }

        public Replay(Reader reader)
        {
            Reader creader = reader.GetCompressedStreamRaw();
            reader.Close();

            // Signature
            if (creader.ReadUInt32() != signature)
            {
                creader.Close();
                throw new Exception("Invalid Replay v5 signature");
            }

            gameVer = creader.ReadInt32();
            isPacked = creader.ReadBool32();
            bool isNotEmpty = creader.ReadBool32();
            int frameCount = creader.ReadInt32();
            startingFrame = creader.ReadInt32();
            zoneID = creader.ReadInt32();
            act = creader.ReadInt32();
            characterID = creader.ReadInt32();
            isPlusLayout = creader.ReadBool32();
            oscillation = creader.ReadInt32();
            int bufferSize = creader.ReadInt32();
            float avgSize = creader.ReadSingle();
            unused = creader.ReadInt32();

            frames.Clear();
            for (int f = 0; f < frameCount; ++f)
            {
                ReplayEntry frame = new ReplayEntry();
                frame.Unpack(creader, isPacked);
                frames.Add(frame);
            }

            creader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
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
                                int frameSize = frame.Pack(fwriter, isPacked);
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
                    cwriter.WriteBool32(isPacked ? true : false);
                    cwriter.WriteBool32(frames.Count >= 1 ? true : false);
                    cwriter.Write(frames.Count);
                    cwriter.Write(startingFrame);
                    cwriter.Write(zoneID);
                    cwriter.Write(act);
                    cwriter.Write(characterID);
                    cwriter.WriteBool32(isPlusLayout ? true : false);
                    cwriter.Write(oscillation);
                    cwriter.Write(bufferSize);
                    cwriter.Write(averageSize);
                    cwriter.Write(unused);

                    cwriter.Write(frameBuffer);
                }

                writer.WriteCompressedRaw(stream.ToArray());
            }

            writer.Close();
        }
    }
}
