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
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.GameconfigEditors.RSDKvBGameconfigEditor
{
    public partial class MainForm : DockContent
    {
        public RSDKvB.GameConfig gameconfig = new RSDKvB.GameConfig();

        public int CurCategory = 0;
        public int CurStage = 0;
        public int CurObj = 0;
        public int CurSfx = 0;
        public int CurPlr = 0;
        public int CurVar = 0;

        public List<string> ActNumList = new List<string>();

        string FILEPATH;

        char buf;

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
            gameconfig = new RSDKvB.GameConfig();
            FILEPATH = null;
            refreshLists();
            RefreshUI();
            RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig";

            string RSDK = "RSDKvB";
            string dispname = "";
            RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig";
            dispname = "New Gameconfig";

            Text = "New Stageconfig";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Gameconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
        }

        public void Open(string Filepath)
        {
            gameconfig = new RSDKvB.GameConfig(new RSDKvB.Reader(Filepath));
            FILEPATH = Filepath;
            RefreshUI();
            refreshLists();
            RetroED.MainForm.Instance.CurrentTabText = Path.GetFileName(Filepath);

            string RSDK = "RSDKvB";
            string dispname = "";
            string folder = Path.GetDirectoryName(Filepath);
            DirectoryInfo di = new DirectoryInfo(folder);
            folder = di.Name;
            string file = Path.GetFileName(Filepath);

            if (Filepath != null)
            {
                RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                dispname = folder + "/" + file;
            }
            else
            {
                RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig - RSDK Gameconfig Editor";
                dispname = "New Stageconfig - RSDK Gameconfig Editor";
            }

            Text = Path.GetFileName(Filepath) + " - RSDK Gameconfig Editor";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Gameconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
        }

        public void Save(string Filepath)
        {
            Console.WriteLine(FILEPATH);
            gameconfig.Write(new RSDKvB.Writer(Filepath));
            FILEPATH = Filepath;
            RetroED.MainForm.Instance.CurrentTabText = Path.GetFileName(Filepath);

            string RSDK = "RSDKvB";
            string dispname = "";
            string folder = Path.GetDirectoryName(Filepath);
            DirectoryInfo di = new DirectoryInfo(folder);
            folder = di.Name;
            string file = Path.GetFileName(Filepath);

            if (Filepath != null)
            {
                RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                dispname = folder + "/" + file;
            }
            else
            {
                RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig - RSDK Gameconfig Editor";
                dispname = "New Stageconfig - RSDK Gameconfig Editor";
            }

            Text = Path.GetFileName(Filepath) + " - RSDK Gameconfig Editor";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Gameconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
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
                Save(FILEPATH);
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDKvB Gameconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKvB Gameconfig Files|Gameconfig*.bin";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                writeLineToConsole(dlg.FileName);
                FILEPATH = dlg.FileName;
                Open(dlg.FileName);             
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKvB Gameconfig Files|Gameconfig*.bin";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Save(dlg.FileName);
                Console.WriteLine("File Saved!");
            }

        }

        private void GameNameTxt_TextChanged(object sender, EventArgs e)
        {
            gameconfig.GameWindowText = GameNameTxt.Text; //When the text box is updated, update the gameName String as well!
        }

        private void textBox4_TextChanged(object sender, EventArgs e)//SubnameTxt Too lazy to fix the name
        {
            //gameconfig.GameSubname = SubNameTxt.Text; //When the text box is updated, update the gameName String as well!
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
            gameconfig.GameWindowText = GameNameTxt.Text; //When the text box is updated, update the gameName String as well!
        }

        public void RefreshUI()
        {
            GameNameTxt.Text = gameconfig.GameWindowText;
            AboutBox.Text = gameconfig.GameDescriptionText;

            if (gameconfig.Categories[CurCategory].Scenes.Count > 0)
            {
                StgNameBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].Name;
                StgFolderBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder;
                StgIDBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].ActID;
                StgUnknownNUD.Value = gameconfig.Categories[CurCategory].Scenes[CurStage].Unknown;
            }

            if (gameconfig.ScriptPaths.Count > 0)
            {
                ObjPathHashBox.Text = gameconfig.ScriptPaths[CurObj];
                ObjCFGBox.Text = gameconfig.ObjectsNames[CurObj];
            }

            if (gameconfig.SoundFX.Count > 0)
            {
                SFXPathBox.Text = gameconfig.SoundFX[CurSfx];
                SFXNameBox.Text = gameconfig.SfxNames[CurSfx];
            }

            if (gameconfig.Players.Count > 0)
            {
                PlayerNameBox.Text = gameconfig.Players[CurPlr];
            }

            if (gameconfig.GlobalVariables.Count > 0)
            {
                VariableNameBox.Text = gameconfig.GlobalVariables[CurVar].Name;
            }

        }

        void refreshStageList()
        {
            StageBox.Items.Clear();
            for (int i = 0; i < gameconfig.Categories[CurCategory].Scenes.Count; i++)
            {
                StageBox.Items.Add(gameconfig.Categories[CurCategory].Scenes[i].Name + ", " + gameconfig.Categories[CurCategory].Scenes[i].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[i].ActID + ", " + gameconfig.Categories[CurCategory].Scenes[i].Unknown);
            }
        }

        void refreshObjectList()
        {
            ObjListBox.Items.Clear();
            for (int i = 0; i < gameconfig.ObjectsNames.Count; i++)
            {
                ObjListBox.Items.Add(gameconfig.ObjectsNames[i]);
            }
        }

        void refreshSoundFXList()
        {
            SoundFXListBox.Items.Clear();
            for (int i = 0; i < gameconfig.SfxNames.Count; i++)
            {
                SoundFXListBox.Items.Add(gameconfig.SfxNames[i]);
            }
        }

        void refreshPlayerList()
        {
            PlayersListBox.Items.Clear();
            for (int i = 0; i < gameconfig.Players.Count; i++)
            {
                PlayersListBox.Items.Add(gameconfig.Players[i]);
            }
        }

        void refreshGlobalVariablesList()
        {
            VariableListBox.Items.Clear();
            for (int i = 0; i < gameconfig.GlobalVariables.Count; i++)
            {
                VariableListBox.Items.Add(gameconfig.GlobalVariables[i].Name);
            }
        }

        void refreshLists()
        {
            refreshObjectList();
            refreshStageList();
            refreshSoundFXList();
            refreshPlayerList();
            refreshGlobalVariablesList();
        }

        private void CategoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurCategory = CategoryListBox.SelectedIndex;
            CurStage = 0;
            RefreshUI();
            refreshStageList();
        }

        private void StgNameBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].Name = StgNameBox.Text;
            if (StageBox.Items.Count > 0)
            {
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Unknown;
            }
        }

        private void StgFolderBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder = StgFolderBox.Text;
            if (StageBox.Items.Count > 0)
            {
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Unknown;
            }
        }

        private void StgIDBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].ActID = StgIDBox.Text;
            if (StageBox.Items.Count > 0)
            {
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Unknown;
            }
        }

        private void StgUnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].Unknown = (byte)StgUnknownNUD.Value;
            if (StageBox.Items.Count > 0)
            {
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].Unknown;
            }
        }

        private void StageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StageBox.SelectedIndex >= 0)
            {
                CurStage = StageBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void AddStgButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes.Add(new RSDKvB.GameConfig.Category.SceneInfo());
            RefreshUI();
            refreshLists();
        }

        private void DelStgButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes.RemoveAt(CurStage);
            CurStage--;
            RefreshUI();
            refreshLists();
        }

        private void ClearStgButton_Click(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes = new List<RSDKvB.GameConfig.Category.SceneInfo>();
            gameconfig.Categories[CurCategory].Scenes.Add(new RSDKvB.GameConfig.Category.SceneInfo());
            CurStage = 0;
            RefreshUI();
            refreshLists();
        }

        private void ObjListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ObjListBox.SelectedIndex >= 0)
            {
                CurObj = ObjListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void AddObjButton_Click(object sender, EventArgs e)
        {
            string fname = "Folder/Object.txt";
            string oname = "New Object";
            gameconfig.ScriptPaths.Add(fname);
            gameconfig.ObjectsNames.Add(oname);
            refreshLists();
        }

        private void DelObjButton_Click(object sender, EventArgs e)
        {
            gameconfig.ScriptPaths.RemoveAt(CurObj);
            gameconfig.ObjectsNames.RemoveAt(CurObj);
            CurObj--;
            refreshLists();
        }

        private void AboutBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.GameDescriptionText = AboutBox.Text;
        }

        private void ObjPathBox_TextChanged(object sender, EventArgs e)
        {
            if (ObjListBox.Items.Count > 0)
            {
                gameconfig.ScriptPaths[CurObj] = ObjPathHashBox.Text;
            }
        }

        private void ObjCFGBox_TextChanged(object sender, EventArgs e)
        {
            if (ObjListBox.Items.Count > 0)
            {
                ObjListBox.Items[CurObj] = gameconfig.ObjectsNames[CurObj] = ObjCFGBox.Text;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDKvB Gameconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
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
            if (SoundFXListBox.SelectedIndex >= 0)
            {
                CurSfx = SoundFXListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void SFXNameBox_TextChanged(object sender, EventArgs e)
        {
            if (SoundFXListBox.Items.Count > 0)
            {
                gameconfig.SfxNames[CurSfx] = SFXNameBox.Text;
                SoundFXListBox.Items[CurSfx] = SFXNameBox.Text;
            }
        }

        private void SFXPathBox_TextChanged(object sender, EventArgs e)
        {
            if (SoundFXListBox.Items.Count > 0)
            {
                gameconfig.SoundFX[CurSfx] = SFXPathBox.Text;
            }
        }

        private void AddSFXButton_Click(object sender, EventArgs e)
        {
            string fname = "Folder/SoundFX.wav";
            string sname = "New SoundFX";
            gameconfig.SoundFX.Add(fname);
            gameconfig.SfxNames.Add(sname);
            refreshLists();
        }

        private void RemoveSFXButton_Click(object sender, EventArgs e)
        {
            if (SoundFXListBox.Items.Count > 0)
            {
                gameconfig.SfxNames.RemoveAt(CurSfx);
                gameconfig.SoundFX.RemoveAt(CurSfx);
                CurSfx--;
                refreshLists();
            }
        }

        private void PlayersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PlayersListBox.SelectedIndex >= 0)
            {
                CurPlr = PlayersListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void PlayerNameBox_TextChanged(object sender, EventArgs e)
        {
            if (PlayersListBox.Items.Count > 0)
            {
                gameconfig.Players[CurPlr] = PlayerNameBox.Text;
                PlayersListBox.Items[CurPlr] = PlayerNameBox.Text;
            }
        }

        private void AddPlrButton_Click(object sender, EventArgs e)
        {
            string pname = "Character";
            gameconfig.Players.Add(pname);
            refreshLists();
        }

        private void RemovePlrButton_Click(object sender, EventArgs e)
        {
            if (PlayersListBox.Items.Count > 0)
            {
                gameconfig.Players.RemoveAt(CurPlr);
                CurPlr--;
                refreshLists();
            }
        }

        private void VariableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VariableListBox.SelectedIndex >= 0)
            {
                CurVar = VariableListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void VariableNameBox_TextChanged(object sender, EventArgs e)
        {
            if (VariableListBox.Items.Count > 0)
            {
                gameconfig.GlobalVariables[CurVar].Name = VariableNameBox.Text;
                VariableListBox.Items[CurVar] = VariableNameBox.Text;
            }
        }

        private void AddVarButton_Click(object sender, EventArgs e)
        {
            RSDKvB.GameConfig.GlobalVariable variable = new RSDKvB.GameConfig.GlobalVariable("Variable");
            gameconfig.GlobalVariables.Add(variable);
            refreshLists();
        }

        private void RemoveVarButton_Click(object sender, EventArgs e)
        {
            if (VariableListBox.Items.Count > 0)
            {
                gameconfig.GlobalVariables.RemoveAt(CurPlr);
                CurVar--;
                refreshLists();
            }
        }
    }   
}
