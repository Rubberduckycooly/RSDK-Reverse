namespace RSDKv2
{
    public class TextFile
    {

        byte[] TextFileInfo = new byte[1];

        public TextFile()
        {

        }

        public TextFile(Reader reader)
        {
            reader.ReadBytes(reader.BaseStream.Length);
            reader.Close();
        }

        public void Write(Writer writer)
        {
            writer.Write(TextFileInfo);
            writer.Close();
        }

    }
}
