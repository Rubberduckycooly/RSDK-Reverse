namespace RSDKv3
{
    public class WindowConfig
    {
        public int windowWidth = 400;
        public int windowHeight = 240;
        public int refreshRate = 60;
        public bool isWindowed = true;

        public WindowConfig() { }

        public WindowConfig(string filename) : this(new Reader(filename)) { }

        public WindowConfig(System.IO.Stream stream) : this(new Reader(stream)) { }

        public WindowConfig(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            windowWidth  = reader.ReadInt32();
            windowHeight = reader.ReadInt32();
            refreshRate  = reader.ReadInt32();
            isWindowed   = reader.ReadBoolean();
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
            writer.Write(windowWidth);
            writer.Write(windowHeight);
            writer.Write(refreshRate);
            writer.Write(isWindowed);
        }

    }
}
