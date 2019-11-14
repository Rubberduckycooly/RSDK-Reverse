using System;
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
            dlg.Filter = "All Files|*";

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

                //RSDKvB.Tiles128x128 t128 = new RSDKvB.Tiles128x128(filepath);

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

                //CONFIGS
                //RSDKv5.GameConfig gameConfig = new RSDKv5.GameConfig(dlg.FileName);

                //RSDKv5.RSDKConfig RSDKConfig = new RSDKv5.RSDKConfig(dlg.FileName);

                //TIME ATTACK
                //RSDKv5.Replay replay = new RSDKv5.Replay(new RSDKv5.Reader(filepath));
                //RSDKv5.UserDB UserDB = new RSDKv5.UserDB(new RSDKv5.Reader(filepath));

                //ANIMATIONS
                //RSDKv2.Animation anim = new RSDKv2.Animation(new RSDKv2.Reader(dlg.FileName));
                /*RSDKv5.Animation anim = new RSDKv5.Animation(new RSDKv5.Reader(dlg.FileName));

                for (int i = 0; i < anim.Animations.Count; i++)
                {
                    for (int ii = 0; ii < anim.Animations[i].Frames.Count; ii++)
                    {
                        anim.Animations[i].Frames[ii].X += 256;
                    }
                }
                anim.Write(new RSDKv5.Writer(dlg.FileName + "2"));*/

                //Videos
                //RSDKv1.Video rsv = new RSDKv1.Video(filepath);
                //RSDKv2.Video rsv = new RSDKv2.Video(filepath);

                //SCENES
                //RSDKv5.Scene scene = new RSDKv5.Scene(new RSDKv5.Reader(filepath));

                //SAVES
                //RSDKv5.SaveFiles save = new RSDKv5.SaveFiles(new RSDKv5.Reader(filepath));
                //save.EncoreBuddyChar = 1;

                //CONTAINERS
                //RSDKv2.ArcContainer arc = new RSDKv2.ArcContainer(new RSDKv2.Reader(dlg.FileName));
                //arc.Unpack("Data/");

                //STRINGS
                //RSDKv2.StringSet strings = new RSDKv2.StringSet(new RSDKv2.Reader(dlg.FileName));

                //Scripts
                //RSDKv5.StaticObject Obj = new RSDKv5.StaticObject(new RSDKv5.Reader(filepath));
                //Obj.Write(new RSDKv5.Writer(filepath));
                //Obj.Write(new RSDKv5.Writer("Object.bin"));
                //RSDKvB.Bytecode bytecode = new RSDKvB.Bytecode(new RSDKvB.Reader(filepath), 0);

                //RSDKvRS.Reader reader = new RSDKvRS.Reader(filepath);
                //RSDKvRS.Script rsf = new RSDKvRS.Script(reader);

                /*string destName = Path.GetFileNameWithoutExtension(filepath);
                string destName2 = Path.GetFileName(filepath);

                string dirpath = filepath.Replace(destName2, "");
                DirectoryInfo dir = new DirectoryInfo(dirpath);
                DirectoryInfo dir2 = new DirectoryInfo(dirpath + "//Scripts");
                dir2.Create();

                foreach (FileInfo f in dir.GetFiles())
                {
                    destName = Path.GetFileNameWithoutExtension(f.FullName);
                    string destpath = dirpath + "//Scripts//" + destName + ".txt";
                    reader = new RSDKvRS.Reader(f.FullName);
                    //try
                    //{
                        //rsf = new RSDKvRS.Script(reader);
                        //rsf.Decompile(destpath);
                    ///}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //    reader.Close();
                    //}
                }*/

                //TILECONFIG
                //RSDKv2.Tileconfig tileconfig = new RSDKv2.Tileconfig(new RSDKv2.Reader(filepath));
                //RSDKv5.TileConfig tileConfig = new RSDKv5.TileConfig(new RSDKv5.Reader(filepath));

                /*RSDKv5.Writer writer = new RSDKv5.Writer("Palette.act");

                for (int i = 0; i < 256; i++)
                {
                    byte red, green, blue;

                    red = 255;
                    green = 0;
                    blue = 255;

                    if (i < Obj.Data.Length)
                    {
                        blue = (byte)((Obj.Data[i] & 0x00FF0000) >> 16);
                        green = (byte)((Obj.Data[i] & 0x0000FF00) >> 8);
                        red = (byte)((Obj.Data[i] & 0x000000FF));
                    }

                    writer.Write(red);
                    writer.Write(green);
                    writer.Write(blue);
                }
                writer.Close();*/

                //Settings
                //RSDKv5.Settings settings = new RSDKv5.Settings(new StreamReader(File.OpenRead(dlg.FileName)));
                //settings.Write(new StreamWriter(File.OpenWrite(dlg.FileName + ".bak")));

                //Models
                //RSDKv5.Model MDL = new RSDKv5.Model(filepath);
                //string tmp = filepath.Replace(Path.GetExtension(filepath), ".png");
                //MDL.Frames[0].AsImage(MDL, 5, 5, 5, 115, 0, 0, false).Save(tmp, System.Drawing.Imaging.ImageFormat.Png);
                //MDL.Write(new RSDKv5.Writer(tmp + "2.bin"));
                //MDL.WriteAsSTLBinary(new RSDKv5.Writer(tmp + ".stl"));
                //MDL.WriteAsOBJ(tmp + ".obj", Path.GetFileName(tmp) + ".mtl");
                //MDL.WriteMTL(new RSDKv5.Writer(tmp + ".mtl"));

                //RSDKvB.Model MDL = new RSDKvB.Model(filepath);

                //Text/Font stuff
                //RSDKvB.BitmapFont BMF = new RSDKvB.BitmapFont(new StreamReader(File.OpenRead(filepath)));
                //RSDKvB.StringList Strings = new RSDKvB.StringList(new StreamReader(File.OpenRead(filepath)));
                //Strings.Write(new StreamWriter(File.OpenWrite(filepath + "2")));
                //Console.WriteLine();
                /* Palettes
                RSDKvB.Palette p = new RSDKvB.Palette();
                RSDKvB.Reader r = new RSDKvB.Reader(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle.act", FileMode.Open));
                p.Read(r);
                RSDKvB.Writer w = new RSDKvB.Writer(File.Open("C:\\Users\\owner\\Documents\\Sonic Hacking Stuff\\Retro Engine Tools\\Sonic 1 Source\\Data\\Palettes\\SYZ_PalCycle2.act", FileMode.Create));
                p.Write(w);*/

                //RSDKv2.StringSet ss = new RSDKv2.StringSet(new RSDKv2.Reader(filepath));

                //ushort c = 2 * (160 >> 3) | ((ushort)(192 >> 3) << 11) | 1 | ((ushort)(224 >> 3) << 6);
                //ushort c = 2 * (224 >> 3) | ((ushort)(192 >> 3) << 11) | 1 | ((ushort)(160 >> 3) << 6);
                //ushort c = ((ushort)(R >> 3) << 11) | ((ushort)(G >> 3) << 6) | 2 * (B >> 3);
                //ushort c = ((ushort)(160 >> 3) << 11) | ((ushort)(192 >> 3) << 6) | 2 * (224 >> 3);

                //Console.WriteLine(c);

                /*int r = 0x20;
                int g = 0x20;
                int b = 0x80;

                ushort paltest = (ushort)(((ushort)(r >> 3) << 11) | ((ushort)(g >> 3) << 6) | 1 | 2 * (b >> 3));
                Console.WriteLine(paltest);*/

                /*StreamReader reader = new StreamReader(File.OpenRead(filepath));
                string data = reader.ReadToEnd();
                reader.Close();
                StreamWriter writer = new StreamWriter(File.OpenWrite(filepath));
                for (int i = 0; i < data.Length; i++)
                {                
                    if (data[i] == 0x2E)
                    {
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.Write(data[i]);
                    }
                }
                //writer.Write(data);
                writer.Close();*/

                //MATH
                //MANIA HEADER CHECKER
                /*BinaryReader reader = new BinaryReader(File.OpenRead(filepath));
                int value = reader.ReadInt32();
                Console.WriteLine(Path.GetFileNameWithoutExtension(filepath) + ": " + value);
                reader.Close();*/
                #region Exe Scanning

                /*
                    StreamReader reader = new StreamReader(File.OpenRead(dlg.FileName));

                    List<string> DataNames = new List<string>();

                    //string buffer = reader.ReadToEnd();

                    string buffer = "";

                    bool isChar(char a)
                    {

                        if (a == 'a' || a == 'A')
                        {
                            return true;
                        }

                        if (a == 'b' || a == 'B')
                        {
                            return true;
                        }

                        if (a == 'c' || a == 'C')
                        {
                            return true;
                        }

                        if (a == 'd' || a == 'D')
                        {
                            return true;
                        }

                        if (a == 'e' || a == 'E')
                        {
                            return true;
                        }

                        if (a == 'f' || a == 'F')
                        {
                            return true;
                        }

                        if (a == 'g' || a == 'G')
                        {
                            return true;
                        }

                        if (a == 'h' || a == 'H')
                        {
                            return true;
                        }

                        if (a == 'i' || a == 'I')
                        {
                            return true;
                        }

                        if (a == 'j' || a == 'J')
                        {
                            return true;
                        }

                        if (a == 'k' || a == 'K')
                        {
                            return true;
                        }

                        if (a == 'l' || a == 'L')
                        {
                            return true;
                        }

                        if (a == 'm' || a == 'M')
                        {
                            return true;
                        }

                        if (a == 'n' || a == 'N')
                        {
                            return true;
                        }

                        if (a == 'o' || a == 'O')
                        {
                            return true;
                        }

                        if (a == 'p' || a == 'P')
                        {
                            return true;
                        }

                        if (a == 'q' || a == 'Q')
                        {
                            return true;
                        }

                        if (a == 'r' || a == 'R')
                        {
                            return true;
                        }

                        if (a == 's' || a == 'S')
                        {
                            return true;
                        }

                        if (a == 't' || a == 'T')
                        {
                            return true;
                        }

                        if (a == 'u' || a == 'U')
                        {
                            return true;
                        }

                        if (a == 'v' || a == 'V')
                        {
                            return true;
                        }

                        if (a == 'w' || a == 'W')
                        {
                            return true;
                        }

                        if (a == 'x' || a == 'X')
                        {
                            return true;
                        }

                        if (a == 'y' || a == 'Y')
                        {
                            return true;
                        }

                        if (a == 'z' || a == 'Z')
                        {
                            return true;
                        }

                        if (a == '/' || a == '\\')
                        {
                            return true;
                        }

                        if (a == '.')
                        {
                            return true;
                        }

                        return false;
                    }


                    while (!reader.EndOfStream)
                    {
                        char d = (char)reader.Read();
                        if (d == 'd' || d == 'D')
                        {
                            char a1 = (char)reader.Read();
                            if (a1 == 'a' || a1 == 'A')
                            {
                                char t = (char)reader.Read();
                                if (t == 't' || t == 'T')
                                {
                                    char a2 = (char)reader.Read();
                                    if (a2 == 'a' || a2 == 'A')
                                    {
                                        char sl = (char)reader.Read();
                                        if (sl == '/' || sl == '/')
                                        {
                                            buffer = buffer + "Data/";

                                            char c;

                                            while (reader.Peek() != (char)0)
                                            {
                                                c = (char)reader.Read();
                                                buffer = buffer + c;
                                            }
                                            DataNames.Add(buffer);
                                        }
                                    }
                                }
                            }
                        }

                    }

                    StreamWriter writer = new StreamWriter(File.OpenWrite("Paths.txt"));

                    for (int i = 0; i < DataNames.Count; i++)
                    {
                        writer.Write(DataNames[i] + Environment.NewLine);
                    }
                    */

                #endregion
            }
        }
    }

    static class stringhelper
    {
        public static string GetUntilOrEmpty(string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return text;
        }
    }

}
