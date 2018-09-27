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
    public partial class CD12_LayerPropertiesForm : Form
    {

        public int LoadedRSDKver;

        public int CurLayer = 0;

        public RSDKv2.BGLayout Mapv3;
        public RSDKvB.BGLayout Mapv4;

        public CD12_LayerPropertiesForm(int RSDKver)
        {
            InitializeComponent();
            LoadedRSDKver = RSDKver;
        }

        public void Setup()
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    MapWidthNUD.Value = Mapv3.Layers[CurLayer].width;
                    MapHeightNUD.Value = Mapv3.Layers[CurLayer].height;
                    Unknown1NUD.Value = Mapv3.Layers[CurLayer].Unknown1;
                    Unknown2NUD.Value = Mapv3.Layers[CurLayer].Unknown2;
                    RelativeVSPDNUD.Value = Mapv3.Layers[CurLayer].RelativeVSpeed;
                    ConstantVSPDNUD.Value = Mapv3.Layers[CurLayer].ConstantVSpeed;
                    break;
                case 0:
                    MapWidthNUD.Value = Mapv4.Layers[CurLayer].width;
                    MapHeightNUD.Value = Mapv4.Layers[CurLayer].height;
                    Unknown1NUD.Value = Mapv4.Layers[CurLayer].Unknown1;
                    Unknown2NUD.Value = Mapv4.Layers[CurLayer].Unknown2;
                    RelativeVSPDNUD.Value = Mapv4.Layers[CurLayer].RelativeVSpeed;
                    ConstantVSPDNUD.Value = Mapv4.Layers[CurLayer].ConstantVSpeed;
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
                case 1:
                    Mapv3.Layers[CurLayer].width = (int)MapWidthNUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].width = (int)MapWidthNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void MapHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].height = (int)MapHeightNUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].height = (int)MapHeightNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown1NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].Unknown1 = (byte)Unknown1NUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].Unknown1 = (byte)Unknown1NUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown2NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].Unknown2 = (byte)Unknown2NUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].Unknown2 = (byte)Unknown2NUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void ConstantVSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].ConstantVSpeed = (byte)ConstantVSPDNUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].ConstantVSpeed = (byte)ConstantVSPDNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void RelativeVSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].RelativeVSpeed = (byte)RelativeVSPDNUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].RelativeVSpeed = (byte)RelativeVSPDNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown1NUD_ValueChanged_1(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].Unknown1 = (byte)Unknown1NUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].Unknown1 = (byte)Unknown1NUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Unknown2NUD_ValueChanged_1(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 1:
                    Mapv3.Layers[CurLayer].Unknown2 = (byte)Unknown2NUD.Value;
                    break;
                case 0:
                    Mapv4.Layers[CurLayer].Unknown2 = (byte)Unknown2NUD.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
