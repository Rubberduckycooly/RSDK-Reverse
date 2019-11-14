using System;
using System.Collections.Generic;
using System.IO;

namespace RSDKv2
{
    public class StringSet
    {
        List<string> strings = new List<string>();

        public StringSet()
        {

        }

        public StringSet(Reader reader)
        {
            int FileSize = (int)reader.BaseStream.Length;
            byte[] FileData = reader.ReadBytes(reader.BaseStream.Length);
            reader.Close();

            byte LanguageCount = FileData[0];
            short StringCount = (short)(FileData[2] + (FileData[1] << 8));

            int StartOffset = (2 * FileData[4]) * StringCount + 3;

            int ID = 0;
            while(true)
            {
                int Count = ((FileSize - StartOffset) / 2);
                if (ID >= Count) {
                    break;
                }
                byte v2 = FileData[2 * ID + StartOffset];
                FileData[2 * ID + StartOffset] = FileData[2 * ID + 1 + StartOffset];
                FileData[2 * ID + 1 + StartOffset] = v2;
                ID++;
            }

            for (int l = 0; l < LanguageCount; l++)
            {
                for (int s = 0; s < StringCount; s++)
                {
                    int Offset = 2 * FileData[4];
                    int v9 = (FileData[Offset * s + 3] << 24) | (FileData[Offset * s + 4] << 16) | (FileData[Offset * s + 5] << 8) | FileData[Offset * s + 6];
                    int v8 = Offset * s + 9;
                    int v10 = 2 * ((FileData[Offset * s + 7] << 8) | FileData[Offset * s + 8]);

                    for (int i = 0; i < l; i++)
                    {
                        v9 += v10;
                        int v4 = FileData[v8] << 8;
                        int v5 = v8 + 1;
                        int v6 = v4 | FileData[v5];
                        v8 = v5 + 1;
                        v10 = 2 * v6;
                    }

                    byte[] array = new byte[0xFF * 2];
                    ID = 0;
                    while(true)
                    {
                        byte A = FileData[v9];
                        byte B = FileData[v9 + 1];
                        if (A == 0 && B == 0)
                        {
                            break;
                        }
                        array[ID + 0] = A;
                        array[ID + 1] = B;
                        ID += 2;
                    }
                    strings.Add(System.Text.Encoding.Unicode.GetString(array));
                }
            }

        }

        public void Write(Writer writer)
        {

            writer.Close();
        }

    }
}
