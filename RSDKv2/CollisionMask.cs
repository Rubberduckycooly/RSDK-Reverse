using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{

    public static class ExtensionMethods
    {
        public static int LowByte(this int number)
        { return number & 0x00FF; }

        public static int HighByte(this int number)
        { return number & 0xFF00; }

        public static int LowWord(this int number)
        { return number & 0x0000FFFF; }

        public static int LowWord(this int number, int newValue)
        { return (int)((number & 0xFFFF0000) + (newValue & 0x0000FFFF)); }

        public static int HighWord(this int number)
        { return (int)(number & 0xFFFF0000); }

        public static int HighWord(this int number, int newValue)
        { return (number & 0x0000FFFF) + (newValue << 16); }
    }

    public class CollisionMask
    {

        const int TILES_COUNT = 1024;

        public TileConfig[] Collision = new TileConfig[TILES_COUNT];
        public TileConfig[] Collision2 = new TileConfig[TILES_COUNT];

        public class TileConfig
        {

            byte v1; // [esp+Ch] [ebp-ACh]
            int v2; // [esp+4Ch] [ebp-6Ch]
            int v3; // [esp+50h] [ebp-68h]
            int v4; // [esp+54h] [ebp-64h]
            int v5; // [esp+58h] [ebp-60h]
            int j; // [esp+5Ch] [ebp-5Ch]
            int k; // [esp+60h] [ebp-58h]
            int i; // [esp+64h] [ebp-54h]
            byte v9; // [esp+68h] [ebp-50h]

            // Collision position for each pixel
            public byte[] Collision;

            // If has collision
            //public bool[] HasCollision;

            // Unknown 5 bytes config
            public byte[] Config; //Some of these affect collision

            //Slope is one value
            //Momentum is another

            //Unknown Byte Values
            public byte[] Unknown;

            public TileConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

            internal TileConfig(Reader reader)
            {
                v3 = reader.ReadInt32();
                Console.WriteLine(Convert.ToString(v3, 2));
                v4 = (byte)v3 >> 4;
                Console.WriteLine(Convert.ToString(v4, 2));
                int UnknownByte1; //Likley the config bytes
                int UnknownDWORD1;
                //int UnknownDWORD2;
                //int UnknownDWORD3;
                //int UnknownDWORD4;

                UnknownByte1 = v3 & 0xF;
                //Console.WriteLine(Convert.ToString(UnknownByte1, 2));
                v3 = reader.ReadInt32();
                UnknownDWORD1 = v3;
                //Console.WriteLine(Convert.ToString(UnknownDWORD1, 2));
                v3 = reader.ReadByte();
                UnknownDWORD1 += v3 << 8;
                //Console.WriteLine(Convert.ToString(UnknownDWORD1, 2));
                v3 = reader.ReadByte();
                UnknownDWORD1 += v3 << 16;
                //Console.WriteLine(Convert.ToString(UnknownDWORD1, 2));
                v3 = reader.ReadByte();
                UnknownDWORD1 += v3 << 24;
                //Console.WriteLine(Convert.ToString(UnknownDWORD1, 2));
                if (v4 != 0)
                {
                    for (k = 0; k < 16; k += 2)
                    {
                        v3 = reader.ReadByte();
                        int b = v3 >> 4;
                        int b2 = v3 & 0xF;
                        Console.WriteLine(Convert.ToString(b, 2) + " " + Convert.ToString(b2, 2));
                    }

                    v3 = reader.ReadByte();
                    v2 = 1;
                    int Unknown1;
                    int Unknown2;
                    for (k = 0; k < 8; k++)
                    {
                        if ((byte)(v2 & v3) >= 1)
                        {
                            Unknown1 = 0;
                        }
                        else
                        {
                            Unknown1 = 64;
                            Unknown2 = -64;
                        }
                        v2 = ExtensionMethods.LowByte(v2);
                        v2 = 2 * v2;
                    }

                    v3 = reader.ReadByte();
                    v2 = 1;
                    int Unknown3;
                    int Unknown4 = 0;
                    for (k = 0; k < 8; k++)
                    {
                        if ((byte)(v2 & v3) >= 1)
                        {
                            Unknown3 = 0;
                        }
                        else
                        {
                            Unknown3 = 64;
                            Unknown4 = -64;
                        }
                        v2 = ExtensionMethods.LowByte(v2);
                        v2 = 2 * v2;
                    }

                    v3 = 0;
                    byte Unknown5;
                    int Unknown6;
                    while ((byte)v3 < 16)
                    {
                        k = 0;
                        while (k > -1)
                        {
                            if (k == 16)
                            {
                                Unknown5 = 64;
                                k = -1;
                            }
                            else if ((byte)v3 > (int)Unknown4)
                            {
                                ++k;
                            }
                            else
                            {
                                Unknown5 = (byte)k;
                                k = -1;
                            }
                        }
                        v3 = ExtensionMethods.LowByte(v3);
                        v3 = v3 + 1;
                    }
                    v3 = 0;
                    while ((byte)v3 < 16)
                    {
                        k = 15;
                        while (k < 16)
                        {
                            if (k == -1)
                            {
                                Unknown6 = -64;
                                k = 16;
                            }
                            else if ((byte)v3 > Unknown4)
                            {
                                --k;
                            }
                            else
                            {
                                Unknown6 = (byte)k;
                                k = 16;
                            }
                        }
                        v3 = ExtensionMethods.LowByte(v3);
                        v3 = v3 + 1;
                    }

                }
            }

            public void Write(Writer writer)
            {
            }
        }

        public CollisionMask(string filename) : this(new Reader(filename))
        {

        }

        public CollisionMask(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        private CollisionMask(Reader reader)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
                Collision[i] = new TileConfig(reader);
            for (int i = 0; i < TILES_COUNT; ++i)
                Collision2[i] = new TileConfig(reader);
            Console.WriteLine("Reader Pos = " + reader.Pos + " ,File Length = " + reader.BaseStream.Length);
            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        internal void Write(Writer writer)
        {
            for (int i = 0; i < TILES_COUNT; ++i)
                Collision[i].Write(writer);
            for (int i = 0; i < TILES_COUNT; ++i)
                Collision2[i].Write(writer);
            writer.Close();
        }
    }
}
