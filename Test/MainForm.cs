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

            //RSDKv5.GameConfig gc = new RSDKv5.GameConfig("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Game\\GameConfig.bin");

            //RSDKv4.GameConfig gc = new RSDKv4.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Game\\GameConfig.bin");
            //gc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Game\\GameConfig2.bin");

            //RSDKv3.GameConfig gc = new RSDKv3.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Game\\GameConfig.bin");
            //gc.SetDevMenu();
            //gc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Game\\GameConfig2.bin");

            //RSDKv2.GameConfig gc = new RSDKv2.GameConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Data\\Game\\GameConfig.bin");
            //gc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Data\\Game\\GameConfig2.bin");

            //RSDKv1.zcf zc = new RSDKv1.zcf("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EGZ\\Zone.zcf");
            //zc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EGZ\\Zone2.zcf");

            //RSDKv2.StageConfig sc = new RSDKv2.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Data\\Stages\\SSZ\\StageConfig.bin");
            //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\StageConfigCopy.bin");

            //RSDKv3.StageConfig sc = new RSDKv3.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\StageConfig.bin");
            //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\StageConfigCopy.bin");

            //RSDKv4.StageConfig sc = new RSDKv4.StageConfig("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\StageConfig.bin");
            //sc.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\StageConfigCopy.bin");

            //RSDKv1.Level lvl = new RSDKv1.Level("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\CPZ\\Act1.map");
            //lvl.Write("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\CPZ\\Act1copy.map");

            //RSDKv1.Level lvl = new RSDKv1.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\DATA\\LEVELS\\EHZ\\Act1.map");
            //lvl.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\DATA\\LEVELS\\EHZ\\Act1copy.map");

            //RSDKv1.BGLayout BGmap = new RSDKv1.BGLayout("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\EHZ\\ZoneBG.map");
            //BGmap.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EHZ\\ZoneBG2.map");

            //RSDKv3.BGLayout BGmap = new RSDKv3.BGLayout("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic CD\\mods\\Testing things\\Data\\Stages\\R31A\\Backgrounds.bin");
            //BGmap.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Backgrounds2.bin");

            //RSDKv4.BGLayout BGmap = new RSDKv4.BGLayout("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 2 Source\\Data\\Stages\\Zone01\\Backgrounds.bin");
            //BGmap.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 2 Source\\Data\\Stages\\Zone01\\Backgrounds2.bin");

            //RSDKv1.til chunks = new RSDKv1.til("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Data\\Levels\\EHZ\\Zone.til");

            //RSDKv2.Level lvl = new RSDKv2.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic Nexus Source\\Stages\\SSZ\\Act1.bin");

            //RSDKv3.Level lvl = new RSDKv3.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Act1.bin");
            //lvl.Write("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic CD Source\\Data\\Stages\\R11A\\Act1Copy.bin");

            //RSDKv4.Level lvl = new RSDKv4.Level("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\Act1.bin");

            //RSDKv5.Scene lvl = new RSDKv5.Scene("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZCutscene\\Scene1.bin");
            //lvl.Write("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZ\\Scene1b2.bin");

            //RSDKv4.Tiles128x128 t128 = new RSDKv4.Tiles128x128("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\128x128Tiles.bin");

            //RSDKv1.gfx img = new RSDKv1.gfx("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Retro Sonic Source\\Levels\\EGZ\\Zone.gfx");

            //RSDKv1.tcf tc = new RSDKv1.tcf("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\EGZ\\ZoneOG.tcf", false);

            for (int i = 0; i < 1024; i++)
            {
                //tc.Collision[i].PCunknown = 68;

                //These Seem to control the Tile properties somehow?

                /*tc.Collision[i].unknownP1[0] = 255;
                tc.Collision[i].unknownP1[1] = 255;
                tc.Collision[i].unknownP1[2] = 255;
                tc.Collision[i].unknownP1[3] = 255;
                tc.Collision[i].unknownP1[4] = 255;
                tc.Collision[i].unknownP1[5] = 255;
                tc.Collision[i].unknownP1[6] = 255;
                tc.Collision[i].unknownP1[7] = 255;
            tc.Collision[i].unknownP1[8] = 255;
            tc.Collision[i].unknownP1[9] = 255;
            tc.Collision[i].unknownP1[10] = 255;
            tc.Collision[i].unknownP1[11] = 255;
            tc.Collision[i].unknownP1[12] = 255;
            tc.Collision[i].unknownP1[13] = 255;
            tc.Collision[i].unknownP1[14] = 255;
            tc.Collision[i].unknownP1[15] = 255;
            tc.Collision[i].unknownP1[16] = 255;
        tc.Collision[i].unknownP1[17] = 255;
        tc.Collision[i].unknownP1[18] = 255;
        tc.Collision[i].unknownP1[19] = 255;
        tc.Collision[i].unknownP1[20] = 255;
        tc.Collision[i].unknownP1[21] = 255;
        tc.Collision[i].unknownP1[22] = 255;
        tc.Collision[i].unknownP1[23] = 255;
        tc.Collision[i].unknownP1[24] = 255;
        tc.Collision[i].unknownP1[25] = 255;
        tc.Collision[i].unknownP1[26] = 255;
        tc.Collision[i].unknownP1[27] = 255;
        tc.Collision[i].unknownP1[28] = 255;
        tc.Collision[i].unknownP1[29] = 255;
        tc.Collision[i].unknownP1[30] = 255;
        tc.Collision[i].unknownP1[31] = 255;*/

                /*tc.Collision[i].unknownP2[0] = 0;
                tc.Collision[i].unknownP2[1] = 0;
                tc.Collision[i].unknownP2[2] = 0;
                tc.Collision[i].unknownP2[3] = 0;
                tc.Collision[i].unknownP2[4] = 0;
                tc.Collision[i].unknownP2[5] = 0;
                tc.Collision[i].unknownP2[6] = 0;
                tc.Collision[i].unknownP2[7] = 0;
                tc.Collision[i].unknownP2[8] = 0;
                tc.Collision[i].unknownP2[9] = 0;
                tc.Collision[i].unknownP2[10] = 0;
                tc.Collision[i].unknownP2[11] = 0;
                tc.Collision[i].unknownP2[12] = 0;
                tc.Collision[i].unknownP2[13] = 0;
                tc.Collision[i].unknownP2[14] = 0;
                tc.Collision[i].unknownP2[15] = 0;
                tc.Collision[i].unknownP2[16] = 1;
                tc.Collision[i].unknownP2[17] = 1;
                tc.Collision[i].unknownP2[18] = 1;
                tc.Collision[i].unknownP2[19] = 1;
                tc.Collision[i].unknownP2[20] = 1;
                tc.Collision[i].unknownP2[21] = 1;
                tc.Collision[i].unknownP2[22] = 1;
                tc.Collision[i].unknownP2[23] = 1;
                tc.Collision[i].unknownP2[24] = 1;
                tc.Collision[i].unknownP2[25] = 1;
                tc.Collision[i].unknownP2[26] = 1;
                tc.Collision[i].unknownP2[27] = 1;
                tc.Collision[i].unknownP2[28] = 1;
                tc.Collision[i].unknownP2[29] = 1;
                tc.Collision[i].unknownP2[30] = 1;
                tc.Collision[i].unknownP2[31] = 1;*/

                //I'm 90% sure that these Values are angles for the tile, but then WHY ARE THERE 32 BYTES?
                /*
                tc.Collision[i].CollisionP1[0] = 128;
                tc.Collision[i].CollisionP1[1] = 128;
                tc.Collision[i].CollisionP1[2] = 128;
                tc.Collision[i].CollisionP1[3] = 128;
                tc.Collision[i].CollisionP1[4] = 128;
                tc.Collision[i].CollisionP1[5] = 128;
                tc.Collision[i].CollisionP1[6] = 128;
                tc.Collision[i].CollisionP1[7] = 128;
                tc.Collision[i].CollisionP1[8] = 128;
                tc.Collision[i].CollisionP1[9] = 128;
                tc.Collision[i].CollisionP1[10] = 128;
                tc.Collision[i].CollisionP1[11] = 128;
                tc.Collision[i].CollisionP1[12] = 128;
                tc.Collision[i].CollisionP1[13] = 128;
                tc.Collision[i].CollisionP1[14] = 128;
                tc.Collision[i].CollisionP1[15] = 128;
                tc.Collision[i].CollisionP1[16] = 128;
                tc.Collision[i].CollisionP1[17] = 128;
                tc.Collision[i].CollisionP1[18] = 128;
                tc.Collision[i].CollisionP1[19] = 128;
                tc.Collision[i].CollisionP1[20] = 128;
                tc.Collision[i].CollisionP1[21] = 128;
                tc.Collision[i].CollisionP1[22] = 128;
                tc.Collision[i].CollisionP1[23] = 128;
                tc.Collision[i].CollisionP1[24] = 128;
                tc.Collision[i].CollisionP1[25] = 128;
                tc.Collision[i].CollisionP1[26] = 128;
                tc.Collision[i].CollisionP1[27] = 128;
                tc.Collision[i].CollisionP1[28] = 128;
                tc.Collision[i].CollisionP1[29] = 128;
                tc.Collision[i].CollisionP1[30] = 128;
                tc.Collision[i].CollisionP1[31] = 128;*/


                /*tc.Collision[i].CollisionP2[0] = 0;
                tc.Collision[i].CollisionP2[1] = 0;
                tc.Collision[i].CollisionP2[2] = 0;
                tc.Collision[i].CollisionP2[3] = 0;
                tc.Collision[i].CollisionP2[4] = 0;
                tc.Collision[i].CollisionP2[5] = 0;
                tc.Collision[i].CollisionP2[6] = 0;
                tc.Collision[i].CollisionP2[7] = 0;
                tc.Collision[i].CollisionP2[8] = 0;
                tc.Collision[i].CollisionP2[9] = 0;
                tc.Collision[i].CollisionP2[10] = 0;
                tc.Collision[i].CollisionP2[11] = 0;
                tc.Collision[i].CollisionP2[12] = 0;
                tc.Collision[i].CollisionP2[13] = 0;
                tc.Collision[i].CollisionP2[14] = 0;
                tc.Collision[i].CollisionP2[15] = 0;
                tc.Collision[i].CollisionP2[16] = 0;
                tc.Collision[i].CollisionP2[17] = 0;
                tc.Collision[i].CollisionP2[18] = 0;
                tc.Collision[i].CollisionP2[19] = 0;
                tc.Collision[i].CollisionP2[20] = 0;
                tc.Collision[i].CollisionP2[21] = 0;
                tc.Collision[i].CollisionP2[22] = 0;
                tc.Collision[i].CollisionP2[23] = 0;
                tc.Collision[i].CollisionP2[24] = 0;
                tc.Collision[i].CollisionP2[25] = 0;
                tc.Collision[i].CollisionP2[26] = 0;
                tc.Collision[i].CollisionP2[27] = 0;
                tc.Collision[i].CollisionP2[28] = 0;
                tc.Collision[i].CollisionP2[29] = 0;
                tc.Collision[i].CollisionP2[30] = 0;
                tc.Collision[i].CollisionP2[31] = 0;*/
            }

            //tc.Write("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\Levels\\EGZ\\Zone2.tcf", false);

            //RSDKv2.CollisionMask cm = new RSDKv2.CollisionMask("C:\\Users\\owner\\Documents\\Fan Games\\Sonic Nexus\\Data\\Stages\\SSZ\\CollisionMasks.bin");

            for (int i = 0; i < 1024; i++)
            {
               /* //cm.Collision[i].Config[0] = 255; //Unknown, Makes the collision wonky if set to 255
                //cm.Collision[i].Config[1] = 0; //Slope
                //cm.Collision[i].Config[2] = 0; //Physics/Stickyness
                //cm.Collision[i].Config[3] = 0; //Unknown
                //cm.Collision[i].Config[4] = 255; //Unknown, if set to 255 it destroys the game's physics, (changes angle, stops player from using "Spin Tubes" & can even "Zip" the player)
                if (i > 0)
                {
                cm.Collision[i].Collision[0] = 255;
                cm.Collision[i].Collision[1] = 255;
                cm.Collision[i].Collision[2] = 255;
                cm.Collision[i].Collision[3] = 255;
                cm.Collision[i].Collision[4] = 255;
                cm.Collision[i].Collision[5] = 255;
                cm.Collision[i].Collision[6] = 255;
                cm.Collision[i].Collision[7] = 236;
                cm.Collision[i].Collision[8] = 255;
                cm.Collision[i].Collision[9] = 255;
                }
*/
            }

            //cm.Write("C:\\Users\\owner\\Documents\\Fan Games\\Sonic Nexus\\Data\\Stages\\SSZ\\CollisionMasks.bin");

            //RSDKv3.CollisionMask cm = new RSDKv3.CollisionMask("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic CD\\mods\\Testing things\\Data\\Stages\\R12A\\CollisionMasks2.bin");

            /*for (int i = 0; i < 1024; i++)
            {
                //cm.Collision[i].Config[0] = 255; //Unknown, Makes the collision wonky if set to 255
                //cm.Collision[i].Config[1] = 0; //Slope
                //cm.Collision[i].Config[2] = 0; //Physics/Stickyness
                //cm.Collision[i].Config[3] = 0; //Unknown
                //cm.Collision[i].Config[4] = 255; //Unknown, if set to 255 it destroys the game's physics, (changes angle, stops player from using "Spin Tubes" & can even "Zip" the player)
                
                cm.Collision[i].Collision[0] = 0;
                cm.Collision[i].Collision[1] = 255;
                cm.Collision[i].Collision[2] = 0;
                cm.Collision[i].Collision[3] = 255;
                cm.Collision[i].Collision[4] = 0;
                cm.Collision[i].Collision[5] = 255;
                cm.Collision[i].Collision[6] = 0;
                cm.Collision[i].Collision[7] = 255;
                cm.Collision[i].Collision[8] = 0;
                cm.Collision[i].Collision[9] = 255;
            }*/

            //cm.Write("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic CD\\mods\\Testing things\\Data\\Stages\\R12A\\CollisionMasks.bin");

            //RSDKv4.CollisionMask cm = new RSDKv4.CollisionMask("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Stages\\Zone01\\CollisionMasks.bin");

            //RSDKv5.TilesConfig tc = new RSDKv5.TilesConfig("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Sonic Mania\\Data\\Stages\\GHZ\\TileConfig.bin");

            //RSDKv1.mdf RetroStages = new RSDKv1.mdf("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\TitleScr\\Zones.mdf");

            //RetroStages.Write("C:\\Users\\owner\\Documents\\Fan Games\\Retro Sonic\\Data\\TitleScr\\Zones2.mdf");

            /* Palettes
            RSDKv4.Palette p = new RSDKv4.Palette();
            RSDKv4.Reader r = new RSDKv4.Reader(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle.act", FileMode.Open));
            p.Read(r);
            RSDKv4.Writer w = new RSDKv4.Writer(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle2.act", FileMode.Create));
            p.Write(w);*/
        }
    }
}
