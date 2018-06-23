using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
namespace RSDKv1
{
    public class gfx
    {
        
        public Bitmap gfxImage;
        public gfxPalette GFXpal;
        int width;
        int height;
        int[] data;

        public gfx(string filename, bool dcGFX) : this(new Reader(filename),dcGFX)
        {

        }

        public gfx(System.IO.Stream stream, bool dcGFX) : this(new Reader(stream),dcGFX)
        {

        }

        public gfx(Reader reader, bool dcGFX)
        {

            if (dcGFX)
            {
                reader.ReadByte();
            }

            width = reader.ReadByte() << 8;
            width |= reader.ReadByte();

            height = reader.ReadByte() << 8;
            height |= reader.ReadByte();

            // Create image
            Bitmap gfxImage2 = new Bitmap(width, height);

            Color[] Palette = new Color[255];

            GFXpal = new gfxPalette();

            gfxImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            gfxImage2 = new Bitmap(width, height);

            // Read & Process palette
            for (int i = 0; i < 255; i++)
            {
                GFXpal.r[i] = reader.ReadByte();
                GFXpal.g[i] = reader.ReadByte();
                GFXpal.b[i] = reader.ReadByte();
                Color c = Color.FromArgb(255, GFXpal.r[i], GFXpal.g[i], GFXpal.b[i]);
                Palette[i] = c;
                gfxImage.Palette.Entries[i] = Palette[i];
            }

            // Read data
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
                        // the graphics from the Dreamcast demo
                            if (dcGFX)
                            {buf[2]--; }



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



            int pixel = 0;
            // Write data to image
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    
                    gfxImage2.SetPixel(w, h, Color.FromArgb(255, GFXpal.r[data[pixel]], GFXpal.g[data[pixel]], GFXpal.b[data[pixel]]));
                    pixel++;
                }
            }

            gfxImage = gfxImage2;
            /*gfxImage = new Bitmap(gfxImage2.Width, gfxImage2.Height, PixelFormat.Format8bppIndexed);
            
            for (int i = 0; i < gfxImage.Palette.Entries.Count(); i++)
            {
                gfxImage.Palette.Entries[i] = Palette[i];
            }*/
        }

        public void export(string exportLocation, System.Drawing.Imaging.ImageFormat format)
        {
            gfxImage.Save(exportLocation, format);	
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

        internal void Write(Writer writer)
        {

        }

    }

    public class gfxPalette
    {
        public byte[] r = new byte[256];
        public byte[] g = new byte[256];
        public byte[] b = new byte[256];
    }

}
