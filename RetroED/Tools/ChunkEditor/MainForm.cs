using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RetroED.Tools.ChunkMappingsEditor
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

        //What RSDK version is loaded?
        int LoadedChunkVer = (int)RSDKver.RSDK3;

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

        public RetroED.MainForm Parent;

        //Chunk Data
        RSDKvRS.Tiles128x128 Chunksv1;
        RSDKv1.Tiles128x128 Chunksv2;
        RSDKv2.Tiles128x128 Chunksv3;
        RSDKvB.Tiles128x128 Chunksv4;

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
            if (LoadedChunkVer == (int)RSDKver.RSDK4)
            {
                DisplayedChunk = Chunksv4.RenderChunk(curChunk, Tiles);//Draw the current chunk!
                OrientationBox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction;
                VisualBox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane;//Update Chunk Values!
                CollisionABox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1;
                ChunkNumberLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv4.BlockList.Count + ":"; //What chunk are we on?
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK3)
            {
                DisplayedChunk = Chunksv3.RenderChunk(curChunk, Tiles); //Draw the current chunk!
                OrientationBox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction;
                VisualBox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane;//Update Chunk Values!
                CollisionABox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1;
                ChunkNumberLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv3.BlockList.Count + ":";//What chunk are we on?
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK2)
            {
                DisplayedChunk = Chunksv2.RenderChunk(curChunk, Tiles); //Draw the current chunk!
                OrientationBox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction;
                VisualBox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane;//Update Chunk Values!
                CollisionABox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1;
                ChunkNumberLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv2.BlockList.Count + ":";//What chunk are we on?
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK1)
            {
                DisplayedChunk = Chunksv1.RenderChunk(curChunk, Tiles); //Draw the current chunk!
                OrientationBox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction; //Update Chunk Values!
                VisualBox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane;
                CollisionABox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0;
                CollisionBBox.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1;
                ChunkNumberLabel.Text = "Chunk " + (curChunk + 1) + " Of " + Chunksv1.BlockList.Count + ":";//What chunk are we on?
            }

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
            dlg.Filter = "RSDKvB (Sonic 1 & 2 Remakes) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic CD) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic Nexus) Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                curChunk = 0;
                filename = dlg.FileName;
                switch (dlg.FilterIndex - 1) //What RSDK version was loaded?
                {
                    case 0:
                        Chunksv1 = null;
                        Chunksv2 = null;
                        Chunksv3 = null;
                        Chunksv4 = new RSDKvB.Tiles128x128(dlg.FileName);
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif")); //A Zone's Tileset should be in the same folder as its chunk mappings
                        LoadedChunkVer = (int)RSDKver.RSDK4;
                        LoadTileSet(Tiles);
                        LoadChunks(Tiles);
                        GotoNUD.Maximum = 512;
                        RedrawChunk();
                        break;
                    case 1:
                        Chunksv1 = null;
                        Chunksv2 = null;
                        Chunksv3 = new RSDKv2.Tiles128x128(dlg.FileName);
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif")); //A Zone's Tileset should be in the same folder as its chunk mappings
                        Chunksv4 = null;
                        LoadedChunkVer = (int)RSDKver.RSDK3;
                        LoadTileSet(Tiles);
                        LoadChunks(Tiles);
                        GotoNUD.Maximum = 512;
                        RedrawChunk();
                        break;
                    case 2:
                        Chunksv1 = null;
                        Chunksv2 = new RSDKv1.Tiles128x128(dlg.FileName);
                        Tiles = new Bitmap(dlg.FileName.Replace("128x128Tiles.bin", "16x16Tiles.gif")); //A Zone's Tileset should be in the same folder as its chunk mappings
                        Chunksv3 = null;
                        Chunksv4 = null;
                        LoadedChunkVer = (int)RSDKver.RSDK2;
                        LoadTileSet(Tiles);
                        LoadChunks(Tiles);
                        GotoNUD.Maximum = 512;
                        RedrawChunk();
                        break;
                    default:
                        Chunksv1 = new RSDKvRS.Tiles128x128(dlg.FileName);
                        Chunksv2 = null;
                        Chunksv3 = null;
                        Chunksv4 = null;
                        LoadedChunkVer = (int)RSDKver.RSDK1;
                        RSDKvRS.gfx gfx = new RSDKvRS.gfx(dlg.FileName.Replace("Zone.til", "Zone.gfx"), false); //A Zone's Tileset should be in the same folder as its chunk mappings
                        Tiles = new Bitmap(gfx.gfxImage);
                        LoadTileSet(Tiles);
                        LoadChunks(Tiles);
                        GotoNUD.Maximum = 256; // Retro Sonic Only Supports 256 Chunks per File :(
                        RedrawChunk();
                        break;
                }

                Parent.rp.state = "RetroED - " + this.Text;
                Parent.rp.details = "Editing: " + System.IO.Path.GetFileName(dlg.FileName);
                SharpPresence.Discord.RunCallbacks();
                SharpPresence.Discord.UpdatePresence(Parent.rp);

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

            switch (LoadedChunkVer)
            {
                case (int)RSDKver.RSDK1:
                    for (int i = 0; i < 256; i++)
                    {
                        StageChunksList.Images.Add(Chunksv1.BlockList[i].Render(TileSet));
                    }
                    break;
                case (int)RSDKver.RSDK2:
                    for (int i = 0; i < 512; i++)
                    {
                        StageChunksList.Images.Add(Chunksv2.BlockList[i].Render(TileSet));
                    }
                    break;
                case (int)RSDKver.RSDK3:
                    for (int i = 0; i < 512; i++)
                    {
                        StageChunksList.Images.Add(Chunksv3.BlockList[i].Render(TileSet));
                    }
                    break;
                case (int)RSDKver.RSDK4:
                    for (int i = 0; i < 512; i++)
                    {
                        StageChunksList.Images.Add(Chunksv4.BlockList[i].Render(TileSet));
                    }
                    break;
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
            dlg.Filter = "RSDKvB (Sonic 1 & 2 Remakes) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic CD) Chunk Mappings|128x128Tiles.bin|RSDKv1 (Sonic Nexus) Chunk Mappings|128x128Tiles.bin|Retro-Sonic Chunk Mappings|Zone.til";

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

        private void renderEachChunkAsAnImageToolStripMenuItem_Click(object sender, EventArgs e) //Sounds Simple Enough...
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
            tilepoint = new Point(((int)(e.X )) / (int)(16 * ZoomLevel), ((int)(e.Y )) / (int)(16 * ZoomLevel)); //Get the tile that was clicked, not the position on the screen
            if (tilepoint.X >= 8 | tilepoint.Y >= 8) return; //Chunks dont have more than 8 tiles vertically OR horizontally!
            Console.WriteLine(tilepoint.X + " " + tilepoint.Y);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (LoadedChunkVer == 1) //if its on, Auto-Set Kicks in!
                    {
                        if (AutoSetDirectionBool)
                        {
                            Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = AutoDirection;
                        }
                        if (AutoSetVisualPlaneBool)
                        {
                            Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = AutoVisualPlane;
                        }
                        if (AutoSetCollisionABool)
                        {
                            Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = AutoCollisionA;
                        }
                        if (AutoSetCollisionBBool)
                        {
                            Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = AutoCollisionB;
                        }
                        if (AutoSet16x16TileBool)
                        {
                            Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = AutoTile;
                        }
                    }
                    if (LoadedChunkVer == 2) //if its on, Auto-Set Kicks in!
                    {
                        if (AutoSetDirectionBool)
                        {
                            Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = AutoDirection;
                        }
                        if (AutoSetVisualPlaneBool)
                        {
                            Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = AutoVisualPlane;
                        }
                        if (AutoSetCollisionABool)
                        {
                            Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = AutoCollisionA;
                        }
                        if (AutoSetCollisionBBool)
                        {
                            Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = AutoCollisionB;
                        }
                        if (AutoSet16x16TileBool)
                        {
                            Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = AutoTile;
                        }
                    }
                    if (LoadedChunkVer == 3) //if its on, Auto-Set Kicks in!
                    {
                        if (AutoSetDirectionBool)
                        {
                            Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = AutoDirection;
                        }
                        if (AutoSetVisualPlaneBool)
                        {
                            Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = AutoVisualPlane;
                        }
                        if (AutoSetCollisionABool)
                        {
                            Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = AutoCollisionA;
                        }
                        if (AutoSetCollisionBBool)
                        {
                            Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = AutoCollisionB;
                        }
                        if (AutoSet16x16TileBool)
                        {
                            Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = AutoTile;
                        }
                    }
                    if (LoadedChunkVer == 4) //if its on, Auto-Set Kicks in!
                    {
                        if (AutoSetDirectionBool)
                        {
                            Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = AutoDirection;
                        }
                        if (AutoSetVisualPlaneBool)
                        {
                            Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = AutoVisualPlane;
                        }
                        if (AutoSetCollisionABool)
                        {
                            Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = AutoCollisionA;
                        }
                        if (AutoSetCollisionBBool)
                        {
                            Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = AutoCollisionB;
                        }
                        if (AutoSet16x16TileBool)
                        {
                            Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = AutoTile;
                        }
                    }
                    RedrawChunk(); //If you don't know what this would do then you clearly shouldn't be here lol
              break;
                case MouseButtons.Middle:
                    switch (LoadedChunkVer)
                    {
                        case (int)RSDKver.RSDK4:
                            StageTilesList.SelectedIndex = Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16;
                            break;
                        case (int)RSDKver.RSDK3:
                            StageTilesList.SelectedIndex = Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16;
                            break;
                        case (int)RSDKver.RSDK2:
                            StageTilesList.SelectedIndex = Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16;
                            break;
                        case (int)RSDKver.RSDK1:
                            StageTilesList.SelectedIndex = Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16;
                            break;
                    }
                    StageTilesList.Refresh();
              break;
            }
        }

        private void OrientationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Direction = (byte)OrientationBox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void VisualBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].VisualPlane = (byte)VisualBox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void CollisionABox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag0 = (byte)CollisionABox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void CollisionBBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].CollisionFlag1 = (byte)CollisionBBox.SelectedIndex;
                    RedrawChunk();
                    break;
            }
        }

        private void StageTilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LoadedChunkVer)
            {
                case 1:
                    Chunksv1.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
                case 2:
                    Chunksv2.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
                case 3:
                    Chunksv3.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
                case 4:
                    Chunksv4.BlockList[curChunk].Mapping[tilepoint.Y][tilepoint.X].Tile16x16 = (ushort)StageTilesList.SelectedIndex;
                    RedrawChunk();
                    break;
            }
            if (StageTilesList.SelectedIndex >= 0)
            { 
                TileIDNUD.Value = StageTilesList.SelectedIndex;
            }
        }

        private void NextChunkButton_Click(object sender, EventArgs e)
        {
            curChunk++; //go to the next chunk
            if (LoadedChunkVer != (int)RSDKver.RSDK1 && curChunk > 511) //Make sure we don't go further than the amount of chunks we have
            {
                curChunk = 511;
            }
            if (LoadedChunkVer == (int)RSDKver.RSDK1 && curChunk > 255) //Make sure we don't go further than the amount of chunks we have (But for Retro-Sonic this time!)
            {
                curChunk = 255;
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

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGridToolStripMenuItem.Checked = !showGridToolStripMenuItem.Checked;
            showGrid = showGridToolStripMenuItem.Checked;
            RedrawChunk();
        }

        private void GotoNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedChunkVer == 1)
            {
                if (GotoNUD.Value <= 256)
                {
                    gotoChunk = (int)GoToChunkNUD.Value - 1;
                }
                else
                {
                    GotoNUD.Value = 256;
                    gotoChunk = (int)GoToChunkNUD.Value - 1;
                }
            }
            if (LoadedChunkVer >= 1)
            {
                if (GotoNUD.Value <= 512)
                {
                    gotoChunk = (int)GoToChunkNUD.Value - 1;
                }
                else
                {
                    GotoNUD.Value = 512;
                    gotoChunk = (int)GoToChunkNUD.Value - 1;
                }
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

        private void orientationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AutoSetDirectionBool)
            {
                AutoSetDirectionBool = false;
                orientationToolStripMenuItem.Checked = false;
            }
            else if (!AutoSetDirectionBool)
            {
                AutoSetDirectionBool = true;
                orientationToolStripMenuItem.Checked = true;
            }
        }

        private void visualPlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AutoSetVisualPlaneBool)
            {
                AutoSetVisualPlaneBool = false;
                visualPlaneToolStripMenuItem.Checked = false;
            }
            else if (!AutoSetVisualPlaneBool)
            {
                AutoSetVisualPlaneBool = true;
                visualPlaneToolStripMenuItem.Checked = true;
            }
        }

        private void collisionAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AutoSetCollisionABool)
            {
                AutoSetCollisionABool = false;
                collisionAToolStripMenuItem.Checked = false;
            }
            else if (!AutoSetCollisionABool)
            {
                AutoSetCollisionABool = true;
                collisionAToolStripMenuItem.Checked = true;
            }
        }

        private void collisionBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AutoSetCollisionBBool)
            {
                AutoSetCollisionBBool = false;
                collisionBToolStripMenuItem.Checked = false;
            }
            else if (!AutoSetCollisionBBool)
            {
                AutoSetCollisionBBool = true;
                collisionBToolStripMenuItem.Checked = true;
            }
        }

        private void tile16x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AutoSet16x16TileBool)
            {
                AutoSet16x16TileBool = false;
                tile16x16ToolStripMenuItem.Checked = false;
            }
            else if (!AutoSet16x16TileBool)
            {
                AutoSet16x16TileBool = true;
                tile16x16ToolStripMenuItem.Checked = true;
            }
        }

        private void setAutoOrientationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetOrientation frm = new ChunkEditor.AutoSetOrientation();
            frm.ShowDialog();
            AutoDirection = frm.Value;
        }

        private void setAutoVisualPlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetVisualPlane frm = new ChunkEditor.AutoSetVisualPlane();
            frm.ShowDialog();
            AutoVisualPlane = frm.Value;
        }

        private void setAutoCollisionAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetCollisionA frm = new ChunkEditor.AutoSetCollisionA();
            frm.ShowDialog();
            AutoCollisionA = frm.Value;
        }

        private void setAutoCollisionBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetCollisionB frm = new ChunkEditor.AutoSetCollisionB();
            frm.ShowDialog();
            AutoCollisionB = frm.Value;
        }

        private void setAutoTile16x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.AutoSetTiles frm = new ChunkEditor.AutoSetTiles(StageTilesList);
            frm.ShowDialog();
            AutoTile = frm.Value;
        }

        private void copyChunkToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChunkEditor.CopyChunkForm frm = new ChunkEditor.CopyChunkForm();
            frm.numericUpDown1.Value = curChunk + 1;
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (LoadedChunkVer == (int)RSDKver.RSDK4)
                {
                    Chunksv4.BlockList[frm.DestinationChunk] = Chunksv4.Clone(frm.SourceChunk);
                }
                if (LoadedChunkVer == (int)RSDKver.RSDK3)
                {
                    Chunksv3.BlockList[frm.DestinationChunk] = Chunksv3.Clone(frm.SourceChunk);
                }
                if (LoadedChunkVer == (int)RSDKver.RSDK2)
                {
                    Chunksv2.BlockList[frm.DestinationChunk] = Chunksv2.Clone(frm.SourceChunk);
                }
                if (LoadedChunkVer == (int)RSDKver.RSDK1)
                {
                    Chunksv1.BlockList[frm.DestinationChunk] = Chunksv1.Clone(frm.SourceChunk);
                }
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
    }
}
