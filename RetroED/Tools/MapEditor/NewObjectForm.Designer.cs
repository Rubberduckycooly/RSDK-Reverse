namespace RetroED.Tools.MapEditor
{
    partial class NewObjectForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ObjTypeLabel = new System.Windows.Forms.Label();
            this.SubTypeLabel = new System.Windows.Forms.Label();
            this.XposLabel = new System.Windows.Forms.Label();
            this.YPosLabel = new System.Windows.Forms.Label();
            this.TypeNUD = new System.Windows.Forms.NumericUpDown();
            this.SubtypeNUD = new System.Windows.Forms.NumericUpDown();
            this.XposNUD = new System.Windows.Forms.NumericUpDown();
            this.YposNUD = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubtypeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XposNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YposNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Controls.Add(this.YposNUD);
            this.panel1.Controls.Add(this.XposNUD);
            this.panel1.Controls.Add(this.SubtypeNUD);
            this.panel1.Controls.Add(this.TypeNUD);
            this.panel1.Controls.Add(this.YPosLabel);
            this.panel1.Controls.Add(this.XposLabel);
            this.panel1.Controls.Add(this.SubTypeLabel);
            this.panel1.Controls.Add(this.ObjTypeLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 227);
            this.panel1.TabIndex = 0;
            // 
            // ObjTypeLabel
            // 
            this.ObjTypeLabel.AutoSize = true;
            this.ObjTypeLabel.Location = new System.Drawing.Point(13, 13);
            this.ObjTypeLabel.Name = "ObjTypeLabel";
            this.ObjTypeLabel.Size = new System.Drawing.Size(89, 17);
            this.ObjTypeLabel.TabIndex = 0;
            this.ObjTypeLabel.Text = "Object Type:";
            // 
            // SubTypeLabel
            // 
            this.SubTypeLabel.AutoSize = true;
            this.SubTypeLabel.Location = new System.Drawing.Point(13, 44);
            this.SubTypeLabel.Name = "SubTypeLabel";
            this.SubTypeLabel.Size = new System.Drawing.Size(109, 17);
            this.SubTypeLabel.TabIndex = 1;
            this.SubTypeLabel.Text = "Object Subtype:";
            // 
            // XposLabel
            // 
            this.XposLabel.AutoSize = true;
            this.XposLabel.Location = new System.Drawing.Point(16, 74);
            this.XposLabel.Name = "XposLabel";
            this.XposLabel.Size = new System.Drawing.Size(120, 17);
            this.XposLabel.TabIndex = 2;
            this.XposLabel.Text = "Object X Position:";
            // 
            // YPosLabel
            // 
            this.YPosLabel.AutoSize = true;
            this.YPosLabel.Location = new System.Drawing.Point(16, 103);
            this.YPosLabel.Name = "YPosLabel";
            this.YPosLabel.Size = new System.Drawing.Size(120, 17);
            this.YPosLabel.TabIndex = 3;
            this.YPosLabel.Text = "Object Y Position:";
            // 
            // TypeNUD
            // 
            this.TypeNUD.Location = new System.Drawing.Point(109, 13);
            this.TypeNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.TypeNUD.Name = "TypeNUD";
            this.TypeNUD.Size = new System.Drawing.Size(80, 22);
            this.TypeNUD.TabIndex = 4;
            this.TypeNUD.ValueChanged += new System.EventHandler(this.TypeNUD_ValueChanged);
            // 
            // SubtypeNUD
            // 
            this.SubtypeNUD.Location = new System.Drawing.Point(128, 42);
            this.SubtypeNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SubtypeNUD.Name = "SubtypeNUD";
            this.SubtypeNUD.Size = new System.Drawing.Size(80, 22);
            this.SubtypeNUD.TabIndex = 5;
            this.SubtypeNUD.ValueChanged += new System.EventHandler(this.SubtypeNUD_ValueChanged);
            // 
            // XposNUD
            // 
            this.XposNUD.Location = new System.Drawing.Point(142, 74);
            this.XposNUD.Maximum = new decimal(new int[] {
            65500,
            0,
            0,
            0});
            this.XposNUD.Name = "XposNUD";
            this.XposNUD.Size = new System.Drawing.Size(80, 22);
            this.XposNUD.TabIndex = 6;
            this.XposNUD.ValueChanged += new System.EventHandler(this.XposNUD_ValueChanged);
            // 
            // YposNUD
            // 
            this.YposNUD.Location = new System.Drawing.Point(142, 101);
            this.YposNUD.Maximum = new decimal(new int[] {
            65500,
            0,
            0,
            0});
            this.YposNUD.Name = "YposNUD";
            this.YposNUD.Size = new System.Drawing.Size(80, 22);
            this.YposNUD.TabIndex = 7;
            this.YposNUD.ValueChanged += new System.EventHandler(this.YposNUD_ValueChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(244, 201);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(91, 23);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "Add Object!";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(165, 201);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(73, 23);
            this.CancelButton.TabIndex = 9;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NewObjectForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(347, 227);
            this.Controls.Add(this.panel1);
            this.Name = "NewObjectForm";
            this.Text = "Create New Object!";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubtypeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XposNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YposNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.NumericUpDown TypeNUD;
        private System.Windows.Forms.Label YPosLabel;
        private System.Windows.Forms.Label XposLabel;
        private System.Windows.Forms.Label SubTypeLabel;
        private System.Windows.Forms.Label ObjTypeLabel;
        public System.Windows.Forms.NumericUpDown YposNUD;
        public System.Windows.Forms.NumericUpDown XposNUD;
        public System.Windows.Forms.NumericUpDown SubtypeNUD;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
    }
}