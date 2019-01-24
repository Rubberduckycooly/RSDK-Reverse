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

        public List<RSDKv2.Bytecode> bytecodev2 = new List<RSDKv2.Bytecode>();
        public List<RSDKvB.Bytecode> bytecodevB = new List<RSDKvB.Bytecode>();
        public List<RSDKvRS.Script> bytecodevRS = new List<RSDKvRS.Script>();

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

                switch (RSDKver)
                {
                    case 0:
                        UnpackDataFolderV2(DataFolderPath);
                        break;
                    case 1:
                        UnpackDataFolderVB(DataFolderPath + "..//");
                        break;
                }

            }
        }

        private void UnpackButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                switch (RSDKver)
                {
                    case 0:
                        ExtractV2(dlg.SelectedPath);
                        break;
                    case 1:
                        ExtractVB(dlg.SelectedPath);
                        break;
                }
            }
        }

        public void UnpackDataFolderV2(string datafolderpath)
        {
            RSDKv2.GameConfig gc = new RSDKv2.GameConfig(datafolderpath + "//Game//Gameconfig.bin");
            DirectoryInfo dir = new DirectoryInfo("Scripts");
            dir.Create();
            bytecodev2.Clear();
            for (int i = 0; i < gc.ScriptPaths.Count; i++)
            {
                if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(gc.ScriptPaths[i])))
                {
                    Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(gc.ScriptPaths[i]));
                }
                if (!File.Exists("Scripts//" + gc.ScriptPaths[i]))
                {
                    File.CreateText("Scripts//" + gc.ScriptPaths[i]);
                }
            }

            foreach (RSDKv2.GameConfig.Category sg in gc.Categories)
            {
                foreach (RSDKv2.GameConfig.Category.SceneInfo si in sg.Scenes)
                {
                    bool filefound = true;

                    filefound = File.Exists(datafolderpath + "//Stages//" + si.SceneFolder + "//Stageconfig.bin");

                    if (filefound)
                    {
                        RSDKv2.StageConfig sc = new RSDKv2.StageConfig(datafolderpath + "//Stages//" + si.SceneFolder + "//Stageconfig.bin");

                        for (int i = 0; i < sc.ScriptPaths.Count; i++)
                        {
                            if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(sc.ScriptPaths[i])))
                            {
                                Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(sc.ScriptPaths[i]));
                            }
                            if (!File.Exists("Scripts//" + sc.ScriptPaths[i]))
                            {
                                File.CreateText("Scripts//" + sc.ScriptPaths[i]);
                            }
                        }
                    }
                }
            }

            RSDKv2.GameConfig gcv2 = new RSDKv2.GameConfig(datafolderpath + "//Game//Gameconfig.bin");

            string GlobalPath = datafolderpath + "//Scripts//Bytecode//GS000.bin";

            bool MobileVer = false;

            if (!File.Exists(GlobalPath))
            {
                GlobalPath = datafolderpath + "//Scripts//Bytecode//GlobalCode.bin";
                MobileVer = true;
            }

            RSDKv2.Bytecode GlobalCode = new RSDKv2.Bytecode(new RSDKv2.Reader(GlobalPath), 1,MobileVer);

            GlobalCode.sourceNames = new string[gcv2.ScriptPaths.Count + 1];
            GlobalCode.typeNames = new string[gcv2.ObjectsNames.Count + 1];

            GlobalCode.sourceNames[0] = "BlankObject";
            GlobalCode.typeNames[0] = "BlankObject";

            for (int i = 0; i < gcv2.GlobalVariables.Count; i++)
            {
                GlobalCode.globalVariableNames[i] = gcv2.GlobalVariables[i].Name;
            }

            for (int i = 1; i < gcv2.ObjectsNames.Count + 1; i++)
            {
                GlobalCode.sourceNames[i] = gcv2.ScriptPaths[i - 1];
                GlobalCode.typeNames[i] = Path.GetFileNameWithoutExtension(gcv2.ScriptPaths[i - 1]);
            }

            bytecodev2.Add(GlobalCode);

            for (int c = 0; c < gcv2.Categories.Count; c++)
            {
                for (int s = 0; s < gcv2.Categories[c].Scenes.Count; s++)
                {
                    string BytecodeName = "";
                    if (!MobileVer)
                    {
                        switch (c)
                        {
                            case 0:
                                if (s <= 9)
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "PS00" + s + ".bin";
                                }
                                else
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "PS0" + s + ".bin";
                                }
                                break;
                            case 1:
                                if (s <= 9)
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "RS00" + s + ".bin";
                                }
                                else
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "RS0" + s + ".bin";
                                }
                                break;
                            case 2:
                                if (s <= 9)
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "SS00" + s + ".bin";
                                }
                                else
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "SS0" + s + ".bin";
                                }
                                break;
                            case 3:
                                if (s <= 9)
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "BS00" + s + ".bin";
                                }
                                else
                                {
                                    BytecodeName = datafolderpath + "//Scripts//Bytecode//" + "BS0" + s + ".bin";
                                }
                                break;
                        }
                    }
                    else
                    {
                        BytecodeName = datafolderpath + "//Scripts//Bytecode//" + gcv2.Categories[c].Scenes[s].SceneFolder + ".bin";
                    }

                    bool filefound = true;

                    filefound = File.Exists(DataFolderPath + "//Stages//" + gc.Categories[c].Scenes[s].SceneFolder + "//Stageconfig.bin");

                    if (filefound)
                    {
                        filefound = File.Exists(BytecodeName);
                    }

                    if (filefound)
                    {
                        RSDKv2.StageConfig scv2 = new RSDKv2.StageConfig(DataFolderPath + "//Stages//" + gc.Categories[c].Scenes[s].SceneFolder + "//Stageconfig.bin");

                        RSDKv2.Bytecode bytecode = new RSDKv2.Bytecode(new RSDKv2.Reader(BytecodeName), gcv2.ObjectsNames.Count + 1,MobileVer);

                        if (scv2.LoadGlobalScripts)
                        {
                            bytecode = new RSDKv2.Bytecode(new RSDKv2.Reader(GlobalPath), 1, MobileVer);
                            bytecode.LoadStageBytecodeData(new RSDKv2.Reader(BytecodeName), gcv2.ObjectsNames.Count + 1, MobileVer);

                            bytecode.sourceNames = new string[gcv2.ObjectsNames.Count + scv2.ScriptPaths.Count + 1];
                            bytecode.typeNames = new string[gcv2.ObjectsNames.Count + scv2.ObjectsNames.Count + 1];

                            bytecode.sourceNames[0] = "BlankObject";
                            bytecode.typeNames[0] = "BlankObject";

                            for (int i = 1; i < gcv2.GlobalVariables.Count; i++)
                            {
                                bytecode.globalVariableNames[i] = gcv2.GlobalVariables[i].Name;
                            }

                            int ID = 1;

                            for (int i = 0; i < gcv2.ObjectsNames.Count; i++)
                            {
                                bytecode.sourceNames[ID] = gcv2.ScriptPaths[i];
                                //bytecode.typeNames[ID] = gcv2.ObjectsNames[i];
                                bytecode.typeNames[ID] = Path.GetFileNameWithoutExtension(gcv2.ScriptPaths[i]);
                                ID++;
                            }

                            for (int i = 0; i < scv2.ObjectsNames.Count; i++)
                            {
                                bytecode.sourceNames[ID] = scv2.ScriptPaths[i];
                                //bytecode.typeNames[ID] = scv2.ObjectsNames[i];
                                bytecode.typeNames[ID] = Path.GetFileNameWithoutExtension(scv2.ScriptPaths[i]);
                                ID++;
                            }
                        }
                        else
                        {
                            bytecode = new RSDKv2.Bytecode(new RSDKv2.Reader(BytecodeName), 1, MobileVer);

                            bytecode.sourceNames = new string[scv2.ScriptPaths.Count + 1];
                            bytecode.typeNames = new string[scv2.ObjectsNames.Count + 1];

                            bytecode.sourceNames[0] = "BlankObject";
                            bytecode.typeNames[0] = "BlankObject";

                            for (int i = 1; i < gcv2.GlobalVariables.Count; i++)
                            {
                                bytecode.globalVariableNames[i] = gcv2.GlobalVariables[i].Name;
                            }

                            int ID = 1;

                            for (int i = 0; i < scv2.ObjectsNames.Count; i++)
                            {
                                bytecode.sourceNames[ID] = scv2.ScriptPaths[i];
                                bytecode.typeNames[ID] = Path.GetFileNameWithoutExtension(scv2.ScriptPaths[i]);
                                ID++;
                            }
                        }

                        bytecodev2.Add(bytecode);
                    }
                }
            }
        }

        public void UnpackDataFolderVB(string datafolderpath)
        {
            RSDKvB.GameConfig gc = new RSDKvB.GameConfig(datafolderpath + "//Game//Gameconfig.bin");
            DirectoryInfo dir = new DirectoryInfo("Scripts");
            dir.Create();
            bytecodevB.Clear();
            for (int i = 0; i < gc.ScriptPaths.Count; i++)
            {
                if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(gc.ScriptPaths[i])))
                {
                    Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(gc.ScriptPaths[i]));
                }
                if (!File.Exists("Scripts//" + gc.ScriptPaths[i]))
                {
                    File.CreateText("Scripts//" + gc.ScriptPaths[i]);
                }
            }

            foreach (RSDKvB.GameConfig.Category sg in gc.Categories)
            {
                foreach (RSDKvB.GameConfig.Category.SceneInfo si in sg.Scenes)
                {
                    RSDKvB.StageConfig sc = new RSDKvB.StageConfig(datafolderpath + "//Stages//" + si.SceneFolder + "//Stageconfig.bin");

                    for (int i = 0; i < sc.ScriptPaths.Count; i++)
                    {
                        if (!Directory.Exists("Scripts//" + Path.GetDirectoryName(sc.ScriptPaths[i])))
                        {
                            Directory.CreateDirectory("Scripts//" + Path.GetDirectoryName(sc.ScriptPaths[i]));
                        }
                        if (!File.Exists("Scripts//" + sc.ScriptPaths[i]))
                        {
                            File.CreateText("Scripts//" + sc.ScriptPaths[i]);
                        }
                    }
                }
            }

            RSDKvB.GameConfig gcvB = new RSDKvB.GameConfig(datafolderpath + "//Game//Gameconfig.bin");

            RSDKvB.Bytecode GlobalCode = new RSDKvB.Bytecode(new RSDKvB.Reader(datafolderpath + "..//Bytecode//GlobalCode.bin"), 1);

            GlobalCode.sourceNames = new string[gcvB.ScriptPaths.Count + 1];
            GlobalCode.typeNames = new string[gcvB.ObjectsNames.Count + 1];

            GlobalCode.sourceNames[0] = "BlankObject";
            GlobalCode.typeNames[0] = "BlankObject";

            for (int i = 0; i < gcvB.GlobalVariables.Count; i++)
            {
                GlobalCode.globalVariableNames[i] = gcvB.GlobalVariables[i].Name;
            }

            for (int i = 1; i < gcvB.ObjectsNames.Count + 1; i++)
            {
                GlobalCode.sourceNames[i] = gcvB.ScriptPaths[i - 1];
                GlobalCode.typeNames[i] = gcvB.ObjectsNames[i - 1];
            }

            bytecodevB.Add(GlobalCode);

            for (int c = 0; c < gcvB.Categories.Count; c++)
            {
                for (int s = 0; s < gcvB.Categories[c].Scenes.Count; s++)
                {
                    string BytecodeName = "";
                    BytecodeName = datafolderpath + "..//Bytecode//" + gcvB.Categories[c].Scenes[s].SceneFolder + ".bin";

                    RSDKvB.StageConfig scvB = new RSDKvB.StageConfig(DataFolderPath + "//Stages//" + gc.Categories[c].Scenes[s].SceneFolder + "//Stageconfig.bin");

                    RSDKvB.Bytecode bytecode = new RSDKvB.Bytecode(new RSDKvB.Reader(BytecodeName), gcvB.ObjectsNames.Count + 1);

                    if (scvB.LoadGlobalScripts)
                    {
                        bytecode = new RSDKvB.Bytecode(new RSDKvB.Reader(datafolderpath + "..//Bytecode//GlobalCode.bin"), 1);
                        bytecode.LoadStageBytecodeData(new RSDKvB.Reader(BytecodeName), gcvB.ObjectsNames.Count + 1);

                        bytecode.sourceNames = new string[gcvB.ScriptPaths.Count + scvB.ScriptPaths.Count + 1];
                        bytecode.typeNames = new string[gcvB.ObjectsNames.Count + scvB.ObjectsNames.Count + 1];

                        bytecode.sourceNames[0] = "BlankObject";
                        bytecode.typeNames[0] = "BlankObject";

                        for (int i = 1; i < gcvB.GlobalVariables.Count; i++)
                        {
                            bytecode.globalVariableNames[i] = gcvB.GlobalVariables[i].Name;
                        }

                        int ID = 1;

                        for (int i = 0; i < gcvB.ObjectsNames.Count; i++)
                        {
                            bytecode.sourceNames[ID] = gcvB.ScriptPaths[i];
                            bytecode.typeNames[ID] = gcvB.ObjectsNames[i];
                            ID++;
                        }

                        for (int i = 0; i < scvB.ObjectsNames.Count; i++)
                        {
                            bytecode.sourceNames[ID] = scvB.ScriptPaths[i];
                            bytecode.typeNames[ID] = scvB.ObjectsNames[i];
                            ID++;
                        }

                        bytecodevB.Add(bytecode);
                    }
                    else
                    {
                        bytecode = new RSDKvB.Bytecode(new RSDKvB.Reader(BytecodeName), 1);

                        bytecode.sourceNames = new string[scvB.ScriptPaths.Count + 1];
                        bytecode.typeNames = new string[scvB.ObjectsNames.Count + 1];

                        bytecode.sourceNames[0] = "BlankObject";
                        bytecode.typeNames[0] = "BlankObject";

                        for (int i = 1; i < gcvB.GlobalVariables.Count; i++)
                        {
                            bytecode.globalVariableNames[i] = gcvB.GlobalVariables[i].Name;
                        }

                        int ID = 1;

                        for (int i = 0; i < scvB.ObjectsNames.Count; i++)
                        {
                            bytecode.sourceNames[ID] = scvB.ScriptPaths[i];
                            bytecode.typeNames[ID] = scvB.ObjectsNames[i];
                            ID++;
                        }

                        bytecodevB.Add(bytecode);
                    }
                }
            }
        }

        public void ExtractV2(string folderpath = "")
        {
            for (int i = 0; i < bytecodev2.Count; i++)
            {
                //try
                //{
                    bytecodev2[i].Decompile(folderpath);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
            }
        }

        public void ExtractVB(string folderpath = "")
        {
            for (int i = 0; i < bytecodevB.Count; i++)
            {
                try
                {
                    bytecodevB[i].Decompile(folderpath);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void BytecodeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Data Folder";
            dlg.Filter = "RSDK Bytecode Files|*.bin|Retro-Sonic Scripts|*.rsf";


            OpenFileDialog dlg2 = new OpenFileDialog();
            dlg2.Title = "Select Gameconfig/Stageconfig Files";

            RSDKvB.GameConfig gcvB = new RSDKvB.GameConfig();
            RSDKv2.GameConfig gcv2 = new RSDKv2.GameConfig();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string tmp = Path.GetFileNameWithoutExtension(dlg.FileName);
                bool IsGlobal = tmp == "GS000" || tmp == "GlobalCode";

                bool mobile = tmp == "GlobalCode";

                if (IsGlobal)
                {
                    dlg2.Filter = "RSDK Gameconfig Files|Gameconfig*.bin";
                }
                else
                {
                    dlg2.Filter = "RSDK Stageconfig Files|Stageconfig*.bin";
                }

                if (dlg2.ShowDialog(this) == DialogResult.OK && RSDKver != 2)
                {

                    if (!IsGlobal && RSDKver != 2)
                    {
                        OpenFileDialog dlg3 = new OpenFileDialog();
                        dlg3.Title = "Select Gameconfig Files|Gameconfig*.bin";

                        if (dlg3.ShowDialog(this) == DialogResult.OK)
                        {
                            switch(RSDKver)
                            {
                                case 0:
                                    gcv2 = new RSDKv2.GameConfig(dlg3.FileName);
                                    break;
                                case 1:
                                    gcvB = new RSDKvB.GameConfig(dlg3.FileName);
                                    break;
                                case 2:
                                    break;
                            }
                        }

                    }

                    switch (RSDKver)
                    {
                        case 0:
                            bytecodev2.Clear();

                            switch(IsGlobal)
                            {
                                case true:
                                    gcv2 = new RSDKv2.GameConfig(dlg2.FileName);
                                    bytecodev2.Add(new RSDKv2.Bytecode(new RSDKv2.Reader(dlg.FileName), 1,mobile));

                                    bytecodev2[0].sourceNames = new string[gcv2.ScriptPaths.Count + 1];
                                    bytecodev2[0].typeNames = new string[gcv2.ObjectsNames.Count + 1];

                                    bytecodev2[0].sourceNames[0] = "BlankObject";
                                    bytecodev2[0].typeNames[0] = "BlankObject";

                                    for (int i = 0; i < gcv2.GlobalVariables.Count; i++)
                                    {
                                        bytecodev2[0].globalVariableNames[i] = gcv2.GlobalVariables[i].Name;
                                    }

                                    for (int i = 1; i < gcv2.ObjectsNames.Count + 1; i++)
                                    {
                                        bytecodev2[0].sourceNames[i] = gcv2.ScriptPaths[i - 1];
                                        bytecodev2[0].typeNames[i] = gcv2.ObjectsNames[i - 1];
                                    }
                                    break;
                                case false:

                                    string fle = Path.GetFileName(dlg.FileName);
                                    string dir = dlg.FileName.Replace(fle, "");
                                    string globalcode = dir + "//GS000.bin";
                                    if (!File.Exists(globalcode))
                                    {
                                        globalcode = dir + "//GlobalCode.bin";
                                        if (!File.Exists(globalcode))
                                        {
                                            return;
                                        }
                                    }

                                    RSDKv2.StageConfig scv2 = new RSDKv2.StageConfig(dlg2.FileName);
                                    RSDKv2.Bytecode SCbytecode = new RSDKv2.Bytecode(new RSDKv2.Reader(globalcode), 1, mobile);
                                    SCbytecode.LoadStageBytecodeData(new RSDKv2.Reader(dlg.FileName), gcv2.ScriptPaths.Count + 1,mobile);
                                    bytecodev2.Add(SCbytecode);

                                    bytecodev2[0].sourceNames = new string[gcv2.ScriptPaths.Count + scv2.ScriptPaths.Count + 1];
                                    bytecodev2[0].typeNames = new string[gcv2.ScriptPaths.Count + scv2.ObjectsNames.Count + 1];

                                    bytecodev2[0].sourceNames[0] = "BlankObject";
                                    bytecodev2[0].typeNames[0] = "BlankObject";

                                    for (int i = 1; i < gcv2.GlobalVariables.Count; i++)
                                    {
                                        bytecodev2[0].globalVariableNames[i] = gcv2.GlobalVariables[i].Name;
                                    }

                                    int ID = 1;

                                    for (int i = 0; i < gcv2.ObjectsNames.Count; i++)
                                    {
                                        bytecodev2[0].sourceNames[ID] = gcv2.ScriptPaths[i];
                                        bytecodev2[0].typeNames[ID] = gcv2.ObjectsNames[i];
                                        ID++;
                                    }

                                    for (int i = 0; i < scv2.ObjectsNames.Count; i++)
                                    {
                                        bytecodev2[0].sourceNames[ID] = scv2.ScriptPaths[i];
                                        bytecodev2[0].typeNames[ID] = scv2.ObjectsNames[i];
                                        ID++;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            bytecodevB.Clear();

                            switch (IsGlobal)
                            {
                                case true:
                                    gcvB = new RSDKvB.GameConfig(dlg2.FileName);
                                    bytecodevB.Add(new RSDKvB.Bytecode(new RSDKvB.Reader(dlg.FileName), 1));

                                    bytecodevB[0].sourceNames = new string[gcvB.ScriptPaths.Count + 1];
                                    bytecodevB[0].typeNames = new string[gcvB.ObjectsNames.Count + 1];

                                    bytecodevB[0].sourceNames[0] = "BlankObject";
                                    bytecodevB[0].typeNames[0] = "BlankObject";

                                    for (int i = 1; i < gcvB.GlobalVariables.Count; i++)
                                    {
                                        bytecodevB[0].globalVariableNames[i] = gcvB.GlobalVariables[i].Name;
                                    }

                                    for (int i = 1; i < gcv2.ObjectsNames.Count + 1; i++)
                                    {
                                        bytecodevB[0].sourceNames[i] = gcvB.ScriptPaths[i - 1];
                                        bytecodevB[0].typeNames[i] = gcvB.ObjectsNames[i - 1];
                                    }
                                    break;
                                case false:

                                    RSDKv2.StageConfig scvB = new RSDKv2.StageConfig(dlg2.FileName);
                                    bytecodevB.Add(new RSDKvB.Bytecode(new RSDKvB.Reader(dlg.FileName), gcvB.ScriptPaths.Count + 1));

                                    bytecodev2[0].sourceNames = new string[gcvB.ScriptPaths.Count + scvB.ScriptPaths.Count + 1];
                                    bytecodev2[0].typeNames = new string[gcvB.ScriptPaths.Count + scvB.ObjectsNames.Count + 1];

                                    bytecodev2[0].sourceNames[0] = "BlankObject";
                                    bytecodev2[0].typeNames[0] = "BlankObject";

                                    for (int i = 0; i < gcvB.GlobalVariables.Count; i++)
                                    {
                                        bytecodevB[0].globalVariableNames[i] = gcv2.GlobalVariables[i].Name;
                                    }

                                    int ID = 1;

                                    for (int i = 0; i < gcvB.ObjectsNames.Count; i++)
                                    {
                                        bytecodevB[0].sourceNames[ID] = gcvB.ScriptPaths[i];
                                        bytecodevB[0].typeNames[ID] = gcvB.ObjectsNames[i];
                                        ID++;
                                    }

                                    for (int i = 0; i < scvB.ObjectsNames.Count; i++)
                                    {
                                        bytecodevB[0].sourceNames[ID] = scvB.ScriptPaths[i];
                                        bytecodevB[0].typeNames[ID] = scvB.ObjectsNames[i];
                                        ID++;
                                    }
                                    break;
                            }
                            break;
                        case 2:
                            //Retro-Sonic Stuff
                            break;
                    }
                }
            }
        }

        private void UnpackBCFileButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select a folder to export the scripts to!";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                switch (RSDKver)
                {
                    case 0:
                        bytecodev2[0].Decompile(dlg.SelectedPath);
                        break;
                    case 1:
                        bytecodevB[0].Decompile(dlg.SelectedPath);
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}
