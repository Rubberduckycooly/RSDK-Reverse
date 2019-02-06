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

namespace RetroED.Tools.CollisionEditor
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

        bool MirrorPaths = false; //Do we want to activate "Mirror Paths" Mode?

        public RSDKvB.Tileconfig tcf; //The Tileconfig Data

        public RetroED.MainForm Parent;

        List<Bitmap> Tiles = new List<Bitmap>(); //List of all the 16x16 Stage Tiles

        int gotoVal; //What collision mask we goto when "GO!" is pressed

        public Mainform()
        {
            InitializeComponent();

            ToolTip ToolTip = new ToolTip();

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

            ToolTip.SetToolTip(SlopeNUD, "Controls the slope angle of the tile");
            ToolTip.SetToolTip(PhysicsNUD, "Controls the physics of the player interacting with the tile");
            ToolTip.SetToolTip(MomentumNUD, "Controls the momentum the player gets from the tile");
            ToolTip.SetToolTip(UnknownNUD, "Controls the Unknown Value of the tile");
            ToolTip.SetToolTip(SpecialNUD, "Controls the the tile's 'Special' Properties, like whether it's a conveyor belt or not");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open";
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKv1/RSDKv2/RSDKvB Tileconfig Files (CollisionMasks.bin)|CollisionMasks.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                curColisionMask = 0; //Set the current collision mask to zero (avoids rare errors)
                filepath = dlg.FileName;
                tcf = new RSDKvB.Tileconfig(dlg.FileName);
                string t = filepath.Replace("CollisionMasks.bin", "16x16tiles.gif"); //get the path to the stage's tileset
                LoadTileSet(new Bitmap(t)); //load each 16x16 tile into the list

                CollisionList.Images.Clear();

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgA.Add(tcf.CollisionPath1[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
                    CollisionList.Images.Add(CollisionListImgA[i]);
                }

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgB.Add(tcf.CollisionPath2[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
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
                tcf.Write(filepath);
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
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKv1/RSDKv2/RSDKvB Tileconfig Files (CollisionMasks.bin)|CollisionMasks.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                tcf.Write(dlg.FileName); //Write the data to a file
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
                if (!showPathB) //if we are showing Path A then refresh the values accordingly
                {
                    CollisionPicBox.BackgroundImage = tcf.CollisionPath1[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0));
                    Overlaypic = tcf.CollisionPath1[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0), Tiles[curColisionMask]);
                    SlopeNUD.Value = tcf.CollisionPath1[curColisionMask].slopeAngle;
                    PhysicsNUD.Value = tcf.CollisionPath1[curColisionMask].physics;
                    MomentumNUD.Value = tcf.CollisionPath1[curColisionMask].momentum;
                    UnknownNUD.Value = tcf.CollisionPath1[curColisionMask].unknown;
                    //SpecialNUD.Value = tcf.CollisionPath1[curColisionMask].special;
                    ICBox.Checked = tcf.CollisionPath1[curColisionMask].isCeiling;

                    lb00.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[0];
                    lb01.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[1];
                    lb02.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[2];
                    lb03.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[3];
                    lb04.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[4];
                    lb05.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[5];
                    lb06.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[6];
                    lb07.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[7];
                    lb08.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[8];
                    lb09.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[9];
                    lb10.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[10];
                    lb11.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[11];
                    lb12.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[12];
                    lb13.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[13];
                    lb14.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[14];
                    lb15.SelectedIndex = tcf.CollisionPath1[curColisionMask].Collision[15];

                    cb00.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[0];
                    cb01.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[1];
                    cb02.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[2];
                    cb03.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[3];
                    cb04.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[4];
                    cb05.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[5];
                    cb06.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[6];
                    cb07.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[7];
                    cb08.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[8];
                    cb09.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[9];
                    cb10.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[10];
                    cb11.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[11];
                    cb12.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[12];
                    cb13.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[13];
                    cb14.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[14];
                    cb15.Checked = tcf.CollisionPath1[curColisionMask].HasCollision[15];

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[0])
                    { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                    else { Viewer1.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[1])
                    { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                    else { Viewer2.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[2])
                    { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                    else { Viewer3.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[3])
                    { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                    else { Viewer4.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[4])
                    { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                    else { Viewer5.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[5])
                    { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                    else { Viewer6.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[6])
                    { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                    else { Viewer7.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[7])
                    { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                    else { Viewer8.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[8])
                    { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                    else { Viewer9.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[9])
                    { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                    else { Viewer10.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[10])
                    { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                    else { Viewer11.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[11])
                    { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                    else { Viewer12.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[12])
                    { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                    else { Viewer13.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[13])
                    { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                    else { Viewer14.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[14])
                    { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                    else { Viewer15.Image = ColImges[16]; }

                    if (tcf.CollisionPath1[curColisionMask].HasCollision[15])
                    { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                    else { Viewer16.Image = ColImges[16]; }




                    if (cb00.Checked) { RGBox0.Image = ColActivatedImges[1]; }
                    else { RGBox0.Image = ColActivatedImges[0]; }

                    if (cb01.Checked) { RGBox1.Image = ColActivatedImges[1]; }
                    else { RGBox1.Image = ColActivatedImges[0]; }

                    if (cb02.Checked) { RGBox2.Image = ColActivatedImges[1]; }
                    else { RGBox2.Image = ColActivatedImges[0]; }

                    if (cb03.Checked) { RGBox3.Image = ColActivatedImges[1]; }
                    else { RGBox3.Image = ColActivatedImges[0]; }

                    if (cb04.Checked) { RGBox4.Image = ColActivatedImges[1]; }
                    else { RGBox4.Image = ColActivatedImges[0]; }

                    if (cb05.Checked) { RGBox5.Image = ColActivatedImges[1]; }
                    else { RGBox5.Image = ColActivatedImges[0]; }

                    if (cb06.Checked) { RGBox6.Image = ColActivatedImges[1]; }
                    else { RGBox6.Image = ColActivatedImges[0]; }

                    if (cb07.Checked) { RGBox7.Image = ColActivatedImges[1]; }
                    else { RGBox7.Image = ColActivatedImges[0]; }

                    if (cb08.Checked) { RGBox8.Image = ColActivatedImges[1]; }
                    else { RGBox8.Image = ColActivatedImges[0]; }

                    if (cb09.Checked) { RGBox9.Image = ColActivatedImges[1]; }
                    else { RGBox9.Image = ColActivatedImges[0]; }

                    if (cb10.Checked) { RGBoxA.Image = ColActivatedImges[1]; }
                    else { RGBoxA.Image = ColActivatedImges[0]; }

                    if (cb11.Checked) { RGBoxB.Image = ColActivatedImges[1]; }
                    else { RGBoxB.Image = ColActivatedImges[0]; }

                    if (cb12.Checked) { RGBoxC.Image = ColActivatedImges[1]; }
                    else { RGBoxC.Image = ColActivatedImges[0]; }

                    if (cb13.Checked) { RGBoxD.Image = ColActivatedImges[1]; }
                    else { RGBoxD.Image = ColActivatedImges[0]; }

                    if (cb14.Checked) { RGBoxE.Image = ColActivatedImges[1]; }
                    else { RGBoxE.Image = ColActivatedImges[0]; }

                    if (cb15.Checked) { RGBoxF.Image = ColActivatedImges[1]; }
                    else { RGBoxF.Image = ColActivatedImges[0]; }

                }

                if (showPathB) //if we are showing Path B then refresh the values accordingly
                {
                    CollisionPicBox.BackgroundImage = tcf.CollisionPath2[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(0, 255, 0));
                    Overlaypic = tcf.CollisionPath2[curColisionMask].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0), Tiles[curColisionMask]);
                    SlopeNUD.Value = tcf.CollisionPath2[curColisionMask].slopeAngle;
                    PhysicsNUD.Value = tcf.CollisionPath2[curColisionMask].physics;
                    MomentumNUD.Value = tcf.CollisionPath2[curColisionMask].momentum;
                    UnknownNUD.Value = tcf.CollisionPath2[curColisionMask].unknown;
                    //SpecialNUD.Value = tcf.CollisionPath2[curColisionMask].special;
                    ICBox.Checked = tcf.CollisionPath2[curColisionMask].isCeiling;

                    lb00.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[0];
                    lb01.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[1];
                    lb02.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[2];
                    lb03.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[3];
                    lb04.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[4];
                    lb05.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[5];
                    lb06.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[6];
                    lb07.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[7];
                    lb08.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[8];
                    lb09.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[9];
                    lb10.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[10];
                    lb11.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[11];
                    lb12.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[12];
                    lb13.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[13];
                    lb14.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[14];
                    lb15.SelectedIndex = tcf.CollisionPath2[curColisionMask].Collision[15];

                    cb00.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[0];
                    cb01.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[1];
                    cb02.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[2];
                    cb03.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[3];
                    cb04.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[4];
                    cb05.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[5];
                    cb06.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[6];
                    cb07.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[7];
                    cb08.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[8];
                    cb09.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[9];
                    cb10.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[10];
                    cb11.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[11];
                    cb12.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[12];
                    cb13.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[13];
                    cb14.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[14];
                    cb15.Checked = tcf.CollisionPath2[curColisionMask].HasCollision[15];


                    if (tcf.CollisionPath2[curColisionMask].HasCollision[0])
                    { Viewer1.Image = ColImges[lb00.SelectedIndex]; }
                    else { Viewer1.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[1])
                    { Viewer2.Image = ColImges[lb01.SelectedIndex]; }
                    else { Viewer2.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[2])
                    { Viewer3.Image = ColImges[lb02.SelectedIndex]; }
                    else { Viewer3.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[3])
                    { Viewer4.Image = ColImges[lb03.SelectedIndex]; }
                    else { Viewer4.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[4])
                    { Viewer5.Image = ColImges[lb04.SelectedIndex]; }
                    else { Viewer5.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[5])
                    { Viewer6.Image = ColImges[lb05.SelectedIndex]; }
                    else { Viewer6.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[6])
                    { Viewer7.Image = ColImges[lb06.SelectedIndex]; }
                    else { Viewer7.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[7])
                    { Viewer8.Image = ColImges[lb07.SelectedIndex]; }
                    else { Viewer8.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[8])
                    { Viewer9.Image = ColImges[lb08.SelectedIndex]; }
                    else { Viewer9.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[9])
                    { Viewer10.Image = ColImges[lb09.SelectedIndex]; }
                    else { Viewer10.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[10])
                    { Viewer11.Image = ColImges[lb10.SelectedIndex]; }
                    else { Viewer11.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[11])
                    { Viewer12.Image = ColImges[lb11.SelectedIndex]; }
                    else { Viewer12.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[12])
                    { Viewer13.Image = ColImges[lb12.SelectedIndex]; }
                    else { Viewer13.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[13])
                    { Viewer14.Image = ColImges[lb13.SelectedIndex]; }
                    else { Viewer14.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[14])
                    { Viewer15.Image = ColImges[lb14.SelectedIndex]; }
                    else { Viewer15.Image = ColImges[16]; }

                    if (tcf.CollisionPath2[curColisionMask].HasCollision[15])
                    { Viewer16.Image = ColImges[lb15.SelectedIndex]; }
                    else { Viewer16.Image = ColImges[16]; }




                    if (cb00.Checked) { RGBox0.Image = ColActivatedImges[1]; }
                    else { RGBox0.Image = ColActivatedImges[0]; }

                    if (cb01.Checked) { RGBox1.Image = ColActivatedImges[1]; }
                    else { RGBox1.Image = ColActivatedImges[0]; }

                    if (cb02.Checked) { RGBox2.Image = ColActivatedImges[1]; }
                    else { RGBox2.Image = ColActivatedImges[0]; }

                    if (cb03.Checked) { RGBox3.Image = ColActivatedImges[1]; }
                    else { RGBox3.Image = ColActivatedImges[0]; }

                    if (cb04.Checked) { RGBox4.Image = ColActivatedImges[1]; }
                    else { RGBox4.Image = ColActivatedImges[0]; }

                    if (cb05.Checked) { RGBox5.Image = ColActivatedImges[1]; }
                    else { RGBox5.Image = ColActivatedImges[0]; }

                    if (cb06.Checked) { RGBox6.Image = ColActivatedImges[1]; }
                    else { RGBox6.Image = ColActivatedImges[0]; }

                    if (cb07.Checked) { RGBox7.Image = ColActivatedImges[1]; }
                    else { RGBox7.Image = ColActivatedImges[0]; }

                    if (cb08.Checked) { RGBox8.Image = ColActivatedImges[1]; }
                    else { RGBox8.Image = ColActivatedImges[0]; }

                    if (cb09.Checked) { RGBox9.Image = ColActivatedImges[1]; }
                    else { RGBox9.Image = ColActivatedImges[0]; }

                    if (cb10.Checked) { RGBoxA.Image = ColActivatedImges[1]; }
                    else { RGBoxA.Image = ColActivatedImges[0]; }

                    if (cb11.Checked) { RGBoxB.Image = ColActivatedImges[1]; }
                    else { RGBoxB.Image = ColActivatedImges[0]; }

                    if (cb12.Checked) { RGBoxC.Image = ColActivatedImges[1]; }
                    else { RGBoxC.Image = ColActivatedImges[0]; }

                    if (cb13.Checked) { RGBoxD.Image = ColActivatedImges[1]; }
                    else { RGBoxD.Image = ColActivatedImges[0]; }

                    if (cb14.Checked) { RGBoxE.Image = ColActivatedImges[1]; }
                    else { RGBoxE.Image = ColActivatedImges[0]; }

                    if (cb15.Checked) { RGBoxF.Image = ColActivatedImges[1]; }
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
                tcf = new RSDKvB.Tileconfig(dlg.FileName); //Tell it to read an uncompressed tileconfig
                string t = filepath.Replace("CollisionMasks.bin", "16x16tiles.gif"); //get the path to the stage's tileset
                LoadTileSet(new Bitmap(t)); //load each 16x16 tile into the list
                RefreshUI(); //update the UI
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About_Form frm = new About_Form();
            frm.ShowDialog(this); //Show the About window
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
                curColisionMask = gotoVal; //Set the Collision Masl to the desired value
                RefreshUI(); //Show the user the new values
            }
        }
        
        private void SlopeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].slopeAngle = (byte)SlopeNUD.Value; //Set Slope angle for Path A
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].slopeAngle = (byte)SlopeNUD.Value; //Set Slope angle for Path B
                }
                RefreshUI();
            }
        }

        private void PhysicsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].physics = (byte)PhysicsNUD.Value; //Set the Physics for Path A
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].physics = (byte)PhysicsNUD.Value; //Set the Physics for Path B
                }
                RefreshUI();
            }
        }

        private void MomentumNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].momentum = (byte)MomentumNUD.Value; //Set the Momentum value for Path A
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].momentum = (byte)MomentumNUD.Value; //Set the Momentum value for Path B
                }
                RefreshUI();
            }
        }

        private void UnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].unknown = (byte)UnknownNUD.Value; //Set the unknown value for Path A
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].unknown = (byte)UnknownNUD.Value; //Set the unknown value for Path B
                }
                RefreshUI();
            }
        }

        private void SpecialNUD_ValueChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    //tcf.CollisionPath1[curColisionMask].special = (byte)SpecialNUD.Value; //Set the "Special" value for Path A
                }
                if (showPathB)
                {
                    //tcf.CollisionPath2[curColisionMask].special = (byte)SpecialNUD.Value; //Set the "Special" value for Path B
                }
                RefreshUI();
            }
        }

        private void ICBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].isCeiling = ICBox.Checked; //Set the "IsCeiling" value for Path A
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].isCeiling = ICBox.Checked; //Set the "IsCeiling" value for Path B
                }
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
            if (!showPathB)
            {
                RSDKvB.Tileconfig.CollisionMask tc = tcf.CollisionPath1[curColisionMask];
                tcf.CollisionPath2[curColisionMask] = tc;
                CollisionListImgB[curColisionMask] = CollisionListImgA[curColisionMask];
                RefreshUI();
            }
            else if (showPathB)
            {
                tcf.CollisionPath1[curColisionMask] = tcf.CollisionPath2[curColisionMask];
                CollisionListImgA[curColisionMask] = CollisionListImgB[curColisionMask];
                RefreshUI();
            }
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

        private void importRSDKv5TileConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open RSDKv5 Tileconfig...";
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKv5 Tileconfig Files (TileConfig.bin)|TileConfig.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                RSDKv5.TileConfig tcf5 = new RSDKv5.TileConfig(dlg.FileName);
                tcf = new RSDKvB.Tileconfig();
                for (int i = 0; i < 1024; i++)
                {
                    //Path A
                    tcf.CollisionPath1[i].Collision = tcf5.CollisionPath1[i].Collision;
                    tcf.CollisionPath1[i].HasCollision = tcf5.CollisionPath1[i].HasCollision;

                    tcf.CollisionPath1[i].slopeAngle = tcf5.CollisionPath1[i].slopeAngle;
                    tcf.CollisionPath1[i].physics = tcf5.CollisionPath1[i].physics;
                    tcf.CollisionPath1[i].momentum = tcf5.CollisionPath1[i].momentum;
                    tcf.CollisionPath1[i].isCeiling = tcf5.CollisionPath1[i].IsCeiling;
                    tcf.CollisionPath1[i].unknown = tcf5.CollisionPath1[i].unknown;

                    //Path B
                    tcf.CollisionPath2[i].Collision = tcf5.CollisionPath2[i].Collision;
                    tcf.CollisionPath2[i].HasCollision = tcf5.CollisionPath2[i].HasCollision;

                    tcf.CollisionPath2[i].slopeAngle = tcf5.CollisionPath2[i].slopeAngle;
                    tcf.CollisionPath2[i].physics = tcf5.CollisionPath2[i].physics;
                    tcf.CollisionPath2[i].momentum = tcf5.CollisionPath2[i].momentum;
                    tcf.CollisionPath2[i].isCeiling = tcf5.CollisionPath2[i].IsCeiling;
                    tcf.CollisionPath2[i].unknown = tcf5.CollisionPath2[i].unknown;
                }
                filepath = dlg.FileName;
                string t = filepath.Replace("TileConfig.bin", "16x16tiles.gif"); //get the path to the stage's tileset
                LoadTileSet(new Bitmap(t)); //load each 16x16 tile into the list

                CollisionList.Images.Clear();

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgA.Add(tcf.CollisionPath1[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
                    CollisionList.Images.Add(CollisionListImgA[i]);
                }

                for (int i = 0; i < 1024; i++)
                {
                    CollisionListImgB.Add(tcf.CollisionPath2[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
                    CollisionList.Images.Add(CollisionListImgB[i]);
                }
                CollisionList.SelectedIndex = curColisionMask - 1;
                CollisionList.Refresh();

                RefreshUI(); //update the UI

            }
        }

        private void exportToRSDKv5TileConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save RSDKv5 TileConfig As...";
            dlg.DefaultExt = ".bin";
            dlg.Filter = "RSDKv5 Tileconfig Files (TileConfig.bin)|TileConfig.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                RSDKv5.TileConfig tcf5 = new RSDKv5.TileConfig();

                for (int i = 0; i < 1024; i++)
                {
                    //Path A
                    tcf5.CollisionPath1[i].Collision = tcf.CollisionPath1[i].Collision;
                    tcf5.CollisionPath1[i].HasCollision = tcf.CollisionPath1[i].HasCollision;

                    tcf5.CollisionPath1[i].slopeAngle = tcf.CollisionPath1[i].slopeAngle;
                    tcf5.CollisionPath1[i].physics = tcf.CollisionPath1[i].physics;
                    tcf5.CollisionPath1[i].momentum = tcf.CollisionPath1[i].momentum;
                    tcf5.CollisionPath1[i].IsCeiling = tcf.CollisionPath1[i].isCeiling;
                    tcf5.CollisionPath1[i].unknown = tcf.CollisionPath1[i].unknown;
                    tcf5.CollisionPath1[i].special = 0;

                    //Path B
                    tcf5.CollisionPath2[i].Collision = tcf.CollisionPath2[i].Collision;
                    tcf5.CollisionPath2[i].HasCollision = tcf.CollisionPath2[i].HasCollision;

                    tcf5.CollisionPath2[i].slopeAngle = tcf.CollisionPath2[i].slopeAngle;
                    tcf5.CollisionPath2[i].physics = tcf.CollisionPath2[i].physics;
                    tcf5.CollisionPath2[i].momentum = tcf.CollisionPath2[i].momentum;
                    tcf5.CollisionPath2[i].IsCeiling = tcf.CollisionPath2[i].isCeiling;
                    tcf5.CollisionPath2[i].unknown = tcf.CollisionPath2[i].unknown;
                    tcf5.CollisionPath2[i].special = 0;
                }
                tcf5.Write(dlg.FileName);
            }
        }

        private void importFromGenesisGamesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Tools.RSDKCollisionEditor.Classic.CollisionArray ColAry;
            Tools.RSDKCollisionEditor.Classic.StageCollisions StgCol;
            Tools.RSDKCollisionEditor.Classic.Angles angles;

            OpenFileDialog Arraydlg = new OpenFileDialog();
            Arraydlg.Filter = "Classic Collision Arrays|*.bin";

            if (Arraydlg.ShowDialog(this) == DialogResult.OK)
            {
                ColAry = new RSDKCollisionEditor.Classic.CollisionArray(new BinaryReader(File.Open(Arraydlg.FileName, FileMode.Open)));
            }
            else
            {
                return;
            }

            OpenFileDialog angledlg = new OpenFileDialog();
            angledlg.Filter = "Classic Collision Angles|*.bin";

            if (angledlg.ShowDialog(this) == DialogResult.OK)
            {
                angles = new RSDKCollisionEditor.Classic.Angles(new BinaryReader(File.Open(angledlg.FileName, FileMode.Open)));
            }
            else
            {
                return;
            }

            OpenFileDialog Coldlg = new OpenFileDialog();
            Coldlg.Filter = "Classic Stage Collisions|*.bin";

            if (Coldlg.ShowDialog(this) == DialogResult.OK)
            {
                StgCol = new RSDKCollisionEditor.Classic.StageCollisions(new BinaryReader(File.Open(Coldlg.FileName, FileMode.Open)));
            }
            else
            {
                return;
            }

            OpenFileDialog imgdlg = new OpenFileDialog();
            imgdlg.Filter = "GIF Images|*.gif";

            if (imgdlg.ShowDialog(this) == DialogResult.OK)
            {
                Bitmap t = (Bitmap)Image.FromFile(imgdlg.FileName).Clone();
                LoadTileSet(t);
            }
            else
            {
                return;
            }

            tcf = new RSDKvB.Tileconfig();

            for (int i = 0; i < 768; i++)
            {
                tcf.CollisionPath1[i].Collision = ColAry.CollisionMasks[StgCol.PathA[i]].Collision;
                tcf.CollisionPath2[i].Collision = ColAry.CollisionMasks[StgCol.PathB[i]].Collision;

                if (i == 236 || i == 235 || i == 237) Console.WriteLine(StgCol.PathA[i] + " " + StgCol.PathB[i]);

                tcf.CollisionPath1[i].slopeAngle = angles.angles[StgCol.PathA[i]];
                tcf.CollisionPath2[i].slopeAngle = angles.angles[StgCol.PathB[i]];
                tcf.CollisionPath1[i].physics = angles.angles[StgCol.PathA[i]];
                tcf.CollisionPath2[i].physics = angles.angles[StgCol.PathB[i]];

                if (angles.angles[StgCol.PathA[i]] == 255) { tcf.CollisionPath1[i].physics = 0xC0; }
                tcf.CollisionPath1[i].momentum = 0x40;
                tcf.CollisionPath1[i].unknown = 0x80;

                if (angles.angles[StgCol.PathB[i]] == 255) { tcf.CollisionPath2[i].physics = 0xC0; }
                tcf.CollisionPath2[i].momentum = 0x40;
                tcf.CollisionPath2[i].unknown = 0x80;

                if (ColAry.CollisionMasks[StgCol.PathA[i]].CollisionConfig[0] == 0xf)
                {
                    tcf.CollisionPath1[i].isCeiling = true;
                }

                if (ColAry.CollisionMasks[StgCol.PathB[i]].CollisionConfig[0] == 0xf)
                {
                    tcf.CollisionPath2[i].isCeiling = true;
                }

                for (int t = 0; t < 16; t++)
                {
                    if (ColAry.CollisionMasks[StgCol.PathA[i]].Collision[t] > 0)
                    {
                        tcf.CollisionPath1[i].HasCollision[t] = true;
                    }
                    if (ColAry.CollisionMasks[StgCol.PathB[i]].Collision[t] > 0)
                    {
                        tcf.CollisionPath2[i].HasCollision[t] = true;
                    }
                }

                CollisionListImgA.Add(tcf.CollisionPath1[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
                CollisionList.Images.Add(CollisionListImgA[i]);
                CollisionListImgB.Add(tcf.CollisionPath2[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
            }

            for (int i = 768; i < 1024; i++)
            {
                tcf.CollisionPath1[i].Collision = ColAry.CollisionMasks[0].Collision;
                tcf.CollisionPath2[i].Collision = ColAry.CollisionMasks[0].Collision;
                CollisionListImgA.Add(tcf.CollisionPath1[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
                CollisionList.Images.Add(CollisionListImgA[i]);
                CollisionListImgB.Add(tcf.CollisionPath2[i].DrawCMask(Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)));
            }

            RefreshUI();

        }

        #region Collision Mask Methods
        private void lb00_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].Collision[0] = (byte)lb00.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[0] = (byte)lb00.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[1] = (byte)lb01.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[1] = (byte)lb01.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[2] = (byte)lb02.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[2] = (byte)lb02.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[3] = (byte)lb03.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[3] = (byte)lb03.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[4] = (byte)lb04.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[4] = (byte)lb04.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[5] = (byte)lb05.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[5] = (byte)lb05.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[6] = (byte)lb06.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[6] = (byte)lb06.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[7] = (byte)lb07.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[7] = (byte)lb07.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[8] = (byte)lb08.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[8] = (byte)lb08.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[9] = (byte)lb09.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[9] = (byte)lb09.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[10] = (byte)lb10.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[10] = (byte)lb10.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[11] = (byte)lb11.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[11] = (byte)lb11.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[12] = (byte)lb12.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[12] = (byte)lb12.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[13] = (byte)lb13.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[13] = (byte)lb13.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[14] = (byte)lb14.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[14] = (byte)lb14.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].Collision[15] = (byte)lb15.SelectedIndex;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].Collision[15] = (byte)lb15.SelectedIndex;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[0] = cb00.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[0] = cb00.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[1] = cb01.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[1] = cb01.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[2] = cb02.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[2] = cb02.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[3] = cb03.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[3] = cb03.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[4] = cb04.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[4] = cb04.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[5] = cb05.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[5] = cb05.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[6] = cb06.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[6] = cb06.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[7] = cb07.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[7] = cb07.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[8] = cb08.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[8] = cb08.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[9] = cb09.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[9] = cb09.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[10] = cb10.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[10] = cb10.Checked;
                }
                RefreshUI();
            }
        }

        private void cb11_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].HasCollision[11] = cb11.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[11] = cb11.Checked;
                }
                RefreshUI();
            }
        }

        private void cb12_CheckedChanged(object sender, EventArgs e)
        {
            if (tcf != null)
            {
                if (!showPathB)
                {
                    tcf.CollisionPath1[curColisionMask].HasCollision[12] = cb12.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[12] = cb12.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[13] = cb13.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[13] = cb13.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[14] = cb14.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[14] = cb14.Checked;
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
                    tcf.CollisionPath1[curColisionMask].HasCollision[15] = cb15.Checked;
                }
                if (showPathB)
                {
                    tcf.CollisionPath2[curColisionMask].HasCollision[15] = cb15.Checked;
                }
                RefreshUI();
            }
        }
        #endregion
    }
}
