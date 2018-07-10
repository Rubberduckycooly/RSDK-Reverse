using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Tools.MapEditor
{
    public partial class NewObjectForm : Form
    {

        public ushort Type;
        public ushort Subtype;
        public ushort Xpos;
        public ushort Ypos;

        public NewObjectForm()
        {
            InitializeComponent();
        }

        private void TypeNUD_ValueChanged(object sender, EventArgs e)
        {
            Type = (ushort)TypeNUD.Value;
        }

        private void SubtypeNUD_ValueChanged(object sender, EventArgs e)
        {
            Subtype = (ushort)SubtypeNUD.Value;
        }

        private void XposNUD_ValueChanged(object sender, EventArgs e)
        {
            Xpos = (ushort)XposNUD.Value;
        }

        private void YposNUD_ValueChanged(object sender, EventArgs e)
        {
            Ypos = (ushort)YposNUD.Value;
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
    }
}
