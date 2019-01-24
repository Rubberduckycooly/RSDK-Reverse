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

        public RSDKv2.BGLayout Mapv2;
        public RSDKvB.BGLayout MapvB;

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
                    MapWidthNUD.Value = Mapv2.Layers[CurLayer].width;
                    MapHeightNUD.Value = Mapv2.Layers[CurLayer].height;
                    DrawLayerNUD.Value = Mapv2.Layers[CurLayer].DrawLayer;
                    BehaviourNUD.Value = Mapv2.Layers[CurLayer].Behaviour;
                    RelativeVSPDNUD.Value = Mapv2.Layers[CurLayer].RelativeSpeed;
                    ConstantVSPDNUD.Value = Mapv2.Layers[CurLayer].ConstantSpeed;
                    break;
                case 0:
                    MapWidthNUD.Value = MapvB.Layers[CurLayer].width;
                    MapHeightNUD.Value = MapvB.Layers[CurLayer].height;
                    DrawLayerNUD.Value = MapvB.Layers[CurLayer].DrawLayer;
                    BehaviourNUD.Value = MapvB.Layers[CurLayer].Behaviour;
                    RelativeVSPDNUD.Value = MapvB.Layers[CurLayer].RelativeSpeed;
                    ConstantVSPDNUD.Value = MapvB.Layers[CurLayer].ConstantSpeed;
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
                    Mapv2.Layers[CurLayer].width = (byte)MapWidthNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].width = (ushort)MapWidthNUD.Value;
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
                    Mapv2.Layers[CurLayer].height = (byte)MapHeightNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].height = (ushort)MapHeightNUD.Value;
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
                    Mapv2.Layers[CurLayer].DrawLayer = (byte)DrawLayerNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].DrawLayer = (byte)DrawLayerNUD.Value;
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
                    Mapv2.Layers[CurLayer].Behaviour = (byte)BehaviourNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].Behaviour = (byte)BehaviourNUD.Value;
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
                    Mapv2.Layers[CurLayer].ConstantSpeed = (byte)ConstantVSPDNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].ConstantSpeed = (byte)ConstantVSPDNUD.Value;
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
                    Mapv2.Layers[CurLayer].RelativeSpeed = (byte)RelativeVSPDNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].RelativeSpeed = (byte)RelativeVSPDNUD.Value;
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
                    Mapv2.Layers[CurLayer].DrawLayer = (byte)DrawLayerNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].DrawLayer = (byte)DrawLayerNUD.Value;
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
                    Mapv2.Layers[CurLayer].Behaviour = (byte)BehaviourNUD.Value;
                    break;
                case 0:
                    MapvB.Layers[CurLayer].Behaviour = (byte)BehaviourNUD.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
