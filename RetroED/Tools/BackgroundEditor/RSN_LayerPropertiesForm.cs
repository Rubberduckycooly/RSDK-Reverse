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
    public partial class RSN_LayerPropertiesForm : DockContent
    {

        public Retro_Formats.EngineType engineType;

        public int CurLayer = 0;

        public Retro_Formats.Background Background = new Retro_Formats.Background();

        public RSN_LayerPropertiesForm(Retro_Formats.EngineType RSDKver)
        {
            InitializeComponent();
            engineType = RSDKver;
        }

        public void Setup()
        {
            MapWidthNUD.Value = Background.Layers[CurLayer].width;
            MapHeightNUD.Value = Background.Layers[CurLayer].height;
            DeformNUD.Value = Background.Layers[CurLayer].Behaviour;
            RVSPDNUD.Value = Background.Layers[CurLayer].RelativeSpeed;
            CVSPDNUD.Value = Background.Layers[CurLayer].ConstantSpeed;
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
            Background.Layers[CurLayer].width = (byte)MapWidthNUD.Value;
        }

        private void MapHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].height = (byte)MapHeightNUD.Value;
        }

        private void Unknown1NUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].Behaviour = (byte)DeformNUD.Value;
        }

        private void Unknown2NUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].RelativeSpeed = (byte)RVSPDNUD.Value;
        }

        private void Unknown3NUD_ValueChanged(object sender, EventArgs e)
        {
            Background.Layers[CurLayer].ConstantSpeed = (byte)CVSPDNUD.Value;
        }
    }
}
