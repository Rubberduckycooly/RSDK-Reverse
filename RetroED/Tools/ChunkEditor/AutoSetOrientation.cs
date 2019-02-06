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

namespace RetroED.Tools.ChunkEditor
{
    public partial class AutoSetOrientation : DockContent
    {
        public byte Value;

        public AutoSetOrientation()
        {
            InitializeComponent();
        }

        private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueBox.SelectedIndex >= 0)
            {
                Value = (byte)ValueBox.SelectedIndex;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
