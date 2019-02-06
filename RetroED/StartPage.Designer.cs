namespace RetroED.StartPage
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label3 = new System.Windows.Forms.Label();
            this.ToolsBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RSDKVerBox = new System.Windows.Forms.ComboBox();
            this.RFBox = new System.Windows.Forms.GroupBox();
            this.RecentFilesList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UpdateNotesLabel = new System.Windows.Forms.Label();
            this.UpdateNotesBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.RFBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(1, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "General Purpose Editor For RSDK versions below 5";
            // 
            // ToolsBox
            // 
            this.ToolsBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.ToolsBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ToolsBox.Location = new System.Drawing.Point(735, 0);
            this.ToolsBox.Name = "ToolsBox";
            this.ToolsBox.Size = new System.Drawing.Size(526, 673);
            this.ToolsBox.TabIndex = 12;
            this.ToolsBox.TabStop = false;
            this.ToolsBox.Text = "Tools";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(6, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Select RSDK Version:";
            // 
            // RSDKVerBox
            // 
            this.RSDKVerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSDKVerBox.FormattingEnabled = true;
            this.RSDKVerBox.Items.AddRange(new object[] {
            "RSDKvB",
            "RSDKv2",
            "RSDKv1",
            "RSDKvRS"});
            this.RSDKVerBox.Location = new System.Drawing.Point(6, 222);
            this.RSDKVerBox.Name = "RSDKVerBox";
            this.RSDKVerBox.Size = new System.Drawing.Size(329, 24);
            this.RSDKVerBox.TabIndex = 10;
            // 
            // RFBox
            // 
            this.RFBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RFBox.Controls.Add(this.RecentFilesList);
            this.RFBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RFBox.Location = new System.Drawing.Point(6, 227);
            this.RFBox.Name = "RFBox";
            this.RFBox.Size = new System.Drawing.Size(710, 359);
            this.RFBox.TabIndex = 9;
            this.RFBox.TabStop = false;
            this.RFBox.Text = "Recent Files";
            // 
            // RecentFilesList
            // 
            this.RecentFilesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.RecentFilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecentFilesList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RecentFilesList.FormattingEnabled = true;
            this.RecentFilesList.ItemHeight = 16;
            this.RecentFilesList.Items.AddRange(new object[] {
            "C:/RetroEngine/v2/R11A/Act1.bin - RSDKv2"});
            this.RecentFilesList.Location = new System.Drawing.Point(3, 18);
            this.RecentFilesList.Name = "RecentFilesList";
            this.RecentFilesList.Size = new System.Drawing.Size(704, 338);
            this.RecentFilesList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UpdateNotesLabel);
            this.groupBox1.Controls.Add(this.UpdateNotesBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(338, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 267);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Latest Update";
            // 
            // UpdateNotesLabel
            // 
            this.UpdateNotesLabel.AutoSize = true;
            this.UpdateNotesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UpdateNotesLabel.Location = new System.Drawing.Point(7, 56);
            this.UpdateNotesLabel.Name = "UpdateNotesLabel";
            this.UpdateNotesLabel.Size = new System.Drawing.Size(99, 17);
            this.UpdateNotesLabel.TabIndex = 2;
            this.UpdateNotesLabel.Text = "Update Notes:";
            // 
            // UpdateNotesBox
            // 
            this.UpdateNotesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.UpdateNotesBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UpdateNotesBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UpdateNotesBox.Location = new System.Drawing.Point(3, 76);
            this.UpdateNotesBox.Name = "UpdateNotesBox";
            this.UpdateNotesBox.ReadOnly = true;
            this.UpdateNotesBox.Size = new System.Drawing.Size(375, 188);
            this.UpdateNotesBox.TabIndex = 1;
            this.UpdateNotesBox.Text = "Version 1.2.0:\n\n- did a bunch of internal tweaks to make everything run better";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version: 1.2.0";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.NameLabel.Location = new System.Drawing.Point(7, 4);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(325, 85);
            this.NameLabel.TabIndex = 7;
            this.NameLabel.Text = "RetroED";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1261, 673);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ToolsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RSDKVerBox);
            this.Controls.Add(this.RFBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.NameLabel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "RetroED Start Page";
            this.RFBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox ToolsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RSDKVerBox;
        private System.Windows.Forms.GroupBox RFBox;
        private System.Windows.Forms.ListBox RecentFilesList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label UpdateNotesLabel;
        private System.Windows.Forms.RichTextBox UpdateNotesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NameLabel;
    }
}

