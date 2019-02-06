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
using System.Media;

namespace ConfigManiac
{
    public partial class MainForm : Form
    {
        public RSDKv5.GameConfig gameconfig = new RSDKv5.GameConfig();
        public RSDKv5.StageConfig stageconfig = new RSDKv5.StageConfig();

        int type = 0;
        public string TypeStr
        {
            get
            {
                if (type == 0)
                {
                    return "Gameconfig";
                }
                else if (type == 1)
                {
                    return "Stageconfig";
                }
                return "";
            }
        }

        public int CurCategory = 0;
        public int CurStage = 0;
        public int CurObj = 0;
        public int CurSfx = 0;

        public List<string> ActNumList = new List<string>();

        string FILEPATH;

        public MainForm()
        {
            InitializeComponent();
            
            RefreshUI();
        }

        void writeLineToConsole(string line) //Printing stuff to the CMD
        {
            Console.WriteLine(line);
        }

        public void New()
        {
            switch(type)
            {
                case 0:
                    gameconfig = new RSDKv5.GameConfig();
                    break;
                case 1:
                    stageconfig = new RSDKv5.StageConfig();
                    break;
            }
            FILEPATH = null;
            refreshLists();
            RefreshUI();
        }

        public void Open(string Filepath,int type)
        {
            switch(type)
            {
                case 0:
                    gameconfig = new RSDKv5.GameConfig(new RSDKv5.Reader(Filepath));
                    break;
                case 1:
                    stageconfig = new RSDKv5.StageConfig(new RSDKv5.Reader(Filepath));
                    break;
                default:
                    break;
            }
            this.type = type;
            FILEPATH = Filepath;
            refreshLists();
            RefreshUI();
        }

        public void Save(string Filepath, int type)
        {
            Console.WriteLine(FILEPATH);
            switch (type)
            {
                case 0:
                    gameconfig.Write(new RSDKv5.Writer(Filepath));
                    break;
                case 1:
                    stageconfig.Write(new RSDKv5.Writer(Filepath));
                    break;
                default:
                    break;
            }
            this.type = type;
            FILEPATH = Filepath;
        }

