using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GfxToImage
{
    public partial class MainForm : Form
    {

        string filename;

        bool dcGFX = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".gfx";
            dlg.Filter = "Sage 2007 (PC Demo) Retro-Sonic Graphics Files|*.gfx|Dreamcast Demo Retro-Sonic Graphics Files|*.gfx";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                SourceFileLocation.Text = dlg.FileName;
                if (dlg.FilterIndex-1 == 1)
                {
                    dcGFX = true;
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".GIF";
            dlg.Filter = "PNG|*.png|GIF|*.gif|.bmp|*.bmp";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                    RSDKv1.gfx old = new RSDKv1.gfx(filename,dcGFX);

                    switch (dlg.FilterIndex - 1)
                    {
                        case 0:
                            old.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        case 1:
                            old.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        case 2:
                            old.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        default:
                            old.export(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }
            }

        }
    }
}
