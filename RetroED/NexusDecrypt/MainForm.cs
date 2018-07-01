using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RetroED.Tools.NexusDecrypt
{
    public partial class MainForm : Form
    {
        string filename;

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
            }
        }

        private void Flip_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                FlipBits(filename ,dlg.FileName);
            }
        }

        void FlipBits(string file,string filepath)
        {
            byte[] filebytes;
            int[] filebytes2;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(File.Open(file, FileMode.Open));

            filebytes = new byte[(int)reader.BaseStream.Length];

            filebytes = reader.ReadBytes((int)reader.BaseStream.Length);

            reader.Close();

            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(File.Open(filepath, FileMode.Create));

            filebytes2 = new int[filebytes.Length];

            for (int i = 0; i < filebytes.Length; i++)
            {
                filebytes2[i] = ~filebytes[i];
            }

            for (int i = 0; i < filebytes2.Length; i++)
            {
                writer.Write((byte)filebytes2[i]);
            }
            writer.Close();
        }
    }
}
