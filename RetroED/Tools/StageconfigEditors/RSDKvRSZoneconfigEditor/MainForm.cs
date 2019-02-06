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

namespace RetroED.Tools.StageconfigEditors.RSDKvRSStageconfigEditor
{
    public partial class MainForm : DockContent
    {
        public RSDKvRS.Zoneconfig stageconfig = new RSDKvRS.Zoneconfig();

        public int CurObj = 0;
        public int CurSfx = 0;
        public int CurMus = 0;
        public int CurSht = 0;

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
            stageconfig = new RSDKvRS.Zoneconfig();
            FILEPATH = null;
            refreshLists();
            RefreshUI();
            RetroED.MainForm.Instance.CurrentTabText = "New stageconfig";

            string RSDK = "RSDKvRS";
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
            stageconfig = new RSDKvRS.Zoneconfig(new RSDKvRS.Reader(Filepath));
            FILEPATH = Filepath;
            refreshLists();
            RefreshUI();
            RetroED.MainForm.Instance.CurrentTabText = Path.GetFileName(Filepath);

            string RSDK = "RSDKvRS";
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
                RetroED.MainForm.Instance.CurrentTabText = "New Stageconfig - RSDK Stageconfig Editor";
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
            stageconfig.Write(new RSDKvRS.Writer(Filepath));
            FILEPATH = Filepath;
            RetroED.MainForm.Instance.CurrentTabText = Path.GetFileName(Filepath);

            string RSDK = "RSDKvRS";
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
                RetroED.MainForm.Instance.CurrentTabText = "New Stageconfig - RSDK Stageconfig Editor";
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
            switch (MessageBox.Show(this, "Do you want to save the current file?", "Retro-Sonic Zoneconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKvRS Zoneconfig Files|Zone*.zcf";
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
            dlg.Filter = "RSDKvRS Zoneconfig Files|Zone*.zcf";
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

            if (stageconfig.Objects.Count > 0)
            {
                ObjPathHashBox.Text = stageconfig.Objects[CurObj].FilePath;
                SheetIDNUD.Value = stageconfig.Objects[CurObj].SpriteSheetID;
            }

            if (stageconfig.SoundFX.Count > 0)
            {
                SFXPathBox.Text = stageconfig.SoundFX[CurSfx];
            }

            if (stageconfig.Music.Count > 0)
            {
                MusicBox.Text = stageconfig.Music[CurMus];
            }

            if (stageconfig.ObjectSpritesheets.Count > 0)
            {
                SheetPathBox.Text = stageconfig.ObjectSpritesheets[CurSht];
            }
        }

        void refreshObjectList()
        {
            ObjListBox.Items.Clear();
            for (int i = 0; i < stageconfig.Objects.Count; i++)
            {
                ObjListBox.Items.Add(stageconfig.Objects[i].FilePath);
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

        void refreshMusicList()
        {
            MusicListBox.Items.Clear();
            for (int i = 0; i < stageconfig.Music.Count; i++)
            {
                MusicListBox.Items.Add(stageconfig.Music[i]);
            }
        }

        void refreshSheetList()
        {
            SheetListbox.Items.Clear();
            for (int i = 0; i < stageconfig.ObjectSpritesheets.Count; i++)
            {
                SheetListbox.Items.Add(stageconfig.ObjectSpritesheets[i]);
            }
        }

        void refreshLists()
        {
            refreshObjectList();
            refreshSoundFXList();
            refreshMusicList();
            refreshSheetList();
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
            RSDKvRS.Zoneconfig.ObjectData OD = new RSDKvRS.Zoneconfig.ObjectData();
            OD.FilePath = "Object.rsf";
            OD.SpriteSheetID = 0;
            stageconfig.Objects.Add(OD);
            refreshLists();
        }

        private void DelObjButton_Click(object sender, EventArgs e)
        {
            stageconfig.Objects.RemoveAt(CurObj);
            CurObj--;
            refreshLists();
        }

        private void SheetIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (ObjListBox.Items.Count > 0)
            {
                stageconfig.Objects[CurObj].SpriteSheetID = (byte)SheetIDNUD.Value;
            }
        }

        private void ObjPathBox_TextChanged(object sender, EventArgs e)
        {
            if (ObjListBox.Items.Count > 0)
            {
                ObjListBox.Items[CurObj] = stageconfig.Objects[CurObj].FilePath = ObjPathHashBox.Text;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show(this, "Do you want to save the current file?", "Retro-Sonic Zoneconfig Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
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

        private void MusicListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MusicListBox.SelectedIndex >= 0)
            {
                CurMus = MusicListBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void MusicBox_TextChanged(object sender, EventArgs e)
        {
            if (stageconfig.Music.Count > 0)
            {
                MusicListBox.Items[CurMus] = stageconfig.Music[CurMus] = MusicBox.Text;
            }
        }

        private void AddMusButton_Click(object sender, EventArgs e)
        {
            string fname = "Music.ogg";
            stageconfig.Music.Add(fname);
            refreshLists();
        }

        private void DelMusButton_Click(object sender, EventArgs e)
        {
            if (MusicListBox.Items.Count > 0)
            {
                stageconfig.Music.RemoveAt(CurMus);
                CurMus--;
                refreshLists();
            }
        }

        private void SheetListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SheetListbox.SelectedIndex >= 0)
            {
                CurSht = SheetListbox.SelectedIndex;
                RefreshUI();
            }
        }

        private void SheetPathBox_TextChanged(object sender, EventArgs e)
        {
            SheetListbox.Items[CurSht] = stageconfig.ObjectSpritesheets[CurSht] = SheetPathBox.Text;
        }

        private void DelSheetButton_Click(object sender, EventArgs e)
        {
            if (SheetListbox.Items.Count > 0)
            {
                stageconfig.ObjectSpritesheets.RemoveAt(CurSht);
                CurSht--;
                refreshLists();
            }
        }

        private void AddSheetButton_Click(object sender, EventArgs e)
        {
            string fname = "Sheet.bmp";
            stageconfig.ObjectSpritesheets.Add(fname);
            refreshLists();
        }
    }   
}
