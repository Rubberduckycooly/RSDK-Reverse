using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.ChunkMappingsEditor
{
    public partial class MainForm : DockContent
    {
        //What RSDK version is loaded?
        Retro_Formats.EngineType engineType = 0;

        //Stuff that tells the program what chunk to display
        int curChunk = 0;
        int selectedTile = 0;
        int gotoChunk = 0;

        //What tile is being selected?
        Point tilepoint;

        //The current Chunk
        Bitmap DisplayedChunk = new Bitmap(128, 128);

        //Do we want a grid?
        bool showGrid = true;

        //Where is the chunk's file path?
        string filename = null;

        //Chunk Data
        Retro_Formats.MetaTiles Chunks = new Retro_Formats.MetaTiles();

        //Tileset
        Bitmap Tiles;
        public List<Bitmap> TileList = new List<Bitmap>();

        //Should Auto-Set Be turned on?
        bool AutoSetDirectionBool = false;
        bool AutoSetVisualPlaneBool = false;
        bool AutoSetCollisionABool = false;
        bool AutoSetCollisionBBool = false;
        bool AutoSet16x16TileBool = false;

        //Auto-Set Values
        byte AutoDirection;
        byte AutoVisualPlane;
        byte AutoCollisionA;
        byte AutoCollisionB;
        ushort AutoTile;

        //Zoom!
        private float ZoomLevel = 1; //TODO: Add Zoom Options!

        public MainForm()
        {
            InitializeComponent();
        }

        void RedrawChunk()
        {
            DisplayedChunk = Chunks.ChunkList[curChunk].Render(Tiles);//Draw the current chunk!
            OrientationBox.SelectedIndex = Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Direction;
            VisualBox.SelectedIndex = Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].VisualPlane;//Update Chunk Values!
            CollisionABox.SelectedIndex = Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag0;
            CollisionBBox.SelectedIndex = Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag1;
            ChunkNumberLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunks.ChunkList.Length + ":"; //What chunk are we on?


            int zw = (int)(DisplayedChunk.Width * ZoomLevel);
            int zh = (int)(DisplayedChunk.Height * ZoomLevel);
            DisplayedChunk = ResizeImage(DisplayedChunk, zw, zh);

            using (Graphics g = Graphics.FromImage(DisplayedChunk))
            {

                if (showGrid) // Do we want a grid?
                {
                    Size gridCellSize = new Size(16 * (int)ZoomLevel, 16 * (int)ZoomLevel); // how big should each cell be?
                    Bitmap mapLine = new Bitmap(128 * (int)ZoomLevel, 128 * (int)ZoomLevel); // how big is the image?

                    Pen pen = new Pen(Color.DarkGray);

                    if (gridCellSize.Width >= 8 && gridCellSize.Height >= 8)
                    {
                        int lft = 0 % gridCellSize.Width;
                        int top = 0 % gridCellSize.Height;
                        int cntX = 128 / gridCellSize.Width;
                        int cntY = 128 / gridCellSize.Height;

                        for (int i = 0; i <= mapLine.Width; ++i)
                        {
                            g.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, mapLine.Height); //Draw Lines every 128 Pixels along the width
                        }

                        for (int j = 0; j <= mapLine.Height; ++j)
                        {
                            g.DrawLine(pen, 0, top + j * gridCellSize.Height, mapLine.Width, top + j * gridCellSize.Height); //Draw Lines every 128 Pixels along the height
                        }

                    }

                    g.TranslateTransform(0, 0);// No idea lmao
                    g.ResetTransform(); //Still No idea lmao
                    mapLine.Dispose(); //Delet This!
                }

                Pen Recpen = new Pen(Color.Yellow); //Draw a yellow rectangle to show the user what tile they are editing!
                g.DrawRectangle(Recpen, new Rectangle(tilepoint.X * 16, tilepoint.Y * 16, 16, 16));
            }

            ChunkDisplay.BackgroundImage = DisplayedChunk; //We want the chunk to show up! So make the background image display the currect chunk!
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKvB Chunk Mappings|128x128Tiles.bin|RSDKv2 Chunk Mappings|128x128Tiles.bin|RSDKv1 Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";
            string RSDK = "RSDKvB";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                curChunk = 0;
                filename = dlg.FileName;
                switch (dlg.FilterIndex - 1) //What RSDK version was loaded?
                {
                    case 0:
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif")); //A Zone's Tileset should be in the same folder as its chunk mappings
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        Chunks.ImportFrom(engineType, dlg.FileName);
                        RSDK = "RSDKvB";
                        break;
                    case 1:
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif")); //A Zone's Tileset should be in the same folder as its chunk mappings
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        Chunks.ImportFrom(engineType, dlg.FileName);
                        RSDK = "RSDKv2";
                        break;
                    case 2:
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif")); //A Zone's Tileset should be in the same folder as its chunk mappings
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        Chunks.ImportFrom(engineType,dlg.FileName);
                        RSDK = "RSDKv1";
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        Chunks.ImportFrom(engineType, dlg.FileName);
                        RSDKvRS.gfx gfx = new RSDKvRS.gfx(dlg.FileName.Replace("Zone.til", "Zone.gfx"), false); //A Zone's Tileset should be in the same folder as its chunk mappings
                        Tiles = new Bitmap(gfx.gfxImage);
                        RSDK = "RSDKvRS";
                        break;
                }

                LoadTileSet(Tiles);
                LoadChunks(Tiles);
                GotoNUD.Maximum = Chunks.MaxChunks;
                RedrawChunk();

                string dispname = "";
                string folder = Path.GetDirectoryName(filename);
                DirectoryInfo di = new DirectoryInfo(folder);
                folder = di.Name;
                string file = Path.GetFileName(filename);

                if (filename != null)
                {
                    RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                    dispname = folder + "/" + file;
                }
                else
                {
                    RetroED.MainForm.Instance.CurrentTabText = "New Scene - RSDK Map Editor";
                    dispname = "New Scene - RSDK Map Editor";
                }



                RetroED.MainForm.Instance.rp.state = "RetroED - " + this.Text;
                RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
                SharpPresence.Discord.RunCallbacks();
                SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);

            }
        }

        public void LoadTileSet(Bitmap TileSet)
        {
            StageTilesList.Images.Clear(); // Clear the previous images, since we load the entire file!
            TileList.Clear();
            int tsize = TileSet.Height; //Height of the image in pixels
            for (int i = 0; i < (tsize / 16); i++) //We divide by 16 to get the "height" in blocks
            {
                Rectangle CropArea = new Rectangle(0, (i * 16), 16, 16); //we then get tile at Y: i * 16, 
                //we have to multiply i by 16 to get the "true Tile value" (1* 16 = 16, 2 * 16 = 32, etc.)

                Bitmap CroppedImage = CropImage(TileSet, CropArea); // crop that image
                StageTilesList.Images.Add(CroppedImage); // add it to the tile list
                TileList.Add(CroppedImage);
            }
            StageTilesList.Refresh(); // Update the tileList control
        }

        public void LoadChunks(Bitmap TileSet)
        {
            StageChunksList.Images.Clear(); // Clear the previous images, since we load the entire file!
            for (int i = 0; i < Chunks.MaxChunks; i++)
            {
                StageChunksList.Images.Add(Chunks.ChunkList[i].Render(TileSet));
            }
            StageChunksList.Refresh(); // Update the tileList control
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

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename != null)
            {
                string dispname = "";
                string folder = Path.GetDirectoryName(filename);
                DirectoryInfo di = new DirectoryInfo(folder);
                folder = di.Name;
                string file = Path.GetFileName(filename);

                if (filename != null)
                {
                    RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                    dispname = folder + "/" + file;
                }
                else
                {
                    RetroED.MainForm.Instance.CurrentTabText = "New Scene - RSDK Map Editor";
                    dispname = "New Scene - RSDK Map Editor";
                }
                Chunks.ExportTo(engineType, filename);
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
            dlg.Filter = "RSDKvB Chunk Mappings|128x128Tiles.bin|RSDKv2 Chunk Mappings|128x128Tiles.bin|RSDKv1 Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
                string RSDK = "RSDKvB";
                switch(dlg.FilterIndex-1)
                {
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        RSDK = "RSDKvB";
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        RSDK = "RSDKv2";
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        RSDK = "RSDKv1";
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        RSDK = "RSDKvRS";
                        break;
                }

                string dispname = "";
                string folder = Path.GetDirectoryName(filename);
                DirectoryInfo di = new DirectoryInfo(folder);
                folder = di.Name;
                string file = Path.GetFileName(filename);

                if (filename != null)
                {
                    RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                    dispname = folder + "/" + file;
                }
                else
                {
                    RetroED.MainForm.Instance.CurrentTabText = "New Chunk Mappings - RSDK Chunk Mappings Editor";
                    dispname = "New Chunk Mappings - RSDK Chunk Mappings Editor";
                }

                RetroED.MainForm.Instance.rp.state = "RetroED - " + this.Text;
                RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
                SharpPresence.Discord.RunCallbacks();
                SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);

                Chunks.ExportTo(engineType, filename);
            }
        }

        private void renderEachChunkAsAnImageToolStripMenuItem_Click(object sender, EventArgs e) //Sounds Simple Enough...
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
            {
                dlg.Description = "Select A Folder To Place The Chunks In";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    for (int i = 0; i < Chunks.MaxChunks; i++)
                    {
                        Bitmap b;
                        b = Chunks.ChunkList[i].Render(Tiles);
                        b.Save(dlg.SelectedPath + "\\" + i + ".png");
                    }
                }
                dlg.Dispose();
            }
        }

        private void ChunkDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            tilepoint = new Point(((int)(e.X )) / (int)(16 * ZoomLevel), ((int)(e.Y )) / (int)(16 * ZoomLevel)); //Get the tile that was clicked, not the position on the screen
            //Make sure our point is in the chunk!
            if (tilepoint.X < 0) tilepoint.X = 0;
            if (tilepoint.Y < 0) tilepoint.Y = 0;
            if (tilepoint.X >= 8) tilepoint.X = 7;
            if (tilepoint.Y >= 8) tilepoint.Y = 7;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (AutoSetDirectionBool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Direction = AutoDirection;
                    }
                    if (AutoSetVisualPlaneBool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].VisualPlane = AutoVisualPlane;
                    }
                    if (AutoSetCollisionABool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag0 = AutoCollisionA;
                    }
                    if (AutoSetCollisionBBool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag1 = AutoCollisionB;
                    }
                    if (MenuItem_PlaceTiles.Checked)
                    {
                        if (StageTilesList.SelectedIndex < 0) StageTilesList.SelectedIndex = 0;
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    }
                    RedrawChunk(); //If you don't know what this would do then you clearly shouldn't be here lol
              break;
                case MouseButtons.Middle:
                    StageTilesList.SelectedIndex = Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Tile16x16;
                    StageTilesList.Refresh();
              break;
            }
        }

        private void OrientationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Direction = (byte)OrientationBox.SelectedIndex;
            RedrawChunk();
        }

        private void VisualBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].VisualPlane = (byte)VisualBox.SelectedIndex;
            RedrawChunk();
        }

        private void CollisionABox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
            RedrawChunk();
        }

        private void CollisionBBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
            RedrawChunk();
        }

        private void StageTilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void NextChunkButton_Click(object sender, EventArgs e)
        {
            curChunk++; //go to the next chunk
            if (curChunk > Chunks.MaxChunks - 1) //Make sure we don't go further than the amount of chunks we have
            {
                curChunk = Chunks.MaxChunks - 1;
            }
            StageChunksList.SelectedIndex = curChunk;
            StageChunksList.Refresh();
            RedrawChunk();
        }

        private void PrevChunkButton_Click(object sender, EventArgs e)
        {
            curChunk--;//go to the previous chunk
            if (curChunk < 0) //Don't go below zero
            {
                curChunk = 0;
            }
            StageChunksList.SelectedIndex = curChunk;
            StageChunksList.Refresh();
            RedrawChunk();
        }

        private void MenuItem_ShowGrid_Click(object sender, EventArgs e)
        {
            MenuItem_ShowGrid.Checked = !MenuItem_ShowGrid.Checked;
            showGrid = MenuItem_ShowGrid.Checked;
            RedrawChunk();
        }

        private void GotoNUD_ValueChanged(object sender, EventArgs e)
        {
            if (GotoNUD.Value <= Chunks.MaxChunks)
            {
                gotoChunk = (int)GoToChunkNUD.Value - 1;
            }
            else
            {
                GotoNUD.Value = Chunks.MaxChunks;
                gotoChunk = (int)GoToChunkNUD.Value - 1;
            }
            if (gotoChunk <= 0)
            {
                gotoChunk = 0;
            }
        }

        private void GotoButton_Click(object sender, EventArgs e)
        {
            curChunk = gotoChunk;
            RedrawChunk();
        }

        private void MenuItem_ASOrientation_Click(object sender, EventArgs e)
        {
            if (AutoSetDirectionBool)
            {
                AutoSetDirectionBool = false;
                MenuItem_ASOrientation.Checked = false;
            }
            else if (!AutoSetDirectionBool)
            {
                AutoSetDirectionBool = true;
                MenuItem_ASOrientation.Checked = true;
            }
        }

        private void MenuItem_ASVisPlane_Click(object sender, EventArgs e)
        {
            if (AutoSetVisualPlaneBool)
            {
                AutoSetVisualPlaneBool = false;
                MenuItem_ASVisPlane.Checked = false;
            }
            else if (!AutoSetVisualPlaneBool)
            {
                AutoSetVisualPlaneBool = true;
                MenuItem_ASVisPlane.Checked = true;
            }
        }

        private void MenuItem_ASCollA_Click(object sender, EventArgs e)
        {
            if (AutoSetCollisionABool)
            {
                AutoSetCollisionABool = false;
                MenuItem_ASCollA.Checked = false;
            }
            else if (!AutoSetCollisionABool)
            {
                AutoSetCollisionABool = true;
                MenuItem_ASCollA.Checked = true;
            }
        }

        private void MenuItem_ASCollB_Click(object sender, EventArgs e)
        {
            if (AutoSetCollisionBBool)
            {
                AutoSetCollisionBBool = false;
                MenuItem_ASCollB.Checked = false;
            }
            else if (!AutoSetCollisionBBool)
            {
                AutoSetCollisionBBool = true;
                MenuItem_ASCollB.Checked = true;
            }
        }

        private void setAutoMenuItem_ASOrientation_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetOrientation frm = new ChunkEditor.AutoSetOrientation();
            frm.ShowDialog();
            AutoDirection = frm.Value;
        }

        private void setAutoMenuItem_ASVisPlane_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetVisualPlane frm = new ChunkEditor.AutoSetVisualPlane();
            frm.ShowDialog();
            AutoVisualPlane = frm.Value;
        }

        private void setAutoMenuItem_ASCollA_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetCollisionA frm = new ChunkEditor.AutoSetCollisionA();
            frm.ShowDialog();
            AutoCollisionA = frm.Value;
        }

        private void setAutoMenuItem_ASCollB_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetCollisionB frm = new ChunkEditor.AutoSetCollisionB();
            frm.ShowDialog();
            AutoCollisionB = frm.Value;
        }

        private void copyChunkToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.CopyChunkForm frm = new ChunkEditor.CopyChunkForm();
            frm.numericUpDown1.Value = curChunk + 1;
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Chunks.ChunkList[frm.DestinationChunk] = Chunks.Clone(frm.SourceChunk);
            }
        }

        private void StageChunksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StageChunksList.SelectedIndex >= 0)
            {
                curChunk = StageChunksList.SelectedIndex;
                RedrawChunk();
            }
        }

        private void refreshTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTileSet(Tiles);
        }

        private void refreshChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadChunks(Tiles);
        }

        private void ChunkDisplay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TileIDNUD_ValueChanged(object sender, EventArgs e)
        {
            StageTilesList.SelectedIndex = (int)TileIDNUD.Value;
         }

        private void ChunkZoomBar_Scroll(object sender, EventArgs e)
        {
            StageChunksList.ImageSize = (64 * ChunkZoomBar.Value);
            StageChunksList.ImageWidth = (64 * ChunkZoomBar.Value);
            StageChunksList.ImageHeight = (64 * ChunkZoomBar.Value);
        }

        private void TileZoomBar_Scroll(object sender, EventArgs e)
        {
            StageTilesList.ImageSize = (16 * TileZoomBar.Value);
            StageTilesList.ImageWidth = (16 * TileZoomBar.Value);
            StageTilesList.ImageHeight = (16 * TileZoomBar.Value);
        }

        private void MenuItem_PlaceTiles_Click(object sender, EventArgs e)
        {
            MenuItem_PlaceTiles.Checked = !MenuItem_PlaceTiles.Checked;
        }

        private void ChunkDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    tilepoint = new Point(((int)(e.X)) / (int)(16 * ZoomLevel), ((int)(e.Y)) / (int)(16 * ZoomLevel)); //Get the tile that was clicked, not the position on the screen
                    //Make sure our point is in the chunk!
                    if (tilepoint.X < 0) tilepoint.X = 0;
                    if (tilepoint.Y < 0) tilepoint.Y = 0;
                    if (tilepoint.X >= 8) tilepoint.X = 7;
                    if (tilepoint.Y >= 8) tilepoint.Y = 7;

                    if (AutoSetDirectionBool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Direction = AutoDirection;
                    }
                    if (AutoSetVisualPlaneBool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].VisualPlane = AutoVisualPlane;
                    }
                    if (AutoSetCollisionABool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag0 = AutoCollisionA;
                    }
                    if (AutoSetCollisionBBool)
                    {
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].CollisionFlag1 = AutoCollisionB;
                    }
                    if (MenuItem_PlaceTiles.Checked)
                    {
                        if (StageTilesList.SelectedIndex < 0) StageTilesList.SelectedIndex = 0;
                        Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    }
                    RedrawChunk(); //If you don't know what this would do then you clearly shouldn't be here lol
                    break;
                case MouseButtons.Middle:
                    StageTilesList.SelectedIndex = Chunks.ChunkList[curChunk].Mappings[tilepoint.Y][tilepoint.X].Tile16x16;
                    StageTilesList.Refresh();
                    break;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chunks = new Retro_Formats.MetaTiles();
            curChunk = 0;
            tilepoint = new Point();
            Tiles = new Bitmap(16, 16368);
            using (Graphics g = Graphics.FromImage(Tiles))
            {
                g.Clear(Color.FromArgb(255, 255, 0, 255));
            }
            LoadTileSet(Tiles);
            LoadChunks(Tiles);
            GotoNUD.Maximum = Chunks.MaxChunks;

            RetroED.MainForm.Instance.rp.state = "RetroED - " + this.Text;
            RetroED.MainForm.Instance.rp.details = "Editing: New Chunk Mappings";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);

            RedrawChunk();
        }
    }
}
