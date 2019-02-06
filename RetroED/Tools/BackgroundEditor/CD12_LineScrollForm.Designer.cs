namespace RetroED.Tools.BackgroundEditor
{
    partial class CD12_LineScrollForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.DrawLayerNUD = new System.Windows.Forms.NumericUpDown();
            this.UnknownLabel = new System.Windows.Forms.Label();
            this.CSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.RSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.CSPDLabel = new System.Windows.Forms.Label();
            this.BehaviourNUD = new System.Windows.Forms.NumericUpDown();
            this.RSPDLabel = new System.Windows.Forms.Label();
            this.Unknown1Label = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawLayerNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BehaviourNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.DrawLayerNUD);
            this.panel1.Controls.Add(this.UnknownLabel);
            this.panel1.Controls.Add(this.CSPDNUD);
            this.panel1.Controls.Add(this.RSPDNUD);
            this.panel1.Controls.Add(this.CSPDLabel);
            this.panel1.Controls.Add(this.BehaviourNUD);
            this.panel1.Controls.Add(this.RSPDLabel);
            this.panel1.Controls.Add(this.Unknown1Label);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 167);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(34, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Remove Value";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DrawLayerNUD
            // 
            this.DrawLayerNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DrawLayerNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DrawLayerNUD.Location = new System.Drawing.Point(145, 72);
            this.DrawLayerNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.DrawLayerNUD.Name = "DrawLayerNUD";
            this.DrawLayerNUD.Size = new System.Drawing.Size(153, 22);
            this.DrawLayerNUD.TabIndex = 11;
            this.DrawLayerNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DrawLayerNUD.ValueChanged += new System.EventHandler(this.UnknownNUD_ValueChanged);
            // 
            // UnknownLabel
            // 
            this.UnknownLabel.AutoSize = true;
            this.UnknownLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UnknownLabel.Location = new System.Drawing.Point(12, 72);
            this.UnknownLabel.Name = "UnknownLabel";
            this.UnknownLabel.Size = new System.Drawing.Size(85, 17);
            this.UnknownLabel.TabIndex = 10;
            this.UnknownLabel.Text = "Draw Order:";
            // 
            // CSPDNUD
            // 
            this.CSPDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CSPDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CSPDNUD.Location = new System.Drawing.Point(145, 44);
            this.CSPDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CSPDNUD.Name = "CSPDNUD";
            this.CSPDNUD.Size = new System.Drawing.Size(153, 22);
            this.CSPDNUD.TabIndex = 9;
            this.CSPDNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CSPDNUD.ValueChanged += new System.EventHandler(this.CSPDNUD_ValueChanged);
            // 
            // RSPDNUD
            // 
            this.RSPDNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RSPDNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RSPDNUD.Location = new System.Drawing.Point(145, 16);
            this.RSPDNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RSPDNUD.Name = "RSPDNUD";
            this.RSPDNUD.Size = new System.Drawing.Size(153, 22);
            this.RSPDNUD.TabIndex = 7;
            this.RSPDNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RSPDNUD.ValueChanged += new System.EventHandler(this.RSPDNUD_ValueChanged);
            // 
            // CSPDLabel
            // 
            this.CSPDLabel.AutoSize = true;
            this.CSPDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CSPDLabel.Location = new System.Drawing.Point(12, 46);
            this.CSPDLabel.Name = "CSPDLabel";
            this.CSPDLabel.Size = new System.Drawing.Size(113, 17);
            this.CSPDLabel.TabIndex = 8;
            this.CSPDLabel.Text = "Constant Speed:";
            // 
            // BehaviourNUD
            // 
            this.BehaviourNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BehaviourNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BehaviourNUD.Location = new System.Drawing.Point(145, 100);
            this.BehaviourNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BehaviourNUD.Name = "BehaviourNUD";
            this.BehaviourNUD.Size = new System.Drawing.Size(153, 22);
            this.BehaviourNUD.TabIndex = 6;
            this.BehaviourNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BehaviourNUD.ValueChanged += new System.EventHandler(this.Unknown1NUD_ValueChanged);
            // 
            // RSPDLabel
            // 
            this.RSPDLabel.AutoSize = true;
            this.RSPDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RSPDLabel.Location = new System.Drawing.Point(12, 18);
            this.RSPDLabel.Name = "RSPDLabel";
            this.RSPDLabel.Size = new System.Drawing.Size(108, 17);
            this.RSPDLabel.TabIndex = 5;
            this.RSPDLabel.Text = "Relative Speed:";
            // 
            // Unknown1Label
            // 
            this.Unknown1Label.AutoSize = true;
            this.Unknown1Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Unknown1Label.Location = new System.Drawing.Point(12, 101);
            this.Unknown1Label.Name = "Unknown1Label";
            this.Unknown1Label.Size = new System.Drawing.Size(76, 17);
            this.Unknown1Label.TabIndex = 4;
            this.Unknown1Label.Text = "Behaviour:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(159, 141);
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
            this.OKButton.Location = new System.Drawing.Point(240, 141);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CD12_LineScrollForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(327, 167);
            this.Controls.Add(this.panel1);
            this.Name = "CD12_LineScrollForm";
            this.Text = "Line-Scroll Properties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawLayerNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BehaviourNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.NumericUpDown RSPDNUD;
        private System.Windows.Forms.NumericUpDown BehaviourNUD;
        private System.Windows.Forms.Label RSPDLabel;
        private System.Windows.Forms.Label Unknown1Label;
        private System.Windows.Forms.NumericUpDown CSPDNUD;
        private System.Windows.Forms.Label CSPDLabel;
        private System.Windows.Forms.NumericUpDown DrawLayerNUD;
        private System.Windows.Forms.Label UnknownLabel;
        private System.Windows.Forms.Button button1;
    }
}