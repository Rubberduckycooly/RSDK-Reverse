using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKvRS
{
    public class Tileconfig
    {
        /// <summary>
        /// 1024, one for each tile
        /// </summary>
        public const int TILES_COUNT = 0x400; //1024

        /// <summary>
        /// A list of 1024 collision masks (one per tile)
        /// </summary>
        public CollisionMask[] Collision = new CollisionMask[TILES_COUNT];

        public Tileconfig()
        {
            Collision = new CollisionMask[TILES_COUNT];
            for (int i = 0; i < Collision.Length; i++)
            {
                Collision[i] = new CollisionMask();
            }
        }

        public Tileconfig(string filename, bool DCver = false) : this(new Reader(filename), DCver)
        {

        }

        public Tileconfig(System.IO.Stream stream, bool DCver = false) : this(new Reader(stream), DCver)
        {

        }

        public Tileconfig(Reader reader, bool DCver = false)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                Collision[i] = new CollisionMask(reader, DCver);
            }
            reader.Close();
        }

        public void Write(string filename, bool DCver = false)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer, DCver);
        }

        public void Write(System.IO.Stream stream, bool DCver = false)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer, DCver);
        }

        public void Write(Writer writer, bool DCver = false)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
            {
                Collision[i].Write(writer, DCver);
            }
            writer.Close();
        }

        public class CollisionMask
        {

            //DC version doesnt have this unknown value
            public byte PCunknown; //if it's Value is FF it causes the player's collision & gravity to be disabled until jumping, 00 causes the player to be unstuck from the tile
            //May be a "Tile Stickiness Value"


            public byte[] Collision1Up = new byte[16]; //Collision Values for Path A (Up)
            public byte[] Collision1UpSolid = new byte[16]; //Collision Solidity for Path A (Up)

            public byte[] Collision1Right = new byte[16]; //Collision Values for Path A (Right)
            public byte[] Collision1LeftSolid = new byte[16]; //Collision Solidity for Path A (Right)

            public byte[] Collision1Left = new byte[16]; //Collision Values for Path A (Left)
            public byte[] Collision1RightSolid = new byte[16]; //Collision Solidity for Path A (Left)

            public byte[] Collision1Down = new byte[16]; //Collision Values for Path A (Down)
            public byte[] Collision1DownSolid = new byte[16]; //Collision Solidity for Path A (Down)


            public byte[] Collision2Up = new byte[16]; //Collision Values for Path B (Up)
            public byte[] Collision2UpSolid = new byte[16]; //Collision Solidity for Path B (Up)

            public byte[] Collision2Right = new byte[16]; //Collision Values for Path B (Right)
            public byte[] Collision2LeftSolid = new byte[16]; //Collision Solidity for Path B (Right)

            public byte[] Collision2Left = new byte[16]; //Collision Values for Path B (Left)
            public byte[] Collision2RightSolid = new byte[16]; //Collision Solidity for Path B (Left)

            public byte[] Collision2Down = new byte[16]; //Collision Values for Path B (Down)
            public byte[] Collision2DownSolid = new byte[16]; //Collision Solidity for Path B (Down)

            public CollisionMask(bool DCver = false)
            {
            }

            public CollisionMask(string filename, bool DCver = false) : this(new Reader(filename), DCver)
            {
            }

            public CollisionMask(System.IO.Stream stream, bool DCver = false) : this(new Reader(stream), DCver)
            {
            }

            public CollisionMask(Reader reader, bool DCver = false)
            {
                if (DCver)
                {
                    //Path A stuff

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Up[i] = (byte)((c & 0xF0) >> 4);
                        Collision1UpSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Right[i] = (byte)((c & 0xF0) >> 4);
                        Collision1RightSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Left[i] = (byte)((c & 0xF0) >> 4);
                        Collision1LeftSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Down[i] = (byte)((c & 0xF0) >> 4);
                        Collision1DownSolid[i] = (byte)(c & 0x0F);
                    }

                    //Path B Junk

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Up[i] = (byte)((c & 0xF0) >> 4);
                        Collision2UpSolid[i] = (byte)(c & 0x0F);
                        //Load Collision Values (Path A)
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Right[i] = (byte)((c & 0xF0) >> 4);
                        Collision2RightSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Left[i] = (byte)((c & 0xF0) >> 4);
                        Collision2LeftSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Down[i] = (byte)((c & 0xF0) >> 4);
                        Collision2DownSolid[i] = (byte)(c & 0x0F);
                    }
                }
                else if (!DCver)
                {
                    //IDEK
                    PCunknown = reader.ReadByte(); // Single Byte

                    //Path A stuff

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Up[i] = (byte)((c & 0xF0) >> 4);
                        Collision1UpSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Right[i] = (byte)((c & 0xF0) >> 4);
                        Collision1RightSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Left[i] = (byte)((c & 0xF0) >> 4);
                        Collision1LeftSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision1Down[i] = (byte)((c & 0xF0) >> 4);
                        Collision1DownSolid[i] = (byte)(c & 0x0F);
                    }

                    //Path B Junk

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Up[i] = (byte)((c & 0xF0) >> 4);
                        Collision2UpSolid[i] = (byte)(c & 0x0F);
                        //Load Collision Values (Path A)
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Right[i] = (byte)((c & 0xF0) >> 4);
                        Collision2RightSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Left[i] = (byte)((c & 0xF0) >> 4);
                        Collision2LeftSolid[i] = (byte)(c & 0x0F);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = reader.ReadByte();
                        Collision2Down[i] = (byte)((c & 0xF0) >> 4);
                        Collision2DownSolid[i] = (byte)(c & 0x0F);
                    }
                }
            }

            public void Write(string filename, bool DCver = false)
            {
                using (Writer writer = new Writer(filename))
                    this.Write(writer, DCver);
            }

            public void Write(System.IO.Stream stream, bool DCver = false)
            {
                using (Writer writer = new Writer(stream))
                    this.Write(writer, DCver);
            }

            internal void Write(Writer writer, bool DCver = false)
            {
                if (DCver)
                {
                    //Path A shizz

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Up[i], (byte)Collision1UpSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Right[i], (byte)Collision1RightSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Left[i], (byte)Collision1LeftSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Down[i], (byte)Collision1DownSolid[i]);
                        writer.Write(c);
                    }

                    //Path B thingys

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Up[i], (byte)Collision2UpSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Right[i], (byte)Collision2RightSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Left[i], (byte)Collision2LeftSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Down[i], (byte)Collision2DownSolid[i]);
                        writer.Write(c);
                    }
                }
                else if (!DCver)
                {
                    //This. Thing...
                    writer.Write(PCunknown);

                    //Path A shizz

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Up[i], (byte)Collision1UpSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Right[i], (byte)Collision1RightSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Left[i], (byte)Collision1LeftSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision1Down[i], (byte)Collision1DownSolid[i]);
                        writer.Write(c);
                    }

                    //Path B thingys

                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Up[i], (byte)Collision2UpSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Right[i], (byte)Collision2RightSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Left[i], (byte)Collision2LeftSolid[i]);
                        writer.Write(c);
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        byte c = AddNibbles((byte)Collision2Down[i], (byte)Collision2DownSolid[i]);
                        writer.Write(c);
                    }

                }
            }

            public byte AddNibbles(byte a, byte b)
            {
                return (byte)((a & 0xF) << 4 | (b & 0xF));
            }

            public bool IsBitSet(ushort b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }

            public Bitmap DrawCMask(System.Drawing.Color bg, System.Drawing.Color fg, int CollisionDir, bool PathB,Bitmap tile = null)
            {
                Bitmap b;
                bool HasTile = false;
                if (tile == null)
                { b = new Bitmap(16, 16); }
                else
                {
                    b = tile.Clone(new Rectangle(0, 0, tile.Width, tile.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    HasTile = true;
                }

                if (!HasTile)
                {
                    for (int h = 0; h < 16; h++) //Set the BG colour
                    {
                        for (int w = 0; w < 16; w++)
                        {
                            b.SetPixel(w, h, bg);
                        }
                    }
                }

                if (!PathB)
                {
                    if (CollisionDir == 0)
                    {
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision1Up[w] <= 15 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision1Up[w] <= 14 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision1Up[w] <= 13 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision1Up[w] <= 12 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision1Up[w] <= 11 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision1Up[w] <= 10 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision1Up[w] <= 9 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision1Up[w] <= 8 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision1Up[w] <= 7 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision1Up[w] <= 6 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision1Up[w] <= 5 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision1Up[w] <= 4 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision1Up[w] <= 3 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision1Up[w] <= 2 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision1Up[w] <= 1 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision1Up[w] <= 0 && Collision1UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }

                    if (CollisionDir == 1)
                    {
                        b.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision1Right[w] <= 15 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision1Right[w] <= 14 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision1Right[w] <= 13 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision1Right[w] <= 12 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision1Right[w] <= 11 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision1Right[w] <= 10 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision1Right[w] <= 9 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision1Right[w] <= 8 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision1Right[w] <= 7 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision1Right[w] <= 6 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision1Right[w] <= 5 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision1Right[w] <= 4 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision1Right[w] <= 3 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision1Right[w] <= 2 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision1Right[w] <= 1 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision1Right[w] <= 0 && Collision1RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }

                    if (CollisionDir == 2)
                    {
                        b.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision1Left[w] <= 15 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision1Left[w] <= 14 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision1Left[w] <= 13 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision1Left[w] <= 12 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision1Left[w] <= 11 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision1Left[w] <= 10 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision1Left[w] <= 9 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision1Left[w] <= 8 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision1Left[w] <= 7 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision1Left[w] <= 6 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision1Left[w] <= 5 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision1Left[w] <= 4 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision1Left[w] <= 3 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision1Left[w] <= 2 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision1Left[w] <= 1 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision1Left[w] <= 0 && Collision1LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }

                    if (CollisionDir == 3)
                    {
                        b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision1Down[w] <= 15 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision1Down[w] <= 14 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision1Down[w] <= 13 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision1Down[w] <= 12 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision1Down[w] <= 11 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision1Down[w] <= 10 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision1Down[w] <= 9 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision1Down[w] <= 8 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision1Down[w] <= 7 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision1Down[w] <= 6 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision1Down[w] <= 5 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision1Down[w] <= 4 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision1Down[w] <= 3 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision1Down[w] <= 2 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision1Down[w] <= 1 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision1Down[w] <= 0 && Collision1DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }
                }

                if (PathB)
                {
                    if (CollisionDir == 0)
                    {
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision2Up[w] <= 15 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision2Up[w] <= 14 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision2Up[w] <= 13 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision2Up[w] <= 12 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision2Up[w] <= 11 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision2Up[w] <= 10 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision2Up[w] <= 9 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision2Up[w] <= 8 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision2Up[w] <= 7 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision2Up[w] <= 6 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision2Up[w] <= 5 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision2Up[w] <= 4 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision2Up[w] <= 3 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision2Up[w] <= 2 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision2Up[w] <= 1 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision2Up[w] <= 0 && Collision2UpSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }

                    if (CollisionDir == 1)
                    {
                        b.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision2Right[w] <= 15 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision2Right[w] <= 14 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision2Right[w] <= 13 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision2Right[w] <= 12 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision2Right[w] <= 11 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision2Right[w] <= 10 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision2Right[w] <= 9 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision2Right[w] <= 8 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision2Right[w] <= 7 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision2Right[w] <= 6 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision2Right[w] <= 5 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision2Right[w] <= 4 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision2Right[w] <= 3 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision2Right[w] <= 2 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision2Right[w] <= 1 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision2Right[w] <= 0 && Collision2RightSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }

                    if (CollisionDir == 2)
                    {
                        b.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision2Left[w] <= 15 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision2Left[w] <= 14 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision2Left[w] <= 13 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision2Left[w] <= 12 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision2Left[w] <= 11 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision2Left[w] <= 10 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision2Left[w] <= 9 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision2Left[w] <= 8 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision2Left[w] <= 7 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision2Left[w] <= 6 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision2Left[w] <= 5 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision2Left[w] <= 4 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision2Left[w] <= 3 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision2Left[w] <= 2 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision2Left[w] <= 1 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision2Left[w] <= 0 && Collision2LeftSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }

                    if (CollisionDir == 3)
                    {
                        b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        for (int w = 0; w < 16; w++) //Set the Active/Main (FG) colour
                        {
                            if (Collision2Down[w] <= 15 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 15, fg);
                            }
                            if (Collision2Down[w] <= 14 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 14, fg);
                            }
                            if (Collision2Down[w] <= 13 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 13, fg);
                            }
                            if (Collision2Down[w] <= 12 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 12, fg);
                            }
                            if (Collision2Down[w] <= 11 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 11, fg);
                            }
                            if (Collision2Down[w] <= 10 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 10, fg);
                            }
                            if (Collision2Down[w] <= 9 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 9, fg);
                            }
                            if (Collision2Down[w] <= 8 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 8, fg);
                            }
                            if (Collision2Down[w] <= 7 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 7, fg);
                            }
                            if (Collision2Down[w] <= 6 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 6, fg);
                            }
                            if (Collision2Down[w] <= 5 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 5, fg);
                            }
                            if (Collision2Down[w] <= 4 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 4, fg);
                            }
                            if (Collision2Down[w] <= 3 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 3, fg);
                            }
                            if (Collision2Down[w] <= 2 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 2, fg);
                            }
                            if (Collision2Down[w] <= 1 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 1, fg);
                            }
                            if (Collision2Down[w] <= 0 && Collision2DownSolid[w] > 0)
                            {
                                b.SetPixel(w, 0, fg);
                            }
                        }
                    }
                }

                return b;
            }
        }

    }
}
