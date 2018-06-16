using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cyotek.Windows.Forms.ColorPicker.Demo
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

    private void addCustomColorsButton_Click(object sender, EventArgs e)
    {
      colorGrid.CustomColors = ColorPalettes.QbColors;
    }

    private void addNewColorButton_Click(object sender, EventArgs e)
    {
      int r;
      int g;
      int b;
      int a;
      Random random;

      random = new Random();
      r = random.Next(0, 254);
      g = random.Next(0, 254);
      b = random.Next(0, 254);
      a = random.Next(0, 254);

      colorGrid.Color = Color.FromArgb(a, r, g, b);
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void colorGrid_ColorChanged(object sender, EventArgs e)
    {
      optionsSplitContainer.Panel2.BackColor = colorGrid.Color;

      colorToolStripStatusLabel.Text = string.Format("{0}, {1}, {2}", colorGrid.Color.R, colorGrid.Color.G, colorGrid.Color.B);

            colorEditor.Color = colorGrid.Color;
            colorEditor.BackColor = optionsSplitContainer.Panel1.BackColor;
            optionsSplitContainer.Panel2.BackColor = optionsSplitContainer.Panel1.BackColor;
        }

    private void grayScaleButton_Click(object sender, EventArgs e)
    {
      colorGrid.Colors = new ColorCollection(Enumerable.Range(0, 254).Select(i => Color.FromArgb(i, i, i)));
    }

    private void hexagonPaletteButton_Click(object sender, EventArgs e)
    {
      // NOTE: Predefined palettes can now be set via the Palette property
      colorGrid.Colors = ColorPalettes.HexagonPalette;
    }

    private void office2010Button_Click(object sender, EventArgs e)
    {
      // NOTE: Predefined palettes can now be set via the Palette property (this does not affect other properties such as Columns below though!)
      colorGrid.Colors = ColorPalettes.Office2010Standard;
      colorGrid.Columns = 10;
    }

    private void paintNetPaletteButton_Click(object sender, EventArgs e)
    {
      // NOTE: Predefined palettes can now be set via the Palette property
      colorGrid.Colors = ColorPalettes.PaintPalette;
    }

    private void resetCustomColorsButton_Click(object sender, EventArgs e)
    {
      colorGrid.CustomColors = new ColorCollection(Enumerable.Repeat(Color.White, 32));
    }

    private void savePaletteButton_Click(object sender, EventArgs e)
    {
      using (FileDialog dialog = new SaveFileDialog
                                 {
                                   Filter = PaletteSerializer.DefaultSaveFilter,
                                   Title = "Save Palette As"
                                 })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          IPaletteSerializer serializer;

          serializer = PaletteSerializer.AllSerializers.Where(s => s.CanWrite).ElementAt(dialog.FilterIndex - 1);

          using (Stream stream = File.Create(dialog.FileName))
          {
            serializer.Serialize(stream, colorGrid.Colors);
          }
        }
      }
    }

    private void shadesOfBlueButton_Click(object sender, EventArgs e)
    {
      colorGrid.Colors = new ColorCollection(Enumerable.Range(0, 254).Select(i => Color.FromArgb(0, 0, i)));
    }

    private void shadesOfGreenButton_Click(object sender, EventArgs e)
    {
      colorGrid.Colors = new ColorCollection(Enumerable.Range(0, 254).Select(i => Color.FromArgb(0, i, 0)));
    }

    private void shadesOfRedButton_Click(object sender, EventArgs e)
    {
      colorGrid.Colors = new ColorCollection(Enumerable.Range(0, 254).Select(i => Color.FromArgb(i, 0, 0)));
    }

    private void standardColorsButton_Click(object sender, EventArgs e)
    {
      // NOTE: Predefined palettes can now be set via the Palette property
      colorGrid.Colors = ColorPalettes.NamedColors;
    }

        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorGrid.Colors.Clear();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Act Files|*.act|RSDKv4 GameConfig Files|GameConfig.bin";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ColorCollection pal = new ColorCollection();
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        break;
                    case 1:
                        RSDKv4.GameConfig gc = new RSDKv4.GameConfig(dlg.FileName);

                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < gc.Palettes[0].COLORS_PER_COLUMN; w++)
                            {
                                Color c = Color.FromArgb(255, gc.Palettes[0].Colors[h][w].R, gc.Palettes[0].Colors[h][w].G, gc.Palettes[0].Colors[h][w].B);
                                pal.Add(c);
                            }

                        }
                        break;
                    default:
                        break;
                }

                colorGrid.Colors = pal;

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
    }
}
