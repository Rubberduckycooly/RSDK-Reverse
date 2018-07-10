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
            this.CurChunkChangePanel = new System.Windows.Forms.Panel();
            this.GotoButton = new System.Windows.Forms.Button();
            this.GotoNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ChunkNoLabel = new System.Windows.Forms.Label();
            this.PrevChunkButton = new System.Windows.Forms.Button();
            this.NextChunkButton = new System.Windows.Forms.Button();
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
            this.ChunkLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TilesLabel = new System.Windows.Forms.Label();
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
            this.CurChunkChangePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GotoNUD)).BeginInit();
            this.OptionsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(736, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(240, 6);
            // 
            // renderEachChunkAsAnImageToolStripMenuItem
            // 
            this.renderEachChunkAsAnImageToolStripMenuItem.Name = "renderEachChunkAsAnImageToolStripMenuItem";
            this.renderEachChunkAsAnImageToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.renderEachChunkAsAnImageToolStripMenuItem.Text = "Render Each Chunk as an Image";
            this.renderEachChunkAsAnImageToolStripMenuItem.Click += new System.EventHandler(this.renderEachChunkAsAnImageToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGridToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.Checked = true;
            this.showGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
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
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
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
            this.autoSetToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.autoSetToolStripMenuItem.Text = "\"Auto-Set\"";
            // 
            // orientationToolStripMenuItem
            // 
            this.orientationToolStripMenuItem.Name = "orientationToolStripMenuItem";
            this.orientationToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.orientationToolStripMenuItem.Text = "Orientation";
            this.orientationToolStripMenuItem.Click += new System.EventHandler(this.orientationToolStripMenuItem_Click);
            // 
            // visualPlaneToolStripMenuItem
            // 
            this.visualPlaneToolStripMenuItem.Name = "visualPlaneToolStripMenuItem";
            this.visualPlaneToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.visualPlaneToolStripMenuItem.Text = "Visual Plane";
            this.visualPlaneToolStripMenuItem.Click += new System.EventHandler(this.visualPlaneToolStripMenuItem_Click);
            // 
            // collisionAToolStripMenuItem
            // 
            this.collisionAToolStripMenuItem.Name = "collisionAToolStripMenuItem";
            this.collisionAToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.collisionAToolStripMenuItem.Text = "Collision A";
            this.collisionAToolStripMenuItem.Click += new System.EventHandler(this.collisionAToolStripMenuItem_Click);
            // 
            // collisionBToolStripMenuItem
            // 
            this.collisionBToolStripMenuItem.Name = "collisionBToolStripMenuItem";
            this.collisionBToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.collisionBToolStripMenuItem.Text = "Collision B";
            this.collisionBToolStripMenuItem.Click += new System.EventHandler(this.collisionBToolStripMenuItem_Click);
            // 
            // tile16x16ToolStripMenuItem
            // 
            this.tile16x16ToolStripMenuItem.Name = "tile16x16ToolStripMenuItem";
            this.tile16x16ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.tile16x16ToolStripMenuItem.Text = "Tile16x16";
            this.tile16x16ToolStripMenuItem.Click += new System.EventHandler(this.tile16x16ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // setAutoOrientationToolStripMenuItem
            // 
            this.setAutoOrientationToolStripMenuItem.Name = "setAutoOrientationToolStripMenuItem";
            this.setAutoOrientationToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setAutoOrientationToolStripMenuItem.Text = "Set Auto-Orientation";
            this.setAutoOrientationToolStripMenuItem.Click += new System.EventHandler(this.setAutoOrientationToolStripMenuItem_Click);
            // 
            // setAutoVisualPlaneToolStripMenuItem
            // 
            this.setAutoVisualPlaneToolStripMenuItem.Name = "setAutoVisualPlaneToolStripMenuItem";
            this.setAutoVisualPlaneToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setAutoVisualPlaneToolStripMenuItem.Text = "Set Auto-Visual Plane";
            this.setAutoVisualPlaneToolStripMenuItem.Click += new System.EventHandler(this.setAutoVisualPlaneToolStripMenuItem_Click);
            // 
            // setAutoCollisionAToolStripMenuItem
            // 
            this.setAutoCollisionAToolStripMenuItem.Name = "setAutoCollisionAToolStripMenuItem";
            this.setAutoCollisionAToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setAutoCollisionAToolStripMenuItem.Text = "Set Auto-Collision A";
            this.setAutoCollisionAToolStripMenuItem.Click += new System.EventHandler(this.setAutoCollisionAToolStripMenuItem_Click);
            // 
            // setAutoCollisionBToolStripMenuItem
            // 
            this.setAutoCollisionBToolStripMenuItem.Name = "setAutoCollisionBToolStripMenuItem";
            this.setAutoCollisionBToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setAutoCollisionBToolStripMenuItem.Text = "Set Auto-Collision B";
            this.setAutoCollisionBToolStripMenuItem.Click += new System.EventHandler(this.setAutoCollisionBToolStripMenuItem_Click);
            // 
            // setAutoTile16x16ToolStripMenuItem
            // 
            this.setAutoTile16x16ToolStripMenuItem.Name = "setAutoTile16x16ToolStripMenuItem";
            this.setAutoTile16x16ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setAutoTile16x16ToolStripMenuItem.Text = "Set Auto-Tile16x16";
            this.setAutoTile16x16ToolStripMenuItem.Click += new System.EventHandler(this.setAutoTile16x16ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(160, 6);
            // 
            // copyChunkToToolStripMenuItem
            // 
            this.copyChunkToToolStripMenuItem.Name = "copyChunkToToolStripMenuItem";
            this.copyChunkToToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.copyChunkToToolStripMenuItem.Text = "Copy Chunk to...";
            this.copyChunkToToolStripMenuItem.Click += new System.EventHandler(this.copyChunkToToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.TilesLabel);
            this.splitContainer1.Size = new System.Drawing.Size(736, 344);
            this.splitContainer1.SplitterDistance = 551;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.CurChunkChangePanel);
            this.splitContainer2.Panel1.Controls.Add(this.OptionsPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ChunkDisplay);
            this.splitContainer2.Panel2.Controls.Add(this.ChunkLabel);
            this.splitContainer2.Panel2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Size = new System.Drawing.Size(551, 344);
            this.splitContainer2.SplitterDistance = 229;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // CurChunkChangePanel
            // 
            this.CurChunkChangePanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CurChunkChangePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurChunkChangePanel.Controls.Add(this.GotoButton);
            this.CurChunkChangePanel.Controls.Add(this.GotoNUD);
            this.CurChunkChangePanel.Controls.Add(this.label4);
            this.CurChunkChangePanel.Controls.Add(this.ChunkNoLabel);
            this.CurChunkChangePanel.Controls.Add(this.PrevChunkButton);
            this.CurChunkChangePanel.Controls.Add(this.NextChunkButton);
            this.CurChunkChangePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurChunkChangePanel.Location = new System.Drawing.Point(0, 284);
            this.CurChunkChangePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CurChunkChangePanel.Name = "CurChunkChangePanel";
            this.CurChunkChangePanel.Size = new System.Drawing.Size(229, 60);
            this.CurChunkChangePanel.TabIndex = 1;
            // 
            // GotoButton
            // 
            this.GotoButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.GotoButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GotoButton.ForeColor = System.Drawing.SystemColors.Control;
            this.GotoButton.Location = new System.Drawing.Point(146, 28);
            this.GotoButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GotoButton.Name = "GotoButton";
            this.GotoButton.Size = new System.Drawing.Size(80, 23);
            this.GotoButton.TabIndex = 5;
            this.GotoButton.Text = "GO!";
            this.GotoButton.UseVisualStyleBackColor = false;
            this.GotoButton.Click += new System.EventHandler(this.GotoButton_Click);
            // 
            // GotoNUD
            // 
            this.GotoNUD.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.GotoNUD.Location = new System.Drawing.Point(178, 5);
            this.GotoNUD.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GotoNUD.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.GotoNUD.Name = "GotoNUD";
            this.GotoNUD.Size = new System.Drawing.Size(47, 20);
            this.GotoNUD.TabIndex = 4;
            this.GotoNUD.ValueChanged += new System.EventHandler(this.GotoNUD_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(108, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Go to chunk";
            // 
            // ChunkNoLabel
            // 
            this.ChunkNoLabel.AutoSize = true;
            this.ChunkNoLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ChunkNoLabel.Location = new System.Drawing.Point(6, 7);
            this.ChunkNoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ChunkNoLabel.Name = "ChunkNoLabel";
            this.ChunkNoLabel.Size = new System.Drawing.Size(71, 13);
            this.ChunkNoLabel.TabIndex = 2;
            this.ChunkNoLabel.Text = "Chunk 0 of 0:";
            // 
            // PrevChunkButton
            // 
            this.PrevChunkButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PrevChunkButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PrevChunkButton.ForeColor = System.Drawing.SystemColors.Control;
            this.PrevChunkButton.Location = new System.Drawing.Point(2, 28);
            this.PrevChunkButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PrevChunkButton.Name = "PrevChunkButton";
            this.PrevChunkButton.Size = new System.Drawing.Size(67, 23);
            this.PrevChunkButton.TabIndex = 1;
            this.PrevChunkButton.Text = "Prev Chunk";
            this.PrevChunkButton.UseVisualStyleBackColor = false;
            this.PrevChunkButton.Click += new System.EventHandler(this.PrevChunkButton_Click);
            // 
            // NextChunkButton
            // 
            this.NextChunkButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NextChunkButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.NextChunkButton.ForeColor = System.Drawing.SystemColors.Control;
            this.NextChunkButton.Location = new System.Drawing.Point(74, 28);
            this.NextChunkButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NextChunkButton.Name = "NextChunkButton";
            this.NextChunkButton.Size = new System.Drawing.Size(67, 23);
            this.NextChunkButton.TabIndex = 0;
            this.NextChunkButton.Text = "Next Chunk";
            this.NextChunkButton.UseVisualStyleBackColor = false;
            this.NextChunkButton.Click += new System.EventHandler(this.NextChunkButton_Click);
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OptionsPanel.Controls.Add(this.CollisionBBox);
            this.OptionsPanel.Controls.Add(this.label3);
            this.OptionsPanel.Controls.Add(this.CollisionABox);
            this.OptionsPanel.Controls.Add(this.label2);
            this.OptionsPanel.Controls.Add(this.VisualBox);
            this.OptionsPanel.Controls.Add(this.label1);
            this.OptionsPanel.Controls.Add(this.OrientationBox);
            this.OptionsPanel.Controls.Add(this.OrientationLabel);
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OptionsPanel.Location = new System.Drawing.Point(0, 0);
            this.OptionsPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(229, 284);
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
            this.CollisionBBox.Location = new System.Drawing.Point(12, 181);
            this.CollisionBBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CollisionBBox.Name = "CollisionBBox";
            this.CollisionBBox.Size = new System.Drawing.Size(129, 21);
            this.CollisionBBox.TabIndex = 7;
            this.CollisionBBox.SelectedIndexChanged += new System.EventHandler(this.CollisionBBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(10, 164);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
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
            this.CollisionABox.Location = new System.Drawing.Point(12, 129);
            this.CollisionABox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CollisionABox.Name = "CollisionABox";
            this.CollisionABox.Size = new System.Drawing.Size(129, 21);
            this.CollisionABox.TabIndex = 5;
            this.CollisionABox.SelectedIndexChanged += new System.EventHandler(this.CollisionABox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(10, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
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
            this.VisualBox.Location = new System.Drawing.Point(12, 80);
            this.VisualBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VisualBox.Name = "VisualBox";
            this.VisualBox.Size = new System.Drawing.Size(129, 21);
            this.VisualBox.TabIndex = 3;
            this.VisualBox.SelectedIndexChanged += new System.EventHandler(this.VisualBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
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
            this.OrientationBox.Location = new System.Drawing.Point(12, 24);
            this.OrientationBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OrientationBox.Name = "OrientationBox";
            this.OrientationBox.Size = new System.Drawing.Size(129, 21);
            this.OrientationBox.TabIndex = 1;
            this.OrientationBox.SelectedIndexChanged += new System.EventHandler(this.OrientationBox_SelectedIndexChanged);
            // 
            // OrientationLabel
            // 
            this.OrientationLabel.AutoSize = true;
            this.OrientationLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.OrientationLabel.Location = new System.Drawing.Point(10, 7);
            this.OrientationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OrientationLabel.Name = "OrientationLabel";
            this.OrientationLabel.Size = new System.Drawing.Size(58, 13);
            this.OrientationLabel.TabIndex = 0;
            this.OrientationLabel.Text = "Orientation";
            // 
            // ChunkDisplay
            // 
            this.ChunkDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ChunkDisplay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChunkDisplay.Location = new System.Drawing.Point(0, 25);
            this.ChunkDisplay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChunkDisplay.Name = "ChunkDisplay";
            this.ChunkDisplay.Size = new System.Drawing.Size(319, 319);
            this.ChunkDisplay.TabIndex = 2;
            this.ChunkDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChunkDisplay_MouseDown);
            // 
            // ChunkLabel
            // 
            this.ChunkLabel.AutoSize = true;
            this.ChunkLabel.Location = new System.Drawing.Point(76, 7);
            this.ChunkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ChunkLabel.Name = "ChunkLabel";
            this.ChunkLabel.Size = new System.Drawing.Size(105, 13);
            this.ChunkLabel.TabIndex = 1;
            this.ChunkLabel.Text = "Current 128x128 Tile";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.StageTilesList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 320);
            this.panel2.TabIndex = 1;
            // 
            // TilesLabel
            // 
            this.TilesLabel.AutoSize = true;
            this.TilesLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.TilesLabel.Location = new System.Drawing.Point(44, 7);
            this.TilesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TilesLabel.Name = "TilesLabel";
            this.TilesLabel.Size = new System.Drawing.Size(58, 13);
            this.TilesLabel.TabIndex = 0;
            this.TilesLabel.Text = "16x16Tiles";
            // 
            // StageTilesList
            // 
            this.StageTilesList.BackColor = System.Drawing.SystemColors.ControlDark;
            this.StageTilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StageTilesList.Location = new System.Drawing.Point(0, 0);
            this.StageTilesList.Margin = new System.Windows.Forms.Padding(4);
            this.StageTilesList.Name = "StageTilesList";
            this.StageTilesList.ScrollValue = 0;
            this.StageTilesList.SelectedIndex = -1;
            this.StageTilesList.Size = new System.Drawing.Size(180, 318);
            this.StageTilesList.TabIndex = 0;
            this.StageTilesList.SelectedIndexChanged += new System.EventHandler(this.StageTilesList_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(736, 368);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "RSDK Chunk Mapping Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.CurChunkChangePanel.ResumeLayout(false);
            this.CurChunkChangePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GotoNUD)).EndInit();
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.Panel CurChunkChangePanel;
        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.ComboBox CollisionBBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CollisionABox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox VisualBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox OrientationBox;
        private System.Windows.Forms.Label OrientationLabel;
        private System.Windows.Forms.Panel ChunkDisplay;
        private System.Windows.Forms.Label ChunkLabel;
        private System.Windows.Forms.Label TilesLabel;
        private System.Windows.Forms.Label ChunkNoLabel;
        private System.Windows.Forms.Button PrevChunkButton;
        private System.Windows.Forms.Button NextChunkButton;
        private System.Windows.Forms.Panel panel2;
        private TileList StageTilesList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem renderEachChunkAsAnImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem;
        private System.Windows.Forms.Button GotoButton;
        private System.Windows.Forms.NumericUpDown GotoNUD;
        private System.Windows.Forms.Label label4;
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
    }
}

