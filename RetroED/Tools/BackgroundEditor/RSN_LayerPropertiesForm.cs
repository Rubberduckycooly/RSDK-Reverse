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
    public partial class RSN_LayerPropertiesForm : Form
    {

        public int LoadedRSDKver;

        public int CurLayer = 0;

        public RSDKvRS.BGLayout Mapv1;
        public RSDKv1.BGLayout Mapv2;

        public RSN_LayerPropertiesForm(int RSDKver)
        {
            InitializeComponent();
            LoadedRSDKver = RSDKver;
        }

        public void Setup()
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    MapWidthNUD.Value = Mapv1.Layers[CurLayer].width;
                    MapHeightNUD.Value = Mapv1.Layers[CurLayer].height;
                    DeformNUD.Value = Mapv1.Layers[CurLayer].Deform;
                    RVSPDNUD.Value = Mapv1.Layers[CurLayer].RelativeVSpeed;
                    CVSPDNUD.Value = Mapv1.Layers[CurLayer].ConstantVSpeed;
                    break;
                case 2:
                    MapWidthNUD.Value = Mapv2.Layers[CurLayer].width;
                    MapHeightNUD.Value = Mapv2.Layers[CurLayer].height;
                    DeformNUD.Value = Mapv2.Layers[CurLayer].Deform;
                    RVSPDNUD.Value = Mapv2.Layers[CurLayer].RelativeVSpeed;
                    CVSPDNUD.Value = Mapv2.Layers[CurLayer].ConstantVSpeed;
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
        private void MapWidthNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Layers[CurLayer].width = (int)MapWidthNUD.Value;
                    break;
                case 2:
                    Mapv2.Layers[CurLayer].width = (int)MapWidthNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void MapHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Layers[CurLayer].height = (int)MapHeightNUD.Value;
                    break;
                case 2:
                    Mapv2.Layers[CurLayer].height = (int)MapHeightNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown1NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Layers[CurLayer].Deform = (byte)DeformNUD.Value;
                    break;
                case 2:
                    Mapv2.Layers[CurLayer].Deform = (byte)DeformNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown2NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Layers[CurLayer].RelativeVSpeed = (byte)RVSPDNUD.Value;
                    break;
                case 2:
                    Mapv2.Layers[CurLayer].RelativeVSpeed = (byte)RVSPDNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown3NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Layers[CurLayer].ConstantVSpeed = (byte)CVSPDNUD.Value;
                    break;
                case 2:
                    Mapv2.Layers[CurLayer].ConstantVSpeed = (byte)CVSPDNUD.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
