using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class StringSet
    {
        string stringList = "";

        public StringSet()
        {

        }

        public StringSet(Reader reader)
        {
            while(!reader.IsEof)
            {
                char c = (char)reader.ReadByte();
                if (c != 0)
                {
                    stringList += c;
                }
            }
            reader.Close();
        }

        public void Write(Writer writer)
        {
            for (int i = 0; i < stringList.Length; i++)
            {
                writer.Write((byte)stringList[i]);
                writer.Write((byte)0);
            }
        }

        public string getString(string str)
        {
            if (stringList.Contains(str))
            {
                return str;
            }
            else
            {
                return "STRING NOT FOUND!";
            }
        }

        public void addString(string str)
        {
            stringList += str;
        }

    }
}
