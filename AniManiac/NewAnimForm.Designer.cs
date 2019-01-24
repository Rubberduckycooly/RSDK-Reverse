namespace AniManiac
{
    partial class NewAnimForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.AnimNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameCountNUD = new System.Windows.Forms.NumericUpDown();
            this.FrameWidthNUD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.FrameHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FrameCountNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameWidthNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameHeightNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Animation Name:";
            // 
            // AnimNameBox
            // 
            this.AnimNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimNameBox.Location = new System.Drawing.Point(133, 23);
            this.AnimNameBox.Name = "AnimNameBox";
            this.AnimNameBox.Size = new System.Drawing.Size(105, 22);
            this.AnimNameBox.TabIndex = 1;
            this.AnimNameBox.Text = "New Animation";
            this.AnimNameBox.TextChanged += new System.EventHandler(this.AnimNameBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Frame Count: ";
            // 
            // FrameCountNUD
            // 
            this.FrameCountNUD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameCountNUD.Location = new System.Drawing.Point(133, 55);
            this.FrameCountNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FrameCountNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FrameCountNUD.Name = "FrameCountNUD";
            this.FrameCountNUD.Size = new System.Drawing.Size(105, 22);
            this.FrameCountNUD.TabIndex = 3;
            this.FrameCountNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FrameCountNUD.ValueChanged += new System.EventHandler(this.FrameCountNUD_ValueChanged);
            // 
            // FrameWidthNUD
            // 
            this.FrameWidthNUD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameWidthNUD.Location = new System.Drawing.Point(133, 83);
            this.FrameWidthNUD.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.FrameWidthNUD.Name = "FrameWidthNUD";
            this.FrameWidthNUD.Size = new System.Drawing.Size(105, 22);
            this.FrameWidthNUD.TabIndex = 5;
            this.FrameWidthNUD.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.FrameWidthNUD.ValueChanged += new System.EventHandler(this.FrameWidthNUD_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Frame Width: ";
            // 
            // FrameHeightNUD
            // 
            this.FrameHeightNUD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameHeightNUD.Location = new System.Drawing.Point(133, 111);
            this.FrameHeightNUD.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.FrameHeightNUD.Name = "FrameHeightNUD";
            this.FrameHeightNUD.Size = new System.Drawing.Size(105, 22);
            this.FrameHeightNUD.TabIndex = 7;
            this.FrameHeightNUD.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.FrameHeightNUD.ValueChanged += new System.EventHandler(this.FrameHeightNUD_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "FrameHeight: ";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(183, 162);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(102, 162);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 9;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NewAnimForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(270, 197);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.FrameHeightNUD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FrameWidthNUD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FrameCountNUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AnimNameBox);
            this.Controls.Add(this.label1);
            this.Name = "NewAnimForm";
            this.Text = "New Animation";
            ((System.ComponentModel.ISupportInitialize)(this.FrameCountNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameWidthNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameHeightNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AnimNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown FrameCountNUD;
        private System.Windows.Forms.NumericUpDown FrameWidthNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown FrameHeightNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}