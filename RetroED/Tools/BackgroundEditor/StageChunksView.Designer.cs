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
            this.BlocksList = new RetroED.Tools.ChunkMappingsEditor.TileList();
            this.HpValues = new System.Windows.Forms.TabPage();
            this.HpValuesList = new System.Windows.Forms.ListBox();
            this.VpValues = new System.Windows.Forms.TabPage();
            this.VpValuesList = new System.Windows.Forms.ListBox();
            this.LineNumberPage = new System.Windows.Forms.TabPage();
            this.LineNumberListBox = new System.Windows.Forms.ListBox();
            this.tabControl.SuspendLayout();
            this.ChunksPage.SuspendLayout();
            this.HpValues.SuspendLayout();
            this.VpValues.SuspendLayout();
            this.LineNumberPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.ChunksPage);
            this.tabControl.Controls.Add(this.HpValues);
            this.tabControl.Controls.Add(this.VpValues);
            this.tabControl.Controls.Add(this.LineNumberPage);
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
            // BlocksList
            // 
            this.BlocksList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.BlocksList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlocksList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            // HpValues
            // 
            this.HpValues.Controls.Add(this.HpValuesList);
            this.HpValues.Location = new System.Drawing.Point(4, 26);
            this.HpValues.Name = "HpValues";
            this.HpValues.Size = new System.Drawing.Size(309, 351);
            this.HpValues.TabIndex = 1;
            this.HpValues.Text = "Horizontal Parallax Values";
            this.HpValues.UseVisualStyleBackColor = true;
            // 
            // HpValuesList
            // 
            this.HpValuesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.HpValuesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HpValuesList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HpValuesList.FormattingEnabled = true;
            this.HpValuesList.ItemHeight = 17;
            this.HpValuesList.Location = new System.Drawing.Point(0, 0);
            this.HpValuesList.Name = "HpValuesList";
            this.HpValuesList.Size = new System.Drawing.Size(309, 351);
            this.HpValuesList.TabIndex = 0;
            this.HpValuesList.DoubleClick += new System.EventHandler(this.HpValuesList_DoubleClick);
            // 
            // VpValues
            // 
            this.VpValues.Controls.Add(this.VpValuesList);
            this.VpValues.Location = new System.Drawing.Point(4, 26);
            this.VpValues.Name = "VpValues";
            this.VpValues.Size = new System.Drawing.Size(309, 351);
            this.VpValues.TabIndex = 2;
            this.VpValues.Text = "Vertical Parallax Values";
            this.VpValues.UseVisualStyleBackColor = true;
            // 
            // VpValuesList
            // 
            this.VpValuesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.VpValuesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VpValuesList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.VpValuesList.FormattingEnabled = true;
            this.VpValuesList.ItemHeight = 17;
            this.VpValuesList.Location = new System.Drawing.Point(0, 0);
            this.VpValuesList.Name = "VpValuesList";
            this.VpValuesList.Size = new System.Drawing.Size(309, 351);
            this.VpValuesList.TabIndex = 1;
            this.VpValuesList.DoubleClick += new System.EventHandler(this.VpValuesList_DoubleClick);
            // 
            // LineNumberPage
            // 
            this.LineNumberPage.Controls.Add(this.LineNumberListBox);
            this.LineNumberPage.Location = new System.Drawing.Point(4, 26);
            this.LineNumberPage.Name = "LineNumberPage";
            this.LineNumberPage.Padding = new System.Windows.Forms.Padding(3);
            this.LineNumberPage.Size = new System.Drawing.Size(309, 351);
            this.LineNumberPage.TabIndex = 3;
            this.LineNumberPage.Text = "Line Numbers";
            this.LineNumberPage.UseVisualStyleBackColor = true;
            // 
            // LineNumberListBox
            // 
            this.LineNumberListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.LineNumberListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineNumberListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LineNumberListBox.FormattingEnabled = true;
            this.LineNumberListBox.ItemHeight = 17;
            this.LineNumberListBox.Location = new System.Drawing.Point(3, 3);
            this.LineNumberListBox.Name = "LineNumberListBox";
            this.LineNumberListBox.Size = new System.Drawing.Size(303, 345);
            this.LineNumberListBox.TabIndex = 0;
            // 
            // StageChunksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(317, 381);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StageChunksView";
            this.Text = "Chunks & Line Scroll";
            this.tabControl.ResumeLayout(false);
            this.ChunksPage.ResumeLayout(false);
            this.HpValues.ResumeLayout(false);
            this.VpValues.ResumeLayout(false);
            this.LineNumberPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ChunkMappingsEditor.TileList BlocksList;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ChunksPage;
        private System.Windows.Forms.TabPage HpValues;
        private System.Windows.Forms.ListBox HpValuesList;
        private System.Windows.Forms.TabPage VpValues;
        private System.Windows.Forms.ListBox VpValuesList;
        private System.Windows.Forms.TabPage LineNumberPage;
        private System.Windows.Forms.ListBox LineNumberListBox;
    }
}