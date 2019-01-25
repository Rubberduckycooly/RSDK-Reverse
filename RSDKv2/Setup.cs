using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv2
{
    public class Setup
    {

        public int GameWidth;
        public int GameHeight;
        public int GameFPS;
        public bool Windowed;

        public Setup()
        {

        }

        public Setup(Reader reader)
        {
            GameWidth = reader.ReadInt32();
            GameHeight = reader.ReadInt32();
            GameFPS = reader.ReadInt32();
            Windowed = reader.ReadBoolean();
        }

        public void Write(Writer writer)
        {
            writer.Write(GameWidth);
            writer.Write(GameHeight);
            writer.Write(GameFPS);
            writer.Write(Windowed);
        }

    }
}
