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

namespace RetroED.Tools.RSDKUnpacker
{
    public partial class MainForm : Form
    {
        string filename;
        int dataVer = 0;

        List<string> FileList = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectDataFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Retro-Sonic/Sonic Nexus Data Files|Data.bin|Sonic CD Data Files|Data.rsdk|Sonic 1 & 2 Data Files|Data.rsdk|Sonic Mania Data Files|Data.rsdk";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dataVer = dlg.FilterIndex - 1;
                filename = dlg.FileName;
                DataFileLocation.Text = filename;
            }
        }

        private void Extract_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (dataVer)
                {
                    case 0:
                        ExtractDataV1(filename, dlg.SelectedPath);
                        break;
                    case 1:
                        ExtractDataV2(filename, dlg.SelectedPath);
                        break;
                    case 2:
                        ExtractDataVB(filename, dlg.SelectedPath);
                        break;
                    case 3:
                        ExtractDataV5(filename, dlg.SelectedPath);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.SelectedPath;
                DataFolderLocation.Text = filename;
            }
        }

        private void BuildDataButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Retro-Sonic/Sonic Nexus Data Files|Data.bin|Sonic CD Data Files|Data.rsdk|Sonic 1 & 2 Data Files|Data.rsdk|Sonic Mania Data Files|Data.rsdk";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        BuildDataV1(filename, dlg.FileName);
                        break;
                    case 1:
                        BuildDataV2(filename, dlg.FileName);
                        break;
                    case 2:
                        BuildDataVB(filename, dlg.FileName);
                        break;
                    case 3:
                        BuildDataV5(filename, dlg.FileName);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ExtractDataV1(string file, string filepath)
        {

        }
        public void ExtractDataV2(string file, string filepath)
        {

        }
        public void ExtractDataVB(string file, string filepath)
        {

        }
        public void ExtractDataV5(string file, string filepath)
        {

        }

        public void BuildDataV1(string file, string filepath)
        {

        }
        public void BuildDataV2(string file, string filepath)
        {

        }
        public void BuildDataVB(string file, string filepath)
        {

        }
        public void BuildDataV5(string file, string filepath)
        {

        }

        private void SelectList_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Files|*.txt";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader reader = new StreamReader(File.Open(dlg.FileName, FileMode.Open));
                FileList.Clear();
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    FileList.Add(s);
                    Console.WriteLine(s);
                }
            }
        }
    }
}
