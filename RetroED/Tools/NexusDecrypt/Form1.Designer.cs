namespace RetroED.Tools.NexusDecrypt
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
            this.SwapFileButton = new System.Windows.Forms.Button();
            this.SourceFileLocation = new System.Windows.Forms.TextBox();
            this.FileButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SwapFileButton);
            this.panel1.Controls.Add(this.SourceFileLocation);
            this.panel1.Controls.Add(this.FileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 97);
            this.panel1.TabIndex = 0;
            // 
            // SwapFileButton
            // 
            this.SwapFileButton.Location = new System.Drawing.Point(12, 51);
            this.SwapFileButton.Name = "SwapFileButton";
            this.SwapFileButton.Size = new System.Drawing.Size(117, 33);
            this.SwapFileButton.TabIndex = 5;
            this.SwapFileButton.Text = "\"Flip\" File";
            this.SwapFileButton.UseVisualStyleBackColor = true;
            this.SwapFileButton.Click += new System.EventHandler(this.Flip_Click);
            // 
            // SourceFileLocation
            // 
            this.SourceFileLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SourceFileLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SourceFileLocation.Location = new System.Drawing.Point(167, 17);
            this.SourceFileLocation.Name = "SourceFileLocation";
            this.SourceFileLocation.ReadOnly = true;
            this.SourceFileLocation.Size = new System.Drawing.Size(265, 22);
            this.SourceFileLocation.TabIndex = 4;
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(9, 12);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(152, 33);
            this.FileButton.TabIndex = 3;
            this.FileButton.Text = "Select File";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(453, 97);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Nexus Decrypter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SwapFileButton;
        private System.Windows.Forms.TextBox SourceFileLocation;
        private System.Windows.Forms.Button FileButton;
    }
}

