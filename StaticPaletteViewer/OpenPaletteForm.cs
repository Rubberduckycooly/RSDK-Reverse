using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaticPaletteViewer
{
    public partial class OpenPaletteForm : Form
    {

        public int PalOffset = 0;
        public int ColourCount = 0;

        public OpenPaletteForm()
        {
            InitializeComponent();
        }

        private void PalOffsetNUD_ValueChanged(object sender, EventArgs e)
        {
            PalOffset = (int)PalOffsetNUD.Value;
        }

        private void ColourCountNUD_ValueChanged(object sender, EventArgs e)
        {
            ColourCount = (int)ColourCountNUD.Value;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
