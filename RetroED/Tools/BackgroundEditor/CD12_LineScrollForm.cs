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

        public bool RemoveVal = false;

        public int Pvalue;

        public RSDKvRS.BGLayout Mapv1;
        public RSDKv1.BGLayout Mapv2;
        public RSDKv2.BGLayout Mapv3;
        public RSDKvB.BGLayout Mapv4;

        int hv = 0;

        public CD12_LineScrollForm(int RSDKver, int PVal, int HV)
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
                    case 1:
                        BehaviourNUD.Value = Mapv3.HLines[Pvalue].Behaviour;
                        RSPDNUD.Value = Mapv3.HLines[Pvalue].RelativeSpeed;
                        CSPDNUD.Value = Mapv3.HLines[Pvalue].ConstantSpeed;
                        DrawLayerNUD.Value = Mapv3.HLines[Pvalue].DrawLayer;
                        break;
                    case 0:
                        BehaviourNUD.Value = Mapv4.HLines[Pvalue].Behaviour;
                        RSPDNUD.Value = Mapv4.HLines[Pvalue].RelativeSpeed;
                        CSPDNUD.Value = Mapv4.HLines[Pvalue].ConstantSpeed;
                        DrawLayerNUD.Value = Mapv4.HLines[Pvalue].Behaviour;
                        break;
                    default:
                        break;
                }
            }
            if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        BehaviourNUD.Value = Mapv3.VLines[Pvalue].Behaviour;
                        RSPDNUD.Value = Mapv3.VLines[Pvalue].RelativeSpeed;
                        CSPDNUD.Value = Mapv3.VLines[Pvalue].ConstantSpeed;
                        DrawLayerNUD.Value = Mapv3.VLines[Pvalue].DrawLayer;
                        break;
                    case 0:
                        BehaviourNUD.Value = Mapv4.VLines[Pvalue].Behaviour;
                        RSPDNUD.Value = Mapv4.VLines[Pvalue].RelativeSpeed;
                        CSPDNUD.Value = Mapv4.VLines[Pvalue].ConstantSpeed;
                        DrawLayerNUD.Value = Mapv4.VLines[Pvalue].DrawLayer;
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
        private void Unknown1NUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        Mapv3.HLines[Pvalue].Behaviour = (byte)BehaviourNUD.Value;
                        break;
                    case 0:
                        Mapv4.HLines[Pvalue].Behaviour = (byte)BehaviourNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        Mapv3.VLines[Pvalue].Behaviour = (byte)BehaviourNUD.Value;
                        break;
                    case 0:
                        Mapv4.VLines[Pvalue].Behaviour = (byte)BehaviourNUD.Value;
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
                    case 1:
                        Mapv3.HLines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
                        break;
                    case 0:
                        Mapv4.HLines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        Mapv3.VLines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
                        break;
                    case 0:
                        Mapv4.VLines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
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
                    case 1:
                        Mapv3.HLines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
                        break;
                    case 0:
                        Mapv4.HLines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        Mapv3.VLines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
                        break;
                    case 0:
                        Mapv4.VLines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void UnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        Mapv3.HLines[Pvalue].DrawLayer = (byte)DrawLayerNUD.Value;
                        break;
                    case 0:
                        Mapv4.HLines[Pvalue].DrawLayer = (byte)DrawLayerNUD.Value;
                        break;
                    default:
                        break;
                }
            }
            else if (hv == 1)
            {
                switch (LoadedRSDKver)
                {
                    case 1:
                        Mapv3.VLines[Pvalue].DrawLayer = (byte)DrawLayerNUD.Value;
                        break;
                    case 0:
                        Mapv4.VLines[Pvalue].DrawLayer = (byte)DrawLayerNUD.Value;
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
