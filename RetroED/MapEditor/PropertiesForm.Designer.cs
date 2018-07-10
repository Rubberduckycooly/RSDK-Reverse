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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.MapNameLabel = new System.Windows.Forms.Label();
            this.MapNameBox = new System.Windows.Forms.TextBox();
            this.MapWidthLabel = new System.Windows.Forms.Label();
            this.MapHeightLabel = new System.Windows.Forms.Label();
            this.MapWidthNUD = new System.Windows.Forms.NumericUpDown();
            this.MapHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RSPlayerPosYNUD = new System.Windows.Forms.NumericUpDown();
            this.RSPlayerPosXNUD = new System.Windows.Forms.NumericUpDown();
            this.PlayerYLabel = new System.Windows.Forms.Label();
            this.PlayerXLabel = new System.Windows.Forms.Label();
            this.RSBGNUD = new System.Windows.Forms.NumericUpDown();
            this.RSMusicNUD = new System.Windows.Forms.NumericUpDown();
            this.RSBGLabel = new System.Windows.Forms.Label();
            this.RSMusicLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosYNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosXNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSBGNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSMusicNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.panel1.Size = new System.Drawing.Size(462, 272);
            this.panel1.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(375, 246);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(294, 246);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // MapNameLabel
            // 
            this.MapNameLabel.AutoSize = true;
            this.MapNameLabel.Location = new System.Drawing.Point(12, 9);
            this.MapNameLabel.Name = "MapNameLabel";
            this.MapNameLabel.Size = new System.Drawing.Size(80, 17);
            this.MapNameLabel.TabIndex = 2;
            this.MapNameLabel.Text = "Map Name:";
            // 
            // MapNameBox
            // 
            this.MapNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.MapNameBox.Location = new System.Drawing.Point(98, 9);
            this.MapNameBox.Name = "MapNameBox";
            this.MapNameBox.Size = new System.Drawing.Size(153, 22);
            this.MapNameBox.TabIndex = 3;
            this.MapNameBox.TextChanged += new System.EventHandler(this.MapNameBox_TextChanged);
            // 
            // MapWidthLabel
            // 
            this.MapWidthLabel.AutoSize = true;
            this.MapWidthLabel.Location = new System.Drawing.Point(12, 40);
            this.MapWidthLabel.Name = "MapWidthLabel";
            this.MapWidthLabel.Size = new System.Drawing.Size(79, 17);
            this.MapWidthLabel.TabIndex = 4;
            this.MapWidthLabel.Text = "Map Width:";
            // 
            // MapHeightLabel
            // 
            this.MapHeightLabel.AutoSize = true;
            this.MapHeightLabel.Location = new System.Drawing.Point(12, 69);
            this.MapHeightLabel.Name = "MapHeightLabel";
            this.MapHeightLabel.Size = new System.Drawing.Size(84, 17);
            this.MapHeightLabel.TabIndex = 5;
            this.MapHeightLabel.Text = "Map Height:";
            // 
            // MapWidthNUD
            // 
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
            // MapHeightNUD
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(3, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 166);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Retro-Sonic Exclusive Properties";
            // 
            // RSPlayerPosYNUD
            // 
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
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 272);
            this.Controls.Add(this.panel1);
            this.Name = "PropertiesForm";
            this.Text = "PropertiesForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosYNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPlayerPosXNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSBGNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSMusicNUD)).EndInit();
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
    }
}