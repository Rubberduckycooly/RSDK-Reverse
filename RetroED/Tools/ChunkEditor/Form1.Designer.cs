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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.CurChunkChangePanel = new System.Windows.Forms.Panel();
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
            this.StageTilesList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            this.TilesLabel = new System.Windows.Forms.Label();
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
            this.viewToolStripMenuItem});
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
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.TilesLabel);
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
            this.splitContainer2.Panel1.Controls.Add(this.CurChunkChangePanel);
            this.splitContainer2.Panel1.Controls.Add(this.OptionsPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ChunkDisplay);
            this.splitContainer2.Panel2.Controls.Add(this.ChunkLabel);
            this.splitContainer2.Size = new System.Drawing.Size(736, 425);
            this.splitContainer2.SplitterDistance = 306;
            this.splitContainer2.TabIndex = 0;
            // 
            // CurChunkChangePanel
            // 
            this.CurChunkChangePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurChunkChangePanel.Controls.Add(this.ChunkNoLabel);
            this.CurChunkChangePanel.Controls.Add(this.PrevChunkButton);
            this.CurChunkChangePanel.Controls.Add(this.NextChunkButton);
            this.CurChunkChangePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CurChunkChangePanel.Location = new System.Drawing.Point(0, 372);
            this.CurChunkChangePanel.Name = "CurChunkChangePanel";
            this.CurChunkChangePanel.Size = new System.Drawing.Size(306, 53);
            this.CurChunkChangePanel.TabIndex = 1;
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
            this.PrevChunkButton.BackColor = System.Drawing.SystemColors.ControlDark;
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
            this.NextChunkButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NextChunkButton.Location = new System.Drawing.Point(98, 28);
            this.NextChunkButton.Name = "NextChunkButton";
            this.NextChunkButton.Size = new System.Drawing.Size(89, 23);
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
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(306, 373);
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
            this.ChunkDisplay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChunkDisplay.Location = new System.Drawing.Point(0, 32);
            this.ChunkDisplay.Name = "ChunkDisplay";
            this.ChunkDisplay.Size = new System.Drawing.Size(426, 393);
            this.ChunkDisplay.TabIndex = 2;
            this.ChunkDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChunkDisplay_MouseDown);
            // 
            // ChunkLabel
            // 
            this.ChunkLabel.AutoSize = true;
            this.ChunkLabel.Location = new System.Drawing.Point(102, 9);
            this.ChunkLabel.Name = "ChunkLabel";
            this.ChunkLabel.Size = new System.Drawing.Size(140, 17);
            this.ChunkLabel.TabIndex = 1;
            this.ChunkLabel.Text = "Current 128x128 Tile";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.StageTilesList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 393);
            this.panel2.TabIndex = 1;
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
            this.StageTilesList.Size = new System.Drawing.Size(240, 391);
            this.StageTilesList.TabIndex = 0;
            this.StageTilesList.SelectedIndexChanged += new System.EventHandler(this.StageTilesList_SelectedIndexChanged);
            // 
            // TilesLabel
            // 
            this.TilesLabel.AutoSize = true;
            this.TilesLabel.Location = new System.Drawing.Point(58, 9);
            this.TilesLabel.Name = "TilesLabel";
            this.TilesLabel.Size = new System.Drawing.Size(76, 17);
            this.TilesLabel.TabIndex = 0;
            this.TilesLabel.Text = "16x16Tiles";
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
    }
}

