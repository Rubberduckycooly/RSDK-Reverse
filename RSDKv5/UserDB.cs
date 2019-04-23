using System;
using System.Collections.Generic;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class UserDB
    {

        public class Entry
        {
            public List<int> Values = new List<int>();
            public List<string> ValueNames = new List<string>();
            public Entry()
            {

            }

            public Entry(Reader reader, UserDB db)
            {
                for (int i = 0; i < 24; i++)
                {
                    Values.Add(reader.ReadInt32());
                }
                ValueNames = db.ValueNames;
            }

            public void Write(Writer writer)
            {
                for (int i = 0; i < Values.Count; i++)
                {
                    writer.Write(Values[i]);
                }
            }
        }

        public List<Entry> Entries = new List<Entry>();
        public List<string> ValueNames = new List<string>();
        public List<int> ValueIDs = new List<int>();
        public byte ValuesPerEntry;
        public UserDB()
        {

        }

        public UserDB(Reader reader)
        {
            Reader creader = reader.GetCompressedStreamRaw();
            creader.BaseStream.Position += 8;

            ushort EntryCount = creader.ReadUInt16();
            ValuesPerEntry = creader.ReadByte();

            for (int i = 0; i < ValuesPerEntry; i++)
            {
                ValueIDs.Add(creader.ReadByte());

                string str = "";
                char c = 'n';
                int count = 0;
                while(c != 0)
                {
                    c = creader.ReadChar();
                    count++;
                    if (c != 0)
                    {
                        str += c;
                    }
                }
                ValueNames.Add(str);
                creader.ReadBytes(16 - count);
            }

            for (int i = 0; i < EntryCount; i++)
            {
                Entries.Add(new Entry(creader, this));
            }

            

            byte[] array = new byte[creader.BaseStream.Length];
            creader.BaseStream.Position = 0;
            creader.Read(array, 0, (int)creader.BaseStream.Length);
            Writer writer = new Writer("UserDB.bin");
            writer.Write(array);
            writer.Close();
            creader.Close();
            reader.Close();
               
        }

        public void Write(Writer writer)
        {

        }

    }
}
