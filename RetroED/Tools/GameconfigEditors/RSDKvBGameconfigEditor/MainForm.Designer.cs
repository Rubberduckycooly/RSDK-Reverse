namespace RetroED.Tools.GameconfigEditors.RSDKvBGameconfigEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DeleteStageButton = new System.Windows.Forms.Button();
            this.AddStageButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ActIDBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StgNameBox = new System.Windows.Forms.TextBox();
            this.StageListBox = new System.Windows.Forms.ListBox();
            this.StgFolderBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.CategoriesBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(736, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 431);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DeleteStageButton);
            this.groupBox2.Controls.Add(this.AddStageButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 307);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 103);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // DeleteStageButton
            // 
            this.DeleteStageButton.Location = new System.Drawing.Point(7, 62);
            this.DeleteStageButton.Name = "DeleteStageButton";
            this.DeleteStageButton.Size = new System.Drawing.Size(106, 35);
            this.DeleteStageButton.TabIndex = 1;
            this.DeleteStageButton.Text = "Delete Stage";
            this.DeleteStageButton.UseVisualStyleBackColor = true;
            this.DeleteStageButton.Click += new System.EventHandler(this.DeleteStageButton_Click);
            // 
            // AddStageButton
            // 
            this.AddStageButton.Location = new System.Drawing.Point(7, 22);
            this.AddStageButton.Name = "AddStageButton";
            this.AddStageButton.Size = new System.Drawing.Size(106, 35);
            this.AddStageButton.TabIndex = 0;
            this.AddStageButton.Text = "Add Stage";
            this.AddStageButton.UseVisualStyleBackColor = true;
            this.AddStageButton.Click += new System.EventHandler(this.AddStageButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Act ID";
            // 
            // ActIDBox
            // 
            this.ActIDBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActIDBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ActIDBox.Location = new System.Drawing.Point(12, 128);
            this.ActIDBox.Name = "ActIDBox";
            this.ActIDBox.Size = new System.Drawing.Size(191, 22);
            this.ActIDBox.TabIndex = 4;
            this.ActIDBox.TextChanged += new System.EventHandler(this.ActIDBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Stage Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stage Name";
            // 
            // StgNameBox
            // 
            this.StgNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StgNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.StgNameBox.Location = new System.Drawing.Point(12, 38);
            this.StgNameBox.Name = "StgNameBox";
            this.StgNameBox.Size = new System.Drawing.Size(191, 22);
            this.StgNameBox.TabIndex = 0;
            this.StgNameBox.TextChanged += new System.EventHandler(this.StgNameBox_TextChanged);
            // 
            // StageListBox
            // 
            this.StageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StageListBox.FormattingEnabled = true;
            this.StageListBox.ItemHeight = 16;
            this.StageListBox.Location = new System.Drawing.Point(0, 0);
            this.StageListBox.Name = "StageListBox";
            this.StageListBox.Size = new System.Drawing.Size(153, 410);
            this.StageListBox.TabIndex = 1;
            this.StageListBox.SelectedIndexChanged += new System.EventHandler(this.StageListBox_SelectedIndexChanged);
            // 
            // StgFolderBox
            // 
            this.StgFolderBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StgFolderBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.StgFolderBox.Location = new System.Drawing.Point(12, 83);
            this.StgFolderBox.Name = "StgFolderBox";
            this.StgFolderBox.Size = new System.Drawing.Size(191, 22);
            this.StgFolderBox.TabIndex = 2;
            this.StgFolderBox.TextChanged += new System.EventHandler(this.StgFolderBox_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.StageListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CategoriesBox);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.ActIDBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.StgFolderBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.StgNameBox);
            this.splitContainer1.Size = new System.Drawing.Size(372, 410);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 2;
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Categories";
            // 
            // CategoriesBox
            // 
            this.CategoriesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoriesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoriesBox.FormattingEnabled = true;
            this.CategoriesBox.Items.AddRange(new object[] {
            "Presentation",
            "Regular",
            "Special",
            "Bonus"});
            this.CategoriesBox.Location = new System.Drawing.Point(15, 200);
            this.CategoriesBox.Name = "CategoriesBox";
            this.CategoriesBox.Size = new System.Drawing.Size(188, 24);
            this.CategoriesBox.TabIndex = 10;
            this.CategoriesBox.SelectedIndexChanged += new System.EventHandler(this.CategoriesBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 459);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "RSDKvB Gameconfig Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox StageListBox;
        private System.Windows.Forms.ComboBox CategoriesBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DeleteStageButton;
        private System.Windows.Forms.Button AddStageButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ActIDBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StgFolderBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StgNameBox;
    }
}