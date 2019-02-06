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

namespace RetroED.Tools.RSDKUnpacker
{
    public partial class MainForm : DockContent
    {
        string filename;
        Retro_Formats.EngineType engineType;

        List<string> FileList = new List<string>();

        RSDKvRS.DataFile DatavRS;
        RSDKv1.DataFile Datav1;
        RSDKv2.DataFile Datav2;
        RSDKvB.DataFile DatavB;
        RSDKv5.DataFile Datav5;

        byte DirID = 0; //FOR DATA FILE BUILDING, DO NOT USE!!!

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectDataFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Retro-Sonic Data files|Data*.bin|Sonic Nexus Data files|Data*.bin|Sonic CD Data files|*.rsdk|RSDKvB Data files|*.rsdk|RSDKv5 Data files|*.rsdk";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch(dlg.FilterIndex-1)
                {
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                    case 4:
                        engineType = Retro_Formats.EngineType.RSDKv5;
                        break;
                }
                filename = dlg.FileName;
                DataFileLocation.Text = filename;

                switch (engineType)
                {
                    case Retro_Formats.EngineType.RSDKvRS:
                        DatavRS = new RSDKvRS.DataFile(filename);
                        SetFileListRS();
                        break;
                    case Retro_Formats.EngineType.RSDKv1:
                        Datav1 = new RSDKv1.DataFile(filename);
                        SetFileListv1();
                        break;
                    case Retro_Formats.EngineType.RSDKv2:
                        Datav2 = new RSDKv2.DataFile(filename);
                        SetFileListv2();
                        break;
                    case Retro_Formats.EngineType.RSDKvB:
                        if (FileList == null || FileList.Count <= 0)
                        {
                            if (File.Exists("RSDKvBFileList.txt"))
                            {
                                StreamReader reader = new StreamReader(File.OpenRead("RSDKvBFileList.txt"));
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
                            SetFileListvB();
                        }
                        break;
                    case Retro_Formats.EngineType.RSDKv5:
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
                switch (engineType)
                {
                    case Retro_Formats.EngineType.RSDKvRS:
                        ExtractDataVRS(filename, dlg.SelectedPath);
                        break;
                    case Retro_Formats.EngineType.RSDKv1:
                        ExtractDataV1(filename, dlg.SelectedPath);
                        break;
                    case Retro_Formats.EngineType.RSDKv2:
                        ExtractDataV2(filename, dlg.SelectedPath);
                        break;
                    case Retro_Formats.EngineType.RSDKvB:
                        ExtractDataVB(filename, dlg.SelectedPath);
                        break;
                    case Retro_Formats.EngineType.RSDKv5:
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

            SelectRSDKForm selectRSDKForm = new SelectRSDKForm();

            selectRSDKForm.RSDKVerBox.Items.Clear();
            selectRSDKForm.RSDKVerBox.Items.Add("RSDKvRS");
            selectRSDKForm.RSDKVerBox.Items.Add("RSDKv1");
            selectRSDKForm.RSDKVerBox.Items.Add("RSDKv2");
            selectRSDKForm.RSDKVerBox.Items.Add("RSDKvB");
            selectRSDKForm.RSDKVerBox.Items.Add("RSDKv5");

            if (selectRSDKForm.ShowDialog(this) == DialogResult.OK)
            {
                engineType = selectRSDKForm.engineType;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    filename = dlg.SelectedPath;
                    DataFolderLocation.Text = filename;

                    switch (engineType)
                    {
                        case Retro_Formats.EngineType.RSDKvRS:
                            BuildFromDataFolderVRS(dlg.SelectedPath);
                            break;
                        case Retro_Formats.EngineType.RSDKv1:
                            BuildFromDataFolderV1(dlg.SelectedPath);
                            break;
                        case Retro_Formats.EngineType.RSDKv2:
                            BuildFromDataFolderV2(dlg.SelectedPath);
                            break;
                        case Retro_Formats.EngineType.RSDKvB:
                            BuildFromDataFolderVB(dlg.SelectedPath);
                            break;
                        case Retro_Formats.EngineType.RSDKv5:
                            BuildFromDataFolderV5(dlg.SelectedPath);
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        private void BuildDataButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Retro-Sonic Data files|Data*.bin|Sonic Nexus Data files|Data*.bin|Sonic CD Data files|Data*.rsdk|RSDKvB Data files|Data*.rsdk|RSDKv5 Data files|Data*.rsdk";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (dlg.FilterIndex-1)
                {
                    case 0:
                        BuildDataVRS(dlg.FileName);
                        break;
                    case 1:
                        BuildDataV1(dlg.FileName);
                        break;
                    case 2:
                        BuildDataV2(dlg.FileName);
                        break;
                    case 3:
                        BuildDataVB(dlg.FileName);
                        break;
                    case 4:
                        BuildDataV5(dlg.FileName);
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

        public void BuildDataVRS(string DataFilepath)
        {
            DatavRS.Write(new RSDKvRS.Writer(DataFilepath));
        }

        public void BuildDataV1(string DataFilepath)
        {
            Datav1.Write(new RSDKv1.Writer(DataFilepath));
        }
        public void BuildDataV2(string DataFilepath)
        {
            Datav2.Write(new RSDKv2.Writer(DataFilepath));
        }
        public void BuildDataVB(string DataFilepath)
        {
            DatavB.Write(new RSDKvB.Writer(DataFilepath));
        }
        public void BuildDataV5(string DataFilepath)
        {
            Datav5.Write(new RSDKv5.Writer(DataFilepath));
        }

        public void BuildFromDataFolderVRS(string folderPath)
        {
            DatavRS = new RSDKvRS.DataFile();
            DirID = 0;
            BuildDataFromFoldersRS(new DirectoryInfo(folderPath));
            SetFileListRS();
            RefreshUI();
        }

        private void BuildDataFromFoldersRS(DirectoryInfo directoryInfo)
        {
            string dir = directoryInfo.FullName.Replace(help.GetUntilOrEmpty(directoryInfo.FullName, "Data"), "");

            RSDKvRS.DataFile.DirInfo dirinfo = new RSDKvRS.DataFile.DirInfo();

            dir.Replace("\\", "/");

            dirinfo.Directory = dir + "/";

            if (directoryInfo.GetFiles().Length > 0)
            {
                DatavRS.Directories.Add(dirinfo);
                DirID++;
            }

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                RSDKvRS.DataFile.FileInfo File = new RSDKvRS.DataFile.FileInfo();

                File.DirID = (byte)(DirID-1);
                File.FullFileName = file.FullName.Replace(help.GetUntilOrEmpty(file.FullName, "Data"),"");

                RSDKvRS.Reader reader = new RSDKvRS.Reader(file.FullName);

                File.Filedata = reader.ReadBytes(reader.BaseStream.Length);
                File.fileSize = (ulong)reader.BaseStream.Length;

                reader.Close();

                File.FileName = Path.GetFileName(file.FullName);

                DatavRS.Files.Add(File);
            }

            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildDataFromFoldersRS(subdir);
            }
        }

        public void BuildFromDataFolderV1(string folderPath)
        {
            Datav1 = new RSDKv1.DataFile();
            DirID = 0;
            BuildDataFromFoldersV1(new DirectoryInfo(folderPath));
            SetFileListv1();
            RefreshUI();
        }

        private void BuildDataFromFoldersV1(DirectoryInfo directoryInfo)
        {
            string dir = directoryInfo.FullName.Replace(help.GetUntilOrEmpty(directoryInfo.FullName, "Data"), "");

            RSDKv1.DataFile.DirInfo dirinfo = new RSDKv1.DataFile.DirInfo();

            dir.Replace("\\", "/");

            dirinfo.Directory = dir + "/";

            if (directoryInfo.GetFiles().Length > 0)
            {
                Datav1.Directories.Add(dirinfo);
                DirID++;
            }

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                RSDKv1.DataFile.FileInfo File = new RSDKv1.DataFile.FileInfo();

                File.DirID = (byte)(DirID - 1);
                File.FullFileName = file.FullName.Replace(help.GetUntilOrEmpty(file.FullName, "Data"), "");

                RSDKv1.Reader reader = new RSDKv1.Reader(file.FullName);

                File.Filedata = reader.ReadBytes(reader.BaseStream.Length);
                File.fileSize = (uint)reader.BaseStream.Length;

                reader.Close();

                File.FileName = Path.GetFileName(file.FullName);

                Datav1.Files.Add(File);
            }

            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildDataFromFoldersV2(subdir);
            }
        }

        public void BuildFromDataFolderV2(string folderPath)
        {
            Datav2 = new RSDKv2.DataFile();
            DirID = 0;
            BuildDataFromFoldersV2(new DirectoryInfo(folderPath));
            SetFileListv2();
            RefreshUI();
        }

        private void BuildDataFromFoldersV2(DirectoryInfo directoryInfo)
        {
            string dir = directoryInfo.FullName.Replace(help.GetUntilOrEmpty(directoryInfo.FullName, "Data"), "");

            RSDKv2.DataFile.DirInfo dirinfo = new RSDKv2.DataFile.DirInfo();

            dir.Replace("\\", "/");

            dirinfo.Directory = dir + "\\";

            if (directoryInfo.GetFiles().Length > 0)
            {
                Datav2.Directories.Add(dirinfo);
                DirID++;
            }

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                RSDKv2.DataFile.FileInfo File = new RSDKv2.DataFile.FileInfo();

                File.DirID = (ushort)(DirID - 1);
                File.FullFileName = file.FullName.Replace(help.GetUntilOrEmpty(file.FullName, "Data"), "");

                RSDKv2.Reader reader = new RSDKv2.Reader(file.FullName);

                File.Filedata = reader.ReadBytes(reader.BaseStream.Length);
                File.fileSize = (uint)reader.BaseStream.Length;

                reader.Close();

                File.FileName = Path.GetFileName(file.FullName);

                Datav2.Files.Add(File);
            }

            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildDataFromFoldersV2(subdir);
            }
        }

