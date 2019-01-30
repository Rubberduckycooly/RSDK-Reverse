namespace RetroED.Extensions.DataSelect
{
    partial class SceneSelect
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
            this.components = new System.ComponentModel.Container();
            this.scenesTree = new System.Windows.Forms.TreeView();
            this.selectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.searchLabel = new System.Windows.Forms.Label();
            this.FilterText = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.isFilesView = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSceneInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scenesTree
            // 
            this.scenesTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scenesTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.scenesTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.scenesTree.Location = new System.Drawing.Point(16, 43);
            this.scenesTree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scenesTree.Name = "scenesTree";
            this.scenesTree.Size = new System.Drawing.Size(612, 341);
            this.scenesTree.TabIndex = 0;
            this.scenesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.scenesTree_AfterSelect);
            this.scenesTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.scenesTree_NodeMouseClick);
            this.scenesTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.scenesTree_NodeMouseDoubleClick);
            this.scenesTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scenesTree_MouseUp);
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.Enabled = false;
            this.selectButton.Location = new System.Drawing.Point(489, 393);
            this.selectButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(140, 34);
            this.selectButton.TabIndex = 4;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(341, 393);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(140, 34);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.searchLabel.Location = new System.Drawing.Point(12, 11);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(43, 17);
            this.searchLabel.TabIndex = 8;
            this.searchLabel.Text = "Filter:";
            // 
            // FilterText
            // 
            this.FilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterText.Location = new System.Drawing.Point(69, 11);
            this.FilterText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FilterText.Name = "FilterText";
            this.FilterText.Size = new System.Drawing.Size(559, 22);
            this.FilterText.TabIndex = 7;
            this.FilterText.TextChanged += new System.EventHandler(this.FilterText_TextChanged);
            // 
            // browse
            // 
            this.browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browse.Location = new System.Drawing.Point(193, 393);
            this.browse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(140, 34);
            this.browse.TabIndex = 9;
            this.browse.Text = "Browse...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // isFilesView
            // 
            this.isFilesView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isFilesView.AutoSize = true;
            this.isFilesView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.isFilesView.Location = new System.Drawing.Point(16, 406);
            this.isFilesView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.isFilesView.Name = "isFilesView";
            this.isFilesView.Size = new System.Drawing.Size(92, 21);
            this.isFilesView.TabIndex = 6;
            this.isFilesView.Text = "Files View";
            this.isFilesView.UseVisualStyleBackColor = true;
            this.isFilesView.CheckedChanged += new System.EventHandler(this.isFilesView_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(70, 26);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.deleteSceneInfoToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(70, 48);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(69, 22);
            // 
            // deleteSceneInfoToolStripMenuItem
            // 
            this.deleteSceneInfoToolStripMenuItem.Name = "deleteSceneInfoToolStripMenuItem";
            this.deleteSceneInfoToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // SceneSelect
            // 
            this.AcceptButton = this.selectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(645, 442);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.FilterText);
            this.Controls.Add(this.isFilesView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.scenesTree);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(461, 235);
            this.Name = "SceneSelect";
            this.ShowIcon = false;
            this.Text = "Select Scene...";
            this.Load += new System.EventHandler(this.SceneSelect_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView scenesTree;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox isFilesView;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox FilterText;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteSceneInfoToolStripMenuItem;
    }
}