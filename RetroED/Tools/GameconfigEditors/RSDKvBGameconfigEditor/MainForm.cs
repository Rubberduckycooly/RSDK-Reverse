using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Tools.GameconfigEditors.RSDKvBGameconfigEditor
{
    public partial class MainForm : Form
    {
        public RSDKvB.GameConfig GameConfig = new RSDKvB.GameConfig();

        public string fileName;

        public int CurCategory = 0;
        public int CurStage = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        public void New()
        {
            GameConfig = new RSDKvB.GameConfig();
        }

        public void Open(string filepath)
        {
            GameConfig = new RSDKvB.GameConfig(filepath);
            fileName = filepath;
            RefreshUI();
            RefreshStageList();
        }

        public void Save(string filepath)
        {
            GameConfig.Write(filepath);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKvB Gameconfigs|Gameconfig*.bin";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Open(dlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                Save(fileName);
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKvB Gameconfigs|Gameconfig*.bin";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Save(dlg.FileName);
            }
        }

        public void RefreshUI()
        {
            StgNameBox.Text = GameConfig.Categories[CurCategory].Scenes[CurStage].Name;
            StgFolderBox.Text = GameConfig.Categories[CurCategory].Scenes[CurStage].SceneFolder;
            ActIDBox.Text = GameConfig.Categories[CurCategory].Scenes[CurStage].ActID;
        }

        public void RefreshStageList()
        {
            StageListBox.Items.Clear();
            for (int i = 0; i < GameConfig.Categories[CurCategory].Scenes.Count; i++)
            {
                StageListBox.Items.Add(GameConfig.Categories[CurCategory].Scenes[i].Name + ", " + GameConfig.Categories[CurCategory].Scenes[i].SceneFolder + ", " + GameConfig.Categories[CurCategory].Scenes[i].ActID);
            }
        }

        private void StageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StageListBox.SelectedIndex >= 0)
            {
                CurStage = StageListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void CategoriesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurCategory = CategoriesBox.SelectedIndex;
            RefreshStageList();
            RefreshUI();
        }

        private void AddStageButton_Click(object sender, EventArgs e)
        {
            GameConfig.Categories[CurCategory].Scenes.Add(new RSDKvB.GameConfig.Category.SceneInfo());
            RefreshStageList();
        }

        private void DeleteStageButton_Click(object sender, EventArgs e)
        {
            GameConfig.Categories[CurCategory].Scenes.RemoveAt(CurStage);
            RefreshStageList();
        }

        private void StgNameBox_TextChanged(object sender, EventArgs e)
        {
            GameConfig.Categories[CurCategory].Scenes[CurStage].Name = StgNameBox.Text;
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[CurStage] = (GameConfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + GameConfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + GameConfig.Categories[CurCategory].Scenes[CurStage].ActID);
            }
        }

        private void StgFolderBox_TextChanged(object sender, EventArgs e)
        {
            GameConfig.Categories[CurCategory].Scenes[CurStage].SceneFolder = StgFolderBox.Text;
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[CurStage] = (GameConfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + GameConfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + GameConfig.Categories[CurCategory].Scenes[CurStage].ActID);
            }
        }

        private void ActIDBox_TextChanged(object sender, EventArgs e)
        {
            GameConfig.Categories[CurCategory].Scenes[CurStage].ActID = ActIDBox.Text;
            if (StageListBox.SelectedIndex >= 0)
            {
                StageListBox.Items[CurStage] = (GameConfig.Categories[CurCategory].Scenes[CurStage].Name + ", " + GameConfig.Categories[CurCategory].Scenes[CurStage].SceneFolder + ", " + GameConfig.Categories[CurCategory].Scenes[CurStage].ActID);
            }
        }
    }
}
