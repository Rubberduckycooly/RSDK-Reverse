namespace RetroED.Tools.MapEditor
{
    partial class StageChunksView
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MousePosStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ChunksPage = new System.Windows.Forms.TabPage();
            this.BlocksList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            this.ObjectsPage = new System.Windows.Forms.TabPage();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.ChunksPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MousePosStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 356);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(317, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MousePosStatusLabel
            // 
            this.MousePosStatusLabel.Name = "MousePosStatusLabel";
            this.MousePosStatusLabel.Size = new System.Drawing.Size(116, 20);
            this.MousePosStatusLabel.Text = "Mouse Position: ";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.ChunksPage);
            this.tabControl.Controls.Add(this.ObjectsPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(317, 356);
            this.tabControl.TabIndex = 5;
            // 
            // ChunksPage
            // 
            this.ChunksPage.Controls.Add(this.BlocksList);
            this.ChunksPage.Location = new System.Drawing.Point(4, 26);
            this.ChunksPage.Name = "ChunksPage";
            this.ChunksPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChunksPage.Size = new System.Drawing.Size(309, 326);
            this.ChunksPage.TabIndex = 0;
            this.ChunksPage.Text = "Chunks";
            this.ChunksPage.UseVisualStyleBackColor = true;
            // 
            // BlocksList
            // 
            this.BlocksList.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BlocksList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlocksList.ImageHeight = 128;
            this.BlocksList.ImageSize = 128;
            this.BlocksList.ImageWidth = 128;
            this.BlocksList.Location = new System.Drawing.Point(3, 3);
            this.BlocksList.Margin = new System.Windows.Forms.Padding(5);
            this.BlocksList.Name = "BlocksList";
            this.BlocksList.ScrollValue = 0;
            this.BlocksList.SelectedIndex = -1;
            this.BlocksList.Size = new System.Drawing.Size(303, 320);
            this.BlocksList.TabIndex = 0;
            this.BlocksList.SelectedIndexChanged += new System.EventHandler(this.BlocksList_SelectedIndexChanged);
            // 
            // ObjectsPage
            // 
            this.ObjectsPage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ObjectsPage.Location = new System.Drawing.Point(4, 26);
            this.ObjectsPage.Name = "ObjectsPage";
            this.ObjectsPage.Size = new System.Drawing.Size(309, 326);
            this.ObjectsPage.TabIndex = 2;
            this.ObjectsPage.Text = "Objects";
            // 
            // StageChunksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(317, 381);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StageChunksView";
            this.Text = "Chunks & Objects";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ChunksPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel MousePosStatusLabel;
        public System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ChunksPage;
        public ChunkMappingsEditor.TileList BlocksList;
        private System.Windows.Forms.TabPage ObjectsPage;
    }
}