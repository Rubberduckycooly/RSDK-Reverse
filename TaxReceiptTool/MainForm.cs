using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TaxReceiptTool
{
    public partial class MainForm : Form
    {

        public RSDKv1.Script Script;
        public string name;

        public string datafolderpath;
        public List<string> scriptnames = new List<string>();
        public List<string> dirnames = new List<string>();
        public List<RSDKv1.Script> Scripts = new List<RSDKv1.Script>();
        int Value0 = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenScriptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "TaxReceipt Scripts|*.txt";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Script = new RSDKv1.Script(new System.IO.StreamReader(dlg.FileName));
                name = Path.GetFileNameWithoutExtension(dlg.FileName);
            }
        }

        private void ExportToAnimButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKvRS Animations|*.ani|RSDKv1 Animations|*.ani|RSDKv2 Animations|*.ani|RSDKvB Animations|*.ani|RSDKv5 Animations|*.bin";

            dlg.FileName = name;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                switch(dlg.FilterIndex-1)
                {
                    case 0:
                        RSDKvRS.Animation animvRS = new RSDKvRS.Animation();
                        List<RSDKvRS.Animation.sprAnimation> sprAnimvRS = new List<RSDKvRS.Animation.sprAnimation>();

                        RSDKv1.Script.Sub subvRS = new RSDKv1.Script.Sub();

                        for (int i = 0; i < Script.Subs.Count; i++)
                        {
                            if (Script.Subs[i].Name == "SubObjectStartup")
                            {
                                subvRS = Script.Subs[i];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSpritesvRS = subvRS.GetFunctionByName("LoadSpriteSheet");

                        for (int i = 0; i < LoadSpritesvRS.Count; i++)
                        {
                            animvRS.SpriteSheets[i] = LoadSpritesvRS[i].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SpriteFramesvRS = subvRS.GetFunctionByName("SpriteFrame");

                        for (byte s = 0; s < LoadSpritesvRS.Count; s++)
                        {
                            RSDKvRS.Animation.sprAnimation a = new RSDKvRS.Animation.sprAnimation();
                            for (int i = 0; i < SpriteFramesvRS.Count; i++)
                            {
                                RSDKvRS.Animation.sprAnimation.sprFrame Frame = new RSDKvRS.Animation.sprAnimation.sprFrame();
                                Frame.PivotX = Convert.ToSByte(SpriteFramesvRS[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesvRS[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesvRS[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesvRS[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesvRS[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesvRS[i].Paramaters[5]);
                                Frame.SpriteSheet = s;
                                a.Frames.Add(Frame);
                            }
                            sprAnimvRS.Add(a);
                        }

                        for (int i = 0; i < sprAnimvRS.Count; i++)
                        {
                            animvRS.Animations.Add(sprAnimvRS[i]);
                        }

                        animvRS.Write(new RSDKvRS.Writer(dlg.FileName));

                        break;
                    case 1:
                        RSDKv1.Animation animv1 = new RSDKv1.Animation();
                        List<RSDKv1.Animation.sprAnimation> sprAnimv1 = new List<RSDKv1.Animation.sprAnimation>();

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int i = 0; i < Script.Subs.Count; i++)
                        {
                            if (Script.Subs[i].Name == "SubObjectStartup")
                            {
                                subv1 = Script.Subs[i];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSpritesv1 = subv1.GetFunctionByName("LoadSpriteSheet");

                        for (int i = 0; i < LoadSpritesv1.Count; i++)
                        {
                            animv1.SpriteSheets[i] = LoadSpritesv1[i].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SpriteFramesv1 = subv1.GetFunctionByName("SpriteFrame");

                        for (byte s = 0; s < LoadSpritesv1.Count; s++)
                        {
                            RSDKv1.Animation.sprAnimation a = new RSDKv1.Animation.sprAnimation();
                            for (int i = 0; i < SpriteFramesv1.Count; i++)
                            {
                                RSDKv1.Animation.sprAnimation.sprFrame Frame = new RSDKv1.Animation.sprAnimation.sprFrame();
                                Frame.PivotX = Convert.ToSByte(SpriteFramesv1[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesv1[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesv1[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesv1[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesv1[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesv1[i].Paramaters[5]);
                                Frame.SpriteSheet = s;
                                a.Frames.Add(Frame);
                            }
                            sprAnimv1.Add(a);
                        }

                        for (int i = 0; i < sprAnimv1.Count; i++)
                        {
                            animv1.Animations.Add(sprAnimv1[i]);
                        }

                        animv1.Write(new RSDKv1.Writer(dlg.FileName));

                        break;
                    case 2:
                        RSDKv2.Animation animv2 = new RSDKv2.Animation();
                        List<RSDKv2.Animation.sprAnimation> sprAnimv2 = new List<RSDKv2.Animation.sprAnimation>();

                        RSDKv1.Script.Sub subv2 = new RSDKv1.Script.Sub();

                        for (int i = 0; i < Script.Subs.Count; i++)
                        {
                            if (Script.Subs[i].Name == "SubObjectStartup")
                            {
                                subv2 = Script.Subs[i];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSpritesv2 = subv2.GetFunctionByName("LoadSpriteSheet");

                        for (int i = 0; i < LoadSpritesv2.Count; i++)
                        {
                            animv2.SpriteSheets.Add(LoadSpritesv2[i].Paramaters[0]);
                        }

                        List<RSDKv1.Script.Sub.Function> SpriteFramesv2 = subv2.GetFunctionByName("SpriteFrame");

                        for (byte s = 0; s < LoadSpritesv2.Count; s++)
                        {
                            RSDKv2.Animation.sprAnimation a = new RSDKv2.Animation.sprAnimation();
                            a.AnimName = name + " Animation " + s;
                            for (int i = 0; i < SpriteFramesv2.Count; i++)
                            {
                                int PivotX = Convert.ToInt32(SpriteFramesv2[i].Paramaters[0]);
                                int PivotY = Convert.ToInt32(SpriteFramesv2[i].Paramaters[1]);
                                uint Width = Convert.ToUInt32(SpriteFramesv2[i].Paramaters[2]);
                                uint Height = Convert.ToUInt32(SpriteFramesv2[i].Paramaters[3]);
                                uint X = Convert.ToUInt32(SpriteFramesv2[i].Paramaters[4]);
                                uint Y = Convert.ToUInt32(SpriteFramesv2[i].Paramaters[5]);

                                if (Width > 255) Width = 255;
                                if (Height > 255) Height = 255;
                                if (X > 255) X = 255;
                                if (Y > 255) Y = 255;


                                RSDKv2.Animation.sprAnimation.sprFrame Frame = new RSDKv2.Animation.sprAnimation.sprFrame();
                                Frame.PivotX = (SByte)PivotX;
                                Frame.PivotY = (SByte)PivotY;
                                Frame.Width = (byte)Width;
                                Frame.Height = (byte)Height;
                                Frame.X = (byte)X;
                                Frame.Y = (byte)Y;
                                a.Frames.Add(Frame);
                            }
                            sprAnimv2.Add(a);
                        }

                        for (int i = 0; i < sprAnimv2.Count; i++)
                        {
                            animv2.Animations.Add(sprAnimv2[i]);
                        }

                        animv2.Write(new RSDKv2.Writer(dlg.FileName));

                        break;
                    case 3:
                        RSDKvB.Animation animvB = new RSDKvB.Animation();
                        List<RSDKvB.Animation.sprAnimation> sprAnimvB = new List<RSDKvB.Animation.sprAnimation>();

                        RSDKv1.Script.Sub subvB = new RSDKv1.Script.Sub();

                        for (int i = 0; i < Script.Subs.Count; i++)
                        {
                            if (Script.Subs[i].Name == "SubObjectStartup")
                            {
                                subvB = Script.Subs[i];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSpritesvB = subvB.GetFunctionByName("LoadSpriteSheet");

                        for (int i = 0; i < LoadSpritesvB.Count; i++)
                        {
                            animvB.SpriteSheets.Add(LoadSpritesvB[i].Paramaters[0]);
                        }

                        List<RSDKv1.Script.Sub.Function> SpriteFramesvB = subvB.GetFunctionByName("SpriteFrame");

                        for (byte s = 0; s < LoadSpritesvB.Count; s++)
                        {
                            RSDKvB.Animation.sprAnimation a = new RSDKvB.Animation.sprAnimation();
                            a.AnimName = name + " Animation " + s;
                            for (int i = 0; i < SpriteFramesvB.Count; i++)
                            {
                                RSDKvB.Animation.sprAnimation.sprFrame Frame = new RSDKvB.Animation.sprAnimation.sprFrame();
                                Frame.PivotX = Convert.ToSByte(SpriteFramesvB[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesvB[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesvB[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesvB[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesvB[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesvB[i].Paramaters[5]);
                                Frame.SpriteSheet = s;
                                a.Frames.Add(Frame);
                            }
                            sprAnimvB.Add(a);
                        }

                        for (int i = 0; i < sprAnimvB.Count; i++)
                        {
                            animvB.Animations.Add(sprAnimvB[i]);
                        }

                        animvB.Write(new RSDKvB.Writer(dlg.FileName));

                        break;
                    case 4:
                        RSDKv5.Animation animv5 = new RSDKv5.Animation();
                        List<RSDKv5.Animation.sprAnimation> sprAnimv5 = new List<RSDKv5.Animation.sprAnimation>();

                        RSDKv1.Script.Sub subv5 = new RSDKv1.Script.Sub();

                        for (int i = 0; i < Script.Subs.Count; i++)
                        {
                            if (Script.Subs[i].Name == "SubObjectStartup")
                            {
                                subv5 = Script.Subs[i];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSpritesv5 = subv5.GetFunctionByName("LoadSpriteSheet");

                        for (int i = 0; i < LoadSpritesv5.Count; i++)
                        {
                            animv5.SpriteSheets.Add(LoadSpritesv5[i].Paramaters[0]);
                        }

                        List<RSDKv1.Script.Sub.Function> SpriteFramesv5 = subv5.GetFunctionByName("SpriteFrame");

                        for (byte s = 0; s < LoadSpritesv5.Count; s++)
                        {
                            RSDKv5.Animation.sprAnimation a = new RSDKv5.Animation.sprAnimation();
                            a.AnimName = name + " Animation " + s;
                            for (int i = 0; i < SpriteFramesv5.Count; i++)
                            {
                                RSDKv5.Animation.sprAnimation.sprFrame Frame = new RSDKv5.Animation.sprAnimation.sprFrame();
                                Frame.PivotX = Convert.ToSByte(SpriteFramesv5[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesv5[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesv5[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesv5[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesv5[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesv5[i].Paramaters[5]);
                                Frame.Delay = 256;
                                Frame.SpriteSheet = s;
                                a.Frames.Add(Frame);
                            }
                            sprAnimv5.Add(a);
                        }

                        for (int i = 0; i < sprAnimv5.Count; i++)
                        {
                            animv5.Animations.Add(sprAnimv5[i]);
                        }

                        animv5.Write(new RSDKv5.Writer(dlg.FileName));

                        break;
                }
            }
        }

        private void OpenDataFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select a data folder!";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath + "//Scripts");
                datafolderpath = dlg.SelectedPath;
                LoadDir(dir);
            }
        }

        public void LoadDir(DirectoryInfo dir)
        {

            foreach(FileInfo f in dir.GetFiles())
            {
                if (Path.GetExtension(f.FullName) == ".txt")
                {
                    string file = f.Directory.Name + "//" + Path.GetFileNameWithoutExtension(f.Name);
                    Scripts.Add(new RSDKv1.Script(new System.IO.StreamReader(datafolderpath + "//Scripts//" + file + ".txt")));
                    scriptnames.Add(file);
                }
            }

            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                dirnames.Add(d.Name);
                LoadDir(d);
            }
        }

        private void ExportDataFolderButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKvRS Animations|*.ani|RSDKv1 Animations|*.ani|RSDKv2 Animations|*.ani|RSDKvB Animations|*.ani|RSDKv5 Animations|*.bin";

            dlg.FileName = name;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(datafolderpath + "//Animations");

                CreateDirs(dir);
                Value0 = 0;

                for (int i = 0; i < scriptnames.Count; i++)
                {
                    ExportAnim(dlg.FilterIndex - 1);
                }
            }
        }

        public void CreateDirs(DirectoryInfo dir)
        {
            if (Value0 < dirnames.Count)
            {
                DirectoryInfo d = new DirectoryInfo(datafolderpath + "//Animations//" + dirnames[Value0]);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                Value0++;
                CreateDirs(d);
            }
        }

        public void ExportAnim(int Type)
        {
            switch (Type)
            {
                case 0:
                    RSDKvRS.Animation animvRS = new RSDKvRS.Animation();
                    List<RSDKvRS.Animation.sprAnimation> sprAnimvRS = new List<RSDKvRS.Animation.sprAnimation>();

                    RSDKv1.Script.Sub subvRS = new RSDKv1.Script.Sub();

                    for (int i = 0; i < Scripts[Value0].Subs.Count; i++)
                    {
                        if (Scripts[Value0].Subs[i].Name == "SubObjectStartup")
                        {
                            subvRS = Scripts[Value0].Subs[i];
                            break;
                        }
                    }

                    List<RSDKv1.Script.Sub.Function> LoadSpritesvRS = subvRS.GetFunctionByName("LoadSpriteSheet");

                    for (int i = 0; i < LoadSpritesvRS.Count; i++)
                    {
                        animvRS.SpriteSheets[i] = LoadSpritesvRS[i].Paramaters[0];
                    }

                    List<RSDKv1.Script.Sub.Function> SpriteFramesvRS = subvRS.GetFunctionByName("SpriteFrame");

                    for (byte s = 0; s < LoadSpritesvRS.Count; s++)
                    {
                        RSDKvRS.Animation.sprAnimation a = new RSDKvRS.Animation.sprAnimation();
                        for (int i = 0; i < SpriteFramesvRS.Count; i++)
                        {
                            RSDKvRS.Animation.sprAnimation.sprFrame Frame = new RSDKvRS.Animation.sprAnimation.sprFrame();
                            Frame.PivotX = Convert.ToSByte(SpriteFramesvRS[i].Paramaters[0]);
                            Frame.PivotY = Convert.ToSByte(SpriteFramesvRS[i].Paramaters[1]);
                            Frame.Width = Convert.ToByte(SpriteFramesvRS[i].Paramaters[2]);
                            Frame.Height = Convert.ToByte(SpriteFramesvRS[i].Paramaters[3]);
                            Frame.X = Convert.ToByte(SpriteFramesvRS[i].Paramaters[4]);
                            Frame.Y = Convert.ToByte(SpriteFramesvRS[i].Paramaters[5]);
                            Frame.SpriteSheet = s;
                            a.Frames.Add(Frame);
                        }
                        sprAnimvRS.Add(a);
                    }

                    for (int i = 0; i < sprAnimvRS.Count; i++)
                    {
                        animvRS.Animations.Add(sprAnimvRS[i]);
                    }

                    string animnamevRS = datafolderpath + "//Animations//" + scriptnames[Value0++] + ".ani";

                    animvRS.Write(new RSDKvRS.Writer(animnamevRS));

                    break;
                case 1:
                    RSDKv1.Animation animv1 = new RSDKv1.Animation();
                    List<RSDKv1.Animation.sprAnimation> sprAnimv1 = new List<RSDKv1.Animation.sprAnimation>();

                    RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                    for (int i = 0; i < Scripts[Value0].Subs.Count; i++)
                    {
                        if (Scripts[Value0].Subs[i].Name == "SubObjectStartup")
                        {
                            subv1 = Scripts[Value0].Subs[i];
                            break;
                        }
                    }

                    List<RSDKv1.Script.Sub.Function> LoadSpritesv1 = subv1.GetFunctionByName("LoadSpriteSheet");

                    for (int i = 0; i < LoadSpritesv1.Count; i++)
                    {
                        animv1.SpriteSheets[i] = LoadSpritesv1[i].Paramaters[0];
                    }

                    List<RSDKv1.Script.Sub.Function> SpriteFramesv1 = subv1.GetFunctionByName("SpriteFrame");

                    for (byte s = 0; s < LoadSpritesv1.Count; s++)
                    {
                        RSDKv1.Animation.sprAnimation a = new RSDKv1.Animation.sprAnimation();
                        for (int i = 0; i < SpriteFramesv1.Count; i++)
                        {
                            RSDKv1.Animation.sprAnimation.sprFrame Frame = new RSDKv1.Animation.sprAnimation.sprFrame();
                            Frame.PivotX = 0;
                            Frame.PivotX = 0;
                            Frame.X = 0;
                            Frame.Y = 0;
                            Frame.Width = 0;
                            Frame.Height = 0;
                            try
                            {
                                Frame.PivotX = Convert.ToSByte(SpriteFramesv1[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesv1[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesv1[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesv1[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesv1[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesv1[i].Paramaters[5]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Fuck!");
                            }
                            Frame.SpriteSheet = s;
                            a.Frames.Add(Frame);
                        }
                        sprAnimv1.Add(a);
                    }

                    for (int i = 0; i < sprAnimv1.Count; i++)
                    {
                        animv1.Animations.Add(sprAnimv1[i]);
                    }

                    string animnamev1 = datafolderpath + "//Animations//" + scriptnames[Value0++] + ".ani";

                    animv1.Write(new RSDKv1.Writer(animnamev1));

                    break;
                case 2:
                    List<RSDKv2.Animation> animv2 = new List<RSDKv2.Animation>();
                    RSDKv2.Animation.sprAnimation sprAnimv2 = new RSDKv2.Animation.sprAnimation();

                    RSDKv1.Script.Sub subv2 = new RSDKv1.Script.Sub();

                    for (int i = 0; i < Scripts[Value0].Subs.Count; i++)
                    {
                        if (Scripts[Value0].Subs[i].Name == "SubObjectStartup")
                        {
                            subv2 = Scripts[Value0].Subs[i];
                            break;
                        }
                    }

                    List<RSDKv1.Script.Sub.Function> LoadSpritesv2 = subv2.GetFunctionByName("LoadSpriteSheet");

                    if (LoadSpritesv2.Count <= 0)
                    {
                        Value0++;
                        break;
                    }

                    for (int i = 0; i < LoadSpritesv2.Count; i++)
                    {
                        animv2.Add(new RSDKv2.Animation());
                        animv2[i].SpriteSheets.Add(LoadSpritesv2[i].Paramaters[0]);
                    }

                    List<RSDKv1.Script.Sub.Function> SpriteFramesv2 = subv2.GetFunctionByName("SpriteFrame");

                    if (SpriteFramesv2.Count <= 0)
                    {
                        Value0++;
                        break;
                    }

                    for (byte s = 0; s < LoadSpritesv2.Count; s++)
                    {

                        RSDKv2.Animation.sprAnimation a = new RSDKv2.Animation.sprAnimation();
                        if (s == 0) a.AnimName = name + " Animation";
                        else if (s > 0) a.AnimName = name + " Animation " + s;
                        for (int i = 0; i < SpriteFramesv2.Count; i++)
                        {
                            RSDKv2.Animation.sprAnimation.sprFrame Frame = new RSDKv2.Animation.sprAnimation.sprFrame();
                            Frame.PivotX = 0;
                            Frame.PivotX = 0;
                            Frame.X = 0;
                            Frame.Y = 0;
                            Frame.Width = 0;
                            Frame.Height = 0;
                            try
                            {
                                Frame.PivotX = Convert.ToSByte(SpriteFramesv2[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesv2[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesv2[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesv2[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesv2[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesv2[i].Paramaters[5]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Fuck!");
                            }
                            a.Frames.Add(Frame);
                        }
                        animv2[s].Animations.Add(a);
                    }

                    string animnamev2 = datafolderpath + "//Animations//" + scriptnames[Value0++];

                    for (int i = 0; i < animv2.Count; i++)
                    {
                        if (i == 0) animv2[i].Write(new RSDKv2.Writer(animnamev2 + ".ani"));
                        if (i > 0) animv2[i].Write(new RSDKv2.Writer(animnamev2 + (i+1) + ".ani"));
                    }

                    break;
                case 3:
                    RSDKvB.Animation animvB = new RSDKvB.Animation();
                    List<RSDKvB.Animation.sprAnimation> sprAnimvB = new List<RSDKvB.Animation.sprAnimation>();

                    RSDKv1.Script.Sub subvB = new RSDKv1.Script.Sub();

                    for (int i = 0; i < Scripts[Value0].Subs.Count; i++)
                    {
                        if (Scripts[Value0].Subs[i].Name == "SubObjectStartup")
                        {
                            subvB = Scripts[Value0].Subs[i];
                            break;
                        }
                    }

                    List<RSDKv1.Script.Sub.Function> LoadSpritesvB = subvB.GetFunctionByName("LoadSpriteSheet");

                    for (int i = 0; i < LoadSpritesvB.Count; i++)
                    {
                        animvB.SpriteSheets.Add(LoadSpritesvB[i].Paramaters[0]);
                    }

                    List<RSDKv1.Script.Sub.Function> SpriteFramesvB = subvB.GetFunctionByName("SpriteFrame");

                    for (byte s = 0; s < LoadSpritesvB.Count; s++)
                    {
                        RSDKvB.Animation.sprAnimation a = new RSDKvB.Animation.sprAnimation();
                        a.AnimName = name + " Animation " + s;
                        for (int i = 0; i < SpriteFramesvB.Count; i++)
                        {
                            RSDKvB.Animation.sprAnimation.sprFrame Frame = new RSDKvB.Animation.sprAnimation.sprFrame();
                            Frame.PivotX = 0;
                            Frame.PivotX = 0;
                            Frame.X = 0;
                            Frame.Y = 0;
                            Frame.Width = 0;
                            Frame.Height = 0;
                            try
                            {
                                Frame.PivotX = Convert.ToSByte(SpriteFramesvB[i].Paramaters[0]);
                                Frame.PivotY = Convert.ToSByte(SpriteFramesvB[i].Paramaters[1]);
                                Frame.Width = Convert.ToByte(SpriteFramesvB[i].Paramaters[2]);
                                Frame.Height = Convert.ToByte(SpriteFramesvB[i].Paramaters[3]);
                                Frame.X = Convert.ToByte(SpriteFramesvB[i].Paramaters[4]);
                                Frame.Y = Convert.ToByte(SpriteFramesvB[i].Paramaters[5]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Fuck!");
                            }
                            Frame.SpriteSheet = s;
                            a.Frames.Add(Frame);
                        }
                        sprAnimvB.Add(a);
                    }

                    for (int i = 0; i < sprAnimvB.Count; i++)
                    {
                        animvB.Animations.Add(sprAnimvB[i]);
                    }

                    string animnamevb = datafolderpath + "//Animations//" + scriptnames[Value0++] + ".ani";

                    animvB.Write(new RSDKvB.Writer(animnamevb));

                    break;
                case 4:
                    RSDKv5.Animation animv5 = new RSDKv5.Animation();
                    List<RSDKv5.Animation.sprAnimation> sprAnimv5 = new List<RSDKv5.Animation.sprAnimation>();

                    RSDKv1.Script.Sub subv5 = new RSDKv1.Script.Sub();

                    for (int i = 0; i < Scripts[Value0].Subs.Count; i++)
                    {
                        if (Scripts[Value0].Subs[i].Name == "SubObjectStartup")
                        {
                            subv5 = Scripts[Value0].Subs[i];
                            break;
                        }
                    }

                    List<RSDKv1.Script.Sub.Function> LoadSpritesv5 = subv5.GetFunctionByName("LoadSpriteSheet");

                    for (int i = 0; i < LoadSpritesv5.Count; i++)
                    {
                        animv5.SpriteSheets.Add(LoadSpritesv5[i].Paramaters[0]);
                    }

                    List<RSDKv1.Script.Sub.Function> SpriteFramesv5 = subv5.GetFunctionByName("SpriteFrame");

                    for (byte s = 0; s < LoadSpritesv5.Count; s++)
                    {
                        RSDKv5.Animation.sprAnimation a = new RSDKv5.Animation.sprAnimation();
                        a.AnimName = name + " Animation " + s;
                        for (int i = 0; i < SpriteFramesv5.Count; i++)
                        {
                            RSDKv5.Animation.sprAnimation.sprFrame Frame = new RSDKv5.Animation.sprAnimation.sprFrame();
                            Frame.PivotX = Convert.ToSByte(SpriteFramesv5[i].Paramaters[0]);
                            Frame.PivotY = Convert.ToSByte(SpriteFramesv5[i].Paramaters[1]);
                            Frame.Width = Convert.ToByte(SpriteFramesv5[i].Paramaters[2]);
                            Frame.Height = Convert.ToByte(SpriteFramesv5[i].Paramaters[3]);
                            Frame.X = Convert.ToByte(SpriteFramesv5[i].Paramaters[4]);
                            Frame.Y = Convert.ToByte(SpriteFramesv5[i].Paramaters[5]);
                            Frame.Delay = 256;
                            Frame.SpriteSheet = s;
                            a.Frames.Add(Frame);
                        }
                        sprAnimv5.Add(a);
                    }

                    for (int i = 0; i < sprAnimv5.Count; i++)
                    {
                        animv5.Animations.Add(sprAnimv5[i]);
                    }

                    string animnamev5 = datafolderpath + "//Animations//" + scriptnames[Value0++] + ".bin";
                    

                    animv5.Write(new RSDKv5.Writer(animnamev5));

                    break;
            }
        }
    }
}
