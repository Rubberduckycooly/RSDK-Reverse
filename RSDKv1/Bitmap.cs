using System;

namespace RSDKv1
{
    public class Bitmap
    {
        /// <summary>
        /// the width of the image
        /// </summary>
        public int width = 0;

        /// <summary>
        /// the height of the image
        /// </summary>
        public int height = 0;

        /// <summary>
        /// the Image's palette
        /// </summary>
        public Palette.Color[] palette = new Palette.Color[0x100];

        /// <summary>
        /// the pixel indices of the image 
        /// </summary>
        public byte[] pixels = new byte[0];

        public Bitmap()
        {
            for (int i = 0; i < 0x100; i++)
                palette[i] = new Palette.Color(0x00, 0x00, 0x00);
        }

        public Bitmap(string filename) : this(new Reader(filename)) { }

        public Bitmap(System.IO.Stream stream) : this(new Reader(stream)) { }

        public Bitmap(Reader reader) : this()
        {
            Read(reader);
        }
        public void Read(Reader reader)
        {
            reader.ReadInt16(); // "BM"
            reader.ReadInt32(); // totalFileSize
            reader.ReadInt32(); // Unused
            int pixelPos = reader.ReadInt32();
            reader.Seek(14 + 4, System.IO.SeekOrigin.Begin);

            width = reader.ReadByte();
            width |= reader.ReadByte() << 8;
            width |= reader.ReadByte() << 16;
            width |= reader.ReadByte() << 24;

            height = reader.ReadByte();
            height |= reader.ReadByte() << 8;
            height |= reader.ReadByte() << 16;
            height |= reader.ReadByte() << 24;

            reader.BaseStream.Position += sizeof(ushort);
            bool indexed = reader.ReadUInt16() <= 8; //bpp
            if (!indexed)
                throw new Exception("RSDK-Formatted Bitmap files must be indexed!");

            reader.BaseStream.Position += 4 * sizeof(int);
            int colorCount = reader.ReadInt32(); // how many colors used

            reader.Seek(14 + 40, System.IO.SeekOrigin.Begin);

            for (int c = 0; c < colorCount; c++)
            {
                palette[c].b = reader.ReadByte();
                palette[c].g = reader.ReadByte();
                palette[c].r = reader.ReadByte();
                reader.ReadByte(); // unused
            }

            long expectedPixelPos = (reader.BaseStream.Length - height * width);
            if (pixelPos != expectedPixelPos)
                throw new Exception("RSDK-Formatted Bitmap files must end with the pixel data!");

            // This is how RSDK does it but there's a small chance it could maybe be wrong
            reader.Seek(expectedPixelPos, System.IO.SeekOrigin.Begin);

            pixels = new byte[width * height];
            int gfxPos = width * (height - 1);
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                    pixels[gfxPos++] = reader.ReadByte();
                gfxPos -= 2 * width;
            }

            reader.Close();
        }

        public void Write(string filename)
        {
            Write(new Writer(filename));
        }

        public void Write(System.IO.Stream s)
        {
            Write(new Writer(s));
        }

        public void Write(Writer writer)
        {
            uint fileSize = (uint)(14 + 40 + (0x100 * 4) + (width * height));
            uint pixelPos = (uint)(14 + 40 + (0x100 * 4));

            // Header
            writer.Write("BM".ToCharArray());
            writer.Write(fileSize);
            // all these bytes are unused in the spec, so here's a "signature"
            writer.Write("RSDK".ToCharArray());
            writer.Write(pixelPos);

            // Info
            writer.Write(40);           // header size
            writer.Write(width);
            writer.Write(height);
            writer.Write((ushort)1);    // color planes, 1, always
            writer.Write((ushort)8);    // BPP, always 8
            writer.Write(0);            // compression, none, never
            writer.Write(0);            // image size, can be 0, we dont care about it
            writer.Write(0);            // pixels per meter on x axis, we dont care
            writer.Write(0);            // pixels per meter on y axis, we dont care
            writer.Write(0x100);        // palette size
            writer.Write(0);            // important s, we dont care, so use 0

            for (int c = 0; c < 0x100; c++)
            {
                writer.Write(palette[c].b);
                writer.Write(palette[c].g);
                writer.Write(palette[c].r);
                writer.Write((byte)0xFF); // unused
            }

            for (int y = height - 1; y >= 0; --y)
            {
                for (int x = 0; x < width; ++x)
                    writer.Write(pixels[x + (y * width)]);
            }

            writer.Close();
        }

        public System.Drawing.Image ToImage()
        {
            // Create image
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            System.Drawing.Imaging.ColorPalette cpal = img.Palette;

            for (int i = 0; i < 0x100; i++)
                cpal.Entries[i] = System.Drawing.Color.FromArgb(255, palette[i].r, palette[i].g, palette[i].b);

            img.Palette = cpal;

            // Write data to image
            System.Drawing.Imaging.BitmapData imgData = img.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, img.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, imgData.Scan0, pixels.Length);
            img.UnlockBits(imgData);

            return img;
        }

        public void FromImage(System.Drawing.Bitmap img)
        {
            // Create image
            width = (ushort)img.Width;
            height = (ushort)img.Height;

            for (int i = 0; i < 0x100; i++)
            {
                palette[i].r = img.Palette.Entries[i].R;
                palette[i].g = img.Palette.Entries[i].G;
                palette[i].b = img.Palette.Entries[i].B;
            }
            pixels = new byte[width * height];

            System.Drawing.Imaging.BitmapData imgData = img.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Runtime.InteropServices.Marshal.Copy(imgData.Scan0, pixels, 0, pixels.Length);
            img.UnlockBits(imgData);
        }
    }
}
