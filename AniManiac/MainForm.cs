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
    public partial class MainForm : Form
    {

        #region Variables

        //the path to the ani file
        public string filePath = null;

        //the name of the file
        public string fileName = "New Animation";

        //Current Animation & Frames (-1 of shown value)
        int curAnim = 0;
        int curFrame = 0;

        //Animation Data
        RSDKvRS.Animation AnimationDatavRS = new RSDKvRS.Animation();
        RSDKv1.Animation AnimationDatav1 = new RSDKv1.Animation();
        RSDKv2.Animation AnimationDatav2 = new RSDKv2.Animation();
        RSDKvB.Animation AnimationDatavB = new RSDKvB.Animation();
        RSDKv5.Animation AnimationDatav5 = new RSDKv5.Animation();

        //What RSDK version it is
        public int RSDKver = 0;

        //Recent Paths
        List<string> RecentFilePaths = new List<string>();

        //Full SpriteSheets
        List<Bitmap> TexturesImg = new List<Bitmap>();

        //All The Frames For The Current Animation
        List<Bitmap> FrameImages = new List<Bitmap>();

        //Animation Values
        int animState = 0;
        int framecheck = 0;
        int PlayedX = 0;
        static int FPS = 60;
        int frameDelay = (1000 / FPS);
        System.Timers.Timer DelayTimer = new System.Timers.Timer(1);
        List<string> Textures = new List<string>();

        public bool FrameOverride = false;
        public bool AnimOverride = false;

        #endregion

        public MainForm()
        {
            curAnim = 0;
            curFrame = 0;
            InitializeComponent();
            AnimationDatavRS = new RSDKvRS.Animation();
            AnimationDatav1 = new RSDKv1.Animation();
            AnimationDatav2 = new RSDKv2.Animation();
            AnimationDatavB = new RSDKvB.Animation();
            AnimationDatav5 = new RSDKv5.Animation();
            CALabel.Text = "Animation " + (curAnim + 1) + " of " + AnimationData.animations.Count;
            CFLabel.Text = "Frame " + (curFrame + 1) + " of " + AnimationData.animations[curAnim].FrameAmount;
            AnimListBox.Items.Add(AnimationData.animations[curAnim].AnimName);
            DelayTimer.Elapsed += Animate;
            //DelayTimer.Interval = /*frameDelay*/;
            //DelayTimer.Tick += new EventHandler(Animate);
            this.Text = "New Animation";

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(this.NameBox, "Change animation name");
            toolTip.SetToolTip(this.FrameAmountNUD, "Change the amount of frames the animation has");
            toolTip.SetToolTip(this.LoopIndexNUD, "Change the frame that the animation loops from");
            toolTip.SetToolTip(this.SpeedMultiplyerNUD, "Change the Speed Multiplyer that animation plays by");

            toolTip.SetToolTip(this.TextureSelector, "Change the spritesheet that this frame will use");
            toolTip.SetToolTip(this.SrcXNUD, "Change the X position of the viewport");
            toolTip.SetToolTip(this.SrcYNUD, "Change the Y position of the viewport");
            toolTip.SetToolTip(this.SrcWNUD, "Change the width of the frame");
            toolTip.SetToolTip(this.SrcHNUD, "Change the height of the frame");
            toolTip.SetToolTip(this.HitBoxSelector, "Change the hitbox of the frame");
            toolTip.SetToolTip(this.PivotXNUD, "Change how far the Sprite is offset from the center horizontaly");
            toolTip.SetToolTip(this.PivotYNUD, "Change how far the Sprite is offset from the center vertically");
            toolTip.SetToolTip(this.DelayNUD, "Change the delay of this frame (1 = one in-game frame, 2 = 2 game-frames, etc.)");
            toolTip.SetToolTip(this.FlipBox, "Change the frame's Flip Value");

            toolTip.SetToolTip(this.AnimListBox, "A List of all the animations in the file");
            toolTip.SetToolTip(this.FrameList, "A List of all the frames in the animation");

            toolTip.SetToolTip(this.PlayButton, "Play Animation");
            toolTip.SetToolTip(this.NextFrameButton, "Go to next frame");
            toolTip.SetToolTip(this.PrevFrameButton, "Go to previous frame");
            toolTip.SetToolTip(this.NewFrameButton, "Add a new frame to the end of the list");
            toolTip.SetToolTip(this.DeleteFrameButton, "Delete the current frame");
            toolTip.SetToolTip(this.DuplicateFrameButton, "Duplicate the current frame");
            toolTip.SetToolTip(this.ImportFrameButton, "Import a frame from a file");
            toolTip.SetToolTip(this.ExportFrameButton, "Export a frame to a file");

            toolTip.SetToolTip(this.NewAnimButton, "Add a new animation to the File");
            toolTip.SetToolTip(this.DeleteAnimButton, "Delete the current animation");
            toolTip.SetToolTip(this.DuplicateAnimButton, "Duplicate the current animation");
            toolTip.SetToolTip(this.ImportAnimButton, "Import an animation from a file");
            toolTip.SetToolTip(this.ExportAnimButton, "Export an animation to a file");

            RefreshUI();
            RefreshFrame();
            RefreshAnimList();
        }

        public void New(int showDLG = 1)
        {
            curFrame = 0;
            curAnim = 0;
            AnimationData = new PSDK.FileFormats.SpriteMappings();
            this.Text = "New Animation";
            RefreshUI();
            RefreshFrame();
            RefreshAnimList();
        }

        public void Open(string path)
        {
            if (File.Exists(path))
            {
                filePath = path;
                curAnim = 0;
                curFrame = 0;
                this.Text = Path.GetFileName(path); Console.WriteLine(this.Text);
                TextureSelector.Items.Clear();
                HitBoxSelector.Items.Clear();
                TexturesImg.Clear();

                switch(RSDKver)
                {
                    case 0:
                        AnimationDatavRS = new RSDKvRS.Animation(new RSDKvRS.Reader(path));

                        for (int i = 0; i < AnimationDatavRS.SpriteSheets.Count; i++)
                        {
                            Bitmap bmp;
                            DirectoryInfo dirinfo = new DirectoryInfo(filePath);
                            DirectoryInfo di = new DirectoryInfo(filePath);
                            string dirPath = filePath.Replace(di.Parent + "\\" + Path.GetFileName(filePath), "");
                            bmp = new Bitmap(dirPath + "\\" + AnimationDatavRS.SpriteSheets[i]);
                            TexturesImg.Add(bmp);
                            TextureSelector.Items.Add(AnimationDatavRS.SpriteSheets[i]);
                        }
                        break;
                    case 1:
                        AnimationDatav1 = new RSDKv1.Animation(new RSDKv1.Reader(path));

                        for (int i = 0; i < AnimationDatav1.SpriteSheets.Count; i++)
                        {
                            Bitmap bmp;
                            DirectoryInfo dirinfo = new DirectoryInfo(filePath);
                            DirectoryInfo di = new DirectoryInfo(filePath);
                            string dirPath = filePath.Replace(di.Parent + "\\" + Path.GetFileName(filePath), "");
                            bmp = new Bitmap(dirPath + "\\" + AnimationDatavRS.SpriteSheets[i]);
                            TexturesImg.Add(bmp);
                            TextureSelector.Items.Add(AnimationDatav1.SpriteSheets[i]);
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }

            RefreshUI();
            RefreshFrame();
            RefreshAnimList();

        }

        void Save(string path)
        {
            this.Text = Path.GetFileName(path); Console.WriteLine(this.Text);

            switch(RSDKver)
            {
                case 0:
                    AnimationDatavRS.Write(new RSDKvRS.Writer(path));
                    break;
                case 1:
                    AnimationDatav1.Write(new RSDKv1.Writer(path));
                    break;
                case 2:
                    AnimationDatav2.Write(new RSDKv2.Writer(path));
                    break;
                case 3:
                    AnimationDatavB.Write(new RSDKvB.Writer(path));
                    break;
                case 4:
                    AnimationDatav5.Write(new RSDKv5.Writer(path));
                    break;
            }
        }

        void SaveFrame(string path, int anim, int frame)
        {
            AnimationData.animations[anim].Frames[frame].Write(new PSDK.Writer(path));
        }

        void LoadFrame(string path)
        {
            if (File.Exists(path))
            {
                if (FrameOverride)
                {
                    AnimationData.animations[curAnim].Frames.Add(new PSDK.FileFormats.SpriteMappings.sprFrame(new PSDK.Reader(path)));
                }
                else
                {
                    AnimationData.animations[curAnim].Frames[curFrame] = new PSDK.FileFormats.SpriteMappings.sprFrame(new PSDK.Reader(path));
                }

                RefreshUI();
                RefreshFrame();
            }
        }

        void SaveAnimation(string path, int anim)
        {
            AnimationData.animations[anim].Write(new PSDK.Writer(path));
        }

        void LoadAnimation(string path)
        {
            if (File.Exists(path))
            {
                if (!AnimOverride)
                {
                    AnimationData.animations.Add(new PSDK.FileFormats.SpriteMappings.sprAnimation(new PSDK.Reader(path)));
                }
                else
                {
                    AnimationData.animations[curAnim] = new PSDK.FileFormats.SpriteMappings.sprAnimation(new PSDK.Reader(path));
                    RefreshUI();
                    RefreshFrame();
                    RefreshAnimList();
                }
            }
        }

        public void AddRecentFile(string path)
        {
            RecentFilePaths.Add(path);
            recentFilesToolStripMenuItem.DropDownItems.Add(path);
            recentFilesToolStripMenuItem.Enabled = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Unload the current animation file and create a new one?", "Eclipse Engine Animation Editor", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
            {
                New();
                PegasusEngine.MainForm.Instance.TabControl.TabPages[PegasusEngine.MainForm.Instance.TabControl.SelectedIndex].Text = "New Sprite Mappings";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrameAmountNUD.Value > 0)
            {
                switch (MessageBox.Show(this, "Do you want to save the current file?", "Eclipse Engine Animation Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        saveToolStripMenuItem_Click(this, EventArgs.Empty);
                        break;
                }
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "spr";
            dlg.Filter = "Eclipse Engine Mappings Files|*.spr";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filePath = dlg.FileName;
                fileName = Path.GetFileName(dlg.FileName);
                AddRecentFile(filePath);
                int fi = dlg.FilterIndex - 1;
                PegasusEngine.MainForm.Instance.TabControl.TabPages[PegasusEngine.MainForm.Instance.TabControl.SelectedIndex].Text = fileName;

                switch (fi)
                {
                    case 0:
                        Open(filePath);
                        break;
                    default:
                        break;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath == null) { saveAsToolStripMenuItem_Click(sender, e); }
            else if (File.Exists(filePath))
            {
                Save(filePath);
            }
            else if (!File.Exists(filePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "spr";
            dlg.Filter = "Eclipse Engine Mappings Files|*.spr";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filePath = dlg.FileName;
                fileName = Path.GetFileName(dlg.FileName);
                if (filePath != null) { this.Text = fileName + " - " + "Eclipse Engine Animation Editor"; }
                else { this.Text = "New Animation - " + "Eclipse Engine Animation Editor"; }
                PegasusEngine.MainForm.Instance.TabControl.TabPages[PegasusEngine.MainForm.Instance.TabControl.SelectedIndex].Text = fileName;
                AddRecentFile(filePath);
                Save(filePath);
            }
        }

        private void recentFilesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Open(RecentFilePaths[recentFilesToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem)]);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textureManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PegasusEngine.Tools.AnimationEditor.TexManagerForm TM = new PegasusEngine.Tools.AnimationEditor.TexManagerForm(AnimationData.textures, Path.GetDirectoryName(filePath));
            TM.ShowDialog();
            Textures = TM.Textures;
            AnimationData.textures = TM.Textures;

            TexturesImg.Clear();
            TextureSelector.Items.Clear();

            for (int i = 0; i < Textures.Count; i++)
            {
                string result = TM.Textures[i];
                if (TM.Textures[i].Contains("/"))
                {
                    result = PSDK.Utils.after(TM.Textures[i], "/");
                }
                Console.WriteLine(Path.GetExtension(result));
                if (Path.GetExtension(result) == ".gif")
                {
                    try
                    {
                        string basepth = filePath.Replace(fileName, "");
                        Bitmap bmp = new Bitmap(basepth + result);
                        TexturesImg.Add(bmp);
                        TextureSelector.Items.Add(Textures[i]);
                    }
                    catch (Exception ex)
                    {
                        Bitmap bmp = new Bitmap(TM.FullTexturePaths[i]);
                        TexturesImg.Add(bmp);
                        TextureSelector.Items.Add(Textures[i]);
                    }
                }

                if (Path.GetExtension(result) == ".gfx")
                {
                    string basepth = filePath.Replace(fileName, "");
                    PSDK.FileFormats.gfx gfx = new PSDK.FileFormats.gfx(basepth + result);
                    Bitmap bmp = new Bitmap(gfx.width, gfx.height);

                    for (int h = 0; h < gfx.height; h++)
                    {
                        for (int w = 0; w < gfx.width; w++)
                        {
                            Color c = Color.FromArgb(gfx.palette.Pal[gfx.IndexedImageLayout[h][w]].R, gfx.palette.Pal[gfx.IndexedImageLayout[h][w]].G, gfx.palette.Pal[gfx.IndexedImageLayout[h][w]].B);
                            bmp.SetPixel(w, h, c);
                        }
                    }

                    TexturesImg.Add(bmp);
                    TextureSelector.Items.Add(Textures[i]);
                }
            }
        }

        private void collisionManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PegasusEngine.Tools.AnimationEditor.HitboxManagerForm HM = new PegasusEngine.Tools.AnimationEditor.HitboxManagerForm();
            HM.Setup(AnimationData);
            HM.ShowDialog();

            AnimationData.HitBoxes = HM.HitBoxList;

            HitBoxSelector.Items.Clear();

            for (int i = 0; i < AnimationData.HitBoxes.Count; i++)
            {
                HitBoxSelector.Items.Add(HM.HitboxListBox.Items[i].ToString());
            }

        }

        private void refreshFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshFrame();
            RefreshUI();
            RefreshAnimList();
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            AnimationData.animations[curAnim].AnimName = NameBox.Text;
            if (AnimListBox.Items[curAnim].ToString() != NameBox.Text)
            { RefreshAnimList(); }
        }

        private void FrameAmountNUD_ValueChanged(object sender, EventArgs e)
        {
        }

        private void SpeedMultiplyerNUD_ValueChanged(object sender, EventArgs e)
        {
            AnimationData.animations[curAnim].SpeedMultiplyer = (byte)SpeedMultiplyerNUD.Value;
        }

        private void LoopIndexNUD_ValueChanged(object sender, EventArgs e)
        {
            AnimationData.animations[curAnim].LoopIndex = (byte)LoopIndexNUD.Value;
        }


        private void RotFlagBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].RotFlags = (byte)RotFlagBox.SelectedIndex;
            }
        }

        private void TextureSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].imgIndex = (byte)TextureSelector.SelectedIndex;
                RefreshFrame();
            }
        }

        private void SrcXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].SrcX = (short)SrcXNUD.Value;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshUI();
                    RefreshFrame();
                }
            }

        }

        private void SrcYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].SrcY = (short)SrcYNUD.Value;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshUI();
                    RefreshFrame();
                }
            }

        }

        private void SrcWNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].SrcW = (short)SrcWNUD.Value;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshUI();
                    RefreshFrame();
                }
            }

        }

        private void SrcHNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].SrcH = (short)SrcHNUD.Value;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshUI();
                    RefreshFrame();
                }
            }

        }

        private void HitBoxSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex = (byte)HitBoxSelector.SelectedIndex;
            }

        }

        private void PivotXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].PivotX = (short)PivotXNUD.Value;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshFrame();
                }
            }

        }

        private void PivotYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].PivotY = (short)PivotYNUD.Value;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshFrame();
                }
            }

        }

        private void DelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].Delay = (short)DelayNUD.Value;
            }

        }

        private void FlipBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].Frames[curFrame].Flip = (byte)FlipBox.SelectedIndex;
                if (CurFrameImgBox.Image != null)
                {
                    RefreshFrame();
                    RefreshFrameListIndex(curFrame);
                }
            }

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            AnimControl();
        }

        private void PrevFrameButton_Click(object sender, EventArgs e)
        {
            if (curFrame - 1 >= 0)
            {
                curFrame = (curFrame - 1);
                FrameList.SelectedIndex = curFrame;
                RefreshUI();
            }

        }

        private void NextFrameButton_Click(object sender, EventArgs e)
        {
            if (curFrame + 1 < AnimationData.animations[curAnim].FrameAmount)
            {
                curFrame = (curFrame + 1);
                FrameList.SelectedIndex = curFrame;
                RefreshUI();
            }
        }

        private void NewFrameButton_Click(object sender, EventArgs e)
        {
            AnimationData.animations[curAnim].NewFrame();
            curFrame = AnimationData.animations[curAnim].FrameAmount - 1;
            try
            {
                FrameList.SelectedIndex = curFrame;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                curFrame = 0;
            }
            RefreshUI();
            RefreshFrame();
        }

        private void DeleteFrameButton_Click(object sender, EventArgs e)
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0)
            {
                AnimationData.animations[curAnim].DeleteFrame(curFrame);
                if (curFrame > 0)
                {
                    curFrame--;
                }
                FrameList.SelectedIndex = curFrame;
                RefreshUI();
                RefreshFrame();
            }
        }

        private void DuplicateFrameButton_Click(object sender, EventArgs e)
        {
            AnimationData.animations[curAnim].CloneFrame(curFrame);
            curFrame = AnimationData.animations[curAnim].FrameAmount - 1;
            try
            {
                FrameList.SelectedIndex = curFrame;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                curFrame = FrameList.SelectedIndex = 0;
            }
            RefreshUI();
            RefreshFrame();
        }

        private void ImportFrameButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "spr.frame";
            dlg.Filter = "Exported Eclipse Engine Mappings Frames|*.spr.frame";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                LoadFrame(dlg.FileName);
                RefreshUI();
                RefreshFrame();
            }
        }

        private void ExportFrameButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "spr.frame";
            dlg.Filter = "Exported Eclipse Engine Mappings Frames|*.spr.frame";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                SaveFrame(dlg.FileName, curAnim, curFrame);
            }
        }

        private void NewAnimButton_Click(object sender, EventArgs e)
        {
            NewAnimForm dlg = new NewAnimForm();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                AnimationData.NewAnimation();
                curAnim = AnimationData.animations.Count - 1;
                curFrame = 0;
                AnimationData.animations[curAnim].AnimName = dlg.AnimName;
                AnimListBox.Items.Add(AnimationData.animations[curAnim].AnimName);

                AnimationData.animations[curAnim].Frames.Clear();

                for (int i = 0; i < dlg.FrameCount; i++)
                {
                    PSDK.FileFormats.SpriteMappings.sprFrame frame = new PSDK.FileFormats.SpriteMappings.sprFrame();
                    frame.SrcW = dlg.FrameWidth;
                    frame.SrcH = dlg.FrameHeight;
                    AnimationData.animations[curAnim].Frames.Add(frame);
                }

                RefreshUI();
                RefreshFrame();
            }
        }

        private void DeleteAnimButton_Click(object sender, EventArgs e)
        {
            if (AnimationData.animations.Count > 1)
            {
                AnimationData.DeleteAnimation(curAnim);
                curAnim--;
                curFrame = 0;
                RefreshAnimList();
                RefreshUI();
                RefreshFrame();
            }
        }

        private void DuplicateAnimButton_Click(object sender, EventArgs e)
        {
            AnimationData.CloneAnimation(curAnim);
            curAnim = AnimationData.animations.Count - 1;
            curFrame = 0;
            RefreshAnimList();
            RefreshUI();
            RefreshFrame();
        }

        private void ImportAnimButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "spr.ani";
            dlg.Filter = "Exported Eclipse Engine Mappings|*.spr.ani";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                LoadAnimation(dlg.FileName);
                RefreshAnimList();
                RefreshUI();
            }
        }

        private void ExportAnimButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "spr.ani";
            dlg.Filter = "Exported Eclipse Engine Mappings|*.spr.ani";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                SaveAnimation(dlg.FileName, curAnim);
            }
        }

        private void AnimListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            curFrame = 0;
            if (AnimListBox.SelectedIndex >= 0)
            {
                curAnim = AnimListBox.SelectedIndex;
                FrameList.Refresh();
            }
            else
            {
                AnimListBox.SelectedIndex = 0;
                curAnim = 0;
                FrameList.Refresh();
            }
            FrameList.Images.Clear();

            if (AnimationData.textures.Count <= 0)
            {
                for (int i = 0; i < AnimationData.animations[curAnim].FrameAmount; i++)
                {
                    Bitmap img = new Bitmap(64, 64);
                    try
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            System.Drawing.SolidBrush b = new SolidBrush(Color.FromArgb(255, 0, 255));
                            g.FillRectangle(b, new RectangleF(0, 0, 64, 64));
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Error: " + Ex.Message);
                    }
                    FrameList.Images.Add(img);
                }
            }
            else
            {
                for (int i = 0; i < AnimationData.animations[curAnim].FrameAmount; i++)
                {
                    Bitmap img = new Bitmap(1, 1);
                    try
                    {
                        Rectangle Crop = new Rectangle(0, 0, 0, 0);
                        Crop.X = AnimationData.animations[curAnim].Frames[i].SrcX;
                        Crop.Y = AnimationData.animations[curAnim].Frames[i].SrcY;
                        Crop.Width = AnimationData.animations[curAnim].Frames[i].SrcW;
                        Crop.Height = AnimationData.animations[curAnim].Frames[i].SrcH;
                        img = CropImage(TexturesImg[AnimationData.animations[curAnim].Frames[i].imgIndex], Crop);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Error: " + Ex.Message);
                    }
                    FrameList.Images.Add(img);
                }
            }
            FrameList.SelectedIndex = curFrame;
            RefreshFrame();
            RefreshUI();

        }

        private void FrameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            curFrame = FrameList.SelectedIndex;
            RefreshFrame();
            RefreshFrameList();
            RefreshUI();
        }

        void RefreshUI()
        {
            CALabel.Text = "Animation " + (curAnim + 1) + " of " + AnimationData.animations.Count;
            CFLabel.Text = "Frame " + (curFrame + 1) + " of " + AnimationData.animations[curAnim].FrameAmount;
            NameBox.Text = AnimationData.animations[curAnim].AnimName;
            if (AnimationData.animations[curAnim].FrameAmount > 0) { FrameAmountNUD.Value = AnimationData.animations[curAnim].FrameAmount; }
            else { FrameAmountNUD.Value = 1; }
            LoopIndexNUD.Value = AnimationData.animations[curAnim].LoopIndex;
            SpeedMultiplyerNUD.Value = AnimationData.animations[curAnim].SpeedMultiplyer;
            RotFlagBox.SelectedIndex = AnimationData.animations[curAnim].RotFlags;
            if (AnimationData.animations[curAnim].Frames.Count <= 0)
            {
                if (TextureSelector.Items.Count > 0)
                { TextureSelector.SelectedIndex = 0; }
                SrcXNUD.Value = 0;
                SrcYNUD.Value = 0;
                SrcWNUD.Value = 0;
                SrcHNUD.Value = 0;
                if (HitBoxSelector.Items.Count > 0)
                { HitBoxSelector.SelectedIndex = 0; }
                PivotXNUD.Value = 0;
                PivotYNUD.Value = 0;
                DelayNUD.Value = 0;
                FlipBox.SelectedIndex = 0;
                CurFrameImgBox.Image = null;
            }
            else if (AnimationData.textures.Count > 0)
            {
                TextureSelector.SelectedIndex = AnimationData.animations[curAnim].Frames[curFrame].imgIndex;
                SrcXNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].SrcX;
                SrcYNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].SrcY;
                SrcWNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].SrcW;
                SrcHNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].SrcH;
                try
                {
                    HitBoxSelector.SelectedIndex = AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex;
                }
                catch
                {

                }
                PivotXNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].PivotX;
                PivotYNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].PivotY;
                DelayNUD.Value = AnimationData.animations[curAnim].Frames[curFrame].Delay;
                FlipBox.SelectedIndex = AnimationData.animations[curAnim].Frames[curFrame].Flip;
            }

            FrameList.Images.Clear();

            if (AnimationData.textures.Count <= 0)
            {
                for (int i = 0; i < AnimationData.animations[curAnim].FrameAmount; i++)
                {
                    Bitmap img = new Bitmap(64, 64);
                    try
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            System.Drawing.SolidBrush b = new SolidBrush(Color.FromArgb(255, 0, 255));
                            g.FillRectangle(b, new RectangleF(0, 0, 64, 64));
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Error: " + Ex.Message);
                    }
                    FrameList.Images.Add(img);
                }
            }
            else
            {
                for (int i = 0; i < AnimationData.animations[curAnim].FrameAmount; i++)
                {
                    Bitmap img = new Bitmap(1, 1);
                    try
                    {
                        Rectangle Crop = new Rectangle(0, 0, 0, 0);
                        Crop.X = AnimationData.animations[curAnim].Frames[i].SrcX;
                        Crop.Y = AnimationData.animations[curAnim].Frames[i].SrcY;
                        Crop.Width = AnimationData.animations[curAnim].Frames[i].SrcW;
                        Crop.Height = AnimationData.animations[curAnim].Frames[i].SrcH;
                        img = CropImage(TexturesImg[AnimationData.animations[curAnim].Frames[i].imgIndex], Crop);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Error: " + Ex.Message);
                    }
                    FrameList.Images.Add(img);
                }
            }
            FrameList.SelectedIndex = curFrame;
            FrameList.Refresh();
        }

        void RefreshFrame()
        {
            if (AnimationData.animations[curAnim].Frames.Count > 0 && TexturesImg.Count > 0)
            {
                CurFrameImgBox.Image = null;
                Rectangle Crop = new Rectangle(0, 0, 0, 0);

                Rectangle Hitbox = new Rectangle(
                    AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].topLeft,
                    AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].topRight,
                    AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].bottomLeft,
                    AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].bottomRight);

                Crop.X = AnimationData.animations[curAnim].Frames[curFrame].SrcX;
                Crop.Y = AnimationData.animations[curAnim].Frames[curFrame].SrcY;
                Crop.Width = AnimationData.animations[curAnim].Frames[curFrame].SrcW;
                Crop.Height = AnimationData.animations[curAnim].Frames[curFrame].SrcH;

                Bitmap img = CropImage(TexturesImg[AnimationData.animations[curAnim].Frames[curFrame].imgIndex], Crop);

                Rectangle newimgrect = new Rectangle(0,0, AnimationData.animations[curAnim].Frames[curFrame].SrcW, AnimationData.animations[curAnim].Frames[curFrame].SrcH);

                if (drawHitBoxesToolStripMenuItem.Checked)
                {
                    if (AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].bottomLeft > AnimationData.animations[curAnim].Frames[curFrame].SrcW)
                    {
                        newimgrect.Height = AnimationData.animations[curAnim].Frames[curFrame].SrcW + AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].bottomLeft;
                    }

                    if (AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].bottomRight > AnimationData.animations[curAnim].Frames[curFrame].SrcH)
                    {
                        newimgrect.Height = AnimationData.animations[curAnim].Frames[curFrame].SrcH + AnimationData.HitBoxes[AnimationData.animations[curAnim].Frames[curFrame].HitBoxIndex].bottomRight;
                    }
                }

                Bitmap fullimg = new Bitmap(1,1);

                if (newimgrect.Width > 0 && newimgrect.Height > 0)
                {
                    fullimg = new Bitmap(newimgrect.Width, newimgrect.Height);
                }
                else if (newimgrect.Width < 0 && newimgrect.Height > 0)
                {
                    fullimg = new Bitmap(1, newimgrect.Height);
                }
                else if (newimgrect.Width > 0 && newimgrect.Height < 0)
                {
                    fullimg = new Bitmap(newimgrect.Width, 1);
                }
                else if (newimgrect.Width < 0 && newimgrect.Height < 0)
                {
                    fullimg = new Bitmap(1,1);
                }

                using (Graphics g = Graphics.FromImage(fullimg))
                {
                    g.Clear(Color.FromArgb(0, 0, 0, 0));

                    g.DrawImage(img,0,0);

                    if (drawHitBoxesToolStripMenuItem.Checked)
                    {
                        Pen p = new Pen(Color.Black);
                        if (drawHitBoxesToolStripMenuItem.Checked)
                        {
                            g.DrawRectangle(p, Hitbox);
                        }
                    }
                }
                try
                {
                    switch (AnimationData.animations[curAnim].Frames[curFrame].Flip)
                    {
                        case 0:
                            fullimg.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            break;
                        case 1:
                            fullimg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 2:
                            fullimg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            break;
                        case 3:
                            fullimg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message);
                    animState = 0;
                }

                CurFrameImgBox.Image = fullimg;
                FrameList.Refresh();
                //CurFrameImgBox.AutoScrollOffset = new Point(AnimationData.animations[curAnim].Frames[curFrame].PivotX, AnimationData.animations[curAnim].Frames[curFrame].PivotY);
            }

            else { CurFrameImgBox.Image = null; FrameList.Images.Clear(); FrameList.Refresh(); }
            FrameList.SelectedIndex = curFrame;
        }

        void RefreshFrameList()
        {
            FrameList.Images.Clear();
            foreach (PSDK.FileFormats.SpriteMappings.sprFrame f in AnimationData.animations[curAnim].Frames)
            {
                Rectangle Crop = new Rectangle(0, 0, 0, 0);

                Crop.X = f.SrcX;
                Crop.Y = f.SrcY;
                Crop.Width = f.SrcW;
                Crop.Height = f.SrcH;

                Bitmap img = CropImage(TexturesImg[f.imgIndex], Crop);

                try
                {
                    switch (f.Flip)
                    {
                        case 0:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            break;
                        case 1:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 2:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            break;
                        case 3:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message);
                    animState = 0;
                }

                FrameList.Images.Add(img);
            }
            FrameList.Refresh();
        }

        void RefreshFrameListIndex(int index)
        {
            Rectangle Crop = new Rectangle(0, 0, 0, 0);

            Crop.X = AnimationData.animations[curAnim].Frames[curFrame].SrcX;
            Crop.Y = AnimationData.animations[curAnim].Frames[curFrame].SrcY;
            Crop.Width = AnimationData.animations[curAnim].Frames[curFrame].SrcW;
            Crop.Height = AnimationData.animations[curAnim].Frames[curFrame].SrcH;

            Bitmap img = CropImage(TexturesImg[AnimationData.animations[curAnim].Frames[curFrame].imgIndex], Crop);

            try
            {
                switch (AnimationData.animations[curAnim].Frames[curFrame].Flip)
                {
                    case 0:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                        break;
                    case 1:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 2:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        break;
                    case 3:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: " + Ex.Message);
                animState = 0;
            }

            FrameList.Images[index] = img;
            FrameList.Refresh();
        }

        void RefreshAnimList()
        {
            AnimListBox.Items.Clear();
            foreach (PSDK.FileFormats.SpriteMappings.sprAnimation a in AnimationData.animations)
            {
                AnimListBox.Items.Add(a.AnimName);
            }
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            if (section.Width <= 0)
            {
                section.Width = 1;
            }
            if (section.Height <= 0)
            {
                section.Height = 1;
            }
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source.Clone(new Rectangle(0, 0, source.Width, source.Height), System.Drawing.Imaging.PixelFormat.DontCare), 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public byte GetIndex(Bitmap img, Point xy)
        {
            System.Drawing.Imaging.BitmapData ImgData = img.LockBits(new Rectangle(xy, new Size(1, 1)), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(ImgData.Scan0);
            img.UnlockBits(ImgData);
            return b;
        }

        private void saveFrameTogifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "gif";
            dlg.Filter = "Gif Image Files|*.gif|Eclipse Engine Graphics Files|*.gfx";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.FilterIndex-1 == 0)
                {
                    CurFrameImgBox.Image.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                }
                if (dlg.FilterIndex-1 == 1)
                {
                    if (CurFrameImgBox.Image.Palette != null)
                    {
                        PSDK.FileFormats.gfx gfx = new PSDK.FileFormats.gfx();

                        gfx.palette.Pal.Clear();

                        for (int i = 0; i < CurFrameImgBox.Image.Palette.Entries.Length; i++)
                        {
                            gfx.palette.Pal.Add(i, new PSDK.Colour(CurFrameImgBox.Image.Palette.Entries[i].R, CurFrameImgBox.Image.Palette.Entries[i].G, CurFrameImgBox.Image.Palette.Entries[i].B));
                        }

                        gfx.height = (ushort)CurFrameImgBox.Image.Height;
                        gfx.width = (ushort)CurFrameImgBox.Image.Width;

                        gfx.IndexedImageLayout = new ushort[gfx.height][];

                        for (int i = 0; i < gfx.height; i++)
                        {
                            gfx.IndexedImageLayout[i] = new ushort[gfx.width];
                        }

                        for (int y = 0; y < CurFrameImgBox.Image.Height; y++)
                        {
                            for (int x = 0; x < CurFrameImgBox.Image.Width; x++)
                            {
                                gfx.IndexedImageLayout[y][x] = GetIndex((Bitmap)CurFrameImgBox.Image, new Point(x, y));
                            }
                        }
                        gfx.Write(dlg.FileName);
                    }
                    else
                    {
                        Console.WriteLine("NO PALETTE!");
                    }
                }
            }
        }

        void AnimControl()
        {
            if (AnimationData.animations[curAnim].FrameAmount > 0)
            {
                switch (animState)
                {
                    case 0:
                        framecheck = 0;
                        PlayedX = 0;
                        if (PlayedX < 1) { curFrame = 1; }
                        animState = 1;
                        DelayTimer.Start();
                        break;
                    case 1:
                        framecheck = 0;
                        PlayedX = 0;
                        curFrame = 0;
                        RefreshFrame();
                        DelayTimer.Stop();
                        animState = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        void Animate(System.Object sender, System.EventArgs e)
        {
            if (animState == 1 && AnimationData.animations[curAnim].SpeedMultiplyer >= 1 && AnimationData.animations[curAnim].FrameAmount >= 2)
            {
                framecheck++;
                if (framecheck >= AnimationData.animations[curAnim].Frames[curFrame].Delay / AnimationData.animations[curAnim].SpeedMultiplyer)
                {
                    framecheck = 0;
                    if (curFrame >= AnimationData.animations[curAnim].FrameAmount - 1)
                    {
                        if (PlayedX < 1) { curFrame = 0; }
                        if (PlayedX >= 1) { curFrame = AnimationData.animations[curAnim].LoopIndex; }
                        RefreshFrame();
                        Application.DoEvents();
                    }
                    else
                    {
                        curFrame++;
                        if (curFrame >= AnimationData.animations[curAnim].FrameAmount - 1)
                        {
                            PlayedX++; if (PlayedX < 1) { curFrame = 0; }
                        }
                        RefreshFrame();
                        Application.DoEvents();
                    }
                }
            }
            else
            {
                AnimControl();
            }
        }

        private void drawHitBoxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawHitBoxesToolStripMenuItem.Checked = !drawHitBoxesToolStripMenuItem.Checked;
            RefreshFrame();
        }

        private void importAnimOverridesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (importAnimOverridesToolStripMenuItem.Checked)
            {
                importAnimOverridesToolStripMenuItem.Checked = AnimOverride = false;
            }
            else
            {
                importAnimOverridesToolStripMenuItem.Checked = AnimOverride = true;
            }
        }

        private void importFrameOverridesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (importFrameOverridesToolStripMenuItem.Checked)
            {
                importFrameOverridesToolStripMenuItem.Checked = FrameOverride = false;
            }
            else
            {
                importFrameOverridesToolStripMenuItem.Checked = FrameOverride = true;
            }
        }

        private void exportAnimsToDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select Directory to Export Anims to!";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = 0; i < AnimationData.animations.Count; i++)
                {
                    AnimationData.animations[i].Write(new PSDK.Writer(dlg.SelectedPath + "//" + AnimationData.animations[i].AnimName + ".spr.ani"));
                }
            }
        }

        private void exportFramesToDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select Directory to Export Anims to!";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = 0; i < AnimationData.animations[curAnim].Frames.Count; i++)
                {
                    AnimationData.animations[curAnim].Frames[i].Write(new PSDK.Writer(dlg.SelectedPath + "//Frame" + i + ".spr.frame"));
                }
            }
        }
    }

}
