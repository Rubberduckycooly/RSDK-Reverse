namespace AniManiac
{
    partial class TexManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TexManagerForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReplaceCurTexButton = new System.Windows.Forms.Button();
            this.DeleteCurTexButton = new System.Windows.Forms.Button();
            this.NewTexButton = new System.Windows.Forms.Button();
            this.TexList = new System.Windows.Forms.Label();
            this.TextureListBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TexPreviewBox = new ImageBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ReplaceCurTexButton);
            this.panel1.Controls.Add(this.DeleteCurTexButton);
            this.panel1.Controls.Add(this.NewTexButton);
            this.panel1.Controls.Add(this.TexList);
            this.panel1.Controls.Add(this.TextureListBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 85);
            this.panel1.TabIndex = 0;
            // 
            // ReplaceCurTexButton
            // 
            this.ReplaceCurTexButton.Location = new System.Drawing.Point(368, 53);
            this.ReplaceCurTexButton.Name = "ReplaceCurTexButton";
            this.ReplaceCurTexButton.Size = new System.Drawing.Size(29, 23);
            this.ReplaceCurTexButton.TabIndex = 7;
            this.ReplaceCurTexButton.Text = "R";
            this.ReplaceCurTexButton.UseVisualStyleBackColor = true;
            this.ReplaceCurTexButton.Click += new System.EventHandler(this.ReplaceCurTexButton_Click);
            // 
            // DeleteCurTexButton
            // 
            this.DeleteCurTexButton.Location = new System.Drawing.Point(333, 53);
            this.DeleteCurTexButton.Name = "DeleteCurTexButton";
            this.DeleteCurTexButton.Size = new System.Drawing.Size(29, 23);
            this.DeleteCurTexButton.TabIndex = 6;
            this.DeleteCurTexButton.Text = "-";
            this.DeleteCurTexButton.UseVisualStyleBackColor = true;
            this.DeleteCurTexButton.Click += new System.EventHandler(this.DeleteCurTexButton_Click);
            // 
            // NewTexButton
            // 
            this.NewTexButton.Location = new System.Drawing.Point(298, 53);
            this.NewTexButton.Name = "NewTexButton";
            this.NewTexButton.Size = new System.Drawing.Size(29, 23);
            this.NewTexButton.TabIndex = 5;
            this.NewTexButton.Text = "+";
            this.NewTexButton.UseVisualStyleBackColor = true;
            this.NewTexButton.Click += new System.EventHandler(this.NewTexButton_Click);
            // 
            // TexList
            // 
            this.TexList.AutoSize = true;
            this.TexList.Location = new System.Drawing.Point(13, 13);
            this.TexList.Name = "TexList";
            this.TexList.Size = new System.Drawing.Size(105, 17);
            this.TexList.TabIndex = 1;
            this.TexList.Text = "List of Textures";
            // 
            // TextureListBox
            // 
            this.TextureListBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TextureListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TextureListBox.FormattingEnabled = true;
            this.TextureListBox.Location = new System.Drawing.Point(12, 52);
            this.TextureListBox.Name = "TextureListBox";
            this.TextureListBox.Size = new System.Drawing.Size(280, 24);
            this.TextureListBox.TabIndex = 0;
            this.TextureListBox.SelectedIndexChanged += new System.EventHandler(this.TexSelectorBox_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TexPreviewBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 308);
            this.panel2.TabIndex = 1;
            // 
            // TexPreviewBox
            // 
            this.TexPreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TexPreviewBox.Location = new System.Drawing.Point(0, 0);
            this.TexPreviewBox.Name = "TexPreviewBox";
            this.TexPreviewBox.Size = new System.Drawing.Size(407, 308);
            this.TexPreviewBox.TabIndex = 9;
            // 
            // TexManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(407, 403);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TexManagerForm";
            this.Text = "TexManagerForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox TextureListBox;
        private System.Windows.Forms.Label TexList;
        private System.Windows.Forms.Button ReplaceCurTexButton;
        private System.Windows.Forms.Button DeleteCurTexButton;
        private System.Windows.Forms.Button NewTexButton;
        private System.Windows.Forms.Panel panel2;
        private ImageBox TexPreviewBox;
    }
}