using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace RSDKv5
{
    public class StageTiles : IDisposable
    {
        /// <summary>
        /// the 16x16Tiles
        /// </summary>
        public readonly GIF Image;
        /// <summary>
        /// the 16x16Tiles (Semi-Transparent)
        /// </summary>
        public readonly GIF ImageTransparent;
        /// <summary>
        /// IDs for each tile
        /// </summary>
        public readonly GIF IDImage;
        /// <summary>
        /// Tiles for Maniac Editor to Use
        /// </summary>
        public readonly GIF EditorImage;
        /// <summary>
        /// the stage's tileconfig data
        /// </summary>
        public readonly TileConfig Config;
        public readonly GIF CollisionA;

		public StageTiles(string stage_directory, string palleteDir = null)
		{
			Image = new GIF(Path.Combine(stage_directory, "16x16Tiles.gif"), palleteDir);
            ImageTransparent = new GIF(SetImageOpacity(Image.ToBitmap(), (float)0.1));
            IDImage = new GIF(Environment.CurrentDirectory + "\\Resources\\Tile Overlays\\" + "16x16Tiles_ID.gif");
			EditorImage = new GIF(Environment.CurrentDirectory + "\\Resources\\Tile Overlays\\" + "16x16Tiles_Edit.gif");
			if (File.Exists(Path.Combine(stage_directory, "TileConfig.bin")))
			{
				Config = new TileConfig(Path.Combine(stage_directory, "TileConfig.bin"));
			}

        }

        private const int bytesPerPixel = 4;
        public Image SetImageOpacity(Image image, float opacity)
        {
            //create a Bitmap the size of the image provided  
            Bitmap bmp = new Bitmap(image.Width, image.Height);

            //create a graphics object from the image  
            using (Graphics gfx = Graphics.FromImage(bmp))
            {

                //create a color matrix object  
                ColorMatrix matrix = new ColorMatrix();

                //set the opacity  
                matrix.Matrix33 = opacity;

                //create image attributes  
                ImageAttributes attributes = new ImageAttributes();

                //set the color(opacity) of the image  
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                //now draw the image  
                gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
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
