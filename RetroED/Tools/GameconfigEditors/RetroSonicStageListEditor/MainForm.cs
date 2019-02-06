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

namespace RetroED.Tools.RetroSonicStageListEditor
{
    public partial class MainForm : DockContent
    {

        public RSDKvRS.ZoneList ZoneList = new RSDKvRS.ZoneList();

        int currentStage = 0;

        string filePath;

        public MainForm()
        {
            InitializeComponent();
        }

        public void New()
        {
            ZoneList = new RSDKvRS.ZoneList();
            filePath = "";
            RefreshUI();

            string RSDK = "RSDKvRS (Zones) ";
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
            ZoneList = new RSDKvRS.ZoneList(filepath);
            filePath = filepath; 
            RefreshUI();
            RefreshList();

            string RSDK = "RSDKvRS (Zones) ";
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
            ZoneList.Write(filepath);

            string RSDK = "RSDKvRS (Zones) ";
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

            StgNameBox.Text = ZoneList.Stages[currentStage].StageName;
            StgFolderBox.Text = ZoneList.Stages[currentStage].StageFolder;
            ActIDBox.Text = ZoneList.Stages[currentStage].ActNo;
            UnknownBox.Text = ZoneList.Stages[currentStage].Unknown;

        }

        void RefreshList()
        {
            StageListBox.Items.Clear();
            for (int i = 0; i < ZoneList.Stages.Count; i++)
            {
                StageListBox.Items.Add(ZoneList.Stages[i].StageName + ", " + ZoneList.Stages[i].StageFolder + ", " + ZoneList.Stages[i].ActNo + ", " + ZoneList.Stages[i].Unknown);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Retro-Sonic Stage Lists|*.mdf";

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
            dlg.Filter = "Retro-Sonic Stage Lists|*.mdf";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Open(dlg.FileName);
            }
        }

        private void StageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StageListBox.SelectedIndex >= 0)
            {
                currentStage = StageListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void StgNameBox_TextChanged(object sender, EventArgs e)
        {
            ZoneList.Stages[currentStage].StageName = StgNameBox.Text;
            RefreshUI();
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[StageListBox.SelectedIndex] = (ZoneList.Stages[currentStage].StageName + ", " + ZoneList.Stages[currentStage].StageFolder + ", " + ZoneList.Stages[currentStage].ActNo + ", " + ZoneList.Stages[currentStage].Unknown);
            }
        }

        private void StgFolderBox_TextChanged(object sender, EventArgs e)
        {
            ZoneList.Stages[currentStage].StageFolder = StgFolderBox.Text;
            RefreshUI();
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[StageListBox.SelectedIndex] = (ZoneList.Stages[currentStage].StageName + ", " + ZoneList.Stages[currentStage].StageFolder + ", " + ZoneList.Stages[currentStage].ActNo + ", " + ZoneList.Stages[currentStage].Unknown);
            }
        }

        private void ActIDBox_TextChanged(object sender, EventArgs e)
        {
            ZoneList.Stages[currentStage].ActNo = ActIDBox.Text;
            RefreshUI();
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[StageListBox.SelectedIndex] = (ZoneList.Stages[currentStage].StageName + ", " + ZoneList.Stages[currentStage].StageFolder + ", " + ZoneList.Stages[currentStage].ActNo + ", " + ZoneList.Stages[currentStage].Unknown);
            }
        }

        private void UnknownBox_TextChanged(object sender, EventArgs e)
        {
            ZoneList.Stages[currentStage].Unknown = UnknownBox.Text;
            RefreshUI();
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[StageListBox.SelectedIndex] = (ZoneList.Stages[currentStage].StageName + ", " + ZoneList.Stages[currentStage].StageFolder + ", " + ZoneList.Stages[currentStage].ActNo + ", " + ZoneList.Stages[currentStage].Unknown);
            }
        }

        private void AddStageButton_Click(object sender, EventArgs e)
        {
            RSDKvRS.ZoneList.Level Lvl = new RSDKvRS.ZoneList.Level();
            ZoneList.Stages.Add(Lvl);
            StageListBox.Items.Add(Lvl.StageName + ", " + Lvl.StageFolder + ", " + Lvl.ActNo + ", " + Lvl.Unknown);
        }

        private void DeleteStageButton_Click(object sender, EventArgs e)
        {
            ZoneList.Stages.RemoveAt(currentStage);
            currentStage--;
            RefreshList();
            RefreshUI();
        }
    }
}
