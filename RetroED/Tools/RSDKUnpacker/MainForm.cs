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

        RSDKvRS.DataFile DatavRS;
        RSDKv1.DataFile Datav1;
        RSDKv2.DataFile Datav2;
        RSDKvB.DataFile DatavB;
        RSDKv5.DataFile Datav5;

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectDataFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Retro-Sonic Data files|Data.bin|Sonic Nexus Data files|Data.bin|Sonic CD Data files|Data.rsdk|RSDKvB Data files|Data.rsdk|RSDKv5 Data files|Data.rsdk";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dataVer = dlg.FilterIndex - 1;
                filename = dlg.FileName;
                DataFileLocation.Text = filename;

                switch (dataVer)
                {
                    case 0:
                        DatavRS = new RSDKvRS.DataFile(filename);
                        BuildTreeRS();
                        SetFileListRS();
                        break;
                    case 1:
                        Datav1 = new RSDKv1.DataFile(filename);
                        BuildTreev1();
                        SetFileListv1();
                        break;
                    case 2:
                        Datav2 = new RSDKv2.DataFile(filename);
                        BuildTreev2();
                        SetFileListv2();
                        break;
                    case 3:
                        if (FileList == null || FileList.Count <= 0)
                        {
                            if (File.Exists("S1FileList.txt"))
                            {
                                StreamReader reader = new StreamReader(File.OpenRead("S1FileList.txt"));
                                while (!reader.EndOfStream)
                                {
                                    FileList.Add(reader.ReadLine());
                                }
                                reader.Close();
                            }
                            else if (File.Exists("S2FileList.txt"))
                            {
                                StreamReader reader = new StreamReader(File.OpenRead("S2FileList.txt"));
                                while (!reader.EndOfStream)
                                {
                                    FileList.Add(reader.ReadLine());
                                }
                                reader.Close();
                            }
                        }
                        if (FileList != null && FileList.Count > 0)
                        {
                            DatavB = new RSDKvB.DataFile(filename, FileList);
                            BuildTreevB();
                            SetFileListvB();
                        }
                        break;
                    case 4:
                        if (FileList == null || FileList.Count <= 0)
                        {
                            if (File.Exists("ManiaFileList.txt"))
                            {
                                StreamReader reader = new StreamReader(File.OpenRead("ManiaFileList.txt"));
                                while (!reader.EndOfStream)
                                {
                                    FileList.Add(reader.ReadLine());
                                }
                                reader.Close();
                            }
                        }
                        if (FileList != null && FileList.Count > 0)
                        {
                            Datav5 = new RSDKv5.DataFile(filename, FileList);
                            BuildTreev5();
                            SetFileListv5();
                        }
                        break;
                    default:
                        break;
                }
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
                        ExtractDataVRS(filename, dlg.SelectedPath);
                        break;
                    case 1:
                        ExtractDataV1(filename, dlg.SelectedPath);
                        break;
                    case 2:
                        ExtractDataV2(filename, dlg.SelectedPath);
                        break;
                    case 3:
                        ExtractDataVB(filename, dlg.SelectedPath);
                        break;
                    case 4:
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
            dlg.Filter = "Retro-Sonic Data files|Data.bin|Sonic Nexus Data files|Data.bin|Sonic CD Data files|Data.rsdk|RSDKvB Data files|Data.rsdk|RSDKv5 Data files|Data.rsdk";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        BuildDataVRS(filename, dlg.FileName);
                        break;
                    case 1:
                        BuildDataV1(filename, dlg.FileName);
                        break;
                    case 2:
                        BuildDataV2(filename, dlg.FileName);
                        break;
                    case 3:
                        BuildDataVB(filename, dlg.FileName);
                        break;
                    case 4:
                        BuildDataV5(filename, dlg.FileName);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ExtractDataVRS(string DataFile, string Extractionpath)
        {
            for (int i = 0; i < DatavRS.Files.Count; i++)
            {
                DatavRS.Files[i].Write(Extractionpath);
            }
        }

        public void ExtractDataV1(string DataFile, string Extractionpath)
        {
            for (int i = 0; i < Datav1.Files.Count; i++)
            {
                Datav1.Files[i].Write(Extractionpath);
            }
        }
        public void ExtractDataV2(string DataFile, string Extractionpath)
        {
            for (int i = 0; i < Datav2.Files.Count; i++)
            {
                Datav2.Files[i].Write(Extractionpath);
            }
        }
        public void ExtractDataVB(string DataFile, string Extractionpath)
        {
            for (int i = 0; i < DatavB.Files.Count; i++)
            {
                DatavB.Files[i].Write(Extractionpath);
            }
        }
        public void ExtractDataV5(string DataFile, string Extractionpath)
        {
            for (int i = 0; i < Datav5.Files.Count; i++)
            {
                Datav5.Files[i].Write(Extractionpath);
            }
        }

        public void BuildDataVRS(string DataFolder, string DataFilepath)
        {

        }

        public void BuildDataV1(string DataFolder, string DataFilepath)
        {

        }
        public void BuildDataV2(string DataFolder, string DataFilepath)
        {

        }
        public void BuildDataVB(string DataFolder, string DataFilepath)
        {

        }
        public void BuildDataV5(string DataFolder, string DataFilepath)
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

        private void DataView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void BuildTreeRS()
        {
            TreeNodeCollection addInMe = DataView.Nodes;
            TreeNode curNode = addInMe.Add("Data");
            DataView.Nodes.Clear();
            foreach (RSDKvRS.DataFile.DirInfo d in DatavRS.Directories)
            {
                if (d.Directory != "Data/")
                curNode.Nodes.Add(d.Directory.Remove(d.Directory.Length-1,1), d.Directory.Remove(d.Directory.Length - 1, 1));
            }
            foreach (RSDKvRS.DataFile.FileInfo subdir in DatavRS.Files)
            {
                //BuildTree(subdir.FileName, curNode.Nodes);
            }
        }

        private void BuildTreev1()
        {
            TreeNodeCollection addInMe = DataView.Nodes;
            TreeNode curNode = addInMe.Add("Data");
            DataView.Nodes.Clear();
            foreach (RSDKv1.DataFile.DirInfo d in Datav1.Directories)
            {
                if (d.Directory != "Data/")
                    curNode.Nodes.Add(d.Directory.Remove(d.Directory.Length - 1, 1), d.Directory.Remove(d.Directory.Length - 1, 1));
            }
            foreach (RSDKv1.DataFile.FileInfo subdir in Datav1.Files)
            {
                //BuildTree(subdir.FileName, curNode.Nodes);
            }
        }

        private void BuildTreev2()
        {
            TreeNodeCollection addInMe = DataView.Nodes;
            TreeNode curNode = addInMe.Add("Data");
            DataView.Nodes.Clear();
            foreach (RSDKv2.DataFile.DirInfo d in Datav2.Directories)
            {
                if (d.Directory != "Data/")
                    curNode.Nodes.Add(d.Directory.Remove(d.Directory.Length - 1, 1), d.Directory.Remove(d.Directory.Length - 1, 1));
            }
            foreach (RSDKv2.DataFile.FileInfo subdir in Datav2.Files)
            {
                //BuildTree(subdir.FileName, curNode.Nodes);
            }
        }

        private void BuildTreevB()
        {
            TreeNodeCollection addInMe = DataView.Nodes;
            TreeNode curNode = addInMe.Add("Data");
            DataView.Nodes.Clear();
            foreach (RSDKvB.DataFile.FileInfo subdir in DatavB.Files)
            {
                //BuildTree(subdir.FileName, curNode.Nodes);
            }
        }

        private void BuildTreev5()
        {
            TreeNodeCollection addInMe = DataView.Nodes;
            TreeNode curNode = addInMe.Add("Data");
            DataView.Nodes.Clear();
            foreach (RSDKv5.DataFile.FileInfo subdir in Datav5.Files)
            {
                //BuildTree(subdir.FileName, curNode.Nodes);
            }
        }

        void SetFileListRS()
        {
            FileListBox.Items.Clear();
            foreach (RSDKvRS.DataFile.FileInfo f in DatavRS.Files)
            {
                FileListBox.Items.Add(f.FullFileName);
            }
        }

        void SetFileListv1()
        {
            FileListBox.Items.Clear();
            foreach (RSDKv1.DataFile.FileInfo f in Datav1.Files)
            {
                FileListBox.Items.Add(f.FullFileName);
            }
        }

        void SetFileListv2()
        {
            FileListBox.Items.Clear();
            foreach (RSDKv2.DataFile.FileInfo f in Datav2.Files)
            {
                FileListBox.Items.Add(f.FullFileName);
            }
        }

        void SetFileListvB()
        {
            FileListBox.Items.Clear();
            foreach (RSDKvB.DataFile.FileInfo f in DatavB.Files)
            {
                FileListBox.Items.Add(f.FileName);
            }
        }

        void SetFileListv5()
        {
            FileListBox.Items.Clear();
            foreach (RSDKv5.DataFile.FileInfo f in Datav5.Files)
            {
                FileListBox.Items.Add(f.FileName);
            }
        }

        private void ExtractDataButton_Click(object sender, EventArgs e)
        {

        }

        private void SelectListButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select FileList";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(File.OpenRead(dlg.FileName));
                while (!reader.EndOfStream)
                {
                    FileList.Add(reader.ReadLine());
                }
                reader.Close();
            }
        }

        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            if (FileListBox.SelectedIndex >= 0)
            {
                FileOptionsForm frm = new FileOptionsForm();
                switch (dataVer)
                {
                    case 0:
                        frm.Setup(DatavRS.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            DatavRS.Files[FileListBox.SelectedIndex] = frm.FileDatavRS;
                            SetFileListRS();
                        }
                        break;
                    case 1:
                        frm.Setup(Datav1.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            Datav1.Files[FileListBox.SelectedIndex] = frm.FileDatav1;
                            SetFileListv1();
                        }
                        break;
                    case 2:
                        frm.Setup(Datav2.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            Datav2.Files[FileListBox.SelectedIndex] = frm.FileDatav2;
                            SetFileListv2();
                        }
                        break;
                    case 3:
                        frm.Setup(DatavB.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            DatavB.Files[FileListBox.SelectedIndex] = frm.FileDatavB;
                            SetFileListvB();
                        }
                        break;
                    case 4:
                        frm.Setup(Datav5.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            Datav5.Files[FileListBox.SelectedIndex] = frm.FileDatav5;
                            SetFileListv5();
                        }
                        break;
                }
            }
        }
    }
}
