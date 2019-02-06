namespace RetroED.Tools.PaletteEditor
{
  internal partial class MainForm
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
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItem_New = new System.Windows.Forms.MenuItem();
            this.MenuItem_Open = new System.Windows.Forms.MenuItem();
            this.MenuItem_Save = new System.Windows.Forms.MenuItem();
            this.MenuItem_SaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.MenuItem_ShowPalRotations = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.MenuItem_ViewRSPal = new System.Windows.Forms.MenuItem();
            this.MenuItem_SNPal = new System.Windows.Forms.MenuItem();
            this.MenuItem_SCDPal = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.MenuItem_ExportPal = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.colorGrid);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.colorEditor);
            this.SplitContainer.Size = new System.Drawing.Size(814, 339);
            this.SplitContainer.SplitterDistance = 407;
            this.SplitContainer.SplitterWidth = 7;
            this.SplitContainer.TabIndex = 1;
            // 
            // colorGrid
            // 
            this.colorGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorGrid.CellSize = new System.Drawing.Size(20, 20);
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorGrid.Location = new System.Drawing.Point(13, 4);
            this.colorGrid.Margin = new System.Windows.Forms.Padding(4);
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.colorGrid.Palette = Cyotek.Windows.Forms.ColorPalette.None;
            this.colorGrid.ShowCustomColors = false;
            this.colorGrid.Size = new System.Drawing.Size(416, 39);
            this.colorGrid.Spacing = new System.Drawing.Size(0, 0);
            this.colorGrid.TabIndex = 2;
            this.colorGrid.ColorChanged += new System.EventHandler(this.colorGrid_ColorChanged);
            // 
            // colorEditor
            // 
            this.colorEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.colorEditor.Location = new System.Drawing.Point(0, 0);
            this.colorEditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorEditor.Name = "colorEditor";
            this.colorEditor.Size = new System.Drawing.Size(400, 339);
            this.colorEditor.TabIndex = 1;
            this.colorEditor.ColorChanged += new System.EventHandler(this.colorEditor_ColorChanged);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_New,
            this.MenuItem_Open,
            this.MenuItem_Save,
            this.MenuItem_SaveAs});
            this.menuItem1.Text = "File";
            // 
            // MenuItem_New
            // 
            this.MenuItem_New.Index = 0;
            this.MenuItem_New.Text = "New";
            this.MenuItem_New.Click += new System.EventHandler(this.MenuItem_New_Click);
            // 
            // MenuItem_Open
            // 
            this.MenuItem_Open.Index = 1;
            this.MenuItem_Open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.MenuItem_Open.Text = "&Open";
            this.MenuItem_Open.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // MenuItem_Save
            // 
            this.MenuItem_Save.Index = 2;
            this.MenuItem_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.MenuItem_Save.Text = "&Save";
            this.MenuItem_Save.Click += new System.EventHandler(this.MenuItem_Save_Click);
            // 
            // MenuItem_SaveAs
            // 
            this.MenuItem_SaveAs.Index = 3;
            this.MenuItem_SaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.MenuItem_SaveAs.Text = "Save &As";
            this.MenuItem_SaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_ShowPalRotations});
            this.menuItem2.Text = "View";
            // 
            // MenuItem_ShowPalRotations
            // 
            this.MenuItem_ShowPalRotations.Index = 0;
            this.MenuItem_ShowPalRotations.Text = "Show Palette Rotations";
            this.MenuItem_ShowPalRotations.Click += new System.EventHandler(this.showPaletteRotationsToolStripMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_ViewRSPal,
            this.MenuItem_SNPal,
            this.MenuItem_SCDPal,
            this.menuItem8,
            this.MenuItem_ExportPal});
            this.menuItem3.Text = "Tools";
            // 
            // MenuItem_ViewRSPal
            // 
            this.MenuItem_ViewRSPal.Index = 0;
            this.MenuItem_ViewRSPal.Text = "View Retro-Sonic Internal Palette";
            this.MenuItem_ViewRSPal.Click += new System.EventHandler(this.viewRetroSonicInternalPaletteToolStripMenuItem_Click);
            // 
            // MenuItem_SNPal
            // 
            this.MenuItem_SNPal.Index = 1;
            this.MenuItem_SNPal.Text = "View Sonic Nexus Internal Palette";
            this.MenuItem_SNPal.Click += new System.EventHandler(this.viewSonicNexusInternalPaletteToolStripMenuItem_Click);
            // 
            // MenuItem_SCDPal
            // 
            this.MenuItem_SCDPal.Index = 2;
            this.MenuItem_SCDPal.Text = "View Sonic CD Internal Palette";
            this.MenuItem_SCDPal.Click += new System.EventHandler(this.viewSonicCDInternalPaletteToolStripMenuItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.Text = "-";
            // 
            // MenuItem_ExportPal
            // 
            this.MenuItem_ExportPal.Index = 4;
            this.MenuItem_ExportPal.Text = "Export Loaded Palette to...";
            this.MenuItem_ExportPal.Click += new System.EventHandler(this.exportLoadedPaletteToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(814, 339);
            this.Controls.Add(this.SplitContainer);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "RSDK Palette Editor";
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
        private System.Windows.Forms.SplitContainer SplitContainer;
        private Cyotek.Windows.Forms.ColorGrid colorGrid;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem MenuItem_New;
        private System.Windows.Forms.MenuItem MenuItem_Open;
        private System.Windows.Forms.MenuItem MenuItem_Save;
        private System.Windows.Forms.MenuItem MenuItem_SaveAs;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem MenuItem_ShowPalRotations;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem MenuItem_ViewRSPal;
        private System.Windows.Forms.MenuItem MenuItem_SNPal;
        private System.Windows.Forms.MenuItem MenuItem_SCDPal;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem MenuItem_ExportPal;
    }
}
