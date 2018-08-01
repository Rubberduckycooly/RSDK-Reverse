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
            this.UnknownNUD = new System.Windows.Forms.NumericUpDown();
            this.UnknownLabel = new System.Windows.Forms.Label();
            this.CSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.RSPDNUD = new System.Windows.Forms.NumericUpDown();
            this.CSPDLabel = new System.Windows.Forms.Label();
            this.LineNoNUD = new System.Windows.Forms.NumericUpDown();
            this.RSPDLabel = new System.Windows.Forms.Label();
            this.LineNoLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnknownNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPDNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineNoNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.UnknownNUD);
            this.panel1.Controls.Add(this.UnknownLabel);
            this.panel1.Controls.Add(this.CSPDNUD);
            this.panel1.Controls.Add(this.RSPDNUD);
            this.panel1.Controls.Add(this.CSPDLabel);
            this.panel1.Controls.Add(this.LineNoNUD);
            this.panel1.Controls.Add(this.RSPDLabel);
            this.panel1.Controls.Add(this.LineNoLabel);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 187);
            this.panel1.TabIndex = 0;
            // 
            // UnknownNUD
            // 
            this.UnknownNUD.Location = new System.Drawing.Point(145, 103);
            this.UnknownNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UnknownNUD.Name = "UnknownNUD";
            this.UnknownNUD.Size = new System.Drawing.Size(153, 22);
            this.UnknownNUD.TabIndex = 11;
            this.UnknownNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UnknownNUD.ValueChanged += new System.EventHandler(this.UnknownNUD_ValueChanged);
            // 
            // UnknownLabel
            // 
            this.UnknownLabel.AutoSize = true;
            this.UnknownLabel.Location = new System.Drawing.Point(12, 103);
            this.UnknownLabel.Name = "UnknownLabel";
            this.UnknownLabel.Size = new System.Drawing.Size(70, 17);
            this.UnknownLabel.TabIndex = 10;
            this.UnknownLabel.Text = "Unknown:";
            // 
            // CSPDNUD
            // 
            this.CSPDNUD.Location = new System.Drawing.Point(145, 70);
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
            this.RSPDNUD.Location = new System.Drawing.Point(145, 36);
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
            this.CSPDLabel.Location = new System.Drawing.Point(12, 72);
            this.CSPDLabel.Name = "CSPDLabel";
            this.CSPDLabel.Size = new System.Drawing.Size(127, 17);
            this.CSPDLabel.TabIndex = 8;
            this.CSPDLabel.Text = "Constant H Speed:";
            // 
            // LineNoNUD
            // 
            this.LineNoNUD.Location = new System.Drawing.Point(145, 8);
            this.LineNoNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.LineNoNUD.Name = "LineNoNUD";
            this.LineNoNUD.Size = new System.Drawing.Size(153, 22);
            this.LineNoNUD.TabIndex = 6;
            this.LineNoNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LineNoNUD.ValueChanged += new System.EventHandler(this.LineNoNUD_ValueChanged);
            // 
            // RSPDLabel
            // 
            this.RSPDLabel.AutoSize = true;
            this.RSPDLabel.Location = new System.Drawing.Point(12, 38);
            this.RSPDLabel.Name = "RSPDLabel";
            this.RSPDLabel.Size = new System.Drawing.Size(122, 17);
            this.RSPDLabel.TabIndex = 5;
            this.RSPDLabel.Text = "Relative H Speed:";
            // 
            // LineNoLabel
            // 
            this.LineNoLabel.AutoSize = true;
            this.LineNoLabel.Location = new System.Drawing.Point(12, 9);
            this.LineNoLabel.Name = "LineNoLabel";
            this.LineNoLabel.Size = new System.Drawing.Size(93, 17);
            this.LineNoLabel.TabIndex = 4;
            this.LineNoLabel.Text = "Line Number:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(203, 161);
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
            this.OKButton.Location = new System.Drawing.Point(284, 161);
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
            this.ClientSize = new System.Drawing.Size(371, 187);
            this.Controls.Add(this.panel1);
            this.Name = "CD12_LineScrollForm";
            this.Text = "Line-Scroll Properties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnknownNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSPDNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineNoNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.NumericUpDown RSPDNUD;
        private System.Windows.Forms.NumericUpDown LineNoNUD;
        private System.Windows.Forms.Label RSPDLabel;
        private System.Windows.Forms.Label LineNoLabel;
        private System.Windows.Forms.NumericUpDown CSPDNUD;
        private System.Windows.Forms.Label CSPDLabel;
        private System.Windows.Forms.NumericUpDown UnknownNUD;
        private System.Windows.Forms.Label UnknownLabel;
    }
}