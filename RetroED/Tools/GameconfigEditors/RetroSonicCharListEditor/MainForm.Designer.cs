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
            this.components = new System.ComponentModel.Container();
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItem_New = new System.Windows.Forms.MenuItem();
            this.MenuItem_Open = new System.Windows.Forms.MenuItem();
            this.MenuItem_Save = new System.Windows.Forms.MenuItem();
            this.MenuItem_SaveAs = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(374, 388);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 0;
            // 
            // CharacterListBox
            // 
            this.CharacterListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.CharacterListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CharacterListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CharacterListBox.FormattingEnabled = true;
            this.CharacterListBox.ItemHeight = 16;
            this.CharacterListBox.Location = new System.Drawing.Point(0, 0);
            this.CharacterListBox.Name = "CharacterListBox";
            this.CharacterListBox.Size = new System.Drawing.Size(154, 388);
            this.CharacterListBox.TabIndex = 1;
            this.CharacterListBox.SelectedIndexChanged += new System.EventHandler(this.StageListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.Char2AnimBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Char2AnimBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Char2AnimBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.groupBox1.Location = new System.Drawing.Point(0, 285);
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
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.Char1AnimBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Char1AnimBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Char1AnimBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.CharCountBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CharCountBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CharCountBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.MainCharBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MainCharBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.MainCharBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.DisplayNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DisplayNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DisplayNameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DisplayNameBox.Location = new System.Drawing.Point(12, 38);
            this.DisplayNameBox.Name = "DisplayNameBox";
            this.DisplayNameBox.Size = new System.Drawing.Size(192, 22);
            this.DisplayNameBox.TabIndex = 0;
            this.DisplayNameBox.Text = "SONIC";
            this.DisplayNameBox.TextChanged += new System.EventHandler(this.StgNameBox_TextChanged);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_New,
            this.MenuItem_Open,
            this.MenuItem_Save,
            this.MenuItem_SaveAs});
            this.menuItem1.Text = "File";
            // 
            // MenuItem_New
            // 
            this.MenuItem_New.Index = 0;
            this.MenuItem_New.Text = "New";
            this.MenuItem_New.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // MenuItem_Open
            // 
            this.MenuItem_Open.Index = 1;
            this.MenuItem_Open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.MenuItem_Open.Text = "&Open";
            this.MenuItem_Open.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // MenuItem_Save
            // 
            this.MenuItem_Save.Index = 2;
            this.MenuItem_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.MenuItem_Save.Text = "&Save";
            this.MenuItem_Save.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // MenuItem_SaveAs
            // 
            this.MenuItem_SaveAs.Index = 3;
            this.MenuItem_SaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.MenuItem_SaveAs.Text = "Save &As";
            this.MenuItem_SaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(374, 388);
            this.Controls.Add(this.splitContainer1);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Retro-Sonic Stage List Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DeleteStageButton;
        private System.Windows.Forms.Button AddStageButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Char2AnimBox;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem MenuItem_New;
        private System.Windows.Forms.MenuItem MenuItem_Open;
        private System.Windows.Forms.MenuItem MenuItem_Save;
        private System.Windows.Forms.MenuItem MenuItem_SaveAs;
    }
}