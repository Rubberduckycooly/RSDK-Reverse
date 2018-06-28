using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SharpDX.Direct3D9;
using SystemColor = System.Drawing.Color;
using System.IO;

namespace RSDKv5
{
    public class GIF : IDisposable
    {
        Bitmap bitmap;
        
        Dictionary<Tuple<Rectangle, bool, bool>, Bitmap> bitmapCache = new Dictionary<Tuple<Rectangle, bool, bool>, Bitmap>();
        Dictionary<Tuple<Rectangle, bool, bool>, Texture> texturesCache = new Dictionary<Tuple<Rectangle, bool, bool>, Texture>();

        public GIF(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("The GIF file was not found.", filename);
            }
            bitmap = new Bitmap(filename);

            if (bitmap.Palette != null && bitmap.Palette.Entries.Length > 0)
            {
                bitmap.MakeTransparent(bitmap.Palette.Entries[0]);
            }
            else
            {
                bitmap.MakeTransparent(SystemColor.FromArgb(0xff00ff));
            }
        }

        public GIF(Bitmap bitmap)
        {
            this.bitmap = new Bitmap(bitmap);
        }

        private Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);
            
            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public Bitmap GetBitmap(Rectangle section, bool flipX = false, bool flipY = false)
        {
            Bitmap bmp;
            if (bitmapCache.TryGetValue(new Tuple<Rectangle, bool, bool>(section, flipX, flipY), out bmp)) return bmp;

            bmp = CropImage(bitmap, section);
            if (flipX)
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if (flipY)
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            bitmapCache[new Tuple<Rectangle, bool, bool>(section, flipX, flipY)] = bmp;
            return bmp;
        }

        // TOREMOVE
        public Texture GetTexture(Device device, Rectangle section, bool flipX = false, bool flipY = false)
        {
            Texture texture;
            if (texturesCache.TryGetValue(new Tuple<Rectangle, bool, bool>(section, flipX, flipY), out texture)) return texture;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                GetBitmap(section, flipX, flipY).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                texture = Texture.FromStream(device, memoryStream);
            }

            texturesCache[new Tuple<Rectangle, bool, bool>(section, flipX, flipY)] = texture;
            return texture;
        }

        public void Dispose()
        {
            bitmap.Dispose();
            texturesCache = null;
        }

        public void DisposeTextures()
        {
            foreach (Texture texture in texturesCache.Values)
                texture.Dispose();
            texturesCache = new Dictionary<Tuple<Rectangle, bool, bool>, Texture>();
        }

        public GIF Clone()
        {
            return new GIF(bitmap);
        }
    }
}
