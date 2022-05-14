using System;
using System.Collections.Generic;
using System.IO;
using zlib;

namespace RSDKv5
{
    public class UserDB
    {
        public class TimeStamp
        {
            public int tm_sec;
            public int tm_min;
            public int tm_hour;
            public int tm_mday;
            public int tm_mon;
            public int tm_year;
            public int tm_wday;
            public int tm_yday;
            public int tm_isdst;

            public TimeStamp() { }
            public TimeStamp(Reader reader)
            {
                Read(reader);
            }

            public void Read(Reader reader)
            {
                tm_sec = reader.ReadInt32();
                tm_min = reader.ReadInt32();
                tm_hour = reader.ReadInt32();
                tm_mday = reader.ReadInt32();
                tm_mon = reader.ReadInt32();
                tm_year = reader.ReadInt32();
                tm_wday = reader.ReadInt32();
                tm_yday = reader.ReadInt32();
                tm_isdst = reader.ReadInt32();
            }

            public void Write(Writer writer)
            {
                writer.Write(tm_sec);
                writer.Write(tm_min);
                writer.Write(tm_hour);
                writer.Write(tm_mday);
                writer.Write(tm_mon);
                writer.Write(tm_year);
                writer.Write(tm_wday);
                writer.Write(tm_yday);
                writer.Write(tm_isdst);
            }

            public override string ToString()
            {
                return $"{tm_mday}/{tm_mon}/{tm_year + 1900} at {tm_hour}:{tm_min}:{String.Format("{0:00}", tm_sec)}";
            }
        }

        public class TableColumn
        {
            public enum Types
            {
                INVALID = 0,
                UINT8 = 2,
                UINT16 = 3,
                UINT32 = 4,
                INT8 = 6,
                INT16 = 7,
                INT32 = 8,
                FLOAT = 10,
                STRING = 16,
            };

            public string name = "";
            public Types type = Types.INVALID;

            public TableColumn() { }

            public TableColumn(Reader reader) { Read(reader); }

            public void Read(Reader reader)
            {
                type = (Types)reader.ReadByte();
                name = System.Text.Encoding.Default.GetString(reader.ReadBytes(0x10)).Replace("\0", "");
            }

            public void Write(Writer writer)
            {
                writer.Write((byte)type);

                int i = 0;
                for (; i < name.Length && i < 0x10; ++i)
                    writer.Write((byte)name[i]);
                for (; i < 0x10; ++i)
                    writer.Write((byte)0x00);
            }
        }

        public class TableRow
        {
            public uint uuid = 0;
            public TimeStamp createDate = new TimeStamp();
            public TimeStamp modifyDate = new TimeStamp();
            public List<byte[]> entries = new List<byte[]>();

            public TableRow() { }

            public TableRow(Reader reader, UserDB db)
            {
                Read(reader, db);
            }

            public void Read(Reader reader, UserDB db)
            {
                uuid = reader.ReadUInt32();
                createDate.Read(reader);
                modifyDate.Read(reader);

                entries.Clear();
                for (int v = 0; v < db.columns.Count; ++v)
                    entries.Add(reader.ReadBytes(reader.ReadByte()));
            }

            public void Write(Writer writer)
            {
                writer.Write(uuid);
                createDate.Write(writer);
                modifyDate.Write(writer);

                foreach (byte[] entry in entries)
                {
                    writer.Write((byte)entry.Length);
                    writer.Write(entry);
                }
            }
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly uint signature = 0x80074B1E;

        /// <summary>
        /// column definitions
        /// </summary>
        public List<TableColumn> columns = new List<TableColumn>();

        /// <summary>
        /// entry rows
        /// </summary>
        public List<TableRow> rows = new List<TableRow>();

        public UserDB() { }

        public UserDB(string filename) : this(new Reader(filename)) { }
        public UserDB(Stream stream) : this(new Reader(stream)) { }

        public UserDB(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            Reader creader = reader.GetCompressedStreamRaw();
            reader.Close();

            uint sig = creader.ReadUInt32();
            if (sig != signature)
            {
                //error
                creader.Close();
                throw new Exception("Invalid UserDB v5 signature");
            }

            int dataSize = creader.ReadInt32(); //total size of the buffer (may not all be used)

            ushort rowCount = creader.ReadUInt16();
            byte colCount = creader.ReadByte();

            for (int i = 0; i < colCount; i++)
                columns.Add(new TableColumn(creader));

            for (int i = 0; i < rowCount; i++)
                rows.Add(new TableRow(creader, this));

            creader.Close();
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            using (var stream = new MemoryStream())
            {
                using (var cwriter = new Writer(stream))
                {
                    cwriter.Write(signature);

                    cwriter.Write(0x00); // data size (temp)

                    int loop = 0;
                    while (loop++ < 2)
                    {
                        if (loop == 2)
                            cwriter.Write(stream.ToArray().Length - 4); // data size (real)

                        cwriter.Write((ushort)rows.Count);
                        cwriter.Write((byte)columns.Count);

                        for (int i = 0; i < columns.Count; i++)
                            columns[i].Write(cwriter);

                        for (int i = 0; i < rows.Count; i++)
                            rows[i].Write(cwriter);

                        if (loop == 1)
                            cwriter.Seek(4, SeekOrigin.Begin);
                    }
                }

                writer.WriteCompressedRaw(stream.ToArray());
            }

            writer.Close();
        }

    }
}
