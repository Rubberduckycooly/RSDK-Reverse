using System;
using System.Collections.Generic;

namespace RSDKv3
{
    public class TextFont
    {
        public class FontCharacter
        {
            public int id = 0;
            public ushort srcX = 0;
            public ushort srcY = 0;
            public ushort width = 0;
            public ushort height = 0;
            public short pivotX = 0;
            public short pivotY = 0;
            public short xAdvance = 0;
            public byte unused1 = 0;
            public byte unused2 = 0;

            public FontCharacter() { }

            public FontCharacter(Reader reader) { read(reader); }

            public void read(Reader reader)
            {
                id = reader.ReadByte();
                id |= reader.ReadByte() << 8;
                id |= reader.ReadByte() << 16;
                id |= reader.ReadByte() << 24;

                srcX = reader.ReadByte();
                srcX |= (ushort)(reader.ReadByte() << 8);

                srcY = reader.ReadByte();
                srcY |= (ushort)(reader.ReadByte() << 8);

                width = reader.ReadByte();
                width |= (ushort)(reader.ReadByte() << 8);

                height = reader.ReadByte();
                height |= (ushort)(reader.ReadByte() << 8);

                pivotX = reader.ReadByte();
                byte buffer = reader.ReadByte();
                if (buffer > 0x80)
                    pivotX |= (short)(((buffer - 0x80) << 8) + -0x8000);
                else
                    pivotX |= (short)(buffer << 8);

                pivotY = reader.ReadByte();
                buffer = reader.ReadByte();
                if (buffer > 0x80)
                    pivotY |= (short)(((buffer - 0x80) << 8) + -0x8000);
                else
                    pivotY |= (short)(buffer << 8);

                xAdvance = reader.ReadByte();
                buffer = reader.ReadByte();
                if (buffer > 0x80)
                    xAdvance |= (short)(((buffer - 0x80) << 8) + -0x8000);
                else
                    xAdvance |= (short)(buffer << 8);

                unused1 = reader.ReadByte();
                unused2 = reader.ReadByte();
            }

            public void write(Writer writer)
            {
                byte[] bytes = BitConverter.GetBytes(id);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);
                writer.Write(bytes[2]);
                writer.Write(bytes[3]);

                bytes = BitConverter.GetBytes(srcX);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                bytes = BitConverter.GetBytes(srcY);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                bytes = BitConverter.GetBytes(width);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                bytes = BitConverter.GetBytes(height);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                bytes = BitConverter.GetBytes(pivotX);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                bytes = BitConverter.GetBytes(pivotY);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                bytes = BitConverter.GetBytes(xAdvance);
                writer.Write(bytes[0]);
                writer.Write(bytes[1]);

                writer.Write(unused1);
                writer.Write(unused2);
            }
        }

        public List<FontCharacter> characters = new List<FontCharacter>();

        public TextFont() { }

        public TextFont(string filename) : this(new Reader(filename)) { }

        public TextFont(System.IO.Stream stream) : this(new Reader(stream)) { }

        public TextFont(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            while (reader.BaseStream.Position + 20 <= reader.BaseStream.Length)
                characters.Add(new FontCharacter(reader));

            reader.Close();
        }

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
        {
            foreach (FontCharacter character in characters)
                character.write(writer);

            writer.Close();
        }

    }
}
