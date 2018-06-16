using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class StageTiles : IDisposable
    {
        public readonly GIF Image;
        public readonly TilesConfig Config;

        public StageTiles(string stage_directory)
        {
            Image = new GIF(Path.Combine(stage_directory, "16x16Tiles.gif"));
            if (File.Exists(Path.Combine(stage_directory, "TileConfig.bin")))
                Config = new TilesConfig(Path.Combine(stage_directory, "TileConfig.bin"));
        }

        public void Dispose()
        {
            Image.Dispose();
        }

        public void DisposeTextures()
        {
            Image.DisposeTextures();
        }
    }
}
