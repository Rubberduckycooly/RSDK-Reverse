using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SharpDX.Direct3D9;
using SystemColor = System.Drawing.Color;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace RSDKv5
{
    public class GIF : IDisposable
    {
        Bitmap _bitmap;
        string _bitmapFilename;

        Dictionary<Tuple<Rectangle, bool, bool>, Bitmap> _bitmapCache = new Dictionary<Tuple<Rectangle, bool, bool>, Bitmap>();
        Dictionary<Tuple<Rectangle, bool, bool>, Texture> _texturesCache = new Dictionary<Tuple<Rectangle, bool, bool>, Texture>();

        public GIF(string filename, string encoreColors = null)
        {

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("The GIF file was not found.", filename);
            }
            _bitmap = new Bitmap(filename);

            if (encoreColors != null)
            {
                loadEncoreColors(encoreColors);
            }


            if (_bitmap.Palette != null && _bitmap.Palette.Entries.Length > 0)
            {
                _bitmap.MakeTransparent(_bitmap.Palette.Entries[0]);
            }
            else
            {
                _bitmap.MakeTransparent(SystemColor.FromArgb(0xff00ff));
            }

            // stash the filename too, so we can reload later
            _bitmapFilename = filename;

            // TODO: Proper transparent (palette index 0)
            _bitmap.MakeTransparent(SystemColor.FromArgb(0xff00ff));
        }

        private void loadEncoreColors(string encoreColors = null)
        {
            Bitmap _bitmapEditMemory;
            _bitmapEditMemory = _bitmap.Clone(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), PixelFormat.Format8bppIndexed);
            //Debug.Print(_bitmapEditMemory.Palette.Entries.Length.ToString() + "(1)");

            //Encore Palettes (WIP Potentially Improvable)
            Color[] readableColors = new Color[256];
            bool loadSpecialColors = false;
            if (encoreColors != null && File.Exists(encoreColors))
            {
                using (var stream = File.OpenRead(encoreColors))
                {
                    for (int y = 0; y < 255; ++y)
                    {
                        readableColors[y].R = (byte)stream.ReadByte();
                        readableColors[y].G = (byte)stream.ReadByte();
                        readableColors[y].B = (byte)stream.ReadByte();
                    }
                }
                loadSpecialColors = true;
            }

            if (loadSpecialColors == true)
            {
                ColorPalette pal = _bitmapEditMemory.Palette;
                //Debug.Print(_bitmapEditMemory.Palette.Entries.Length.ToString() + "(2)");
                for (int y = 0; y < 255; ++y)
                {
                    //if (readableColors[y].R != 255 && readableColors[y].G != 0 && readableColors[y].B != 255)
                    //{
                        pal.Entries[y] = SystemColor.FromArgb(readableColors[y].R, readableColors[y].G, readableColors[y].B);
                    //}
                }
                _bitmapEditMemory.Palette = pal;
            }
            _bitmap = _bitmapEditMemory;
        }

        private GIF(Bitmap bitmap)
        {
            this._bitmap = new Bitmap(bitmap);
        }

        private Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Draw the given area (section) of the source image
                // at location 0,0 on the empty bitmap (bmp)
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        public Bitmap GetBitmap(Rectangle section, bool flipX = false, bool flipY = false)
        {
            Bitmap bmp;
            if (_bitmapCache.TryGetValue(new Tuple<Rectangle, bool, bool>(section, flipX, flipY), out bmp)) return bmp;

            bmp = CropImage(_bitmap, section);
            if (flipX)
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if (flipY)
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            _bitmapCache[new Tuple<Rectangle, bool, bool>(section, flipX, flipY)] = bmp;
            return bmp;
        }

        // TOREMOVE
        public Texture GetTexture(Device device, Rectangle section, bool flipX = false, bool flipY = false)
        {
            Texture texture;
            if (_texturesCache.TryGetValue(new Tuple<Rectangle, bool, bool>(section, flipX, flipY), out texture)) return texture;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                GetBitmap(section, flipX, flipY).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                texture = Texture.FromStream(device, memoryStream);
            }

            _texturesCache[new Tuple<Rectangle, bool, bool>(section, flipX, flipY)] = texture;
            return texture;
        }

        public void Dispose()
        {
            ReleaseResources();
        }

        public void DisposeTextures()
        {
            if (null == _texturesCache) return;
            foreach (Texture texture in _texturesCache.Values)
                texture?.Dispose();
            _texturesCache.Clear();
        }

        public void Reload(string encoreColors = null)
        {
            if (!File.Exists(_bitmapFilename))
            {
                throw new FileNotFoundException(string.Format("Could not find the file {0}", _bitmapFilename),
                                                _bitmapFilename);
            }
            ReleaseResources();
            _bitmap = new Bitmap(_bitmapFilename);

            if (encoreColors != null)
            {
                loadEncoreColors(encoreColors);
            }

            if (_bitmap.Palette != null && _bitmap.Palette.Entries.Length > 0)
            {
                _bitmap.MakeTransparent(_bitmap.Palette.Entries[0]);
            }
            else
            {
                _bitmap.MakeTransparent(SystemColor.FromArgb(0xff00ff));
            }

        }

        private void ReleaseResources()
        {
            _bitmap.Dispose();
            DisposeTextures();
            foreach (Bitmap b in _bitmapCache.Values)
                b?.Dispose();
            _bitmapCache.Clear();
        }

        public GIF Clone()
        {
            return new GIF(_bitmapFilename);
        }
    }
}