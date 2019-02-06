namespace RetroED.Tools.BackgroundEditor
{
    partial class SelectLayerForm
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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.LayerList = new System.Windows.Forms.ComboBox();
            this.SelLayerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(240, 144);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(159, 144);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // LayerList
            // 
            this.LayerList.BackColor = System.Drawing.SystemColors.Control;
            this.LayerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerList.FormattingEnabled = true;
            this.LayerList.Location = new System.Drawing.Point(27, 83);
            this.LayerList.Name = "LayerList";
            this.LayerList.Size = new System.Drawing.Size(279, 24);
            this.LayerList.TabIndex = 2;
            this.LayerList.SelectedIndexChanged += new System.EventHandler(this.LayerList_SelectedIndexChanged);
            // 
            // SelLayerLabel
            // 
            this.SelLayerLabel.AutoSize = true;
            this.SelLayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelLayerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SelLayerLabel.Location = new System.Drawing.Point(71, 25);
            this.SelLayerLabel.Name = "SelLayerLabel";
            this.SelLayerLabel.Size = new System.Drawing.Size(185, 29);
            this.SelLayerLabel.TabIndex = 3;
            this.SelLayerLabel.Text = "Select A Layer!";
            // 
            // SelectLayerForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(327, 168);
            this.Controls.Add(this.SelLayerLabel);
            this.Controls.Add(this.LayerList);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Name = "SelectLayerForm";
            this.Text = "Select Layer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        public System.Windows.Forms.ComboBox LayerList;
        private System.Windows.Forms.Label SelLayerLabel;
    }
}