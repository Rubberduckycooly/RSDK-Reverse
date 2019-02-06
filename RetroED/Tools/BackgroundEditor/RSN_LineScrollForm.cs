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
    public partial class RSN_LineScrollForm : DockContent
    {

        public Retro_Formats.EngineType engineType;

        public int curLine;

        public bool RemoveVal = false;

        public Retro_Formats.Background Background = new Retro_Formats.Background();

        int isVertical = 0;

        public RSN_LineScrollForm(Retro_Formats.EngineType RSDKver, int PVal, int isVertical)
        {
            InitializeComponent();
            isVertical = isVertical;
            engineType = RSDKver;
            curLine = PVal;
        }

        public void Setup()
        {
            if (isVertical == 0)
            {
                LineNoNUD.Value = Background.HLines[curLine].RelativeSpeed;
                SPDNUD.Value = Background.HLines[curLine].ConstantSpeed;
                DeformNUD.Value = Background.HLines[curLine].Behaviour;
            }
            else if (isVertical == 1)
            {
                LineNoNUD.Value = Background.HLines[curLine].RelativeSpeed;
                SPDNUD.Value = Background.HLines[curLine].ConstantSpeed;
                DeformNUD.Value = Background.HLines[curLine].Behaviour;
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
            if (isVertical == 0)
            {
                Background.HLines[curLine].ConstantSpeed = (byte)SPDNUD.Value;
            }
            else if (isVertical == 1)
            {
                Background.VLines[curLine].ConstantSpeed = (byte)SPDNUD.Value;
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
            if (isVertical == 0)
            {
                Background.HLines[curLine].Behaviour = (byte)DeformNUD.Value;
            }
            else if (isVertical == 1)
            {
                Background.VLines[curLine].Behaviour = (byte)DeformNUD.Value;
            }
        }

        private void RSPDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (isVertical == 0)
            {
                Background.HLines[curLine].RelativeSpeed = (byte)LineNoNUD.Value;
            }
            else if (isVertical == 1)
            {
                Background.VLines[curLine].RelativeSpeed = (byte)LineNoNUD.Value;
            }
        }
    }
}
