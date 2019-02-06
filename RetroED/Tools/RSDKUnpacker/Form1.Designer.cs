namespace RetroED.Tools.RSDKUnpacker
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FileListPage = new System.Windows.Forms.TabPage();
            this.FileListBox = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DirectoryListBox = new System.Windows.Forms.ListBox();
            this.FileInfoBox = new System.Windows.Forms.GroupBox();
            this.DirectoryList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FullFileNameLabel = new System.Windows.Forms.Label();
            this.FileOffsetLabel = new System.Windows.Forms.Label();
            this.EncryptedCB = new System.Windows.Forms.CheckBox();
            this.FileSizeLabel = new System.Windows.Forms.Label();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SelectListButton = new System.Windows.Forms.Button();
            this.ExtractDataButton = new System.Windows.Forms.Button();
            this.DataFileLocation = new System.Windows.Forms.TextBox();
            this.SelDataFileButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BuildDataButton = new System.Windows.Forms.Button();
            this.DataFolderLocation = new System.Windows.Forms.TextBox();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.FileListPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.FileInfoBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 508);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FileInfoBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(695, 508);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FileListPage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(184, 508);
            this.tabControl1.TabIndex = 0;
            // 
            // FileListPage
            // 
            this.FileListPage.Controls.Add(this.FileListBox);
            this.FileListPage.Location = new System.Drawing.Point(4, 25);
            this.FileListPage.Name = "FileListPage";
            this.FileListPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileListPage.Size = new System.Drawing.Size(176, 479);
            this.FileListPage.TabIndex = 1;
            this.FileListPage.Text = "FileList";
            this.FileListPage.UseVisualStyleBackColor = true;
            // 
            // FileListBox
            // 
            this.FileListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.FileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.ItemHeight = 16;
            this.FileListBox.Location = new System.Drawing.Point(3, 3);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.Size = new System.Drawing.Size(170, 473);
            this.FileListBox.TabIndex = 0;
            this.FileListBox.SelectedIndexChanged += new System.EventHandler(this.FileListBox_SelectedIndexChanged);
            this.FileListBox.DoubleClick += new System.EventHandler(this.FileListBox_DoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DirectoryListBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(176, 479);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Directories";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DirectoryListBox
            // 
            this.DirectoryListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.DirectoryListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DirectoryListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DirectoryListBox.FormattingEnabled = true;
            this.DirectoryListBox.ItemHeight = 16;
            this.DirectoryListBox.Location = new System.Drawing.Point(3, 3);
            this.DirectoryListBox.Name = "DirectoryListBox";
            this.DirectoryListBox.Size = new System.Drawing.Size(170, 473);
            this.DirectoryListBox.TabIndex = 1;
            this.DirectoryListBox.SelectedIndexChanged += new System.EventHandler(this.DirectoryListBox_SelectedIndexChanged);
            // 
            // FileInfoBox
            // 
            this.FileInfoBox.Controls.Add(this.DirectoryList);
            this.FileInfoBox.Controls.Add(this.label1);
            this.FileInfoBox.Controls.Add(this.FullFileNameLabel);
            this.FileInfoBox.Controls.Add(this.FileOffsetLabel);
            this.FileInfoBox.Controls.Add(this.EncryptedCB);
            this.FileInfoBox.Controls.Add(this.FileSizeLabel);
            this.FileInfoBox.Controls.Add(this.FileNameLabel);
            this.FileInfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileInfoBox.Location = new System.Drawing.Point(0, 131);
            this.FileInfoBox.Name = "FileInfoBox";
            this.FileInfoBox.Size = new System.Drawing.Size(507, 237);
            this.FileInfoBox.TabIndex = 23;
            this.FileInfoBox.TabStop = false;
            this.FileInfoBox.Text = "File Info";
            // 
            // DirectoryList
            // 
            this.DirectoryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectoryList.FormattingEnabled = true;
            this.DirectoryList.Location = new System.Drawing.Point(82, 190);
            this.DirectoryList.Name = "DirectoryList";
            this.DirectoryList.Size = new System.Drawing.Size(231, 24);
            this.DirectoryList.TabIndex = 6;
            this.DirectoryList.SelectedIndexChanged += new System.EventHandler(this.DirectoryList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(7, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Directory:";
            // 
            // FullFileNameLabel
            // 
            this.FullFileNameLabel.AutoSize = true;
            this.FullFileNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FullFileNameLabel.Location = new System.Drawing.Point(7, 152);
            this.FullFileNameLabel.Name = "FullFileNameLabel";
            this.FullFileNameLabel.Size = new System.Drawing.Size(137, 17);
            this.FullFileNameLabel.TabIndex = 4;
            this.FullFileNameLabel.Text = "Full File Name = Null";
            // 
            // FileOffsetLabel
            // 
            this.FileOffsetLabel.AutoSize = true;
            this.FileOffsetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FileOffsetLabel.Location = new System.Drawing.Point(7, 82);
            this.FileOffsetLabel.Name = "FileOffsetLabel";
            this.FileOffsetLabel.Size = new System.Drawing.Size(131, 17);
            this.FileOffsetLabel.TabIndex = 3;
            this.FileOffsetLabel.Text = "FileOffset = 0 Bytes";
            // 
            // EncryptedCB
            // 
            this.EncryptedCB.AutoSize = true;
            this.EncryptedCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.EncryptedCB.Location = new System.Drawing.Point(10, 117);
            this.EncryptedCB.Name = "EncryptedCB";
            this.EncryptedCB.Size = new System.Drawing.Size(102, 21);
            this.EncryptedCB.TabIndex = 2;
            this.EncryptedCB.Text = "Encrypted?";
            this.EncryptedCB.UseVisualStyleBackColor = true;
            this.EncryptedCB.CheckedChanged += new System.EventHandler(this.EncryptedCB_CheckedChanged);
            // 
            // FileSizeLabel
            // 
            this.FileSizeLabel.AutoSize = true;
            this.FileSizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FileSizeLabel.Location = new System.Drawing.Point(7, 50);
            this.FileSizeLabel.Name = "FileSizeLabel";
            this.FileSizeLabel.Size = new System.Drawing.Size(124, 17);
            this.FileSizeLabel.TabIndex = 1;
            this.FileSizeLabel.Text = "File Size = 0 Bytes";
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FileNameLabel.Location = new System.Drawing.Point(7, 22);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(111, 17);
            this.FileNameLabel.TabIndex = 0;
            this.FileNameLabel.Text = "File Name = Null";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SelectListButton);
            this.groupBox2.Controls.Add(this.ExtractDataButton);
            this.groupBox2.Controls.Add(this.DataFileLocation);
            this.groupBox2.Controls.Add(this.SelDataFileButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 131);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Extract Data File";
            // 
            // SelectListButton
            // 
            this.SelectListButton.Location = new System.Drawing.Point(161, 72);
            this.SelectListButton.Name = "SelectListButton";
            this.SelectListButton.Size = new System.Drawing.Size(152, 33);
            this.SelectListButton.TabIndex = 25;
            this.SelectListButton.Text = "Select File List";
            this.SelectListButton.UseVisualStyleBackColor = true;
            this.SelectListButton.Click += new System.EventHandler(this.SelectListButton_Click);
            // 
            // ExtractDataButton
            // 
            this.ExtractDataButton.Location = new System.Drawing.Point(6, 72);
            this.ExtractDataButton.Name = "ExtractDataButton";
            this.ExtractDataButton.Size = new System.Drawing.Size(152, 33);
            this.ExtractDataButton.TabIndex = 23;
            this.ExtractDataButton.Text = "Extract Data File";
            this.ExtractDataButton.UseVisualStyleBackColor = true;
            this.ExtractDataButton.Click += new System.EventHandler(this.Extract_Click);
            // 
            // DataFileLocation
            // 
            this.DataFileLocation.Location = new System.Drawing.Point(164, 38);
            this.DataFileLocation.Name = "DataFileLocation";
            this.DataFileLocation.ReadOnly = true;
            this.DataFileLocation.Size = new System.Drawing.Size(265, 22);
            this.DataFileLocation.TabIndex = 22;
            // 
            // SelDataFileButton
            // 
            this.SelDataFileButton.Location = new System.Drawing.Point(6, 33);
            this.SelDataFileButton.Name = "SelDataFileButton";
            this.SelDataFileButton.Size = new System.Drawing.Size(152, 33);
            this.SelDataFileButton.TabIndex = 21;
            this.SelDataFileButton.Text = "Select Data File";
            this.SelDataFileButton.UseVisualStyleBackColor = true;
            this.SelDataFileButton.Click += new System.EventHandler(this.SelectDataFileButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BuildDataButton);
            this.groupBox1.Controls.Add(this.DataFolderLocation);
            this.groupBox1.Controls.Add(this.SelectFolderButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 368);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 140);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compress Data File";
            // 
            // BuildDataButton
            // 
            this.BuildDataButton.Location = new System.Drawing.Point(6, 81);
            this.BuildDataButton.Name = "BuildDataButton";
            this.BuildDataButton.Size = new System.Drawing.Size(152, 33);
            this.BuildDataButton.TabIndex = 21;
            this.BuildDataButton.Text = "Build Data File";
            this.BuildDataButton.UseVisualStyleBackColor = true;
            this.BuildDataButton.Click += new System.EventHandler(this.BuildDataButton_Click);
            // 
            // DataFolderLocation
            // 
            this.DataFolderLocation.Location = new System.Drawing.Point(164, 47);
            this.DataFolderLocation.Name = "DataFolderLocation";
            this.DataFolderLocation.ReadOnly = true;
            this.DataFolderLocation.Size = new System.Drawing.Size(265, 22);
            this.DataFolderLocation.TabIndex = 20;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(6, 42);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(152, 33);
            this.SelectFolderButton.TabIndex = 19;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(695, 508);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Data File Manager";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.FileListPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.FileInfoBox.ResumeLayout(false);
            this.FileInfoBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button SelectListButton;
        private System.Windows.Forms.Button ExtractDataButton;
        private System.Windows.Forms.TextBox DataFileLocation;
        private System.Windows.Forms.Button SelDataFileButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BuildDataButton;
        private System.Windows.Forms.TextBox DataFolderLocation;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.GroupBox FileInfoBox;
        private System.Windows.Forms.Label FileSizeLabel;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Label FileOffsetLabel;
        private System.Windows.Forms.CheckBox EncryptedCB;
        private System.Windows.Forms.Label FullFileNameLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage FileListPage;
        private System.Windows.Forms.ListBox FileListBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox DirectoryListBox;
        private System.Windows.Forms.ComboBox DirectoryList;
        private System.Windows.Forms.Label label1;
    }
}

