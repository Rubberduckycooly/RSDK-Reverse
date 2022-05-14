using System;

namespace RSDKv2
{
    public class Gfx
    {
        /// <summary>
        /// the width of the image
        /// </summary>
        public ushort width = 0;

        /// <summary>
        /// the height of the image
        /// </summary>
        public ushort height = 0;

        /// <summary>
        /// the Image's palette
        /// </summary>
        public Palette.Color[] palette = new Palette.Color[0xFF];
        /// <summary>
        /// the pixel indices of the image 
        /// </summary>
        public byte[] pixels = new byte[0];

        public Gfx()
        {
            for (int i = 0; i < 0xFF; i++)
                palette[i] = new Palette.Color(0xFF, 0x00, 0xFF);
        }

        public Gfx(string filename, bool dcVer = false) : this(new Reader(filename), dcVer) { }

        public Gfx(System.IO.Stream stream, bool dcVer = false) : this(new Reader(stream), dcVer) { }

        public Gfx(Reader reader, bool dcVer = false) : this()
        {
            Read(reader, dcVer);
        }

        public void Read(Reader reader, bool dcVer = false)
        {
            if (dcVer)
                reader.ReadByte();

            width = (ushort)(reader.ReadByte() << 8);
            width |= reader.ReadByte();

            height = (ushort)(reader.ReadByte() << 8);
            height |= reader.ReadByte();

            // Read & Process palette
            for (int c = 0; c < 255; c++)
            {
                palette[c].r = reader.ReadByte();
                palette[c].g = reader.ReadByte();
                palette[c].b = reader.ReadByte();
            }

            // Read Pixels
            pixels = new byte[width * height];
            byte[] buf = new byte[3];
            int pos = 0;
            while (true)
            {
                buf[0] = reader.ReadByte();
                if (buf[0] == 0xFF)
                {
                    buf[1] = reader.ReadByte();
                    if (buf[1] != 0xFF)
                    {
                        buf[2] = reader.ReadByte();
                        byte val = buf[1];
                        int cnt = buf[2];
                        if (dcVer)
                            cnt--;
                        for (int c = 0; c < cnt; ++c) pixels[pos++] = val;
                    }
                    else
                        break;
                }
                else
                    pixels[pos++] = buf[0];
            }


            reader.Close();
        }

        public void Write(string filename, bool dcVer = false)
        {
            using (Writer writer = new Writer(filename))
                Write(writer, dcVer);
        }

        public void Write(System.IO.Stream stream, bool dcVer = false)
        {
            using (Writer writer = new Writer(stream))
                Write(writer, dcVer);
        }

        public void Write(Writer writer, bool dcVer = false)
        {
            if (dcVer)
                writer.Write((byte)0);

            // Output width and height
            writer.Write((byte)(width >> 8));
            writer.Write((byte)(width & 0xff));

            writer.Write((byte)(height >> 8));
            writer.Write((byte)(height & 0xff));

            // Output palette
            for (int c = 0; c < 0xFF; c++)
            {
                writer.Write(palette[c].r);
                writer.Write(palette[c].g);
                writer.Write(palette[c].b);
            }

            // Output data
            int p = 0;
            int cnt = 0;

            for (int x = 0; x < width * height; x++)
            {
                if (pixels[x] != p && x > 0)
                {
                    WriteRLE(writer, p, cnt, dcVer);
                    cnt = 0;
                }
                p = pixels[x];
                cnt++;
            }

            WriteRLE(writer, p, cnt, dcVer);

            // End of GFX file		
            writer.Write((byte)0xFF);
            writer.Write((byte)0xFF);

            writer.Close();
        }

        private static void WriteRLE(Writer file, int pixel, int count, bool dcVer = false)
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

                    if (dcVer)
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

        public System.Drawing.Image ToImage()
        {
            // Create image
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            System.Drawing.Imaging.ColorPalette cpal = img.Palette;

            for (int i = 0; i < 0xFF; i++)
                cpal.Entries[i] = System.Drawing.Color.FromArgb(255, palette[i].r, palette[i].g, palette[i].b);

            img.Palette = cpal;

            // Write data to image
            System.Drawing.Imaging.BitmapData imgData = img.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, img.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, imgData.Scan0, pixels.Length);
            img.UnlockBits(imgData);

            return img;
        }

        public Bitmap ToBitmap()
        {
            // Create image
            Bitmap img = new Bitmap();
            img.width = width;
            img.height = height;

            for (int c = 0; c < 0xFF; c++)
            {
                img.palette[c].r = palette[c].r;
                img.palette[c].g = palette[c].g;
                img.palette[c].b = palette[c].b;
            }
            img.palette[0xFF].r = 0xFF;
            img.palette[0xFF].g = 0x00;
            img.palette[0xFF].b = 0xFF;

            img.pixels = new byte[width * height];
            Array.Copy(pixels, img.pixels, pixels.Length);

            return img;
        }

        public Gif ToGif()
        {
            // Create image
            Gif img = new Gif();
            img.width = width;
            img.height = height;

            for (int c = 0; c < 0xFF; c++)
            {
                img.palette[c].r = palette[c].r;
                img.palette[c].g = palette[c].g;
                img.palette[c].b = palette[c].b;
            }
            img.palette[0xFF].r = 0xFF;
            img.palette[0xFF].g = 0x00;
            img.palette[0xFF].b = 0xFF;

            img.pixels = new byte[width * height];
            Array.Copy(pixels, img.pixels, pixels.Length);

            return img;
        }

        public void FromImage(System.Drawing.Bitmap img)
        {
            // Create image
            width = (ushort)img.Width;
            height = (ushort)img.Height;

            for (int c = 0; c < 0xFF; c++)
            {
                palette[c].r = img.Palette.Entries[c].R;
                palette[c].g = img.Palette.Entries[c].G;
                palette[c].b = img.Palette.Entries[c].B;
            }
            pixels = new byte[width * height];

            System.Drawing.Imaging.BitmapData imgData = img.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Runtime.InteropServices.Marshal.Copy(imgData.Scan0, pixels, 0, pixels.Length);
        }

        public void FromImage(Bitmap img)
        {
            // Create image
            width = (ushort)img.width;
            height = (ushort)img.height;

            for (int c = 0; c < 0xFF; c++)
            {
                palette[c].r = img.palette[c].r;
                palette[c].g = img.palette[c].g;
                palette[c].b = img.palette[c].b;
            }

            pixels = new byte[width * height];
            Array.Copy(img.pixels, pixels, pixels.Length);
        }

        public void fromImage(Gif img)
        {
            // Create image
            width = img.width;
            height = img.height;

            for (int c = 0; c < 0xFF; c++)
            {
                palette[c].r = img.palette[c].r;
                palette[c].g = img.palette[c].g;
                palette[c].b = img.palette[c].b;
            }

            pixels = new byte[width * height];
            Array.Copy(img.pixels, pixels, pixels.Length);
        }
    }
}
