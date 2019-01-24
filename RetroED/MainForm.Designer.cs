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
            this.MenuItem_ScriptEditor = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.MenuItem_RSonicMdfEditor = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.MenuItem_GFXTool = new System.Windows.Forms.MenuItem();
            this.MenuItem_NexusDecrypter = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.MenuItem_CloseTab = new System.Windows.Forms.MenuItem();
            this.GameStarterButton = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.RunExeButton = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.StartupTab = new System.Windows.Forms.TabPage();
            this.NameLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdateNotesBox = new System.Windows.Forms.RichTextBox();
            this.UpdateNotesLabel = new System.Windows.Forms.Label();
            this.RFBox = new System.Windows.Forms.GroupBox();
            this.RecentFilesList = new System.Windows.Forms.ListBox();
            this.RSDKVerBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ToolsBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.StartupTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.RFBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.StartupTab);
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
            this.MenuItem_ScriptEditor,
            this.menuItem10,
            this.menuItem11,
            this.MenuItem_GFXTool,
            this.MenuItem_NexusDecrypter,
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
            // MenuItem_ScriptEditor
            // 
            this.MenuItem_ScriptEditor.Index = 8;
            this.MenuItem_ScriptEditor.Text = "Script Editor";
            this.MenuItem_ScriptEditor.Click += new System.EventHandler(this.MenuItem_ScriptEditor_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 9;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_RSonicMdfEditor,
            this.menuItem9,
            this.menuItem13,
            this.menuItem15,
            this.menuItem16});
            this.menuItem10.Text = "Gameconfig Editor";
            // 
            // MenuItem_RSonicMdfEditor
            // 
            this.MenuItem_RSonicMdfEditor.Index = 0;
            this.MenuItem_RSonicMdfEditor.Text = "Retro-Sonic Stage List Editor";
            this.MenuItem_RSonicMdfEditor.Click += new System.EventHandler(this.MenuItem_RSonicMdfEditor_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "Retro-Sonic Character List Editor";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 2;
            this.menuItem13.Text = "RSDKv1 Gameconfig Editor";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 3;
            this.menuItem15.Text = "RSDKv2 Gameconfig Editor";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 4;
            this.menuItem16.Text = "RSDKvB Gameconfig Editor";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 10;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem20,
            this.menuItem17,
            this.menuItem18,
            this.menuItem19});
            this.menuItem11.Text = "Stageconfig Editor";
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 0;
            this.menuItem20.Text = "Retro-Sonic Zoneconfig Editor";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 1;
            this.menuItem17.Text = "RSDKv1 Stageconfig Editor";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 2;
            this.menuItem18.Text = "RSDKv2 Stageconfig Editor";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 3;
            this.menuItem19.Text = "RSDKvB Stageconfig Editor";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // MenuItem_GFXTool
            // 
            this.MenuItem_GFXTool.Index = 11;
            this.MenuItem_GFXTool.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftG;
            this.MenuItem_GFXTool.Text = "GFX Tool";
            this.MenuItem_GFXTool.Click += new System.EventHandler(this.MenuItem_GFXTool_Click);
            // 
            // MenuItem_NexusDecrypter
            // 
            this.MenuItem_NexusDecrypter.Index = 12;
            this.MenuItem_NexusDecrypter.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftN;
            this.MenuItem_NexusDecrypter.Text = "Nexus Decrypter";
            this.MenuItem_NexusDecrypter.Click += new System.EventHandler(this.MenuItem_NexusDecrypter_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 13;
            this.menuItem12.Text = "-";
            // 
            // MenuItem_CloseTab
            // 
            this.MenuItem_CloseTab.Enabled = false;
            this.MenuItem_CloseTab.Index = 14;
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
            // StartupTab
            // 
            this.StartupTab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.StartupTab.Controls.Add(this.label3);
            this.StartupTab.Controls.Add(this.ToolsBox);
            this.StartupTab.Controls.Add(this.label2);
            this.StartupTab.Controls.Add(this.RSDKVerBox);
            this.StartupTab.Controls.Add(this.RFBox);
            this.StartupTab.Controls.Add(this.groupBox1);
            this.StartupTab.Controls.Add(this.NameLabel);
            this.StartupTab.Location = new System.Drawing.Point(4, 25);
            this.StartupTab.Name = "StartupTab";
            this.StartupTab.Size = new System.Drawing.Size(1253, 644);
            this.StartupTab.TabIndex = 0;
            this.StartupTab.Text = "Start Page";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(9, 4);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(325, 85);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "RetroED";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UpdateNotesLabel);
            this.groupBox1.Controls.Add(this.UpdateNotesBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(340, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 267);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Latest Update";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version: 1.0.0";
            // 
            // UpdateNotesBox
            // 
            this.UpdateNotesBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.UpdateNotesBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UpdateNotesBox.Location = new System.Drawing.Point(3, 76);
            this.UpdateNotesBox.Name = "UpdateNotesBox";
            this.UpdateNotesBox.ReadOnly = true;
            this.UpdateNotesBox.Size = new System.Drawing.Size(375, 188);
            this.UpdateNotesBox.TabIndex = 1;
            this.UpdateNotesBox.Text = "Version 1.2.0:\n\n- Stuff has been added/fixed";
            // 
            // UpdateNotesLabel
            // 
            this.UpdateNotesLabel.AutoSize = true;
            this.UpdateNotesLabel.Location = new System.Drawing.Point(7, 56);
            this.UpdateNotesLabel.Name = "UpdateNotesLabel";
            this.UpdateNotesLabel.Size = new System.Drawing.Size(99, 17);
            this.UpdateNotesLabel.TabIndex = 2;
            this.UpdateNotesLabel.Text = "Update Notes:";
            // 
            // RFBox
            // 
            this.RFBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RFBox.Controls.Add(this.RecentFilesList);
            this.RFBox.Location = new System.Drawing.Point(8, 277);
            this.RFBox.Name = "RFBox";
            this.RFBox.Size = new System.Drawing.Size(710, 359);
            this.RFBox.TabIndex = 2;
            this.RFBox.TabStop = false;
            this.RFBox.Text = "Recent Files";
            // 
            // RecentFilesList
            // 
            this.RecentFilesList.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.RecentFilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecentFilesList.FormattingEnabled = true;
            this.RecentFilesList.ItemHeight = 16;
            this.RecentFilesList.Items.AddRange(new object[] {
            "C:/RetroEngine/v2/R11A/Act1.bin - RSDKv2"});
            this.RecentFilesList.Location = new System.Drawing.Point(3, 18);
            this.RecentFilesList.Name = "RecentFilesList";
            this.RecentFilesList.Size = new System.Drawing.Size(704, 338);
            this.RecentFilesList.TabIndex = 0;
            // 
            // RSDKVerBox
            // 
            this.RSDKVerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSDKVerBox.FormattingEnabled = true;
            this.RSDKVerBox.Items.AddRange(new object[] {
            "RSDKvB",
            "RSDKv2",
            "RSDKv1",
            "RSDKvRS"});
            this.RSDKVerBox.Location = new System.Drawing.Point(8, 222);
            this.RSDKVerBox.Name = "RSDKVerBox";
            this.RSDKVerBox.Size = new System.Drawing.Size(329, 24);
            this.RSDKVerBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select RSDK Version:";
            // 
            // ToolsBox
            // 
            this.ToolsBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.ToolsBox.Location = new System.Drawing.Point(727, 0);
            this.ToolsBox.Name = "ToolsBox";
            this.ToolsBox.Size = new System.Drawing.Size(526, 644);
            this.ToolsBox.TabIndex = 5;
            this.ToolsBox.TabStop = false;
            this.ToolsBox.Text = "Tools";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "General Purpose Editor For RSDK versions below 5";
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
            this.TabControl.ResumeLayout(false);
            this.StartupTab.ResumeLayout(false);
            this.StartupTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.RFBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl TabControl;
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
        private System.Windows.Forms.MenuItem MenuItem_RSonicMdfEditor;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem MenuItem_ScriptEditor;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.TabPage StartupTab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox ToolsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RSDKVerBox;
        private System.Windows.Forms.GroupBox RFBox;
        private System.Windows.Forms.ListBox RecentFilesList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label UpdateNotesLabel;
        private System.Windows.Forms.RichTextBox UpdateNotesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NameLabel;
    }
}

