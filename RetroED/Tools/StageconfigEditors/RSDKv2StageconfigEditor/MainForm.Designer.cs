namespace RetroED.Tools.StageconfigEditors.RSDKv2StageconfigEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.eclipseEngine1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ObjectsPage = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ObjCFGBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ObjPathHashBox = new System.Windows.Forms.TextBox();
            this.DelObjButton = new System.Windows.Forms.Button();
            this.ObjListBox = new System.Windows.Forms.ListBox();
            this.AddObjButton = new System.Windows.Forms.Button();
            this.SoundFXPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SFXPathBox = new System.Windows.Forms.TextBox();
            this.RemoveSFXButton = new System.Windows.Forms.Button();
            this.SoundFXListBox = new System.Windows.Forms.ListBox();
            this.AddSFXButton = new System.Windows.Forms.Button();
            this.LoadGlobalScriptsCB = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.ObjectsPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SoundFXPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eclipseEngine1ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(712, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // eclipseEngine1ToolStripMenuItem
            // 
            this.eclipseEngine1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.eclipseEngine1ToolStripMenuItem.Name = "eclipseEngine1ToolStripMenuItem";
            this.eclipseEngine1ToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.eclipseEngine1ToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.saveAsToolStripMenuItem.Text = "Save as....";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ObjectsPage);
            this.tabControl1.Controls.Add(this.SoundFXPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(712, 531);
            this.tabControl1.TabIndex = 47;
            // 
            // ObjectsPage
            // 
            this.ObjectsPage.Controls.Add(this.groupBox7);
            this.ObjectsPage.Location = new System.Drawing.Point(4, 25);
            this.ObjectsPage.Name = "ObjectsPage";
            this.ObjectsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObjectsPage.Size = new System.Drawing.Size(704, 502);
            this.ObjectsPage.TabIndex = 1;
            this.ObjectsPage.Text = "Objects";
            this.ObjectsPage.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox7.Controls.Add(this.LoadGlobalScriptsCB);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.ObjCFGBox);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.ObjPathHashBox);
            this.groupBox7.Controls.Add(this.DelObjButton);
            this.groupBox7.Controls.Add(this.ObjListBox);
            this.groupBox7.Controls.Add(this.AddObjButton);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(698, 496);
            this.groupBox7.TabIndex = 47;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Object List";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(405, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 17);
            this.label12.TabIndex = 70;
            this.label12.Text = "Object Name";
            // 
            // ObjCFGBox
            // 
            this.ObjCFGBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjCFGBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ObjCFGBox.Location = new System.Drawing.Point(408, 87);
            this.ObjCFGBox.Margin = new System.Windows.Forms.Padding(4);
            this.ObjCFGBox.Name = "ObjCFGBox";
            this.ObjCFGBox.Size = new System.Drawing.Size(283, 22);
            this.ObjCFGBox.TabIndex = 69;
            this.ObjCFGBox.TextChanged += new System.EventHandler(this.ObjCFGBox_TextChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(404, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 17);
            this.label14.TabIndex = 68;
            this.label14.Text = "Script Path";
            // 
            // ObjPathHashBox
            // 
            this.ObjPathHashBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjPathHashBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ObjPathHashBox.Location = new System.Drawing.Point(407, 40);
            this.ObjPathHashBox.Margin = new System.Windows.Forms.Padding(4);
            this.ObjPathHashBox.Name = "ObjPathHashBox";
            this.ObjPathHashBox.Size = new System.Drawing.Size(284, 22);
            this.ObjPathHashBox.TabIndex = 67;
            this.ObjPathHashBox.TextChanged += new System.EventHandler(this.ObjPathBox_TextChanged);
            // 
            // DelObjButton
            // 
            this.DelObjButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DelObjButton.Location = new System.Drawing.Point(408, 160);
            this.DelObjButton.Name = "DelObjButton";
            this.DelObjButton.Size = new System.Drawing.Size(284, 33);
            this.DelObjButton.TabIndex = 55;
            this.DelObjButton.Text = "Remove Object";
            this.DelObjButton.UseVisualStyleBackColor = true;
            this.DelObjButton.Click += new System.EventHandler(this.DelObjButton_Click);
            // 
            // ObjListBox
            // 
            this.ObjListBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ObjListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ObjListBox.FormattingEnabled = true;
            this.ObjListBox.ItemHeight = 16;
            this.ObjListBox.Location = new System.Drawing.Point(3, 18);
            this.ObjListBox.Margin = new System.Windows.Forms.Padding(4);
            this.ObjListBox.Name = "ObjListBox";
            this.ObjListBox.Size = new System.Drawing.Size(399, 475);
            this.ObjListBox.TabIndex = 1;
            this.ObjListBox.SelectedIndexChanged += new System.EventHandler(this.ObjListBox_SelectedIndexChanged);
            // 
            // AddObjButton
            // 
            this.AddObjButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddObjButton.Location = new System.Drawing.Point(408, 116);
            this.AddObjButton.Name = "AddObjButton";
            this.AddObjButton.Size = new System.Drawing.Size(284, 33);
            this.AddObjButton.TabIndex = 54;
            this.AddObjButton.Text = "Add Object";
            this.AddObjButton.UseVisualStyleBackColor = true;
            this.AddObjButton.Click += new System.EventHandler(this.AddObjButton_Click);
            // 
            // SoundFXPage
            // 
            this.SoundFXPage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SoundFXPage.Controls.Add(this.groupBox2);
            this.SoundFXPage.Location = new System.Drawing.Point(4, 25);
            this.SoundFXPage.Name = "SoundFXPage";
            this.SoundFXPage.Padding = new System.Windows.Forms.Padding(3);
            this.SoundFXPage.Size = new System.Drawing.Size(704, 502);
            this.SoundFXPage.TabIndex = 2;
            this.SoundFXPage.Text = "SoundFX";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.SFXPathBox);
            this.groupBox2.Controls.Add(this.RemoveSFXButton);
            this.groupBox2.Controls.Add(this.SoundFXListBox);
            this.groupBox2.Controls.Add(this.AddSFXButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(698, 496);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SoundFX List";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 68;
            this.label2.Text = "SFX Path";
            // 
            // SFXPathBox
            // 
            this.SFXPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFXPathBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SFXPathBox.Location = new System.Drawing.Point(407, 39);
            this.SFXPathBox.Margin = new System.Windows.Forms.Padding(4);
            this.SFXPathBox.Name = "SFXPathBox";
            this.SFXPathBox.Size = new System.Drawing.Size(284, 22);
            this.SFXPathBox.TabIndex = 67;
            this.SFXPathBox.TextChanged += new System.EventHandler(this.SFXPathBox_TextChanged);
            // 
            // RemoveSFXButton
            // 
            this.RemoveSFXButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveSFXButton.Location = new System.Drawing.Point(406, 165);
            this.RemoveSFXButton.Name = "RemoveSFXButton";
            this.RemoveSFXButton.Size = new System.Drawing.Size(283, 33);
            this.RemoveSFXButton.TabIndex = 55;
            this.RemoveSFXButton.Text = "Remove SFX";
            this.RemoveSFXButton.UseVisualStyleBackColor = true;
            this.RemoveSFXButton.Click += new System.EventHandler(this.RemoveSFXButton_Click);
            // 
            // SoundFXListBox
            // 
            this.SoundFXListBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SoundFXListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.SoundFXListBox.FormattingEnabled = true;
            this.SoundFXListBox.ItemHeight = 16;
            this.SoundFXListBox.Location = new System.Drawing.Point(3, 18);
            this.SoundFXListBox.Margin = new System.Windows.Forms.Padding(4);
            this.SoundFXListBox.Name = "SoundFXListBox";
            this.SoundFXListBox.Size = new System.Drawing.Size(399, 475);
            this.SoundFXListBox.TabIndex = 1;
            this.SoundFXListBox.SelectedIndexChanged += new System.EventHandler(this.SoundFXListBox_SelectedIndexChanged);
            // 
            // AddSFXButton
            // 
            this.AddSFXButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddSFXButton.Location = new System.Drawing.Point(406, 126);
            this.AddSFXButton.Name = "AddSFXButton";
            this.AddSFXButton.Size = new System.Drawing.Size(285, 33);
            this.AddSFXButton.TabIndex = 54;
            this.AddSFXButton.Text = "Add SFX";
            this.AddSFXButton.UseVisualStyleBackColor = true;
            this.AddSFXButton.Click += new System.EventHandler(this.AddSFXButton_Click);
            // 
            // LoadGlobalScriptsCB
            // 
            this.LoadGlobalScriptsCB.AutoSize = true;
            this.LoadGlobalScriptsCB.Location = new System.Drawing.Point(409, 199);
            this.LoadGlobalScriptsCB.Name = "LoadGlobalScriptsCB";
            this.LoadGlobalScriptsCB.Size = new System.Drawing.Size(154, 21);
            this.LoadGlobalScriptsCB.TabIndex = 71;
            this.LoadGlobalScriptsCB.Text = "Load Global Scripts";
            this.LoadGlobalScriptsCB.UseVisualStyleBackColor = true;
            this.LoadGlobalScriptsCB.CheckedChanged += new System.EventHandler(this.LoadGlobalScriptsCB_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(712, 559);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "RSDKv2 Stageconfig Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ObjectsPage.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.SoundFXPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eclipseEngine1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ObjectsPage;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ObjCFGBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox ObjPathHashBox;
        private System.Windows.Forms.Button DelObjButton;
        private System.Windows.Forms.ListBox ObjListBox;
        private System.Windows.Forms.Button AddObjButton;
        private System.Windows.Forms.TabPage SoundFXPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SFXPathBox;
        private System.Windows.Forms.Button RemoveSFXButton;
        private System.Windows.Forms.ListBox SoundFXListBox;
        private System.Windows.Forms.Button AddSFXButton;
        private System.Windows.Forms.CheckBox LoadGlobalScriptsCB;
    }
}

