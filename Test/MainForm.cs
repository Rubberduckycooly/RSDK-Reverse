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

namespace Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string filepath = dlg.FileName;
                //RSDKv5.GameConfig gc = new RSDKv5.GameConfig(filepath);

                //RSDKv5.GameConfig gc = new RSDKv5.GameConfig("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Game\\GameConfig.bin");

                //RSDKv5.GameConfig gc = new RSDKv5.GameConfig("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\mods\\Sonic Mega Mania\\Data\\Game\\GameConfig.bin");

                //RSDKvB.GameConfig gc = new RSDKvB.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data - Dev\\Game\\GameConfig.bin");
                //gc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Game\\GameConfig2.bin");

                //RSDKv2.GameConfig gc = new RSDKv2.GameConfig(filepath);
                //gc.SetDevMenu();
                //gc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Game\\GameConfig2.bin");

                //RSDKv1.GameConfig gc = new RSDKv1.GameConfig("C:\\Users\\owner\\Downloads\\sonic-legends-master\\Data\\Game\\GameConfig.bin");

                //RSDKv1.GameConfig gc = new RSDKv1.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Data\\Game\\GameConfig.bin");
                //gc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Data\\Game\\GameConfig2.bin");

                //RSDKvRS.Zoneconfig zc = new RSDKvRS.Zoneconfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\PC (2007)\\Data\\Levels\\EGZ\\Zone.zcf");
                //zc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\PC (2007)\\Data\\Levels\\EGZ\\Zone2.zcf");

                //RSDKv1.StageConfig sc = new RSDKv1.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Data\\Stages\\SSZ\\StageConfig.bin");
                //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\StageConfigCopy.bin");

                //RSDKv2.StageConfig sc = new RSDKv2.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\StageConfig.bin");           

                //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\StageConfigCopy.bin");

                //RSDKvB.StageConfig sc = new RSDKvB.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\StageConfig.bin");
                //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\StageConfigCopy.bin");

                //RSDKvRS.Level lvl = new RSDKvRS.Level("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\CPZ\\Act1.map");
                //lvl.Write("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\CPZ\\Act1copy.map");

                //RSDKvRS.Level lvl = new RSDKvRS.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\DATA\\LEVELS\\EHZ\\Act1.map");
                //lvl.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\DATA\\LEVELS\\EHZ\\Act1copy.map");

                //RSDKvRS.BGLayout BGmap = new RSDKvRS.BGLayout("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\EHZ\\ZoneBG.map");
                //BGmap.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EHZ\\ZoneBG2.map");

                //RSDKv2.BGLayout BGmap = new RSDKv2.BGLayout("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic CD\\mods\\Testing things\\Data\\Stages\\R31A\\Backgrounds.bin");
                //BGmap.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Backgrounds2.bin");

                //RSDKvB.BGLayout BGmap = new RSDKvB.BGLayout("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 2 Source\\Data\\Stages\\Zone01\\Backgrounds.bin");
                //BGmap.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 2 Source\\Data\\Stages\\Zone01\\Backgrounds2.bin");

                //RSDKvRS.til chunks = new RSDKvRS.til("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EHZ\\Zone.til");

                //RSDKv1.Level lvl = new RSDKv1.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\Act1.bin");

                //RSDKv2.Level lvl = new RSDKv2.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Act1.bin");
                //lvl.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Act1Copy.bin");

                //RSDKvB.Level lvl = new RSDKvB.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\Act1.bin");

                //RSDKv5.Scene lvl = new RSDKv5.Scene("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZCutscene\\Scene1.bin");
                //lvl.Write("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZ\\Scene1b2.bin");

                //RSDKvB.Tiles128x128 t128 = new RSDKvB.Tiles128x128("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\128x128Tiles.bin");

                //RSDKvRS.gfx img = new RSDKvRS.gfx("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Levels\\EGZ\\Zone.gfx");

                //RSDKvRS.tcf tc = new RSDKvRS.tcf("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic Plus\\Data\\Levels\\CPZ\\ZoneOG.tcf", true);

                //for (int i = 0; i < 1024; i++)
                //{
                //tc.Collision[i].PCunknown = 68;
                //}
                //tc.Write("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic Plus\\Data\\Levels\\CPZ\\Zone.tcf", false);
                //cm.Write("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic CD\\mods\\Testing things\\Data\\Stages\\R12A\\CollisionMasks.bin");

                //RSDKvB.CollisionMask cm = new RSDKvB.CollisionMask("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\(Source) Data\\Stages\\Zone01\\CollisionMasks.bin");

                //RSDKv5.TilesConfig tc = new RSDKv5.TilesConfig("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZ\\TileConfig.bin");

                //RSDKvRS.mdf RetroStages = new RSDKvRS.mdf("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\TitleScr\\Zones.mdf");

                //RetroStages.Write("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\TitleScr\\Zones2.mdf");

                //RSDKv1.Video rsv = new RSDKv1.Video(filepath);

                //RSDKvRS.Script rsf = new RSDKvRS.Script(filepath);

                //Models
                //RSDKv5.Model MDL = new RSDKv5.Model(filepath);
                //string tmp = filepath.Replace(Path.GetExtension(filepath), "");
                //MDL.Write(new RSDKv5.Writer(tmp + "2.bin"));
                //MDL.WriteAsSTLBinary(new RSDKv5.Writer(tmp + ".stl"));
                //MDL.WriteAsOBJ(tmp + ".obj", Path.GetFileName(tmp) + ".mtl");
                //MDL.WriteMTL(new RSDKv5.Writer(tmp + ".mtl"));


                RSDKvB.Model MDL = new RSDKvB.Model(filepath);
                //RSDKvB.BitmapFont BMF = new RSDKvB.BitmapFont(new StreamReader(File.OpenRead(filepath)));

                /* Palettes
                RSDKvB.Palette p = new RSDKvB.Palette();
                RSDKvB.Reader r = new RSDKvB.Reader(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle.act", FileMode.Open));
                p.Read(r);
                RSDKvB.Writer w = new RSDKvB.Writer(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle2.act", FileMode.Create));
                p.Write(w);*/

                /*int r = 0x20;
                int g = 0x20;
                int b = 0x80;

                ushort paltest = (ushort)(((ushort)(r >> 3) << 11) | ((ushort)(g >> 3) << 6) | 1 | 2 * (b >> 3));
                Console.WriteLine(paltest);*/
            }
        }
    }
}
