using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AniManiac
{
    public partial class TexManagerForm : Form
    {

        public List<string> Textures;
        public List<string> FullTexturePaths;

        public TexManagerForm(List<string> textures, string basepath)
        {
            InitializeComponent();
            Textures = textures;
            FullTexturePaths = new List<string>();

            for (int i = 0; i < Textures.Count; i++)
            {
                FullTexturePaths.Add(basepath + "\\" + Textures[i]);
            }

            for (int i = 0; i < Textures.Count; i++)
            {
                TextureListBox.Items.Add(Textures[i]);
            }

        }

        private void TexSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FullTexturePaths.Count > 0 && TextureListBox.SelectedIndex < FullTexturePaths.Count)
            {
                if (System.IO.File.Exists(FullTexturePaths[TextureListBox.SelectedIndex]))
                {
                    if (Path.GetExtension(FullTexturePaths[TextureListBox.SelectedIndex]) == ".gif")
                    {
                        Bitmap Img = (Bitmap)Image.FromFile(FullTexturePaths[TextureListBox.SelectedIndex]).Clone();
                        TexPreviewBox.Size = Img.Size;
                        TexPreviewBox.Image = Img;
                    }
                    if (Path.GetExtension(FullTexturePaths[TextureListBox.SelectedIndex]) == ".gfx")
                    {
                        Bitmap Img = new RSDKvRS.gfx(FullTexturePaths[TextureListBox.SelectedIndex],false).gfxImage;
                        TexPreviewBox.Size = Img.Size;
                        TexPreviewBox.Image = Img;
                    }
                    if (Path.GetExtension(FullTexturePaths[TextureListBox.SelectedIndex]) == ".png")
                    {
                        Bitmap Img = (Bitmap)Image.FromFile(FullTexturePaths[TextureListBox.SelectedIndex]).Clone();
                        TexPreviewBox.Size = Img.Size;
                        TexPreviewBox.Image = Img;
                    }
                    if (Path.GetExtension(FullTexturePaths[TextureListBox.SelectedIndex]) == ".bmp")
                    {
                        Bitmap Img = (Bitmap)Image.FromFile(FullTexturePaths[TextureListBox.SelectedIndex]).Clone();
                        TexPreviewBox.Size = Img.Size;
                        TexPreviewBox.Image = Img;
                    }
                }
            }
        }

        private void NewTexButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".gif";
            dlg.Filter = "GIF Images|*.gif|PNG Images|*.png|Bitmap Images|*.bmp|Retro Engine Graphics|*.gfx";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryInfo di = new DirectoryInfo(dlg.FileName);
                string imgpath = Path.GetFileName(dlg.FileName);
                string texpath = di.Parent + "/" + imgpath; 
                TextureListBox.Items.Add(texpath);
                Textures.Add(texpath);
                FullTexturePaths.Add(dlg.FileName);
            }
        }

        private void DeleteCurTexButton_Click(object sender, EventArgs e)
        {
            if (TextureListBox.Items.Count > 0 && TextureListBox.SelectedIndex + 1 > 0)
            {
                TextureListBox.Items.RemoveAt(TextureListBox.SelectedIndex);
                Textures.RemoveAt(TextureListBox.SelectedIndex + 1);
                if (FullTexturePaths.Count > 0 && TextureListBox.SelectedIndex < FullTexturePaths.Count)
                {
                    FullTexturePaths.RemoveAt(TextureListBox.SelectedIndex + 1);
                }
            }
        }

        private void ReplaceCurTexButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".gif";
            dlg.Filter = "GIF Images|*.gif|PNG Images|*.png|Bitmap Images|*.bmp|Retro Engine Graphics|*.gfx";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryInfo di = new DirectoryInfo(dlg.FileName);
                string imgpath = Path.GetFileName(dlg.FileName);
                string texpath = di.Parent + "/" + imgpath;
                TextureListBox.Items[TextureListBox.SelectedIndex] = texpath;
                Textures[TextureListBox.SelectedIndex] = texpath;
                if (FullTexturePaths.Count > 0 && TextureListBox.SelectedIndex < FullTexturePaths.Count)
                {
                    FullTexturePaths[TextureListBox.SelectedIndex] = dlg.FileName;
                }
                else
                {
                    FullTexturePaths.Add(dlg.FileName);
                }
            }
        }
    }
}
