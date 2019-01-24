using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

/* Yes, RSDKv2 has support for the .gfx format */

namespace RSDKv2
{
    public class GraphicsImage
    {
        /// <summary>
        /// the data of the image layed out in a bitmap form for easy use
        /// </summary>
        public Bitmap gfxImage;
        /// <summary>
        /// the Image's palette
        /// </summary>
        public PaletteColour[] GFXpal = new PaletteColour[255];
        /// <summary>
        /// the width of the image
        /// </summary>
        public ushort width;
        /// <summary>
        /// the height of the image
        /// </summary>
        public ushort height;
        /// <summary>
        /// the image data 
        /// </summary>
        public byte[] data;

        public GraphicsImage()
        {

        }

        public GraphicsImage(string filename, bool dcGFX = false) : this(new Reader(filename), dcGFX)
        {

        }

        public GraphicsImage(System.IO.Stream stream, bool dcGFX = false) : this(new Reader(stream), dcGFX)
        {

        }

        public GraphicsImage(Reader reader, bool dcGFX = false)
        {

            if (dcGFX)
            {
                reader.ReadByte();
            }

            width = (ushort)(reader.ReadByte() << 8);
            width |= reader.ReadByte();

            height = (ushort)(reader.ReadByte() << 8);
            height |= reader.ReadByte();

            // Create image
            gfxImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette cp = gfxImage.Palette;

            // Read & Process palette
            for (int i = 0; i < 255; i++)
            {
                GFXpal[i].R = reader.ReadByte();
                GFXpal[i].G = reader.ReadByte();
                GFXpal[i].B = reader.ReadByte();
                cp.Entries[i] = Color.FromArgb(255, GFXpal[i].R, GFXpal[i].G, GFXpal[i].B);

            }
            gfxImage.Palette = cp;

            //Read Image Data
            byte[] buf = new byte[3];
            bool finished = false;
            int cnt = 0;
            int loop = 0;

            data = new byte[(width * height) + 1];

            while (!finished)
            {
                buf[0] = reader.ReadByte();
                if (buf[0] == 255)
                {
                    buf[1] = reader.ReadByte();
                    if (buf[1] == 255)
                    {
                        finished = true;
                        break;
                    }
                    else
                    {
                        buf[2] = reader.ReadByte();
                        loop = 0;

                        // Repeat value needs to decreased by one to decode 
                        // the graphics from the Dreamcast demo
                        if (dcGFX)
                        { buf[2]--; }

                        while (loop < buf[2] && !reader.IsEof)
                        {
                            data[cnt++] = buf[1];
                            loop++;
                        }
                    }
                }
                else
                {
                    data[cnt++] = buf[0];
                }
            }

            Console.Write("file Length = " + reader.BaseStream.Length + " file pos = " + reader.Pos + " data remaining = " + (reader.BaseStream.Length - reader.Pos));

            // Write data to image
            int pixel = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    BitmapData ImgData = gfxImage.LockBits(new Rectangle(new Point(w, h), new Size(1, 1)), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    byte b = System.Runtime.InteropServices.Marshal.ReadByte(ImgData.Scan0);
                    System.Runtime.InteropServices.Marshal.WriteByte(ImgData.Scan0, (data[pixel]));
                    gfxImage.UnlockBits(ImgData);
                    pixel++;
                }
            }

            reader.Close();
        }

