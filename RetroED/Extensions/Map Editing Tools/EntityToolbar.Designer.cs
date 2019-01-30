namespace RetroED.Extensions.EntityToolbar
{
    partial class EntityToolbar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbEditor = new System.Windows.Forms.GroupBox();
            this.entitiesList = new System.Windows.Forms.ComboBox();
            this.entityProperties = new System.Windows.Forms.PropertyGrid();
            this.gbSpawn = new System.Windows.Forms.GroupBox();
            this.btnSpawn = new System.Windows.Forms.Button();
            this.cbSpawn = new System.Windows.Forms.ComboBox();
            this.gbEditor.SuspendLayout();
            this.gbSpawn.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEditor
            // 
            this.gbEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEditor.Controls.Add(this.entitiesList);
            this.gbEditor.Controls.Add(this.entityProperties);
            this.gbEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gbEditor.Location = new System.Drawing.Point(0, 72);
            this.gbEditor.Margin = new System.Windows.Forms.Padding(4);
            this.gbEditor.Name = "gbEditor";
            this.gbEditor.Padding = new System.Windows.Forms.Padding(4);
            this.gbEditor.Size = new System.Drawing.Size(321, 425);
            this.gbEditor.TabIndex = 5;
            this.gbEditor.TabStop = false;
            this.gbEditor.Text = "Entity Editor";
            // 
            // entitiesList
            // 
            this.entitiesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entitiesList.FormattingEnabled = true;
            this.entitiesList.Location = new System.Drawing.Point(9, 23);
            this.entitiesList.Margin = new System.Windows.Forms.Padding(4);
            this.entitiesList.Name = "entitiesList";
            this.entitiesList.Size = new System.Drawing.Size(283, 24);
            this.entitiesList.TabIndex = 0;
            this.entitiesList.DropDown += new System.EventHandler(this.entitiesList_DropDown);
            this.entitiesList.SelectedIndexChanged += new System.EventHandler(this.entitiesList_SelectedIndexChanged);
            // 
            // entityProperties
            // 
            this.entityProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entityProperties.HelpVisible = false;
            this.entityProperties.LineColor = System.Drawing.SystemColors.ControlDark;
            this.entityProperties.Location = new System.Drawing.Point(9, 57);
            this.entityProperties.Margin = new System.Windows.Forms.Padding(4);
            this.entityProperties.Name = "entityProperties";
            this.entityProperties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.entityProperties.Size = new System.Drawing.Size(283, 360);
            this.entityProperties.TabIndex = 1;
            this.entityProperties.ToolbarVisible = false;
            this.entityProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.entityProperties_PropertyValueChanged);
            // 
            // gbSpawn
            // 
            this.gbSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSpawn.Controls.Add(this.btnSpawn);
            this.gbSpawn.Controls.Add(this.cbSpawn);
            this.gbSpawn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gbSpawn.Location = new System.Drawing.Point(0, 4);
            this.gbSpawn.Margin = new System.Windows.Forms.Padding(4);
            this.gbSpawn.Name = "gbSpawn";
            this.gbSpawn.Padding = new System.Windows.Forms.Padding(4);
            this.gbSpawn.Size = new System.Drawing.Size(321, 60);
            this.gbSpawn.TabIndex = 4;
            this.gbSpawn.TabStop = false;
            this.gbSpawn.Text = "Entity Spawner";
            // 
            // btnSpawn
            // 
            this.btnSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpawn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSpawn.Location = new System.Drawing.Point(247, 22);
            this.btnSpawn.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpawn.Name = "btnSpawn";
            this.btnSpawn.Size = new System.Drawing.Size(67, 28);
            this.btnSpawn.TabIndex = 1;
            this.btnSpawn.Text = "Spawn";
            this.btnSpawn.UseVisualStyleBackColor = true;
            this.btnSpawn.Click += new System.EventHandler(this.btnSpawn_Click);
            // 
            // cbSpawn
            // 
            this.cbSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSpawn.DisplayMember = "Name";
            this.cbSpawn.FormattingEnabled = true;
            this.cbSpawn.Location = new System.Drawing.Point(9, 25);
            this.cbSpawn.Margin = new System.Windows.Forms.Padding(4);
            this.cbSpawn.Name = "cbSpawn";
            this.cbSpawn.Size = new System.Drawing.Size(230, 24);
            this.cbSpawn.TabIndex = 0;
            // 
            // EntityToolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Controls.Add(this.gbEditor);
            this.Controls.Add(this.gbSpawn);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Name = "EntityToolbar";
            this.Size = new System.Drawing.Size(325, 501);
            this.gbEditor.ResumeLayout(false);
            this.gbSpawn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEditor;
        public System.Windows.Forms.ComboBox entitiesList;
        public System.Windows.Forms.PropertyGrid entityProperties;
        private System.Windows.Forms.GroupBox gbSpawn;
        private System.Windows.Forms.Button btnSpawn;
        public System.Windows.Forms.ComboBox cbSpawn;
    }
}
