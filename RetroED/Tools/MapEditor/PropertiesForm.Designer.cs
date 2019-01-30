namespace RetroED.Tools.MapEditor
{
    partial class PropertiesForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Layer3NUD = new System.Windows.Forms.NumericUpDown();
            this.Layer3Label = new System.Windows.Forms.Label();
            this.Layer2NUD = new System.Windows.Forms.NumericUpDown();
            this.Layer1NUD = new System.Windows.Forms.NumericUpDown();
            this.Layer2Label = new System.Windows.Forms.Label();
            this.Layer1Label = new System.Windows.Forms.Label();
            this.Layer0NUD = new System.Windows.Forms.NumericUpDown();
            this.MidpointNUD = new System.Windows.Forms.NumericUpDown();
            this.Layer0Label = new System.Windows.Forms.Label();
            this.MidpointLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RSPlayerPosYNUD = new System.Windows.Forms.NumericUpDown();
            this.RSPlayerPosXNUD = new System.Windows.Forms.NumericUpDown();
            this.PlayerYLabel = new System.Windows.Forms.Label();
            this.PlayerXLabel = new System.Windows.Forms.Label();
            this.RSBGNUD = new System.Windows.Forms.NumericUpDown();
            this.RSMusicNUD = new System.Windows.Forms.NumericUpDown();
            this.RSBGLabel = new System.Windows.Forms.Label();
            this.RSMusicLabel = new System.Windows.Forms.Label();
            this.MapHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.MapWidthNUD = new System.Windows.Forms.NumericUpDown();
            this.MapHeightLabel = new System.Windows.Forms.Label();
            this.MapWidthLabel = new System.Windows.Forms.Label();
            this.MapNameBox = new System.Windows.Forms.TextBox();
            this.MapNameLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Layer3NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Layer2NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Layer1NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Layer0NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MidpointNUD)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosYNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosXNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSBGNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSMusicNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.MapHeightNUD);
            this.panel1.Controls.Add(this.MapWidthNUD);
            this.panel1.Controls.Add(this.MapHeightLabel);
            this.panel1.Controls.Add(this.MapWidthLabel);
            this.panel1.Controls.Add(this.MapNameBox);
            this.panel1.Controls.Add(this.MapNameLabel);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 292);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Layer3NUD);
            this.groupBox2.Controls.Add(this.Layer3Label);
            this.groupBox2.Controls.Add(this.Layer2NUD);
            this.groupBox2.Controls.Add(this.Layer1NUD);
            this.groupBox2.Controls.Add(this.Layer2Label);
            this.groupBox2.Controls.Add(this.Layer1Label);
            this.groupBox2.Controls.Add(this.Layer0NUD);
            this.groupBox2.Controls.Add(this.MidpointNUD);
            this.groupBox2.Controls.Add(this.Layer0Label);
            this.groupBox2.Controls.Add(this.MidpointLabel);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox2.Location = new System.Drawing.Point(282, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 164);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Retro Engine Display Properties";
            // 
            // Layer3NUD
            // 
            this.Layer3NUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Layer3NUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Layer3NUD.Location = new System.Drawing.Point(142, 136);
            this.Layer3NUD.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.Layer3NUD.Name = "Layer3NUD";
            this.Layer3NUD.Size = new System.Drawing.Size(74, 22);
            this.Layer3NUD.TabIndex = 25;
            this.Layer3NUD.ValueChanged += new System.EventHandler(this.Layer3NUD_ValueChanged);
            // 
            // Layer3Label
            // 
            this.Layer3Label.AutoSize = true;
            this.Layer3Label.Location = new System.Drawing.Point(10, 138);
            this.Layer3Label.Name = "Layer3Label";
            this.Layer3Label.Size = new System.Drawing.Size(60, 17);
            this.Layer3Label.TabIndex = 24;
            this.Layer3Label.Text = "Layer 3:";
            // 
            // Layer2NUD
            // 
            this.Layer2NUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Layer2NUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Layer2NUD.Location = new System.Drawing.Point(142, 110);
            this.Layer2NUD.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.Layer2NUD.Name = "Layer2NUD";
            this.Layer2NUD.Size = new System.Drawing.Size(74, 22);
            this.Layer2NUD.TabIndex = 23;
            this.Layer2NUD.ValueChanged += new System.EventHandler(this.Layer2NUD_ValueChanged);
            // 
            // Layer1NUD
            // 
            this.Layer1NUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Layer1NUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Layer1NUD.Location = new System.Drawing.Point(142, 83);
            this.Layer1NUD.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.Layer1NUD.Name = "Layer1NUD";
            this.Layer1NUD.Size = new System.Drawing.Size(74, 22);
            this.Layer1NUD.TabIndex = 22;
            this.Layer1NUD.ValueChanged += new System.EventHandler(this.Layer1NUD_ValueChanged);
            // 
            // Layer2Label
            // 
            this.Layer2Label.AutoSize = true;
            this.Layer2Label.Location = new System.Drawing.Point(10, 112);
            this.Layer2Label.Name = "Layer2Label";
            this.Layer2Label.Size = new System.Drawing.Size(60, 17);
            this.Layer2Label.TabIndex = 21;
            this.Layer2Label.Text = "Layer 2:";
            // 
            // Layer1Label
            // 
            this.Layer1Label.AutoSize = true;
            this.Layer1Label.Location = new System.Drawing.Point(10, 83);
            this.Layer1Label.Name = "Layer1Label";
            this.Layer1Label.Size = new System.Drawing.Size(60, 17);
            this.Layer1Label.TabIndex = 20;
            this.Layer1Label.Text = "Layer 1:";
            // 
            // Layer0NUD
            // 
            this.Layer0NUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Layer0NUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Layer0NUD.Location = new System.Drawing.Point(142, 55);
            this.Layer0NUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Layer0NUD.Name = "Layer0NUD";
            this.Layer0NUD.Size = new System.Drawing.Size(74, 22);
            this.Layer0NUD.TabIndex = 19;
            this.Layer0NUD.ValueChanged += new System.EventHandler(this.Layer0NUD_ValueChanged);
            // 
            // MidpointNUD
            // 
            this.MidpointNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MidpointNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MidpointNUD.Location = new System.Drawing.Point(142, 28);
            this.MidpointNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.MidpointNUD.Name = "MidpointNUD";
            this.MidpointNUD.Size = new System.Drawing.Size(74, 22);
            this.MidpointNUD.TabIndex = 18;
            this.MidpointNUD.ValueChanged += new System.EventHandler(this.MidpointNUD_ValueChanged);
            // 
            // Layer0Label
            // 
            this.Layer0Label.AutoSize = true;
            this.Layer0Label.Location = new System.Drawing.Point(10, 57);
            this.Layer0Label.Name = "Layer0Label";
            this.Layer0Label.Size = new System.Drawing.Size(60, 17);
            this.Layer0Label.TabIndex = 17;
            this.Layer0Label.Text = "Layer 0:";
            // 
            // MidpointLabel
            // 
            this.MidpointLabel.AutoSize = true;
            this.MidpointLabel.Location = new System.Drawing.Point(10, 28);
            this.MidpointLabel.Name = "MidpointLabel";
            this.MidpointLabel.Size = new System.Drawing.Size(65, 17);
            this.MidpointLabel.TabIndex = 16;
            this.MidpointLabel.Text = "Midpoint:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RSPlayerPosYNUD);
            this.groupBox1.Controls.Add(this.RSPlayerPosXNUD);
            this.groupBox1.Controls.Add(this.PlayerYLabel);
            this.groupBox1.Controls.Add(this.PlayerXLabel);
            this.groupBox1.Controls.Add(this.RSBGNUD);
            this.groupBox1.Controls.Add(this.RSMusicNUD);
            this.groupBox1.Controls.Add(this.RSBGLabel);
            this.groupBox1.Controls.Add(this.RSMusicLabel);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 166);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Retro-Sonic Exclusive Properties";
            // 
            // RSPlayerPosYNUD
            // 
            this.RSPlayerPosYNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RSPlayerPosYNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RSPlayerPosYNUD.Location = new System.Drawing.Point(109, 116);
            this.RSPlayerPosYNUD.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.RSPlayerPosYNUD.Name = "RSPlayerPosYNUD";
            this.RSPlayerPosYNUD.Size = new System.Drawing.Size(153, 22);
            this.RSPlayerPosYNUD.TabIndex = 23;
            this.RSPlayerPosYNUD.ValueChanged += new System.EventHandler(this.RSPlayerPosYNUD_ValueChanged);
            // 
            // RSPlayerPosXNUD
            // 
            this.RSPlayerPosXNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RSPlayerPosXNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RSPlayerPosXNUD.Location = new System.Drawing.Point(109, 89);
            this.RSPlayerPosXNUD.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.RSPlayerPosXNUD.Name = "RSPlayerPosXNUD";
            this.RSPlayerPosXNUD.Size = new System.Drawing.Size(153, 22);
            this.RSPlayerPosXNUD.TabIndex = 22;
            this.RSPlayerPosXNUD.ValueChanged += new System.EventHandler(this.RSPlayerPosXNUD_ValueChanged);
            // 
            // PlayerYLabel
            // 
            this.PlayerYLabel.AutoSize = true;
            this.PlayerYLabel.Location = new System.Drawing.Point(10, 118);
            this.PlayerYLabel.Name = "PlayerYLabel";
            this.PlayerYLabel.Size = new System.Drawing.Size(93, 17);
            this.PlayerYLabel.TabIndex = 21;
            this.PlayerYLabel.Text = "Player Pos Y:";
            // 
            // PlayerXLabel
            // 
            this.PlayerXLabel.AutoSize = true;
            this.PlayerXLabel.Location = new System.Drawing.Point(10, 89);
            this.PlayerXLabel.Name = "PlayerXLabel";
            this.PlayerXLabel.Size = new System.Drawing.Size(93, 17);
            this.PlayerXLabel.TabIndex = 20;
            this.PlayerXLabel.Text = "Player Pos X:";
            // 
            // RSBGNUD
            // 
            this.RSBGNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RSBGNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RSBGNUD.Location = new System.Drawing.Point(109, 55);
            this.RSBGNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RSBGNUD.Name = "RSBGNUD";
            this.RSBGNUD.Size = new System.Drawing.Size(153, 22);
            this.RSBGNUD.TabIndex = 19;
            this.RSBGNUD.ValueChanged += new System.EventHandler(this.RSBGNUD_ValueChanged);
            // 
            // RSMusicNUD
            // 
            this.RSMusicNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RSMusicNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RSMusicNUD.Location = new System.Drawing.Point(109, 28);
            this.RSMusicNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RSMusicNUD.Name = "RSMusicNUD";
            this.RSMusicNUD.Size = new System.Drawing.Size(153, 22);
            this.RSMusicNUD.TabIndex = 18;
            this.RSMusicNUD.ValueChanged += new System.EventHandler(this.RSMusicNUD_ValueChanged);
            // 
            // RSBGLabel
            // 
            this.RSBGLabel.AutoSize = true;
            this.RSBGLabel.Location = new System.Drawing.Point(10, 57);
            this.RSBGLabel.Name = "RSBGLabel";
            this.RSBGLabel.Size = new System.Drawing.Size(88, 17);
            this.RSBGLabel.TabIndex = 17;
            this.RSBGLabel.Text = "Background:";
            // 
            // RSMusicLabel
            // 
            this.RSMusicLabel.AutoSize = true;
            this.RSMusicLabel.Location = new System.Drawing.Point(10, 28);
            this.RSMusicLabel.Name = "RSMusicLabel";
            this.RSMusicLabel.Size = new System.Drawing.Size(44, 17);
            this.RSMusicLabel.TabIndex = 16;
            this.RSMusicLabel.Text = "Music";
            // 
            // MapHeightNUD
            // 
            this.MapHeightNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MapHeightNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapHeightNUD.Location = new System.Drawing.Point(98, 67);
            this.MapHeightNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MapHeightNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MapHeightNUD.Name = "MapHeightNUD";
            this.MapHeightNUD.Size = new System.Drawing.Size(153, 22);
            this.MapHeightNUD.TabIndex = 7;
            this.MapHeightNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MapHeightNUD.ValueChanged += new System.EventHandler(this.MapHeightNUD_ValueChanged);
            // 
            // MapWidthNUD
            // 
            this.MapWidthNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MapWidthNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapWidthNUD.Location = new System.Drawing.Point(98, 40);
            this.MapWidthNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MapWidthNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MapWidthNUD.Name = "MapWidthNUD";
            this.MapWidthNUD.Size = new System.Drawing.Size(153, 22);
            this.MapWidthNUD.TabIndex = 6;
            this.MapWidthNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MapWidthNUD.ValueChanged += new System.EventHandler(this.MapWidthNUD_ValueChanged);
            // 
            // MapHeightLabel
            // 
            this.MapHeightLabel.AutoSize = true;
            this.MapHeightLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapHeightLabel.Location = new System.Drawing.Point(12, 69);
            this.MapHeightLabel.Name = "MapHeightLabel";
            this.MapHeightLabel.Size = new System.Drawing.Size(84, 17);
            this.MapHeightLabel.TabIndex = 5;
            this.MapHeightLabel.Text = "Map Height:";
            // 
            // MapWidthLabel
            // 
            this.MapWidthLabel.AutoSize = true;
            this.MapWidthLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapWidthLabel.Location = new System.Drawing.Point(12, 40);
            this.MapWidthLabel.Name = "MapWidthLabel";
            this.MapWidthLabel.Size = new System.Drawing.Size(79, 17);
            this.MapWidthLabel.TabIndex = 4;
            this.MapWidthLabel.Text = "Map Width:";
            // 
            // MapNameBox
            // 
            this.MapNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MapNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.MapNameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapNameBox.Location = new System.Drawing.Point(98, 9);
            this.MapNameBox.Name = "MapNameBox";
            this.MapNameBox.Size = new System.Drawing.Size(153, 22);
            this.MapNameBox.TabIndex = 3;
            this.MapNameBox.TextChanged += new System.EventHandler(this.MapNameBox_TextChanged);
            // 
            // MapNameLabel
            // 
            this.MapNameLabel.AutoSize = true;
            this.MapNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapNameLabel.Location = new System.Drawing.Point(12, 9);
            this.MapNameLabel.Name = "MapNameLabel";
            this.MapNameLabel.Size = new System.Drawing.Size(80, 17);
            this.MapNameLabel.TabIndex = 2;
            this.MapNameLabel.Text = "Map Name:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(424, 266);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(505, 266);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(592, 292);
            this.Controls.Add(this.panel1);
            this.Name = "PropertiesForm";
            this.Text = "PropertiesForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Layer3NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Layer2NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Layer1NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Layer0NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MidpointNUD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosYNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosXNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSBGNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSMusicNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label MapNameLabel;
        private System.Windows.Forms.NumericUpDown MapHeightNUD;
        private System.Windows.Forms.NumericUpDown MapWidthNUD;
        private System.Windows.Forms.Label MapHeightLabel;
        private System.Windows.Forms.Label MapWidthLabel;
        private System.Windows.Forms.TextBox MapNameBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown RSPlayerPosYNUD;
        private System.Windows.Forms.NumericUpDown RSPlayerPosXNUD;
        private System.Windows.Forms.Label PlayerYLabel;
        private System.Windows.Forms.Label PlayerXLabel;
        private System.Windows.Forms.NumericUpDown RSBGNUD;
        private System.Windows.Forms.NumericUpDown RSMusicNUD;
        private System.Windows.Forms.Label RSBGLabel;
        private System.Windows.Forms.Label RSMusicLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown Layer3NUD;
        private System.Windows.Forms.Label Layer3Label;
        private System.Windows.Forms.NumericUpDown Layer2NUD;
        private System.Windows.Forms.NumericUpDown Layer1NUD;
        private System.Windows.Forms.Label Layer2Label;
        private System.Windows.Forms.Label Layer1Label;
        private System.Windows.Forms.NumericUpDown Layer0NUD;
        private System.Windows.Forms.NumericUpDown MidpointNUD;
        private System.Windows.Forms.Label Layer0Label;
        private System.Windows.Forms.Label MidpointLabel;
    }
}