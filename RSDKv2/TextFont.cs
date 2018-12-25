using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class TextFont
    {

        public TextFont()
        {

        }

        public TextFont(Reader reader)
        {
            int result = 0;

            for (int i = 0; ; i += 5)
            {
                if (reader.IsEof)
                {
                    break;
                }

                reader.ReadInt32();

                reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();

                reader.ReadByte();
                byte b = reader.ReadByte();
                if (b <= 0x80u)
                {
                    //Combine it
                }
                else
                {
                    //idk lol
                }

                byte b2 = reader.ReadByte();
                reader.ReadByte();

                if (b2 > 0x80u)
                {
                    //Combine it
                }
                else
                {
                    //idk lol
                }

                byte b3 = reader.ReadByte();
                reader.ReadByte();

                if (b3 > 0x80u)
                {
                    //Combine it
                }
                else
                {
                    //idk lol
                }

                reader.ReadByte(); //this do jack shit lmao
                result = reader.ReadByte(); //get reader pos?
            }
        }

        public void Write(Writer writer)
        {

        }

    }
}
