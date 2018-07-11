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

        public List<Tuple<MenuItem, MenuItem, MenuItem>> Links = new List<Tuple<MenuItem, MenuItem, MenuItem>>();

        public MainForm()
        {
            InitializeComponent();
        }

        public void LinkMenubar()
        {
            UnlinkMenubar();
            var form = (Form)TabControl.SelectedTab.Controls[0];
            var menu = form.Menu;
            if (menu == null)
                return;
            foreach (MenuItem item in menu.MenuItems)
            {
                bool found = false;
                for (int i = 0; i < Menu.MenuItems.Count; ++i)
                { if (item.Text == Menu.MenuItems[i].Text) found = true; }
                if (!found)
                {
                    var menuItem = new MenuItem(item.Text);
                    Links.Add(new Tuple<MenuItem, MenuItem, MenuItem>(null, null, menuItem));
                    Menu.MenuItems.Add(3, menuItem);
                }
                for (int i = 0; i < Menu.MenuItems.Count; ++i)
                {
                    if (item.Text == Menu.MenuItems[i].Text)
                    {
                        bool HasItems = Menu.MenuItems[i].MenuItems.Count > 0;
                        int ii = 0;
                        while (item.MenuItems.Count > 0)
                            if (item.MenuItems[0].Text != "Exit")
                            {
                                Links.Add(new Tuple<MenuItem, MenuItem, MenuItem>(Menu.MenuItems[i], item, item.MenuItems[0]));
                                Menu.MenuItems[i].MenuItems.Add(ii, item.MenuItems[0]);
                                ++ii;
                            }
                            else
                            item.MenuItems.Remove(item.MenuItems[0]);
                        if (HasItems)
                        {
                            var spacer = new MenuItem("-");
                            Links.Add(new Tuple<MenuItem, MenuItem, MenuItem>(Menu.MenuItems[i], null, spacer));
                            Menu.MenuItems[i].MenuItems.Add(ii, spacer);
                        }

                        if (Menu.MenuItems[i].MenuItems.Count > 0)
                            Menu.MenuItems[i].Enabled = true;

                        break;
                    }
                }
            }
        }

        public void UnlinkMenubar()
        {
            foreach (var item in Links)
            {
                if (item.Item1 == null)
                {
                    Menu.MenuItems.Remove(item.Item3);
                    continue;
                }
                if (item.Item2 != null)
                {
                    item.Item2.MenuItems.Add(item.Item3);
                }
                else
                    item.Item1.MenuItems.Remove(item.Item3);
                if (item.Item1.MenuItems.Count == 0)
                    item.Item1.Enabled = false;
            }
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

        Tools.BackgroundEditor.MainView OpenBackgroundEditor()
        {
            Tools.BackgroundEditor.MainView frm = new Tools.BackgroundEditor.MainView();
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

        private void backgroundEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBackgroundEditor();
        }

        private void MenuItem_CloseTab_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab != null)
            {
                TabControl.TabPages.Remove(TabControl.SelectedTab);
                UnlinkMenubar();
            }
        }

        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab != null)
            {
                Text = $"RetroED - {TabControl.SelectedTab.Text}";
                MenuItem_CloseTab.Enabled = true;
                LinkMenubar();
            }
            else
            {
                Text = $"RetroED";
                MenuItem_CloseTab.Enabled = false;
                UnlinkMenubar();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void TabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            TabControl_SelectedIndexChanged(sender, e);
        }
    }
}
