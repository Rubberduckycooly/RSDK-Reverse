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
    public partial class SelectLayerForm : DockContent
    {

        public int SelLayer = 0;

        public SelectLayerForm()
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

        private void LayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndex >= 0)
            {
                SelLayer = LayerList.SelectedIndex;
                Console.WriteLine(SelLayer + " " + LayerList.SelectedIndex);
            }
        }
    }
}
