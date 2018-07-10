using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    class MapObject
    {
        public string Name, SpriteSheet;
        public int ID, SubType, X, Y, Width, Height;

        public MapObject()
        {

        }

        public MapObject(string name, int id, int sID, string sheet, int Sheetxpos, int Sheetypos, int width, int height)
        {
            Name = name;
            ID = id;
            SubType = sID;
            SpriteSheet = sheet;
            X = Sheetxpos;
            Y = Sheetypos;
            Width = width;
            Height = height;
        }

        public Bitmap RenderObject()
        {
            RSDKv1.gfx g = new RSDKv1.gfx("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\" + SpriteSheet, false);
            Bitmap b;
            b = CropImage(g.gfxImage, new Rectangle(X, Y, Width, Height));
            return b;
        }

        public Bitmap RenderObject(Color Transparent)
        {
            Bitmap b = (Bitmap)Image.FromFile(SpriteSheet).Clone();
            b.MakeTransparent(Transparent);
            b = CropImage(b, new Rectangle(X, Y, Width, Height));
            return b;
        }

        Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

    }
}
