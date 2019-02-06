using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.RetroSonicCharacterListEditor
{
    public partial class MainForm : DockContent
    {

        public RSDKvRS.CharacterList CharacterList = new RSDKvRS.CharacterList();

        int currentStage = 0;

        string filePath;

        public MainForm()
        {
            InitializeComponent();
        }

        public void New()
        {
            CharacterList = new RSDKvRS.CharacterList();
            filePath = "";
            RefreshUI();

            string RSDK = "RSDKvRS (Charaters) ";
            string dispname = "";
            RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig";
            dispname = "New Gameconfig";

            Text = "New Stageconfig";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Gameconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
        }

        public void Open(string filepath)
        {
            CharacterList = new RSDKvRS.CharacterList(filepath);
            filePath = filepath; 
            RefreshUI();
            RefreshList();

            string RSDK = "RSDKvRS (Characters) ";
            string dispname = "";
            string folder = Path.GetDirectoryName(filepath);
            DirectoryInfo di = new DirectoryInfo(folder);
            folder = di.Name;
            string file = Path.GetFileName(filepath);

            if (filepath != null)
            {
                RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                dispname = folder + "/" + file;
            }
            else
            {
                RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig - RSDK Gameconfig Editor";
                dispname = "New Stageconfig - RSDK Gameconfig Editor";
            }

            Text = Path.GetFileName(filepath) + " - RSDK Gameconfig Editor";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Gameconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
        }

        public void Save(string filepath)
        {
            CharacterList.Write(filepath);

            string RSDK = "RSDKvRS (Characters) ";
            string dispname = "";
            string folder = Path.GetDirectoryName(filepath);
            DirectoryInfo di = new DirectoryInfo(folder);
            folder = di.Name;
            string file = Path.GetFileName(filepath);

            if (filepath != null)
            {
                RetroED.MainForm.Instance.CurrentTabText = folder + "/" + file;
                dispname = folder + "/" + file;
            }
            else
            {
                RetroED.MainForm.Instance.CurrentTabText = "New Gameconfig - RSDK Gameconfig Editor";
                dispname = "New Stageconfig - RSDK Gameconfig Editor";
            }

            Text = Path.GetFileName(filepath) + " - RSDK Gameconfig Editor";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Gameconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
        }

        void RefreshUI()
        {

            DisplayNameBox.Text = CharacterList.Characters[currentStage].DisplayName;
            MainCharBox.Text = CharacterList.Characters[currentStage].MainCharacter;
            CharCountBox.Text = CharacterList.Characters[currentStage].CharacterCount;
            Char1AnimBox.Text = CharacterList.Characters[currentStage].Character1Anim;
            Char2AnimBox.Text = CharacterList.Characters[currentStage].Character2Anim;

        }

        void RefreshList()
        {
            CharacterListBox.Items.Clear();
            for (int i = 0; i < CharacterList.Characters.Count; i++)
            {
                CharacterListBox.Items.Add(CharacterList.Characters[i].DisplayName);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Retro-Sonic Character Lists|Characters*.mdf";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Open(dlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                Save(filePath);
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Retro-Sonic Character Lists|Characters*.mdf";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Open(dlg.FileName);
            }
        }

        private void StageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CharacterListBox.SelectedIndex >= 0)
            {
                currentStage = CharacterListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void StgNameBox_TextChanged(object sender, EventArgs e)
        {
            CharacterList.Characters[currentStage].DisplayName = DisplayNameBox.Text;
            RefreshUI();
            if (CharacterListBox.SelectedIndex >= 0)
            {
                CharacterListBox.Items[CharacterListBox.SelectedIndex] = (CharacterList.Characters[currentStage].DisplayName);
            }
        }

        private void StgFolderBox_TextChanged(object sender, EventArgs e)
        {
            CharacterList.Characters[currentStage].MainCharacter = MainCharBox.Text;
            RefreshUI();
        }

        private void ActIDBox_TextChanged(object sender, EventArgs e)
        {
            CharacterList.Characters[currentStage].CharacterCount = CharCountBox.Text;
            RefreshUI();
        }

        private void UnknownBox_TextChanged(object sender, EventArgs e)
        {
            CharacterList.Characters[currentStage].Character1Anim = Char1AnimBox.Text;
            RefreshUI();
        }
        private void Char2AnimBox_TextChanged(object sender, EventArgs e)
        {
            CharacterList.Characters[currentStage].Character2Anim = Char2AnimBox.Text;
            RefreshUI();
        }


        private void AddStageButton_Click(object sender, EventArgs e)
        {
            RSDKvRS.CharacterList.Character Lvl = new RSDKvRS.CharacterList.Character();
            CharacterList.Characters.Add(Lvl);
            CharacterListBox.Items.Add(Lvl.DisplayName);
        }

        private void DeleteStageButton_Click(object sender, EventArgs e)
        {
            CharacterList.Characters.RemoveAt(currentStage);
            currentStage--;
            RefreshList();
            RefreshUI();
        }
    }
}
