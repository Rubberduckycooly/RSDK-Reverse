namespace RetroED.Tools.ChunkMappingsEditor
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renderEachChunkAsAnImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orientationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualPlaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tile16x16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setAutoOrientationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAutoVisualPlaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAutoCollisionAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAutoCollisionBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAutoTile16x16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyChunkToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.CollisionBBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CollisionABox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VisualBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OrientationBox = new System.Windows.Forms.ComboBox();
            this.OrientationLabel = new System.Windows.Forms.Label();
            this.ChunkDisplay = new System.Windows.Forms.Panel();
            this.CurChunkChangePanel = new System.Windows.Forms.Panel();
            this.GotoButton = new System.Windows.Forms.Button();
            this.GotoNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ChunkNoLabel = new System.Windows.Forms.Label();
            this.PrevChunkButton = new System.Windows.Forms.Button();
            this.NextChunkButton = new System.Windows.Forms.Button();
            this.StageTilesList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.OptionsPanel.SuspendLayout();
            this.CurChunkChangePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GotoNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.renderEachChunkAsAnImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(291, 6);
            // 
            // renderEachChunkAsAnImageToolStripMenuItem
            // 
            this.renderEachChunkAsAnImageToolStripMenuItem.Name = "renderEachChunkAsAnImageToolStripMenuItem";
            this.renderEachChunkAsAnImageToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.renderEachChunkAsAnImageToolStripMenuItem.Text = "Render Each Chunk as an Image";
            this.renderEachChunkAsAnImageToolStripMenuItem.Click += new System.EventHandler(this.renderEachChunkAsAnImageToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGridToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.Checked = true;
            this.showGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.showGridToolStripMenuItem.Text = "Show Grid";
            this.showGridToolStripMenuItem.Click += new System.EventHandler(this.showGridToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoSetToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyChunkToToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // autoSetToolStripMenuItem
            // 
            this.autoSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orientationToolStripMenuItem,
            this.visualPlaneToolStripMenuItem,
            this.collisionAToolStripMenuItem,
            this.collisionBToolStripMenuItem,
            this.tile16x16ToolStripMenuItem,
            this.toolStripSeparator2,
            this.setAutoOrientationToolStripMenuItem,
            this.setAutoVisualPlaneToolStripMenuItem,
            this.setAutoCollisionAToolStripMenuItem,
            this.setAutoCollisionBToolStripMenuItem,
            this.setAutoTile16x16ToolStripMenuItem});
            this.autoSetToolStripMenuItem.Name = "autoSetToolStripMenuItem";
            this.autoSetToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.autoSetToolStripMenuItem.Text = "\"Auto-Set\"";
            // 
            // orientationToolStripMenuItem
            // 
            this.orientationToolStripMenuItem.Name = "orientationToolStripMenuItem";
            this.orientationToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.orientationToolStripMenuItem.Text = "Orientation";
            this.orientationToolStripMenuItem.Click += new System.EventHandler(this.orientationToolStripMenuItem_Click);
            // 
            // visualPlaneToolStripMenuItem
            // 
            this.visualPlaneToolStripMenuItem.Name = "visualPlaneToolStripMenuItem";
            this.visualPlaneToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.visualPlaneToolStripMenuItem.Text = "Visual Plane";
            this.visualPlaneToolStripMenuItem.Click += new System.EventHandler(this.visualPlaneToolStripMenuItem_Click);
            // 
            // collisionAToolStripMenuItem
            // 
            this.collisionAToolStripMenuItem.Name = "collisionAToolStripMenuItem";
            this.collisionAToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.collisionAToolStripMenuItem.Text = "Collision A";
            this.collisionAToolStripMenuItem.Click += new System.EventHandler(this.collisionAToolStripMenuItem_Click);
            // 
            // collisionBToolStripMenuItem
            // 
            this.collisionBToolStripMenuItem.Name = "collisionBToolStripMenuItem";
            this.collisionBToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.collisionBToolStripMenuItem.Text = "Collision B";
            this.collisionBToolStripMenuItem.Click += new System.EventHandler(this.collisionBToolStripMenuItem_Click);
            // 
            // tile16x16ToolStripMenuItem
            // 
            this.tile16x16ToolStripMenuItem.Name = "tile16x16ToolStripMenuItem";
            this.tile16x16ToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.tile16x16ToolStripMenuItem.Text = "Tile16x16";
            this.tile16x16ToolStripMenuItem.Click += new System.EventHandler(this.tile16x16ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // setAutoOrientationToolStripMenuItem
            // 
            this.setAutoOrientationToolStripMenuItem.Name = "setAutoOrientationToolStripMenuItem";
            this.setAutoOrientationToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.setAutoOrientationToolStripMenuItem.Text = "Set Auto-Orientation";
            this.setAutoOrientationToolStripMenuItem.Click += new System.EventHandler(this.setAutoOrientationToolStripMenuItem_Click);
            // 
            // setAutoVisualPlaneToolStripMenuItem
            // 
            this.setAutoVisualPlaneToolStripMenuItem.Name = "setAutoVisualPlaneToolStripMenuItem";
            this.setAutoVisualPlaneToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.setAutoVisualPlaneToolStripMenuItem.Text = "Set Auto-Visual Plane";
            this.setAutoVisualPlaneToolStripMenuItem.Click += new System.EventHandler(this.setAutoVisualPlaneToolStripMenuItem_Click);
            // 
            // setAutoCollisionAToolStripMenuItem
            // 
            this.setAutoCollisionAToolStripMenuItem.Name = "setAutoCollisionAToolStripMenuItem";
            this.setAutoCollisionAToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.setAutoCollisionAToolStripMenuItem.Text = "Set Auto-Collision A";
            this.setAutoCollisionAToolStripMenuItem.Click += new System.EventHandler(this.setAutoCollisionAToolStripMenuItem_Click);
            // 
            // setAutoCollisionBToolStripMenuItem
            // 
            this.setAutoCollisionBToolStripMenuItem.Name = "setAutoCollisionBToolStripMenuItem";
            this.setAutoCollisionBToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.setAutoCollisionBToolStripMenuItem.Text = "Set Auto-Collision B";
            this.setAutoCollisionBToolStripMenuItem.Click += new System.EventHandler(this.setAutoCollisionBToolStripMenuItem_Click);
            // 
            // setAutoTile16x16ToolStripMenuItem
            // 
            this.setAutoTile16x16ToolStripMenuItem.Name = "setAutoTile16x16ToolStripMenuItem";
            this.setAutoTile16x16ToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.setAutoTile16x16ToolStripMenuItem.Text = "Set Auto-Tile16x16";
            this.setAutoTile16x16ToolStripMenuItem.Click += new System.EventHandler(this.setAutoTile16x16ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
            // 
            // copyChunkToToolStripMenuItem
            // 
            this.copyChunkToToolStripMenuItem.Name = "copyChunkToToolStripMenuItem";
            this.copyChunkToToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.copyChunkToToolStripMenuItem.Text = "Copy Chunk to...";
            this.copyChunkToToolStripMenuItem.Click += new System.EventHandler(this.copyChunkToToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.StageTilesList);
            this.splitContainer1.Size = new System.Drawing.Size(982, 425);
            this.splitContainer1.SplitterDistance = 736;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.OptionsPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ChunkDisplay);
            this.splitContainer2.Size = new System.Drawing.Size(736, 425);
            this.splitContainer2.SplitterDistance = 306;
            this.splitContainer2.TabIndex = 0;
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.CurChunkChangePanel);
            this.OptionsPanel.Controls.Add(this.CollisionBBox);
            this.OptionsPanel.Controls.Add(this.label3);
            this.OptionsPanel.Controls.Add(this.CollisionABox);
            this.OptionsPanel.Controls.Add(this.label2);
            this.OptionsPanel.Controls.Add(this.VisualBox);
            this.OptionsPanel.Controls.Add(this.label1);
            this.OptionsPanel.Controls.Add(this.OrientationBox);
            this.OptionsPanel.Controls.Add(this.OrientationLabel);
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsPanel.Location = new System.Drawing.Point(0, 0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(306, 425);
            this.OptionsPanel.TabIndex = 0;
            // 
            // CollisionBBox
            // 
            this.CollisionBBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CollisionBBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollisionBBox.FormattingEnabled = true;
            this.CollisionBBox.Items.AddRange(new object[] {
            "All",
            "Top",
            "All But Top",
            "None"});
            this.CollisionBBox.Location = new System.Drawing.Point(16, 223);
            this.CollisionBBox.Name = "CollisionBBox";
            this.CollisionBBox.Size = new System.Drawing.Size(171, 24);
            this.CollisionBBox.TabIndex = 7;
            this.CollisionBBox.SelectedIndexChanged += new System.EventHandler(this.CollisionBBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Collision Properties (Plane B)";
            // 
            // CollisionABox
            // 
            this.CollisionABox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CollisionABox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollisionABox.FormattingEnabled = true;
            this.CollisionABox.Items.AddRange(new object[] {
            "All",
            "Top",
            "All But Top",
            "None"});
            this.CollisionABox.Location = new System.Drawing.Point(16, 159);
            this.CollisionABox.Name = "CollisionABox";
            this.CollisionABox.Size = new System.Drawing.Size(171, 24);
            this.CollisionABox.TabIndex = 5;
            this.CollisionABox.SelectedIndexChanged += new System.EventHandler(this.CollisionABox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Collision Properties (Plane A)";
            // 
            // VisualBox
            // 
            this.VisualBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.VisualBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VisualBox.FormattingEnabled = true;
            this.VisualBox.Items.AddRange(new object[] {
            "Low Plane",
            "High Plane"});
            this.VisualBox.Location = new System.Drawing.Point(16, 98);
            this.VisualBox.Name = "VisualBox";
            this.VisualBox.Size = new System.Drawing.Size(171, 24);
            this.VisualBox.TabIndex = 3;
            this.VisualBox.SelectedIndexChanged += new System.EventHandler(this.VisualBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Visual Plane";
            // 
            // OrientationBox
            // 
            this.OrientationBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.OrientationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OrientationBox.FormattingEnabled = true;
            this.OrientationBox.Items.AddRange(new object[] {
            "Normal",
            "Flip Horizontally",
            "Flip Vertically",
            "Flip Both"});
            this.OrientationBox.Location = new System.Drawing.Point(16, 30);
            this.OrientationBox.Name = "OrientationBox";
            this.OrientationBox.Size = new System.Drawing.Size(171, 24);
            this.OrientationBox.TabIndex = 1;
            this.OrientationBox.SelectedIndexChanged += new System.EventHandler(this.OrientationBox_SelectedIndexChanged);
            // 
            // OrientationLabel
            // 
            this.OrientationLabel.AutoSize = true;
            this.OrientationLabel.Location = new System.Drawing.Point(13, 9);
            this.OrientationLabel.Name = "OrientationLabel";
            this.OrientationLabel.Size = new System.Drawing.Size(78, 17);
            this.OrientationLabel.TabIndex = 0;
            this.OrientationLabel.Text = "Orientation";
            // 
            // ChunkDisplay
            // 
            this.ChunkDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ChunkDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChunkDisplay.Location = new System.Drawing.Point(0, 0);
            this.ChunkDisplay.Name = "ChunkDisplay";
            this.ChunkDisplay.Size = new System.Drawing.Size(426, 425);
            this.ChunkDisplay.TabIndex = 3;
            // 
            // CurChunkChangePanel
            // 
            this.CurChunkChangePanel.Controls.Add(this.GotoButton);
            this.CurChunkChangePanel.Controls.Add(this.GotoNUD);
            this.CurChunkChangePanel.Controls.Add(this.label4);
            this.CurChunkChangePanel.Controls.Add(this.ChunkNoLabel);
            this.CurChunkChangePanel.Controls.Add(this.PrevChunkButton);
            this.CurChunkChangePanel.Controls.Add(this.NextChunkButton);
            this.CurChunkChangePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CurChunkChangePanel.Location = new System.Drawing.Point(0, 372);
            this.CurChunkChangePanel.Name = "CurChunkChangePanel";
            this.CurChunkChangePanel.Size = new System.Drawing.Size(306, 53);
            this.CurChunkChangePanel.TabIndex = 8;
            // 
            // GotoButton
            // 
            this.GotoButton.Location = new System.Drawing.Point(194, 28);
            this.GotoButton.Name = "GotoButton";
            this.GotoButton.Size = new System.Drawing.Size(107, 23);
            this.GotoButton.TabIndex = 5;
            this.GotoButton.Text = "GO!";
            this.GotoButton.UseVisualStyleBackColor = true;
            this.GotoButton.Click += new System.EventHandler(this.GotoButton_Click);
            // 
            // GotoNUD
            // 
            this.GotoNUD.Location = new System.Drawing.Point(247, 6);
            this.GotoNUD.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.GotoNUD.Name = "GotoNUD";
            this.GotoNUD.Size = new System.Drawing.Size(54, 22);
            this.GotoNUD.TabIndex = 4;
            this.GotoNUD.ValueChanged += new System.EventHandler(this.GotoNUD_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Go to chunk";
            // 
            // ChunkNoLabel
            // 
            this.ChunkNoLabel.AutoSize = true;
            this.ChunkNoLabel.Location = new System.Drawing.Point(4, 6);
            this.ChunkNoLabel.Name = "ChunkNoLabel";
            this.ChunkNoLabel.Size = new System.Drawing.Size(95, 17);
            this.ChunkNoLabel.TabIndex = 2;
            this.ChunkNoLabel.Text = "Chunk 0 Of 0:";
            // 
            // PrevChunkButton
            // 
            this.PrevChunkButton.BackColor = System.Drawing.SystemColors.Control;
            this.PrevChunkButton.Location = new System.Drawing.Point(3, 28);
            this.PrevChunkButton.Name = "PrevChunkButton";
            this.PrevChunkButton.Size = new System.Drawing.Size(89, 23);
            this.PrevChunkButton.TabIndex = 1;
            this.PrevChunkButton.Text = "Prev Chunk";
            this.PrevChunkButton.UseVisualStyleBackColor = false;
            this.PrevChunkButton.Click += new System.EventHandler(this.PrevChunkButton_Click);
            // 
            // NextChunkButton
            // 
            this.NextChunkButton.BackColor = System.Drawing.SystemColors.Control;
            this.NextChunkButton.Location = new System.Drawing.Point(98, 28);
            this.NextChunkButton.Name = "NextChunkButton";
            this.NextChunkButton.Size = new System.Drawing.Size(89, 23);
            this.NextChunkButton.TabIndex = 0;
            this.NextChunkButton.Text = "Next Chunk";
            this.NextChunkButton.UseVisualStyleBackColor = false;
            this.NextChunkButton.Click += new System.EventHandler(this.NextChunkButton_Click);
            // 
            // StageTilesList
            // 
            this.StageTilesList.BackColor = System.Drawing.SystemColors.ControlDark;
            this.StageTilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StageTilesList.Location = new System.Drawing.Point(0, 0);
            this.StageTilesList.Margin = new System.Windows.Forms.Padding(5);
            this.StageTilesList.Name = "StageTilesList";
            this.StageTilesList.ScrollValue = 0;
            this.StageTilesList.SelectedIndex = -1;
            this.StageTilesList.Size = new System.Drawing.Size(242, 425);
            this.StageTilesList.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(982, 453);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "RSDK Chunk Mapping Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.CurChunkChangePanel.ResumeLayout(false);
            this.CurChunkChangePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GotoNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.ComboBox CollisionBBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CollisionABox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox VisualBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox OrientationBox;
        private System.Windows.Forms.Label OrientationLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem renderEachChunkAsAnImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orientationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualPlaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collisionAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collisionBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tile16x16ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem setAutoOrientationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAutoVisualPlaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAutoCollisionAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAutoCollisionBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAutoTile16x16ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem copyChunkToToolStripMenuItem;
        private System.Windows.Forms.Panel ChunkDisplay;
        private TileList StageTilesList;
        private System.Windows.Forms.Panel CurChunkChangePanel;
        private System.Windows.Forms.Button GotoButton;
        private System.Windows.Forms.NumericUpDown GotoNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ChunkNoLabel;
        private System.Windows.Forms.Button PrevChunkButton;
        private System.Windows.Forms.Button NextChunkButton;
    }
}

