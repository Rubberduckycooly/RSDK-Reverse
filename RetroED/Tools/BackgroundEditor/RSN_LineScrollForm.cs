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

        public bool RemoveVal = false;

        /*The unknown values seem to be line positions for each LineScroll Class*/

        public RSDKvRS.BGLayout Mapv1;
        public RSDKv1.BGLayout Mapv2;
        public RSDKv2.BGLayout Mapv3;
        public RSDKvB.BGLayout Mapv4;

        int hv = 0;

        public RSN_LineScrollForm(int RSDKver, int PVal, int HV)
        {
            InitializeComponent();
            hv = HV;
            LoadedRSDKver = RSDKver;
            Pvalue = PVal;
        }

        public void Setup()
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        LineNoNUD.Value = Mapv1.HLines[Pvalue].RHSpeed;
                        SPDNUD.Value = Mapv1.HLines[Pvalue].CHSpeed;
                        DeformNUD.Value = Mapv1.HLines[Pvalue].Deform;
                        break;
                    case 2:
                        LineNoNUD.Value = Mapv2.HLines[Pvalue].RHSpeed;
                        SPDNUD.Value = Mapv2.HLines[Pvalue].CHSpeed;
                        DeformNUD.Value = Mapv2.HLines[Pvalue].Deform;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        LineNoNUD.Value = Mapv1.VLines[Pvalue].RHSpeed;
                        SPDNUD.Value = Mapv1.VLines[Pvalue].CHSpeed;
                        DeformNUD.Value = Mapv1.VLines[Pvalue].Deform;
                        break;
                    case 2:
                        LineNoNUD.Value = Mapv2.VLines[Pvalue].RHSpeed;
                        SPDNUD.Value = Mapv2.VLines[Pvalue].CHSpeed;
                        DeformNUD.Value = Mapv2.VLines[Pvalue].Deform;
                        break;
                    default:
                        break;
                }
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
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        Mapv1.HLines[Pvalue].RHSpeed = (byte)LineNoNUD.Value;
                        break;
                    case 2:
                        Mapv2.HLines[Pvalue].RHSpeed = (byte)LineNoNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        Mapv1.VLines[Pvalue].RHSpeed = (byte)LineNoNUD.Value;
                        break;
                    case 2:
                        Mapv2.VLines[Pvalue].RHSpeed = (byte)LineNoNUD.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void SPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        Mapv1.HLines[Pvalue].CHSpeed = (byte)SPDNUD.Value;
                        break;
                    case 2:
                        Mapv2.HLines[Pvalue].CHSpeed = (byte)SPDNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        Mapv1.VLines[Pvalue].CHSpeed = (byte)SPDNUD.Value;
                        break;
                    case 2:
                        Mapv2.VLines[Pvalue].CHSpeed = (byte)SPDNUD.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void CSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        Mapv1.HLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    case 2:
                        Mapv2.HLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        Mapv1.VLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    case 2:
                        Mapv2.VLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveVal = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
