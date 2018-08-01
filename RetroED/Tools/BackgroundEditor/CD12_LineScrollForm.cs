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
    public partial class CD12_LineScrollForm : Form
    {

        public int LoadedRSDKver;

        public int Pvalue;

        public RSDKv1.BGLayout Mapv1;
        public RSDKv2.BGLayout Mapv2;
        public RSDKv3.BGLayout Mapv3;
        public RSDKv4.BGLayout Mapv4;

        public CD12_LineScrollForm(int RSDKver, int PVal)
        {
            InitializeComponent();
            LoadedRSDKver = RSDKver;
            Pvalue = PVal;
        }

        public void Setup()
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    LineNoNUD.Value = Mapv3.Lines[Pvalue].LineNo;
                    RSPDNUD.Value = Mapv3.Lines[Pvalue].RelativeSpeed;
                    CSPDNUD.Value = Mapv3.Lines[Pvalue].ConstantSpeed;
                    UnknownNUD.Value = Mapv3.Lines[Pvalue].Unknown;
                    break;
                case 0:
                    LineNoNUD.Value = Mapv4.Lines[Pvalue].LineNo;
                    RSPDNUD.Value = Mapv4.Lines[Pvalue].RelativeSpeed;
                    CSPDNUD.Value = Mapv4.Lines[Pvalue].ConstantSpeed;
                    UnknownNUD.Value = Mapv4.Lines[Pvalue].Unknown;
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
                case 1:
                    Mapv3.Lines[Pvalue].LineNo = (byte)LineNoNUD.Value;
                    break;
                case 0:
                    Mapv4.Lines[Pvalue].LineNo = (byte)LineNoNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void RSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Lines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
                    break;
                case 0:
                    Mapv4.Lines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void CSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Lines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
                    break;
                case 0:
                    Mapv4.Lines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void UnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Lines[Pvalue].Unknown = (byte)UnknownNUD.Value;
                    break;
                case 0:
                    Mapv4.Lines[Pvalue].Unknown = (byte)UnknownNUD.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
