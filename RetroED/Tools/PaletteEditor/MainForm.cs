using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.PaletteEditor
{
  // Cyotek Color Picker controls library
  // Copyright © 2013-2017 Cyotek Ltd.
  // http://cyotek.com/blog/tag/colorpicker

  // Licensed under the MIT License. See license.txt for the full text.

  // If you use this code in your applications, donations or attribution are welcome

  internal partial class MainForm : BaseForm
  {
        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
        int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress,
          byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        bool MemoryEditing = false;
        int GameType = -1; //-1 for NULL, 0 = RSonic, 1 = Nexus, 2 = CD

    #region Constructors

        public MainForm()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Properties

        RSDKvRS.Zoneconfig StageconfigvRS = new RSDKvRS.Zoneconfig();
        RSDKv1.StageConfig Stageconfigv1 = new RSDKv1.StageConfig();
        RSDKv2.StageConfig Stageconfigv2 = new RSDKv2.StageConfig();
        RSDKvB.StageConfig StageconfigvB = new RSDKvB.StageConfig();
        RSDKvB.GameConfig GameconfigvB = new RSDKvB.GameConfig();

        public bool ShowPalRotations = true;

        public RetroED.MainForm Parent;

        Timer palTimer = new Timer();

        public string Filepath;
        public int Type = 0;

        #endregion

        #region Methods

        protected override void OnLoad(EventArgs e)
        {
                base.OnLoad(e);
                colorGrid.Colors.Clear();
            for (int i = 0; i < 256; i++)
            {
                colorGrid.Colors.Add(Color.FromArgb(255, 255, 0, 255));
            }
            palTimer.Interval = ((int)(0.1f * 1000));
            palTimer.Tick += new EventHandler(Palette_Update);
            palTimer.Start();
        }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

        private void colorGrid_ColorChanged(object sender, EventArgs e)
        {
            SplitContainer.Panel2.BackColor = colorGrid.Color;

            //colorToolStripStatusLabel.Text = string.Format("{0}, {1}, {2}", colorGrid.Color.R, colorGrid.Color.G, colorGrid.Color.B);

            colorEditor.Color = colorGrid.Color;
            colorEditor.BackColor = SplitContainer.Panel1.BackColor;
            SplitContainer.Panel2.BackColor = SplitContainer.Panel1.BackColor;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKvRS ZoneConfig Files|Zone.zcf|RSDKv1 StageConfig Files|StageConfig.bin|RSDKv2 StageConfig Files|StageConfig.bin|RSDKvB StageConfig Files|StageConfig.bin|RSDKvB GameConfig Files|GameConfig.bin|Adobe Colour Table Files|*.act";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                MemoryEditing = false;
                GameType = 0;
                Cyotek.Windows.Forms.ColorCollection pal = new Cyotek.Windows.Forms.ColorCollection();
                colorGrid.Colors.Clear(); //Clear the colourGrid!
                string RSDK = "RSDKvB";
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        StageconfigvRS = new RSDKvRS.Zoneconfig(dlg.FileName);

                        for (int h = 0; h < 2; h++) //For two rows...
                        {
                            for (int w = 0; w < 16; w++)// Get 16 colours...
                            {
                                Color c = Color.FromArgb(255, StageconfigvRS.StagePalette.Colors[h][w].R, StageconfigvRS.StagePalette.Colors[h][w].G, StageconfigvRS.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        RSDK = "RSDKvRS";
                        break;
                    case 1:
                        Stageconfigv1 = new RSDKv1.StageConfig(dlg.FileName);

                        for (int h = 0; h < 2; h++)// For six rows...
                        {
                            for (int w = 0; w < 16; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, Stageconfigv1.StagePalette.Colors[h][w].R, Stageconfigv1.StagePalette.Colors[h][w].G, Stageconfigv1.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        RSDK = "RSDKv1";
                        break;
                    case 2:
                        Stageconfigv2 = new RSDKv2.StageConfig(dlg.FileName);

                        for (int h = 0; h < 2; h++)// For six rows...
                        {
                            for (int w = 0; w < 16; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, Stageconfigv2.StagePalette.Colors[h][w].R, Stageconfigv2.StagePalette.Colors[h][w].G, Stageconfigv2.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        RSDK = "RSDKv2";
                        break;
                    case 3:
                        StageconfigvB = new RSDKvB.StageConfig(dlg.FileName);

                        for (int h = 0; h < 2; h++)// For six rows...
                        {
                            for (int w = 0; w < 16; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, StageconfigvB.StagePalette.Colors[h][w].R, StageconfigvB.StagePalette.Colors[h][w].G, StageconfigvB.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        RSDK = "RSDKvB";
                        break;
                    case 4:
                        GameconfigvB = new RSDKvB.GameConfig(dlg.FileName);

                        for (int h = 0; h < 6; h++)// For six rows...
                        {
                            for (int w = 0; w < GameconfigvB.MasterPalette.COLORS_PER_COLUMN; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, GameconfigvB.MasterPalette.Colors[h][w].R, GameconfigvB.MasterPalette.Colors[h][w].G, GameconfigvB.MasterPalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        RSDK = "RSDKvB";
                        break;
                    case 5:
                        pal = Cyotek.Windows.Forms.ColorCollection.LoadPalette(dlg.FileName); //Load .act file
                        RSDK = ".Act File";
                        break;
                    default:
                        break;
                }

                string dispname = "";
                string folder = Path.GetDirectoryName(dlg.FileName);
                DirectoryInfo di = new DirectoryInfo(folder);
                folder = di.Name;
                string file = Path.GetFileName(dlg.FileName);

                if (dlg.FileName != null)
                {
                    RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                    dispname = folder + "/" + file;
                }
                else
                {
                    RetroED.MainForm.Instance.CurrentTabText = "New Palette - RSDK Palette Editor";
                    dispname = "New Palette - RSDK Palette Editor";
                }

                Text = Path.GetFileName(dlg.FileName) + " - RSDK Palette Editor";

                Parent.rp.state = "RetroED - " + this.Text;
                Parent.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
                SharpPresence.Discord.RunCallbacks();
                SharpPresence.Discord.UpdatePresence(Parent.rp);

                colorGrid.Colors = pal; //Set the Colourgrid's colours!

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKvRS ZoneConfig Files|Zone.zcf|RSDKv1 StageConfig Files|StageConfig.bin|RSDKv2 StageConfig Files|StageConfig.bin|RSDKvB StageConfig Files|StageConfig.bin|RSDKvB GameConfig Files|GameConfig.bin|Adobe Colour Table Files|*.act";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Type = dlg.FilterIndex - 1;
                switch (Type)
                {
                    case 0:
                        int i1 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i1].R, colorGrid.Colors[i1].G, colorGrid.Colors[i1].B);
                                i1++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                StageconfigvRS.StagePalette.Colors[h][w].R = c.R;
                                StageconfigvRS.StagePalette.Colors[h][w].G = c.G;
                                StageconfigvRS.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        StageconfigvRS.Write(dlg.FileName); //Save that!
                        break;
                    case 1:
                        int i2 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i2].R, colorGrid.Colors[i2].G, colorGrid.Colors[i2].B);
                                i2++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                Stageconfigv1.StagePalette.Colors[h][w].R = c.R;
                                Stageconfigv1.StagePalette.Colors[h][w].G = c.G;
                                Stageconfigv1.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        Stageconfigv1.Write(dlg.FileName); //Save that!
                        break;
                    case 2:
                        int i3 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i3].R, colorGrid.Colors[i3].G, colorGrid.Colors[i3].B);
                                i3++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                Stageconfigv2.StagePalette.Colors[h][w].R = c.R;
                                Stageconfigv2.StagePalette.Colors[h][w].G = c.G;
                                Stageconfigv2.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        Stageconfigv2.Write(dlg.FileName); //Save that!
                        break;
                    case 3:
                        int is4 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[is4].R, colorGrid.Colors[is4].G, colorGrid.Colors[is4].B);
                                is4++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                StageconfigvB.StagePalette.Colors[h][w].R = c.R;
                                StageconfigvB.StagePalette.Colors[h][w].G = c.G;
                                StageconfigvB.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        Stageconfigv2.Write(dlg.FileName); //Save that!
                        break;
                    case 4:
                        int i4 = 0;
                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < GameconfigvB.MasterPalette.COLORS_PER_COLUMN; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i4].R, colorGrid.Colors[i4].G, colorGrid.Colors[i4].B);
                                i4++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                GameconfigvB.MasterPalette.Colors[h][w].R = c.R;
                                GameconfigvB.MasterPalette.Colors[h][w].G = c.G;
                                GameconfigvB.MasterPalette.Colors[h][w].B = c.B;
                            }
                        }
                        GameconfigvB.Write(dlg.FileName); //Save that!
                        break;
                    case 5:
                        Cyotek.Windows.Forms.IPaletteSerializer serializer;

                        serializer = new Cyotek.Windows.Forms.AdobeColorTablePaletteSerializer();

                        using (Stream stream = File.Create(dlg.FileName))
                        {
                            serializer.Serialize(stream, colorGrid.Colors); //Save a .act file
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            if (!MemoryEditing)
            {
                if (colorGrid.ColorIndex < colorGrid.Colors.Count && colorGrid.ColorIndex >= 0) { colorGrid.Colors[colorGrid.ColorIndex] = colorEditor.Color; } //Change the colour if it's in bounds of the palette
            }
            else
            {
                if (colorGrid.ColorIndex < colorGrid.Colors.Count && colorGrid.ColorIndex >= 0)
                {
                    colorGrid.Colors[colorGrid.ColorIndex] = colorEditor.Color; //Change the colour if it's in bounds of the palette

                    if (GameType == 0)
                    {
                        Process process = Process.GetProcessesByName("RSonic")[0];
                        IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);

                        int bytesWritten = 0;
                        byte[] buffer = new byte[] { colorEditor.Color.B, colorEditor.Color.G, colorEditor.Color.R, 0};

                        int offset = 0x0053FD74;

                        WriteProcessMemory((int)processHandle, offset + (colorGrid.ColorIndex * 4), buffer, buffer.Length, ref bytesWritten);
                    }

                    if (GameType == 1)
                    {
                        Process process = Process.GetProcessesByName("Nexus")[0];
                        IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);

                        int bytesWritten = 0;
                        byte[] buffer = new byte[] { colorEditor.Color.B, colorEditor.Color.G, colorEditor.Color.R, 0 };

                        int offset = 0x00473408;

                        WriteProcessMemory((int)processHandle, offset + (colorGrid.ColorIndex * 4), buffer, buffer.Length, ref bytesWritten);
                    }

                    if (GameType == 2)
                    {
                        Process process = Process.GetProcessesByName("Soniccd")[0];
                        /*IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);

                        int bytesWritten = 0;
                        byte[] buffer = new byte[] { colorEditor.Color.B, colorEditor.Color.G, colorEditor.Color.R, 0 };

                        int offset = 0x00473408;

                        WriteProcessMemory((int)processHandle, offset + (colorGrid.ColorIndex * 4), buffer, buffer.Length, ref bytesWritten);
                        */
                    }

                } 

            }
        }


        private void viewRetroSonicInternalPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.GetProcessesByName("RSonic")[0];

                /*Process[] process = Process.GetProcesses();

                for (int i = 0; i < process.Length; i++)
                {
                    Console.WriteLine(process[i].ProcessName);
                }*/

                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                MemoryEditing = true;
                GameType = 0;

                int bytesRead = 0;
                byte[] buffer = new byte[1024]; //3 bytes for RGB and one for... idk lmfao

                colorGrid.Colors.Clear();
                Cyotek.Windows.Forms.ColorCollection cc = new Cyotek.Windows.Forms.ColorCollection();

                int offset = 0x0053FD74;

                for (int i = 0; i < 256; i++)
                {
                    ReadProcessMemory((int)processHandle, offset + (i * 4), buffer, buffer.Length, ref bytesRead);

                    Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                    cc.Add(c);

                    Console.WriteLine(buffer[0] + " " + buffer[1] + " " + buffer[2]);
                }
                colorGrid.Colors = cc;

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: " + Ex.Message + Environment.NewLine + "(This likely means you don't have 'RSonic.exe' open!)");
                MemoryEditing = false;
            }
        }

        private void viewSonicNexusInternalPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.GetProcessesByName("Nexus")[0];

                MemoryEditing = true;
                GameType = 1;

                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                int bytesRead = 0;
                byte[] buffer = new byte[1024]; //3 bytes for RGB and one for... idk lmfao

                colorGrid.Colors.Clear();
                Cyotek.Windows.Forms.ColorCollection cc = new Cyotek.Windows.Forms.ColorCollection();

                int offset = 0x00473408;

                for (int i = 0; i < 256; i++)
                {
                    ReadProcessMemory((int)processHandle, offset + (i * 4), buffer, buffer.Length, ref bytesRead);

                    Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                    cc.Add(c);

                    Console.WriteLine(buffer[0] + " " + buffer[1] + " " + buffer[2]);
                }
                colorGrid.Colors = cc;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: " + Ex.Message + Environment.NewLine + "(This likely means you don't have 'Nexus.exe' open!)");
                MemoryEditing = false;
            }
        }

        private void viewSonicCDInternalPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.GetProcessesByName("Soniccd")[0];

                MemoryEditing = true;
                GameType = 2;

                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                int bytesRead = 0;
                byte[] buffer = new byte[2048]; //3 bytes for RGB and one for... idk lmfao

                colorGrid.Colors.Clear();
                Cyotek.Windows.Forms.ColorCollection cc = new Cyotek.Windows.Forms.ColorCollection();

                //int offset = 0x01C34D88;
                int offset = 0xABFB64;

                for (int i = 0; i < 2048; i++)
                {
                    ReadProcessMemory((int)processHandle, offset + (i), buffer, buffer.Length, ref bytesRead);

                    Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                    cc.Add(c);

                    Console.WriteLine(buffer[0]);// + " " + buffer[1] + " " + buffer[2]);
                }

                /*for (int i = 0; i < 256; i++)
                {
                    ReadProcessMemory((int)processHandle, offset + (i), buffer, buffer.Length, ref bytesRead);

                    Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                    cc.Add(c);

                    Console.WriteLine(buffer[0]);// + " " + buffer[1] + " " + buffer[2]);
                }*/
                colorGrid.Colors = cc;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: " + Ex.Message + Environment.NewLine + "(This likely means you don't have 'Soniccd.exe' open!)");
                MemoryEditing = false;
            }
        }

        private void showPaletteRotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ShowPalRotations)
            {
                MenuItem_ShowPalRotations.Checked = ShowPalRotations = true;
                palTimer.Start();
            }
            else
            {
                MenuItem_ShowPalRotations.Checked = ShowPalRotations = false;
                palTimer.Stop();
            }
        }

        private void Palette_Update(object sender, EventArgs e)
        {
            if (MemoryEditing)
            {
                try
                {
                    if (GameType == 0)
                    {
                        Process process = Process.GetProcessesByName("RSonic")[0];

                        IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                        MemoryEditing = true;
                        GameType = 0;

                        int bytesRead = 0;
                        byte[] buffer = new byte[1024]; //3 bytes for RGB and one for... idk lmfao

                        colorGrid.Colors.Clear();
                        Cyotek.Windows.Forms.ColorCollection cc = new Cyotek.Windows.Forms.ColorCollection();

                        int offset = 0x0053FD74;

                        for (int i = 0; i < 256; i++)
                        {
                            ReadProcessMemory((int)processHandle, offset + (i * 4), buffer, buffer.Length, ref bytesRead);

                            Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                            cc.Add(c);
                        }
                        colorGrid.Colors = cc;
                    }

                    if (GameType == 1)
                    {
                        Process process = Process.GetProcessesByName("Nexus")[0];

                        IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                        int bytesRead = 0;
                        byte[] buffer = new byte[1024]; //3 bytes for RGB and one for... idk lmfao

                        colorGrid.Colors.Clear();
                        Cyotek.Windows.Forms.ColorCollection cc = new Cyotek.Windows.Forms.ColorCollection();

                        int offset = 0x00473408;

                        for (int i = 0; i < 256; i++)
                        {
                            ReadProcessMemory((int)processHandle, offset + (i * 4), buffer, buffer.Length, ref bytesRead);

                            Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                            cc.Add(c);
                        }
                        colorGrid.Colors = cc;
                    }
                    if (GameType == 2)
                    {
                        Process process = Process.GetProcessesByName("soniccd")[0];

                        IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                        int bytesRead = 0;
                        byte[] buffer = new byte[2048]; //3 bytes for RGB and one for... idk lmfao

                        colorGrid.Colors.Clear();
                        Cyotek.Windows.Forms.ColorCollection cc = new Cyotek.Windows.Forms.ColorCollection();

                        //int offset = 0x01C34D88;
                        int offset = 0x00A2EC00;

                        for (int i = 0; i < 2048; i++)
                        {
                            ReadProcessMemory((int)processHandle, offset + (316) + (i), buffer, buffer.Length, ref bytesRead);

                            Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                            cc.Add(c);

                            Console.WriteLine(buffer[0]);// + " " + buffer[1] + " " + buffer[2]);
                        }

                        /*for (int i = 0; i < 256; i++)
                        {
                            ReadProcessMemory((int)processHandle, offset + (i), buffer, buffer.Length, ref bytesRead);

                            Color c = Color.FromArgb(255, buffer[2], buffer[1], buffer[0]);

                            cc.Add(c);

                            Console.WriteLine(buffer[0]);// + " " + buffer[1] + " " + buffer[2]);
                        }*/
                        colorGrid.Colors = cc;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message + Environment.NewLine + "(This likely means you don't have 'RSonic.exe', 'Nexus.exe' or 'Soniccd.exe' open!)");
                    MemoryEditing = false;
                }
            }
        }

        private void exportLoadedPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Adobe Colour Table Files|*.act";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Cyotek.Windows.Forms.IPaletteSerializer serializer;

                serializer = new Cyotek.Windows.Forms.AdobeColorTablePaletteSerializer();

                using (Stream stream = File.Create(dlg.FileName))
                {
                    serializer.Serialize(stream, colorGrid.Colors); //Save a .act file
                }
            }
        }

        private void MenuItem_New_Click(object sender, EventArgs e)
        {
            StageconfigvRS = new RSDKvRS.Zoneconfig();
            Stageconfigv1 = new RSDKv1.StageConfig();
            Stageconfigv2 = new RSDKv2.StageConfig();
            StageconfigvB = new RSDKvB.StageConfig();
            GameconfigvB = new RSDKvB.GameConfig();
            colorGrid.Colors.Clear();
            for (int i = 0; i < 256; i++)
            {
                colorGrid.Colors.Add(Color.FromArgb(255, 255, 0, 255));
            }
            Filepath = null;
        }

        private void MenuItem_Save_Click(object sender, EventArgs e)
        {
            if (Filepath != null)
            {
                switch (Type)
                {
                    case 0:
                        int i1 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i1].R, colorGrid.Colors[i1].G, colorGrid.Colors[i1].B);
                                i1++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                StageconfigvRS.StagePalette.Colors[h][w].R = c.R;
                                StageconfigvRS.StagePalette.Colors[h][w].G = c.G;
                                StageconfigvRS.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        StageconfigvRS.Write(Filepath); //Save that!
                        break;
                    case 1:
                        int i2 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i2].R, colorGrid.Colors[i2].G, colorGrid.Colors[i2].B);
                                i2++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                Stageconfigv1.StagePalette.Colors[h][w].R = c.R;
                                Stageconfigv1.StagePalette.Colors[h][w].G = c.G;
                                Stageconfigv1.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        Stageconfigv1.Write(Filepath); //Save that!
                        break;
                    case 2:
                        int i3 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i3].R, colorGrid.Colors[i3].G, colorGrid.Colors[i3].B);
                                i3++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                Stageconfigv2.StagePalette.Colors[h][w].R = c.R;
                                Stageconfigv2.StagePalette.Colors[h][w].G = c.G;
                                Stageconfigv2.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        Stageconfigv2.Write(Filepath); //Save that!
                        break;
                    case 3:
                        int is4 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[is4].R, colorGrid.Colors[is4].G, colorGrid.Colors[is4].B);
                                is4++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                StageconfigvB.StagePalette.Colors[h][w].R = c.R;
                                StageconfigvB.StagePalette.Colors[h][w].G = c.G;
                                StageconfigvB.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        Stageconfigv2.Write(Filepath); //Save that!
                        break;
                    case 4:
                        int i4 = 0;
                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < GameconfigvB.MasterPalette.COLORS_PER_COLUMN; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i4].R, colorGrid.Colors[i4].G, colorGrid.Colors[i4].B);
                                i4++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                GameconfigvB.MasterPalette.Colors[h][w].R = c.R;
                                GameconfigvB.MasterPalette.Colors[h][w].G = c.G;
                                GameconfigvB.MasterPalette.Colors[h][w].B = c.B;
                            }
                        }
                        GameconfigvB.Write(Filepath); //Save that!
                        break;
                    case 5:
                        Cyotek.Windows.Forms.IPaletteSerializer serializer;

                        serializer = new Cyotek.Windows.Forms.AdobeColorTablePaletteSerializer();

                        using (Stream stream = File.Create(Filepath))
                        {
                            serializer.Serialize(stream, colorGrid.Colors); //Save a .act file
                        }
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

        #endregion
    }
}
