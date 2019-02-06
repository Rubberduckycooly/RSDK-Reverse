using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.RSDKUnpacker
{
    public partial class FileOptionsForm : DockContent
    {

        public RSDKvRS.DataFile.FileInfo FileDatavRS;
        public RSDKv1.DataFile.FileInfo FileDatav1;
        public RSDKv2.DataFile.FileInfo FileDatav2;
        public RSDKvB.DataFile.FileInfo FileDatavB;
        public RSDKv5.DataFile.FileInfo FileDatav5;
        int Dataver = 0;

        public FileOptionsForm()
        {
            InitializeComponent();
        }

        public void Setup(RSDKvRS.DataFile.FileInfo file)
        {
            Dataver = 0;
            FileDatavRS = file;
            FileNameBox.Text = FileDatavRS.FileName;
        }

        public void Setup(RSDKv1.DataFile.FileInfo file)
        {
            Dataver = 1;
            FileDatav1 = file;
            FileNameBox.Text = FileDatav1.FileName;
        }

        public void Setup(RSDKv2.DataFile.FileInfo file)
        {
            Dataver = 2;
            FileDatav2 = file;
            FileNameBox.Text = FileDatav2.FileName;
        }

        public void Setup(RSDKvB.DataFile.FileInfo file)
        {
            Dataver = 3;
            FileDatavB = file;
            FileNameBox.Text = FileDatavB.FileName;
        }

        public void Setup(RSDKv5.DataFile.FileInfo file)
        {
            Dataver = 4;
            FileDatav5 = file;
            FileNameBox.Text = FileDatav5.FileName;
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            switch (Dataver)
            {
                case 0:
                    dlg.FileName = FileDatavRS.FileName;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        FileDatavRS.Write(dlg.FileName.Replace(Path.GetFileName(dlg.FileName), ""));
                    }
                    break;
                case 1:
                    dlg.FileName = FileDatav1.FileName;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        FileDatav1.Write(dlg.FileName.Replace(Path.GetFileName(dlg.FileName), ""));
                    }
                    break;
                case 2:
                    dlg.FileName = FileDatav2.FileName;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        FileDatav2.Write(dlg.FileName.Replace(Path.GetFileName(dlg.FileName), ""));
                    }
                    break;
                case 3:
                    dlg.FileName = FileDatavB.FileName;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        FileDatavB.Write(dlg.FileName.Replace(Path.GetFileName(dlg.FileName), ""));
                    }
                    break;
                case 4:
                    dlg.FileName = FileDatav5.FileName;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        FileDatav5.Write(dlg.FileName.Replace(Path.GetFileName(dlg.FileName), ""));
                    }
                    break;
            }
        }

        private void ReplaceFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(dlg.FileName));
                switch (Dataver)
                {
                    case 0:
                        FileDatavRS.FileName = Path.GetFileName(dlg.FileName);
                        FileDatavRS.fileSize = (ulong)reader.BaseStream.Length;
                        FileDatavRS.Filedata = reader.ReadBytes((int)reader.BaseStream.Length);
                        FileNameBox.Text = Path.GetFileName(dlg.FileName);
                        break;
                    case 1:
                        FileDatav1.FileName = Path.GetFileName(dlg.FileName);
                        FileDatav1.fileSize = (uint)reader.BaseStream.Length;
                        FileDatav1.Filedata = reader.ReadBytes((int)reader.BaseStream.Length);
                        FileNameBox.Text = Path.GetFileName(dlg.FileName);
                        break;
                    case 2:
                        FileDatav2.FileName = Path.GetFileName(dlg.FileName);
                        FileDatav2.fileSize = (uint)reader.BaseStream.Length;
                        FileDatav2.Filedata = reader.ReadBytes((int)reader.BaseStream.Length);
                        FileNameBox.Text = Path.GetFileName(dlg.FileName);
                        break;
                    case 3:
                        FileDatavB.FileName = Path.GetFileName(dlg.FileName);
                        FileDatavB.FileSize = (uint)reader.BaseStream.Length;
                        FileDatavB.Filedata = reader.ReadBytes((int)reader.BaseStream.Length);
                        FileNameBox.Text = Path.GetFileName(dlg.FileName);
                        break;
                    case 4:
                        FileDatav5.FileName = Path.GetFileName(dlg.FileName);
                        FileDatav5.FileSize = (uint)reader.BaseStream.Length;
                        FileDatav5.Filedata = reader.ReadBytes((int)reader.BaseStream.Length);
                        FileNameBox.Text = Path.GetFileName(dlg.FileName);
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FileNameBox_TextChanged(object sender, EventArgs e)
        {
            switch(Dataver)
            {
                case 0:
                    FileDatavRS.FileName = FileNameBox.Text;
                    break;
                case 1:
                    FileDatav1.FileName = FileNameBox.Text;
                    break;
                case 2:
                    FileDatav2.FileName = FileNameBox.Text;
                    break;
                case 3:
                    FileDatavB.FileName = FileNameBox.Text;
                    break;
                case 4:
                    FileDatav5.FileName = FileNameBox.Text;
                    break;
            }
        }
    }
}
