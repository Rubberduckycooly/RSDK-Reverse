namespace RetroED.Tools.StageconfigEditors.RSDKvRSStageconfigEditor
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ObjectsPage = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.SheetIDNUD = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
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
            this.MusicPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MusicBox = new System.Windows.Forms.TextBox();
            this.DelMusButton = new System.Windows.Forms.Button();
            this.MusicListBox = new System.Windows.Forms.ListBox();
            this.AddMusButton = new System.Windows.Forms.Button();
            this.SheetPage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SheetPathBox = new System.Windows.Forms.TextBox();
            this.DelSheetButton = new System.Windows.Forms.Button();
            this.SheetListbox = new System.Windows.Forms.ListBox();
            this.AddSheetButton = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItem_New = new System.Windows.Forms.MenuItem();
            this.MenuItem_Open = new System.Windows.Forms.MenuItem();
            this.MenuItem_Save = new System.Windows.Forms.MenuItem();
            this.MenuItem_SaveAs = new System.Windows.Forms.MenuItem();
            this.tabControl1.SuspendLayout();
            this.ObjectsPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SheetIDNUD)).BeginInit();
            this.SoundFXPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MusicPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SheetPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ObjectsPage);
            this.tabControl1.Controls.Add(this.SoundFXPage);
            this.tabControl1.Controls.Add(this.MusicPage);
            this.tabControl1.Controls.Add(this.SheetPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(712, 559);
            this.tabControl1.TabIndex = 47;
            // 
            // ObjectsPage
            // 
            this.ObjectsPage.Controls.Add(this.groupBox7);
            this.ObjectsPage.Location = new System.Drawing.Point(4, 25);
            this.ObjectsPage.Name = "ObjectsPage";
            this.ObjectsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObjectsPage.Size = new System.Drawing.Size(704, 530);
            this.ObjectsPage.TabIndex = 1;
            this.ObjectsPage.Text = "Objects";
            this.ObjectsPage.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox7.Controls.Add(this.SheetIDNUD);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.ObjPathHashBox);
            this.groupBox7.Controls.Add(this.DelObjButton);
            this.groupBox7.Controls.Add(this.ObjListBox);
            this.groupBox7.Controls.Add(this.AddObjButton);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(698, 524);
            this.groupBox7.TabIndex = 47;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Object List";
            // 
            // SheetIDNUD
            // 
            this.SheetIDNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SheetIDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SheetIDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SheetIDNUD.Location = new System.Drawing.Point(410, 88);
            this.SheetIDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.SheetIDNUD.Name = "SheetIDNUD";
            this.SheetIDNUD.Size = new System.Drawing.Size(102, 22);
            this.SheetIDNUD.TabIndex = 71;
            this.SheetIDNUD.ValueChanged += new System.EventHandler(this.SheetIDNUD_ValueChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label12.Location = new System.Drawing.Point(405, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 17);
            this.label12.TabIndex = 70;
            this.label12.Text = "Object Sheet ID";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label14.Location = new System.Drawing.Point(404, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 17);
            this.label14.TabIndex = 68;
            this.label14.Text = "Script Path";
            // 
            // ObjPathHashBox
            // 
            this.ObjPathHashBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjPathHashBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ObjPathHashBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.ObjListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ObjListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ObjListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ObjListBox.FormattingEnabled = true;
            this.ObjListBox.ItemHeight = 16;
            this.ObjListBox.Location = new System.Drawing.Point(3, 18);
            this.ObjListBox.Margin = new System.Windows.Forms.Padding(4);
            this.ObjListBox.Name = "ObjListBox";
            this.ObjListBox.Size = new System.Drawing.Size(399, 503);
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
            this.SoundFXPage.Size = new System.Drawing.Size(704, 530);
            this.SoundFXPage.TabIndex = 2;
            this.SoundFXPage.Text = "SoundFX";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.SFXPathBox);
            this.groupBox2.Controls.Add(this.RemoveSFXButton);
            this.groupBox2.Controls.Add(this.SoundFXListBox);
            this.groupBox2.Controls.Add(this.AddSFXButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(698, 524);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SoundFX List";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(404, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 68;
            this.label2.Text = "SFX Path";
            // 
            // SFXPathBox
            // 
            this.SFXPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFXPathBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SFXPathBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.SoundFXListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.SoundFXListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.SoundFXListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SoundFXListBox.FormattingEnabled = true;
            this.SoundFXListBox.ItemHeight = 16;
            this.SoundFXListBox.Location = new System.Drawing.Point(3, 18);
            this.SoundFXListBox.Margin = new System.Windows.Forms.Padding(4);
            this.SoundFXListBox.Name = "SoundFXListBox";
            this.SoundFXListBox.Size = new System.Drawing.Size(399, 503);
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
            // MusicPage
            // 
            this.MusicPage.Controls.Add(this.groupBox1);
            this.MusicPage.Location = new System.Drawing.Point(4, 25);
            this.MusicPage.Name = "MusicPage";
            this.MusicPage.Size = new System.Drawing.Size(704, 530);
            this.MusicPage.TabIndex = 3;
            this.MusicPage.Text = "Music";
            this.MusicPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.MusicBox);
            this.groupBox1.Controls.Add(this.DelMusButton);
            this.groupBox1.Controls.Add(this.MusicListBox);
            this.groupBox1.Controls.Add(this.AddMusButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 530);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Music List";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(410, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 68;
            this.label3.Text = "Music Path";
            // 
            // MusicBox
            // 
            this.MusicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MusicBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MusicBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MusicBox.Location = new System.Drawing.Point(413, 40);
            this.MusicBox.Margin = new System.Windows.Forms.Padding(4);
            this.MusicBox.Name = "MusicBox";
            this.MusicBox.Size = new System.Drawing.Size(284, 22);
            this.MusicBox.TabIndex = 67;
            this.MusicBox.TextChanged += new System.EventHandler(this.MusicBox_TextChanged);
            // 
            // DelMusButton
            // 
            this.DelMusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DelMusButton.Location = new System.Drawing.Point(414, 160);
            this.DelMusButton.Name = "DelMusButton";
            this.DelMusButton.Size = new System.Drawing.Size(284, 33);
            this.DelMusButton.TabIndex = 55;
            this.DelMusButton.Text = "Remove Music";
            this.DelMusButton.UseVisualStyleBackColor = true;
            this.DelMusButton.Click += new System.EventHandler(this.DelMusButton_Click);
            // 
            // MusicListBox
            // 
            this.MusicListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.MusicListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.MusicListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MusicListBox.FormattingEnabled = true;
            this.MusicListBox.ItemHeight = 16;
            this.MusicListBox.Location = new System.Drawing.Point(3, 18);
            this.MusicListBox.Margin = new System.Windows.Forms.Padding(4);
            this.MusicListBox.Name = "MusicListBox";
            this.MusicListBox.Size = new System.Drawing.Size(399, 509);
            this.MusicListBox.TabIndex = 1;
            this.MusicListBox.SelectedIndexChanged += new System.EventHandler(this.MusicListBox_SelectedIndexChanged);
            // 
            // AddMusButton
            // 
            this.AddMusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddMusButton.Location = new System.Drawing.Point(414, 116);
            this.AddMusButton.Name = "AddMusButton";
            this.AddMusButton.Size = new System.Drawing.Size(284, 33);
            this.AddMusButton.TabIndex = 54;
            this.AddMusButton.Text = "Add Music";
            this.AddMusButton.UseVisualStyleBackColor = true;
            this.AddMusButton.Click += new System.EventHandler(this.AddMusButton_Click);
            // 
            // SheetPage
            // 
            this.SheetPage.Controls.Add(this.groupBox3);
            this.SheetPage.Location = new System.Drawing.Point(4, 25);
            this.SheetPage.Name = "SheetPage";
            this.SheetPage.Size = new System.Drawing.Size(704, 530);
            this.SheetPage.TabIndex = 4;
            this.SheetPage.Text = "Spritesheets";
            this.SheetPage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.SheetPathBox);
            this.groupBox3.Controls.Add(this.DelSheetButton);
            this.groupBox3.Controls.Add(this.SheetListbox);
            this.groupBox3.Controls.Add(this.AddSheetButton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(704, 530);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sheet List";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(410, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 68;
            this.label1.Text = "Sheet Path";
            // 
            // SheetPathBox
            // 
            this.SheetPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SheetPathBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SheetPathBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SheetPathBox.Location = new System.Drawing.Point(413, 40);
            this.SheetPathBox.Margin = new System.Windows.Forms.Padding(4);
            this.SheetPathBox.Name = "SheetPathBox";
            this.SheetPathBox.Size = new System.Drawing.Size(284, 22);
            this.SheetPathBox.TabIndex = 67;
            this.SheetPathBox.TextChanged += new System.EventHandler(this.SheetPathBox_TextChanged);
            // 
            // DelSheetButton
            // 
            this.DelSheetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DelSheetButton.Location = new System.Drawing.Point(414, 160);
            this.DelSheetButton.Name = "DelSheetButton";
            this.DelSheetButton.Size = new System.Drawing.Size(284, 33);
            this.DelSheetButton.TabIndex = 55;
            this.DelSheetButton.Text = "Remove Sheet";
            this.DelSheetButton.UseVisualStyleBackColor = true;
            this.DelSheetButton.Click += new System.EventHandler(this.DelSheetButton_Click);
            // 
            // SheetListbox
            // 
            this.SheetListbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.SheetListbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.SheetListbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SheetListbox.FormattingEnabled = true;
            this.SheetListbox.ItemHeight = 16;
            this.SheetListbox.Location = new System.Drawing.Point(3, 18);
            this.SheetListbox.Margin = new System.Windows.Forms.Padding(4);
            this.SheetListbox.Name = "SheetListbox";
            this.SheetListbox.Size = new System.Drawing.Size(399, 509);
            this.SheetListbox.TabIndex = 1;
            this.SheetListbox.SelectedIndexChanged += new System.EventHandler(this.SheetListbox_SelectedIndexChanged);
            // 
            // AddSheetButton
            // 
            this.AddSheetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddSheetButton.Location = new System.Drawing.Point(414, 116);
            this.AddSheetButton.Name = "AddSheetButton";
            this.AddSheetButton.Size = new System.Drawing.Size(284, 33);
            this.AddSheetButton.TabIndex = 54;
            this.AddSheetButton.Text = "Add Sheet";
            this.AddSheetButton.UseVisualStyleBackColor = true;
            this.AddSheetButton.Click += new System.EventHandler(this.AddSheetButton_Click);
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
            this.ClientSize = new System.Drawing.Size(712, 559);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "RSDKvRS Stageconfig Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.ObjectsPage.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SheetIDNUD)).EndInit();
            this.SoundFXPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MusicPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.SheetPage.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ObjectsPage;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label12;
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
        private System.Windows.Forms.NumericUpDown SheetIDNUD;
        private System.Windows.Forms.TabPage MusicPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MusicBox;
        private System.Windows.Forms.Button DelMusButton;
        private System.Windows.Forms.ListBox MusicListBox;
        private System.Windows.Forms.Button AddMusButton;
        private System.Windows.Forms.TabPage SheetPage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SheetPathBox;
        private System.Windows.Forms.Button DelSheetButton;
        private System.Windows.Forms.ListBox SheetListbox;
        private System.Windows.Forms.Button AddSheetButton;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem MenuItem_New;
        private System.Windows.Forms.MenuItem MenuItem_Open;
        private System.Windows.Forms.MenuItem MenuItem_Save;
        private System.Windows.Forms.MenuItem MenuItem_SaveAs;
    }
}

