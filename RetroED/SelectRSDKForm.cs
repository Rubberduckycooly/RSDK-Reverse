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

namespace RetroED
{
    public partial class SelectRSDKForm : DockContent
    {
        public Retro_Formats.EngineType engineType = 0;

        public bool usingRSDKv5 = false;

        public SelectRSDKForm()
        {
            InitializeComponent();
        }

        private void RSDKVerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!usingRSDKv5)
            {
                switch (RSDKVerBox.SelectedIndex)
                {
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                }
            }
            else
            {
                switch (RSDKVerBox.SelectedIndex)
                {
                    case 4:
                        engineType = Retro_Formats.EngineType.RSDKv5;
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
