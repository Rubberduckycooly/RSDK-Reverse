using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Tools.GFXTool
{
    public partial class MainForm : Form
    {

        string filename;

        bool dcGFX = false;

        RSDKvRS.gfx GFX;
        System.Drawing.Imaging.ColorPalette GFXPal;

        Bitmap IMG;
        System.Drawing.Imaging.ColorPalette IMGPal;

        public MainForm()
        {
            InitializeComponent();
            GFX = new RSDKvRS.gfx();
            IMG = new Bitmap(1,1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".gfx";
            dlg.Filter = "Sage 2007 (PC Demo) Retro-Sonic Graphics Files|*.gfx|Dreamcast Demo Retro-Sonic Graphics Files|*.gfx";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                SourceGFXLocation.Text = dlg.FileName;
                if (dlg.FilterIndex-1 == 1) //Did the user want to load a Dreamcast .gfx file?
                {
                    dcGFX = true;
                }
                GFX = new RSDKvRS.gfx(filename, dcGFX); //Load the GFX file into a bitmap
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".gif";
            dlg.Filter = "Portable Network Graphics|*.png|Graphics Interchange Format Image|*.gif|Bitmap Image|*.bmp";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                    switch (dlg.FilterIndex - 1)
                    {
                        case 0:
                        string txt0 = this.Text;
                        this.Text = "Exporting - " + txt0;
                        GFX.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png); //Export to indexed PNG
                        this.Text = txt0;
                        break;
                        case 1:
                        string txt1 = this.Text;
                        this.Text = "Exporting - " + txt1;
                        GFX.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif); //Export to indexed GIF
                        this.Text = txt1;
                        break;
                        case 2:
                        string txt2 = this.Text;
                        this.Text = "Exporting - " + txt2;
                        GFX.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp); //Export to indexed BMP
                        this.Text = txt2;
                        break;
                        default:
                        string txt3 = this.Text;
                        this.Text = "Exporting - " + txt3;
                        GFX.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif); //Export to indexed GIF
                        this.Text = txt3;
                        break;
                    }
            }

        }

        private void SelectGIFButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".gif";
            dlg.Filter = "Graphics Interchange Format Image|*.gif|Bitmap Image|*.bmp";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                SourceIMGLocation.Text = dlg.FileName;
                IMG = (Bitmap)Image.FromFile(dlg.FileName).Clone(); //Copy the image into memory
                GFX.importFromBitmap(IMG);
                //GFX = null;
            }
        }

        private void ExportToGFX_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".gfx";
            dlg.Filter = "Sage 2007 (PC Demo) Retro-Sonic Graphics Files|*.gfx|Dreamcast Demo Retro-Sonic Graphics Files|*.gfx";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                SourceIMGLocation.Text = dlg.FileName;
                if (dlg.FilterIndex - 1 == 1)
                {
                    dcGFX = true;
                }
                string txt = this.Text;
                this.Text = "Exporting - " + txt;
                GFX.Write(dlg.FileName,dcGFX);
                this.Text = txt;
            }
        }
    }
}
