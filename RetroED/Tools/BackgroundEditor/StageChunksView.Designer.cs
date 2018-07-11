namespace RetroED.Tools.BackgroundEditor
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ChunksPage = new System.Windows.Forms.TabPage();
            this.pValues = new System.Windows.Forms.TabPage();
            this.pValuesList = new System.Windows.Forms.ListBox();
            this.BlocksList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            this.tabControl.SuspendLayout();
            this.ChunksPage.SuspendLayout();
            this.pValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.ChunksPage);
            this.tabControl.Controls.Add(this.pValues);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(317, 381);
            this.tabControl.TabIndex = 1;
            // 
            // ChunksPage
            // 
            this.ChunksPage.Controls.Add(this.BlocksList);
            this.ChunksPage.Location = new System.Drawing.Point(4, 26);
            this.ChunksPage.Name = "ChunksPage";
            this.ChunksPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChunksPage.Size = new System.Drawing.Size(309, 351);
            this.ChunksPage.TabIndex = 0;
            this.ChunksPage.Text = "Chunks";
            this.ChunksPage.UseVisualStyleBackColor = true;
            // 
            // pValues
            // 
            this.pValues.Controls.Add(this.pValuesList);
            this.pValues.Location = new System.Drawing.Point(4, 26);
            this.pValues.Name = "pValues";
            this.pValues.Size = new System.Drawing.Size(309, 351);
            this.pValues.TabIndex = 1;
            this.pValues.Text = "Parallax Values";
            this.pValues.UseVisualStyleBackColor = true;
            // 
            // pValuesList
            // 
            this.pValuesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pValuesList.FormattingEnabled = true;
            this.pValuesList.ItemHeight = 17;
            this.pValuesList.Location = new System.Drawing.Point(0, 0);
            this.pValuesList.Name = "pValuesList";
            this.pValuesList.Size = new System.Drawing.Size(309, 351);
            this.pValuesList.TabIndex = 0;
            this.pValuesList.SelectedIndexChanged += new System.EventHandler(this.pValuesList_SelectedIndexChanged);
            this.pValuesList.DoubleClick += new System.EventHandler(this.pValuesList_DoubleClick);
            // 
            // BlocksList
            // 
            this.BlocksList.BackColor = System.Drawing.SystemColors.Window;
            this.BlocksList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlocksList.ImageHeight = 128;
            this.BlocksList.ImageSize = 128;
            this.BlocksList.ImageWidth = 128;
            this.BlocksList.Location = new System.Drawing.Point(3, 3);
            this.BlocksList.Margin = new System.Windows.Forms.Padding(5);
            this.BlocksList.Name = "BlocksList";
            this.BlocksList.ScrollValue = 0;
            this.BlocksList.SelectedIndex = -1;
            this.BlocksList.Size = new System.Drawing.Size(303, 345);
            this.BlocksList.TabIndex = 0;
            this.BlocksList.SelectedIndexChanged += new System.EventHandler(this.BlocksList_SelectedIndexChanged);
            // 
            // StageChunksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(317, 381);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StageChunksView";
            this.Text = "128x128 Chunks";
            this.tabControl.ResumeLayout(false);
            this.ChunksPage.ResumeLayout(false);
            this.pValues.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ChunkMappingsEditor.TileList BlocksList;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ChunksPage;
        private System.Windows.Forms.TabPage pValues;
        private System.Windows.Forms.ListBox pValuesList;
    }
}