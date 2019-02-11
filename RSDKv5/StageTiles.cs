using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace RSDKv5
{
    public class StageTiles : IDisposable
    {
        /// <summary>
        /// the 16x16Tiles
        /// </summary>
        public readonly GIF Image;
        /// <summary>
        /// IDs for each tile
        /// </summary>
        public readonly GIF IDImage;
        public readonly GIF EditorImage;
        /// <summary>
        /// the stage's tileconfig data
        /// </summary>
        public readonly TileConfig Config;
        public readonly GIF CollisionA;

		public StageTiles(string stage_directory, string palleteDir = null)
		{
			Image = new GIF(Path.Combine(stage_directory, "16x16Tiles.gif"), palleteDir);
			IDImage = new GIF(Environment.CurrentDirectory + "\\Resources\\Tile Overlays\\" + "16x16Tiles_ID.gif");
			EditorImage = new GIF(Environment.CurrentDirectory + "\\Resources\\Tile Overlays\\" + "16x16Tiles_Edit.gif");
			if (File.Exists(Path.Combine(stage_directory, "TileConfig.bin")))
			{
				Config = new TileConfig(Path.Combine(stage_directory, "TileConfig.bin"));
			}

		}

		public StageTiles()
		{
			Image = new GIF(Path.Combine(Environment.CurrentDirectory, "16x16Tiles_ID.gif"));
			Config = new TileConfig();
		}

		public void Write(string filename)
        {
            Bitmap write = Image.GetBitmap(new Rectangle(0,0,16,16384));
            write.Save(filename);
                
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
