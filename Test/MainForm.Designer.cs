namespace Test
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
            this.TestFuncButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestFuncButton
            // 
            this.TestFuncButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestFuncButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestFuncButton.Location = new System.Drawing.Point(0, 0);
            this.TestFuncButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TestFuncButton.Name = "TestFuncButton";
            this.TestFuncButton.Size = new System.Drawing.Size(600, 366);
            this.TestFuncButton.TabIndex = 0;
            this.TestFuncButton.Text = "Test Function";
            this.TestFuncButton.UseVisualStyleBackColor = true;
            this.TestFuncButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.TestFuncButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestFuncButton;
    }
}

