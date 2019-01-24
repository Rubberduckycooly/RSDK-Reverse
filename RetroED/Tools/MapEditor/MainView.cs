using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RetroED.Tools.MapEditor
{
    public partial class MainView : Form
    {
        enum Placementmode //Placement Modes
        {
            NONE,
            PlaceTiles,
            PlaceObjects
        }

        //What RSDK version is loaded?
        public int LoadedRSDKver = 0;

        bool LoadedObjDefinitions = false;

        //Stage's Tileset
        private Image _loadedTiles;

        //The Chunk/Object Viewer
        public StageChunksView _blocksViewer;

        //The Map Viewer
        public StageMapView _mapViewer;

        public RetroED.MainForm Parent;

        //Stack<UndoAction> UndoList;
        //Stack<UndoAction> RedoList;

        public int categoryID = 0;

        //The Path to these files
        string tiles;
        string mappings;
        string Map;
        string CollisionMasks;
        string Stageconfig;

        bool showgrid = false;
        int PlacementMode = 0;

        public string FilePath;

        public string DataDirectory = null;
        public bool DataDirLoaded = false;

        new RetroED.Extensions.EntityToolbar.EntityToolbar EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar();

        #region Retro-Sonic Development Kit
        //Why is Retro-Sonic so bad lmao
        public RSDKvRS.ZoneList _RStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _CStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _SStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _BStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.Scene _RSDKRSScene = new RSDKvRS.Scene();
        public RSDKvRS.Tiles128x128 _RSDKRSChunks = new RSDKvRS.Tiles128x128();
        public RSDKvRS.Tileconfig _RSDKRSCollisionMask = new RSDKvRS.Tileconfig();
        public RSDKvRS.Zoneconfig _RSDKRSStageconfig = new RSDKvRS.Zoneconfig();
        #endregion

        #region RSDKv1
        public RSDKv1.GameConfig _RSDK1GameConfig;
        public RSDKv1.Scene _RSDK1Scene = new RSDKv1.Scene();
        public RSDKv1.Tiles128x128 _RSDK1Chunks = new RSDKv1.Tiles128x128();
        public RSDKv1.Tileconfig _RSDK1CollisionMask = new RSDKv1.Tileconfig();
        public RSDKv1.StageConfig _RSDK1Stageconfig = new RSDKv1.StageConfig();
        #endregion

        #region RSDKv2
        public RSDKv2.GameConfig _RSDK2GameConfig = new RSDKv2.GameConfig();
        public RSDKv2.Scene _RSDK2Scene = new RSDKv2.Scene();
        public RSDKv2.Tiles128x128 _RSDK2Chunks = new RSDKv2.Tiles128x128();
        public RSDKv2.Tileconfig _RSDK2CollisionMask = new RSDKv2.Tileconfig();
        public RSDKv2.StageConfig _RSDK2Stageconfig = new RSDKv2.StageConfig();
        #endregion

        #region RSDKvB
        public RSDKvB.GameConfig _RSDKBGameConfig = new RSDKvB.GameConfig();
        public RSDKvB.Scene _RSDKBScene = new RSDKvB.Scene();
        public RSDKvB.Tiles128x128 _RSDKBChunks = new RSDKvB.Tiles128x128();
        public RSDKvB.Tileconfig _RSDKBCollisionMask = new RSDKvB.Tileconfig();
        public RSDKvB.StageConfig _RSDKBStageconfig = new RSDKvB.StageConfig();
        #endregion

        List<string> ObjectNames = new List<string>();
        List<RSDKvRS.Object> ObjectTypesvRS = new List<RSDKvRS.Object>();
        List<RSDKv1.Object> ObjectTypesv1 = new List<RSDKv1.Object>();
        List<RSDKv2.Object> ObjectTypesv2 = new List<RSDKv2.Object>();
        List<RSDKvB.Object> ObjectTypesvB = new List<RSDKvB.Object>();

        public MainView()
        {
            InitializeComponent();
            _mapViewer = new StageMapView(this);
            _mapViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            _blocksViewer = new StageChunksView(_mapViewer,this);
            _blocksViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            _mapViewer._ChunkView = _blocksViewer;
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar();
            EntityToolbar.Dock = DockStyle.Fill;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
        }

        void LoadScene(string Scene, int RSDKver)
        {
            //Clears the map
            _mapViewer.DrawScene();
            switch (RSDKver)
            {
                case 0:
                    _RSDKBScene = new RSDKvB.Scene(Scene);
                    _RSDKBChunks = new RSDKvB.Tiles128x128(mappings);
                    _RSDKBCollisionMask = new RSDKvB.Tileconfig(CollisionMasks);
                    _RSDKBStageconfig = new RSDKvB.StageConfig(Stageconfig);

                    using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        _loadedTiles = (Bitmap)bmp.Clone();
                    }
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetScene();
                    break;
                case 1:
                    _RSDK2Scene = new RSDKv2.Scene(Scene);
                    _RSDK2Chunks = new RSDKv2.Tiles128x128(mappings);
                    _RSDK2CollisionMask = new RSDKv2.Tileconfig(CollisionMasks);
                    _RSDK2Stageconfig = new RSDKv2.StageConfig(Stageconfig);

                    using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        _loadedTiles = (Bitmap)bmp.Clone();
                    }
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetScene();
                    break;
                case 2:
                    _RSDK1Scene = new RSDKv1.Scene(Scene);
                    _RSDK1Chunks = new RSDKv1.Tiles128x128(mappings);
                    _RSDK1CollisionMask = new RSDKv1.Tileconfig(CollisionMasks);
                    _RSDK1Stageconfig = new RSDKv1.StageConfig(Stageconfig);

                    using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        _loadedTiles = (Bitmap)bmp.Clone();
                    }
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetScene();
                    break;
                case 3:
                    _RSDKRSScene = new RSDKvRS.Scene(Scene);
                    _RSDKRSChunks = new RSDKvRS.Tiles128x128(mappings);
                    _RSDKRSCollisionMask = new RSDKvRS.Tileconfig(CollisionMasks);
                    _RSDKRSStageconfig = new RSDKvRS.Zoneconfig(Stageconfig);

                    RSDKvRS.gfx gfx = new RSDKvRS.gfx(tiles, false);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage.Clone(new Rectangle(0,0,gfx.gfxImage.Width, gfx.gfxImage.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    _blocksViewer.SetChunks();

                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer._tiles = gfx.gfxImage.Clone(new Rectangle(0, 0, gfx.gfxImage.Width, gfx.gfxImage.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    _mapViewer.SetScene();
                    gfx = null;
                    break;
                default:
                    break;
            }
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PlaceTileButton_Click(object sender, EventArgs e)
        {
            if (PlacementMode != 1) //Change what Placement mode is activated
            {
                PlacementMode = (int)Placementmode.PlaceTiles;
                PlaceTileButton.Checked = true;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceTileButton.BackColor = SystemColors.ControlDarkDark;
            }
            else if (PlacementMode == 1) //Change what Placement mode is activated
            {
                PlacementMode = (int)Placementmode.NONE;
                PlaceTileButton.Checked = false;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceTileButton.BackColor = SystemColors.Control;
            }
        }

        void CheckDimensions(int RSDKver, ushort[][] OldTiles, ushort[][] NewTiles, int OLDwidth, int OLDheight) //Check to see if the map is a different size to another map/an older version of this map
        {

            if (RSDKver == 3)
            {
                if (_RSDKRSScene.width != OLDwidth || _RSDKRSScene.height != OLDheight) //Well, Is it different?
                {
                    Console.WriteLine("Different"); //It is!
                    //Update the map!
                    _RSDKRSScene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDKRSScene.width, (ushort)_RSDKRSScene.height, RSDKver);
                    _mapViewer.DrawScene(); //Draw the map
                }
            }
            if (RSDKver == 2)
            {
                if (_RSDK1Scene.width != OLDwidth || _RSDK1Scene.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK1Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK1Scene.width, (ushort)_RSDK1Scene.height, RSDKver);
                    _mapViewer.DrawScene();
                }

            }
            if (RSDKver == 1)
            {
                if (_RSDK2Scene.width != OLDwidth || _RSDK2Scene.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK2Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK2Scene.width, (ushort)_RSDK2Scene.height, RSDKver);
                    _mapViewer.DrawScene();
                }
            }
            if (RSDKver == 0)
            {
                if (_RSDKBScene.width != OLDwidth || _RSDKBScene.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDKBScene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDKBScene.width, (ushort)_RSDKBScene.height, RSDKver);
                    _mapViewer.DrawScene();
                }
            }
        }

        ushort[][] UpdateMapDimensions(ushort[][] OldTiles, ushort[][] NewTiles, ushort oldWidth, ushort oldHeight, ushort NewWidth, ushort NewHeight, int RSDKver)
        {
            //Yeah, I "borrowed" this from Maniac

            // fill the extended tile arrays with "empty" values
            Array.Resize(ref NewTiles, NewHeight);

            // if we're actaully getting shorter, do nothing!
            for (ushort i = oldHeight; i < NewHeight; i++)
            {
                // first create arrays child arrays to the old width
                // a little inefficient, but at least they'll all be equal sized
                NewTiles[i] = new ushort[oldWidth];
                for (int j = 0; j < oldWidth; ++j)
                    NewTiles[i][j] = 0; // fill the new ones with blanks
            }

            for (ushort i = 0; i < NewHeight; i++)
            {
                // now resize all child arrays to the new width
                Array.Resize(ref NewTiles[i], NewWidth);
                for (ushort j = oldWidth; j < NewWidth; j++)
                    NewTiles[i][j] = 0; // and fill with blanks if wider
            }
            _mapViewer.ResizeScrollBars();
            return NewTiles;
        }


        private void clearObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(LoadedRSDKver)
            {
                case 3:
                    _RSDKRSScene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
                case 2:
                    _RSDK1Scene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
                case 1:
                    _RSDK2Scene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
                case 0:
                    _RSDKBScene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
            }
        }

        /// <summary>
        /// Sets the GameConfig property in relation to the DataDirectory property.
        /// </summary>
        private void SetGameConfig()
        {
            switch (LoadedRSDKver)
            {
                case 0:
                    _RSDKBGameConfig = new RSDKvB.GameConfig(Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
                    break;
                case 1:
                    _RSDK2GameConfig = new RSDKv2.GameConfig(Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
                    break;
                case 2:
                    _RSDK1GameConfig = new RSDKv1.GameConfig(Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
                    break;
                case 3:
                    _RStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "Zones.mdf"));
                    _CStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "CZones.mdf"));
                    _SStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "SStages.mdf"));
                    _BStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "BStages.mdf"));
                    break;
            }
        }

        private string GetDataDirectory()
        {
            using (var folderBrowserDialog = new RetroED.Extensions.DataSelect.FolderSelectDialog())
            {
                folderBrowserDialog.Title = "Select Data Folder";

                if (!folderBrowserDialog.ShowDialog())
                    return null;

                return folderBrowserDialog.FileName;
            }
        }

        private bool IsDataDirectoryValid()
        {
            return IsDataDirectoryValid(DataDirectory);
        }

        private bool IsDataDirectoryValid(string directoryToCheck)
        {
            bool exists = false;
            switch(LoadedRSDKver)
            {
                case 0:
                    exists = File.Exists(Path.Combine(directoryToCheck, "Game", "GameConfig.bin"));
                    break;
                case 1:
                    exists = File.Exists(Path.Combine(directoryToCheck, "Game", "GameConfig.bin"));
                    break;
                case 2:
                    exists = File.Exists(Path.Combine(directoryToCheck, "Game", "GameConfig.bin"));
                    break;
                case 3:
                    exists = File.Exists(Path.Combine(directoryToCheck, "TitleScr", "Zones.mdf"));
                    break;
            }
            return exists;
        }

        private void ResetDataDirectoryToAndResetScene(string newDataDirectory)
        {
            DataDirectory = newDataDirectory;
            //AddRecentDataFolder(newDataDirectory);
            SetGameConfig();
            OpenScene();
        }

        public void OpenDataDirectory()
        {

            //if (DataDirLoaded == false)
            //{
            string newDataDirectory = GetDataDirectory();
            if (null == newDataDirectory) return;
            if (newDataDirectory.Equals(DataDirectory)) return;

            if (IsDataDirectoryValid(newDataDirectory))
            {
                DataDirectory = newDataDirectory;
                //AddRecentDataFolder(newDataDirectory);
                SetGameConfig();
                OpenScene();
                //ResetDataDirectoryToAndResetScene(newDataDirectory);
                DataDirLoaded = true;
            }
            else
                    MessageBox.Show($@"{newDataDirectory} is nota valid Data Directory.",
                                    "Invalid Data Directory!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            //}
            //else
            //{
            //    return;
            //}
        }

        void OpenScene()
        {

            switch(LoadedRSDKver)
            {
                case 0:
                    RetroED.Extensions.DataSelect.SceneSelect dlgB = new RetroED.Extensions.DataSelect.SceneSelect(_RSDKBGameConfig);

                    dlgB.ShowDialog();
                    if (dlgB.Result.FilePath != null)
                    {
                        string SelectedScene = Path.GetFileName(dlgB.Result.FilePath);
                        string SelectedZone = dlgB.Result.FilePath.Replace(SelectedScene, "");

                        categoryID = dlgB.Result.Category;

                        FilePath = Path.Combine(DataDirectory, "Stages", SelectedZone, SelectedScene);
                        string stageDir = Path.Combine(DataDirectory, "Stages", SelectedZone);
                        Console.WriteLine("Scene Path = " + FilePath);
                        //try
                        //{
                        if (File.Exists(FilePath))
                        {
                            Open(FilePath);
                        }
                        else
                        {
                            Console.WriteLine("Scene Doesn't Exist! " + FilePath);
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Load failed. Error: " + ex.ToString());
                        //    return;
                        //}
                        
                    }
                    break;
                case 1:
                    RetroED.Extensions.DataSelect.SceneSelect dlg2 = new RetroED.Extensions.DataSelect.SceneSelect(_RSDK2GameConfig);

                    dlg2.ShowDialog();
                    if (dlg2.Result.FilePath != null)
                    {
                        string SelectedScene = Path.GetFileName(dlg2.Result.FilePath);
                        string SelectedZone = dlg2.Result.FilePath.Replace(SelectedScene, "");

                        categoryID = dlg2.Result.Category;

                        FilePath = Path.Combine(DataDirectory, "Stages", SelectedZone, SelectedScene);
                        string stageDir = Path.Combine(DataDirectory, "Stages", SelectedZone);
                        Console.WriteLine("Scene Path = " + FilePath);
                        //try
                        //{
                        if (File.Exists(FilePath))
                        {
                            Open(FilePath);
                        }
                        else
                        {
                            Console.WriteLine("Scene Doesn't Exist! " + FilePath);
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Load failed. Error: " + ex.ToString());
                        //    return;
                        //}
                    }
                    break;
                case 2:
                    RetroED.Extensions.DataSelect.SceneSelect dlg1 = new RetroED.Extensions.DataSelect.SceneSelect(_RSDK1GameConfig);

                    dlg1.ShowDialog();
                    if (dlg1.Result.FilePath != null)
                    {
                        string SelectedScene = Path.GetFileName(dlg1.Result.FilePath);
                        string SelectedZone = dlg1.Result.FilePath.Replace(SelectedScene, "");

                        categoryID = dlg1.Result.Category;

                        FilePath = Path.Combine(DataDirectory, "Stages", SelectedZone, SelectedScene);
                        string stageDir = Path.Combine(DataDirectory, "Stages", SelectedZone);
                        Console.WriteLine("Scene Path = " + FilePath);
                        //try
                        //{
                        if (File.Exists(FilePath))
                        {
                            Open(FilePath);
                        }
                        else
                        {
                            Console.WriteLine("Scene Doesn't Exist! " + FilePath);
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Load failed. Error: " + ex.ToString());
                        //    return;
                        //}
                    }
                    break;
                case 3:
                    RetroED.Extensions.DataSelect.SceneSelect dlgRS = new RetroED.Extensions.DataSelect.SceneSelect(_RStagesvRS,_CStagesvRS,_SStagesvRS,_BStagesvRS);

                    dlgRS.ShowDialog();
                    if (dlgRS.Result.FilePath != null)
                    {
                        string SelectedScene = Path.GetFileName(dlgRS.Result.FilePath);
                        string SelectedZone = dlgRS.Result.FilePath.Replace(SelectedScene, "");

                        categoryID = dlgRS.Result.Category;

                        FilePath = Path.Combine(DataDirectory, "Levels", SelectedZone, SelectedScene);
                        string stageDir = Path.Combine(DataDirectory, "Levels", SelectedZone);
                        Console.WriteLine("Scene Path = " + FilePath);
                        //try
                        //{
                        if (File.Exists(FilePath))
                        {
                            Open(FilePath);
                        }
                        else
                        {
                            Console.WriteLine("Scene Doesn't Exist! " + FilePath);
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Load failed. Error: " + ex.ToString());
                        //    return;
                        //}
                    }
                    break;
            }
        }

        private void MenuItem_Open_Click(object sender, EventArgs e)
        {

            switch (MessageBox.Show(this, "Do you want to save the current file?", "RetroED Map Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    saveAsToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
            }

            SelectRSDKForm dlg = new SelectRSDKForm();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                LoadedRSDKver = dlg.RSDKver;
                DataDirectory = null;
                DataDirLoaded = false;
                //if (DataDirectory == null || !DataDirLoaded)
                //{
                    OpenDataDirectory();
                //}
                //else
                //{
                //    SetGameConfig();
                //  OpenScene();
                //}
            }

            /*
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Sonic 1/Sonic 2 Act#.bin files (Act*.bin)|Act*.bin|Sonic CD Act#.bin files (Act*.bin)|Act*.bin|Sonic Nexus Act#.bin files (Act*.bin)|Act*.bin|Retro-Sonic Act#.map files (Act*.map)|Act*.map";

            if (_mapViewer.datapath == null && LoadedObjDefinitions)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = "Select Data Folder";

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _mapViewer.datapath = dlg.SelectedPath + "\\";
                }
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadedRSDKver = ofd.FilterIndex - 1;


        }*/
        }

        public void Open(string folderpath)
        {
            if (LoadedRSDKver == 3) //Are We displaying a Retro-Sonic Map?
            {
                //Load the files needed to show the map
                tiles = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.gfx");
                mappings = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.til");
                Map = folderpath;
                CollisionMasks = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.tcf");
                Stageconfig = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.zcf");
                if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(CollisionMasks) && File.Exists(Stageconfig))
                {
                    LoadScene(folderpath, LoadedRSDKver);
                    LoadObjectsFromDataFolder(DataDirectory);

                    string filename = Path.GetFileName(folderpath);

                    string dispname = "";

                    if (filename != null)
                    {
                        string dir = "";
                        string pth = Path.GetFileName(folderpath);
                        string tmp = folderpath.Replace(pth, "");
                        DirectoryInfo di = new DirectoryInfo(tmp);
                        dir = di.Name;
                        RetroED.MainForm.Instance.TabControl.SelectedTab.Text = dir + "/" + pth;
                        dispname = dir + "/" + pth;
                    }
                    else
                    {
                        RetroED.MainForm.Instance.TabControl.SelectedTab.Text = "New Scene - RSDK Map Editor";
                        dispname = "New Scene - RSDK Map Editor";
                    }

                    Parent.rp.state = "RetroED - " + this.Text;
                    switch (LoadedRSDKver)
                    {
                        case 0:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKvB)";
                            break;
                        case 1:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv2)";
                            break;
                        case 2:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv1)";
                            break;
                        case 3:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKvRS)";
                            break;
                    }
                    SharpPresence.Discord.RunCallbacks();
                    SharpPresence.Discord.UpdatePresence(Parent.rp);
                }
                else
                {
                    MessageBox.Show("Tiles, Mappings, Collision Masks and Stageconfig need to exist in the same folder as act data, just like the game.");
                }
            }

            if (LoadedRSDKver != 3) //Are we NOT displaying a Retro-Sonic Map?
            {
                //Load Map Data
                tiles = Path.Combine(Path.GetDirectoryName(folderpath), "16x16Tiles.gif");
                mappings = Path.Combine(Path.GetDirectoryName(folderpath), "128x128Tiles.bin");
                Map = folderpath;
                CollisionMasks = Path.Combine(Path.GetDirectoryName(folderpath), "CollisionMasks.bin");
                Stageconfig = Path.Combine(Path.GetDirectoryName(folderpath), "Stageconfig.bin");
                if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(CollisionMasks) && File.Exists(Stageconfig))
                {
                    LoadScene(folderpath, LoadedRSDKver);
                    LoadObjectsFromDataFolder(DataDirectory);

                    string filename = Path.GetFileName(folderpath);

                    string dispname = "";

                    if (filename != null)
                    {
                        string dir = "";
                        string pth = Path.GetFileName(folderpath);
                        string tmp = folderpath.Replace(pth, "");
                        DirectoryInfo di = new DirectoryInfo(tmp);
                        dir = di.Name;
                        RetroED.MainForm.Instance.TabControl.SelectedTab.Text = dir + "/" + pth;
                        dispname = dir + "/" + pth;
                    }
                    else
                    {
                        RetroED.MainForm.Instance.TabControl.SelectedTab.Text = "New Scene - RSDK Map Editor";
                        dispname = "New Scene - RSDK Map Editor";
                    }

                    Parent.rp.state = "RetroED - " + this.Text;
                    switch (LoadedRSDKver)
                    {
                        case 0:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKvB)";
                            break;
                        case 1:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv2)";
                            break;
                        case 2:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv1)";
                            break;
                        case 3:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKvRS)";
                            break;
                    }
                    SharpPresence.Discord.RunCallbacks();
                    SharpPresence.Discord.UpdatePresence(Parent.rp);
                }
                else
                {
                    MessageBox.Show("Tiles, Mappings, Collision Masks and Stageconfig need to exist in the same folder as act data, just like the game.");
                }
            }
        }

        private void MenuItem_Save_Click(object sender, EventArgs e)
        {
            if (Map == null) //Do we have a map file open?
            {
                saveAsToolStripMenuItem_Click(this, e); //If not, then let the user make one
            }
            else
            {
                switch (LoadedRSDKver) //Find out what RSDK version is loaded and then write the map data to the selected file
                {
                    case 0:
                        _RSDKBScene.objects = EntityToolbar.EntitiesvB;
                        _RSDKBScene.Write(Map);
                        break;
                    case 1:
                        _RSDK2Scene.objects = EntityToolbar.Entitiesv2;
                        _RSDK2Scene.Write(Map);
                        break;
                    case 2:
                        _RSDK1Scene.objects = EntityToolbar.Entitiesv1;
                        _RSDK1Scene.Write(Map);
                        break;
                    case 3:
                        _RSDKRSScene.objects = EntityToolbar.EntitiesvRS;
                        _RSDKRSScene.Write(Map);
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Sonic 1/Sonic 2 Act#.bin files (Act*.bin)|Act*.bin|Sonic CD Act#.bin files (Act*.bin)|Act*.bin|Sonic Nexus Act#.bin files (Act*.bin)|Act*.bin|Retro-Sonic Act#.map files (Act*.map)|Act*.map";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (LoadedRSDKver) //Find out what RSDK version is loaded and then write the map data to the selected file
                {
                    case 0:
                        _RSDKBScene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    case 1:
                        _RSDK2Scene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    case 2:
                        _RSDK1Scene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    case 3:
                        _RSDKRSScene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuItem_ExportFullImage_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)  //Find out what RSDK version is loaded and then write the map data to the selected image location
            {
                case 0:
                    if (_RSDKBScene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDKBScene.MapLayout[0].Length * 128, _RSDKBScene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDKBScene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDKBScene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDKBChunks.BlockList[_RSDKBScene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDKBScene.objects.Count; o++)
                                {
                                        Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDKBScene.objects[o].type, _RSDKBScene.objects[o].subtype);
                                        if (mapobj != null && mapobj.ID > 0)
                                        {
                                            g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDKBScene.objects[o].xPos + mapobj.PivotX, _RSDKBScene.objects[o].yPos + mapobj.PivotY);
                                        }
                                        else
                                        {
                                            g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDKBScene.objects[o].xPos, _RSDKBScene.objects[o].yPos);
                                        }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK2Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK2Scene.MapLayout[0].Length * 128, _RSDK2Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK2Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK2Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK2Chunks.BlockList[_RSDK2Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK2Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDK2Scene.objects[o].type, _RSDK2Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK2Scene.objects[o].xPos + mapobj.PivotX, _RSDK2Scene.objects[o].yPos + mapobj.PivotY);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK2Scene.objects[o].xPos, _RSDK2Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK1Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK1Scene.MapLayout[0].Length * 128, _RSDK1Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK1Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK1Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK1Chunks.BlockList[_RSDK1Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK1Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDK1Scene.objects[o].type, _RSDK1Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK1Scene.objects[o].xPos + mapobj.PivotX, _RSDK1Scene.objects[o].yPos + mapobj.PivotY);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK1Scene.objects[o].xPos, _RSDK1Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDKRSScene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDKRSScene.MapLayout[0].Length * 128, _RSDKRSScene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDKRSScene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDKRSScene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDKRSChunks.BlockList[_RSDKRSScene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDKRSScene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDKRSScene.objects[o].type, _RSDKRSScene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDKRSScene.objects[o].xPos + mapobj.PivotX, _RSDKRSScene.objects[o].yPos + mapobj.PivotY);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDKRSScene.objects[o].xPos, _RSDKRSScene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_ExportMapImage_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)  //Find out what RSDK version is loaded and then write the map data to the selected image location
            {
                case 0:
                    if (_RSDKBScene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDKBScene.MapLayout[0].Length * 128, _RSDKBScene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDKBScene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDKBScene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDKBChunks.BlockList[_RSDKBScene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK2Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK2Scene.MapLayout[0].Length * 128, _RSDK2Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK2Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK2Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK2Chunks.BlockList[_RSDK2Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK1Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK1Scene.MapLayout[0].Length * 128, _RSDK1Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK1Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK1Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK1Chunks.BlockList[_RSDK1Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDKRSScene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDKRSScene.MapLayout[0].Length * 128, _RSDKRSScene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDKRSScene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDKRSScene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDKRSChunks.BlockList[_RSDKRSScene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_ExportObjImage_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)  //Find out what RSDK version is loaded and then write the map data to the selected image location
            {
                case 0:
                    if (_RSDKBScene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDKBScene.MapLayout[0].Length * 128, _RSDKBScene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDKBScene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDKBScene.objects[o].type, _RSDKBScene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDKBScene.objects[o].xPos, _RSDKBScene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDKBScene.objects[o].xPos, _RSDKBScene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK2Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK2Scene.MapLayout[0].Length * 128, _RSDK2Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK2Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDK2Scene.objects[o].type, _RSDK2Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK2Scene.objects[o].xPos, _RSDK2Scene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK2Scene.objects[o].xPos, _RSDK2Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK1Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK1Scene.MapLayout[0].Length * 128, _RSDK1Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK1Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDK1Scene.objects[o].type, _RSDK1Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK1Scene.objects[o].xPos, _RSDK1Scene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK1Scene.objects[o].xPos, _RSDK1Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDKRSScene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDKRSScene.MapLayout[0].Length * 128, _RSDKRSScene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDKRSScene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(_RSDKRSScene.objects[o].type, _RSDKRSScene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDKRSScene.objects[o].xPos, _RSDKRSScene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDKRSScene.objects[o].xPos, _RSDKRSScene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            Close(); //Close the "Applet"
        }

        private void MenuItem_ClearChunks_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Find out what RSDK version is used and replace all the chunks with a transparent one (Chunk Zero)
            {
                case 3:
                    ushort[][] NewChunks1 = new ushort[_RSDKRSScene.height][];
                    for (ushort i = 0; i < _RSDKRSScene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewChunks1[i] = new ushort[_RSDKRSScene.width];
                        for (int j = 0; j < _RSDKRSScene.width; ++j)
                            NewChunks1[i][j] = 0; // fill the chunks with blanks
                    }
                    _RSDKRSScene.MapLayout = NewChunks1;
                    _mapViewer.DrawScene();
                    break;
                case 2:
                    ushort[][] NewTiles2 = new ushort[_RSDK1Scene.height][];
                    for (ushort i = 0; i < _RSDK1Scene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles2[i] = new ushort[_RSDK1Scene.width];
                        for (int j = 0; j < _RSDK1Scene.width; ++j)
                            NewTiles2[i][j] = 0; // fill the chunks with blanks
                    }
                    _RSDK1Scene.MapLayout = NewTiles2;

                    _mapViewer.DrawScene();
                    break;
                case 1:
                    ushort[][] NewTiles3 = new ushort[_RSDK2Scene.height][];
                    for (ushort i = 0; i < _RSDK2Scene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles3[i] = new ushort[_RSDK2Scene.width];
                        for (int j = 0; j < _RSDK2Scene.width; ++j)
                            NewTiles3[i][j] = 0; // fill the chunks with blanks
                    }
                    _RSDK2Scene.MapLayout = NewTiles3;
                    _mapViewer.DrawScene();
                    break;
                case 0:
                    ushort[][] NewTiles4 = new ushort[_RSDKBScene.height][];
                    for (ushort i = 0; i < _RSDKBScene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles4[i] = new ushort[_RSDKBScene.width];
                        for (int j = 0; j < _RSDKBScene.width; ++j)
                            NewTiles4[i][j] = 0; // fill the chunks with blanks
                    }
                    _RSDKBScene.MapLayout = NewTiles4;
                    _mapViewer.DrawScene();
                    break;
            }
        }

        private void MenuItem_ClearObjects_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Find out what RSDK version is loaded and clear the object list of objects
            {
                case 3:
                    _RSDKRSScene.objects.Clear();
                    _blocksViewer.tabControl.TabPages[1].Controls.Clear();
                    EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesvRS, this);
                    EntityToolbar.RSDKVer = LoadedRSDKver;
                    EntityToolbar.Dock = DockStyle.Fill;
                    EntityToolbar.EntitiesvRS = _RSDKRSScene.objects;
                    _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
                    _mapViewer.DrawScene();
                    break;
                case 2:
                    _RSDK1Scene.objects.Clear();
                    _blocksViewer.tabControl.TabPages[1].Controls.Clear();
                    EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesv1, this);
                    EntityToolbar.RSDKVer = LoadedRSDKver;
                    EntityToolbar.Dock = DockStyle.Fill;
                    EntityToolbar.Entitiesv1 = _RSDK1Scene.objects;
                    _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
                    _mapViewer.DrawScene();
                    break;
                case 1:
                    _RSDK2Scene.objects.Clear();
                    _blocksViewer.tabControl.TabPages[1].Controls.Clear();
                    EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesv2, this);
                    EntityToolbar.RSDKVer = LoadedRSDKver;
                    EntityToolbar.Dock = DockStyle.Fill;
                    EntityToolbar.Entitiesv2 = _RSDK2Scene.objects;
                    _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
                    _mapViewer.DrawScene();
                    break;
                case 0:
                    _RSDKBScene.objects.Clear();
                    _blocksViewer.tabControl.TabPages[1].Controls.Clear();
                    EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesvB, this);
                    EntityToolbar.RSDKVer = LoadedRSDKver;
                    EntityToolbar.Dock = DockStyle.Fill;
                    EntityToolbar.EntitiesvB = _RSDKBScene.objects;
                    _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
                    _mapViewer.DrawScene();
                    break;
            }
        }

        private void MenuItem_MapProp_Click(object sender, EventArgs e)
        {
            PropertiesForm frm = new PropertiesForm(LoadedRSDKver);

            ushort[][] OldTiles;
            ushort[][] NewTiles;
            int OLDwidth = 0;
            int OLDheight = 0;

            switch (LoadedRSDKver)
            {
                case 3:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDKRSScene.MapLayout;
                    OLDwidth = _RSDKRSScene.width;
                    OLDheight = _RSDKRSScene.height;
                    //Set the form data to the map data
                    frm.Mapv1 = _RSDKRSScene;
                    frm.Setup();
                    break;
                case 2:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK1Scene.MapLayout;
                    OLDwidth = _RSDK1Scene.width;
                    OLDheight = _RSDK1Scene.height;
                    //Set the form data to the map data
                    frm.Mapv2 = _RSDK1Scene;
                    frm.Setup();
                    break;
                case 1:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK2Scene.MapLayout;
                    OLDwidth = _RSDK2Scene.width;
                    OLDheight = _RSDK2Scene.height;
                    //Set the form data to the map data
                    frm.Mapv3 = _RSDK2Scene;
                    frm.Setup();
                    break;
                case 0:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDKBScene.MapLayout;
                    OLDwidth = _RSDKBScene.width;
                    OLDheight = _RSDKBScene.height;
                    //Set the form data to the map data
                    frm.Mapv4 = _RSDKBScene;
                    frm.Setup();
                    break;
                default:
                    break;
            }
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                switch (LoadedRSDKver)
                {
                    case 3:
                        OldTiles = _RSDKRSScene.MapLayout; //Get Old Chunks
                        _RSDKRSScene = frm.Mapv1; //Set the map data to the updated data
                        NewTiles = _RSDKRSScene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    case 2:
                        OldTiles = _RSDK1Scene.MapLayout; //Get Old Chunks
                        _RSDK1Scene = frm.Mapv2; //Set the map data to the updated data
                        NewTiles = _RSDK1Scene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    case 1:
                        OldTiles = _RSDK2Scene.MapLayout; //Get Old Chunks
                        _RSDK2Scene = frm.Mapv3; //Set the map data to the updated data
                        NewTiles = _RSDK2Scene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    case 0:
                        OldTiles = _RSDKBScene.MapLayout; //Get Old Chunks
                        _RSDKBScene = frm.Mapv4; //Set the map data to the updated data
                        NewTiles = _RSDKBScene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuItem_MapLayer_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowMap = MenuItem_MapLayer.Checked = !_mapViewer.ShowMap; //Are we going to show the Map Layout?
            _mapViewer.DrawScene();
        }

        private void MenuItem_Objects_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowObjects = MenuItem_Objects.Checked = !_mapViewer.ShowObjects; //Are we going to show the Objects?
            _mapViewer.DrawScene();
        }

        private void MenuItem_CollisionMasks_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowCollisionA = MenuItem_CollisionMasksLyrA.Checked = !_mapViewer.ShowCollisionA; //Are we going to show the Collision Masks for each tile?
            _mapViewer.ShowCollisionB = MenuItem_CollisionMasksLyrB.Checked = false;
            _mapViewer.DrawScene();
        }

        private void MenuItem_CollisionMasksLyrB_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowCollisionB = MenuItem_CollisionMasksLyrB.Checked = !_mapViewer.ShowCollisionB; //Are we going to show the Collision Masks for each tile?
            _mapViewer.ShowCollisionA = MenuItem_CollisionMasksLyrA.Checked = false;
            _mapViewer.DrawScene();
        }

        private void MenuItem_ShowGrid_Click(object sender, EventArgs e)
        {
            showgrid = _mapViewer.ShowGrid = MenuItem_ShowGrid.Checked = !showgrid; //Do we want a grid overlayed on our map?
            _mapViewer.DrawScene();
        }

        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(); //Show the about page
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select Data Folder";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _mapViewer.datapath = dlg.SelectedPath + "\\";
            }
        }

        private void MenuItem_LoadObjListFromData_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                LoadObjectsFromDataFolder(dlg.SelectedPath);
            }
        }

        public void LoadObjectsFromDataFolder(string datapath)
        {
            switch (LoadedRSDKver)
            {
                case 0:
                    LoadRSDKBObjectsFromStageconfig(datapath);
                    break;
                case 1:
                    LoadRSDK2ObjectsFromStageconfig(datapath);
                    break;
                case 2:
                    LoadRSDK1ObjectsFromStageconfig(datapath);
                    break;
                case 3:
                    LoadRSDKObjectDefinitionsFromStageconfig(datapath);
                    break;
            }
        }

        public void LoadRSDKObjectDefinitionsFromStageconfig(string datapath)
        {
            RSDKvRS.Zoneconfig _RSDKRSZoneconfig = new RSDKvRS.Zoneconfig(Stageconfig);
            _mapViewer.ObjectDefinitions.Objects.Clear();

            ObjectNames.Clear();
            ObjectTypesvRS.Clear();

            List<string> Sheets = new List<string>();

            Sheets.Add(datapath + "\\Objects\\Titlecard1.gfx");
            Sheets.Add("???");
            Sheets.Add(datapath + "\\Objects\\Shields.gfx");
            Sheets.Add("???");
            Sheets.Add(datapath + "\\Objects\\General.gfx");
            Sheets.Add(datapath + "\\Objects\\General2.gfx");
            

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("BlankObject", 0, 0, datapath + "Blank Objects Don't need sprites lmao", 0, 0, 0, 0, 0, 0, 0));
            ObjectTypesvRS.Add(new RSDKvRS.Object(0, 0, 0, 0, 0, "BlankObject"));
            ObjectNames.Add("BlankObject");

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(1, 0), new Object_Definitions.MapObject("Ring", 1, 0, datapath + "\\Objects\\General.gfx", 0, 0, 16, 16, -8, -8, 0));
            ObjectNames.Add("Ring");
            ObjectTypesvRS.Add(new RSDKvRS.Object(1, 0, 0, 0, 1, "Ring"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(2, 0), new Object_Definitions.MapObject("DroppedRing", 2, 0, datapath + "//Objects//General.gfx", 0, 0, 16, 16, -8, -8, 0));
            ObjectNames.Add("DroppedRing");
            ObjectTypesvRS.Add(new RSDKvRS.Object(2, 0, 0, 0, 2, "DroppedRing"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(3, 0), new Object_Definitions.MapObject("RingSparkle", 3, 0, datapath + "//Objects//General.gfx", 0, 0, 16, 16, -8, -8, 0));
            ObjectNames.Add("RingSparkle");
            ObjectTypesvRS.Add(new RSDKvRS.Object(3, 0, 0, 0, 3, "RingSparkle"));

            Object_Definitions.MapObject BlankItemBox = new Object_Definitions.MapObject("BlankItemBox", 4, 0, datapath + "\\Objects\\General.gfx", 24, 0, 30, 32,-15,-16,0);
            Object_Definitions.MapObject RingBox = new Object_Definitions.MapObject("RingBox", 4, 1, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16,-7,-8,0);
            Object_Definitions.MapObject BlueShield = new Object_Definitions.MapObject("BlueShieldBox", 4, 2, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject MagnetShield = new Object_Definitions.MapObject("MagnetShieldBox", 4, 3, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject FireShield = new Object_Definitions.MapObject("FireShieldBox", 4, 4, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject BubbleShield = new Object_Definitions.MapObject("BubbleShieldBox", 4, 5, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject InvincibilityMonitor = new Object_Definitions.MapObject("InvincibilityBox", 4, 6, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject SpeedShoesMonitor = new Object_Definitions.MapObject("SpeedShoesBox", 4, 7, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject EggmanMonitor = new Object_Definitions.MapObject("EggmanBox", 4, 8, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject oneUP = new Object_Definitions.MapObject("1UPBox", 4, 9, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            Object_Definitions.MapObject BlueRing = new Object_Definitions.MapObject("BlueRingBox", 4, 10, datapath + "\\Objects\\General.gfx", 0, 0, 14, 16, -7, -8, 0);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 0), BlankItemBox);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 1), RingBox);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 2), BlueShield);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 3), MagnetShield);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 4), FireShield);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 5), BubbleShield);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 6), InvincibilityMonitor);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 7), SpeedShoesMonitor);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 8), EggmanMonitor);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 9), oneUP);
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(4, 10), BlueRing);
            ObjectNames.Add("ItemBox");
            ObjectTypesvRS.Add(new RSDKvRS.Object(4, 0, 0, 0, 4, "ItemBox"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(5, 0), new Object_Definitions.MapObject("BrokenItemBox", 5, 0, datapath + "\\Objects\\General.gfx", 24, 0, 30, 32,-15,-16,0));
            ObjectNames.Add("BrokenItemBox");
            ObjectTypesvRS.Add(new RSDKvRS.Object(5, 0, 0, 0, 5, "BrokenItemBox"));

            //TO-DO: MAP THE SPRING DIRECTIONS
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 0), new Object_Definitions.MapObject("YellowSpring", 6, 0, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 1), new Object_Definitions.MapObject("YellowSpring", 6, 1, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 2), new Object_Definitions.MapObject("YellowSpring", 6, 2, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 3), new Object_Definitions.MapObject("YellowSpring", 6, 3, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 4), new Object_Definitions.MapObject("YellowSpring", 6, 4, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 5), new Object_Definitions.MapObject("YellowSpring", 6, 5, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 6), new Object_Definitions.MapObject("YellowSpring", 6, 6, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(6, 7), new Object_Definitions.MapObject("YellowSpring", 6, 7, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            ObjectNames.Add("YellowSpring");
            ObjectTypesvRS.Add(new RSDKvRS.Object(6, 0, 0, 0, 6, "YellowSpring"));

            //TO-DO: MAP THE SPRING DIRECTIONS
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 0), new Object_Definitions.MapObject("RedSpring", 7, 0, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 1), new Object_Definitions.MapObject("RedSpring", 7, 1, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 2), new Object_Definitions.MapObject("RedSpring", 7, 2, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 3), new Object_Definitions.MapObject("RedSpring", 7, 3, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 4), new Object_Definitions.MapObject("RedSpring", 7, 4, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 5), new Object_Definitions.MapObject("RedSpring", 7, 5, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 6), new Object_Definitions.MapObject("RedSpring", 7, 6, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(7, 7), new Object_Definitions.MapObject("RedSpring", 7, 7, datapath + "\\Objects\\General.gfx", 86, 16, 32, 16, -16, -8, 0));
            ObjectNames.Add("RedSpring");
            ObjectTypesvRS.Add(new RSDKvRS.Object(7, 0, 0, 0, 7, "RedSpring"));

            //TO-DO: MAP THE SPIKE DIRECTIONS
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 0), new Object_Definitions.MapObject("Spikes", 8, 0, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 1), new Object_Definitions.MapObject("Spikes", 8, 1, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 2), new Object_Definitions.MapObject("Spikes", 8, 2, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 3), new Object_Definitions.MapObject("Spikes", 8, 3, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            ObjectNames.Add("Spikes");
            ObjectTypesvRS.Add(new RSDKvRS.Object(8, 0, 0, 0, 8, "Spikes"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(9, 0), new Object_Definitions.MapObject("Checkpoint", 9, 0, datapath + "\\Objects\\General.gfx", 240, 0, 16, 48, -8, -24, 0));
            ObjectNames.Add("Checkpoint");
            ObjectTypesvRS.Add(new RSDKvRS.Object(9, 0, 0, 0, 9, "Checkpoint"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(10, 0), new Object_Definitions.MapObject("UnknownObject(Type10)", 10, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type10)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(10, 0, 0, 0, 10, "UnknownObject(Type10)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(11, 0), new Object_Definitions.MapObject("UnknownObject(Type11)", 11, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type11)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(11, 0, 0, 0, 11, "UnknownObject(Type11)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(12, 0), new Object_Definitions.MapObject("UnknownObject(Type12)", 12, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type12)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(12, 0, 0, 0, 12, "UnknownObject(Type12)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(13, 0), new Object_Definitions.MapObject("UnknownObject(Type13)", 13, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type13)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(13, 0, 0, 0, 13, "UnknownObject(Type13)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(14, 0), new Object_Definitions.MapObject("UnknownObject(Type14)", 14, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type14)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(14, 0, 0, 0, 14, "UnknownObject(Type14)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(15, 0), new Object_Definitions.MapObject("UnknownObject(Type15)", 15, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type15)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(15, 0, 0, 0, 15, "UnknownObject(Type15)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(16, 0), new Object_Definitions.MapObject("UnknownObject(Type16)", 16, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type16)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(16, 0, 0, 0, 16, "UnknownObject(Type16)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(17, 0), new Object_Definitions.MapObject("UnknownObject(Type17)", 17, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type17)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(17, 0, 0, 0, 17, "UnknownObject(Type17)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(18, 0), new Object_Definitions.MapObject("SignPost", 18, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("SignPost");
            ObjectTypesvRS.Add(new RSDKvRS.Object(18, 0, 0, 0, 18, "SignPost"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(19, 0), new Object_Definitions.MapObject("EggPrison", 19, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("EggPrison");
            ObjectTypesvRS.Add(new RSDKvRS.Object(19, 0, 0, 0, 19, "EggPrison"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(20, 0), new Object_Definitions.MapObject("SmallExplosion", 20, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("SmallExplosion");
            ObjectTypesvRS.Add(new RSDKvRS.Object(20, 0, 0, 0, 20, "SmallExplosion"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(21, 0), new Object_Definitions.MapObject("BigExplosion", 21, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("BigExplosion");
            ObjectTypesvRS.Add(new RSDKvRS.Object(21, 0, 0, 0, 21, "BigExplosion"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(22, 0), new Object_Definitions.MapObject("EggPrisonDebris", 22, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("EggPrisonDebris");
            ObjectTypesvRS.Add(new RSDKvRS.Object(22, 0, 0, 0, 22, "EggPrisonDebris"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(23, 0), new Object_Definitions.MapObject("Animal", 23, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("Animal");
            ObjectTypesvRS.Add(new RSDKvRS.Object(23, 0, 0, 0, 23, "Animal"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(24, 0), new Object_Definitions.MapObject("UnknownObject(Type24)", 24, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type24)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(24, 0, 0, 0, 24, "UnknownObject(Type24)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(25, 0), new Object_Definitions.MapObject("UnknownObject(Type25)", 25, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type25)");
            ObjectTypesvRS.Add(new RSDKvRS.Object(25, 0, 0, 0, 25, "UnknownObject(Type25)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(26, 0), new Object_Definitions.MapObject("BigRing", 26, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("BigRing");
            ObjectTypesvRS.Add(new RSDKvRS.Object(26, 0, 0, 0, 26, "BigRing"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(27, 0), new Object_Definitions.MapObject("WaterSplash", 27, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("WaterSplash");
            ObjectTypesvRS.Add(new RSDKvRS.Object(27, 0, 0, 0, 27, "WaterSplash"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(28, 0), new Object_Definitions.MapObject("AirBubbleSpawner", 28, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("AirBubbleSpawner");
            ObjectTypesvRS.Add(new RSDKvRS.Object(28, 0, 0, 0, 28, "AirBubbleSpawner"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(29, 0), new Object_Definitions.MapObject("SmallAirBubble", 29, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("SmallAirBubble");
            ObjectTypesvRS.Add(new RSDKvRS.Object(29, 0, 0, 0, 29, "SmallAirBubble"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(30, 0), new Object_Definitions.MapObject("SmokePuff", 30, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("SmokePuff");
            ObjectTypesvRS.Add(new RSDKvRS.Object(30, 0, 0, 0, 30, "SmokePuff"));

            for (int i = 1; i <= _RSDKRSZoneconfig.Objects.Count; i++)
            {
                try
                {
                    RSDKvRS.Script Script = new RSDKvRS.Script((new RSDKvRS.Reader(datapath + "//Objects//Scripts//" + _RSDKRSZoneconfig.Objects[i - 1].FilePath)));

                   for (int s = 0; s < _RSDKRSZoneconfig.ObjectSpritesheets.Count; s++)
                    {
                        Sheets.Add(datapath + "\\Objects\\gfx\\" + _RSDKRSZoneconfig.ObjectSpritesheets[s]);
                    }

                    string Sheet = Sheets[_RSDKRSZoneconfig.Objects[i - 1].SpriteSheetID];

                    int PivotX = 0;
                    int PivotY = 0;
                    int Width = 0;
                    int Height = 0;
                    int X = 0;
                    int Y = 0;

                    if (Script.spriteFrames.Count >= 1)
                    {
                        int SprFrameIndex = Script.EditorDecompile();

                        PivotX = Script.spriteFrames[SprFrameIndex].PivotX;
                        PivotY = Script.spriteFrames[SprFrameIndex].PivotY;
                        Width = Script.spriteFrames[SprFrameIndex].Width;
                        Height = Script.spriteFrames[SprFrameIndex].Height;
                        X = Script.spriteFrames[SprFrameIndex].Xpos;
                        Y = Script.spriteFrames[SprFrameIndex].Ypos;
                    }

                if (Width == 0 && Height == 0)
                {
                    Sheet = "";
                }

                _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 30), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath), (i + 30), 0, Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath));
                    ObjectTypesvRS.Add(new RSDKvRS.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 30), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath), (i + 30), 0, "", 0, 0, 0, 0));
                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath));
                    ObjectTypesvRS.Add(new RSDKvRS.Object(i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath)));
                }
            }
            for (int i = 0; i < _RSDKRSScene.objects.Count; i++)
            {
                _RSDKRSScene.objects[i].Name = ObjectNames[_RSDKRSScene.objects[i].type];
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesvRS, this);
            EntityToolbar.RSDKVer = LoadedRSDKver;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.EntitiesvRS = _RSDKRSScene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);

        }

        public void LoadRSDK1ObjectsFromStageconfig(string datapath)
        {
            _mapViewer.ObjectDefinitions.Objects.Clear();
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(1, 0), new Object_Definitions.MapObject("Player Spawn", 0, 0, "", 0, 0, 0, 0));

            ObjectNames.Clear();
            ObjectTypesv1.Clear();

            ObjectNames.Add("BlankObject");
            ObjectTypesv1.Add(new RSDKv1.Object(0, 0, 0, 0, 0, "BlankObject"));

            ObjectNames.Add("PlayerSpawn");
            ObjectTypesv1.Add(new RSDKv1.Object(1, 0, 0, 0, 1, "PlayerSpawn"));

            if (_RSDK1Stageconfig.LoadGlobalScripts)
            {
                for (int i = 1; i <= _RSDK1GameConfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDK1GameConfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = "NULL";

                        if (LoadSprites.Count >= 1)
                        {
                            Sheet = LoadSprites[0].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1]));
                                    ObjectTypesv1.Add(new RSDKv1.Object(i + 1, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1]));
                            ObjectTypesv1.Add(new RSDKv1.Object(i + 1, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1]));
                        ObjectTypesv1.Add(new RSDKv1.Object(i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1GameConfig.ScriptPaths[i - 1])));
                    }
                }
                for (int i = 1; i <= _RSDK1Stageconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDK1Stageconfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = LoadSprites[0].Paramaters[0];

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]));
                                    ObjectTypesv1.Add(new RSDKv1.Object(i + 1, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDK1GameConfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]), (i + _RSDK1GameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDK1GameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]), (i + _RSDK1GameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]));
                            ObjectTypesv1.Add(new RSDKv1.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDK1GameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]), (i + _RSDK1GameConfig.ScriptPaths.Count + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]));
                        ObjectTypesv1.Add(new RSDKv1.Object(i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1])));
                    }
                }
                for (int i = 0; i < _RSDK1Scene.objects.Count; i++)
                {
                    _RSDK1Scene.objects[i].Name = ObjectNames[_RSDK1Scene.objects[i].type];
                }
            }
            else
            {
                for (int i = 1; i <= _RSDK1Stageconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDK1Stageconfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = "NULL";

                        if (LoadSprites.Count >= 1)
                        {
                            Sheet = LoadSprites[0].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        if (SetEditorIcon.Count >= 1)
                        {
                            PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                            PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                            Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                            Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                            X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                            Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                        }

                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]));
                        ObjectTypesv1.Add(new RSDKv1.Object(i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i +  1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1]));
                        ObjectTypesv1.Add(new RSDKv1.Object(i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK1Stageconfig.ScriptPaths[i - 1])));
                    }
                }
                for (int i = 0; i < _RSDK1Scene.objects.Count; i++)
                {
                    _RSDK1Scene.objects[i].Name = ObjectNames[_RSDK1Scene.objects[i].type];
                }
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesv1,this);
            EntityToolbar.RSDKVer = LoadedRSDKver;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entitiesv1 = _RSDK1Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);

        }

        public void LoadRSDK2ObjectsFromStageconfig(string datapath)
        {
            _mapViewer.ObjectDefinitions.Objects.Clear();
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));

            bool UsingBytecode = Directory.Exists(datapath + "//Scripts//Bytecode");

            ObjectNames.Clear();
            ObjectTypesv2.Clear();

            ObjectNames.Add("BlankObject");

            ObjectTypesv2.Add(new RSDKv2.Object(0, 0, 0, 0, 0, "BlankObject"));

            if (_RSDK2Stageconfig.LoadGlobalScripts)
            {
                for (int i = 1; i <= _RSDK2GameConfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDK2GameConfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = "NULL";

                        if (LoadSprites.Count >= 1)
                        {
                            Sheet = LoadSprites[0].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1]));
                                    ObjectTypesv2.Add(new RSDKv2.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1]));
                            ObjectTypesv2.Add(new RSDKv2.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1]));
                        ObjectTypesv2.Add(new RSDKv2.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2GameConfig.ScriptPaths[i - 1])));
                    }
                }
                for (int i = 1; i <= _RSDK2Stageconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDK2Stageconfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = LoadSprites[0].Paramaters[0];

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]));
                                    ObjectTypesv2.Add(new RSDKv2.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDK2GameConfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]), (i + _RSDK2GameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDK2GameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]), (i + _RSDK2GameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]));
                            ObjectTypesv2.Add(new RSDKv2.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDK2GameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]), (i + _RSDK2GameConfig.ScriptPaths.Count + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypesv2.Add(new RSDKv2.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < _RSDK2Scene.objects.Count; i++)
                {
                    try
                    {
                        _RSDK2Scene.objects[i].Name = ObjectNames[_RSDK2Scene.objects[i].type];
                    }
                    catch (Exception ex)
                    {
                        _RSDK2Scene.objects[i].Name = ObjectNames[0];
                    }
                }
            }
            else
            {
                for (int i = 1; i <= _RSDK2Stageconfig.ObjectsNames.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDK2Stageconfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = "NULL";

                        if (LoadSprites.Count >= 1)
                        {
                            Sheet = LoadSprites[0].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        if (SetEditorIcon.Count >= 1)
                        {
                            PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                            PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                            Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                            Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                            X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                            Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                        }

                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypesv2.Add(new RSDKv2.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypesv2.Add(new RSDKv2.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDK2Stageconfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < _RSDK2Scene.objects.Count; i++)
                {
                    _RSDK2Scene.objects[i].Name = ObjectNames[_RSDK2Scene.objects[i].type];
                }
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesv2, this);
            EntityToolbar.RSDKVer = LoadedRSDKver;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entitiesv2 = _RSDK2Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
        }

        public void LoadRSDKBObjectsFromStageconfig(string datapath)
        {
            RSDKvB.GameConfig _RSDKBGameConfig = new RSDKvB.GameConfig(datapath + "//Game//Gameconfig.bin");
            RSDKvB.StageConfig _RSDKBStageConfig = new RSDKvB.StageConfig(Stageconfig);
            _mapViewer.ObjectDefinitions.Objects.Clear();
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));

            bool UsingBytecode = Directory.Exists(datapath + "//Scripts//Bytecode");

            if (!Directory.Exists(datapath + "//Scripts"))
            {
                Directory.CreateDirectory(datapath + "//Scripts");
            }

            ObjectNames.Clear();
            ObjectTypesvB.Clear();

            ObjectNames.Add("BlankObject");

            ObjectTypesvB.Add(new RSDKvB.Object(0, 0, 0, 0, 0, "BlankObject"));

            if (_RSDKBStageConfig.LoadGlobalScripts)
            {
                for (int i = 1; i <= _RSDKBGameConfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDKBGameConfig.ScriptPaths[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = "NULL";

                        if (LoadSprites.Count >= 1)
                        {
                            Sheet = LoadSprites[0].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1]));
                                    ObjectTypesv1.Add(new RSDKv1.Object(i + 1, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1]));
                            ObjectTypesvB.Add(new RSDKvB.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1]));
                        ObjectTypesv2.Add(new RSDKv2.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBGameConfig.ScriptPaths[i - 1])));
                    }
                }
                for (int i = 1; i <= _RSDKBStageConfig.ObjectsNames.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDKBStageConfig.ObjectsNames[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = LoadSprites[0].Paramaters[0];

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]));
                                    ObjectTypesvB.Add(new RSDKvB.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDKBGameConfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]), (i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]), (i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]));
                            ObjectTypesvB.Add(new RSDKvB.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]), (i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]));
                        ObjectTypesvB.Add(new RSDKvB.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < _RSDKBScene.objects.Count; i++)
                {
                    try
                    {
                        _RSDKBScene.objects[i].Name = ObjectNames[_RSDKBScene.objects[i].type];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message);
                    }
                }
            }
            else
            {
                for (int i = 1; i <= _RSDKBStageConfig.ObjectsNames.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + _RSDKBStageConfig.ObjectsNames[i - 1]));

                        RSDKv1.Script.Sub subv1 = new RSDKv1.Script.Sub();

                        for (int ii = 0; ii < Script.Subs.Count; ii++)
                        {
                            if (Script.Subs[ii].Name == "SubRSDK")
                            {
                                subv1 = Script.Subs[ii];
                                break;
                            }
                        }

                        List<RSDKv1.Script.Sub.Function> LoadSprites = subv1.GetFunctionByName("LoadSpriteSheet");

                        string Sheet = "NULL";

                        if (LoadSprites.Count >= 1)
                        {
                            Sheet = LoadSprites[0].Paramaters[0];
                        }

                        List<RSDKv1.Script.Sub.Function> SetEditorIcon = subv1.GetFunctionByName("SetEditorIcon");

                        int PivotX = 0;
                        int PivotY = 0;
                        int Width = 0;
                        int Height = 0;
                        int X = 0;
                        int Y = 0;

                        string IconType = "SingleIcon";

                        bool SingleIcon = true;

                        if (SetEditorIcon.Count >= 1)
                        {
                            IconType = SetEditorIcon[0].Paramaters[1];

                            switch (IconType)
                            {
                                case "SingleIcon":
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                case "RepeatV": //Temp, Will make actual one later
                                    PivotX = Convert.ToInt32(SetEditorIcon[0].Paramaters[2]);
                                    PivotY = Convert.ToInt32(SetEditorIcon[0].Paramaters[3]);
                                    Width = Convert.ToInt32(SetEditorIcon[0].Paramaters[4]);
                                    Height = Convert.ToInt32(SetEditorIcon[0].Paramaters[5]);
                                    X = Convert.ToInt32(SetEditorIcon[0].Paramaters[6]);
                                    Y = Convert.ToInt32(SetEditorIcon[0].Paramaters[7]);
                                    break;
                                default:
                                    SingleIcon = false;
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]));
                                    ObjectTypesvB.Add(new RSDKvB.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1])));
                                    for (int s = 0; s < SetEditorIcon.Count; s++)
                                    {
                                        PivotX = Convert.ToInt32(SetEditorIcon[s].Paramaters[2]);
                                        PivotY = Convert.ToInt32(SetEditorIcon[s].Paramaters[3]);
                                        Width = Convert.ToInt32(SetEditorIcon[s].Paramaters[4]);
                                        Height = Convert.ToInt32(SetEditorIcon[s].Paramaters[5]);
                                        X = Convert.ToInt32(SetEditorIcon[s].Paramaters[6]);
                                        Y = Convert.ToInt32(SetEditorIcon[s].Paramaters[7]);

                                        string IconStr = SetEditorIcon[s].Paramaters[0].Replace("Icon", "");

                                        int iconSubType = Convert.ToInt32(IconStr);

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDKBGameConfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]), (i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]), (i + _RSDKBGameConfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]));
                            ObjectTypesvB.Add(new RSDKvB.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1]));
                        ObjectTypesvB.Add(new RSDKvB.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKBStageConfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < _RSDKBScene.objects.Count; i++)
                {
                    try
                    {
                        _RSDKBScene.objects[i].Name = ObjectNames[_RSDKBScene.objects[i].type];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message);
                    }
                }
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypesvB, this);
            EntityToolbar.RSDKVer = LoadedRSDKver;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.EntitiesvB = _RSDKBScene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
        }

        private void menuItem_RedrawMap_Click(object sender, EventArgs e)
        {
            _mapViewer.DrawScene();
        }

        private void MenuItem_RefreshChunks_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    _RSDKBChunks = new RSDKvB.Tiles128x128(mappings);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                case 1:
                    _RSDK2Chunks = new RSDKv2.Tiles128x128(mappings);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                case 2:
                    _RSDK1Chunks = new RSDKv1.Tiles128x128(mappings);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                case 3:
                    _RSDKRSChunks = new RSDKvRS.Tiles128x128(mappings);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_ReloadMap_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    _RSDKBScene = new RSDKvB.Scene(Map);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                case 1:
                    _RSDK2Scene = new RSDKv2.Scene(Map);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                case 2:
                    _RSDK1Scene = new RSDKv1.Scene(Map);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                case 3:
                    _RSDKRSScene = new RSDKvRS.Scene(Map);
                    _blocksViewer.SetChunks();
                    _mapViewer.SetScene();
                    break;
                default:
                    break;
            }

        }

        private void MenuItem_ReloadTileset_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        _loadedTiles = (Bitmap)bmp.Clone();
                    }
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.SetScene();
                    break;
                case 1:
                    using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        _loadedTiles = (Bitmap)bmp.Clone();
                    }
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.SetScene();
                    break;
                case 2:
                    using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
                    {
                        var bmp = new Bitmap(fs);
                        _loadedTiles = (Bitmap)bmp.Clone();
                    }
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.SetScene();
                    break;
                case 3:
                    RSDKvRS.gfx gfx = new RSDKvRS.gfx(tiles);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetScene();
                    break;
                default:
                    break;
            }

        }

        private void MenuItem_ReloadTileconfig_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    _RSDKBCollisionMask = new RSDKvB.Tileconfig(CollisionMasks);
                    _mapViewer.SetScene();
                    break;
                case 1:
                    _RSDK2CollisionMask = new RSDKv2.Tileconfig(CollisionMasks);
                    _mapViewer.SetScene();
                    break;
                case 2:
                    _RSDK1CollisionMask = new RSDKv1.Tileconfig(CollisionMasks);
                    _mapViewer.SetScene();
                    break;
                case 3:
                    _RSDKRSCollisionMask = new RSDKvRS.Tileconfig(CollisionMasks,false);
                    _mapViewer.SetScene();
                    break;
                default:
                    break;
            }

        }

        private void MenuItem_ReloadStageconfig_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    _RSDKRSStageconfig = new RSDKvRS.Zoneconfig(Stageconfig);
                    _mapViewer.SetScene();
                    break;
                case 1:
                    _RSDK1Stageconfig = new RSDKv1.StageConfig(Stageconfig);
                    _mapViewer.SetScene();
                    break;
                case 2:
                    _RSDK2Stageconfig = new RSDKv2.StageConfig(Stageconfig);
                    _mapViewer.SetScene();
                    break;
                case 3:
                    _RSDKBStageconfig = new RSDKvB.StageConfig(Stageconfig);
                    _mapViewer.SetScene();
                    break;
                default:
                    break;
            }

        }

        private void menuItem_deleteObject_Click(object sender, EventArgs e)
        {
            if (EntityToolbar.entitiesList.SelectedIndex > 0)
            {
                switch(LoadedRSDKver)
                {
                    case 0:
                        EntityToolbar.EntitiesvB.RemoveAt(EntityToolbar.entitiesList.SelectedIndex);
                        _mapViewer.DrawScene();
                        break;
                    case 1:
                        EntityToolbar.Entitiesv2.RemoveAt(EntityToolbar.entitiesList.SelectedIndex);
                        _mapViewer.DrawScene();
                        break;
                    case 2:
                        EntityToolbar.Entitiesv1.RemoveAt(EntityToolbar.entitiesList.SelectedIndex);
                        _mapViewer.DrawScene();
                        break;
                    case 3:
                        EntityToolbar.EntitiesvRS.RemoveAt(EntityToolbar.entitiesList.SelectedIndex);
                        _mapViewer.DrawScene();
                        break;
                }
            }
        }
    }
}
