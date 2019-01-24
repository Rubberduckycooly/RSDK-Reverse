using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RetroED
{
    public partial class MainForm : Form
    {
        public string GameExe = null;
        public List<Tuple<MenuItem, MenuItem, MenuItem>> Links = new List<Tuple<MenuItem, MenuItem, MenuItem>>();

        public SharpPresence.Discord.RichPresence rp = new SharpPresence.Discord.RichPresence();
        public SharpPresence.Discord.EventHandlers eh = new SharpPresence.Discord.EventHandlers();

        public static MainForm Instance;

        public MainForm()
        {
            InitializeComponent();
            InitDiscord();
            Instance = this;
        }

        public void InitDiscord()
        {
            SharpPresence.Discord.Initialize("477031085788626947", eh);

            rp.state = "RetroED";
            rp.details = "Chillin' out without any tools open";

            rp.largeImageKey = "retroed-big";
            rp.largeImageText = "RetroED";

            //rp.startTimestamp = 1507665886;
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            rp.startTimestamp = secondsSinceEpoch;

            SharpPresence.Discord.RunCallbacks();
            SharpPresence.Discord.UpdatePresence(rp);
        }

        public void UpdateDiscord()
        {
            SharpPresence.Discord.RunCallbacks();
            if (TabControl.SelectedTab != null)
            {
                rp.details = TabControl.TabPages[TabControl.SelectedIndex].Text;
                
            }
            else
            {
                rp.details = "Chillin' out without any tools open";
            }
            SharpPresence.Discord.UpdatePresence(rp);
        }

        public void DisposeDiscord()
        {
            rp.startTimestamp = 0;
            SharpPresence.Discord.Shutdown();
        }

        /// <summary>
        /// Called after RunCallbacks() when ready.
        /// </summary>
        private void ReadyCallback()
        {
            //this.SetStatusBarMessage("Ready.");
            Console.WriteLine("Ready");
        }

        /// <summary>
        /// Called after RunCallbacks() in cause of disconnection.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        private void DisconnectedCallback(int errorCode, string message)
        {
            //this.SetStatusBarMessage(string.Format("Disconnect {0}: {1}", errorCode, message));
            Console.WriteLine(string.Format("Disconnect {0}: {1}", errorCode, message));
        }

        /// <summary>
        /// Called after RunCallbacks() in cause of error.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        private void ErrorCallback(int errorCode, string message)
        {
            //this.SetStatusBarMessage(string.Format("Error {0}: {1}", errorCode, message));
            Console.WriteLine(string.Format("Error {0}: {1}", errorCode, message));
        }

        public void LinkMenubar()
        {
            UnlinkMenubar();
            try
            {
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
            catch (Exception ex)
            {

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
            frm.Parent = this;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        } //Opens the Map Editor (and returns a referece to it)

        Tools.ChunkMappingsEditor.MainForm OpenMappingsEditor()
        {
            var frm = new Tools.ChunkMappingsEditor.MainForm();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Parent = this;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        } //Opens the Chunk Mappings Editor (and returns a referece to it)

        Tools.BackgroundEditor.MainView OpenBackgroundEditor()
        {
            Tools.BackgroundEditor.MainView frm = new Tools.BackgroundEditor.MainView();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Parent = this;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        } //Opens the Background Editor (and returns a referece to it)

        Tools.CollisionEditor.Mainform OpenRSDKCollisionEditor() //Opens the RSDK Collision Editor (and returns a referece to it)
        {
            Tools.CollisionEditor.Mainform frm = new Tools.CollisionEditor.Mainform();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Parent = this;
            var newTab = new TabPage();
            newTab.Controls.Add(frm);
            this.TabControl.TabPages.Add(newTab);
            this.TabControl.SelectedTab = newTab;
            this.TabControl.SelectedTab.Text = frm.Text;
            frm.Show();
            return frm;
        }

        Tools.RSonicCollisionEditor.Mainform OpenRSonicCollisionEditor() //Opens the Retro-Sonic Collision Editor (and returns a referece to it)
        {
            Tools.RSonicCollisionEditor.Mainform frm = new Tools.RSonicCollisionEditor.Mainform();
            frm.TopLevel = false;
            frm.ControlBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Parent = this;
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
            frm.Parent = this;
            var newTab = new TabPage(frm.Text);
            newTab.Controls.Add(frm);
            TabControl.TabPages.Add(newTab);
            TabControl.SelectedTab = newTab;
            frm.Show();
            return frm;
        } //Opens the Palette Editor (and returns a referece to it)

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
        } //Opens the GFX Tool (and returns a referece to it)

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
        } //Opens the Nexus Decrypter (and returns a referece to it)

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
        } //Opens the RSDK Unpacker (and returns a referece to it)

        Tools.RetroSonicStageListEditor.MainForm OpenRSonicMdfEditor()
        {
            var frm = new Tools.RetroSonicStageListEditor.MainForm();
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
        } //Opens the Retro Sonic Stage List Editor (and returns a referece to it)

        Tools.RetroSonicCharacterListEditor.MainForm OpenRSonicCharListEditor()
        {
            var frm = new Tools.RetroSonicCharacterListEditor.MainForm();
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
        } //Opens the Retro Sonic Stage List Editor (and returns a referece to it)

        Tools.ScriptEditor.MainForm OpenScriptEditor()
        {
            var frm = new Tools.ScriptEditor.MainForm();
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
        } //Opens the RSDK Script List Editor (and returns a referece to it)

        Tools.GameconfigEditors.RSDKv1GameconfigEditor.MainForm OpenGameconfig1Editor()
        {
            var frm = new Tools.GameconfigEditors.RSDKv1GameconfigEditor.MainForm();
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
        } //Opens the RSDKv1 Gameconfig Editor (and returns a referece to it)

        Tools.GameconfigEditors.RSDKv2GameconfigEditor.MainForm OpenGameconfig2Editor()
        {
            var frm = new Tools.GameconfigEditors.RSDKv2GameconfigEditor.MainForm();
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
        } //Opens the RSDKv2 Gameconfig Editor (and returns a referece to it)

        Tools.GameconfigEditors.RSDKvBGameconfigEditor.MainForm OpenGameconfigBEditor()
        {
            var frm = new Tools.GameconfigEditors.RSDKvBGameconfigEditor.MainForm();
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
        } //Opens the RSDKvB Gameconfig Editor (and returns a referece to it)

        Tools.StageconfigEditors.RSDKvRSStageconfigEditor.MainForm OpenStageconfigRSEditor()
        {
            var frm = new Tools.StageconfigEditors.RSDKvRSStageconfigEditor.MainForm();
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
        } //Opens the RSDKvRS Stageconfig Editor (and returns a referece to it)

        Tools.StageconfigEditors.RSDKv1StageconfigEditor.MainForm OpenStageconfig1Editor()
        {
            var frm = new Tools.StageconfigEditors.RSDKv1StageconfigEditor.MainForm();
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
        } //Opens the RSDKv1 Stageconfig Editor (and returns a referece to it)

        Tools.StageconfigEditors.RSDKv2StageconfigEditor.MainForm OpenStageconfig2Editor()
        {
            var frm = new Tools.StageconfigEditors.RSDKv2StageconfigEditor.MainForm();
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
        } //Opens the RSDKv2 Stageconfig Editor (and returns a referece to it)

        Tools.StageconfigEditors.RSDKvBStageconfigEditor.MainForm OpenStageconfigBEditor()
        {
            var frm = new Tools.StageconfigEditors.RSDKvBStageconfigEditor.MainForm();
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
        } //Opens the RSDKvB Stageconfig Editor (and returns a referece to it)

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
            UpdateDiscord();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void TabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            TabControl_SelectedIndexChanged(sender, e);
        }

        private void RunEXEButton_Click(object sender, EventArgs e)
        {
            if (GameExe == null)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Windows Executable Files (*.exe)|*.exe";

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    GameExe = dlg.FileName;
                }
            }
            System.Diagnostics.Process.Start(GameExe);
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Windows Executable Files (*.exe)|*.exe";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                GameExe = dlg.FileName;
            }
        }

        private void MenuItem_CollisionEditor_Click(object sender, EventArgs e)
        {
            OpenRSDKCollisionEditor();
        }

        private void MenuItem_RSonicCollisionEditor_Click(object sender, EventArgs e)
        {
            OpenRSonicCollisionEditor();
        }

        private void MainForm_Close(object sender, FormClosingEventArgs e)
        {
            DisposeDiscord();
        }

        private void MenuItem_RSonicMdfEditor_Click(object sender, EventArgs e)
        {
            OpenRSonicMdfEditor();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            OpenRSonicCharListEditor();
        }

        private void MenuItem_ScriptEditor_Click(object sender, EventArgs e)
        {
            OpenScriptEditor();
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            OpenGameconfigBEditor();
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            OpenGameconfig2Editor();
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            OpenGameconfig1Editor();
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {
            OpenStageconfigRSEditor();
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            OpenStageconfig1Editor();
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            OpenStageconfig2Editor();
        }

        private void menuItem19_Click(object sender, EventArgs e)
        {
            OpenStageconfigBEditor();
        }
    }
}
