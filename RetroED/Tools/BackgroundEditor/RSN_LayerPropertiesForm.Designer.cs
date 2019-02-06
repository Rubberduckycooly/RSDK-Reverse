namespace RetroED.Tools.BackgroundEditor
{
    partial class RSN_LayerPropertiesForm
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
            this.CVSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.CVSPDLabel = new System.Windows.Forms.Label();
            this.RVSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.DeformNUD = new System.Windows.Forms.NumericUpDown();
            this.RVSPDLabel = new System.Windows.Forms.Label();
            this.DeformLabel = new System.Windows.Forms.Label();
            this.MapHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.MapWidthNUD = new System.Windows.Forms.NumericUpDown();
            this.MapHeightLabel = new System.Windows.Forms.Label();
            this.MapWidthLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CVSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RVSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeformNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.CVSPDNUD);
            this.panel1.Controls.Add(this.CVSPDLabel);
            this.panel1.Controls.Add(this.RVSPDNUD);
            this.panel1.Controls.Add(this.DeformNUD);
            this.panel1.Controls.Add(this.RVSPDLabel);
            this.panel1.Controls.Add(this.DeformLabel);
            this.panel1.Controls.Add(this.MapHeightNUD);
            this.panel1.Controls.Add(this.MapWidthNUD);
            this.panel1.Controls.Add(this.MapHeightLabel);
            this.panel1.Controls.Add(this.MapWidthLabel);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 187);
            this.panel1.TabIndex = 0;
            // 
            // CVSPDNUD
            // 
            this.CVSPDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CVSPDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CVSPDNUD.Location = new System.Drawing.Point(182, 121);
            this.CVSPDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CVSPDNUD.Name = "CVSPDNUD";
            this.CVSPDNUD.Size = new System.Drawing.Size(96, 22);
            this.CVSPDNUD.TabIndex = 13;
            this.CVSPDNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CVSPDNUD.ValueChanged += new System.EventHandler(this.Unknown3NUD_ValueChanged);
            // 
            // CVSPDLabel
            // 
            this.CVSPDLabel.AutoSize = true;
            this.CVSPDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CVSPDLabel.Location = new System.Drawing.Point(12, 123);
            this.CVSPDLabel.Name = "CVSPDLabel";
            this.CVSPDLabel.Size = new System.Drawing.Size(164, 17);
            this.CVSPDLabel.TabIndex = 12;
            this.CVSPDLabel.Text = "Constant Vertical Speed:";
            // 
            // RVSPDNUD
            // 
            this.RVSPDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RVSPDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RVSPDNUD.Location = new System.Drawing.Point(182, 92);
            this.RVSPDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RVSPDNUD.Name = "RVSPDNUD";
            this.RVSPDNUD.Size = new System.Drawing.Size(96, 22);
            this.RVSPDNUD.TabIndex = 11;
            this.RVSPDNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RVSPDNUD.ValueChanged += new System.EventHandler(this.Unknown2NUD_ValueChanged);
            // 
            // DeformNUD
            // 
            this.DeformNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DeformNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DeformNUD.Location = new System.Drawing.Point(182, 65);
            this.DeformNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.DeformNUD.Name = "DeformNUD";
            this.DeformNUD.Size = new System.Drawing.Size(96, 22);
            this.DeformNUD.TabIndex = 10;
            this.DeformNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DeformNUD.ValueChanged += new System.EventHandler(this.Unknown1NUD_ValueChanged);
            // 
            // RVSPDLabel
            // 
            this.RVSPDLabel.AutoSize = true;
            this.RVSPDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RVSPDLabel.Location = new System.Drawing.Point(12, 94);
            this.RVSPDLabel.Name = "RVSPDLabel";
            this.RVSPDLabel.Size = new System.Drawing.Size(159, 17);
            this.RVSPDLabel.TabIndex = 9;
            this.RVSPDLabel.Text = "Relative Vertical Speed:";
            // 
            // DeformLabel
            // 
            this.DeformLabel.AutoSize = true;
            this.DeformLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DeformLabel.Location = new System.Drawing.Point(12, 65);
            this.DeformLabel.Name = "DeformLabel";
            this.DeformLabel.Size = new System.Drawing.Size(76, 17);
            this.DeformLabel.TabIndex = 8;
            this.DeformLabel.Text = "Behaviour:";
            // 
            // MapHeightNUD
            // 
            this.MapHeightNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MapHeightNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapHeightNUD.Location = new System.Drawing.Point(182, 36);
            this.MapHeightNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.MapHeightNUD.Name = "MapHeightNUD";
            this.MapHeightNUD.Size = new System.Drawing.Size(96, 22);
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
            this.MapWidthNUD.Location = new System.Drawing.Point(182, 9);
            this.MapWidthNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.MapWidthNUD.Name = "MapWidthNUD";
            this.MapWidthNUD.Size = new System.Drawing.Size(96, 22);
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
            this.MapHeightLabel.Location = new System.Drawing.Point(12, 38);
            this.MapHeightLabel.Name = "MapHeightLabel";
            this.MapHeightLabel.Size = new System.Drawing.Size(93, 17);
            this.MapHeightLabel.TabIndex = 5;
            this.MapHeightLabel.Text = "Layer Height:";
            // 
            // MapWidthLabel
            // 
            this.MapWidthLabel.AutoSize = true;
            this.MapWidthLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapWidthLabel.Location = new System.Drawing.Point(12, 9);
            this.MapWidthLabel.Name = "MapWidthLabel";
            this.MapWidthLabel.Size = new System.Drawing.Size(88, 17);
            this.MapWidthLabel.TabIndex = 4;
            this.MapWidthLabel.Text = "Layer Width:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(131, 161);
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
            this.OKButton.Location = new System.Drawing.Point(212, 161);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // RSN_LayerPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(299, 187);
            this.Controls.Add(this.panel1);
            this.Name = "RSN_LayerPropertiesForm";
            this.Text = "Layer Properties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CVSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RVSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeformNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.NumericUpDown MapHeightNUD;
        private System.Windows.Forms.NumericUpDown MapWidthNUD;
        private System.Windows.Forms.Label MapHeightLabel;
        private System.Windows.Forms.Label MapWidthLabel;
        private System.Windows.Forms.NumericUpDown CVSPDNUD;
        private System.Windows.Forms.Label CVSPDLabel;
        private System.Windows.Forms.NumericUpDown RVSPDNUD;
        private System.Windows.Forms.NumericUpDown DeformNUD;
        private System.Windows.Forms.Label RVSPDLabel;
        private System.Windows.Forms.Label DeformLabel;
    }
}