namespace RetroED
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TabControl = new System.Windows.Forms.TabControl();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.MenuItem_RSDKUnpacker = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.MenuItem_AnimationEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_MapEditor = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.MenuItem_ChunkEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_CollisionEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_RSDKCollisionEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_RSonicCollisionEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_PaletteEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_GFXTool = new System.Windows.Forms.MenuItem();
            this.MenuItem_NexusDecrypter = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.MenuItem_CloseTab = new System.Windows.Forms.MenuItem();
            this.GameStarterButton = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.RunExeButton = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1261, 673);
            this.TabControl.TabIndex = 0;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            this.TabControl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.TabControl_ControlAdded);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.GameStarterButton,
            this.menuItem4});
            // 
            // menuItem1
            // 
            this.menuItem1.Enabled = false;
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Enabled = false;
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "View";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_RSDKUnpacker,
            this.menuItem14,
            this.MenuItem_AnimationEditor,
            this.MenuItem_MapEditor,
            this.menuItem6,
            this.MenuItem_ChunkEditor,
            this.MenuItem_CollisionEditor,
            this.MenuItem_PaletteEditor,
            this.MenuItem_GFXTool,
            this.MenuItem_NexusDecrypter,
            this.menuItem8,
            this.menuItem12,
            this.MenuItem_CloseTab});
            this.menuItem3.Text = "Engine";
            // 
            // MenuItem_RSDKUnpacker
            // 
            this.MenuItem_RSDKUnpacker.Index = 0;
            this.MenuItem_RSDKUnpacker.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftU;
            this.MenuItem_RSDKUnpacker.Text = "RSDK Unpacker";
            this.MenuItem_RSDKUnpacker.Click += new System.EventHandler(this.MenuItem_RSDKUnpacker_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 1;
            this.menuItem14.Text = "-";
            // 
            // MenuItem_AnimationEditor
            // 
            this.MenuItem_AnimationEditor.Enabled = false;
            this.MenuItem_AnimationEditor.Index = 2;
            this.MenuItem_AnimationEditor.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftA;
            this.MenuItem_AnimationEditor.Text = "Animation Editor";
            this.MenuItem_AnimationEditor.Click += new System.EventHandler(this.MenuItem_AnimationEditor_Click);
            // 
            // MenuItem_MapEditor
            // 
            this.MenuItem_MapEditor.Index = 3;
            this.MenuItem_MapEditor.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftM;
            this.MenuItem_MapEditor.Text = "Map Editor";
            this.MenuItem_MapEditor.Click += new System.EventHandler(this.MenuItem_MapEditor_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftB;
            this.menuItem6.Text = "Background Editor";
            this.menuItem6.Click += new System.EventHandler(this.backgroundEditorToolStripMenuItem_Click);
            // 
            // MenuItem_ChunkEditor
            // 
            this.MenuItem_ChunkEditor.Index = 5;
            this.MenuItem_ChunkEditor.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftC;
            this.MenuItem_ChunkEditor.Text = "Chunk Editor";
            this.MenuItem_ChunkEditor.Click += new System.EventHandler(this.MenuItem_ChunkEditor_Click);
            // 
            // MenuItem_CollisionEditor
            // 
            this.MenuItem_CollisionEditor.Index = 6;
            this.MenuItem_CollisionEditor.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_RSDKCollisionEditor,
            this.MenuItem_RSonicCollisionEditor});
            this.MenuItem_CollisionEditor.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftT;
            this.MenuItem_CollisionEditor.Text = "Collision Editor";
            // 
            // MenuItem_RSDKCollisionEditor
            // 
            this.MenuItem_RSDKCollisionEditor.Index = 0;
            this.MenuItem_RSDKCollisionEditor.Text = "Retro Engine";
            this.MenuItem_RSDKCollisionEditor.Click += new System.EventHandler(this.MenuItem_CollisionEditor_Click);
            // 
            // MenuItem_RSonicCollisionEditor
            // 
            this.MenuItem_RSonicCollisionEditor.Index = 1;
            this.MenuItem_RSonicCollisionEditor.Text = "Retro-Sonic";
            this.MenuItem_RSonicCollisionEditor.Click += new System.EventHandler(this.MenuItem_RSonicCollisionEditor_Click);
            // 
            // MenuItem_PaletteEditor
            // 
            this.MenuItem_PaletteEditor.Index = 7;
            this.MenuItem_PaletteEditor.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
            this.MenuItem_PaletteEditor.Text = "Palette Editor";
            this.MenuItem_PaletteEditor.Click += new System.EventHandler(this.MenuItem_PaletteEditor_Click);
            // 
            // MenuItem_GFXTool
            // 
            this.MenuItem_GFXTool.Index = 8;
            this.MenuItem_GFXTool.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftG;
            this.MenuItem_GFXTool.Text = "GFX Tool";
            this.MenuItem_GFXTool.Click += new System.EventHandler(this.MenuItem_GFXTool_Click);
            // 
            // MenuItem_NexusDecrypter
            // 
            this.MenuItem_NexusDecrypter.Index = 9;
            this.MenuItem_NexusDecrypter.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftN;
            this.MenuItem_NexusDecrypter.Text = "Nexus Decrypter";
            this.MenuItem_NexusDecrypter.Click += new System.EventHandler(this.MenuItem_NexusDecrypter_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Enabled = false;
            this.menuItem8.Index = 10;
            this.menuItem8.Text = "rsf Decrypter";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 11;
            this.menuItem12.Text = "-";
            // 
            // MenuItem_CloseTab
            // 
            this.MenuItem_CloseTab.Enabled = false;
            this.MenuItem_CloseTab.Index = 12;
            this.MenuItem_CloseTab.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.MenuItem_CloseTab.Text = "Close Tab";
            this.MenuItem_CloseTab.Click += new System.EventHandler(this.MenuItem_CloseTab_Click);
            // 
            // GameStarterButton
            // 
            this.GameStarterButton.Index = 3;
            this.GameStarterButton.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.RunExeButton});
            this.GameStarterButton.Text = "Game Starter";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "Change Exe";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // RunExeButton
            // 
            this.RunExeButton.Index = 1;
            this.RunExeButton.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftR;
            this.RunExeButton.Text = "Run Exe";
            this.RunExeButton.Click += new System.EventHandler(this.RunEXEButton_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5});
            this.menuItem4.Text = "About";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItem5.Text = "About RetroED";
            this.menuItem5.Click += new System.EventHandler(this.MenuItem_About_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 673);
            this.Controls.Add(this.TabControl);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "RetroED";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Close);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem MenuItem_RSDKUnpacker;
        private System.Windows.Forms.MenuItem MenuItem_AnimationEditor;
        private System.Windows.Forms.MenuItem MenuItem_MapEditor;
        private System.Windows.Forms.MenuItem MenuItem_PaletteEditor;
        private System.Windows.Forms.MenuItem MenuItem_ChunkEditor;
        private System.Windows.Forms.MenuItem MenuItem_GFXTool;
        private System.Windows.Forms.MenuItem MenuItem_NexusDecrypter;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem MenuItem_CloseTab;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem GameStarterButton;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem RunExeButton;
        private System.Windows.Forms.MenuItem MenuItem_CollisionEditor;
        private System.Windows.Forms.MenuItem MenuItem_RSDKCollisionEditor;
        private System.Windows.Forms.MenuItem MenuItem_RSonicCollisionEditor;
        private System.Windows.Forms.MenuItem menuItem8;
    }
}

