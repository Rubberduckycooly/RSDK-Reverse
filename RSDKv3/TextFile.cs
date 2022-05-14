using System;
using System.Collections.Generic;
using System.Text;

namespace RSDKv3
{
    public class TextFile
    {
        public List<List<ushort>> rows = new List<List<ushort>>();
        public bool use16BitChars = false;

        public TextFile() { }

        public TextFile(string filename) : this(new Reader(filename)) { }

        public TextFile(System.IO.Stream stream) : this(new Reader(stream)) { }

        public TextFile(Reader reader) { Read(reader); }

        public void Read(Reader reader)
        {
            rows.Clear();
            List<ushort> rowBuffer = new List<ushort>();
            bool finished = false;

            byte buffer = reader.ReadByte();
            use16BitChars = buffer == 0xFF;
            if (use16BitChars)
            {
                reader.ReadByte();
                while (!finished)
                {
                    ushort character = reader.ReadByte();
                    character |= (ushort)(reader.ReadByte() << 8);

                    if (character == '\r')
                    {
                        if (rows.Count >= 512)
                        {
                            finished = true;
                        }
                        else
                        {
                            rows.Add(rowBuffer);
                            rowBuffer = new List<ushort>();
                        }
                    }
                    else
                    {
                        rowBuffer.Add(character);
                    }

                    if (!finished)
                        finished = reader.BaseStream.Position >= reader.BaseStream.Length;
                }
            }
            else
            {
                byte character = buffer;
                if (character == '\r')
                {
                    rows.Add(rowBuffer);
                    rowBuffer = new List<ushort>();
                }
                else
                {
                    rowBuffer.Add(character);
                }

                while (!finished)
                {
                    character = reader.ReadByte();
                    if (character == '\r')
                    {
                        if (rows.Count >= 512)
                        {
                            finished = true;
                        }
                        else
                        {
                            rows.Add(rowBuffer);
                            rowBuffer = new List<ushort>();
                        }
                    }
                    else
                    {
                        rowBuffer.Add(character);
                    }

                    if (!finished)
                        finished = reader.BaseStream.Position >= reader.BaseStream.Length;
                }
            }

            rows.Add(rowBuffer);
            rowBuffer.Clear();

            reader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            for (int i = 0; i < rows.Count; ++i)
            {
                foreach (ushort character in rows[i])
                {
                    if (use16BitChars)
                        writer.Write(character);
                    else
                        writer.Write((byte)character);
                }

                if (use16BitChars)
                    writer.Write((ushort)'\r');
                else
                    writer.Write('\r');
            }

            writer.Close();
        }

    }
}
