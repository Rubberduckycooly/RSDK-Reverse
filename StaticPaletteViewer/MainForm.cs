using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;

namespace RetroED.Tools.PaletteEditor
{
  // Cyotek Color Picker controls library
  // Copyright © 2013-2017 Cyotek Ltd.
  // http://cyotek.com/blog/tag/colorpicker

  // Licensed under the MIT License. See license.txt for the full text.

  // If you use this code in your applications, donations or attribution are welcome

  internal partial class MainForm : BaseForm
  {

    #region Constructors

        public MainForm()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Properties

    private string PalettePath
    {
      get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "palettes"); }
    }

        public bool ShowPalRotations = true;

        Timer palTimer = new Timer();

        #endregion

        #region Methods

        protected override void OnLoad(EventArgs e)
        {
                base.OnLoad(e);
                colorGrid.Colors.Clear();
            for (int i = 0; i < 256; i++)
            {
                colorGrid.Colors.Add(Color.FromArgb(255, 255, 0, 255));
            }
        }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

        private void colorGrid_ColorChanged(object sender, EventArgs e)
        {
            SplitContainer.Panel2.BackColor = colorGrid.Color;

            colorToolStripStatusLabel.Text = string.Format("{0}, {1}, {2}", colorGrid.Color.R, colorGrid.Color.G, colorGrid.Color.B);

            colorEditor.Color = colorGrid.Color;
            colorEditor.BackColor = SplitContainer.Panel1.BackColor;
            SplitContainer.Panel2.BackColor = SplitContainer.Panel1.BackColor;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Static Objects|*.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                StaticPaletteViewer.OpenPaletteForm frm = new StaticPaletteViewer.OpenPaletteForm();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Cyotek.Windows.Forms.ColorCollection pal = new Cyotek.Windows.Forms.ColorCollection();
                    colorGrid.Colors.Clear(); //Clear the colourGrid!

                    BinaryReader reader = new BinaryReader(File.OpenRead(dlg.FileName));
                    reader.BaseStream.Position = frm.PalOffset;

                    for (int i = 0; i < frm.ColourCount; i++)
                    {
                        byte r = reader.ReadByte();
                        byte g = reader.ReadByte();
                        byte b = reader.ReadByte();
                        reader.ReadByte();
                        Color c = Color.FromArgb(r, g, b);
                        pal.Add(c);
                    }

                    Text = Path.GetFileName(dlg.FileName) + " - RSDK Palette Editor";

                    /*Parent.rp.state = "RetroED - " + this.Text;
                    Parent.rp.details = "Editing: " + System.IO.Path.GetFileName(dlg.FileName);
                    SharpPresence.Discord.RunCallbacks();
                    SharpPresence.Discord.UpdatePresence(Parent.rp);*/

                    colorGrid.Colors = pal; //Set the Colourgrid's colours!
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Static Objects|*.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
            }

        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
                if (colorGrid.ColorIndex < colorGrid.Colors.Count && colorGrid.ColorIndex >= 0) { colorGrid.Colors[colorGrid.ColorIndex] = colorEditor.Color; } //Change the colour if it's in bounds of the palette

        }

        #endregion

    }
}
