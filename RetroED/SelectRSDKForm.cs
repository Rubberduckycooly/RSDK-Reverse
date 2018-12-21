using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED
{
    public partial class SelectRSDKForm : Form
    {
        public int RSDKver = 0;

        public SelectRSDKForm()
        {
            InitializeComponent();
        }

        private void RSDKVerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RSDKVerBox.SelectedIndex >= 0) RSDKver = RSDKVerBox.SelectedIndex;
            else RSDKver = 0;
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
