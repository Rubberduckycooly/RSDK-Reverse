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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.TileIDNUD = new System.Windows.Forms.NumericUpDown();
            this.CurChunkChangePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.GoToChunkNUD = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ChunkNumberLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.GotoButton = new System.Windows.Forms.Button();
            this.GotoNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ChunkNoLabel = new System.Windows.Forms.Label();
            this.PrevChunkButton = new System.Windows.Forms.Button();
            this.NextChunkButton = new System.Windows.Forms.Button();
            this.CollisionBBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CollisionABox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VisualBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OrientationBox = new System.Windows.Forms.ComboBox();
            this.OrientationLabel = new System.Windows.Forms.Label();
            this.ChunkDisplay = new System.Windows.Forms.Panel();
            this.TileListControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.TileZoomBar = new System.Windows.Forms.TrackBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.ChunkZoomBar = new System.Windows.Forms.TrackBar();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItem_New = new System.Windows.Forms.MenuItem();
            this.MenuItem_Open = new System.Windows.Forms.MenuItem();
            this.MenuItem_Save = new System.Windows.Forms.MenuItem();
            this.MenuItem_SaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.MenuItem_RenderChunks = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.MenuItem_PlaceTiles = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.MenuItem_ShowGrid = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.MenuItem_RefTiles = new System.Windows.Forms.MenuItem();
            this.MenuItem_RefChunks = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.MenuItem_AutoSet = new System.Windows.Forms.MenuItem();
            this.MenuItem_ASOrientation = new System.Windows.Forms.MenuItem();
            this.MenuItem_ASVisPlane = new System.Windows.Forms.MenuItem();
            this.MenuItem_ASCollA = new System.Windows.Forms.MenuItem();
            this.MenuItem_ASCollB = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.MenuItem_SetOrientation = new System.Windows.Forms.MenuItem();
            this.MenuItem_SetVisPlane = new System.Windows.Forms.MenuItem();
            this.MenuItem_SetCollA = new System.Windows.Forms.MenuItem();
            this.MenuItem_SetCollB = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.MenuItem_CpyChunk = new System.Windows.Forms.MenuItem();
            this.StageTilesList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            this.StageChunksList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.OptionsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileIDNUD)).BeginInit();
            this.CurChunkChangePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoToChunkNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GotoNUD)).BeginInit();
            this.TileListControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileZoomBar)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkZoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TileListControl);
            this.splitContainer1.Size = new System.Drawing.Size(981, 427);
            this.splitContainer1.SplitterDistance = 734;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.OptionsPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ChunkDisplay);
            this.splitContainer2.Size = new System.Drawing.Size(734, 427);
            this.splitContainer2.SplitterDistance = 305;
            this.splitContainer2.TabIndex = 0;
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.panel2);
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
            this.OptionsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(305, 427);
            this.OptionsPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.TileIDNUD);
            this.panel2.Location = new System.Drawing.Point(0, 336);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 34);
            this.panel2.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Location = new System.Drawing.Point(4, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Currently Selected Tile:";
            // 
            // TileIDNUD
            // 
            this.TileIDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TileIDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TileIDNUD.Location = new System.Drawing.Point(195, 5);
            this.TileIDNUD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TileIDNUD.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.TileIDNUD.Name = "TileIDNUD";
            this.TileIDNUD.Size = new System.Drawing.Size(105, 22);
            this.TileIDNUD.TabIndex = 6;
            this.TileIDNUD.ValueChanged += new System.EventHandler(this.TileIDNUD_ValueChanged);
            // 
            // CurChunkChangePanel
            // 
            this.CurChunkChangePanel.Controls.Add(this.panel1);
            this.CurChunkChangePanel.Controls.Add(this.GotoButton);
            this.CurChunkChangePanel.Controls.Add(this.GotoNUD);
            this.CurChunkChangePanel.Controls.Add(this.label4);
            this.CurChunkChangePanel.Controls.Add(this.ChunkNoLabel);
            this.CurChunkChangePanel.Controls.Add(this.PrevChunkButton);
            this.CurChunkChangePanel.Controls.Add(this.NextChunkButton);
            this.CurChunkChangePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CurChunkChangePanel.Location = new System.Drawing.Point(0, 374);
            this.CurChunkChangePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CurChunkChangePanel.Name = "CurChunkChangePanel";
            this.CurChunkChangePanel.Size = new System.Drawing.Size(305, 53);
            this.CurChunkChangePanel.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.GoToChunkNUD);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.ChunkNumberLabel);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 53);
            this.panel1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "GO!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GotoButton_Click);
            // 
            // GoToChunkNUD
            // 
            this.GoToChunkNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GoToChunkNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GoToChunkNUD.Location = new System.Drawing.Point(247, 6);
            this.GoToChunkNUD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GoToChunkNUD.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.GoToChunkNUD.Name = "GoToChunkNUD";
            this.GoToChunkNUD.Size = new System.Drawing.Size(53, 22);
            this.GoToChunkNUD.TabIndex = 4;
            this.GoToChunkNUD.ValueChanged += new System.EventHandler(this.GotoNUD_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(155, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Go to chunk";
            // 
            // ChunkNumberLabel
            // 
            this.ChunkNumberLabel.AutoSize = true;
            this.ChunkNumberLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ChunkNumberLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ChunkNumberLabel.Location = new System.Drawing.Point(4, 6);
            this.ChunkNumberLabel.Name = "ChunkNumberLabel";
            this.ChunkNumberLabel.Size = new System.Drawing.Size(95, 17);
            this.ChunkNumberLabel.TabIndex = 2;
            this.ChunkNumberLabel.Text = "Chunk 0 Of 0:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(3, 28);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Prev Chunk";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.PrevChunkButton_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Location = new System.Drawing.Point(99, 28);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Next Chunk";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.NextChunkButton_Click);
            // 
            // GotoButton
            // 
            this.GotoButton.Location = new System.Drawing.Point(195, 28);
            this.GotoButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.GotoNUD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GotoNUD.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.GotoNUD.Name = "GotoNUD";
            this.GotoNUD.Size = new System.Drawing.Size(53, 22);
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
            this.PrevChunkButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.NextChunkButton.Location = new System.Drawing.Point(99, 28);
            this.NextChunkButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextChunkButton.Name = "NextChunkButton";
            this.NextChunkButton.Size = new System.Drawing.Size(89, 23);
            this.NextChunkButton.TabIndex = 0;
            this.NextChunkButton.Text = "Next Chunk";
            this.NextChunkButton.UseVisualStyleBackColor = false;
            this.NextChunkButton.Click += new System.EventHandler(this.NextChunkButton_Click);
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
            "None",
            "Unknown (RSDKvB Only)"});
            this.CollisionBBox.Location = new System.Drawing.Point(16, 223);
            this.CollisionBBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CollisionBBox.Name = "CollisionBBox";
            this.CollisionBBox.Size = new System.Drawing.Size(171, 24);
            this.CollisionBBox.TabIndex = 7;
            this.CollisionBBox.SelectedIndexChanged += new System.EventHandler(this.CollisionBBox_SelectedIndexChanged);
            this.CollisionBBox.Click += new System.EventHandler(this.CollisionBBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            "None",
            "Unknown (RSDKvB Only)"});
            this.CollisionABox.Location = new System.Drawing.Point(16, 159);
            this.CollisionABox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CollisionABox.Name = "CollisionABox";
            this.CollisionABox.Size = new System.Drawing.Size(171, 24);
            this.CollisionABox.TabIndex = 5;
            this.CollisionABox.SelectedIndexChanged += new System.EventHandler(this.CollisionABox_SelectedIndexChanged);
            this.CollisionABox.Click += new System.EventHandler(this.CollisionABox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.VisualBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VisualBox.Name = "VisualBox";
            this.VisualBox.Size = new System.Drawing.Size(171, 24);
            this.VisualBox.TabIndex = 3;
            this.VisualBox.SelectedIndexChanged += new System.EventHandler(this.VisualBox_SelectedIndexChanged);
            this.VisualBox.Click += new System.EventHandler(this.VisualBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(13, 78);
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
            this.OrientationBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OrientationBox.Name = "OrientationBox";
            this.OrientationBox.Size = new System.Drawing.Size(171, 24);
            this.OrientationBox.TabIndex = 1;
            this.OrientationBox.SelectedIndexChanged += new System.EventHandler(this.OrientationBox_SelectedIndexChanged);
            this.OrientationBox.Click += new System.EventHandler(this.OrientationBox_SelectedIndexChanged);
            // 
            // OrientationLabel
            // 
            this.OrientationLabel.AutoSize = true;
            this.OrientationLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.OrientationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.ChunkDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChunkDisplay.Name = "ChunkDisplay";
            this.ChunkDisplay.Size = new System.Drawing.Size(425, 427);
            this.ChunkDisplay.TabIndex = 3;
            this.ChunkDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.ChunkDisplay_Paint);
            this.ChunkDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChunkDisplay_MouseDown);
            this.ChunkDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChunkDisplay_MouseDown);
            this.ChunkDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChunkDisplay_MouseMove);
            // 
            // TileListControl
            // 
            this.TileListControl.Controls.Add(this.tabPage1);
            this.TileListControl.Controls.Add(this.tabPage2);
            this.TileListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileListControl.Location = new System.Drawing.Point(0, 0);
            this.TileListControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TileListControl.Name = "TileListControl";
            this.TileListControl.SelectedIndex = 0;
            this.TileListControl.Size = new System.Drawing.Size(243, 427);
            this.TileListControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(235, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tiles";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 2);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.TileZoomBar);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.StageTilesList);
            this.splitContainer3.Size = new System.Drawing.Size(229, 394);
            this.splitContainer3.TabIndex = 0;
            // 
            // TileZoomBar
            // 
            this.TileZoomBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TileZoomBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileZoomBar.LargeChange = 1;
            this.TileZoomBar.Location = new System.Drawing.Point(0, 0);
            this.TileZoomBar.Maximum = 5;
            this.TileZoomBar.Minimum = 1;
            this.TileZoomBar.Name = "TileZoomBar";
            this.TileZoomBar.Size = new System.Drawing.Size(229, 50);
            this.TileZoomBar.TabIndex = 0;
            this.TileZoomBar.Value = 4;
            this.TileZoomBar.Scroll += new System.EventHandler(this.TileZoomBar_Scroll);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(235, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chunks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 2);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.ChunkZoomBar);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.StageChunksList);
            this.splitContainer4.Size = new System.Drawing.Size(229, 420);
            this.splitContainer4.SplitterDistance = 51;
            this.splitContainer4.TabIndex = 0;
            // 
            // ChunkZoomBar
            // 
            this.ChunkZoomBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ChunkZoomBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChunkZoomBar.LargeChange = 1;
            this.ChunkZoomBar.Location = new System.Drawing.Point(0, 0);
            this.ChunkZoomBar.Maximum = 4;
            this.ChunkZoomBar.Minimum = 1;
            this.ChunkZoomBar.Name = "ChunkZoomBar";
            this.ChunkZoomBar.Size = new System.Drawing.Size(229, 56);
            this.ChunkZoomBar.TabIndex = 1;
            this.ChunkZoomBar.Value = 2;
            this.ChunkZoomBar.Scroll += new System.EventHandler(this.ChunkZoomBar_Scroll);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_New,
            this.MenuItem_Open,
            this.MenuItem_Save,
            this.MenuItem_SaveAs,
            this.menuItem8,
            this.MenuItem_RenderChunks});
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
            this.MenuItem_Save.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // MenuItem_SaveAs
            // 
            this.MenuItem_SaveAs.Index = 3;
            this.MenuItem_SaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.MenuItem_SaveAs.Text = "Save &As";
            this.MenuItem_SaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 4;
            this.menuItem8.Text = "-";
            // 
            // MenuItem_RenderChunks
            // 
            this.MenuItem_RenderChunks.Index = 5;
            this.MenuItem_RenderChunks.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.MenuItem_RenderChunks.Text = "Render Each Chunk As Image";
            this.MenuItem_RenderChunks.Click += new System.EventHandler(this.renderEachChunkAsAnImageToolStripMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_PlaceTiles});
            this.menuItem5.Text = "Edit";
            // 
            // MenuItem_PlaceTiles
            // 
            this.MenuItem_PlaceTiles.Index = 0;
            this.MenuItem_PlaceTiles.Text = "Placing Tiles?";
            this.MenuItem_PlaceTiles.Click += new System.EventHandler(this.MenuItem_PlaceTiles_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_ShowGrid,
            this.menuItem7,
            this.MenuItem_RefTiles,
            this.MenuItem_RefChunks});
            this.menuItem2.Text = "View";
            // 
            // MenuItem_ShowGrid
            // 
            this.MenuItem_ShowGrid.Index = 0;
            this.MenuItem_ShowGrid.Text = "Show Grid";
            this.MenuItem_ShowGrid.Click += new System.EventHandler(this.MenuItem_ShowGrid_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "-";
            // 
            // MenuItem_RefTiles
            // 
            this.MenuItem_RefTiles.Index = 2;
            this.MenuItem_RefTiles.Text = "Refresh Tiles";
            this.MenuItem_RefTiles.Click += new System.EventHandler(this.refreshTilesToolStripMenuItem_Click);
            // 
            // MenuItem_RefChunks
            // 
            this.MenuItem_RefChunks.Index = 3;
            this.MenuItem_RefChunks.Text = "Refresh Chunks";
            this.MenuItem_RefChunks.Click += new System.EventHandler(this.refreshChunksToolStripMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_AutoSet,
            this.menuItem12,
            this.MenuItem_CpyChunk});
            this.menuItem3.Text = "Tools";
            // 
            // MenuItem_AutoSet
            // 
            this.MenuItem_AutoSet.Index = 0;
            this.MenuItem_AutoSet.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_ASOrientation,
            this.MenuItem_ASVisPlane,
            this.MenuItem_ASCollA,
            this.MenuItem_ASCollB,
            this.menuItem18,
            this.MenuItem_SetOrientation,
            this.MenuItem_SetVisPlane,
            this.MenuItem_SetCollA,
            this.MenuItem_SetCollB});
            this.MenuItem_AutoSet.Text = "Auto-Set";
            // 
            // MenuItem_ASOrientation
            // 
            this.MenuItem_ASOrientation.Index = 0;
            this.MenuItem_ASOrientation.Text = "Orientation";
            this.MenuItem_ASOrientation.Click += new System.EventHandler(this.MenuItem_ASOrientation_Click);
            // 
            // MenuItem_ASVisPlane
            // 
            this.MenuItem_ASVisPlane.Index = 1;
            this.MenuItem_ASVisPlane.Text = "Visual Plane";
            this.MenuItem_ASVisPlane.Click += new System.EventHandler(this.MenuItem_ASVisPlane_Click);
            // 
            // MenuItem_ASCollA
            // 
            this.MenuItem_ASCollA.Index = 2;
            this.MenuItem_ASCollA.Text = "Collision A";
            this.MenuItem_ASCollA.Click += new System.EventHandler(this.MenuItem_ASCollA_Click);
            // 
            // MenuItem_ASCollB
            // 
            this.MenuItem_ASCollB.Index = 3;
            this.MenuItem_ASCollB.Text = "Collision B";
            this.MenuItem_ASCollB.Click += new System.EventHandler(this.MenuItem_ASCollB_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 4;
            this.menuItem18.Text = "-";
            // 
            // MenuItem_SetOrientation
            // 
            this.MenuItem_SetOrientation.Index = 5;
            this.MenuItem_SetOrientation.Text = "Set Auto-Orientation";
            this.MenuItem_SetOrientation.Click += new System.EventHandler(this.setAutoMenuItem_ASOrientation_Click);
            // 
            // MenuItem_SetVisPlane
            // 
            this.MenuItem_SetVisPlane.Index = 6;
            this.MenuItem_SetVisPlane.Text = "Set Auto-Visual Plane";
            this.MenuItem_SetVisPlane.Click += new System.EventHandler(this.setAutoMenuItem_ASVisPlane_Click);
            // 
            // MenuItem_SetCollA
            // 
            this.MenuItem_SetCollA.Index = 7;
            this.MenuItem_SetCollA.Text = "Set Auto-Collision A";
            this.MenuItem_SetCollA.Click += new System.EventHandler(this.setAutoMenuItem_ASCollA_Click);
            // 
            // MenuItem_SetCollB
            // 
            this.MenuItem_SetCollB.Index = 8;
            this.MenuItem_SetCollB.Text = "Set Auto-Collision B";
            this.MenuItem_SetCollB.Click += new System.EventHandler(this.setAutoMenuItem_ASCollB_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 1;
            this.menuItem12.Text = "-";
            // 
            // MenuItem_CpyChunk
            // 
            this.MenuItem_CpyChunk.Index = 2;
            this.MenuItem_CpyChunk.Text = "Copy Chunk To";
            this.MenuItem_CpyChunk.Click += new System.EventHandler(this.copyChunkToToolStripMenuItem_Click);
            // 
            // StageTilesList
            // 
            this.StageTilesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.StageTilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StageTilesList.ImageHeight = 64;
            this.StageTilesList.ImageSize = 64;
            this.StageTilesList.ImageWidth = 64;
            this.StageTilesList.Location = new System.Drawing.Point(0, 0);
            this.StageTilesList.Margin = new System.Windows.Forms.Padding(5);
            this.StageTilesList.Name = "StageTilesList";
            this.StageTilesList.ScrollValue = 0;
            this.StageTilesList.SelectedIndex = -1;
            this.StageTilesList.Size = new System.Drawing.Size(229, 340);
            this.StageTilesList.TabIndex = 3;
            this.StageTilesList.SelectedIndexChanged += new System.EventHandler(this.StageTilesList_SelectedIndexChanged);
            // 
            // StageChunksList
            // 
            this.StageChunksList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.StageChunksList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StageChunksList.ImageHeight = 128;
            this.StageChunksList.ImageSize = 128;
            this.StageChunksList.ImageWidth = 128;
            this.StageChunksList.Location = new System.Drawing.Point(0, 0);
            this.StageChunksList.Margin = new System.Windows.Forms.Padding(5);
            this.StageChunksList.Name = "StageChunksList";
            this.StageChunksList.ScrollValue = 0;
            this.StageChunksList.SelectedIndex = -1;
            this.StageChunksList.Size = new System.Drawing.Size(229, 365);
            this.StageChunksList.TabIndex = 3;
            this.StageChunksList.SelectedIndexChanged += new System.EventHandler(this.StageChunksList_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(981, 427);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "RSDK Chunk Mapping Editor";
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileIDNUD)).EndInit();
            this.CurChunkChangePanel.ResumeLayout(false);
            this.CurChunkChangePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoToChunkNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GotoNUD)).EndInit();
            this.TileListControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TileZoomBar)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChunkZoomBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Panel ChunkDisplay;
        private System.Windows.Forms.Panel CurChunkChangePanel;
        private System.Windows.Forms.Button GotoButton;
        private System.Windows.Forms.NumericUpDown GotoNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ChunkNoLabel;
        private System.Windows.Forms.Button PrevChunkButton;
        private System.Windows.Forms.Button NextChunkButton;
        private System.Windows.Forms.TabControl TileListControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown TileIDNUD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown GoToChunkNUD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ChunkNumberLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TrackBar TileZoomBar;
        private TileList StageTilesList;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TrackBar ChunkZoomBar;
        private TileList StageChunksList;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem MenuItem_New;
        private System.Windows.Forms.MenuItem MenuItem_Open;
        private System.Windows.Forms.MenuItem MenuItem_Save;
        private System.Windows.Forms.MenuItem MenuItem_SaveAs;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem MenuItem_RenderChunks;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem MenuItem_PlaceTiles;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem MenuItem_ShowGrid;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem MenuItem_RefTiles;
        private System.Windows.Forms.MenuItem MenuItem_RefChunks;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem MenuItem_AutoSet;
        private System.Windows.Forms.MenuItem MenuItem_ASOrientation;
        private System.Windows.Forms.MenuItem MenuItem_ASVisPlane;
        private System.Windows.Forms.MenuItem MenuItem_ASCollA;
        private System.Windows.Forms.MenuItem MenuItem_ASCollB;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem MenuItem_SetOrientation;
        private System.Windows.Forms.MenuItem MenuItem_SetVisPlane;
        private System.Windows.Forms.MenuItem MenuItem_SetCollA;
        private System.Windows.Forms.MenuItem MenuItem_SetCollB;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem MenuItem_CpyChunk;
    }
}

