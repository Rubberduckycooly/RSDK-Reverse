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

        enum RSDKver //All 4 Versions use the same file format lol
        {
            NONE,
            RSDK1,
            RSDK2,
            RSDK3,
            RSDK4
        }

        int LoadedChunkVer = (int)RSDKver.RSDK3;

        int curChunk = 0;
        int selectedTile = 0;

        Point tilepoint;
        Bitmap DisplayedChunk = new Bitmap(128, 128);

        bool showGrid = true;

        string filename = null;

        RSDKv1.til Chunksv1;
        RSDKv2.Tiles128x128 Chunksv2;
        RSDKv3.Tiles128x128 Chunksv3;
        RSDKv4.Tiles128x128 Chunksv4;

        Bitmap Tiles;

        private float ZoomLevel = 1; //TODO: Add Zoom Options!

        public MainForm()
        {
            InitializeComponent();
        }

        void RedrawChunk()
        {
            if (LoadedChunkVer == (int)RSDKver.RSDK4)
            {
                DisplayedChunk = Chunksv4.RenderChunk(curChunk, Tiles);
                OrientationBox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[selectedTile].Direction;
                VisualBox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[selectedTile].VisualPlane;
                CollisionABox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1;
                ChunkNoLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv4.BlockList.Count + ":";
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK3)
            {
                DisplayedChunk = Chunksv3.RenderChunk(curChunk, Tiles);
                OrientationBox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[selectedTile].Direction;
                VisualBox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[selectedTile].VisualPlane;
                CollisionABox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1;
                ChunkNoLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv3.BlockList.Count + ":";
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK2)
            {
                DisplayedChunk = Chunksv2.RenderChunk(curChunk, Tiles);
                OrientationBox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[selectedTile].Direction;
                VisualBox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[selectedTile].VisualPlane;
                CollisionABox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1;
                ChunkNoLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv2.BlockList.Count + ":";
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK1)
            {
                DisplayedChunk = Chunksv1.RenderChunk(curChunk, Tiles);
                OrientationBox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[selectedTile].Orientation;
                VisualBox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[selectedTile].VisualPlane;
                CollisionABox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1;
                ChunkNoLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv1.BlockList.Count + ":";
            }

            using (Graphics g = Graphics.FromImage(DisplayedChunk))
            {

                if (showGrid)
            {
                Size gridCellSize = new Size(16, 16);
                Bitmap mapLine = new Bitmap(128, 128);

                    Pen pen = new Pen(Color.DarkGray);

                    if (gridCellSize.Width >= 8 && gridCellSize.Height >= 8)
                    {
                        int lft = 0 % gridCellSize.Width;
                        int top = 0 % gridCellSize.Height;
                        int cntX = 128 / gridCellSize.Width;
                        int cntY = 128 / gridCellSize.Height;

                        for (int i = 0; i <= mapLine.Width; ++i)
                        {
                            g.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, mapLine.Height);
                        }

                        for (int j = 0; j <= mapLine.Height; ++j)
                        {
                            g.DrawLine(pen, 0, top + j * gridCellSize.Height, mapLine.Width, top + j * gridCellSize.Height);
                        }

                    }

                    g.TranslateTransform(0, 0);
                    g.ResetTransform();
                    mapLine.Dispose();
                }
           

            Pen Recpen = new Pen(Color.Yellow);
            g.DrawRectangle(Recpen, new Rectangle(tilepoint.X * 16, tilepoint.Y * 16, 16, 16));
        }

        ChunkDisplay.BackgroundImage = DisplayedChunk;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKvB (Sonic 1 & 2 Remakes) Chunk Mappings|128x128Tiles.bin|RSDKv2 (Sonic CD) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic Nexus) Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                curChunk = 0;
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
            if (filename != null)
            {
                switch (LoadedChunkVer)
                {
                    case 1:
                        Chunksv1.Write(filename);
                        break;
                    case 2:
                        Chunksv2.Write(filename);
                        break;
                    case 3:
                        Chunksv3.Write(filename);
                        break;
                    case 4:
                        Chunksv4.Write(filename);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKvB (Sonic 1 & 2 Remakes) Chunk Mappings|128x128Tiles.bin|RSDKv2 (Sonic CD) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic Nexus) Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                switch (LoadedChunkVer)
                {
                    case 1:
                        Chunksv1.Write(dlg.FileName);
                        break;
                    case 2:
                        Chunksv2.Write(dlg.FileName);
                        break;
                    case 3:
                        Chunksv3.Write(dlg.FileName);
                        break;
                    case 4:
                        Chunksv4.Write(dlg.FileName);
                        break;
                    default:
                        break;
                }
            }
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

        private void ChunkDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            tilepoint = new Point(((int)(e.X / ZoomLevel)) / 16, ((int)(e.Y / ZoomLevel)) / 16);
            if (tilepoint.X >= 8 | tilepoint.Y >= 8) return;
            Console.WriteLine("Hello From " + tilepoint.X + " " + tilepoint.Y);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    //Draw a yellow box around the tilepoint & change the selected Mapping tile 

                    switch (tilepoint.Y)
                    {
                        case 0:
                            selectedTile = tilepoint.X;
                            break;
                        case 1:
                            selectedTile = tilepoint.X + 8;
                            break;
                        case 2:
                            selectedTile = tilepoint.X + 16;
                            break;
                        case 3:
                            selectedTile = tilepoint.X + 24;
                            break;
                        case 4:
                            selectedTile = tilepoint.X + 32;
                            break;
                        case 5:
                            selectedTile = tilepoint.X + 40;
                            break;
                        case 6:
                            selectedTile = tilepoint.X + 48;
                            break;
                        case 7:
                            selectedTile = tilepoint.X + 56;
                            break;
                        default:
                            break;
                    }
                    RedrawChunk();
              break;
            }
        }

        private void OrientationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[selectedTile].Orientation = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[selectedTile].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[selectedTile].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[selectedTile].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void VisualBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[selectedTile].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[selectedTile].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[selectedTile].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[selectedTile].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void CollisionABox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[selectedTile].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void CollisionBBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[selectedTile].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void StageTilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[selectedTile].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[selectedTile].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[selectedTile].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[selectedTile].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
            }
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

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGridToolStripMenuItem.Checked = !showGridToolStripMenuItem.Checked;
            showGrid = showGridToolStripMenuItem.Checked;
            RedrawChunk();
        }
    }
}
