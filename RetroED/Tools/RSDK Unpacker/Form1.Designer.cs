namespace RetroED.Tools.RSDKUnpacker
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
            this.label1 = new System.Windows.Forms.Label();
            this.GFX2IMGLabel = new System.Windows.Forms.Label();
            this.BuildDataButton = new System.Windows.Forms.Button();
            this.DataFolderLocation = new System.Windows.Forms.TextBox();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.ExtractDataButton = new System.Windows.Forms.Button();
            this.DataFileLocation = new System.Windows.Forms.TextBox();
            this.SelDataFileButton = new System.Windows.Forms.Button();
            this.SelectListButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SelectListButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.GFX2IMGLabel);
            this.panel1.Controls.Add(this.BuildDataButton);
            this.panel1.Controls.Add(this.DataFolderLocation);
            this.panel1.Controls.Add(this.SelectFolderButton);
            this.panel1.Controls.Add(this.ExtractDataButton);
            this.panel1.Controls.Add(this.DataFileLocation);
            this.panel1.Controls.Add(this.SelDataFileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 277);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Export Data File to Folder";
            // 
            // GFX2IMGLabel
            // 
            this.GFX2IMGLabel.AutoSize = true;
            this.GFX2IMGLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GFX2IMGLabel.Location = new System.Drawing.Point(12, 152);
            this.GFX2IMGLabel.Name = "GFX2IMGLabel";
            this.GFX2IMGLabel.Size = new System.Drawing.Size(281, 25);
            this.GFX2IMGLabel.TabIndex = 9;
            this.GFX2IMGLabel.Text = "Compress Folder into Data FIle";
            // 
            // BuildDataButton
            // 
            this.BuildDataButton.Location = new System.Drawing.Point(12, 230);
            this.BuildDataButton.Name = "BuildDataButton";
            this.BuildDataButton.Size = new System.Drawing.Size(149, 33);
            this.BuildDataButton.TabIndex = 8;
            this.BuildDataButton.Text = "Build Data File";
            this.BuildDataButton.UseVisualStyleBackColor = true;
            this.BuildDataButton.Click += new System.EventHandler(this.BuildDataButton_Click);
            // 
            // DataFolderLocation
            // 
            this.DataFolderLocation.Location = new System.Drawing.Point(170, 196);
            this.DataFolderLocation.Name = "DataFolderLocation";
            this.DataFolderLocation.ReadOnly = true;
            this.DataFolderLocation.Size = new System.Drawing.Size(265, 22);
            this.DataFolderLocation.TabIndex = 7;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(12, 191);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(152, 33);
            this.SelectFolderButton.TabIndex = 6;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // ExtractDataButton
            // 
            this.ExtractDataButton.Location = new System.Drawing.Point(12, 88);
            this.ExtractDataButton.Name = "ExtractDataButton";
            this.ExtractDataButton.Size = new System.Drawing.Size(149, 33);
            this.ExtractDataButton.TabIndex = 5;
            this.ExtractDataButton.Text = "Extract Data File";
            this.ExtractDataButton.UseVisualStyleBackColor = true;
            this.ExtractDataButton.Click += new System.EventHandler(this.Extract_Click);
            // 
            // DataFileLocation
            // 
            this.DataFileLocation.Location = new System.Drawing.Point(170, 54);
            this.DataFileLocation.Name = "DataFileLocation";
            this.DataFileLocation.ReadOnly = true;
            this.DataFileLocation.Size = new System.Drawing.Size(265, 22);
            this.DataFileLocation.TabIndex = 4;
            // 
            // SelDataFileButton
            // 
            this.SelDataFileButton.Location = new System.Drawing.Point(12, 49);
            this.SelDataFileButton.Name = "SelDataFileButton";
            this.SelDataFileButton.Size = new System.Drawing.Size(152, 33);
            this.SelDataFileButton.TabIndex = 3;
            this.SelDataFileButton.Text = "Select Data File";
            this.SelDataFileButton.UseVisualStyleBackColor = true;
            this.SelDataFileButton.Click += new System.EventHandler(this.SelectDataFileButton_Click);
            // 
            // SelectListButton
            // 
            this.SelectListButton.Location = new System.Drawing.Point(167, 88);
            this.SelectListButton.Name = "SelectListButton";
            this.SelectListButton.Size = new System.Drawing.Size(152, 33);
            this.SelectListButton.TabIndex = 11;
            this.SelectListButton.Text = "Select File List";
            this.SelectListButton.UseVisualStyleBackColor = true;
            this.SelectListButton.Click += new System.EventHandler(this.SelectList_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 277);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Nexus Decrypter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ExtractDataButton;
        private System.Windows.Forms.TextBox DataFileLocation;
        private System.Windows.Forms.Button SelDataFileButton;
        private System.Windows.Forms.Button BuildDataButton;
        private System.Windows.Forms.TextBox DataFolderLocation;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label GFX2IMGLabel;
        private System.Windows.Forms.Button SelectListButton;
    }
}