        public void BuildFromDataFolderVB(string folderPath)
        {
            DatavB = new RSDKvB.DataFile();
            DirID = 0;
            BuildDataFromFoldersVB(new DirectoryInfo(folderPath));
            SetFileListvB();
            RefreshUI();
        }

        private void BuildDataFromFoldersVB(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                RSDKvB.DataFile.FileInfo File = new RSDKvB.DataFile.FileInfo();

                File.FileName = file.FullName.Replace(help.GetUntilOrEmpty(file.FullName, "Data"), "");

                RSDKvB.Reader reader = new RSDKvB.Reader(file.FullName);

                File.Filedata = reader.ReadBytes(reader.BaseStream.Length);
                File.FileSize = (uint)reader.BaseStream.Length;

                reader.Close();

                DatavB.Files.Add(File);
            }

            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildDataFromFoldersVB(subdir);
            }
        }

        public void BuildFromDataFolderV5(string folderPath)
        {
            Datav5 = new RSDKv5.DataFile();
            DirID = 0;
            BuildDataFromFoldersV5(new DirectoryInfo(folderPath));
            SetFileListv5();
            RefreshUI();
        }

        private void BuildDataFromFoldersV5(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                RSDKv5.DataFile.FileInfo File = new RSDKv5.DataFile.FileInfo();

                File.FileName = file.FullName.Replace(help.GetUntilOrEmpty(file.FullName, "Data"), "");

                RSDKv5.Reader reader = new RSDKv5.Reader(file.FullName);

                File.Filedata = reader.ReadBytes(reader.BaseStream.Length);
                File.FileSize = (uint)reader.BaseStream.Length;

                reader.Close();

                Datav5.Files.Add(File);
            }

            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildDataFromFoldersVB(subdir);
            }
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

        void SetFileListRS()
        {
            DirectoryListBox.Items.Clear();
            foreach (RSDKvRS.DataFile.DirInfo d in DatavRS.Directories)
            {
                DirectoryListBox.Items.Add(d.Directory);
                DirectoryList.Items.Add(d.Directory);
            }
            FileListBox.Items.Clear();
            foreach (RSDKvRS.DataFile.FileInfo f in DatavRS.Files)
            {
                FileListBox.Items.Add(DatavRS.Directories[f.DirID].Directory + "\\" + f.FileName);
            }
        }

        void SetFileListv1()
        {
            FileListBox.Items.Clear();
            foreach (RSDKv1.DataFile.FileInfo f in Datav1.Files)
            {
                FileListBox.Items.Add(f.FullFileName);
            }
            DirectoryListBox.Items.Clear();
            foreach (RSDKv1.DataFile.DirInfo d in Datav1.Directories)
            {
                DirectoryListBox.Items.Add(d.Directory);
            }
        }

        void SetFileListv2()
        {
            FileListBox.Items.Clear();
            foreach (RSDKv2.DataFile.FileInfo f in Datav2.Files)
            {
                FileListBox.Items.Add(f.FullFileName);
            }
            DirectoryListBox.Items.Clear();
            foreach (RSDKv2.DataFile.DirInfo d in Datav2.Directories)
            {
                DirectoryListBox.Items.Add(d.Directory);
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
                switch (engineType)
                {
                    case Retro_Formats.EngineType.RSDKvRS:
                        frm.Setup(DatavRS.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            DatavRS.Files[FileListBox.SelectedIndex] = frm.FileDatavRS;
                            SetFileListRS();
                        }
                        break;
                    case Retro_Formats.EngineType.RSDKv1:
                        frm.Setup(Datav1.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            Datav1.Files[FileListBox.SelectedIndex] = frm.FileDatav1;
                            SetFileListv1();
                        }
                        break;
                    case Retro_Formats.EngineType.RSDKv2:
                        frm.Setup(Datav2.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            Datav2.Files[FileListBox.SelectedIndex] = frm.FileDatav2;
                            SetFileListv2();
                        }
                        break;
                    case Retro_Formats.EngineType.RSDKvB:
                        frm.Setup(DatavB.Files[FileListBox.SelectedIndex]);

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            DatavB.Files[FileListBox.SelectedIndex] = frm.FileDatavB;
                            SetFileListvB();
                        }
                        break;
                    case Retro_Formats.EngineType.RSDKv5:
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

        private void EncryptedCB_CheckedChanged(object sender, EventArgs e)
        {
            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS:
                    EncryptedCB.Checked = false;
                    break;
                case Retro_Formats.EngineType.RSDKv1:
                    EncryptedCB.Checked = Datav1.Files[FileListBox.SelectedIndex].encrypted;
                    break;
                case Retro_Formats.EngineType.RSDKv2:
                    //EncryptedCB.Checked = Datav2.Files[FileListBox.SelectedIndex].encrypted;
                    EncryptedCB.Checked = true;
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    EncryptedCB.Checked = DatavB.Files[FileListBox.SelectedIndex].Encrypted;
                    break;
                case Retro_Formats.EngineType.RSDKv5:
                    EncryptedCB.Checked = Datav5.Files[FileListBox.SelectedIndex].Encrypted;
                    break;
            }
        }

        void RefreshUI()
        {
            switch(engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS: //RSonic '07 (RSDKvRS)
                    if (FileListBox.SelectedIndex >= 0)
                    {
                        FileSizeLabel.Text = "File Size = " + DatavRS.Files[FileListBox.SelectedIndex].fileSize + " Bytes";
                        FileNameLabel.Text = "File Name = " + DatavRS.Files[FileListBox.SelectedIndex].FileName;
                        FullFileNameLabel.Text = "Full File Name = " + DatavRS.Files[FileListBox.SelectedIndex].FullFileName;
                        FileOffsetLabel.Text = "RSDKvRS doesn't use file offsets!";
                        EncryptedCB.Checked = false;
                        DirectoryList.SelectedIndex = DatavRS.Files[FileListBox.SelectedIndex].DirID;
                    }
                    break;
                case Retro_Formats.EngineType.RSDKv1: //Sonic Nexus (RSDKv1)
                    if (FileListBox.SelectedIndex >= 0)
                    {
                        FileSizeLabel.Text = "File Size = " + Datav1.Files[FileListBox.SelectedIndex].fileSize + " Bytes";
                        FileNameLabel.Text = "File Name = " + Datav1.Files[FileListBox.SelectedIndex].FileName;
                        FullFileNameLabel.Text = "Full File Name = " + Datav1.Files[FileListBox.SelectedIndex].FullFileName;
                        FileOffsetLabel.Text = "RSDKv1 doesn't use file offsets!";
                        EncryptedCB.Checked = Datav1.Files[FileListBox.SelectedIndex].encrypted;
                    }
                    break;
                case Retro_Formats.EngineType.RSDKv2: //Sonic CD (RSDKv2)
                    if (FileListBox.SelectedIndex >= 0)
                    {
                        FileSizeLabel.Text = "File Size = " + Datav2.Files[FileListBox.SelectedIndex].fileSize + " Bytes";
                        FileNameLabel.Text = "File Name = " + Datav2.Files[FileListBox.SelectedIndex].FileName;
                        FullFileNameLabel.Text = "Full File Name = " + Datav2.Files[FileListBox.SelectedIndex].FullFileName;
                        FileOffsetLabel.Text = "RSDKv2 doesn't use file offsets!";
                        //EncryptedCB.Checked = Datav2.Files[FileListBox.SelectedIndex].encrypted;
                        EncryptedCB.Checked = true;
                    }
                    break;
                case Retro_Formats.EngineType.RSDKvB: //Sonic 1 & 2 (RSDKvB)
                    if (FileListBox.SelectedIndex >= 0)
                    {
                        FileSizeLabel.Text = "File Size = " + DatavB.Files[FileListBox.SelectedIndex].FileSize + " Bytes";
                        FileNameLabel.Text = "File Name = " + Path.GetFileName(DatavB.Files[FileListBox.SelectedIndex].FileName);
                        FullFileNameLabel.Text = "Full File Name = " + DatavB.Files[FileListBox.SelectedIndex].FileName;
                        FileOffsetLabel.Text = "File Offset = " + DatavB.Files[FileListBox.SelectedIndex].DataOffset + " Bytes";
                        EncryptedCB.Checked = DatavB.Files[FileListBox.SelectedIndex].Encrypted;
                    }
                    break;
                case Retro_Formats.EngineType.RSDKv5: //Sonic Mania (RSDKv5)
                    if (FileListBox.SelectedIndex >= 0)
                    {
                        FileSizeLabel.Text = "File Size = " + Datav5.Files[FileListBox.SelectedIndex].FileSize + " Bytes";
                        FileNameLabel.Text = "File Name = " + Path.GetFileName(Datav5.Files[FileListBox.SelectedIndex].FileName);
                        FullFileNameLabel.Text = "Full File Name = " + Datav5.Files[FileListBox.SelectedIndex].FileName;
                        FileOffsetLabel.Text = "File Offset = " + Datav5.Files[FileListBox.SelectedIndex].DataOffset + " Bytes";
                        EncryptedCB.Checked = Datav5.Files[FileListBox.SelectedIndex].Encrypted;
                    }
                    break;
            }
        }

        private void FileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void DirectoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DirectoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS: //RSonic '07 (RSDKvRS)
                    DatavRS.Files[FileListBox.SelectedIndex].DirID = (byte)DirectoryList.SelectedIndex;
                    break;
                case Retro_Formats.EngineType.RSDKv1: //Sonic Nexus (RSDKv1)
                    Datav1.Files[FileListBox.SelectedIndex].DirID = (byte)DirectoryList.SelectedIndex;
                    break;
                case Retro_Formats.EngineType.RSDKv2: //Sonic CD (RSDKv2)
                    Datav2.Files[FileListBox.SelectedIndex].DirID = (byte)DirectoryList.SelectedIndex;
                    break;
            }
        }
    }

    static class help
    {
        public static string GetUntilOrEmpty(string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return text;
        }
    }

}
