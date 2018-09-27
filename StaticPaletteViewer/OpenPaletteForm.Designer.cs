namespace StaticPaletteViewer
{
    partial class OpenPaletteForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PalOffsetNUD = new System.Windows.Forms.NumericUpDown();
            this.ColourCountNUD = new System.Windows.Forms.NumericUpDown();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PalOffsetNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColourCountNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Palette Offset:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Colour Count:";
            // 
            // PalOffsetNUD
            // 
            this.PalOffsetNUD.Location = new System.Drawing.Point(176, 68);
            this.PalOffsetNUD.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.PalOffsetNUD.Name = "PalOffsetNUD";
            this.PalOffsetNUD.Size = new System.Drawing.Size(120, 22);
            this.PalOffsetNUD.TabIndex = 2;
            this.PalOffsetNUD.ValueChanged += new System.EventHandler(this.PalOffsetNUD_ValueChanged);
            // 
            // ColourCountNUD
            // 
            this.ColourCountNUD.Location = new System.Drawing.Point(176, 103);
            this.ColourCountNUD.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ColourCountNUD.Name = "ColourCountNUD";
            this.ColourCountNUD.Size = new System.Drawing.Size(120, 22);
            this.ColourCountNUD.TabIndex = 3;
            this.ColourCountNUD.ValueChanged += new System.EventHandler(this.ColourCountNUD_ValueChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(289, 232);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(208, 232);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // OpenPaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 267);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ColourCountNUD);
            this.Controls.Add(this.PalOffsetNUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OpenPaletteForm";
            this.Text = "OpenPaletteForm";
            ((System.ComponentModel.ISupportInitialize)(this.PalOffsetNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColourCountNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PalOffsetNUD;
        private System.Windows.Forms.NumericUpDown ColourCountNUD;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
    }
}