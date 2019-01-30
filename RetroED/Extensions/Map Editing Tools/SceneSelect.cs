using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Extensions.DataSelect
{
    public partial class SceneSelect : Form
    {

        public struct returnData
        {
            public string FilePath;
            public int Category;
        }

        public List<Tuple<string, List<Tuple<string, string>>>> Categories = new List<Tuple<string, List<Tuple<string, string>>>>();
        public Dictionary<string, List<string>> Directories = new Dictionary<string, List<string>>();

        //Why is Retro-Sonic so bad lmao
        public RSDKvRS.ZoneList _RStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _CStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _SStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _BStagesvRS = new RSDKvRS.ZoneList();

        public Retro_Formats.Gameconfig _GameConfig;

        public returnData Result = new returnData();

        int RSDKver = 0;

        public SceneSelect(RSDKvRS.ZoneList RS, RSDKvRS.ZoneList CS, RSDKvRS.ZoneList SS, RSDKvRS.ZoneList BS)
        {
            InitializeComponent();
            LoadFromGameConfig(RS,CS,SS,BS);
            _RStagesvRS = RS;
            _CStagesvRS = CS;
            _SStagesvRS = SS;
            _BStagesvRS = BS;
        }

        public SceneSelect(Retro_Formats.Gameconfig config)
        {
            InitializeComponent();
            LoadFromGameConfig(config);
            _GameConfig = config;
        }

        public void LoadFromGameConfig(RSDKvRS.ZoneList RS, RSDKvRS.ZoneList CS, RSDKvRS.ZoneList SS, RSDKvRS.ZoneList BS)
        {
            Categories.Clear();
            Directories.Clear();
            for (int c = 0; c < 4; c++)
            {
                List<Tuple<string, string>> scenes = new List<Tuple<string, string>>();

                switch (c)
                {
                    case 0:
                        foreach (RSDKvRS.ZoneList.Level scene in RS.Stages)
                        {
                            scenes.Add(new Tuple<string, string>(scene.StageName, scene.StageFolder + "/Act" + scene.ActNo + ".map"));

                            List<string> files;
                            if (!Directories.TryGetValue(scene.StageFolder, out files))
                            {
                                files = new List<string>();
                                Directories[scene.StageFolder] = files;
                            }
                            files.Add("Act" + scene.ActNo + ".map");
                        }
                        break;
                    case 1:
                        foreach (RSDKvRS.ZoneList.Level scene in CS.Stages)
                        {
                            scenes.Add(new Tuple<string, string>(scene.StageName, scene.StageFolder + "/Act" + scene.ActNo + ".map"));

                            List<string> files;
                            if (!Directories.TryGetValue(scene.StageFolder, out files))
                            {
                                files = new List<string>();
                                Directories[scene.StageFolder] = files;
                            }
                            files.Add("Act" + scene.ActNo + ".map");
                        }
                        break;
                    case 2:
                        foreach (RSDKvRS.ZoneList.Level scene in SS.Stages)
                        {
                            scenes.Add(new Tuple<string, string>(scene.StageName, scene.StageFolder + "/Act" + scene.ActNo + ".map"));

                            List<string> files;
                            if (!Directories.TryGetValue(scene.StageFolder, out files))
                            {
                                files = new List<string>();
                                Directories[scene.StageFolder] = files;
                            }
                            files.Add("Act" + scene.ActNo + ".map");
                        }
                        break;
                    case 3:
                        foreach (RSDKvRS.ZoneList.Level scene in BS.Stages)
                        {
                            scenes.Add(new Tuple<string, string>(scene.StageName, scene.StageFolder + "/Act" + scene.ActNo + ".map"));

                            List<string> files;
                            if (!Directories.TryGetValue(scene.StageFolder, out files))
                            {
                                files = new List<string>();
                                Directories[scene.StageFolder] = files;
                            }
                            files.Add("Act" + scene.ActNo + ".map");
                        }
                        break;
                    default:
                        break;
                }

                string categoryName = "Unknown Category";

                switch (c)
                {
                    case 0:
                        categoryName = "Regular Stages";
                        break;
                    case 1:
                        categoryName = "Custom Stages";
                        break;
                    case 2:
                        categoryName = "Special Stages";
                        break;
                    case 3:
                        categoryName = "Bonus Stages";
                        break;
                    default:
                        categoryName = "Unknown Category";
                        break;
                }

                Categories.Add(new Tuple<string, List<Tuple<string, string>>>(categoryName, scenes));
            }

            // Sort
            Directories = Directories.OrderBy(key => key.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            foreach (KeyValuePair<string, List<String>> dir in Directories)
                dir.Value.Sort();

            this.scenesTree.ImageList = new ImageList();
            this.scenesTree.ImageList.Images.Add("Folder", RetroED.Properties.Resources.folder);
            this.scenesTree.ImageList.Images.Add("File", RetroED.Properties.Resources.file);

            UpdateTree();
            if (RetroED.Properties.Settings.Default.IsFilesViewDefault)
            {
                this.isFilesView.Checked = true;
            }
            else
            {
                this.isFilesView.Checked = false;
            }
        }

        public void LoadFromGameConfig(Retro_Formats.Gameconfig config)
        {
            Categories.Clear();
            Directories.Clear();
            for (int c = 0; c < config.Categories.Length; c++)
            {
                List<Tuple<string, string>> scenes = new List<Tuple<string, string>>();
                foreach (Retro_Formats.Gameconfig.Category.SceneInfo scene in config.Categories[c].Scenes)
                {
                    scenes.Add(new Tuple<string, string>(scene.Name, scene.SceneFolder + "/Act" + scene.ActID + ".bin"));

                    List<string> files;
                    if (!Directories.TryGetValue(scene.ActID, out files))
                    {
                        files = new List<string>();
                        Directories[scene.ActID] = files;
                    }
                    files.Add("Act" + scene.ActID + ".bin");
                }

                string categoryName = "Unknown Category";

                switch(c)
                {
                    case 0:
                        categoryName = "Presentation Stages";
                        break;
                    case 1:
                        categoryName = "Regular Stages";
                        break;
                    case 2:
                        categoryName = "Special Stages";
                        break;
                    case 3:
                        categoryName = "Bonus Stages";
                        break;
                    default:
                        categoryName = "Unknown Category";
                        break;
                }

                Categories.Add(new Tuple<string, List<Tuple<string, string>>>(categoryName, scenes));
            }

            // Sort
            Directories = Directories.OrderBy(key => key.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            foreach (KeyValuePair<string, List<String>> dir in Directories)
                dir.Value.Sort();

            this.scenesTree.ImageList = new ImageList();
            this.scenesTree.ImageList.Images.Add("Folder", RetroED.Properties.Resources.folder);
            this.scenesTree.ImageList.Images.Add("File", RetroED.Properties.Resources.file);

            UpdateTree();
            if (RetroED.Properties.Settings.Default.IsFilesViewDefault)
            {
                this.isFilesView.Checked = true;
            }
            else
            {
                this.isFilesView.Checked = false;
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            Result.FilePath = scenesTree.SelectedNode.Tag as string;
            Result.Category = scenesTree.SelectedNode.Parent.Index;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateTree()
        {
            Show(FilterText.Text);
        }

        private void Show(string filter)
        {
            scenesTree.Nodes.Clear();
            if (isFilesView.Checked)
            {
                foreach (KeyValuePair<string, List<string>> directory in Directories)
                {
                    TreeNode dir_node = new TreeNode(directory.Key);
                    dir_node.ImageKey = "Folder";
                    dir_node.SelectedImageKey = "Folder";
                    dir_node.ContextMenuStrip = contextMenuStrip1;
                    foreach (string file in directory.Value) {
                        TreeNode file_node = new TreeNode(file);
                        file_node.Tag = directory.Key + "/" + file;
                        file_node.ImageKey = "File";
                        file_node.ImageKey = "File";
                        file_node.SelectedImageKey = "File";
                        if (filter == "" || (directory.Key + "/" + file).ToLower().Contains(filter.ToLower()))
                            dir_node.Nodes.Add(file_node);
                    }
                    if (dir_node.Nodes.Count > 0)
                        scenesTree.Nodes.Add(dir_node);
                }
            }
            else
            {
                foreach (Tuple<string, List<Tuple<string, string>>> category in Categories)
                {
                    TreeNode dir_node = new TreeNode(category.Item1);
                    dir_node.ImageKey = "Folder";
                    dir_node.SelectedImageKey = "Folder";
                    string last = "";
                    foreach (Tuple<string, string> scene in category.Item2)
                    {
                        string scene_name = scene.Item1;
                        if (char.IsDigit(scene.Item1[0]))
                            scene_name = last + scene.Item1;

                        TreeNode file_node = new TreeNode(scene_name + " (" + scene.Item2 + ")");
                        file_node.Tag = scene.Item2;
                        file_node.ImageKey = "File";
                        file_node.SelectedImageKey = "File";
                        if (filter == "" || scene.Item2.ToLower().Contains(filter.ToLower()) || scene_name.ToLower().Contains(filter.ToLower()))
                            dir_node.Nodes.Add(file_node);

                        // Only the first act specify the full name, so lets save it
                        int i = scene_name.Length;
                        while (char.IsDigit(scene_name[i - 1]) || (i >= 2 && char.IsDigit(scene_name[i - 2])))
                            --i;
                        last = scene_name.Substring(0, i);
                    }
                    if (dir_node.Nodes.Count > 0)
                        scenesTree.Nodes.Add(dir_node);
                }
            }
            if (filter != "")
            {
                scenesTree.ExpandAll();
            }
        }

        private void isFilesView_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void FilterText_TextChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void scenesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectButton.Enabled = scenesTree.SelectedNode.Tag != null;
        }

        private void scenesTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (selectButton.Enabled)
            {
                selectButton_Click(sender, e);
            }
        }

        private void scenesTree_MouseUp(object sender, MouseEventArgs e)
        {
            if (scenesTree.SelectedNode == null)
            {
                selectButton.Enabled = false;
            }
        }

        private void scenesTree_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Scene File|*.bin";
            if (open.ShowDialog() != DialogResult.Cancel)
            {
                Result.FilePath = open.FileName;
                Close();
            }
        }

        private void scenesTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                scenesTree.SelectedNode = e.Node;
                if (e.Node.ImageKey == "Folder")
                    contextMenuStrip1.Show(scenesTree, e.Location);
                else if (e.Node.ImageKey == "File")
                    contextMenuStrip2.Show(scenesTree, e.Location);
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void SceneSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
