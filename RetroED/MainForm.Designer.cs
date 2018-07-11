namespace RetroED
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSDKUnpackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.animationEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunkEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GFXToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nexusDecryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 28);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1262, 645);
            this.TabControl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpAboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1262, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Enabled = false;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Enabled = false;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rSDKUnpackerToolStripMenuItem,
            this.toolStripSeparator2,
            this.animationEditorToolStripMenuItem,
            this.mapEditorToolStripMenuItem,
            this.backgroundEditorToolStripMenuItem,
            this.paletteEditorToolStripMenuItem,
            this.chunkEditorToolStripMenuItem,
            this.GFXToolToolStripMenuItem,
            this.nexusDecryptToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeTabToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // rSDKUnpackerToolStripMenuItem
            // 
            this.rSDKUnpackerToolStripMenuItem.Enabled = false;
            this.rSDKUnpackerToolStripMenuItem.Name = "rSDKUnpackerToolStripMenuItem";
            this.rSDKUnpackerToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.rSDKUnpackerToolStripMenuItem.Text = "RSDK Unpacker";
            this.rSDKUnpackerToolStripMenuItem.Click += new System.EventHandler(this.rSDKUnpackerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(213, 6);
            // 
            // animationEditorToolStripMenuItem
            // 
            this.animationEditorToolStripMenuItem.Enabled = false;
            this.animationEditorToolStripMenuItem.Name = "animationEditorToolStripMenuItem";
            this.animationEditorToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.animationEditorToolStripMenuItem.Text = "Animation Editor";
            this.animationEditorToolStripMenuItem.Click += new System.EventHandler(this.animationEditorToolStripMenuItem_Click);
            // 
            // mapEditorToolStripMenuItem
            // 
            this.mapEditorToolStripMenuItem.Name = "mapEditorToolStripMenuItem";
            this.mapEditorToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.mapEditorToolStripMenuItem.Text = "Map Editor";
            this.mapEditorToolStripMenuItem.Click += new System.EventHandler(this.mapEditorToolStripMenuItem_Click);
            // 
            // paletteEditorToolStripMenuItem
            // 
            this.paletteEditorToolStripMenuItem.Name = "paletteEditorToolStripMenuItem";
            this.paletteEditorToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.paletteEditorToolStripMenuItem.Text = "Palette Editor";
            this.paletteEditorToolStripMenuItem.Click += new System.EventHandler(this.paletteEditorToolStripMenuItem_Click);
            // 
            // chunkEditorToolStripMenuItem
            // 
            this.chunkEditorToolStripMenuItem.Name = "chunkEditorToolStripMenuItem";
            this.chunkEditorToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.chunkEditorToolStripMenuItem.Text = "Chunk Editor";
            this.chunkEditorToolStripMenuItem.Click += new System.EventHandler(this.chunkEditorToolStripMenuItem_Click);
            // 
            // GFXToolToolStripMenuItem
            // 
            this.GFXToolToolStripMenuItem.Name = "GFXToolToolStripMenuItem";
            this.GFXToolToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.GFXToolToolStripMenuItem.Text = "GFX Tool";
            this.GFXToolToolStripMenuItem.Click += new System.EventHandler(this.GFXToolToolStripMenuItem_Click);
            // 
            // nexusDecryptToolStripMenuItem
            // 
            this.nexusDecryptToolStripMenuItem.Name = "nexusDecryptToolStripMenuItem";
            this.nexusDecryptToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.nexusDecryptToolStripMenuItem.Text = "Nexus Decrypter";
            this.nexusDecryptToolStripMenuItem.Click += new System.EventHandler(this.NexusDecryptToolToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.closeTabToolStripMenuItem.Text = "Close Tab";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // helpAboutToolStripMenuItem
            // 
            this.helpAboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpAboutToolStripMenuItem.Name = "helpAboutToolStripMenuItem";
            this.helpAboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.helpAboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // backgroundEditorToolStripMenuItem
            // 
            this.backgroundEditorToolStripMenuItem.Name = "backgroundEditorToolStripMenuItem";
            this.backgroundEditorToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.backgroundEditorToolStripMenuItem.Text = "Background Editor";
            this.backgroundEditorToolStripMenuItem.Click += new System.EventHandler(this.backgroundEditorToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "RetroED";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animationEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paletteEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chunkEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GFXToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nexusDecryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rSDKUnpackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem backgroundEditorToolStripMenuItem;
    }
}

