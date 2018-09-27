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
        public readonly GIF IDImage;
        public readonly GIF EditorImage;
        public readonly TilesConfig Config;

        public StageTiles(string stage_directory)
        {
            Image = new GIF(Path.Combine(stage_directory, "16x16Tiles.gif"));
            IDImage = new GIF(Path.Combine(Environment.CurrentDirectory, "16x16Tiles_ID.gif"));
            EditorImage = new GIF(Path.Combine(Environment.CurrentDirectory, "16x16Tiles_Edit.gif"));
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
