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
        public class gfxPalette
        {
            public byte[] r = new byte[256];
            public byte[] g = new byte[256];
            public byte[] b = new byte[256];
        }

        public Bitmap gfxImage;
        public gfxPalette GFXpal;
        int width;
        int height;
        int[] data;

        public GraphicsImage()
        {

        }

        public GraphicsImage(string filename) : this(new Reader(filename))
        {

        }

        public GraphicsImage(System.IO.Stream stream) : this(new Reader(stream))
        {

        }

        public GraphicsImage(Reader reader)
        {

            width = reader.ReadByte() << 8;
            width |= reader.ReadByte();

            height = reader.ReadByte() << 8;
            height |= reader.ReadByte();

            // Create image

            GFXpal = new gfxPalette();

            gfxImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette cp = gfxImage.Palette;

            // Read & Process palette
            for (int i = 0; i < 255; i++)
            {
                GFXpal.r[i] = reader.ReadByte();
                GFXpal.g[i] = reader.ReadByte();
                GFXpal.b[i] = reader.ReadByte();
                cp.Entries[i] = Color.FromArgb(255, GFXpal.r[i], GFXpal.g[i], GFXpal.b[i]);
                
            }
            gfxImage.Palette = cp;
            
            //Read Image Data
            int[] buf = new int[3];
            bool finished = false;
            int cnt = 0;
            int loop = 0;

            data = new int[width * height];

            while (!reader.IsEof)
            {
                buf[0] = reader.ReadByte();

                if (buf[0] != 0xFF && !reader.IsEof)
                { data[cnt++] = buf[0]; }

                else
                {
                    buf[1] = reader.ReadByte();

                    if (buf[1] != 0xFF && !reader.IsEof)
                    {
                        buf[2] = reader.ReadByte();
                        loop = 0;

                        // Repeat value needs to decreased by one to decode 

                        while (loop < buf[2] && !reader.IsEof)
                        {
                            data[cnt++] = buf[1];
                            loop++;
                        }
                    }
                    else
                        finished = true;
                }
            }

            // Write data to image
            int pixel = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    BitmapData ImgData = gfxImage.LockBits(new Rectangle(new Point(w, h), new Size(1, 1)), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    byte b = System.Runtime.InteropServices.Marshal.ReadByte(ImgData.Scan0);
                    System.Runtime.InteropServices.Marshal.WriteByte(ImgData.Scan0, (byte)(data[pixel]));
                    gfxImage.UnlockBits(ImgData);
                    pixel++;
                }
            }
            reader.Close();
        }

        public void export(string exportLocation, System.Drawing.Imaging.ImageFormat format)
        {
            gfxImage.Save(exportLocation, format);	
        }

        public void importFromBitmap(Bitmap IMG)
        {
            gfxImage = IMG;
            width = IMG.Width; 
            height = IMG.Height;
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        public static byte Get8bppImagePixel(Bitmap bmp, Point location)
        {
            Color pixelRGB = bmp.GetPixel(location.X, location.Y);
            int pixel8bpp = Array.IndexOf(bmp.Palette.Entries, pixelRGB);
            return (byte)pixel8bpp;
        }

        internal void Write(Writer writer)
        {
            if (gfxImage == null)
                throw new Exception("Image is NULL");

            if (gfxImage.Palette == null || gfxImage.Palette.Entries.Length == 0)
			throw new Exception("Only indexed images can be converted to GFX format.");

            if (gfxImage.Width > 65535)
                throw new Exception("Images to be converted to GFX format can't be wider than 65535 pixels");

            if (gfxImage.Height > 65535)
                throw new Exception("Images to be converted to GFX format can't be higher than 65535 pixels");

            GFXpal = new gfxPalette();

            int num_pixels = gfxImage.Width * gfxImage.Height;
            int[] pixels = new int[num_pixels]; //Pallete Indexes

            // Images can't contain index 255
            for (int x = 0; x < num_pixels; x++)
            {
                if (pixels[x] == 255)
                    throw new Exception("Images to be converted to GFX format can't contain index 255.");
            }

            int pix = 0;
            for (int h = 0; h < gfxImage.Height; h++)
            {
                for (int w = 0; w < gfxImage.Width; w++)
                {
                    pixels[pix++] = Get8bppImagePixel(gfxImage, new Point(w, h));
                }
            }

            // Output width and height
            writer.Write((byte)(gfxImage.Width >> 8));
            writer.Write((byte)(gfxImage.Width & 0xff));

            writer.Write((byte)(gfxImage.Height >> 8));
            writer.Write((byte)(gfxImage.Height & 0xff));

            for (int i = 0; i < gfxImage.Palette.Entries.Length; i++)
            {
                GFXpal.r[i] = gfxImage.Palette.Entries[i].R;
                GFXpal.g[i] = gfxImage.Palette.Entries[i].G;
                GFXpal.b[i] = gfxImage.Palette.Entries[i].B;
            }

            // Output palette
            for (int x = 0; x < 255; x++)
            {
                writer.Write(GFXpal.r[x]);
                writer.Write(GFXpal.g[x]);
                writer.Write(GFXpal.b[x]);
            }

            // Output data
            int p = 0;
            int cnt = 0;

            for (int x = 0; x < num_pixels; x++)
            {
                if (pixels[x] != p && x > 0)
                {
                    rle_write(writer, p, cnt);
                    cnt = 0;
                }
                p = pixels[x];
                cnt++;
            }

            rle_write(writer, p, cnt);

            // End of GFX file		
            writer.Write((byte)0xFF);
            writer.Write((byte)0xFF);

            writer.Close();
        }

        private static void rle_write(Writer file, int pixel, int count)
        {
		if(count <= 2)
		{
			for(int y = 0; y<count; y++)
				file.Write((byte)pixel);
		}
		else
		{
			while(count > 0)
			{
				file.Write((byte)0xFF);
				
				file.Write((byte)pixel);

				file.Write((byte)((count>254) ? 254 : count));
				count -= 254;
			}
		}
	}

    }

}
