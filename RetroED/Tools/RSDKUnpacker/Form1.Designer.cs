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
            this.AssetTreePage = new System.Windows.Forms.TabPage();
            this.DataView = new System.Windows.Forms.TreeView();
            this.FileListPage = new System.Windows.Forms.TabPage();
            this.FileListBox = new System.Windows.Forms.ListBox();
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
            this.AssetTreePage.SuspendLayout();
            this.FileListPage.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(695, 277);
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
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(695, 277);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AssetTreePage);
            this.tabControl1.Controls.Add(this.FileListPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(184, 277);
            this.tabControl1.TabIndex = 0;
            // 
            // AssetTreePage
            // 
            this.AssetTreePage.Controls.Add(this.DataView);
            this.AssetTreePage.Location = new System.Drawing.Point(4, 25);
            this.AssetTreePage.Name = "AssetTreePage";
            this.AssetTreePage.Padding = new System.Windows.Forms.Padding(3);
            this.AssetTreePage.Size = new System.Drawing.Size(176, 248);
            this.AssetTreePage.TabIndex = 0;
            this.AssetTreePage.Text = "Asset Tree";
            this.AssetTreePage.UseVisualStyleBackColor = true;
            // 
            // DataView
            // 
            this.DataView.BackColor = System.Drawing.SystemColors.Control;
            this.DataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataView.Location = new System.Drawing.Point(3, 3);
            this.DataView.Name = "DataView";
            this.DataView.Size = new System.Drawing.Size(170, 242);
            this.DataView.TabIndex = 1;
            this.DataView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DataView_AfterSelect);
            // 
            // FileListPage
            // 
            this.FileListPage.Controls.Add(this.FileListBox);
            this.FileListPage.Location = new System.Drawing.Point(4, 25);
            this.FileListPage.Name = "FileListPage";
            this.FileListPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileListPage.Size = new System.Drawing.Size(176, 248);
            this.FileListPage.TabIndex = 1;
            this.FileListPage.Text = "FileList";
            this.FileListPage.UseVisualStyleBackColor = true;
            // 
            // FileListBox
            // 
            this.FileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.ItemHeight = 16;
            this.FileListBox.Location = new System.Drawing.Point(3, 3);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.Size = new System.Drawing.Size(170, 242);
            this.FileListBox.TabIndex = 0;
            this.FileListBox.DoubleClick += new System.EventHandler(this.FileListBox_DoubleClick);
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
            this.groupBox1.Location = new System.Drawing.Point(0, 137);
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
            this.ClientSize = new System.Drawing.Size(695, 277);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Data File Manager";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.AssetTreePage.ResumeLayout(false);
            this.FileListPage.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AssetTreePage;
        private System.Windows.Forms.TreeView DataView;
        private System.Windows.Forms.TabPage FileListPage;
        private System.Windows.Forms.ListBox FileListBox;
    }
}

