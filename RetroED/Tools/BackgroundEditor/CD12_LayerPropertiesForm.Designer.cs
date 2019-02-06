namespace RetroED.Tools.BackgroundEditor
{
    partial class CD12_LayerPropertiesForm
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
            this.BehaviourNUD = new System.Windows.Forms.NumericUpDown();
            this.DrawLayerNUD = new System.Windows.Forms.NumericUpDown();
            this.Unknown2Label = new System.Windows.Forms.Label();
            this.Unknown1Label = new System.Windows.Forms.Label();
            this.RelativeVSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.RelativeVSPDLabel = new System.Windows.Forms.Label();
            this.ConstantVSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.ConstantVSPDLabel = new System.Windows.Forms.Label();
            this.MapHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.MapWidthNUD = new System.Windows.Forms.NumericUpDown();
            this.MapHeightLabel = new System.Windows.Forms.Label();
            this.MapWidthLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BehaviourNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawLayerNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelativeVSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantVSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.BehaviourNUD);
            this.panel1.Controls.Add(this.DrawLayerNUD);
            this.panel1.Controls.Add(this.Unknown2Label);
            this.panel1.Controls.Add(this.Unknown1Label);
            this.panel1.Controls.Add(this.RelativeVSPDNUD);
            this.panel1.Controls.Add(this.RelativeVSPDLabel);
            this.panel1.Controls.Add(this.ConstantVSPDNUD);
            this.panel1.Controls.Add(this.ConstantVSPDLabel);
            this.panel1.Controls.Add(this.MapHeightNUD);
            this.panel1.Controls.Add(this.MapWidthNUD);
            this.panel1.Controls.Add(this.MapHeightLabel);
            this.panel1.Controls.Add(this.MapWidthLabel);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 214);
            this.panel1.TabIndex = 0;
            // 
            // BehaviourNUD
            // 
            this.BehaviourNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.BehaviourNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BehaviourNUD.Location = new System.Drawing.Point(209, 148);
            this.BehaviourNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BehaviourNUD.Name = "BehaviourNUD";
            this.BehaviourNUD.Size = new System.Drawing.Size(96, 22);
            this.BehaviourNUD.TabIndex = 21;
            this.BehaviourNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BehaviourNUD.ValueChanged += new System.EventHandler(this.Unknown2NUD_ValueChanged_1);
            // 
            // DrawLayerNUD
            // 
            this.DrawLayerNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.DrawLayerNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DrawLayerNUD.Location = new System.Drawing.Point(209, 121);
            this.DrawLayerNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.DrawLayerNUD.Name = "DrawLayerNUD";
            this.DrawLayerNUD.Size = new System.Drawing.Size(96, 22);
            this.DrawLayerNUD.TabIndex = 20;
            this.DrawLayerNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DrawLayerNUD.ValueChanged += new System.EventHandler(this.Unknown1NUD_ValueChanged_1);
            // 
            // Unknown2Label
            // 
            this.Unknown2Label.AutoSize = true;
            this.Unknown2Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Unknown2Label.Location = new System.Drawing.Point(12, 150);
            this.Unknown2Label.Name = "Unknown2Label";
            this.Unknown2Label.Size = new System.Drawing.Size(76, 17);
            this.Unknown2Label.TabIndex = 19;
            this.Unknown2Label.Text = "Behaviour:";
            // 
            // Unknown1Label
            // 
            this.Unknown1Label.AutoSize = true;
            this.Unknown1Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Unknown1Label.Location = new System.Drawing.Point(12, 121);
            this.Unknown1Label.Name = "Unknown1Label";
            this.Unknown1Label.Size = new System.Drawing.Size(85, 17);
            this.Unknown1Label.TabIndex = 18;
            this.Unknown1Label.Text = "Draw Order:";
            // 
            // RelativeVSPDNUD
            // 
            this.RelativeVSPDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.RelativeVSPDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RelativeVSPDNUD.Location = new System.Drawing.Point(209, 93);
            this.RelativeVSPDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RelativeVSPDNUD.Name = "RelativeVSPDNUD";
            this.RelativeVSPDNUD.Size = new System.Drawing.Size(96, 22);
            this.RelativeVSPDNUD.TabIndex = 17;
            this.RelativeVSPDNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RelativeVSPDNUD.ValueChanged += new System.EventHandler(this.RelativeVSPDNUD_ValueChanged);
            // 
            // RelativeVSPDLabel
            // 
            this.RelativeVSPDLabel.AutoSize = true;
            this.RelativeVSPDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RelativeVSPDLabel.Location = new System.Drawing.Point(12, 95);
            this.RelativeVSPDLabel.Name = "RelativeVSPDLabel";
            this.RelativeVSPDLabel.Size = new System.Drawing.Size(159, 17);
            this.RelativeVSPDLabel.TabIndex = 16;
            this.RelativeVSPDLabel.Text = "Relative Vertical Speed:";
            // 
            // ConstantVSPDNUD
            // 
            this.ConstantVSPDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ConstantVSPDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ConstantVSPDNUD.Location = new System.Drawing.Point(209, 64);
            this.ConstantVSPDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ConstantVSPDNUD.Name = "ConstantVSPDNUD";
            this.ConstantVSPDNUD.Size = new System.Drawing.Size(96, 22);
            this.ConstantVSPDNUD.TabIndex = 15;
            this.ConstantVSPDNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ConstantVSPDNUD.ValueChanged += new System.EventHandler(this.ConstantVSPDNUD_ValueChanged);
            // 
            // ConstantVSPDLabel
            // 
            this.ConstantVSPDLabel.AutoSize = true;
            this.ConstantVSPDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ConstantVSPDLabel.Location = new System.Drawing.Point(12, 64);
            this.ConstantVSPDLabel.Name = "ConstantVSPDLabel";
            this.ConstantVSPDLabel.Size = new System.Drawing.Size(164, 17);
            this.ConstantVSPDLabel.TabIndex = 14;
            this.ConstantVSPDLabel.Text = "Constant Vertical Speed:";
            // 
            // MapHeightNUD
            // 
            this.MapHeightNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.MapHeightNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapHeightNUD.Location = new System.Drawing.Point(209, 36);
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
            this.MapWidthNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.MapWidthNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapWidthNUD.Location = new System.Drawing.Point(209, 9);
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
            this.CancelButton.Location = new System.Drawing.Point(149, 188);
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
            this.OKButton.Location = new System.Drawing.Point(230, 188);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CD12_LayerPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(317, 214);
            this.Controls.Add(this.panel1);
            this.Name = "CD12_LayerPropertiesForm";
            this.Text = "Layer Properties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BehaviourNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawLayerNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelativeVSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantVSPDNUD)).EndInit();
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
        private System.Windows.Forms.NumericUpDown ConstantVSPDNUD;
        private System.Windows.Forms.Label ConstantVSPDLabel;
        private System.Windows.Forms.NumericUpDown RelativeVSPDNUD;
        private System.Windows.Forms.Label RelativeVSPDLabel;
        private System.Windows.Forms.NumericUpDown BehaviourNUD;
        private System.Windows.Forms.NumericUpDown DrawLayerNUD;
        private System.Windows.Forms.Label Unknown2Label;
        private System.Windows.Forms.Label Unknown1Label;
    }
}