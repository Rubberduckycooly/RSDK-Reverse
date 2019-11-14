using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKvB
{
    public class StaticObject
    {

        public List<int[]> Data = new List<int[]>();

        public StaticObject()
        {
        }

        public StaticObject(string filepath) : this(new Reader(filepath))
        {

        }

        public StaticObject(System.IO.Stream strm) : this(new Reader(strm))
        {

        }

        public StaticObject(Reader reader)
        {
            while(!reader.IsEof)
            {
                int size = reader.ReadInt32();
                int[] DataSet = new int[size];
                for (int i = 0; i < size; i++)
                {
                    DataSet[i] = reader.ReadInt32();
                }
                Data.Add(DataSet);
            }
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


        public void Write(Writer writer)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                writer.Write(Data[i].Length);
                for (int p = 0; p < Data[i].Length; p++)
                {
                    writer.Write(Data[i][p]);
                }
            }
            writer.Close();
        }
    }
}
