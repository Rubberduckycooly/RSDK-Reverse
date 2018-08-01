using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Tools.BackgroundEditor
{
    public partial class RSN_LineScrollForm : Form
    {

        public int LoadedRSDKver;

        public int Pvalue;

        /*The unknown values seem to be line positions for each LineScroll Class*/

        public RSDKv1.BGLayout Mapv1;
        public RSDKv2.BGLayout Mapv2;
        public RSDKv3.BGLayout Mapv3;
        public RSDKv4.BGLayout Mapv4;

        public RSN_LineScrollForm(int RSDKver, int PVal)
        {
            InitializeComponent();
            LoadedRSDKver = RSDKver;
            Pvalue = PVal;
        }

        public void Setup()
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    LineNoNUD.Value = Mapv1.Lines[Pvalue].RHSpeed;
                    SPDNUD.Value = Mapv1.Lines[Pvalue].CHSpeed;
                    DeformNUD.Value = Mapv1.Lines[Pvalue].Deform;
                    break;
                case 2:
                    LineNoNUD.Value = Mapv2.Lines[Pvalue].RHSpeed;
                    SPDNUD.Value = Mapv2.Lines[Pvalue].CHSpeed;
                    DeformNUD.Value = Mapv2.Lines[Pvalue].Deform;
                    break;
                default:
                    break;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void LineNoNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Lines[Pvalue].RHSpeed = (byte)LineNoNUD.Value;
                    break;
                case 2:
                    Mapv2.Lines[Pvalue].RHSpeed = (byte)LineNoNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void SPDNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Lines[Pvalue].CHSpeed = (byte)SPDNUD.Value;
                    break;
                case 2:
                    Mapv2.Lines[Pvalue].CHSpeed = (byte)SPDNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void CSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Lines[Pvalue].Deform = (byte)DeformNUD.Value;
                    break;
                case 2:
                    Mapv2.Lines[Pvalue].Deform = (byte)DeformNUD.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
