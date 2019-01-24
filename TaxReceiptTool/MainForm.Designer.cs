namespace TaxReceiptTool
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ExportToAnimButton = new System.Windows.Forms.Button();
            this.OpenScriptButton = new System.Windows.Forms.Button();
            this.OpenDataFolderButton = new System.Windows.Forms.Button();
            this.ExportDataFolderButton = new System.Windows.Forms.Button();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(552, 450);
            this.TabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPage1.Controls.Add(this.ExportDataFolderButton);
            this.tabPage1.Controls.Add(this.OpenDataFolderButton);
            this.tabPage1.Controls.Add(this.ExportToAnimButton);
            this.tabPage1.Controls.Add(this.OpenScriptButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(544, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TaxReceipt2Ani";
            // 
            // ExportToAnimButton
            // 
            this.ExportToAnimButton.Location = new System.Drawing.Point(110, 255);
            this.ExportToAnimButton.Name = "ExportToAnimButton";
            this.ExportToAnimButton.Size = new System.Drawing.Size(150, 47);
            this.ExportToAnimButton.TabIndex = 1;
            this.ExportToAnimButton.Text = "Export to Anim";
            this.ExportToAnimButton.UseVisualStyleBackColor = true;
            this.ExportToAnimButton.Click += new System.EventHandler(this.ExportToAnimButton_Click);
            // 
            // OpenScriptButton
            // 
            this.OpenScriptButton.Location = new System.Drawing.Point(110, 86);
            this.OpenScriptButton.Name = "OpenScriptButton";
            this.OpenScriptButton.Size = new System.Drawing.Size(150, 47);
            this.OpenScriptButton.TabIndex = 0;
            this.OpenScriptButton.Text = "Open TaxReceipt Script";
            this.OpenScriptButton.UseVisualStyleBackColor = true;
            this.OpenScriptButton.Click += new System.EventHandler(this.OpenScriptButton_Click);
            // 
            // OpenDataFolderButton
            // 
            this.OpenDataFolderButton.Location = new System.Drawing.Point(286, 86);
            this.OpenDataFolderButton.Name = "OpenDataFolderButton";
            this.OpenDataFolderButton.Size = new System.Drawing.Size(150, 47);
            this.OpenDataFolderButton.TabIndex = 2;
            this.OpenDataFolderButton.Text = "Open Data Folder";
            this.OpenDataFolderButton.UseVisualStyleBackColor = true;
            this.OpenDataFolderButton.Click += new System.EventHandler(this.OpenDataFolderButton_Click);
            // 
            // ExportDataFolderButton
            // 
            this.ExportDataFolderButton.Location = new System.Drawing.Point(286, 255);
            this.ExportDataFolderButton.Name = "ExportDataFolderButton";
            this.ExportDataFolderButton.Size = new System.Drawing.Size(150, 47);
            this.ExportDataFolderButton.TabIndex = 3;
            this.ExportDataFolderButton.Text = "Export Data Folder";
            this.ExportDataFolderButton.UseVisualStyleBackColor = true;
            this.ExportDataFolderButton.Click += new System.EventHandler(this.ExportDataFolderButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(552, 450);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.Text = "TaxReceipt Tool";
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button ExportToAnimButton;
        private System.Windows.Forms.Button OpenScriptButton;
        private System.Windows.Forms.Button OpenDataFolderButton;
        private System.Windows.Forms.Button ExportDataFolderButton;
    }
}

