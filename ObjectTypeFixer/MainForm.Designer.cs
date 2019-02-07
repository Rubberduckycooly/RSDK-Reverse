namespace ObjectTypeFixer
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
            this.label1 = new System.Windows.Forms.Label();
            this.OldGCSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.NewGCSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.FixStageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.OldGCSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewGCSizeNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(46, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Old Global Obj Count:";
            // 
            // OldGCSizeNUD
            // 
            this.OldGCSizeNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OldGCSizeNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OldGCSizeNUD.Location = new System.Drawing.Point(60, 36);
            this.OldGCSizeNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.OldGCSizeNUD.Name = "OldGCSizeNUD";
            this.OldGCSizeNUD.Size = new System.Drawing.Size(120, 22);
            this.OldGCSizeNUD.TabIndex = 1;
            this.OldGCSizeNUD.ValueChanged += new System.EventHandler(this.OldGCSizeNUD_ValueChanged);
            // 
            // NewGCSizeNUD
            // 
            this.NewGCSizeNUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NewGCSizeNUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.NewGCSizeNUD.Location = new System.Drawing.Point(60, 97);
            this.NewGCSizeNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NewGCSizeNUD.Name = "NewGCSizeNUD";
            this.NewGCSizeNUD.Size = new System.Drawing.Size(120, 22);
            this.NewGCSizeNUD.TabIndex = 3;
            this.NewGCSizeNUD.ValueChanged += new System.EventHandler(this.NewGCSizeNUD_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(46, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Global Obj Count:";
            // 
            // FixStageButton
            // 
            this.FixStageButton.Location = new System.Drawing.Point(60, 134);
            this.FixStageButton.Name = "FixStageButton";
            this.FixStageButton.Size = new System.Drawing.Size(120, 33);
            this.FixStageButton.TabIndex = 4;
            this.FixStageButton.Text = "Fix Stage";
            this.FixStageButton.UseVisualStyleBackColor = true;
            this.FixStageButton.Click += new System.EventHandler(this.FixStageButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(249, 179);
            this.Controls.Add(this.FixStageButton);
            this.Controls.Add(this.NewGCSizeNUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OldGCSizeNUD);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Object Type Fixer";
            ((System.ComponentModel.ISupportInitialize)(this.OldGCSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewGCSizeNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown OldGCSizeNUD;
        private System.Windows.Forms.NumericUpDown NewGCSizeNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FixStageButton;
    }
}

