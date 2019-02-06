using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.BackgroundEditor
{
    public partial class CD12_LayerPropertiesForm : DockContent
    {

        public Retro_Formats.EngineType engineType;

        public int CurLayer = 0;

        public Retro_Formats.Background Background = new Retro_Formats.Background();

        public CD12_LayerPropertiesForm(Retro_Formats.EngineType RSDKver)
        {
            InitializeComponent();
            engineType = RSDKver;
        }

        public void Setup()
        {
            MapWidthNUD.Value = Background.Layers[CurLayer].width;
            MapHeightNUD.Value = Background.Layers[CurLayer].height;
            DrawLayerNUD.Value = Background.Layers[CurLayer].DrawLayer;
            BehaviourNUD.Value = Background.Layers[CurLayer].Behaviour;
            RelativeVSPDNUD.Value = Background.Layers[CurLayer].RelativeSpeed;
            ConstantVSPDNUD.Value = Background.Layers[CurLayer].ConstantSpeed;

            if (engineType == Retro_Formats.EngineType.RSDKv2)
            {
                MapWidthNUD.Maximum = 255;
                MapHeightNUD.Maximum = 255;
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
            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKv2:
                    Background.Layers[CurLayer].width = (byte)MapWidthNUD.Value;
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    Background.Layers[CurLayer].width = (ushort)MapWidthNUD.Value;
                    break;
            }
        }

        private void MapHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKv2:
                    Background.Layers[CurLayer].height = (byte)MapHeightNUD.Value;
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    Background.Layers[CurLayer].height = (ushort)MapHeightNUD.Value;
                    break;
            }
        }

        private void Unknown1NUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].DrawLayer = (byte)DrawLayerNUD.Value;
        }

        private void Unknown2NUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].Behaviour = (byte)BehaviourNUD.Value;
        }

        private void ConstantVSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].ConstantSpeed = (byte)ConstantVSPDNUD.Value;
        }

        private void RelativeVSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].RelativeSpeed = (byte)RelativeVSPDNUD.Value;
        }

        private void Unknown1NUD_ValueChanged_1(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].DrawLayer = (byte)DrawLayerNUD.Value;
        }

        private void Unknown2NUD_ValueChanged_1(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].Behaviour = (byte)BehaviourNUD.Value;
        }
    }
}
