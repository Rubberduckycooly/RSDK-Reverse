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
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.NexusDecrypt
{
    public partial class MainForm : DockContent
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
                SourceFileLocation.Text = filename;
            }
        }

        private void Flip_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                FlipBytes(filename, dlg.FileName);
            }
        }

        void FlipBytes(string file,string filepath)
        {
            byte[] filebytes;
            int[] filebytes2;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(File.Open(file, FileMode.Open));

            filebytes = new byte[(int)reader.BaseStream.Length];

            filebytes = reader.ReadBytes((int)reader.BaseStream.Length); //Load every byte in the file

            reader.Close(); //Close the reader

            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(File.Open(filepath, FileMode.Create));

            filebytes2 = new int[filebytes.Length];

            for (int i = 0; i < filebytes.Length; i++)
            {
                filebytes2[i] = ~filebytes[i]; //"Mirror" the byte (e.g. 00 becomes FF)
            }

            for (int i = 0; i < filebytes2.Length; i++)
            {
                writer.Write((byte)filebytes2[i]); //Save The "mirrored" bytes
            }
            writer.Close(); //Close the writer
        }
    }
}
