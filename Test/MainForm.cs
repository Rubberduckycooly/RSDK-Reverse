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

            //RSDKv5.GameConfig gc = new RSDKv5.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Mania Source\\Original\\Data\\Game\\GameConfig.bin");

            //RSDKv4.GameConfig gc = new RSDKv4.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 2 Source\\Data\\Game\\GameConfig.bin");

            //RSDKv3.GameConfig gc = new RSDKv3.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Game\\GameConfig.bin");
            //gc.SetDevMenu();

            //RSDKv2.GameConfig gc = new RSDKv2.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Game\\GameConfig.bin");

            //RSDKv2.StageConfig sc = new RSDKv2.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\StageConfig.bin");
            //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\StageConfigCopy.bin");

            //RSDKv3.StageConfig sc = new RSDKv3.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\StageConfig.bin");
            //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\StageConfigCopy.bin");

            //RSDKv4.StageConfig sc = new RSDKv4.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\StageConfig.bin");
            //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\StageConfigCopy.bin");

            //RSDKv1.Level lvl = new RSDKv1.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Levels\\EGZ\\Act1.map");
            //lvl.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Levels\\EGZ\\Act1copy.map");
            //RSDKv1.Map map = new RSDKv1.Map("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Levels\\EGZ\\ZoneBG.map");

            //RSDKv2.Level lvl = new RSDKv2.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\Act1.bin");

            //RSDKv3.Level lvl = new RSDKv3.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Act1.bin");
            //lvl.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Act1Copy.bin");

            //RSDKv4.Level lvl = new RSDKv4.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\Act1.bin");

            RSDKv5.Scene lvl = new RSDKv5.Scene("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Mania Source\\Original\\Data\\Stages\\GHZ\\Scene1.bin");


            //RSDKv1.gfx img = new RSDKv1.gfx("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Levels\\EGZ\\Zone.gfx");

            /* Palettes
            RSDKv4.Palette p = new RSDKv4.Palette();
            RSDKv4.Reader r = new RSDKv4.Reader(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle.act", FileMode.Open));
            p.Read(r);
            RSDKv4.Writer w = new RSDKv4.Writer(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle2.act", FileMode.Create));
            p.Write(w);*/
        }
    }
}
