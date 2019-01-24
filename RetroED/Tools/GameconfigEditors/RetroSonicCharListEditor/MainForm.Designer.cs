namespace RetroED.Tools.RetroSonicCharacterListEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CharacterListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Char2AnimBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DeleteStageButton = new System.Windows.Forms.Button();
            this.AddStageButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Char1AnimBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CharCountBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MainCharBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DisplayNameBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.CharacterListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.Char2AnimBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.Char1AnimBox);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.CharCountBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.MainCharBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.DisplayNameBox);
            this.splitContainer1.Size = new System.Drawing.Size(374, 360);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 0;
            // 
            // CharacterListBox
            // 
            this.CharacterListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CharacterListBox.FormattingEnabled = true;
            this.CharacterListBox.ItemHeight = 16;
            this.CharacterListBox.Location = new System.Drawing.Point(0, 0);
            this.CharacterListBox.Name = "CharacterListBox";
            this.CharacterListBox.Size = new System.Drawing.Size(154, 360);
            this.CharacterListBox.TabIndex = 1;
            this.CharacterListBox.SelectedIndexChanged += new System.EventHandler(this.StageListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Character 2 Anim File";
            // 
            // Char2AnimBox
            // 
            this.Char2AnimBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Char2AnimBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Char2AnimBox.Location = new System.Drawing.Point(12, 218);
            this.Char2AnimBox.Name = "Char2AnimBox";
            this.Char2AnimBox.Size = new System.Drawing.Size(192, 22);
            this.Char2AnimBox.TabIndex = 9;
            this.Char2AnimBox.Text = "NULL";
            this.Char2AnimBox.TextChanged += new System.EventHandler(this.Char2AnimBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeleteStageButton);
            this.groupBox1.Controls.Add(this.AddStageButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 103);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // DeleteStageButton
            // 
            this.DeleteStageButton.Location = new System.Drawing.Point(7, 62);
            this.DeleteStageButton.Name = "DeleteStageButton";
            this.DeleteStageButton.Size = new System.Drawing.Size(145, 35);
            this.DeleteStageButton.TabIndex = 1;
            this.DeleteStageButton.Text = "Delete Character";
            this.DeleteStageButton.UseVisualStyleBackColor = true;
            this.DeleteStageButton.Click += new System.EventHandler(this.DeleteStageButton_Click);
            // 
            // AddStageButton
            // 
            this.AddStageButton.Location = new System.Drawing.Point(7, 22);
            this.AddStageButton.Name = "AddStageButton";
            this.AddStageButton.Size = new System.Drawing.Size(145, 35);
            this.AddStageButton.TabIndex = 0;
            this.AddStageButton.Text = "Add Character";
            this.AddStageButton.UseVisualStyleBackColor = true;
            this.AddStageButton.Click += new System.EventHandler(this.AddStageButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Character 1 Anim File";
            // 
            // Char1AnimBox
            // 
            this.Char1AnimBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Char1AnimBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Char1AnimBox.Location = new System.Drawing.Point(12, 173);
            this.Char1AnimBox.Name = "Char1AnimBox";
            this.Char1AnimBox.Size = new System.Drawing.Size(192, 22);
            this.Char1AnimBox.TabIndex = 6;
            this.Char1AnimBox.Text = "NULL";
            this.Char1AnimBox.TextChanged += new System.EventHandler(this.UnknownBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Character Count";
            // 
            // CharCountBox
            // 
            this.CharCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CharCountBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CharCountBox.Location = new System.Drawing.Point(12, 128);
            this.CharCountBox.Name = "CharCountBox";
            this.CharCountBox.Size = new System.Drawing.Size(192, 22);
            this.CharCountBox.TabIndex = 4;
            this.CharCountBox.Text = "1";
            this.CharCountBox.TextChanged += new System.EventHandler(this.ActIDBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Main Character";
            // 
            // MainCharBox
            // 
            this.MainCharBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainCharBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.MainCharBox.Location = new System.Drawing.Point(12, 83);
            this.MainCharBox.Name = "MainCharBox";
            this.MainCharBox.Size = new System.Drawing.Size(192, 22);
            this.MainCharBox.TabIndex = 2;
            this.MainCharBox.Text = "SONIC";
            this.MainCharBox.TextChanged += new System.EventHandler(this.StgFolderBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Display Name";
            // 
            // DisplayNameBox
            // 
            this.DisplayNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DisplayNameBox.Location = new System.Drawing.Point(12, 38);
            this.DisplayNameBox.Name = "DisplayNameBox";
            this.DisplayNameBox.Size = new System.Drawing.Size(192, 22);
            this.DisplayNameBox.TabIndex = 0;
            this.DisplayNameBox.Text = "SONIC";
            this.DisplayNameBox.TextChanged += new System.EventHandler(this.StgNameBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(374, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 388);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Retro-Sonic Stage List Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox CharacterListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Char1AnimBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CharCountBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MainCharBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DisplayNameBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DeleteStageButton;
        private System.Windows.Forms.Button AddStageButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Char2AnimBox;
    }
}