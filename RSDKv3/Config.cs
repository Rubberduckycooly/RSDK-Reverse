namespace RSDKv3
{
    public class Config
    {
        public int windowWidth = 400;
        public int windowHeight = 240;
        public int refreshRate = 60;
        public bool isWindowed = true;

        public Config() { }

        public Config(string filename) : this(new Reader(filename)) { }

        public Config(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Config(Reader reader)
        {
            read(reader);
        }

        public void read(Reader reader)
        {
            windowWidth = reader.ReadInt32();
            windowHeight = reader.ReadInt32();
            refreshRate = reader.ReadInt32();
            isWindowed = reader.ReadBoolean();
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
            writer.Write(windowWidth);
            writer.Write(windowHeight);
            writer.Write(refreshRate);
            writer.Write(isWindowed);
        }

    }
}
