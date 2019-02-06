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
    public partial class CopyChunkForm : DockContent
    {

        public int SourceChunk;
        public int DestinationChunk;

        public CopyChunkForm()
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
                SourceChunk = (int)numericUpDown1.Value - 1;
                Console.WriteLine(SourceChunk);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
                DestinationChunk = (int)numericUpDown2.Value - 1;
                Console.WriteLine(DestinationChunk);
        }
    }
}
