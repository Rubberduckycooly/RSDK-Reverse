using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;

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

    private string PalettePath
    {
      get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "palettes"); }
    }
        RSDKvRS.Zoneconfig sc1;
        RSDKv1.StageConfig sc2;
        RSDKv2.StageConfig sc3;
        RSDKvB.StageConfig sc4;
        RSDKvB.GameConfig gc4;
        //RSDKv5.GameConfig gc5;
        //RSDKv5.StageConfig sc5;

        public bool ShowPalRotations = true;

        public RetroED.MainForm Parent;

        Timer palTimer = new Timer();

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

            colorToolStripStatusLabel.Text = string.Format("{0}, {1}, {2}", colorGrid.Color.R, colorGrid.Color.G, colorGrid.Color.B);

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
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        sc1 = new RSDKvRS.Zoneconfig(dlg.FileName);

                        for (int h = 0; h < 2; h++) //For two rows...
                        {
                            for (int w = 0; w < 16; w++)// Get 16 colours...
                            {
                                Color c = Color.FromArgb(255, sc1.StagePalette.Colors[h][w].R, sc1.StagePalette.Colors[h][w].G, sc1.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        break;
                    case 1:
                        sc2 = new RSDKv1.StageConfig(dlg.FileName);

                        for (int h = 0; h < 2; h++)// For six rows...
                        {
                            for (int w = 0; w < 16; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, sc2.StagePalette.Colors[h][w].R, sc2.StagePalette.Colors[h][w].G, sc2.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        break;
                    case 2:
                        sc3 = new RSDKv2.StageConfig(dlg.FileName);

                        for (int h = 0; h < 2; h++)// For six rows...
                        {
                            for (int w = 0; w < 16; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, sc3.StagePalette.Colors[h][w].R, sc3.StagePalette.Colors[h][w].G, sc3.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        break;
                    case 3:
                        sc4 = new RSDKvB.StageConfig(dlg.FileName);

                        for (int h = 0; h < 2; h++)// For six rows...
                        {
                            for (int w = 0; w < 16; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, sc4.StagePalette.Colors[h][w].R, sc4.StagePalette.Colors[h][w].G, sc4.StagePalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        break;
                    case 4:
                        gc4 = new RSDKvB.GameConfig(dlg.FileName);

                        for (int h = 0; h < 6; h++)// For six rows...
                        {
                            for (int w = 0; w < gc4.MasterPalette.COLORS_PER_COLUMN; w++) //Get 16 Colours
                            {
                                Color c = Color.FromArgb(255, gc4.MasterPalette.Colors[h][w].R, gc4.MasterPalette.Colors[h][w].G, gc4.MasterPalette.Colors[h][w].B);
                                pal.Add(c); //And place them into the grid!
                            }
                        }
                        break;
                    case 5:
                        pal = Cyotek.Windows.Forms.ColorCollection.LoadPalette(dlg.FileName); //Load .act file
                        break;
                    default:
                        break;
                }
                Text = Path.GetFileName(dlg.FileName) + " - RSDK Palette Editor";

                Parent.rp.state = "RetroED - " + this.Text;
                Parent.rp.details = "Editing: " + System.IO.Path.GetFileName(dlg.FileName);
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
                switch (dlg.FilterIndex - 1)
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
                                sc1.StagePalette.Colors[h][w].R = c.R;
                                sc1.StagePalette.Colors[h][w].G = c.G;
                                sc1.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        sc1.Write(dlg.FileName); //Save that!
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
                                sc2.StagePalette.Colors[h][w].R = c.R;
                                sc2.StagePalette.Colors[h][w].G = c.G;
                                sc2.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        sc2.Write(dlg.FileName); //Save that!
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
                                sc3.StagePalette.Colors[h][w].R = c.R;
                                sc3.StagePalette.Colors[h][w].G = c.G;
                                sc3.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        sc3.Write(dlg.FileName); //Save that!
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
                                sc4.StagePalette.Colors[h][w].R = c.R;
                                sc4.StagePalette.Colors[h][w].G = c.G;
                                sc4.StagePalette.Colors[h][w].B = c.B;
                            }
                        }
                        sc3.Write(dlg.FileName); //Save that!
                        break;
                    case 4:
                        int i4 = 0;
                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < gc4.MasterPalette.COLORS_PER_COLUMN; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i4].R, colorGrid.Colors[i4].G, colorGrid.Colors[i4].B);
                                i4++;
                                //Set the colours in the Stage Config to the modified colours in the ColourGrid!
                                gc4.MasterPalette.Colors[h][w].R = c.R;
                                gc4.MasterPalette.Colors[h][w].G = c.G;
                                gc4.MasterPalette.Colors[h][w].B = c.B;
                            }
                        }
                        gc4.Write(dlg.FileName); //Save that!
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
                MessageBox.Show("Error: " + Ex.Message + Environment.NewLine + "(This likely means you don't have 'Soniccd.exe' open!)");
            }
        }

        private void showPaletteRotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ShowPalRotations)
            {
                showPaletteRotationsToolStripMenuItem.Checked = ShowPalRotations = true;
                palTimer.Start();
            }
            else
            {
                showPaletteRotationsToolStripMenuItem.Checked = ShowPalRotations = false;
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
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message + Environment.NewLine + "(This likely means you don't have 'RSonic.exe', 'Nexus.exe' or 'Soniccd.exe' open!)");
                }
            }
        }

        #endregion

    }
}
