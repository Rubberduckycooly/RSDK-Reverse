using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Level_Converter
{
    public partial class RSDKMapConverter : Form
    {

        #region RSDKvRS
        RSDKv1.Level Lvlv1;
        RSDKv1.BGLayout BGv1;
        RSDKv1.zcf SCv1;
        RSDKv1.til Tiles128x128v1;
        RSDKv1.tcf CMv1;
        #endregion

        #region RSDKv1
        RSDKv2.Level Lvlv2;
        RSDKv2.BGLayout BGv2;
        RSDKv2.StageConfig SCv2;
        RSDKv2.Tiles128x128 Tiles128x128v2;
        RSDKv2.CollisionMask CMv2;
        #endregion

        #region RSDKv2
        RSDKv3.Level Lvlv3;
        RSDKv3.BGLayout BGv3;
        RSDKv3.StageConfig SCv3;
        RSDKv3.Tiles128x128 Tiles128x128v3;
        RSDKv3.CollisionMask CMv3;
        #endregion

        #region RSDKvB
        RSDKv4.Level Lvlv4;
        RSDKv4.BGLayout BGv4;
        RSDKv4.StageConfig SCv4;
        RSDKv4.Tiles128x128 Tiles128x128v4;
        RSDKv4.CollisionMask CMv4;
        #endregion

        #region RSDKv5
        RSDKv5.Scene Lvlv5;
        RSDKv5.StageConfig SCv5;
        RSDKv5.TilesConfig TCv5;
        #endregion

        int loadedRSDKver = 0;

        string exportPath;

        public RSDKMapConverter()
        {
            InitializeComponent();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Retro-Sonic Stages|Act*.map|RSDKv1 (Sonic Nexus) Stages|Act*.bin|RSDKv2 (Sonic CD) Stages|Act*.bin|RSDKvB (Sonic 1 & 2) Stages|Act*.bin|RSDKv5 (Sonic Mania) Stages|Scene.bin";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                loadedRSDKver = dlg.FilterIndex - 1;
                
                switch (loadedRSDKver)
                {
                    case 0://vRS
                        Lvlv1 = new RSDKv1.Level(dlg.FileName);
                        ImportedLvlBox.Text = dlg.FileName;
                        break;
                    case 1://v1
                        Lvlv2 = new RSDKv2.Level(dlg.FileName);
                        ImportedLvlBox.Text = dlg.FileName;
                        break;
                    case 2://v2
                        Lvlv3 = new RSDKv3.Level(dlg.FileName);
                        ImportedLvlBox.Text = dlg.FileName;
                        break;
                    case 3://vB
                        Lvlv4 = new RSDKv4.Level(dlg.FileName);
                        ImportedLvlBox.Text = dlg.FileName;
                        break;
                    case 4://v5
                        Lvlv5 = new RSDKv5.Scene(dlg.FileName);
                        ImportedLvlBox.Text = dlg.FileName;
                        break;
                    default:
                        break;
                        
                }
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Retro-Sonic Stages|*.map|RSDKv1 (Sonic Nexus) Stages|*.bin|RSDKv2 (Sonic CD) Stages|*.bin|RSDKvB (Sonic 1 & 2) Stages|*.bin|RSDKv5 (Sonic Mania) Stages|*.bin";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                exportPath = dlg.FileName;
                switch (dlg.FilterIndex-1)
                {
                    case 0://vRS
                        ConvertLevel(dlg.FilterIndex - 1);
                        break;
                    case 1://v1
                        ConvertLevel(dlg.FilterIndex - 1);
                        break;
                    case 2://v2
                        ConvertLevel(dlg.FilterIndex - 1);
                        break;
                    case 3://vB
                        ConvertLevel(dlg.FilterIndex - 1);
                        break;
                    case 4://v5
                        ConvertLevel(dlg.FilterIndex - 1);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ConvertLevel(int NewVer)
        {
            if (NewVer == 2)
            {
                switch (loadedRSDKver)
                {
                    case 0://vRS
                        break;
                    case 1://v1
                        Lvlv3 = new RSDKv3.Level();
                        Lvlv3.width = Lvlv2.width;
                        Lvlv3.height = Lvlv2.height;
                        Lvlv3.Title = Lvlv2.Title;
                        Lvlv3.MapLayout = Lvlv2.MapLayout;
                        Lvlv3.objects.Add(new RSDKv3.Object(1, 0, 64, 0)); //Player
                        Lvlv3.objects.Add(new RSDKv3.Object(2, 0, 0, 0)); //Stage Setup
                        Lvlv3.objects.Add(new RSDKv3.Object(3, 0, 0, 0)); //HUD
                        Lvlv3.objects.Add(new RSDKv3.Object(5, 0, 0, 0)); //Death Event
                        Lvlv3.objects.Add(new RSDKv3.Object(7, 0, 0, 0)); //Pause Menu
                        Lvlv3.Write(exportPath);
                        break;
                    case 2://v2
                        break;
                    case 3://vB
                        Lvlv3 = new RSDKv3.Level();
                        Lvlv3.width = Lvlv4.width;
                        Lvlv3.height = Lvlv4.height;
                        Lvlv3.Title = Lvlv4.Title;
                        Lvlv3.MapLayout = Lvlv4.MapLayout;
                        Lvlv3.objects.Add(new RSDKv3.Object(1, 0, 64, 1893));
                        Lvlv3.objects.Add(new RSDKv3.Object(1, 0, 64, 0)); //Player
                        Lvlv3.objects.Add(new RSDKv3.Object(2, 0, 0, 0)); //Stage Setup
                        Lvlv3.objects.Add(new RSDKv3.Object(3, 0, 0, 0)); //HUD
                        Lvlv3.objects.Add(new RSDKv3.Object(5, 0, 0, 0)); //Death Event
                        Lvlv3.objects.Add(new RSDKv3.Object(7, 0, 0, 0)); //Pause Menu
                        Lvlv3.Write(exportPath);
                        break;
                    case 4://v5
                        break;
                    default:
                        break;
                }
            }

            if (NewVer == 4)
            {
                switch (loadedRSDKver)
                {
                    case 0://vRS
                        break;
                    case 1://v1
                        break;
                    case 2://v2
                        RSDKv5.Scene scn = new RSDKv5.Scene("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZ\\Scene1 - Copy.bin");
                        scn.Layers[0].Height = (ushort)(Lvlv3.height * 8);
                        scn.Layers[0].Width = (ushort)(Lvlv3.width * 8);

                        scn.Layers[0].Tiles = new ushort[Lvlv3.height * 8][];

                        for (ushort i = 0; i < Lvlv3.height * 8; i++)
                        {
                            scn.Layers[0].Tiles[i] = new ushort[(ushort)(Lvlv3.width * 8)];
                            for (int j = 0; j < (ushort)(Lvlv3.width * 8); ++j)
                            { scn.Layers[0].Tiles[i][j] = 0xffff; /* the new ones with blanks*/}
                        }

                        for (int h = 0; h < Lvlv3.height; h++)
                        {
                            for (int w = 0; w < Lvlv3.width; w++)
                            {
                                for (int y = 0; h < 128; y++)
                                {
                                    for (int x = 0; w < 128; x++)
                                    {
                                        ushort tile;
                                        tile = 0xffff;
                                    }
                                }
                                        //scn.MapLayout[h][w] = (ushort)(Lvlv3.MapLayout[h][w]));
                            }
                        }

                        scn.Write("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZ\\Scene1.bin");
                
                break;
                    case 3://vB
                        break;
                    default:
                        break;
                }
            }

        }

    }
}
