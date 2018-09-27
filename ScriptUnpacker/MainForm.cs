using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ScriptUnpacker
{
    public partial class MainForm : Form
    {

        public int RSDKver = 0;
        public string DataFolderPath;

        RSDKv2.Bytecode bytecode;

        public MainForm()
        {
            InitializeComponent();
        }

        private void RSDKverBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RSDKverBox.SelectedIndex >= 0)
            {
                RSDKver = RSDKverBox.SelectedIndex;
            }
        }

        private void SelectDataFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataFolderPath = dlg.SelectedPath;
            }
        }

        private void UnpackButton_Click(object sender, EventArgs e)
        {
            switch(RSDKver)
            {
                case 0:
                    UnpackDataFolderV2(DataFolderPath);
                    break;
                case 1:
                    UnpackDataFolderVB(DataFolderPath);
                    break;
            }
        }

        public void UnpackDataFolderV2(string datafolderpath)
        {
            RSDKv2.GameConfig gc = new RSDKv2.GameConfig(datafolderpath + "//Game//Gameconfig.bin");
            DirectoryInfo dir = new DirectoryInfo("Scripts");
            dir.Create();
            for (int i = 0; i < gc.SourceTxtLocations.Count; i++)
            {
                if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(gc.SourceTxtLocations[i])))
                {
                    Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(gc.SourceTxtLocations[i]));
                }
                if (!File.Exists("Scripts//" + gc.SourceTxtLocations[i]))
                {
                    File.CreateText("Scripts//" + gc.SourceTxtLocations[i]);
                }
            }

            foreach (RSDKv2.GameConfig.SceneGroup sg in gc.Stages.Scenes)
            {
                foreach (RSDKv2.GameConfig.SceneInfo si in sg.Scenes)
                {
                    RSDKv2.StageConfig sc = new RSDKv2.StageConfig(datafolderpath + "//Stages//" + si.SceneFolder + "//Stageconfig.bin");

                    for (int i = 0; i < sc.SourceTxtLocations.Count; i++)
                    {
                        if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(sc.SourceTxtLocations[i])))
                        {
                            Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(sc.SourceTxtLocations[i]));
                        }
                        if (!File.Exists("Scripts//" + sc.SourceTxtLocations[i]))
                        {
                            File.CreateText("Scripts//" + sc.SourceTxtLocations[i]);
                        }
                    }
                }
            }

        }

        public void UnpackDataFolderVB(string datafolderpath)
        {
            RSDKvB.GameConfig gc = new RSDKvB.GameConfig(datafolderpath + "//Game//Gameconfig.bin");
            DirectoryInfo dir = new DirectoryInfo("Scripts");
            dir.Create();
            for (int i = 0; i < gc.SourceTxtLocations.Count; i++)
            {
                if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(gc.SourceTxtLocations[i])))
                {
                    Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(gc.SourceTxtLocations[i]));
                }
                if (!File.Exists("Scripts//" + gc.SourceTxtLocations[i]))
                {
                    File.CreateText("Scripts//" + gc.SourceTxtLocations[i]);
                }
            }

            foreach (RSDKvB.GameConfig.SceneGroup sg in gc.Stages.Scenes)
            {
                foreach (RSDKvB.GameConfig.SceneInfo si in sg.Scenes)
                {
                    RSDKvB.StageConfig sc = new RSDKvB.StageConfig(datafolderpath + "//Stages//" + si.SceneFolder + "//Stageconfig.bin");

                    for (int i = 0; i < sc.SourceTxtLocations.Count; i++)
                    {
                        if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(sc.SourceTxtLocations[i])))
                        {
                            Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(sc.SourceTxtLocations[i]));
                        }
                        if (!File.Exists("Scripts//" + sc.SourceTxtLocations[i]))
                        {
                            File.CreateText("Scripts//" + sc.SourceTxtLocations[i]);
                        }
                    }
                }
            }

        }

        private void BytecodeButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                RSDKv2.GameConfig gc = new RSDKv2.GameConfig(dlg.SelectedPath + "//Game//Gameconfig.bin");
                bytecode = new RSDKv2.Bytecode(new RSDKv2.Reader(dlg.SelectedPath + "//Scripts//Bytecode//GS000.bin"), gc.ObjectsNames.Count);

                bytecode.sourceNames = new string[gc.SourceTxtLocations.Count + 1];
                bytecode.typeNames = new string[gc.ObjectsNames.Count+1];

                bytecode.sourceNames[0] = "BlankObject";
                bytecode.typeNames[0] = "BlankObject";

                for (int i = 1; i < gc.ObjectsNames.Count+1; i++)
                {
                    bytecode.sourceNames[i] = gc.SourceTxtLocations[i - 1];
                    bytecode.typeNames[i] = gc.ObjectsNames[i-1];
                }
            }
        }

        private void UnpackBCFileButton_Click(object sender, EventArgs e)
        {
            bytecode.Decompile();
            //FolderBrowserDialog dlg = new FolderBrowserDialog();

            //if (dlg.ShowDialog)
        }
    }
}
