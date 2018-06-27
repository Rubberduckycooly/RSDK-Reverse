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
        RSDKv1.zcf sc1;
        RSDKv4.GameConfig gc4;
        RSDKv5.GameConfig gc5;
        RSDKv5.StageConfig sc5;

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


        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKv1 ZoneConfig Files|Zone.zcf|RSDKv4 GameConfig Files|GameConfig.bin|Act Files|*.act";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ColorCollection pal = new ColorCollection();
                colorGrid.Colors.Clear();
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        sc1 = new RSDKv1.zcf(dlg.FileName);

                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, sc1.palette.Colors[h][w].R, sc1.palette.Colors[h][w].G, sc1.palette.Colors[h][w].B);
                                pal.Add(c);
                            }
                        }
                        break;
                    case 1:
                        gc4 = new RSDKv4.GameConfig(dlg.FileName);

                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < gc4.Palette.COLORS_PER_COLUMN; w++)
                            {
                                Color c = Color.FromArgb(255, gc4.Palette.Colors[h][w].R, gc4.Palette.Colors[h][w].G, gc4.Palette.Colors[h][w].B);
                                pal.Add(c);
                            }
                        }
                        break;
                    /*case 2:
                        gc5 = new RSDKv5.GameConfig(dlg.FileName);

                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, gc5.Palettes[0].Colors[h][w].R, gc5.Palettes[0].Colors[h][w].G, gc5.Palettes[0].Colors[h][w].B);
                                pal.Add(c);
                            }
                        }
                        break;
                    case 3:
                        sc5 = new RSDKv5.StageConfig(dlg.FileName);

                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, sc5.Palettes[0].Colors[h][w].R, sc5.Palettes[0].Colors[h][w].G, sc5.Palettes[0].Colors[h][w].B);
                                pal.Add(c);
                            }
                        }
                        break;*/
                    case 2:
                        pal = ColorCollection.LoadPalette(dlg.FileName);
                        break;
                    default:
                        break;
                }
                Text = Path.GetFileName(dlg.FileName) + " - RSDK Palette Editor";
                colorGrid.Colors = pal;

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKv1 ZoneConfig Files|Zone*.zcf|RSDKv4 GameConfig Files|GameConfig*.bin|Act Files|*.act";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (dlg.FilterIndex - 1)
                {
                    case 0:
                        int i1 = 0;
                        for (int h = 0; h < 2; h++)
                        {
                            for (int w = 0; w < 16; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i1].R, colorGrid.Colors[i1].G, colorGrid.Colors[i1].B);
                                i1++;
                                sc1.palette.Colors[h][w].R = c.R;
                                sc1.palette.Colors[h][w].G = c.G;
                                sc1.palette.Colors[h][w].B = c.B;
                            }
                        }
                        sc1.Write(dlg.FileName);
                        break;
                    case 1:
                        int i4 = 0;
                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < gc4.Palette.COLORS_PER_COLUMN; w++)
                            {
                                Color c = Color.FromArgb(255, colorGrid.Colors[i4].R, colorGrid.Colors[i4].G, colorGrid.Colors[i4].B);
                                i4++;
                                gc4.Palette.Colors[h][w].R = c.R;
                                gc4.Palette.Colors[h][w].G = c.G;
                                gc4.Palette.Colors[h][w].B = c.B;
                            }
                        }
                        gc4.Write(dlg.FileName);
                        break;
                    case 2:
                        IPaletteSerializer serializer;

                        serializer = new AdobeColorTablePaletteSerializer();

                        using (Stream stream = File.Create(dlg.FileName))
                        {
                            serializer.Serialize(stream, colorGrid.Colors);
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            if (colorGrid.ColorIndex <= 255 && colorGrid.ColorIndex >= 0) { colorGrid.Colors[colorGrid.ColorIndex] = colorEditor.Color; }
        }
    }
}
