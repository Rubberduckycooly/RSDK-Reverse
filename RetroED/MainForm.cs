using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Tools.MapEditor.MainView OpenMapEditor()
        {
            Tools.MapEditor.MainView frm = new Tools.MapEditor.MainView();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        Tools.ChunkMappingsEditor.MainForm OpenMappingsEditor()
        {
            Tools.ChunkMappingsEditor.MainForm frm = new Tools.ChunkMappingsEditor.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        Tools.PaletteEditor.MainForm OpenPaletteEditor()
        {
            Tools.PaletteEditor.MainForm frm = new Tools.PaletteEditor.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        Tools.GFXTool.MainForm OpenGFXTool()
        {
            Tools.GFXTool.MainForm frm = new Tools.GFXTool.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        Tools.NexusDecrypt.MainForm OpenNexusDecryptTool()
        {
            Tools.NexusDecrypt.MainForm frm = new Tools.NexusDecrypt.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        Tools.RSDKUnpacker.MainForm OpenRSDKUnpacker()
        {
            Tools.RSDKUnpacker.MainForm frm = new Tools.RSDKUnpacker.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        private void animationEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMapEditor();
        }

        private void paletteEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPaletteEditor();
        }

        private void chunkEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMappingsEditor();
        }

        private void GFXToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenGFXTool();
        }

        private void NexusDecryptToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNexusDecryptTool();
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab != null) TabControl.SelectedTab.Dispose();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void rSDKUnpackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRSDKUnpacker();
        }
    }
}
