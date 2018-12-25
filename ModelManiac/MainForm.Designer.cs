namespace ModelManiac
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
            this.HeaderBox = new System.Windows.Forms.GroupBox();
            this.QuadsCB = new System.Windows.Forms.CheckBox();
            this.ColoursCB = new System.Windows.Forms.CheckBox();
            this.UnknownCB = new System.Windows.Forms.CheckBox();
            this.NormalsCB = new System.Windows.Forms.CheckBox();
            this.VerticiesBox = new System.Windows.Forms.ListBox();
            this.FramesBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DeleteColourButton = new System.Windows.Forms.Button();
            this.AddColourButton = new System.Windows.Forms.Button();
            this.VertexWNUD = new System.Windows.Forms.NumericUpDown();
            this.UnknownYNUD = new System.Windows.Forms.NumericUpDown();
            this.UnknownXNUD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.DeleteFrameButton = new System.Windows.Forms.Button();
            this.AddFrameButton = new System.Windows.Forms.Button();
            this.DeleteVertexButton = new System.Windows.Forms.Button();
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.NormalZNUD = new System.Windows.Forms.NumericUpDown();
            this.NormalYNUD = new System.Windows.Forms.NumericUpDown();
            this.NormalXNUD = new System.Windows.Forms.NumericUpDown();
            this.VertexZNUD = new System.Windows.Forms.NumericUpDown();
            this.VertexYNUD = new System.Windows.Forms.NumericUpDown();
            this.VertexXNUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MeshColourEditor = new Cyotek.Windows.Forms.ColorEditor();
            this.MeshColourGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportColoursToactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importModelFromstlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HeaderBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VertexWNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnknownYNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnknownXNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalZNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalYNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalXNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VertexZNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VertexYNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VertexXNUD)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderBox
            // 
            this.HeaderBox.Controls.Add(this.QuadsCB);
            this.HeaderBox.Controls.Add(this.ColoursCB);
            this.HeaderBox.Controls.Add(this.UnknownCB);
            this.HeaderBox.Controls.Add(this.NormalsCB);
            this.HeaderBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderBox.Location = new System.Drawing.Point(159, 28);
            this.HeaderBox.Name = "HeaderBox";
            this.HeaderBox.Size = new System.Drawing.Size(607, 56);
            this.HeaderBox.TabIndex = 8;
            this.HeaderBox.TabStop = false;
            this.HeaderBox.Text = "Header Data";
            // 
            // QuadsCB
            // 
            this.QuadsCB.AutoSize = true;
            this.QuadsCB.Location = new System.Drawing.Point(356, 21);
            this.QuadsCB.Name = "QuadsCB";
            this.QuadsCB.Size = new System.Drawing.Size(101, 21);
            this.QuadsCB.TabIndex = 3;
            this.QuadsCB.Text = "Use Quads";
            this.QuadsCB.UseVisualStyleBackColor = true;
            this.QuadsCB.CheckedChanged += new System.EventHandler(this.QuadsCB_CheckedChanged);
            // 
            // ColoursCB
            // 
            this.ColoursCB.AutoSize = true;
            this.ColoursCB.Location = new System.Drawing.Point(243, 21);
            this.ColoursCB.Name = "ColoursCB";
            this.ColoursCB.Size = new System.Drawing.Size(107, 21);
            this.ColoursCB.TabIndex = 2;
            this.ColoursCB.Text = "Use Colours";
            this.ColoursCB.UseVisualStyleBackColor = true;
            this.ColoursCB.CheckedChanged += new System.EventHandler(this.ColoursCB_CheckedChanged);
            // 
            // UnknownCB
            // 
            this.UnknownCB.AutoSize = true;
            this.UnknownCB.Location = new System.Drawing.Point(126, 21);
            this.UnknownCB.Name = "UnknownCB";
            this.UnknownCB.Size = new System.Drawing.Size(117, 21);
            this.UnknownCB.TabIndex = 1;
            this.UnknownCB.Text = "Use Unknown";
            this.UnknownCB.UseVisualStyleBackColor = true;
            this.UnknownCB.CheckedChanged += new System.EventHandler(this.UnknownCB_CheckedChanged);
            // 
            // NormalsCB
            // 
            this.NormalsCB.AutoSize = true;
            this.NormalsCB.Location = new System.Drawing.Point(9, 21);
            this.NormalsCB.Name = "NormalsCB";
            this.NormalsCB.Size = new System.Drawing.Size(111, 21);
            this.NormalsCB.TabIndex = 0;
            this.NormalsCB.Text = "Use Normals";
            this.NormalsCB.UseVisualStyleBackColor = true;
            this.NormalsCB.CheckedChanged += new System.EventHandler(this.NormalsCB_CheckedChanged);
            // 
            // VerticiesBox
            // 
            this.VerticiesBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.VerticiesBox.FormattingEnabled = true;
            this.VerticiesBox.ItemHeight = 16;
            this.VerticiesBox.Location = new System.Drawing.Point(766, 28);
            this.VerticiesBox.Name = "VerticiesBox";
            this.VerticiesBox.Size = new System.Drawing.Size(175, 486);
            this.VerticiesBox.TabIndex = 7;
            this.VerticiesBox.SelectedIndexChanged += new System.EventHandler(this.VerticiesBox_SelectedIndexChanged);
            // 
            // FramesBox
            // 
            this.FramesBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.FramesBox.FormattingEnabled = true;
            this.FramesBox.ItemHeight = 16;
            this.FramesBox.Location = new System.Drawing.Point(0, 28);
            this.FramesBox.Name = "FramesBox";
            this.FramesBox.Size = new System.Drawing.Size(159, 486);
            this.FramesBox.TabIndex = 6;
            this.FramesBox.SelectedIndexChanged += new System.EventHandler(this.FramesBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 486);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Face Data";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.DeleteColourButton);
            this.groupBox3.Controls.Add(this.AddColourButton);
            this.groupBox3.Controls.Add(this.VertexWNUD);
            this.groupBox3.Controls.Add(this.UnknownYNUD);
            this.groupBox3.Controls.Add(this.UnknownXNUD);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.DeleteFrameButton);
            this.groupBox3.Controls.Add(this.AddFrameButton);
            this.groupBox3.Controls.Add(this.DeleteVertexButton);
            this.groupBox3.Controls.Add(this.AddVertexButton);
            this.groupBox3.Controls.Add(this.NormalZNUD);
            this.groupBox3.Controls.Add(this.NormalYNUD);
            this.groupBox3.Controls.Add(this.NormalXNUD);
            this.groupBox3.Controls.Add(this.VertexZNUD);
            this.groupBox3.Controls.Add(this.VertexYNUD);
            this.groupBox3.Controls.Add(this.VertexXNUD);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(168, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(592, 225);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mesh Data";
            // 
            // DeleteColourButton
            // 
            this.DeleteColourButton.Location = new System.Drawing.Point(479, 99);
            this.DeleteColourButton.Name = "DeleteColourButton";
            this.DeleteColourButton.Size = new System.Drawing.Size(107, 40);
            this.DeleteColourButton.TabIndex = 39;
            this.DeleteColourButton.Text = "Delete Colour";
            this.DeleteColourButton.UseVisualStyleBackColor = true;
            this.DeleteColourButton.Click += new System.EventHandler(this.DeleteColourButton_Click);
            // 
            // AddColourButton
            // 
            this.AddColourButton.Location = new System.Drawing.Point(366, 99);
            this.AddColourButton.Name = "AddColourButton";
            this.AddColourButton.Size = new System.Drawing.Size(107, 40);
            this.AddColourButton.TabIndex = 38;
            this.AddColourButton.Text = "Add Colour";
            this.AddColourButton.UseVisualStyleBackColor = true;
            this.AddColourButton.Click += new System.EventHandler(this.AddColourButton_Click);
            // 
            // VertexWNUD
            // 
            this.VertexWNUD.DecimalPlaces = 10;
            this.VertexWNUD.Location = new System.Drawing.Point(480, 71);
            this.VertexWNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.VertexWNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.VertexWNUD.Name = "VertexWNUD";
            this.VertexWNUD.Size = new System.Drawing.Size(108, 22);
            this.VertexWNUD.TabIndex = 37;
            this.VertexWNUD.ThousandsSeparator = true;
            this.VertexWNUD.ValueChanged += new System.EventHandler(this.VertexWNUD_ValueChanged);
            // 
            // UnknownYNUD
            // 
            this.UnknownYNUD.DecimalPlaces = 10;
            this.UnknownYNUD.Location = new System.Drawing.Point(252, 109);
            this.UnknownYNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.UnknownYNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.UnknownYNUD.Name = "UnknownYNUD";
            this.UnknownYNUD.Size = new System.Drawing.Size(108, 22);
            this.UnknownYNUD.TabIndex = 36;
            this.UnknownYNUD.ThousandsSeparator = true;
            this.UnknownYNUD.ValueChanged += new System.EventHandler(this.UnknownYNUD_ValueChanged);
            // 
            // UnknownXNUD
            // 
            this.UnknownXNUD.DecimalPlaces = 10;
            this.UnknownXNUD.Location = new System.Drawing.Point(138, 109);
            this.UnknownXNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.UnknownXNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.UnknownXNUD.Name = "UnknownXNUD";
            this.UnknownXNUD.Size = new System.Drawing.Size(108, 22);
            this.UnknownXNUD.TabIndex = 35;
            this.UnknownXNUD.ThousandsSeparator = true;
            this.UnknownXNUD.ValueChanged += new System.EventHandler(this.UnknownXNUD_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "Current Unknown:";
            // 
            // DeleteFrameButton
            // 
            this.DeleteFrameButton.Location = new System.Drawing.Point(336, 159);
            this.DeleteFrameButton.Name = "DeleteFrameButton";
            this.DeleteFrameButton.Size = new System.Drawing.Size(107, 40);
            this.DeleteFrameButton.TabIndex = 33;
            this.DeleteFrameButton.Text = "Delete Frame";
            this.DeleteFrameButton.UseVisualStyleBackColor = true;
            this.DeleteFrameButton.Click += new System.EventHandler(this.DeleteFrameButton_Click);
            // 
            // AddFrameButton
            // 
            this.AddFrameButton.Location = new System.Drawing.Point(233, 159);
            this.AddFrameButton.Name = "AddFrameButton";
            this.AddFrameButton.Size = new System.Drawing.Size(97, 40);
            this.AddFrameButton.TabIndex = 32;
            this.AddFrameButton.Text = "Add Frame";
            this.AddFrameButton.UseVisualStyleBackColor = true;
            this.AddFrameButton.Click += new System.EventHandler(this.AddFrameButton_Click);
            // 
            // DeleteVertexButton
            // 
            this.DeleteVertexButton.Location = new System.Drawing.Point(120, 159);
            this.DeleteVertexButton.Name = "DeleteVertexButton";
            this.DeleteVertexButton.Size = new System.Drawing.Size(107, 40);
            this.DeleteVertexButton.TabIndex = 31;
            this.DeleteVertexButton.Text = "Delete Vertex";
            this.DeleteVertexButton.UseVisualStyleBackColor = true;
            this.DeleteVertexButton.Click += new System.EventHandler(this.DeleteVertexButton_Click);
            // 
            // AddVertexButton
            // 
            this.AddVertexButton.Location = new System.Drawing.Point(17, 159);
            this.AddVertexButton.Name = "AddVertexButton";
            this.AddVertexButton.Size = new System.Drawing.Size(97, 40);
            this.AddVertexButton.TabIndex = 30;
            this.AddVertexButton.Text = "Add Vertex";
            this.AddVertexButton.UseVisualStyleBackColor = true;
            this.AddVertexButton.Click += new System.EventHandler(this.AddVertexButton_Click);
            // 
            // NormalZNUD
            // 
            this.NormalZNUD.DecimalPlaces = 10;
            this.NormalZNUD.Location = new System.Drawing.Point(366, 36);
            this.NormalZNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NormalZNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.NormalZNUD.Name = "NormalZNUD";
            this.NormalZNUD.Size = new System.Drawing.Size(111, 22);
            this.NormalZNUD.TabIndex = 29;
            this.NormalZNUD.ThousandsSeparator = true;
            this.NormalZNUD.ValueChanged += new System.EventHandler(this.NormalZNUD_ValueChanged);
            // 
            // NormalYNUD
            // 
            this.NormalYNUD.DecimalPlaces = 10;
            this.NormalYNUD.Location = new System.Drawing.Point(252, 36);
            this.NormalYNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NormalYNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.NormalYNUD.Name = "NormalYNUD";
            this.NormalYNUD.Size = new System.Drawing.Size(107, 22);
            this.NormalYNUD.TabIndex = 28;
            this.NormalYNUD.ThousandsSeparator = true;
            this.NormalYNUD.ValueChanged += new System.EventHandler(this.NormalYNUD_ValueChanged);
            // 
            // NormalXNUD
            // 
            this.NormalXNUD.DecimalPlaces = 10;
            this.NormalXNUD.Location = new System.Drawing.Point(138, 36);
            this.NormalXNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NormalXNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.NormalXNUD.Name = "NormalXNUD";
            this.NormalXNUD.Size = new System.Drawing.Size(106, 22);
            this.NormalXNUD.TabIndex = 27;
            this.NormalXNUD.ThousandsSeparator = true;
            this.NormalXNUD.ValueChanged += new System.EventHandler(this.NormalXNUD_ValueChanged);
            // 
            // VertexZNUD
            // 
            this.VertexZNUD.DecimalPlaces = 10;
            this.VertexZNUD.Location = new System.Drawing.Point(366, 71);
            this.VertexZNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.VertexZNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.VertexZNUD.Name = "VertexZNUD";
            this.VertexZNUD.Size = new System.Drawing.Size(108, 22);
            this.VertexZNUD.TabIndex = 26;
            this.VertexZNUD.ThousandsSeparator = true;
            this.VertexZNUD.ValueChanged += new System.EventHandler(this.VertexZNUD_ValueChanged);
            // 
            // VertexYNUD
            // 
            this.VertexYNUD.DecimalPlaces = 10;
            this.VertexYNUD.Location = new System.Drawing.Point(252, 71);
            this.VertexYNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.VertexYNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.VertexYNUD.Name = "VertexYNUD";
            this.VertexYNUD.Size = new System.Drawing.Size(108, 22);
            this.VertexYNUD.TabIndex = 25;
            this.VertexYNUD.ThousandsSeparator = true;
            this.VertexYNUD.ValueChanged += new System.EventHandler(this.VertexYNUD_ValueChanged);
            // 
            // VertexXNUD
            // 
            this.VertexXNUD.DecimalPlaces = 10;
            this.VertexXNUD.Location = new System.Drawing.Point(138, 71);
            this.VertexXNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.VertexXNUD.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.VertexXNUD.Name = "VertexXNUD";
            this.VertexXNUD.Size = new System.Drawing.Size(108, 22);
            this.VertexXNUD.TabIndex = 24;
            this.VertexXNUD.ThousandsSeparator = true;
            this.VertexXNUD.ValueChanged += new System.EventHandler(this.VertexXNUD_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Current Vertex:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Current Normal:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.MeshColourEditor);
            this.groupBox2.Controls.Add(this.MeshColourGrid);
            this.groupBox2.Location = new System.Drawing.Point(165, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 206);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Colour Data";
            // 
            // MeshColourEditor
            // 
            this.MeshColourEditor.Dock = System.Windows.Forms.DockStyle.Right;
            this.MeshColourEditor.Location = new System.Drawing.Point(392, 18);
            this.MeshColourEditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MeshColourEditor.Name = "MeshColourEditor";
            this.MeshColourEditor.Size = new System.Drawing.Size(200, 185);
            this.MeshColourEditor.TabIndex = 1;
            this.MeshColourEditor.ColorChanged += new System.EventHandler(this.MeshColourEditor_ColorChanged);
            // 
            // MeshColourGrid
            // 
            this.MeshColourGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.MeshColourGrid.Location = new System.Drawing.Point(3, 18);
            this.MeshColourGrid.Name = "MeshColourGrid";
            this.MeshColourGrid.Palette = Cyotek.Windows.Forms.ColorPalette.None;
            this.MeshColourGrid.ShowCustomColors = false;
            this.MeshColourGrid.Size = new System.Drawing.Size(295, 25);
            this.MeshColourGrid.TabIndex = 0;
            this.MeshColourGrid.ColorChanged += new System.EventHandler(this.MeshColourGrid_ColorChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(941, 28);
            this.menuStrip1.TabIndex = 10;
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportColoursToactToolStripMenuItem,
            this.importModelFromstlToolStripMenuItem,
            this.exportToToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // exportColoursToactToolStripMenuItem
            // 
            this.exportColoursToactToolStripMenuItem.Name = "exportColoursToactToolStripMenuItem";
            this.exportColoursToactToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.exportColoursToactToolStripMenuItem.Text = "Export Colours to .act";
            this.exportColoursToactToolStripMenuItem.Click += new System.EventHandler(this.exportColoursToactToolStripMenuItem_Click);
            // 
            // importModelFromstlToolStripMenuItem
            // 
            this.importModelFromstlToolStripMenuItem.Name = "importModelFromstlToolStripMenuItem";
            this.importModelFromstlToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.importModelFromstlToolStripMenuItem.Text = "Import Model from...";
            this.importModelFromstlToolStripMenuItem.Click += new System.EventHandler(this.importModelFromstlToolStripMenuItem_Click);
            // 
            // exportToToolStripMenuItem
            // 
            this.exportToToolStripMenuItem.Name = "exportToToolStripMenuItem";
            this.exportToToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.exportToToolStripMenuItem.Text = "Export Model To...";
            this.exportToToolStripMenuItem.Click += new System.EventHandler(this.exportToToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 514);
            this.Controls.Add(this.HeaderBox);
            this.Controls.Add(this.VerticiesBox);
            this.Controls.Add(this.FramesBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Model Maniac";
            this.HeaderBox.ResumeLayout(false);
            this.HeaderBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VertexWNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnknownYNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnknownXNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalZNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalYNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalXNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VertexZNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VertexYNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VertexXNUD)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox HeaderBox;
        private System.Windows.Forms.CheckBox NormalsCB;
        private System.Windows.Forms.ListBox VerticiesBox;
        private System.Windows.Forms.ListBox FramesBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button DeleteVertexButton;
        private System.Windows.Forms.Button AddVertexButton;
        private System.Windows.Forms.NumericUpDown NormalZNUD;
        private System.Windows.Forms.NumericUpDown NormalYNUD;
        private System.Windows.Forms.NumericUpDown NormalXNUD;
        private System.Windows.Forms.NumericUpDown VertexZNUD;
        private System.Windows.Forms.NumericUpDown VertexYNUD;
        private System.Windows.Forms.NumericUpDown VertexXNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private Cyotek.Windows.Forms.ColorEditor MeshColourEditor;
        private Cyotek.Windows.Forms.ColorGrid MeshColourGrid;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportColoursToactToolStripMenuItem;
        private System.Windows.Forms.CheckBox QuadsCB;
        private System.Windows.Forms.CheckBox ColoursCB;
        private System.Windows.Forms.CheckBox UnknownCB;
        private System.Windows.Forms.ToolStripMenuItem importModelFromstlToolStripMenuItem;
        private System.Windows.Forms.Button DeleteFrameButton;
        private System.Windows.Forms.Button AddFrameButton;
        private System.Windows.Forms.ToolStripMenuItem exportToToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown UnknownYNUD;
        private System.Windows.Forms.NumericUpDown UnknownXNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown VertexWNUD;
        private System.Windows.Forms.Button DeleteColourButton;
        private System.Windows.Forms.Button AddColourButton;
    }
}

