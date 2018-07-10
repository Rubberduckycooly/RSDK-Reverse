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
            var frm = new Tools.MapEditor.MainView();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        }

        Tools.ChunkMappingsEditor.MainForm OpenMappingsEditor()
        {
            var frm = new Tools.ChunkMappingsEditor.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        }

        Tools.PaletteEditor.MainForm OpenPaletteEditor()
        {
            var frm = new Tools.PaletteEditor.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        }

        Tools.GFXTool.MainForm OpenGFXTool()
        {
            var frm = new Tools.GFXTool.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        }

        Tools.NexusDecrypt.MainForm OpenNexusDecryptTool()
        {
            var frm = new Tools.NexusDecrypt.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        }

        Tools.RSDKUnpacker.MainForm OpenRSDKUnpacker()
        {
            var frm = new Tools.RSDKUnpacker.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        }

        private void MenuItem_RSDKUnpacker_Click(object sender, EventArgs e)
        {
            OpenRSDKUnpacker();
        }

        private void MenuItem_AnimationEditor_Click(object sender, EventArgs e)
        {
            // TODO: Add Animation Editor
        }

        private void MenuItem_MapEditor_Click(object sender, EventArgs e)
        {
            OpenMapEditor();
        }

        private void MenuItem_PaletteEditor_Click(object sender, EventArgs e)
        {
            OpenPaletteEditor();
        }

        private void MenuItem_ChunkEditor_Click(object sender, EventArgs e)
        {
            OpenMappingsEditor();
        }

        private void MenuItem_GFXTool_Click(object sender, EventArgs e)
        {
            OpenGFXTool();
        }

        private void MenuItem_NexusDecrypter_Click(object sender, EventArgs e)
        {
            OpenNexusDecryptTool();
        }

        private void MenuItem_CloseTab_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab != null)
                TabControl.TabPages.Remove(TabControl.SelectedTab);
        }

        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab.Text != null)
                Text = $"RetroED - {TabControl.SelectedTab.Text}";
            else
                Text = $"RetroED";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
