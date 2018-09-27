using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Tools.ChunkEditor
{
    public partial class AutoSetTiles : Form
    {
        public ushort Value;

        Bitmap Tileset;

        public AutoSetTiles(ChunkMappingsEditor.TileList t)
        {
            InitializeComponent();
            //TilesList = t;
            //TilesList.Images = t.Images;
        }

        private void tileList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Value = (ushort)TilesList.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".gif";
            dlg.Filter = "gif images|*.gif|Retro-Sonic Graphics Images (Sage 2007)|*.gfx|Retro-Sonic Graphics Images (Dreamcast Demo)|*.gfx";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.FilterIndex-1 == 0)
                {
                    Tileset = new Bitmap(dlg.FileName);
                    LoadTileSet(Tileset);
                }
                if (dlg.FilterIndex - 1 == 1)
                {
                    RSDKvRS.gfx g = new RSDKvRS.gfx(dlg.FileName, false);
                    Tileset = g.gfxImage;
                    LoadTileSet(Tileset);
                }
                if (dlg.FilterIndex - 1 == 2)
                {
                    RSDKvRS.gfx g = new RSDKvRS.gfx(dlg.FileName, true);
                    Tileset = g.gfxImage;
                    LoadTileSet(Tileset);
                }
            }
        }
        public void LoadTileSet(Bitmap TileSet)
        {
            TilesList.Images.Clear();
            int tsize = TileSet.Height;
            for (int i = 0; i < (tsize / 16); i++)
            {
                Rectangle CropArea = new Rectangle(0, (i * 16), 16, 16);
                Bitmap CroppedImage = CropImage(TileSet, CropArea);
                TilesList.Images.Add(CroppedImage);
            }
            TilesList.Refresh();
            Refresh();
        }
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
