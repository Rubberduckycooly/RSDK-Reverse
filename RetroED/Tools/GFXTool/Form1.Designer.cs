namespace RetroED.Tools.GFXTool
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectPaletteButton = new System.Windows.Forms.Button();
            this.TransparentCB = new System.Windows.Forms.CheckBox();
            this.IMG2GFXLabel = new System.Windows.Forms.Label();
            this.GFX2IMGLabel = new System.Windows.Forms.Label();
            this.ExportToGFX = new System.Windows.Forms.Button();
            this.SourceIMGLocation = new System.Windows.Forms.TextBox();
            this.SelectGIFButton = new System.Windows.Forms.Button();
            this.ExportIMGButton = new System.Windows.Forms.Button();
            this.SourceGFXLocation = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SelectPaletteButton);
            this.panel1.Controls.Add(this.TransparentCB);
            this.panel1.Controls.Add(this.IMG2GFXLabel);
            this.panel1.Controls.Add(this.GFX2IMGLabel);
            this.panel1.Controls.Add(this.ExportToGFX);
            this.panel1.Controls.Add(this.SourceIMGLocation);
            this.panel1.Controls.Add(this.SelectGIFButton);
            this.panel1.Controls.Add(this.ExportIMGButton);
            this.panel1.Controls.Add(this.SourceGFXLocation);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 306);
            this.panel1.TabIndex = 0;
            // 
            // SelectPaletteButton
            // 
            this.SelectPaletteButton.Location = new System.Drawing.Point(133, 109);
            this.SelectPaletteButton.Name = "SelectPaletteButton";
            this.SelectPaletteButton.Size = new System.Drawing.Size(264, 33);
            this.SelectPaletteButton.TabIndex = 9;
            this.SelectPaletteButton.Text = "Select Palette file";
            this.SelectPaletteButton.UseVisualStyleBackColor = true;
            this.SelectPaletteButton.Click += new System.EventHandler(this.SelectPaletteButton_Click);
            // 
            // TransparentCB
            // 
            this.TransparentCB.AutoSize = true;
            this.TransparentCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TransparentCB.Location = new System.Drawing.Point(133, 82);
            this.TransparentCB.Name = "TransparentCB";
            this.TransparentCB.Size = new System.Drawing.Size(243, 21);
            this.TransparentCB.TabIndex = 8;
            this.TransparentCB.Text = "Make Transparent Colour FF00FF";
            this.TransparentCB.UseVisualStyleBackColor = true;
            // 
            // IMG2GFXLabel
            // 
            this.IMG2GFXLabel.AutoSize = true;
            this.IMG2GFXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IMG2GFXLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IMG2GFXLabel.Location = new System.Drawing.Point(4, 176);
            this.IMG2GFXLabel.Name = "IMG2GFXLabel";
            this.IMG2GFXLabel.Size = new System.Drawing.Size(181, 25);
            this.IMG2GFXLabel.TabIndex = 7;
            this.IMG2GFXLabel.Text = "Import image to .gfx";
            // 
            // GFX2IMGLabel
            // 
            this.GFX2IMGLabel.AutoSize = true;
            this.GFX2IMGLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GFX2IMGLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GFX2IMGLabel.Location = new System.Drawing.Point(13, 13);
            this.GFX2IMGLabel.Name = "GFX2IMGLabel";
            this.GFX2IMGLabel.Size = new System.Drawing.Size(183, 25);
            this.GFX2IMGLabel.TabIndex = 6;
            this.GFX2IMGLabel.Text = "Export .gfx to image";
            // 
            // ExportToGFX
            // 
            this.ExportToGFX.Location = new System.Drawing.Point(9, 257);
            this.ExportToGFX.Name = "ExportToGFX";
            this.ExportToGFX.Size = new System.Drawing.Size(117, 33);
            this.ExportToGFX.TabIndex = 5;
            this.ExportToGFX.Text = "Export To GFX";
            this.ExportToGFX.UseVisualStyleBackColor = true;
            this.ExportToGFX.Click += new System.EventHandler(this.ExportToGFX_Click);
            // 
            // SourceIMGLocation
            // 
            this.SourceIMGLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SourceIMGLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SourceIMGLocation.Location = new System.Drawing.Point(132, 209);
            this.SourceIMGLocation.Name = "SourceIMGLocation";
            this.SourceIMGLocation.ReadOnly = true;
            this.SourceIMGLocation.Size = new System.Drawing.Size(265, 22);
            this.SourceIMGLocation.TabIndex = 4;
            // 
            // SelectGIFButton
            // 
            this.SelectGIFButton.Location = new System.Drawing.Point(9, 204);
            this.SelectGIFButton.Name = "SelectGIFButton";
            this.SelectGIFButton.Size = new System.Drawing.Size(117, 33);
            this.SelectGIFButton.TabIndex = 3;
            this.SelectGIFButton.Text = "Select IMG File";
            this.SelectGIFButton.UseVisualStyleBackColor = true;
            this.SelectGIFButton.Click += new System.EventHandler(this.SelectGIFButton_Click);
            // 
            // ExportIMGButton
            // 
            this.ExportIMGButton.Location = new System.Drawing.Point(9, 101);
            this.ExportIMGButton.Name = "ExportIMGButton";
            this.ExportIMGButton.Size = new System.Drawing.Size(117, 33);
            this.ExportIMGButton.TabIndex = 2;
            this.ExportIMGButton.Text = "Export To IMG";
            this.ExportIMGButton.UseVisualStyleBackColor = true;
            this.ExportIMGButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // SourceGFXLocation
            // 
            this.SourceGFXLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SourceGFXLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SourceGFXLocation.Location = new System.Drawing.Point(132, 53);
            this.SourceGFXLocation.Name = "SourceGFXLocation";
            this.SourceGFXLocation.ReadOnly = true;
            this.SourceGFXLocation.Size = new System.Drawing.Size(265, 22);
            this.SourceGFXLocation.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select .gfx File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(420, 306);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "GFX Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox SourceGFXLocation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ExportIMGButton;
        private System.Windows.Forms.Button ExportToGFX;
        private System.Windows.Forms.TextBox SourceIMGLocation;
        private System.Windows.Forms.Button SelectGIFButton;
        private System.Windows.Forms.Label IMG2GFXLabel;
        private System.Windows.Forms.Label GFX2IMGLabel;
        private System.Windows.Forms.CheckBox TransparentCB;
        private System.Windows.Forms.Button SelectPaletteButton;
    }
}

