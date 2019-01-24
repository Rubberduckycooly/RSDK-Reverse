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

        public RSDKvRS.BGLayout BackgroundvRS;
        public RSDKv1.BGLayout Backgroundv1;

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
                        LineNoNUD.Value = BackgroundvRS.HLines[Pvalue].RelativeSpeed;
                        SPDNUD.Value = BackgroundvRS.HLines[Pvalue].ConstantSpeed;
                        DeformNUD.Value = BackgroundvRS.HLines[Pvalue].Deform;
                        break;
                    case 2:
                        LineNoNUD.Value = Backgroundv1.HLines[Pvalue].RelativeSpeed;
                        SPDNUD.Value = Backgroundv1.HLines[Pvalue].ConstantSpeed;
                        DeformNUD.Value = Backgroundv1.HLines[Pvalue].Deform;
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
                        LineNoNUD.Value = BackgroundvRS.VLines[Pvalue].RelativeSpeed;
                        SPDNUD.Value = BackgroundvRS.VLines[Pvalue].RelativeSpeed;
                        DeformNUD.Value = BackgroundvRS.VLines[Pvalue].Deform;
                        break;
                    case 2:
                        LineNoNUD.Value = Backgroundv1.VLines[Pvalue].RelativeSpeed;
                        SPDNUD.Value = Backgroundv1.VLines[Pvalue].ConstantSpeed;
                        DeformNUD.Value = Backgroundv1.VLines[Pvalue].Deform;
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

        private void CSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        BackgroundvRS.HLines[Pvalue].ConstantSpeed = (byte)SPDNUD.Value;
                        break;
                    case 2:
                        Backgroundv1.HLines[Pvalue].ConstantSpeed = (byte)SPDNUD.Value;
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
                        BackgroundvRS.VLines[Pvalue].ConstantSpeed = (byte)SPDNUD.Value;
                        break;
                    case 2:
                        Backgroundv1.VLines[Pvalue].ConstantSpeed = (byte)SPDNUD.Value;
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

        private void DeformNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        BackgroundvRS.HLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    case 2:
                        Backgroundv1.HLines[Pvalue].Deform = (byte)DeformNUD.Value;
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
                        BackgroundvRS.VLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    case 2:
                        Backgroundv1.VLines[Pvalue].Deform = (byte)DeformNUD.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void RSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        BackgroundvRS.HLines[Pvalue].RelativeSpeed = (byte)LineNoNUD.Value;
                        break;
                    case 2:
                        Backgroundv1.HLines[Pvalue].RelativeSpeed = (byte)LineNoNUD.Value;
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
                        BackgroundvRS.VLines[Pvalue].RelativeSpeed = (byte)LineNoNUD.Value;
                        break;
                    case 2:
                        Backgroundv1.VLines[Pvalue].RelativeSpeed = (byte)LineNoNUD.Value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
