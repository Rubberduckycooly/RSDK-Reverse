using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RetroED.Tools.RSDKCollisionEditor.Classic
{
    public class CollisionArray
    {
        public class CollisionMask
        {
            public byte[] Collision = new byte[16];
            public byte[] CollisionConfig = new byte[16];

            public CollisionMask(BinaryReader reader)
            {
                //for (int c = 15; c >= 0; c--)
                for (int c = 0; c < 16; c++)
                {
                    byte col = reader.ReadByte();
                    CollisionConfig[c] = (byte)((col & 0xF0) >> 4);
                    byte colval = (byte)(col & 0x0F);

                    if (CollisionConfig[c] == 0xf)
                    {
                        Collision[SwapVal((byte)c)] = colval;
                    }
                    else if (CollisionConfig[c] == 0x1)
                    {
                        Collision[c] = 0x0;
                    }
                    else if (CollisionConfig[c] == 0x0)
                    {
                        Collision[c] = SwapVal(colval);
                    }
                }
            }

            public byte SwapVal(byte val)
            {
                switch (val)
                {
                    case 0:
                        val = 15;
                        break;
                    case 1:
                        val = 14;
                        break;
                    case 2:
                        val = 13;
                        break;
                    case 3:
                        val = 12;
                        break;
                    case 4:
                        val = 11;
                        break;
                    case 5:
                        val = 10;
                        break;
                    case 6:
                        val = 9;
                        break;
                    case 7:
                        val = 8;
                        break;
                    case 8:
                        val = 7;
                        break;
                    case 9:
                        val = 6;
                        break;
                    case 10:
                        val = 5;
                        break;
                    case 11:
                        val = 4;
                        break;
                    case 12:
                        val = 3;
                        break;
                    case 13:
                        val = 2;
                        break;
                    case 14:
                        val = 1;
                        break;
                    case 15:
                        val = 0;
                        break;
                }
                return val;
            }

            public void Write(BinaryWriter writer)
            {
                //writer.Write(Collision);
            }

        }

        public CollisionMask[] CollisionMasks = new CollisionMask[256];

        public CollisionArray(BinaryReader reader)
        {
            for (int i = 0; i < 256; i++)
            {
                CollisionMasks[i] = new CollisionMask(reader);
            }
            reader.Close();
        }

        public void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 256; i++)
            {
                CollisionMasks[i].Write(writer);
            }
            writer.Close();
        }

    }
}
