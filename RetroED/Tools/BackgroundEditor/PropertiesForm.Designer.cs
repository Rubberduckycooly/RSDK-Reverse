namespace RetroED.Tools.BackgroundEditor
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
            this.MapHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.MapWidthNUD = new System.Windows.Forms.NumericUpDown();
            this.MapHeightLabel = new System.Windows.Forms.Label();
            this.MapWidthLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapHeightNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapWidthNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MapHeightNUD);
            this.panel1.Controls.Add(this.MapWidthNUD);
            this.panel1.Controls.Add(this.MapHeightLabel);
            this.panel1.Controls.Add(this.MapWidthLabel);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 187);
            this.panel1.TabIndex = 0;
            // 
            // MapHeightNUD
            // 
            this.MapHeightNUD.Location = new System.Drawing.Point(98, 36);
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
            this.MapWidthNUD.Location = new System.Drawing.Point(98, 9);
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
            this.MapHeightLabel.Location = new System.Drawing.Point(12, 38);
            this.MapHeightLabel.Name = "MapHeightLabel";
            this.MapHeightLabel.Size = new System.Drawing.Size(77, 17);
            this.MapHeightLabel.TabIndex = 5;
            this.MapHeightLabel.Text = "BG Height:";
            // 
            // MapWidthLabel
            // 
            this.MapWidthLabel.AutoSize = true;
            this.MapWidthLabel.Location = new System.Drawing.Point(12, 9);
            this.MapWidthLabel.Name = "MapWidthLabel";
            this.MapWidthLabel.Size = new System.Drawing.Size(72, 17);
            this.MapWidthLabel.TabIndex = 4;
            this.MapWidthLabel.Text = "BG Width:";
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
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 187);
            this.Controls.Add(this.panel1);
            this.Name = "PropertiesForm";
            this.Text = "PropertiesForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}