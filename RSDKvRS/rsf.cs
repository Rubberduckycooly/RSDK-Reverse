using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvRS
{
    public class rsf
    {
        //-----Notes Section!-----//
        //A Set of "FF FF FF FF" signals the end of something
        //ALL Scripts end with 6 bytes of "00"
        //There are only 5 sets of "FF FF FF FF" Maybe 5 functions?

        int v2; // ST78_4
        int v3; // ST68_4
        int v4; // ST60_4
        int v6; // [esp+50h] [ebp-8Ch]
        int v7; // [esp+54h] [ebp-88h]
        int v8; // [esp+58h] [ebp-84h]
        int v9; // [esp+58h] [ebp-84h]
        int v10; // [esp+58h] [ebp-84h]
        int v11; // [esp+5Ch] [ebp-80h]
        int v12; // [esp+60h] [ebp-7Ch]
        int i; // [esp+64h] [ebp-78h]
        int j; // [esp+68h] [ebp-74h]
        int l; // [esp+68h] [ebp-74h]
        int m; // [esp+68h] [ebp-74h]
        int v17; // [esp+6Ch] [ebp-70h]
        int v18; // [esp+6Ch] [ebp-70h]
        int v19; // [esp+6Ch] [ebp-70h]
        int v20; // [esp+6Ch] [ebp-70h]
        int v21; // [esp+6Ch] [ebp-70h]
        int v22; // [esp+70h] [ebp-6Ch]
        byte v23; // [esp+74h] [ebp-68h]
        byte v24; // [esp+74h] [ebp-68h]
        byte v25; // [esp+74h] [ebp-68h]
        int v26; // [esp+78h] [ebp-64h]
        int v27; // [esp+7Ch] [ebp-60h]
        int k; // [esp+80h] [ebp-5Ch]
        char v29; // [esp+84h] [ebp-58h]
        int v30; // [esp+D4h] [ebp-8h]
        int v31; // [esp+D8h] [ebp-4h]

        public rsf(string filename): this(new Reader(filename))
        { }

        /*public rsf(Reader reader)
        {
            bool read = true;
            while (read)
            {
                byte b = reader.ReadByte();
                Function1.Add(b);
                if (b == 255)
                {
                    byte b2 = reader.ReadByte();
                    Function1.Add(b2);
                    if (b2 == 255)
                    {
                        byte b3 = reader.ReadByte();
                        Function1.Add(b3);
                        if (b3 == 255)
                        {
                            byte b4 = reader.ReadByte();
                            Function1.Add(b4);
                            if (b4 == 255)
                            {
                                Console.WriteLine(reader.Pos);
                                read = false;
                            }
                        }
                    }
                }
            }

            read = true;
            while (read)
            {
                byte b = reader.ReadByte();
                Function2.Add(b);
                if (b == 255)
                {
                    byte b2 = reader.ReadByte();
                    Function2.Add(b2);
                    if (b2 == 255)
                    {
                        byte b3 = reader.ReadByte();
                        Function2.Add(b3);
                        if (b3 == 255)
                        {
                            byte b4 = reader.ReadByte();
                            Function2.Add(b4);
                            if (b4 == 255)
                            {
                                Console.WriteLine(reader.Pos);
                                read = false;
                            }
                        }
                    }
                }
            }

            read = true;
            while (read)
            {
                byte b = reader.ReadByte();
                Function3.Add(b);
                if (b == 255)
                {
                    byte b2 = reader.ReadByte();
                    Function3.Add(b2);
                    if (b2 == 255)
                    {
                        byte b3 = reader.ReadByte();
                        Function3.Add(b3);
                        if (b3 == 255)
                        {
                            byte b4 = reader.ReadByte();
                            Function3.Add(b4);
                            if (b4 == 255)
                            {
                                Console.WriteLine(reader.Pos);
                                read = false;
                            }
                        }
                    }
                }
            }

            read = true;
            while (read)
            {
                byte b = reader.ReadByte();
                Function4.Add(b);
                if (b == 255)
                {
                    byte b2 = reader.ReadByte();
                    Function4.Add(b2);
                    if (b2 == 255)
                    {
                        byte b3 = reader.ReadByte();
                        Function4.Add(b3);
                        if (b3 == 255)
                        {
                            byte b4 = reader.ReadByte();
                            Function4.Add(b4);
                            if (b4 == 255)
                            {
                                Console.WriteLine(reader.Pos);
                                read = false;
                            }
                        }
                    }
                }
            }

            read = true;
            while (read)
            {
                byte b = reader.ReadByte();
                Function5.Add(b);
                if (b == 255)
                {
                    byte b2 = reader.ReadByte();
                    Function5.Add(b2);
                    if (b2 == 255)
                    {
                        byte b3 = reader.ReadByte();
                        Function5.Add(b3);
                        if (b3 == 255)
                        {
                            byte b4 = reader.ReadByte();
                            Function5.Add(b4);
                            if (b4 == 255)
                            {
                                Console.WriteLine(reader.Pos);
                                read = false;
                            }
                        }
                    }
                }
            }

            SixBytesUnknown = reader.ReadBytes(6);

        }*/

        public rsf(Reader reader)
        {
            byte readbyte;
            for (int i = 0; i < 4; i++)
            {
                reader.ReadByte();
                //v30 = readbyte << 8;
                reader.ReadByte();
                //v30 += readbyte;

                if (i != 0)
                {
                    if (i == 1)
                    {
                        //dword_AFFA98[16129 * a2] = v30;
                        //memset(&dword_AF4A94[16129 * a2], 0, 0xFFFu);
                    }
                    else if (i == 2)
                    {
                        //dword_AFFA9C[16129 * a2] = v30;
                        //memset(&dword_AF8A94[16129 * a2], 0, 0xFFFu);
                    }
                }
                else
                {
                    //dword_AFFA94[16129 * a2] = v30;
                    //memset(&dword_AF0A94[16129 * a2], 0, 0xFFFu);
                }
                int k = 0;

                byte OpCode = 0;

                while (k == 0)
                {
                    byte b1 = reader.ReadByte();
                    if (b1 == 255)
                    {
                        byte b2 = reader.ReadByte();
                        if (b2 == 255)
                        {
                            byte b3 = reader.ReadByte();
                            if (b3 == 255)
                            {
                                byte b4 = reader.ReadByte();
                                if (b4 == 255)
                                {
                                    k = 1;
                                }
                                else
                                {
                                    OpCode = b4;
                                }
                            }
                            else
                            {
                                OpCode = b3;
                            }
                        }
                        else
                        {
                            OpCode = b2;
                        }
                    }
                    else
                    {
                        OpCode = b1;
                    }

                    if (k < 1)
                    {
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                //dword_AF4A94[16129 * a2 + v17] = readbyte;
                            }
                            else if (i == 2)
                            {
                                //dword_AF8A94[16129 * a2 + v17] = readbyte;
                            }
                        }
                        else
                        {
                            //dword_AF0A94[16129 * a2 + v17] = readbyte;
                        }
                        byte UnknownByte = 0;
                        switch (OpCode)
                        {
                            case 0x0:
                                break;
                            case 0x4:
                                break;
                            case 0x5:
                                break;
                            case 0xA:
                                break;
                            case 0xB:
                                break;
                            case 0xD:
                                break;
                            case 0x23:
                                break;
                            case 0x24:
                                break;
                            case 0x28:
                                break;
                            case 0x2A:
                                break;
                            case 0x2C:
                                break;
                            case 0x2D:
                                break;
                            case 0x31:
                                break;
                            case 0x34:
                                break;
                            case 0x3E:
                                break;
                            case 0x3F:
                                break;
                            case 0x42:
                                UnknownByte = 1;
                                break;
                            case 0x1:
                                break;
                            case 0x2:
                                break;
                            case 0x3:
                                break;
                            case 0x6:
                                break;
                            case 0x7:
                                break;
                            case 0x8:
                                break;
                            case 0x9:
                                break;
                            case 0xC:
                                break;
                            case 0x30:
                                break;
                            case 0x41:
                                UnknownByte = 2;
                                break;
                            case 0xE:
                                UnknownByte = 6;
                                break;
                            case 0xF:
                                break;
                            case 0x10:
                                break;
                            case 0x39:
                                break;
                            case 0x3A:
                                break;
                            case 0x3B:
                                break;
                            case 0x11:
                                UnknownByte = 6;
                                break;
                            case 0x12:
                                UnknownByte = 1;
                                break;
                            case 0x13:
                                break;
                            case 0x14:
                                break;
                            case 0x15:
                                break;
                            case 0x16:
                                break;
                            case 0x17:
                                break;
                            case 0x18:
                                break;
                            case 0x2E:
                                break;
                            case 0x37:
                                break;
                            case 0x38:
                                break;
                            case 0x3C:
                                break;
                            case 0x3D:
                                UnknownByte = 4;
                                break;
                            case 0x19:
                                UnknownByte = 2;
                                break;
                            case 0x1A:
                                UnknownByte = 2;
                                break;
                            case 0x1B:
                                UnknownByte = 2;
                                break;
                            case 0x1C:
                                UnknownByte = 2;
                                break;
                            case 0x1D:
                                UnknownByte = 2;
                                break;
                            case 0x1E:
                                UnknownByte = 2;
                                break;
                            case 0x1F:
                                UnknownByte = 4;
                                break;
                            case 0x20:
                                UnknownByte = 4;
                                break;
                            case 0x21:
                                UnknownByte = 4;
                                break;
                            case 0x22:
                                UnknownByte = 4;
                                break;
                            case 0x25:
                                UnknownByte = 3;
                                break;
                            case 0x26:
                                UnknownByte = 3;
                                break;
                            case 0x27:
                                UnknownByte = 2;
                                break;
                            case 0x29:
                                break;
                            case 0x40:
                                UnknownByte = 6;
                                break;
                            case 0x2B:
                                UnknownByte = 4;
                                break;
                            case 0x2F:
                                UnknownByte = 5;
                                break;
                            case 0x35:
                                UnknownByte = 7;
                                break;
                            case 0x36:
                                UnknownByte = 8;
                                break;
                            default:
                                break;
                        }

                        for (int j = 0; i < UnknownByte; j++)
                        {
                            readbyte = reader.ReadByte();
                            if (i != 0)
                            {
                                if (i == 1)
                                {
                                    //dword_AF4A94[16129 * a2 + v17] = readbyte;
                                }
                                else if (i == 2)
                                {
                                    //dword_AF8A94[16129 * a2 + v17] = readbyte;
                                }
                            }
                            else
                            {
                                //dword_AF0A94[16129 * a2 + v17] = readbyte;
                            }
                            //v18 = v17 + 1;

                            if (readbyte != 0)
                            {
                                readbyte = reader.ReadByte();
                                if (i != 0)
                                {
                                    if (i == 1)
                                    {
                                        //dword_AF4A94[16129 * a2 + v17] = readbyte;
                                    }
                                    else if (i == 2)
                                    {
                                        //dword_AF8A94[16129 * a2 + v17] = readbyte;
                                    }
                                }
                                else
                                {
                                    //dword_AF0A94[16129 * a2 + v17] = readbyte;
                                }
                                //v19 = v18 + 1;
                                readbyte = reader.ReadByte();

                                //v30 = readbyte;
                                if (readbyte > 128 )
                                {
                                //v30 = 128 - v30;
                                }
                                if (i != 0)
                                {
                                    if (i == 1)
                                    {
                                        //dword_AF4A94[16129 * a2 + v17] = readbyte;
                                    }
                                    else if (i == 2)
                                    {
                                        //dword_AF8A94[16129 * a2 + v17] = readbyte;
                                    }
                                }
                                else
                                {
                                    //dword_AF0A94[16129 * a2 + v17] = readbyte;
                                }
                                //v17 = v19 + 1;
                            }
                            else
                            {
                                readbyte = reader.ReadByte();
                                if (readbyte < 128)
                                {
                                    //v30 = readbyte << 8;
                                    readbyte = reader.ReadByte();
                                    //v30 += readbyte;
                                }
                                else
                                {
                                    //LOBYTE(v26) = v26 + -128;
                                    //v30 = readbyte << 8;
                                    readbyte = reader.ReadByte();
                                    //v30 += readbyte;
                                    //v30 = -v30;
                                }
                                switch (i)
                                {
                                    case 0:
                                        //dword_AF0A94[16129 * a2 + v18] = v30;
                                        break;
                                    case 1:
                                        //dword_AF4A94[16129 * a2 + v18] = v30;
                                        break;
                                    case 2:
                                        //dword_AF8A94[16129 * a2 + v18] = v30;
                                        break;
                                    case 3:
                                        /*if ((unsigned __int8)v27 == 17)
                                        {
                                            switch(j)
                                            {
                    case 0:
                        //byte_AFFC9E[64516 * a2 + v22] = v30;
                        break;
                    case 1:
                        //byte_AFFD9D[64516 * a2 + v22] = v30;
                        break;
                    case 2:
                        //dword_AFFE9C[16129 * a2 + v22] = v30;
                        break;
                    case 3:
                        //dword_B00298[16129 * a2 + v22] = v30;
                        break;
                    case 4:
                        //byte_AFFB9F[64516 * a2 + v22] = v30;
                        break;
                    case 5:
                        //byte_AFFAA0[64516 * a2 + v22++] = v30;
                        break;
                    default:
                        goto LABEL_76;
                        break;
                                            }
                                        }*/
                                        break;
                                    default:
                                        break;
                                }
                            LABEL_76:
                                v17 = v18 + 1;
                            }
                        }
                    }
                }

                readbyte = reader.ReadByte();
                readbyte = reader.ReadByte();
                byte tmp = readbyte;

                for (int t = 0; t < tmp; t++)
                {
                    readbyte = reader.ReadByte();
                    readbyte = reader.ReadByte();
                }
                readbyte = reader.ReadByte();
                readbyte = reader.ReadByte();

                if (i != 0)
                {
                    if (i == 1)
                    {
                        //dword_AF4A94[16129 * a2 + v17] = readbyte;
                    }
                    else if (i == 2)
                    {
                        //dword_AF8A94[16129 * a2 + v17] = readbyte;
                    }
                }
                else
                {
                    //dword_AF0A94[16129 * a2 + v17] = readbyte;
                }

                k = 0;
                readbyte = reader.ReadByte();
                readbyte = reader.ReadByte();
                v23 = readbyte;
                v8 = 0;
                for (k = 0; k < 512; k++)
                {
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AF4A94[16129 * a2 + v17] = readbyte;
                        }
                        else if (i == 2)
                        {
                            //dword_AF8A94[16129 * a2 + v17] = readbyte;
                        }
                    }
                    else
                    {
                        //dword_AF0A94[16129 * a2 + v17] = readbyte;
                    }
                }
                for (k = 0; k < v23; k++)
                {
                    v12 = 0x8000;
                    v6 = 0;
                    readbyte = reader.ReadByte();
                    //v31 = readbyte << 8;
                    readbyte = reader.ReadByte();
                    //v31 += readbyte;
                    readbyte = reader.ReadByte();
                    v3 = readbyte << 8;
                    readbyte = reader.ReadByte();
                    v11 = readbyte + v3;
                    //v21 = v8;
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AFEA94[16129 * a2 + v8++] = 0;
                            //dword_AFEA94[16129 * a2 + v8] = 0;
                        }
                        else if (i == 2)
                        {
                            //dword_AFF294[16129 * a2 + v8++] = 0;
                            //dword_AFF294[16129 * a2 + v8] = 0;
                        }
                    }
                    else
                    {
                        //dword_AFE294[16129 * a2 + v8++] = 0;
                        //dword_AFE294[16129 * a2 + v8] = 0;
                    }
                    v9 = v8 + 1;
                    readbyte = reader.ReadByte();
                    //v31 = readbyte << 8;
                    readbyte = reader.ReadByte();
                    //v31 += readbyte;
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AFEA94[16129 * a2 + v8++] = 0;
                            //dword_AFEA94[16129 * a2 + v8] = 0;
                        }
                        else if (i == 2)
                        {
                            //dword_AFF294[16129 * a2 + v8++] = 0;
                            //dword_AFF294[16129 * a2 + v8] = 0;
                        }
                    }
                    else
                    {
                        //dword_AFE294[16129 * a2 + v8++] = 0;
                        //dword_AFE294[16129 * a2 + v8] = 0;
                    }
                    readbyte = reader.ReadByte();
                    readbyte = reader.ReadByte();
                    readbyte = reader.ReadByte();
                    //v31 = readbyte << 8;
                    readbyte = reader.ReadByte();
                    //v31 += readbyte;
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AFEA94[16129 * a2 + v8++] = 0;
                            //dword_AFEA94[16129 * a2 + v8] = 0;
                        }
                        else if (i == 2)
                        {
                            //dword_AFF294[16129 * a2 + v8++] = 0;
                            //dword_AFF294[16129 * a2 + v8] = 0;
                        }
                    }
                    else //if(!dword_AFE294[16129 * a2 + v9])
                    {
                        //dword_AFE294[16129 * a2 + v8++] = 0;
                        //dword_AFE294[16129 * a2 + v8] = 0;
                    }
                    v10 = v9 + 1;
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AFEA94[16129 * a2 + v8++] = 0;
                            //dword_AFEA94[16129 * a2 + v8] = 0;
                        }
                        else if (i == 2)
                        {
                            //dword_AFF294[16129 * a2 + v8++] = 0;
                            //dword_AFF294[16129 * a2 + v8] = 0;
                        }
                    }
                    else //if(!dword_AFE294[16129 * a2 + v9])
                    {
                        //dword_AFE294[16129 * a2 + v8++] = 0;
                        //dword_AFE294[16129 * a2 + v8] = 0;
                    }
                    v8 = v10 + 1;
                    for (l = 0; l < v11; ++l)
                    {
                        readbyte = reader.ReadByte();
                        //v31 = readbyte << 8;
                        readbyte = reader.ReadByte();
                        //v31 += readbyte;
                        readbyte = reader.ReadByte();
                        //v31 = readbyte << 8;
                        readbyte = reader.ReadByte();
                        //v31 += readbyte;
                        if (v7 < v12)
                        {
                            v12 = v7;
                        }
                        if (v7 > v6)
                        {
                            v6 = v7;
                        }
                        readbyte = reader.ReadByte();
                        readbyte = reader.ReadByte();
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                //dword_AFEA94[16129 * a2 + v8++] = 0;
                            }
                            else if (i == 2)
                            {
                                //dword_AFF294[16129 * a2 + v8++] = 0;
                            }
                        }
                        else
                        {
                            //dword_AFE294[16129 * a2 + v8++] = 0;
                        }
                    }

                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AFEA94[16129 * a2 + v8++] = 0;
                            //dword_AFEA94[16129 * a2 + v8] = 0;
                        }
                        else if (i == 2)
                        {
                            //dword_AFF294[16129 * a2 + v8++] = 0;
                            //dword_AFF294[16129 * a2 + v8] = 0;
                        }
                    }
                    else
                    {
                        //dword_AFE294[16129 * a2 + v8++] = 0;
                        //dword_AFE294[16129 * a2 + v8] = 0;
                    }

                    for (m = v12 - 1; m < v6; m++)
                    {
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                //dword_AFEA94[16129 * a2 + v8++] = 0;
                                //dword_AFEA94[16129 * a2 + v8] = 0;
                            }
                            else if (i == 2)
                            {
                                //dword_AFF294[16129 * a2 + v8++] = 0;
                                //dword_AFF294[16129 * a2 + v8] = 0;
                            }
                        }
                        else
                        {
                            //dword_AFE294[16129 * a2 + v8++] = 0;
                            //dword_AFE294[16129 * a2 + v8] = 0;
                        }
                        v8++;
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                //dword_AFEA94[16129 * a2 + v8++] = 0;
                                //dword_AFEA94[16129 * a2 + v8] = 0;
                            }
                            else if (i == 2)
                            {
                                //dword_AFF294[16129 * a2 + v8++] = 0;
                                //dword_AFF294[16129 * a2 + v8] = 0;
                            }
                        }
                        else
                        {
                            //dword_AFE294[16129 * a2 + v8++] = 0;
                            //dword_AFE294[16129 * a2 + v8] = 0;
                        }
                        //else //if (dword_AF0A98[16129 * a2 + v31] <= 0)
                        //{
                           //dword_AF0AA4[16129 * a2 + v31] = v21;
                        //}

                    }
                }
                k = 0;
            }
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

            writer.Close();
        }

    }
}
