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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.MenuItem_RSDKUnpacker = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.MenuItem_AnimationEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_MapEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_PaletteEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_ChunkEditor = new System.Windows.Forms.MenuItem();
            this.MenuItem_GFXTool = new System.Windows.Forms.MenuItem();
            this.MenuItem_NexusDecrypter = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.MenuItem_CloseTab = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Margin = new System.Windows.Forms.Padding(2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(946, 547);
            this.TabControl.TabIndex = 0;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
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
            this.MenuItem_PaletteEditor,
            this.MenuItem_ChunkEditor,
            this.MenuItem_GFXTool,
            this.MenuItem_NexusDecrypter,
            this.menuItem12,
            this.MenuItem_CloseTab});
            this.menuItem3.Text = "Tools";
            // 
            // MenuItem_RSDKUnpacker
            // 
            this.MenuItem_RSDKUnpacker.Index = 0;
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
            this.MenuItem_AnimationEditor.Index = 2;
            this.MenuItem_AnimationEditor.Text = "Animation Editor";
            this.MenuItem_AnimationEditor.Click += new System.EventHandler(this.MenuItem_AnimationEditor_Click);
            // 
            // MenuItem_MapEditor
            // 
            this.MenuItem_MapEditor.Index = 3;
            this.MenuItem_MapEditor.Text = "Map Editor";
            this.MenuItem_MapEditor.Click += new System.EventHandler(this.MenuItem_MapEditor_Click);
            // 
            // MenuItem_PaletteEditor
            // 
            this.MenuItem_PaletteEditor.Index = 4;
            this.MenuItem_PaletteEditor.Text = "Palette Editor";
            this.MenuItem_PaletteEditor.Click += new System.EventHandler(this.MenuItem_PaletteEditor_Click);
            // 
            // MenuItem_ChunkEditor
            // 
            this.MenuItem_ChunkEditor.Index = 5;
            this.MenuItem_ChunkEditor.Text = "Chunk Editor";
            this.MenuItem_ChunkEditor.Click += new System.EventHandler(this.MenuItem_ChunkEditor_Click);
            // 
            // MenuItem_GFXTool
            // 
            this.MenuItem_GFXTool.Index = 6;
            this.MenuItem_GFXTool.Text = "GFX Tool";
            this.MenuItem_GFXTool.Click += new System.EventHandler(this.MenuItem_GFXTool_Click);
            // 
            // MenuItem_NexusDecrypter
            // 
            this.MenuItem_NexusDecrypter.Index = 7;
            this.MenuItem_NexusDecrypter.Text = "Nexus Decrypter";
            this.MenuItem_NexusDecrypter.Click += new System.EventHandler(this.MenuItem_NexusDecrypter_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 8;
            this.menuItem12.Text = "-";
            // 
            // MenuItem_CloseTab
            // 
            this.MenuItem_CloseTab.Index = 9;
            this.MenuItem_CloseTab.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.MenuItem_CloseTab.Text = "Close Tab";
            this.MenuItem_CloseTab.Click += new System.EventHandler(this.MenuItem_CloseTab_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "About";
            this.menuItem4.Click += new System.EventHandler(this.MenuItem_About_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 547);
            this.Controls.Add(this.TabControl);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "RetroED";
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
    }
}