        public void ReDrawImage()
        {
            gfxImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette cp = gfxImage.Palette;

            // Read & Process palette
            for (int i = 0; i < 255; i++)
            {
                cp.Entries[i] = Color.FromArgb(255, GFXpal[i].R, GFXpal[i].G, GFXpal[i].B);
            }
            gfxImage.Palette = cp;

            // Write data to image
            int pixel = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    BitmapData ImgData = gfxImage.LockBits(new Rectangle(new Point(w, h), new Size(1, 1)), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    byte b = System.Runtime.InteropServices.Marshal.ReadByte(ImgData.Scan0);
                    System.Runtime.InteropServices.Marshal.WriteByte(ImgData.Scan0, (data[pixel]));
                    gfxImage.UnlockBits(ImgData);
                    pixel++;
                }
            }
        }

        public void export(string exportLocation, System.Drawing.Imaging.ImageFormat format)
        {
            gfxImage.Save(exportLocation, format);
        }

        public void importFromBitmap(Bitmap IMG)
        {
            gfxImage = IMG;
            width = (ushort)IMG.Width;
            height = (ushort)IMG.Height;
        }

        public void Write(string filename, bool dcGFX = false)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer, dcGFX);
        }

        public void Write(System.IO.Stream stream, bool dcGFX = false)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer, dcGFX);
        }

        public static byte Get8bppImagePixel(Bitmap bmp, Point location)
        {
            Color pixelRGB = bmp.GetPixel(location.X, location.Y);
            int pixel8bpp = Array.IndexOf(bmp.Palette.Entries, pixelRGB);
            return (byte)pixel8bpp;
        }

        public void Write(Writer writer, bool dcGFX = false, bool raw = false)
        {
            if (gfxImage == null)
                throw new Exception("Image is NULL");

            if (gfxImage.Palette == null || gfxImage.Palette.Entries.Length == 0)
                throw new Exception("Only indexed images can be converted to GFX format.");

            if (gfxImage.Width > 65535)
                throw new Exception("GFX Images can't be wider than 65535 pixels");

            if (gfxImage.Height > 65535)
                throw new Exception("GFX Images can't be higher than 65535 pixels");

            int num_pixels = gfxImage.Width * gfxImage.Height;
            int[] pixels = new int[num_pixels]; //Pallete Indexes

            // Images can't contain index 255
            for (int x = 0; x < num_pixels; x++)
            {
                if (pixels[x] == 255)
                    throw new Exception("Images to be converted to GFX format can't contain index 255.");
            }

            int pix = 0;
            if (raw) //get data from "data" array
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        pixels[pix] = data[pix];
                        pix++;
                    }
                }
            }
            else //Get Data from Bitmap Class
            {
                for (int h = 0; h < gfxImage.Height; h++)
                {
                    for (int w = 0; w < gfxImage.Width; w++)
                    {
                        pixels[pix++] = Get8bppImagePixel(gfxImage, new Point(w, h));
                    }
                }
            }

            if (dcGFX)
            {
                byte z = 0;
                writer.Write(z);
            }

            // Output width and height
            writer.Write((byte)(gfxImage.Width >> 8));
            writer.Write((byte)(gfxImage.Width & 0xff));

            writer.Write((byte)(gfxImage.Height >> 8));
            writer.Write((byte)(gfxImage.Height & 0xff));

            for (int i = 0; i < gfxImage.Palette.Entries.Length; i++)
            {
                GFXpal[i].R = gfxImage.Palette.Entries[i].R;
                GFXpal[i].G = gfxImage.Palette.Entries[i].G;
                GFXpal[i].B = gfxImage.Palette.Entries[i].B;
            }

            // Output palette
            for (int x = 0; x < 255; x++)
            {
                writer.Write(GFXpal[x].R);
                writer.Write(GFXpal[x].G);
                writer.Write(GFXpal[x].B);
            }

            // Output data
            int p = 0;
            int cnt = 0;

            for (int x = 0; x < num_pixels; x++)
            {
                if (pixels[x] != p && x > 0)
                {
                    rle_write(writer, p, cnt, dcGFX);
                    cnt = 0;
                }
                p = pixels[x];
                cnt++;
            }

            rle_write(writer, p, cnt, dcGFX);

            // End of GFX file		
            writer.Write((byte)0xFF);
            writer.Write((byte)0xFF);

            writer.Close();
        }

        private static void rle_write(Writer file, int pixel, int count, bool dcGfx = false)
        {
            if (count <= 2)
            {
                for (int y = 0; y < count; y++)
                    file.Write((byte)pixel);
            }
            else
            {
                while (count > 0)
                {
                    file.Write((byte)0xFF);

                    file.Write((byte)pixel);

                    if (dcGfx)
                    {
                        file.Write((byte)((count > 253) ? 254 : (count + 1)));
                        count -= 253;
                    }
                    else
                    {
                        file.Write((byte)((count > 254) ? 254 : count));
                        count -= 254;
                    }
                }
            }
        }

    }
}