        public string DecToHex(int DecVal)
        {
            string HEXOUT = "";// Define String?
            HEXOUT = System.String.Format("{0:x}", DecVal);//Convert Dec to Hex
            return HEXOUT;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FILEPATH != null)
            {
                Save(FILEPATH,type);
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "ConfigManiac - " + TypeStr, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            switch(ConfigManager.SelectedIndex)
            {
                case 0://Gameconfig editor
                    dlg.Filter = "RSDKv5 Gameconfig Files|Gameconfig*.bin";
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        writeLineToConsole(dlg.FileName);
                        FILEPATH = dlg.FileName;
                        Open(dlg.FileName, ConfigManager.SelectedIndex);
                    }
                    break;
                case 1://Stageconfig editor
                    dlg.Filter = "RSDKv5 Stageconfig Files|Stageconfig*.bin";
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        writeLineToConsole(dlg.FileName);
                        FILEPATH = dlg.FileName;
                        Open(dlg.FileName, ConfigManager.SelectedIndex);
                    }
                    break;
                default://what
                    break;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            switch (ConfigManager.SelectedIndex)
            {
                case 0:
                    dlg.Filter = "RSDKv5 Gameconfig Files|Gameconfig*.bin";
                    break;
                case 1:
                    dlg.Filter = "RSDKv5 Stageconfig Files|Stageconfig*.bin";
                    break;
                default:
                    break;
            }
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Save(dlg.FileName, ConfigManager.SelectedIndex);
                Console.WriteLine("File Saved!");
            }

        }

        private void GameNameTxt_TextChanged(object sender, EventArgs e)
        {
            gameconfig.GameName = GameNameTxt.Text; //When the text box is updated, update the gameName String as well!
        }

        private void textBox4_TextChanged(object sender, EventArgs e)//Subname Too lazy to fix the name
        {
            gameconfig.GameSubname = SubNameTxt.Text; //When the text box is updated, update the gameName String as well!
        }

        private void VersionBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Version = VersionBox.Text;
        }

        private void StartCatBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartCatBox.SelectedIndex >= 0)
            {
                gameconfig.StartSceneCategoryIndex = (byte)StartCatBox.SelectedIndex;
            }
        }

        private void StartScnBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartCatBox.SelectedIndex >= 0)
            {
                gameconfig.StartSceneIndex = (ushort)StartScnBox.SelectedIndex;
            }
        }

        private void StopMusButton_Click(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.Stop();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void RefreshUI()
        {
            GameNameTxt.Text = gameconfig.GameName;
            SubNameTxt.Text = gameconfig.GameSubname;
            VersionBox.Text = gameconfig.Version;

            if (gameconfig.Categories.Count > 0)
            {
                GCCatNameBox.Text = gameconfig.Categories[CurCategory].Name;
                StartCatBox.SelectedIndex = gameconfig.StartSceneCategoryIndex;

                StartScnBox.Items.Clear();
                if (gameconfig.Categories.Count > 0)
                {
                    for (int i = 0; i < gameconfig.Categories[CurCategory].Scenes.Count; i++)
                    {
                        StartScnBox.Items.Add(gameconfig.Categories[CurCategory].Scenes[i].Name);
                    }
                }

                if (gameconfig.Categories[CurCategory].Scenes.Count > 0)
                {
                    GCStgNameBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].Name;
                    GCStgFolderBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].Zone;
                    GCStgIDBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].SceneID;
                    GCStageModeNUD.Value = gameconfig.Categories[CurCategory].Scenes[CurStage].ModeFilter;
                    StartScnBox.SelectedIndex = gameconfig.StartSceneIndex;
                }
            }

            if (gameconfig.ObjectsNames.Count > 0)
            {
                GCObjCFGBox.Text = gameconfig.ObjectsNames[CurObj];
            }

            if (gameconfig.WAVs.Count > 0)
            {
                GCSFXPathBox.Text = gameconfig.WAVs[CurSfx].Name;
                GCMaxPlayNUD.Value = gameconfig.WAVs[CurSfx].MaxConcurrentPlay;
            }


            if (stageconfig.ObjectsNames.Count > 0)
            {
                SCObjectNameBox.Text = stageconfig.ObjectsNames[CurObj];
            }

            if (stageconfig.WAVs.Count > 0)
            {
                SCSFXPathBox.Text = stageconfig.WAVs[CurSfx].Name;
                SCSFXMaxPlayNUD.Value = stageconfig.WAVs[CurSfx].MaxConcurrentPlay;
            }

            LoadGlobalObjectsCB.Checked = stageconfig.LoadGlobalObjects;
        }

        void refreshStageList()
        {
            GCStageBox.Items.Clear();
            if (gameconfig.Categories.Count > 0)
            {
                for (int i = 0; i < gameconfig.Categories[CurCategory].Scenes.Count; i++)
                {
                    GCStageBox.Items.Add(gameconfig.Categories[CurCategory].Scenes[i].Name + ", " + gameconfig.Categories[CurCategory].Scenes[i].Zone + ", " + gameconfig.Categories[CurCategory].Scenes[i].SceneID + ", " + gameconfig.Categories[CurCategory].Scenes[i].ModeFilter);
                }
            }
        }

        void refreshCategoryList()
        {
            CategoryListBox.Items.Clear();
            StartCatBox.Items.Clear();
            for (int i = 0; i < gameconfig.Categories.Count; i++)
            {
                CategoryListBox.Items.Add(gameconfig.Categories[i].Name);
                StartCatBox.Items.Add(gameconfig.Categories[i].Name);
            }
        }

        void refreshObjectList()
        {
            GCObjListBox.Items.Clear();
            for (int i = 0; i < gameconfig.ObjectsNames.Count; i++)
            {
                GCObjListBox.Items.Add(gameconfig.ObjectsNames[i]);
            }
            SCObjectList.Items.Clear();
            for (int i = 0; i < stageconfig.ObjectsNames.Count; i++)
            {
                SCObjectList.Items.Add(stageconfig.ObjectsNames[i]);
            }
        }

        void refreshSoundFXList()
        {
            GCSoundFXListBox.Items.Clear();
            for (int i = 0; i < gameconfig.WAVs.Count; i++)
            {
                GCSoundFXListBox.Items.Add(gameconfig.WAVs[i].Name + ", " + gameconfig.WAVs[i].MaxConcurrentPlay);
            }
            SCSFXList.Items.Clear();
            for (int i = 0; i < stageconfig.WAVs.Count; i++)
            {
                SCSFXList.Items.Add(stageconfig.WAVs[i].Name + ", " + stageconfig.WAVs[i].MaxConcurrentPlay);
            }
        }

        void refreshLists()
        {
            refreshObjectList();
            refreshStageList();
            refreshCategoryList();
            refreshSoundFXList();
        }

        private void CategoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurCategory = CategoryListBox.SelectedIndex;
            if (CurCategory < 0) CurCategory = 0;
            CurStage = 0;
            RefreshUI();
            refreshStageList();
        }

        private void GCCatNameBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Name = GCCatNameBox.Text;
            if (StartCatBox.Items.Count > 0)
            {
                StartCatBox.Items[CurCategory] = GCCatNameBox.Text;
            }
        }

        private void StgNameBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].Name = GCStgNameBox.Text;
            if (GCStageBox.Items.Count > 0)
            {
                GCStageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Zone + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ModeFilter;
            }
        }

        private void StgFolderBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].Zone = GCStgFolderBox.Text;
            if (GCStageBox.Items.Count > 0)
            {
                GCStageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Zone + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ModeFilter;
            }
        }

        private void StgIDBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].SceneID = GCStgIDBox.Text;
            if (GCStageBox.Items.Count > 0)
            {
                GCStageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Zone + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ModeFilter;
            }
        }

        private void GCStageModeNUD_ValueChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].ModeFilter = (byte)GCStageModeNUD.Value;
            if (GCStageBox.Items.Count > 0)
            {
                GCStageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Zone + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ModeFilter;
            }
        }

        private void StageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GCStageBox.SelectedIndex >= 0)
            {
                CurStage = GCStageBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void AddStgButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes.Add(new RSDKv5.GameConfig.SceneInfo());
            RefreshUI();
            refreshLists();
        }

        private void DelStgButton_Click(object sender, EventArgs e)
        {
            if (gameconfig.Categories[CurCategory].Scenes.Count > 0)
            {
                gameconfig.Categories[CurCategory].Scenes.RemoveAt(CurStage);
                if (CurStage > 0) CurStage--;
                RefreshUI();
                refreshLists();
            }
        }

        private void ClearStgButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes = new List<RSDKv5.GameConfig.SceneInfo>();
            gameconfig.Categories[CurCategory].Scenes.Add(new RSDKv5.GameConfig.SceneInfo());
            CurStage = 0;
            RefreshUI();
            refreshLists();
        }

        private void ObjListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GCObjListBox.SelectedIndex >= 0)
            {
                CurObj = GCObjListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void AddObjButton_Click(object sender, EventArgs e)
        {
            string oname = "Blank Object";
            gameconfig.ObjectsNames.Add(oname);
            refreshLists();
        }

        private void DelObjButton_Click(object sender, EventArgs e)
        {
            gameconfig.ObjectsNames.RemoveAt(CurObj);
            if (CurObj > 0) CurObj--;
            refreshLists();
        }

        private void ObjCFGBox_TextChanged(object sender, EventArgs e)
        {
            if (GCObjListBox.Items.Count > 0)
            {
                GCObjListBox.Items[CurObj] = gameconfig.ObjectsNames[CurObj] = GCObjCFGBox.Text;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "ConfigManiac - " + TypeStr, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            New();
        }

        private void SoundFXListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GCSoundFXListBox.SelectedIndex >= 0)
            {
                CurSfx = GCSoundFXListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void SFXPathBox_TextChanged(object sender, EventArgs e)
        {
            if (GCSoundFXListBox.Items.Count > 0)
            {
                gameconfig.WAVs[CurSfx].Name = GCSFXPathBox.Text;
                GCSoundFXListBox.Items[CurSfx] = gameconfig.WAVs[CurSfx].Name + ", " + gameconfig.WAVs[CurSfx].MaxConcurrentPlay;
            }
        }

        private void GCMaxPlayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (GCSoundFXListBox.Items.Count > 0)
            {
                gameconfig.WAVs[CurSfx].MaxConcurrentPlay = (byte)GCMaxPlayNUD.Value;
                GCSoundFXListBox.Items[CurSfx] = gameconfig.WAVs[CurSfx].Name + ", " + gameconfig.WAVs[CurSfx].MaxConcurrentPlay;
            }
        }

        private void AddSFXButton_Click(object sender, EventArgs e)
        {
            string fname = "Folder/SoundFX.wav";
            RSDKv5.WAVConfiguration wc = new RSDKv5.WAVConfiguration();
            wc.Name = fname;
            gameconfig.WAVs.Add(wc);
            refreshLists();
        }

        private void RemoveSFXButton_Click(object sender, EventArgs e)
        {
            if (GCSoundFXListBox.Items.Count > 0)
            {
                gameconfig.WAVs.RemoveAt(CurSfx);
                if (CurSfx > 0) CurSfx--;
                refreshLists();
            }
        }

        private void SCObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCObjectList.SelectedIndex >= 0)
            {
                CurObj = SCObjectList.SelectedIndex;
                RefreshUI();
            }
        }

        private void SCObjectNameBox_TextChanged(object sender, EventArgs e)
        {
            if (SCObjectList.Items.Count > 0)
            {
                SCObjectList.Items[CurObj] = stageconfig.ObjectsNames[CurObj] = SCObjectNameBox.Text;
            }
        }

        private void SCAddObjButton_Click(object sender, EventArgs e)
        {
            string oname = "Blank Object";
            stageconfig.ObjectsNames.Add(oname);
            refreshLists();
        }

        private void SCDelObjButton_Click(object sender, EventArgs e)
        {
            stageconfig.ObjectsNames.RemoveAt(CurObj);
            if (CurObj > 0) CurObj--;
            refreshLists();
        }

        private void SCSFXList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCSFXList.SelectedIndex >= 0)
            {
                CurSfx = SCSFXList.SelectedIndex;
                RefreshUI();
            }
        }

        private void SCSFXPathBox_TextChanged(object sender, EventArgs e)
        {
            if (SCSFXList.Items.Count > 0)
            {
                stageconfig.WAVs[CurSfx].Name = SCSFXPathBox.Text;
                SCSFXList.Items[CurSfx] = stageconfig.WAVs[CurSfx].Name + ", " + stageconfig.WAVs[CurSfx].MaxConcurrentPlay;
            }
        }

        private void SCSFXMaxPlayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (SCSFXList.Items.Count > 0)
            {
                stageconfig.WAVs[CurSfx].MaxConcurrentPlay = (byte)SCSFXMaxPlayNUD.Value;
                SCSFXList.Items[CurSfx] = stageconfig.WAVs[CurSfx].Name + ", " + stageconfig.WAVs[CurSfx].MaxConcurrentPlay;
            }
        }

        private void SCAddSFXButton_Click(object sender, EventArgs e)
        {
            string fname = "Folder/SoundFX.wav";
            RSDKv5.WAVConfiguration wc = new RSDKv5.WAVConfiguration();
            wc.Name = fname;
            stageconfig.WAVs.Add(wc);
            refreshLists();
        }

        private void SCDelSFXButton_Click(object sender, EventArgs e)
        {
            if (SCSFXList.Items.Count > 0)
            {
                stageconfig.WAVs.RemoveAt(CurSfx);
                if (CurSfx > 0) CurSfx--;
                refreshLists();
            }
        }

        private void LoadGlobalObjectsCB_CheckedChanged(object sender, EventArgs e)
        {
            stageconfig.LoadGlobalObjects = LoadGlobalObjectsCB.Checked;
        }

        private void ConfigManager_TabIndexChanged(object sender, EventArgs e)
        {
            CurCategory = 0;
            CurSfx = 0;
            CurStage = 0;
            CurObj = 0;
            refreshLists();
            RefreshUI();
            type = ConfigManager.SelectedIndex;
            this.Text = "Config Manager - " + TypeStr;
        }

        private void AddCatButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories.Add(new RSDKv5.GameConfig.Category(gameconfig._scenesHaveModeFilter));
            RefreshUI();
            refreshLists();
        }

        private void DelCatButton_Click(object sender, EventArgs e)
        {
            if (gameconfig.Categories.Count > 0)
            {
                gameconfig.Categories.RemoveAt(CurSfx);
                if (CurCategory > 0) CurCategory--;
                CurStage = 0;
                RefreshUI();
                refreshLists();
            }
        }

        private void ClearCatButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories = new List<RSDKv5.GameConfig.Category>();
            gameconfig.Categories.Add(new RSDKv5.GameConfig.Category());
            CurStage = 0;
            CurCategory = 0;
            RefreshUI();
            refreshLists();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm dlg = new AboutForm();
            dlg.ShowDialog();
        }
    }   
}
