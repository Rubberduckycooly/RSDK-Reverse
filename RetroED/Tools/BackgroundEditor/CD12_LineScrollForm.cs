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
    public partial class CD12_LineScrollForm : DockContent
    {

        public Retro_Formats.EngineType engineType;

        public bool RemoveVal = false;

        public int Pvalue;

        public Retro_Formats.Background Background = new Retro_Formats.Background();

        int hv = 0;

        public CD12_LineScrollForm(Retro_Formats.EngineType RSDKver, int PVal, int HV)
        {
            InitializeComponent();
            hv = HV;
            engineType = RSDKver;
            Pvalue = PVal;
        }

        public void Setup()
        {
            if (hv == 0)
            {
                BehaviourNUD.Value = Background.HLines[Pvalue].Behaviour;
                RSPDNUD.Value = Background.HLines[Pvalue].RelativeSpeed;
                CSPDNUD.Value = Background.HLines[Pvalue].ConstantSpeed;
                DrawLayerNUD.Value = Background.HLines[Pvalue].Behaviour;
            }
            if (hv == 1)
            {
                BehaviourNUD.Value = Background.VLines[Pvalue].Behaviour;
                RSPDNUD.Value = Background.VLines[Pvalue].RelativeSpeed;
                CSPDNUD.Value = Background.VLines[Pvalue].ConstantSpeed;
                DrawLayerNUD.Value = Background.VLines[Pvalue].DrawLayer;
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
                Background.HLines[Pvalue].Behaviour = (byte)BehaviourNUD.Value;
            }
            if (hv == 1)
            {
                Background.VLines[Pvalue].Behaviour = (byte)BehaviourNUD.Value;
            }
        }

        private void RSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                Background.HLines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
            }
            else if (hv == 1)
            {
                Background.VLines[Pvalue].RelativeSpeed = (byte)RSPDNUD.Value;
            }
        }

        private void CSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                Background.HLines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
            }
            else if (hv == 1)
            {
                Background.VLines[Pvalue].ConstantSpeed = (byte)CSPDNUD.Value;
            }
        }

        private void UnknownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (hv == 0)
            {
                Background.HLines[Pvalue].DrawLayer = (byte)DrawLayerNUD.Value;
            }
            else if (hv == 1)
            {
                Background.VLines[Pvalue].DrawLayer = (byte)DrawLayerNUD.Value;
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
