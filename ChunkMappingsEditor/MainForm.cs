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

namespace ChunkMappingsEditor
{
    public partial class MainForm : Form
    {

        enum RSDKver
        {
            NONE,
            RSDK1,
            RSDK2,
            RSDK3,
            RSDK4
        }

        int LoadedChunkVer = (int)RSDKver.RSDK3;

        int curChunk = 0;

        string filename = "";

        RSDKv1.til Chunksv1;
        RSDKv2.Tiles128x128 Chunksv2;
        RSDKv3.Tiles128x128 Chunksv3;
        RSDKv4.Tiles128x128 Chunksv4;

        Bitmap Tiles;

        public MainForm()
        {
            InitializeComponent();
        }

        void RedrawChunk()
        {
            Bitmap b = new Bitmap(128, 128);
            if (LoadedChunkVer == (int)RSDKver.RSDK4)
            {
                b = Chunksv4.RenderChunk(curChunk, Tiles);
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK3)
            {
                b = Chunksv3.RenderChunk(curChunk, Tiles);
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK2)
            {
                b = Chunksv2.RenderChunk(curChunk, Tiles);
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK1)
            {
                b = Chunksv1.RenderChunk(curChunk, Tiles);
            }

            ChunkDisplay.BackgroundImage = b;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKvB (Sonic 1 & 2 Remakes) Chunk Mappings|128x128Tiles.bin|RSDKv2 (Sonic CD) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic Nexus) Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                switch (dlg.FilterIndex - 1)
                {
                    case 0:
                        Chunksv1 = null;
                        Chunksv2 = null;
                        Chunksv3 = null;
                        Chunksv4 = new RSDKv4.Tiles128x128(dlg.FileName);
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif"));
                        LoadedChunkVer = (int)RSDKver.RSDK4;
                        LoadTileSet(Tiles);
                        RedrawChunk();
                        break;
                    case 1:
                        Chunksv1 = null;
                        Chunksv2 = null;
                        Chunksv3 = new RSDKv3.Tiles128x128(dlg.FileName);
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif"));
                        Chunksv4 = null;
                        LoadedChunkVer = (int)RSDKver.RSDK3;
                        LoadTileSet(Tiles);
                        RedrawChunk();
                        break;
                    case 2:
                        Chunksv1 = null;
                        Chunksv2 = new RSDKv2.Tiles128x128(dlg.FileName);
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif"));
                        Chunksv3 = null;
                        Chunksv4 = null;
                        LoadedChunkVer = (int)RSDKver.RSDK2;
                        LoadTileSet(Tiles);
                        RedrawChunk();
                        break;
                    default:
                        Chunksv1 = new RSDKv1.til(dlg.FileName);
                        Chunksv2 = null;
                        Chunksv3 = null;
                        Chunksv4 = null;
                        LoadedChunkVer = (int)RSDKver.RSDK1;
                        Tiles = new Bitmap(dlg.FileName.Replace("Zone.til", "Zone.gif"));
                        LoadTileSet(Tiles);
                        RedrawChunk();
                        break;
                }
            }
        }

        public void LoadTileSet(Bitmap TileSet)
        {
            StageTilesList.Images.Clear();
            int tsize = TileSet.Height;
            for (int i = 0; i < (tsize / 16); i++)
            {
                Rectangle CropArea = new Rectangle(0, (i * 16), 16, 16);
                Bitmap CroppedImage = CropImage(TileSet, CropArea);
                //Console.WriteLine("Data: i =" + i + " I*16 = " + (i * 16) + " CropArea.y = " + CropArea.Y);
                StageTilesList.Images.Add(CroppedImage);
            }
            StageTilesList.Refresh();
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void renderEachChunkAsAnImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
            {
                dlg.Description = "Select A Folder To Place The Chunks In";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (LoadedChunkVer != (int)RSDKver.RSDK1)
                    {
                    for (int i = 0; i < 512; i++)
                    {
                        if (LoadedChunkVer == (int)RSDKver.RSDK4)
                        {
                            Bitmap b;
                            b = Chunksv4.RenderChunk(i, Tiles);
                            b.Save(dlg.SelectedPath + "\\" + i + ".png");
                        }
                        if (LoadedChunkVer == (int)RSDKver.RSDK3)
                        {
                            Bitmap b;
                            b = Chunksv3.RenderChunk(i, Tiles);
                            b.Save(dlg.SelectedPath + "\\" + i + ".png");
                        }
                        if (LoadedChunkVer == (int)RSDKver.RSDK2)
                        {
                            Bitmap b;
                            b = Chunksv2.RenderChunk(i, Tiles);
                            b.Save(dlg.SelectedPath + "\\" + i + ".png");
                        }
                    }
                    }
                    else
                    {
                        for (int i = 0; i < 256; i++)
                        {
                            if (LoadedChunkVer == (int)RSDKver.RSDK1)
                            {
                                Bitmap b;
                                b = Chunksv1.RenderChunk(i, Tiles);
                                b.Save(dlg.SelectedPath + "\\" + i + ".png");
                            }
                        }
                    }
                }
                dlg.Dispose();
            }
        }

        private void OrientationBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void VisualBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CollisionABox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CollisionBBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StageTilesList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NextChunkButton_Click(object sender, EventArgs e)
        {
            curChunk++;
            if (curChunk > 511)
            {
                curChunk = 511;
            }
            RedrawChunk();
        }

        private void PrevChunkButton_Click(object sender, EventArgs e)
        {
            curChunk--;
            if (curChunk < 0)
            {
                curChunk = 0;
            }
            RedrawChunk();
        }
    }
}
