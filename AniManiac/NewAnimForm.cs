using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AniManiac
{
    public partial class NewAnimForm : Form
    {

        public string AnimName = "New Animation";
        public short FrameCount = 1;
        public short FrameWidth = 16;
        public short FrameHeight = 16;

        public NewAnimForm()
        {
            InitializeComponent();
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

        private void AnimNameBox_TextChanged(object sender, EventArgs e)
        {
            AnimName = AnimNameBox.Text;
        }

        private void FrameCountNUD_ValueChanged(object sender, EventArgs e)
        {
            FrameCount = (short)FrameCountNUD.Value;
        }

        private void FrameWidthNUD_ValueChanged(object sender, EventArgs e)
        {
            FrameWidth = (short)FrameWidthNUD.Value;
        }

        private void FrameHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            FrameHeight = (short)FrameHeightNUD.Value;
        }
    }
}
