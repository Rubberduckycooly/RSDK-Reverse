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

namespace RetroED.Tools.GameconfigEditors.RSDKv1GameconfigEditor
{
    public partial class MainForm : Form
    {
        public RSDKv1.GameConfig gameconfig = new RSDKv1.GameConfig();

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
            gameconfig = new RSDKv1.GameConfig();
            FILEPATH = null;
            refreshLists();
            RefreshUI();
            RetroED.MainForm.Instance.TabControl.TabPages[RetroED.MainForm.Instance.TabControl.SelectedIndex].Text = "New Gameconfig";
        }

        public void Open(string Filepath)
        {
            gameconfig = new RSDKv1.GameConfig(new RSDKv1.Reader(Filepath));
            FILEPATH = Filepath;
            RefreshUI();
            refreshLists();
            RetroED.MainForm.Instance.TabControl.TabPages[RetroED.MainForm.Instance.TabControl.SelectedIndex].Text = Path.GetFileName(Filepath);
        }

        public void Save(string Filepath)
        {
            Console.WriteLine(FILEPATH);
            gameconfig.Write(new RSDKv1.Writer(Filepath));
            FILEPATH = Filepath;
            RetroED.MainForm.Instance.TabControl.TabPages[RetroED.MainForm.Instance.TabControl.SelectedIndex].Text = Path.GetFileName(Filepath);
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
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDKv2 Gameconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKv1 Gameconfig Files|Gameconfig*.bin";
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
            dlg.Filter = "RSDKv1 Gameconfig Files|Gameconfig*.bin";
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
            gameconfig.DataFileName = SubNameTxt.Text; //When the text box is updated, update the gameName String as well!
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
            SubNameTxt.Text = gameconfig.DataFileName;
            AboutBox.Text = gameconfig.GameDescriptionText;

            if (gameconfig.Categories[CurCategory].Scenes.Count > 0)
            {
                StgNameBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].Name;
                StgFolderBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder;
                StgIDBox.Text = gameconfig.Categories[CurCategory].Scenes[CurStage].ActID;
            }

            if (gameconfig.ScriptPaths.Count > 0)
            {
                ObjPathHashBox.Text = gameconfig.ScriptPaths[CurObj];
            }

            if (gameconfig.SoundFX.Count > 0)
            {
                SFXPathBox.Text = gameconfig.SoundFX[CurSfx];
            }

            if (gameconfig.playerData.Count > 0)
            {
                PlayerNameBox.Text = gameconfig.playerData[CurPlr].PlayerName;
                PlayerAnimBox.Text = gameconfig.playerData[CurPlr].PlayerAnimLocation;
                PlayerScriptPathBox.Text = gameconfig.playerData[CurPlr].PlayerScriptLocation;
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
                StageBox.Items.Add(gameconfig.Categories[CurCategory].Scenes[i].Name + ", " + gameconfig.Categories[CurCategory].Scenes[i].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[i].ActID);
            }
        }

        void refreshObjectList()
        {
            ObjListBox.Items.Clear();
            for (int i = 0; i < gameconfig.ScriptPaths.Count; i++)
            {
                ObjListBox.Items.Add(gameconfig.ScriptPaths[i]);
            }
        }

        void refreshSoundFXList()
        {
            SoundFXListBox.Items.Clear();
            for (int i = 0; i < gameconfig.SoundFX.Count; i++)
            {
                SoundFXListBox.Items.Add(gameconfig.SoundFX[i]);
            }
        }

        void refreshPlayerList()
        {
            PlayersListBox.Items.Clear();
            for (int i = 0; i < gameconfig.playerData.Count; i++)
            {
                PlayersListBox.Items.Add(gameconfig.playerData[i].PlayerName + ", " + gameconfig.playerData[i].PlayerAnimLocation + ", " + gameconfig.playerData[i].PlayerScriptLocation);
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
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID;
            }
        }

        private void StgFolderBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder = StgFolderBox.Text;
            if (StageBox.Items.Count > 0)
            {
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID;
            }
        }

        private void StgIDBox_TextChanged(object sender, EventArgs e)
        {
            gameconfig.Categories[CurCategory].Scenes[CurStage].ActID = StgIDBox.Text;
            if (StageBox.Items.Count > 0)
            {
                StageBox.Items[CurStage] = gameconfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + gameconfig.Categories[CurCategory].Scenes[CurStage].ActID;
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
            gameconfig.Categories[CurCategory].Scenes.Add(new RSDKv1.GameConfig.Category.SceneInfo());
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
            gameconfig.ScriptPaths.Add(fname);
            refreshLists();
        }

        private void DelObjButton_Click(object sender, EventArgs e)
        {
            gameconfig.ScriptPaths.RemoveAt(CurObj);
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDKv2 Gameconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
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

        private void SFXPathBox_TextChanged(object sender, EventArgs e)
        {
            if (SoundFXListBox.Items.Count > 0)
            {
                gameconfig.SoundFX[CurSfx] = SFXPathBox.Text;
                SoundFXListBox.Items[CurSfx] = SFXPathBox.Text;
            }
        }

        private void AddSFXButton_Click(object sender, EventArgs e)
        {
            string fname = "Folder/SoundFX.wav";
            gameconfig.SoundFX.Add(fname);
            refreshLists();
        }

        private void RemoveSFXButton_Click(object sender, EventArgs e)
        {
            if (SoundFXListBox.Items.Count > 0)
            {
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
                gameconfig.playerData[CurPlr].PlayerName = PlayerNameBox.Text;
                PlayersListBox.Items[CurPlr] = gameconfig.playerData[CurPlr].PlayerName + ", " + gameconfig.playerData[CurPlr].PlayerAnimLocation + ", " + gameconfig.playerData[CurPlr].PlayerScriptLocation;
            }
        }

        private void PlayerAnimBox_TextChanged(object sender, EventArgs e)
        {
            if (PlayersListBox.Items.Count > 0)
            {
                gameconfig.playerData[CurPlr].PlayerAnimLocation = PlayerAnimBox.Text;
                PlayersListBox.Items[CurPlr] = gameconfig.playerData[CurPlr].PlayerName + ", " + gameconfig.playerData[CurPlr].PlayerAnimLocation + ", " + gameconfig.playerData[CurPlr].PlayerScriptLocation;
            }
        }

        private void PlayerScriptPathBox_TextChanged(object sender, EventArgs e)
        {
            if (PlayersListBox.Items.Count > 0)
            {
                gameconfig.playerData[CurPlr].PlayerScriptLocation = PlayerScriptPathBox.Text;
                PlayersListBox.Items[CurPlr] = gameconfig.playerData[CurPlr].PlayerName + ", " + gameconfig.playerData[CurPlr].PlayerAnimLocation + ", " + gameconfig.playerData[CurPlr].PlayerScriptLocation;
            }
        }

        private void AddPlrButton_Click(object sender, EventArgs e)
        {
            RSDKv1.GameConfig.PlayerData p = new RSDKv1.GameConfig.PlayerData();
            p.PlayerName = "Character";
            gameconfig.playerData.Add(p);
            refreshLists();
        }

        private void RemovePlrButton_Click(object sender, EventArgs e)
        {
            if (PlayersListBox.Items.Count > 0)
            {
                gameconfig.playerData.RemoveAt(CurPlr);
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
            RSDKv1.GameConfig.GlobalVariable variable = new RSDKv1.GameConfig.GlobalVariable("Variable");
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
