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

namespace RetroED.Tools.StageconfigEditors.RSDKv1StageconfigEditor
{
    public partial class MainForm : DockContent
    {
        public RSDKv1.StageConfig stageconfig = new RSDKv1.StageConfig();

        public int CurObj = 0;
        public int CurSfx = 0;

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
            stageconfig = new RSDKv1.StageConfig();
            FILEPATH = null;
            refreshLists();
            RefreshUI();
            RetroED.MainForm.Instance.CurrentTabText = "New stageconfig";

            string RSDK = "RSDKv1";
            string dispname = "";
            RetroED.MainForm.Instance.CurrentTabText = "New Stageconfig";
            dispname = "New Stageconfig";

            Text = "New Stageconfig";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Stageconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);
        }

        public void Open(string Filepath)
        {
            stageconfig = new RSDKv1.StageConfig(new RSDKv1.Reader(Filepath));
            FILEPATH = Filepath;
            RefreshUI();
            refreshLists();
            RetroED.MainForm.Instance.CurrentTabText = Path.GetFileName(Filepath);

            string RSDK = "RSDKv1";
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
                RetroED.MainForm.Instance.CurrentTabText = "New Palette - RSDK Stageconfig Editor";
                dispname = "New Stageconfig - RSDK Stageconfig Editor";
            }

            Text = Path.GetFileName(Filepath) + " - RSDK Stageconfig Editor";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Stageconfig Editor";
            RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (" + RSDK + ")";
            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);

        }

        public void Save(string Filepath)
        {
            Console.WriteLine(FILEPATH);
            stageconfig.Write(new RSDKv1.Writer(Filepath));
            FILEPATH = Filepath;
            RetroED.MainForm.Instance.CurrentTabText = Path.GetFileName(Filepath);

            string RSDK = "RSDKv1";
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
                RetroED.MainForm.Instance.CurrentTabText = "New Palette - RSDK Stageconfig Editor";
                dispname = "New Stageconfig - RSDK Stageconfig Editor";
            }

            Text = Path.GetFileName(Filepath) + " - RSDK Stageconfig Editor";

            RetroED.MainForm.Instance.rp.state = "RetroED - RSDK Stageconfig Editor";
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
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDKv1 Stageconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKv1 Stageconfig Files|Stageconfig*.bin";
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
            dlg.Filter = "RSDKv1 stageconfig Files|stageconfig*.bin";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Save(dlg.FileName);
                Console.WriteLine("File Saved!");
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

            if (stageconfig.ScriptPaths.Count > 0)
            {
                ObjPathHashBox.Text = stageconfig.ScriptPaths[CurObj];
            }

            if (stageconfig.SoundFX.Count > 0)
            {
                SFXPathBox.Text = stageconfig.SoundFX[CurSfx];
            }

            LoadGlobalScriptsCB.Checked = stageconfig.LoadGlobalScripts;
        }

        void refreshObjectList()
        {
            ObjListBox.Items.Clear();
            for (int i = 0; i < stageconfig.ScriptPaths.Count; i++)
            {
                ObjListBox.Items.Add(stageconfig.ScriptPaths[i]);
            }
        }

        void refreshSoundFXList()
        {
            SoundFXListBox.Items.Clear();
            for (int i = 0; i < stageconfig.SoundFX.Count; i++)
            {
                SoundFXListBox.Items.Add(stageconfig.SoundFX[i]);
            }
        }

        void refreshLists()
        {
            refreshObjectList();
            refreshSoundFXList();
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
            stageconfig.ScriptPaths.Add(fname);
            refreshLists();
        }

        private void DelObjButton_Click(object sender, EventArgs e)
        {
            stageconfig.ScriptPaths.RemoveAt(CurObj);
            CurObj--;
            refreshLists();
        }

        private void ObjPathBox_TextChanged(object sender, EventArgs e)
        {
            if (ObjListBox.Items.Count > 0)
            {
                ObjListBox.Items[CurObj] = stageconfig.ScriptPaths[CurObj] = ObjPathHashBox.Text;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "RSDKv1 Stageconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
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
                SoundFXListBox.Items[CurSfx] = stageconfig.SoundFX[CurSfx] = SFXPathBox.Text;
            }
        }

        private void AddSFXButton_Click(object sender, EventArgs e)
        {
            string fname = "Folder/SoundFX.wav";
            stageconfig.SoundFX.Add(fname);
            refreshLists();
        }

        private void RemoveSFXButton_Click(object sender, EventArgs e)
        {
            if (SoundFXListBox.Items.Count > 0)
            {
                stageconfig.SoundFX.RemoveAt(CurSfx);
                CurSfx--;
                refreshLists();
            }
        }

        private void LoadGlobalScriptsCB_CheckedChanged(object sender, EventArgs e)
        {
            stageconfig.LoadGlobalScripts = LoadGlobalScriptsCB.Checked;
        }
    }   
}
