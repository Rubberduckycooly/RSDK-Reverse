using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.RSonicCollisionEditor
{
    public partial class Mainform : DockContent
    {

        List<Bitmap> ColImges = new List<Bitmap>(); //List of images, saves memory
        List<Bitmap> ColActivatedImges = new List<Bitmap>(); //List of images, saves memory

        List<Bitmap> CollisionListImgA = new List<Bitmap>();
        List<Bitmap> CollisionListImgB = new List<Bitmap>();

        public int curColisionMask; //What Collision Mask are we editing?

        public string filepath; //Where is the file located?

        bool showPathB = false; //should we show Path A or Path B?
        int SelectedDir = 0;

        bool MirrorPaths = false; //Do we want to activate "Mirror Paths" Mode?

        public RSDKvRS.Tileconfig tcf; //The Tileconfig Data

        public RetroED.MainForm Parent;

        List<Bitmap> Tiles = new List<Bitmap>(); //List of all the 16x16 Stage Tiles
        Bitmap Tileset;

        int gotoVal; //What collision mask we goto when "GO!" is pressed

        public Mainform()
        {
            InitializeComponent();

            ToolTip ToolTip = new ToolTip();
            //SelectDirBox.SelectedIndex = 0;

            ColImges.Add(new Bitmap(Properties.Resources._1));
            ColImges.Add(new Bitmap(Properties.Resources._2));
            ColImges.Add(new Bitmap(Properties.Resources._3));
            ColImges.Add(new Bitmap(Properties.Resources._4));
            ColImges.Add(new Bitmap(Properties.Resources._5));
            ColImges.Add(new Bitmap(Properties.Resources._6));
            ColImges.Add(new Bitmap(Properties.Resources._7));
            ColImges.Add(new Bitmap(Properties.Resources._8));
            ColImges.Add(new Bitmap(Properties.Resources._9));
            ColImges.Add(new Bitmap(Properties.Resources._10));
            ColImges.Add(new Bitmap(Properties.Resources._11));
            ColImges.Add(new Bitmap(Properties.Resources._12));
            ColImges.Add(new Bitmap(Properties.Resources._13));
            ColImges.Add(new Bitmap(Properties.Resources._14));
            ColImges.Add(new Bitmap(Properties.Resources._15));
            ColImges.Add(new Bitmap(Properties.Resources._16));
            ColImges.Add(new Bitmap(Properties.Resources._0));

            ColActivatedImges.Add(Properties.Resources.Red);
            ColActivatedImges.Add(Properties.Resources.Green);

            Viewer1.Image = ColImges[16];
            Viewer2.Image = ColImges[16];
            Viewer3.Image = ColImges[16];
            Viewer4.Image = ColImges[16];
            Viewer5.Image = ColImges[16];
            Viewer6.Image = ColImges[16];
            Viewer7.Image = ColImges[16];
            Viewer8.Image = ColImges[16];
            Viewer9.Image = ColImges[16];
            Viewer10.Image = ColImges[16];
            Viewer11.Image = ColImges[16];
            Viewer12.Image = ColImges[16];
            Viewer13.Image = ColImges[16];
            Viewer14.Image = ColImges[16];
            Viewer15.Image = ColImges[16];
            Viewer16.Image = ColImges[16];


            RGBox0.Image = ColActivatedImges[0];
            RGBox1.Image = ColActivatedImges[0];
            RGBox2.Image = ColActivatedImges[0];
            RGBox3.Image = ColActivatedImges[0];
            RGBox4.Image = ColActivatedImges[0];
            RGBox5.Image = ColActivatedImges[0];
            RGBox6.Image = ColActivatedImges[0];
            RGBox7.Image = ColActivatedImges[0];
            RGBox8.Image = ColActivatedImges[0];
            RGBox9.Image = ColActivatedImges[0];
            RGBoxA.Image = ColActivatedImges[0];
            RGBoxB.Image = ColActivatedImges[0];
            RGBoxC.Image = ColActivatedImges[0];
            RGBoxD.Image = ColActivatedImges[0];
            RGBoxE.Image = ColActivatedImges[0];
            RGBoxF.Image = ColActivatedImges[0];

            ToolTip.SetToolTip(UnknownNUD, "Controls the slope angle of the tile");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open";
            dlg.DefaultExt = ".tcf";
            dlg.Filter = "Retro-Sonic (2007 PC Demo) Tileconfig Files (Zone.tcf)|Zone.tcf|Retro-Sonic (2006 Dreamcast Demo) Tileconfig Files (Zone.tcf)|Zone.tcf";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                curColisionMask = 0; //Set the current collision mask to zero (avoids rare errors)
                filepath = dlg.FileName;
                bool DC = false;
                if ((dlg.FilterIndex - 1) == 1) { DC = true; }
                if (!DC)
                {
                    tcf = new RSDKvRS.Tileconfig(dlg.FileName, DC);
                    string t = filepath.Replace("Zone.tcf", "Zone.gfx"); //get the path to the stage's tileset
                    RSDKvRS.gfx gfx = new RSDKvRS.gfx(t, DC);
                    Tileset = gfx.gfxImage;
                }
                else if (DC)
                {
                    tcf = new RSDKvRS.Tileconfig(dlg.FileName, DC);
                    string t = filepath.Replace("ZONE.TCF", "ZONEL1.GFX"); //get the path to the stage's tileset
                    RSDKvRS.gfx gfx = new RSDKvRS.gfx(t, DC);
                    Tileset = gfx.gfxImage;
                }
                LoadTileSet(Tileset); //load each 16x16 tile into the list
                
                CollisionList.Images.Clear();

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgA.Add(tcf.Collision[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0),SelectedDir, false));
                    CollisionList.Images.Add(CollisionListImgA[i]);
                }

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgB.Add(tcf.Collision[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0),SelectedDir, true));
                    CollisionList.Images.Add(CollisionListImgB[i]);
                }
                CollisionList.SelectedIndex = curColisionMask - 1;
                CollisionList.Refresh();

                Parent.rp.state = "RetroED - " + this.Text;
                Parent.rp.details = "Editing: " + Path.GetFileName(dlg.FileName);
                SharpPresence.Discord.RunCallbacks();
                SharpPresence.Discord.UpdatePresence(Parent.rp);

                RefreshUI(); //update the UI
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filepath != null) //Did we open a file?
            {
                tcf.Write(filepath,false);
            }
            else //if not then use "Save As..."
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save As...";
            dlg.DefaultExt = ".tcf";
            dlg.Filter = "Retro-Sonic (2007 PC Demo) Tileconfig Files (Zone.tcf)|Zone.tcf|Retro-Sonic (2006 Dreamcast Demo) Tileconfig Files (Zone.tcf)|Zone.tcf";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                bool DC = dlg.FilterIndex-1 == 1;
                tcf.Write(dlg.FileName,DC); //Write the data to a file
            }
        }

        private void openSingleCollisionMaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import CollisionMask...";
            dlg.DefaultExt = ".rcm";
            dlg.Filter = "Singular RSDK CollisionMask (*.rcm)|*.rcm";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                
            }
        }

        private void exportCollisionMaskAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export As...";
            dlg.DefaultExt = ".rcm";
            dlg.Filter = "Singular RSDK CollisionMask (*.rcm)|*.rcm";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                
            }
        }

        private void splitFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select Folder to Export to...";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                
            }

        }

        public void LoadTileSet(Bitmap TileSet)
        {
            Tiles.Clear(); // Clear the previous images, since we load the entire file!
            int tsize = TileSet.Height; //Height of the image in pixels
            for (int i = 0; i < (tsize / 16); i++) //We divide by 16 to get the "height" in blocks
            {
                Rectangle CropArea = new Rectangle(0, (i * 16), 16, 16); //we then get tile at Y: i * 16, 
                //we have to multiply i by 16 to get the "true Tile value" (1* 16 = 16, 2 * 16 = 32, etc.)

                Bitmap CroppedImage = CropImage(TileSet, CropArea); // crop that image
                Tiles.Add(CroppedImage); // add it to the tile list
            }
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

        void RefreshUI()
        {
            if (tcf != null)
            {
                CurMaskLabel.Text = "Collision Mask "+ (curColisionMask+1) +" of "+ 1024; //what collision mask are we on?
                TilePicBox.BackgroundImage = Tiles[curColisionMask]; //update the tile preview
                Bitmap Overlaypic = new Bitmap(16,16);
                UnknownNUD.Value = tcf.Collision[curColisionMask].PCunknown;
                if (!showPathB) //if we are showing Path A then refresh the values accordingly
                {
                    CollisionPicBox.BackgroundImage = tcf.Collision[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255),SelectedDir, showPathB);
                    Overlaypic = tcf.Collision[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0),SelectedDir, showPathB, Tiles[curColisionMask]);
                    if (SelectedDir == 0)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision1Up[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision1UpSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision1UpSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision1UpSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision1UpSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision1UpSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision1UpSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision1UpSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision1UpSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision1UpSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision1UpSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision1UpSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision1UpSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision1UpSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision1UpSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision1UpSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision1UpSolid[15];

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1UpSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (SelectedDir == 1)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision1Right[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision1RightSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision1RightSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision1RightSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision1RightSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision1RightSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision1RightSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision1RightSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision1RightSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision1RightSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision1RightSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision1RightSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision1RightSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision1RightSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision1RightSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision1RightSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision1RightSolid[15];

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1RightSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (SelectedDir == 2)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision1Left[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision1LeftSolid[15];

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1LeftSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (SelectedDir == 3)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision1Down[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision1DownSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision1DownSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision1DownSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision1DownSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision1DownSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision1DownSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision1DownSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision1DownSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision1DownSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision1DownSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision1DownSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision1DownSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision1DownSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision1DownSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision1DownSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision1DownSolid[15];

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision1DownSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (cb00.Value > 0) { RGBox0.Image = ColActivatedImges[1]; }
                    else { RGBox0.Image = ColActivatedImges[0]; }

                    if (cb01.Value > 0) { RGBox1.Image = ColActivatedImges[1]; }
                    else { RGBox1.Image = ColActivatedImges[0]; }

                    if (cb02.Value > 0) { RGBox2.Image = ColActivatedImges[1]; }
                    else { RGBox2.Image = ColActivatedImges[0]; }

                    if (cb03.Value > 0) { RGBox3.Image = ColActivatedImges[1]; }
                    else { RGBox3.Image = ColActivatedImges[0]; }

                    if (cb04.Value > 0) { RGBox4.Image = ColActivatedImges[1]; }
                    else { RGBox4.Image = ColActivatedImges[0]; }

                    if (cb05.Value > 0) { RGBox5.Image = ColActivatedImges[1]; }
                    else { RGBox5.Image = ColActivatedImges[0]; }

                    if (cb06.Value > 0) { RGBox6.Image = ColActivatedImges[1]; }
                    else { RGBox6.Image = ColActivatedImges[0]; }

                    if (cb07.Value > 0) { RGBox7.Image = ColActivatedImges[1]; }
                    else { RGBox7.Image = ColActivatedImges[0]; }

                    if (cb08.Value > 0) { RGBox8.Image = ColActivatedImges[1]; }
                    else { RGBox8.Image = ColActivatedImges[0]; }

                    if (cb09.Value > 0) { RGBox9.Image = ColActivatedImges[1]; }
                    else { RGBox9.Image = ColActivatedImges[0]; }

                    if (cb10.Value > 0) { RGBoxA.Image = ColActivatedImges[1]; }
                    else { RGBoxA.Image = ColActivatedImges[0]; }

                    if (cb11.Value > 0) { RGBoxB.Image = ColActivatedImges[1]; }
                    else { RGBoxB.Image = ColActivatedImges[0]; }

                    if (cb12.Value > 0) { RGBoxC.Image = ColActivatedImges[1]; }
                    else { RGBoxC.Image = ColActivatedImges[0]; }

                    if (cb13.Value > 0) { RGBoxD.Image = ColActivatedImges[1]; }
                    else { RGBoxD.Image = ColActivatedImges[0]; }

                    if (cb14.Value > 0) { RGBoxE.Image = ColActivatedImges[1]; }
                    else { RGBoxE.Image = ColActivatedImges[0]; }

                    if (cb15.Value > 0) { RGBoxF.Image = ColActivatedImges[1]; }
                    else { RGBoxF.Image = ColActivatedImges[0]; }

                }

                if (showPathB) //if we are showing Path B then refresh the values accordingly
                {
                    CollisionPicBox.BackgroundImage = tcf.Collision[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(0, 255, 0),SelectedDir, showPathB);
                    Overlaypic = tcf.Collision[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0), SelectedDir, showPathB, Tiles[curColisionMask]);

                    if (SelectedDir == 0)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision2Up[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision2UpSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision2UpSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision2UpSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision2UpSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision2UpSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision2UpSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision2UpSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision2UpSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision2UpSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision2UpSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision2UpSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision2UpSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision2UpSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision2UpSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision2UpSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision2UpSolid[15];

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2UpSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (SelectedDir == 1)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision2Right[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision2RightSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision2RightSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision2RightSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision2RightSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision2RightSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision2RightSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision2RightSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision2RightSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision2RightSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision2RightSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision2RightSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision2RightSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision2RightSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision2RightSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision2RightSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision2RightSolid[15];

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2RightSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (SelectedDir == 2)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision2Left[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision2LeftSolid[15];

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2LeftSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (SelectedDir == 3)
                    {
                        lb00.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[0];
                        lb01.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[1];
                        lb02.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[2];
                        lb03.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[3];
                        lb04.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[4];
                        lb05.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[5];
                        lb06.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[6];
                        lb07.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[7];
                        lb08.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[8];
                        lb09.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[9];
                        lb10.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[10];
                        lb11.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[11];
                        lb12.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[12];
                        lb13.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[13];
                        lb14.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[14];
                        lb15.SelectedIndex = tcf.Collision[curColisionMask].Collision2Down[15];

                        cb00.Value = tcf.Collision[curColisionMask].Collision2DownSolid[0];
                        cb01.Value = tcf.Collision[curColisionMask].Collision2DownSolid[1];
                        cb02.Value = tcf.Collision[curColisionMask].Collision2DownSolid[2];
                        cb03.Value = tcf.Collision[curColisionMask].Collision2DownSolid[3];
                        cb04.Value = tcf.Collision[curColisionMask].Collision2DownSolid[4];
                        cb05.Value = tcf.Collision[curColisionMask].Collision2DownSolid[5];
                        cb06.Value = tcf.Collision[curColisionMask].Collision2DownSolid[6];
                        cb07.Value = tcf.Collision[curColisionMask].Collision2DownSolid[7];
                        cb08.Value = tcf.Collision[curColisionMask].Collision2DownSolid[8];
                        cb09.Value = tcf.Collision[curColisionMask].Collision2DownSolid[9];
                        cb10.Value = tcf.Collision[curColisionMask].Collision2DownSolid[10];
                        cb11.Value = tcf.Collision[curColisionMask].Collision2DownSolid[11];
                        cb12.Value = tcf.Collision[curColisionMask].Collision2DownSolid[12];
                        cb13.Value = tcf.Collision[curColisionMask].Collision2DownSolid[13];
                        cb14.Value = tcf.Collision[curColisionMask].Collision2DownSolid[14];
                        cb15.Value = tcf.Collision[curColisionMask].Collision2DownSolid[15];

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[0] > 0)
                        { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                        else { Viewer1.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[1] > 0)
                        { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                        else { Viewer2.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[2] > 0)
                        { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                        else { Viewer3.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[3] > 0)
                        { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                        else { Viewer4.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[4] > 0)
                        { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                        else { Viewer5.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[5] > 0)
                        { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                        else { Viewer6.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[6] > 0)
                        { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                        else { Viewer7.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[7] > 0)
                        { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                        else { Viewer8.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[8] > 0)
                        { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                        else { Viewer9.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[9] > 0)
                        { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                        else { Viewer10.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[10] > 0)
                        { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                        else { Viewer11.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[11] > 0)
                        { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                        else { Viewer12.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[12] > 0)
                        { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                        else { Viewer13.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[13] > 0)
                        { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                        else { Viewer14.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[14] > 0)
                        { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                        else { Viewer15.Image = ColImges[16]; }

                        if (tcf.Collision[curColisionMask].Collision2DownSolid[15] > 0)
                        { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                        else { Viewer16.Image = ColImges[16]; }
                    }

                    if (cb00.Value > 0) { RGBox0.Image = ColActivatedImges[1]; }
                    else { RGBox0.Image = ColActivatedImges[0]; }

                    if (cb01.Value > 0) { RGBox1.Image = ColActivatedImges[1]; }
                    else { RGBox1.Image = ColActivatedImges[0]; }

                    if (cb02.Value > 0) { RGBox2.Image = ColActivatedImges[1]; }
                    else { RGBox2.Image = ColActivatedImges[0]; }

                    if (cb03.Value > 0) { RGBox3.Image = ColActivatedImges[1]; }
                    else { RGBox3.Image = ColActivatedImges[0]; }

                    if (cb04.Value > 0) { RGBox4.Image = ColActivatedImges[1]; }
                    else { RGBox4.Image = ColActivatedImges[0]; }

                    if (cb05.Value > 0) { RGBox5.Image = ColActivatedImges[1]; }
                    else { RGBox5.Image = ColActivatedImges[0]; }

                    if (cb06.Value > 0) { RGBox6.Image = ColActivatedImges[1]; }
                    else { RGBox6.Image = ColActivatedImges[0]; }

                    if (cb07.Value > 0) { RGBox7.Image = ColActivatedImges[1]; }
                    else { RGBox7.Image = ColActivatedImges[0]; }

                    if (cb08.Value > 0) { RGBox8.Image = ColActivatedImges[1]; }
                    else { RGBox8.Image = ColActivatedImges[0]; }

                    if (cb09.Value > 0) { RGBox9.Image = ColActivatedImges[1]; }
                    else { RGBox9.Image = ColActivatedImges[0]; }

                    if (cb10.Value > 0) { RGBoxA.Image = ColActivatedImges[1]; }
                    else { RGBoxA.Image = ColActivatedImges[0]; }

                    if (cb11.Value > 0) { RGBoxB.Image = ColActivatedImges[1]; }
                    else { RGBoxB.Image = ColActivatedImges[0]; }

                    if (cb12.Value > 0) { RGBoxC.Image = ColActivatedImges[1]; }
                    else { RGBoxC.Image = ColActivatedImges[0]; }

                    if (cb13.Value > 0) { RGBoxD.Image = ColActivatedImges[1]; }
                    else { RGBoxD.Image = ColActivatedImges[0]; }

                    if (cb14.Value > 0) { RGBoxE.Image = ColActivatedImges[1]; }
                    else { RGBoxE.Image = ColActivatedImges[0]; }

                    if (cb15.Value > 0) { RGBoxF.Image = ColActivatedImges[1]; }
                    else { RGBoxF.Image = ColActivatedImges[0]; }
                }
                OverlayPicBox.BackgroundImage = Overlaypic;
                RefreshCollisionList();
            }
        }

        public void RefreshCollisionList()
        {
            CollisionList.Images.Clear();
            if (!showPathB)
            {
                for (int i = 0; i < 1024; i++)
                {
                    CollisionList.Images.Add(CollisionListImgA[i]);
                }
            }
            else if (showPathB)
            {
                for (int i = 0; i < 1024; i++)
                {
                    CollisionList.Images.Add(CollisionListImgB[i]);
                }
            }
            CollisionList.SelectedIndex = curColisionMask;
            CollisionList.Refresh();
        }

        private void showPathBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Do we want to show Path B's Collision Masks instead of Path A's ones?
            if (!showPathB)
            {
                showPathB = showPathBToolStripMenuItem.Checked = true;
                VPLabel.Text = "Currently Viewing: Path B";
                RefreshUI();
            }
            else if (showPathB)
            {
                showPathB = showPathBToolStripMenuItem.Checked = false;
                VPLabel.Text = "Currently Viewing: Path A";
                RefreshUI();
            }
        }

        private void openUncompressedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Uncompressed";
            dlg.DefaultExt = ".bin";
            dlg.Filter = "Uncompressed RSDKv5 Tileconfig Files (CollisionMasks.bin)|CollisionMasks.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                curColisionMask = 0; //Make sure we start at Collision Mask 0
                filepath = dlg.FileName; //Set the filepath
                //tcf = new RSDKvRS.tcf(dlg.FileName); //Tell it to read an uncompressed tileconfig
                string t = filepath.Replace("CollisionMasks.bin", "16x16tiles.gif"); //get the path to the stage's tileset
                LoadTileSet(new Bitmap(t)); //load each 16x16 tile into the list
                RefreshUI(); //update the UI
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                curColisionMask++;
                if (curColisionMask > 1023) //Don't go above 1024... 1023 + 1 (because it starts at zero) = 1024
                {
                    curColisionMask = 1023;
                }
                RefreshUI();
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                curColisionMask--;
                if (curColisionMask < 0) //Don't go below zero
                {
                    curColisionMask = 0; 
                }
                RefreshUI();
            }
        }

        private void GotoNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                gotoVal = (int)GotoNUD.Value - 1; //Set the goto value, we then take -1 to get the "true value"
            }
        }

        private void GotoButton_Click(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                curColisionMask = gotoVal; //Set the Collision Mask to the desired value
                RefreshUI(); //Show the user the new values
            }
        }
        
        private void SlopeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                tcf.Collision[curColisionMask].PCunknown = (byte)UnknownNUD.Value; //Set Slope angle for Path A
                RefreshUI();
            }
        }

        private void CollisionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CollisionList.SelectedIndex >= 0)
            {
                curColisionMask = CollisionList.SelectedIndex;
            }
            RefreshUI();
        }
        
        private void copyToOtherPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        
        private void mirrorPathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mirrorPathsToolStripMenuItem.Checked)
            {
                mirrorPathsToolStripMenuItem.Checked = MirrorPaths = false;
            }
            else if (!mirrorPathsToolStripMenuItem.Checked)
            {
                mirrorPathsToolStripMenuItem.Checked = MirrorPaths = true;
            }
        }
        
        #region Collision Mask Methods
        private void lb00_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[0] = (byte)lb00.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[0] = (byte)lb00.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[0] = (byte)lb00.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[0] = (byte)lb00.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[0] = (byte)lb00.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[0] = (byte)lb00.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[0] = (byte)lb00.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[0] = (byte)lb00.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb01_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[1] = (byte)lb01.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[1] = (byte)lb01.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[1] = (byte)lb01.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[1] = (byte)lb01.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[1] = (byte)lb01.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[1] = (byte)lb01.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[1] = (byte)lb01.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[1] = (byte)lb01.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb02_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[2] = (byte)lb02.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[2] = (byte)lb02.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[2] = (byte)lb02.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[2] = (byte)lb02.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[2] = (byte)lb02.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[2] = (byte)lb02.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[2] = (byte)lb02.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[2] = (byte)lb02.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb03_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[3] = (byte)lb03.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[3] = (byte)lb03.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[3] = (byte)lb03.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[3] = (byte)lb03.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[3] = (byte)lb03.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[3] = (byte)lb03.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[3] = (byte)lb03.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[3] = (byte)lb03.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb04_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[4] = (byte)lb04.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[4] = (byte)lb04.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[4] = (byte)lb04.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[4] = (byte)lb04.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[4] = (byte)lb04.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[4] = (byte)lb04.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[4] = (byte)lb04.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[4] = (byte)lb04.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[5] = (byte)lb05.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[5] = (byte)lb05.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[5] = (byte)lb05.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[5] = (byte)lb05.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[5] = (byte)lb05.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[5] = (byte)lb05.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[5] = (byte)lb05.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[5] = (byte)lb05.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb06_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[6] = (byte)lb06.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[6] = (byte)lb06.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[6] = (byte)lb06.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[6] = (byte)lb06.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[6] = (byte)lb06.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[6] = (byte)lb06.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[6] = (byte)lb06.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[6] = (byte)lb06.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb07_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[7] = (byte)lb07.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[7] = (byte)lb07.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[7] = (byte)lb07.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[7] = (byte)lb07.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[7] = (byte)lb07.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[7] = (byte)lb07.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[7] = (byte)lb07.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[7] = (byte)lb07.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb08_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[8] = (byte)lb08.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[8] = (byte)lb08.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[8] = (byte)lb08.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[8] = (byte)lb08.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[8] = (byte)lb08.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[8] = (byte)lb08.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[8] = (byte)lb08.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[8] = (byte)lb08.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb09_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[9] = (byte)lb09.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[9] = (byte)lb09.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[9] = (byte)lb09.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[9] = (byte)lb09.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[9] = (byte)lb09.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[9] = (byte)lb09.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[9] = (byte)lb09.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[9] = (byte)lb09.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[10] = (byte)lb10.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[10] = (byte)lb10.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[10] = (byte)lb10.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[10] = (byte)lb10.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[10] = (byte)lb10.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[10] = (byte)lb10.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[10] = (byte)lb10.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[10] = (byte)lb10.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[11] = (byte)lb11.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[11] = (byte)lb11.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[11] = (byte)lb11.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[11] = (byte)lb11.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[11] = (byte)lb11.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[11] = (byte)lb11.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[11] = (byte)lb11.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[11] = (byte)lb11.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[12] = (byte)lb12.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[12] = (byte)lb12.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[12] = (byte)lb12.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[12] = (byte)lb12.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[12] = (byte)lb12.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[12] = (byte)lb12.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[12] = (byte)lb12.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[12] = (byte)lb12.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[13] = (byte)lb13.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[13] = (byte)lb13.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[13] = (byte)lb13.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[13] = (byte)lb13.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[13] = (byte)lb13.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[13] = (byte)lb13.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[13] = (byte)lb13.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[13] = (byte)lb13.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[14] = (byte)lb14.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[14] = (byte)lb14.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[14] = (byte)lb14.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[14] = (byte)lb14.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[14] = (byte)lb14.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[14] = (byte)lb14.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[14] = (byte)lb14.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[14] = (byte)lb14.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void lb15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1Up[15] = (byte)lb15.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1Right[15] = (byte)lb15.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1Left[15] = (byte)lb15.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1Down[15] = (byte)lb15.SelectedIndex;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2Up[15] = (byte)lb15.SelectedIndex;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2Right[15] = (byte)lb15.SelectedIndex;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2Left[15] = (byte)lb15.SelectedIndex;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2Down[15] = (byte)lb15.SelectedIndex;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb00_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[0] = (byte)cb00.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[0] = (byte)cb00.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[0] = (byte)cb00.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[0] = (byte)cb00.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[0] = (byte)cb00.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[0] = (byte)cb00.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[0] = (byte)cb00.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[0] = (byte)cb00.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb01_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[1] = (byte)cb01.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[1] = (byte)cb01.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[1] = (byte)cb01.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[1] = (byte)cb01.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[1] = (byte)cb01.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[1] = (byte)cb01.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[1] = (byte)cb01.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[1] = (byte)cb01.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb02_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[2] = (byte)cb02.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[2] = (byte)cb02.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[2] = (byte)cb02.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[2] = (byte)cb02.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[2] = (byte)cb02.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[2] = (byte)cb02.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[2] = (byte)cb02.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[2] = (byte)cb02.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb03_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[3] = (byte)cb03.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[3] = (byte)cb03.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[3] = (byte)cb03.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[3] = (byte)cb03.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[3] = (byte)cb03.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[3] = (byte)cb03.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[3] = (byte)cb03.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[3] = (byte)cb03.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb04_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[4] = (byte)cb04.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[4] = (byte)cb04.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[4] = (byte)cb04.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[4] = (byte)cb04.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[4] = (byte)cb04.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[4] = (byte)cb04.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[4] = (byte)cb04.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[4] = (byte)cb04.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb05_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[5] = (byte)cb05.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[5] = (byte)cb05.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[5] = (byte)cb05.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[5] = (byte)cb05.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[5] = (byte)cb05.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[5] = (byte)cb05.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[5] = (byte)cb05.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[5] = (byte)cb05.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb06_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[6] = (byte)cb06.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[6] = (byte)cb06.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[6] = (byte)cb06.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[6] = (byte)cb06.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[6] = (byte)cb06.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[6] = (byte)cb06.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[6] = (byte)cb06.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[6] = (byte)cb06.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb07_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[7] = (byte)cb07.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[7] = (byte)cb07.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[7] = (byte)cb07.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[7] = (byte)cb07.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[7] = (byte)cb07.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[7] = (byte)cb07.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[7] = (byte)cb07.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[7] = (byte)cb07.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb08_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[8] = (byte)cb08.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[8] = (byte)cb08.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[8] = (byte)cb08.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[8] = (byte)cb08.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[8] = (byte)cb08.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[8] = (byte)cb08.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[8] = (byte)cb08.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[8] = (byte)cb08.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb09_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[9] = (byte)cb09.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[9] = (byte)cb09.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[9] = (byte)cb09.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[9] = (byte)cb09.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[9] = (byte)cb09.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[9] = (byte)cb09.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[9] = (byte)cb09.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[9] = (byte)cb09.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb10_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[10] = (byte)cb10.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[10] = (byte)cb10.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[10] = (byte)cb10.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[10] = (byte)cb10.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[10] = (byte)cb10.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[10] = (byte)cb10.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[10] = (byte)cb10.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[10] = (byte)cb10.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb11_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                switch (SelectedDir)
                {
                    case 0:
                        tcf.Collision[curColisionMask].Collision1UpSolid[11] = (byte)cb11.Value;
                        break;
                    case 1:
                        tcf.Collision[curColisionMask].Collision1RightSolid[11] = (byte)cb11.Value;
                        break;
                    case 2:
                        tcf.Collision[curColisionMask].Collision1LeftSolid[11] = (byte)cb11.Value;
                        break;
                    case 3:
                        tcf.Collision[curColisionMask].Collision1DownSolid[11] = (byte)cb11.Value;
                        break;
                }
            }
            if (showPathB)
            {
                switch (SelectedDir)
                {
                    case 0:
                        tcf.Collision[curColisionMask].Collision2UpSolid[11] = (byte)cb11.Value;
                        break;
                    case 1:
                        tcf.Collision[curColisionMask].Collision2RightSolid[11] = (byte)cb11.Value;
                        break;
                    case 2:
                        tcf.Collision[curColisionMask].Collision2LeftSolid[11] = (byte)cb11.Value;
                        break;
                    case 3:
                        tcf.Collision[curColisionMask].Collision2DownSolid[11] = (byte)cb11.Value;
                        break;
                }
            }
            RefreshUI();
        }

        private void cb12_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[12] = (byte)cb12.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[12] = (byte)cb12.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[12] = (byte)cb12.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[12] = (byte)cb12.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[12] = (byte)cb12.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[12] = (byte)cb12.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[12] = (byte)cb12.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[12] = (byte)cb12.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb13_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[13] = (byte)cb13.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[13] = (byte)cb13.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[13] = (byte)cb13.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[13] = (byte)cb13.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[13] = (byte)cb13.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[13] = (byte)cb13.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[13] = (byte)cb13.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[13] = (byte)cb13.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb14_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[14] = (byte)cb14.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[14] = (byte)cb14.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[14] = (byte)cb14.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[14] = (byte)cb14.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[14] = (byte)cb14.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[14] = (byte)cb14.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[14] = (byte)cb14.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[14] = (byte)cb14.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }

        private void cb15_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision1UpSolid[15] = (byte)cb15.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision1RightSolid[15] = (byte)cb15.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision1LeftSolid[15] = (byte)cb15.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision1DownSolid[15] = (byte)cb15.Value;
                            break;
                    }
                }
                if (showPathB)
                {
                    switch (SelectedDir)
                    {
                        case 0:
                            tcf.Collision[curColisionMask].Collision2UpSolid[15] = (byte)cb15.Value;
                            break;
                        case 1:
                            tcf.Collision[curColisionMask].Collision2RightSolid[15] = (byte)cb15.Value;
                            break;
                        case 2:
                            tcf.Collision[curColisionMask].Collision2LeftSolid[15] = (byte)cb15.Value;
                            break;
                        case 3:
                            tcf.Collision[curColisionMask].Collision2DownSolid[15] = (byte)cb15.Value;
                            break;
                    }
                }
                RefreshUI();
            }
        }
        #endregion

        private void SelectDirBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectDirBox.SelectedIndex >= 0)
            {
                SelectedDir = SelectDirBox.SelectedIndex;

                CollisionListImgA.Clear();
                CollisionListImgB.Clear();

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgA.Add(tcf.Collision[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0), SelectedDir, false));
                }

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgB.Add(tcf.Collision[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0), SelectedDir, true));
                }
                RefreshCollisionList();
                RefreshUI(); //update the UI
            }
        }

        private void GlobalActiveNUD_ValueChanged(object sender, EventArgs e)
        {
            cb00.Value = GlobalActiveNUD.Value;
            cb01.Value = GlobalActiveNUD.Value;
            cb02.Value = GlobalActiveNUD.Value;
            cb03.Value = GlobalActiveNUD.Value;
            cb04.Value = GlobalActiveNUD.Value;
            cb05.Value = GlobalActiveNUD.Value;
            cb06.Value = GlobalActiveNUD.Value;
            cb07.Value = GlobalActiveNUD.Value;
            cb08.Value = GlobalActiveNUD.Value;
            cb09.Value = GlobalActiveNUD.Value;
            cb10.Value = GlobalActiveNUD.Value;
            cb11.Value = GlobalActiveNUD.Value;
            cb12.Value = GlobalActiveNUD.Value;
            cb13.Value = GlobalActiveNUD.Value;
            cb14.Value = GlobalActiveNUD.Value;
            cb15.Value = GlobalActiveNUD.Value;
        }
    }
}
