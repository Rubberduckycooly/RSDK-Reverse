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
        private StageChunksView _blocksViewer;

        //The Map Viewer
        private StageMapView _mapViewer;

        public RetroED.MainForm Parent;

        //Stack<UndoAction> UndoList;
        //Stack<UndoAction> RedoList;

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

        //Why is Retro-Sonic so bad lmao
        public RSDKvRS.ZoneList _RStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _CStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _SStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList _BStagesvRS = new RSDKvRS.ZoneList();

        public RSDKv1.GameConfig _GameConfigv1;
        public RSDKv2.GameConfig _GameConfigv2;
        public RSDKvB.GameConfig _GameConfigvB;

        #region Retro-Sonic Development Kit
        RSDKvRS.Scene _RSDK1Scene;
        RSDKvRS.Tiles128x128 _RSDK1Chunks;
        RSDKvRS.Tileconfig _RSDK1CollisionMask;
        #endregion

        #region RSDKv1
        RSDKv1.Scene _RSDK2Scene;
        RSDKv1.Tiles128x128 _RSDK2Chunks;
        RSDKv1.Tileconfig _RSDK2CollisionMask;
        #endregion

        #region RSDKv1
        RSDKv2.Scene _RSDK3Scene;
        RSDKv2.Tiles128x128 _RSDK3Chunks;
        RSDKv2.Tileconfig _RSDK3CollisionMask;
        #endregion

        #region RSDKvB
        RSDKvB.Scene _RSDK4Scene;
        RSDKvB.Tiles128x128 _RSDK4Chunks;
        RSDKvB.Tileconfig _RSDK4CollisionMask;
        #endregion

        public MainView()
        {
            InitializeComponent();
            _mapViewer = new StageMapView();
            _mapViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            _blocksViewer = new StageChunksView(_mapViewer);
            _blocksViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            _mapViewer._ChunkView = _blocksViewer;
        }

        void LoadScene(string Scene, int RSDKver)
        {
            //Clears the map
            _mapViewer.DrawScene();
            switch (RSDKver)
            {
                case 0:
                    using (Stream strm = File.OpenRead(Scene))
                    {
                        _RSDK4Scene = new RSDKvB.Scene(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK4Chunks = new RSDKvB.Tiles128x128(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(CollisionMasks))
                    {
                        _RSDK4CollisionMask = new RSDKvB.Tileconfig(strm);
                        strm.Close();
                    }
                    _loadedTiles = (Bitmap)Image.FromFile(tiles).Clone();
                    _blocksViewer._RSDK4Chunks = _RSDK4Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV4 = _RSDK4Scene.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK4Scene = _RSDK4Scene;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
                    _mapViewer._RSDK4CollisionMask = _RSDK4CollisionMask;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetScene();
                    break;
                case 1:
                    using (Stream strm = File.OpenRead(Scene))
                    {
                        _RSDK3Scene = new RSDKv2.Scene(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK3Chunks = new RSDKv2.Tiles128x128(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(CollisionMasks))
                    {
                        _RSDK3CollisionMask = new RSDKv2.Tileconfig(strm);
                        strm.Close();
                    }
                    _loadedTiles = (Bitmap)Image.FromFile(tiles).Clone();
                    _blocksViewer._RSDK3Chunks = _RSDK3Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV3 = _RSDK3Scene.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK3Scene = _RSDK3Scene;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
                    _mapViewer._RSDK3CollisionMask = _RSDK3CollisionMask;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetScene();
                    break;
                case 2:
                    using (Stream strm = File.OpenRead(Scene))
                    {
                        _RSDK2Scene = new RSDKv1.Scene(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK2Chunks = new RSDKv1.Tiles128x128(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(CollisionMasks))
                    {
                        _RSDK2CollisionMask = new RSDKv1.Tileconfig(strm);
                        strm.Close();
                    }
                    _loadedTiles = (Bitmap)Image.FromFile(tiles).Clone();
                    _blocksViewer._RSDK2Chunks = _RSDK2Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV2 = _RSDK2Scene.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK2Scene = _RSDK2Scene;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
                    _mapViewer._RSDK2CollisionMask = _RSDK2CollisionMask;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetScene();
                    break;
                case 3:
                    using (Stream strm = File.OpenRead(Scene))
                    {
                        _RSDK1Scene = new RSDKvRS.Scene(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK1Chunks = new RSDKvRS.Tiles128x128(strm);
                        strm.Close();
                    }
                    using (Stream strm = File.OpenRead(CollisionMasks))
                    {
                        _RSDK1CollisionMask = new RSDKvRS.Tileconfig(strm,false);
                        strm.Close();
                    }

                    RSDKvRS.gfx gfx = new RSDKvRS.gfx(tiles, false);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage.Clone(new Rectangle(0,0,gfx.gfxImage.Width, gfx.gfxImage.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    _blocksViewer._RSDK1Chunks = _RSDK1Chunks;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV1 = _RSDK1Scene.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer._tiles = gfx.gfxImage.Clone(new Rectangle(0, 0, gfx.gfxImage.Width, gfx.gfxImage.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                    _mapViewer._RSDK1CollisionMask = _RSDK1CollisionMask;
                    _mapViewer._RSDK1Scene = _RSDK1Scene;
                    _mapViewer._RSDK1Chunks = _RSDK1Chunks;
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
                PlaceObjectButton.BackColor = SystemColors.Control;
                PlaceObjectButton.Checked = false;
            }
            else if (PlacementMode == 1) //Change what Placement mode is activated
            {
                PlacementMode = (int)Placementmode.NONE;
                PlaceTileButton.Checked = false;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceTileButton.BackColor = SystemColors.Control;
                PlaceObjectButton.BackColor = SystemColors.Control;
                PlaceObjectButton.Checked = false;
            }
        }

        private void PlaceObject_Click(object sender, EventArgs e)
        {
            if (PlacementMode != 2) //Change what Placement mode is activated
            {
                PlacementMode = (int)Placementmode.PlaceObjects;
                PlaceObjectButton.Checked = true;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceObjectButton.BackColor = SystemColors.ControlDarkDark;
                PlaceTileButton.BackColor = SystemColors.Control;
                PlaceTileButton.Checked = false;
            }
            else if (PlacementMode == 2) //Change what Placement mode is activated
            {
                PlacementMode = (int)Placementmode.NONE;
                PlaceObjectButton.Checked = false;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceObjectButton.BackColor = SystemColors.Control;
                PlaceTileButton.BackColor = SystemColors.Control;
                PlaceTileButton.Checked = false;
            }
        }

        private void mapPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        void CheckDimensions(int RSDKver, ushort[][] OldTiles, ushort[][] NewTiles, int OLDwidth, int OLDheight) //Check to see if the map is a different size to another map/an older version of this map
        {

            if (RSDKver == 3)
            {
                if (_RSDK1Scene.width != OLDwidth || _RSDK1Scene.height != OLDheight) //Well, Is it different?
                {
                    Console.WriteLine("Different"); //It is!
                    //Update the map!
                    _RSDK1Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDK1Scene.width, (ushort)_RSDK1Scene.height, RSDKver);
                    _mapViewer.DrawScene(); //Draw the map
                }
            }
            if (RSDKver == 2)
            {
                if (_RSDK2Scene.width != OLDwidth || _RSDK2Scene.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK2Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK2Scene.width, (ushort)_RSDK2Scene.height, RSDKver);
                    _mapViewer.DrawScene();
                }

            }
            if (RSDKver == 1)
            {
                if (_RSDK3Scene.width != OLDwidth || _RSDK3Scene.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK3Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK3Scene.width, (ushort)_RSDK3Scene.height, RSDKver);
                    _mapViewer.DrawScene();
                }
            }
            if (RSDKver == 0)
            {
                if (_RSDK4Scene.width != OLDwidth || _RSDK4Scene.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK4Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK4Scene.width, (ushort)_RSDK4Scene.height, RSDKver);
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

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void refreshChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void clearChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void clearObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(LoadedRSDKver)
            {
                case 3:
                    _mapViewer._RSDK1Scene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
                case 2:
                    _mapViewer._RSDK2Scene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
                case 1:
                    _mapViewer._RSDK3Scene.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawScene(); //Let's redraw the Scene
                    break;
                case 0:
                    _mapViewer._RSDK4Scene.objects.Clear(); //Clear the Object list (Delete all objects)
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
                    _GameConfigvB = new RSDKvB.GameConfig(Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
                    break;
                case 1:
                    _GameConfigv2 = new RSDKv2.GameConfig(Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
                    break;
                case 2:
                    _GameConfigv1 = new RSDKv1.GameConfig(Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
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
                { ResetDataDirectoryToAndResetScene(newDataDirectory); DataDirLoaded = true; }
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
                    RetroED.Extensions.DataSelect.SceneSelect dlgB = new RetroED.Extensions.DataSelect.SceneSelect(_GameConfigvB);

                    dlgB.ShowDialog();
                    if (dlgB.Result != null)
                    {
                        string SelectedScene = Path.GetFileName(dlgB.Result);
                        string SelectedZone = dlgB.Result.Replace(SelectedScene, "");

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
                    RetroED.Extensions.DataSelect.SceneSelect dlg2 = new RetroED.Extensions.DataSelect.SceneSelect(_GameConfigv2);

                    dlg2.ShowDialog();
                    if (dlg2.Result != null)
                    {
                        string SelectedScene = Path.GetFileName(dlg2.Result);
                        string SelectedZone = dlg2.Result.Replace(SelectedScene, "");

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
                    RetroED.Extensions.DataSelect.SceneSelect dlg1 = new RetroED.Extensions.DataSelect.SceneSelect(_GameConfigv1);

                    dlg1.ShowDialog();
                    if (dlg1.Result != null)
                    {
                        string SelectedScene = Path.GetFileName(dlg1.Result);
                        string SelectedZone = dlg1.Result.Replace(SelectedScene, "");

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
                    if (dlgRS.Result != null)
                    {
                        string SelectedScene = Path.GetFileName(dlgRS.Result);
                        string SelectedZone = dlgRS.Result.Replace(SelectedScene, "");

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
                if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(CollisionMasks))
                {
                    LoadScene(folderpath, LoadedRSDKver);
                    LoadObjectsFromDataFolder(DataDirectory);
                    Parent.rp.state = "RetroED - " + this.Text;
                    switch (LoadedRSDKver)
                    {
                        case 0:
                            Parent.rp.details = "Editing: " + _RSDK4Scene.Title;
                            break;
                        case 1:
                            Parent.rp.details = "Editing: " + _RSDK3Scene.Title;
                            break;
                        case 2:
                            Parent.rp.details = "Editing: " + _RSDK2Scene.Title;
                            break;
                        case 3:
                            Parent.rp.details = "Editing: " + _RSDK1Scene.Title;
                            break;
                    }
                    SharpPresence.Discord.RunCallbacks();
                    SharpPresence.Discord.UpdatePresence(Parent.rp);
                }
                else
                {
                    MessageBox.Show("Tiles, Mappings and Collision Masks need to exist in the same folder as act data, just like the game.");
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
                if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(CollisionMasks))
                {
                    LoadScene(folderpath, LoadedRSDKver);
                    LoadObjectsFromDataFolder(DataDirectory);
                    Parent.rp.state = "RetroED - " + this.Text;
                    switch (LoadedRSDKver)
                    {
                        case 0:
                            Parent.rp.details = "Editing: " + _RSDK4Scene.Title;
                            break;
                        case 1:
                            Parent.rp.details = "Editing: " + _RSDK3Scene.Title;
                            break;
                        case 2:
                            Parent.rp.details = "Editing: " + _RSDK2Scene.Title;
                            break;
                        case 3:
                            Parent.rp.details = "Editing: " + _RSDK1Scene.Title;
                            break;
                    }
                    SharpPresence.Discord.RunCallbacks();
                    SharpPresence.Discord.UpdatePresence(Parent.rp);
                }
                else
                {
                    MessageBox.Show("Tiles, Mappings and Collision Masks need to exist in the same folder as act data, just like the game.");
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
                        _mapViewer._RSDK4Scene.Write(Map);
                        break;
                    case 1:
                        _mapViewer._RSDK3Scene.Write(Map);
                        break;
                    case 2:
                        _mapViewer._RSDK2Scene.Write(Map);
                        break;
                    case 3:
                        _mapViewer._RSDK1Scene.Write(Map);
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
                        _mapViewer._RSDK4Scene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    case 1:
                        _mapViewer._RSDK3Scene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    case 2:
                        _mapViewer._RSDK2Scene.Write(dlg.FileName);
                        Map = dlg.FileName;
                        break;
                    case 3:
                        _mapViewer._RSDK1Scene.Write(dlg.FileName);
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
                    if (_RSDK4Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK4Scene.MapLayout[0].Length * 128, _RSDK4Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK4Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK4Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK4Chunks.BlockList[_RSDK4Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK4Scene.objects.Count; o++)
                                {
                                        Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK4Scene.objects[o].type, _RSDK4Scene.objects[o].subtype);
                                        if (mapobj != null && mapobj.ID > 0)
                                        {
                                            g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK4Scene.objects[o].xPos, _RSDK4Scene.objects[o].yPos);
                                        }
                                        else
                                        {
                                            g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK4Scene.objects[o].xPos, _RSDK4Scene.objects[o].yPos);
                                        }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK3Scene.MapLayout[0].Length * 128, _RSDK3Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK3Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK3Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK3Chunks.BlockList[_RSDK3Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK3Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK3Scene.objects[o].type, _RSDK3Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK3Scene.objects[o].xPos, _RSDK3Scene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK3Scene.objects[o].xPos, _RSDK3Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
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
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK2Scene.objects[o].type, _RSDK2Scene.objects[o].subtype);
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
                case 3:
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
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK1Scene.objects[o].type, _RSDK1Scene.objects[o].subtype);
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
                default:
                    break;
            }
        }

        private void MenuItem_ExportMapImage_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)  //Find out what RSDK version is loaded and then write the map data to the selected image location
            {
                case 0:
                    if (_RSDK4Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK4Scene.MapLayout[0].Length * 128, _RSDK4Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK4Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK4Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK4Chunks.BlockList[_RSDK4Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK3Scene.MapLayout[0].Length * 128, _RSDK3Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK3Scene.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK3Scene.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK3Chunks.BlockList[_RSDK3Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
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
                case 3:
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
                default:
                    break;
            }
        }

        private void MenuItem_ExportObjImage_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)  //Find out what RSDK version is loaded and then write the map data to the selected image location
            {
                case 0:
                    if (_RSDK4Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK4Scene.MapLayout[0].Length * 128, _RSDK4Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK4Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK4Scene.objects[o].type, _RSDK4Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK4Scene.objects[o].xPos, _RSDK4Scene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK4Scene.objects[o].xPos, _RSDK4Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Scene != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK3Scene.MapLayout[0].Length * 128, _RSDK3Scene.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK3Scene.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK3Scene.objects[o].type, _RSDK3Scene.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK3Scene.objects[o].xPos, _RSDK3Scene.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK3Scene.objects[o].xPos, _RSDK3Scene.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
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
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK2Scene.objects[o].type, _RSDK2Scene.objects[o].subtype);
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
                case 3:
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
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK1Scene.objects[o].type, _RSDK1Scene.objects[o].subtype);
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
                    ushort[][] NewChunks1 = new ushort[_RSDK1Scene.height][];
                    for (ushort i = 0; i < _RSDK1Scene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewChunks1[i] = new ushort[_RSDK1Scene.width];
                        for (int j = 0; j < _RSDK1Scene.width; ++j)
                            NewChunks1[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK1Scene.MapLayout = NewChunks1;
                    _mapViewer.DrawScene();
                    break;
                case 2:
                    ushort[][] NewTiles2 = new ushort[_RSDK2Scene.height][];
                    for (ushort i = 0; i < _RSDK2Scene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles2[i] = new ushort[_RSDK2Scene.width];
                        for (int j = 0; j < _RSDK2Scene.width; ++j)
                            NewTiles2[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK2Scene.MapLayout = NewTiles2;

                    _mapViewer.DrawScene();
                    break;
                case 1:
                    ushort[][] NewTiles3 = new ushort[_RSDK3Scene.height][];
                    for (ushort i = 0; i < _RSDK3Scene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles3[i] = new ushort[_RSDK3Scene.width];
                        for (int j = 0; j < _RSDK3Scene.width; ++j)
                            NewTiles3[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK3Scene.MapLayout = NewTiles3;
                    _mapViewer.DrawScene();
                    break;
                case 0:
                    ushort[][] NewTiles4 = new ushort[_RSDK4Scene.height][];
                    for (ushort i = 0; i < _RSDK4Scene.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles4[i] = new ushort[_RSDK4Scene.width];
                        for (int j = 0; j < _RSDK4Scene.width; ++j)
                            NewTiles4[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK4Scene.MapLayout = NewTiles4;
                    _mapViewer.DrawScene();
                    break;
            }
        }

        private void MenuItem_ClearObjects_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Find out what RSDK version is loaded and clear the object list of objects
            {
                case 3:
                    _mapViewer._RSDK1Scene.objects.Clear();
                    _mapViewer.DrawScene();
                    _blocksViewer.RefreshObjList();
                    break;
                case 2:
                    _mapViewer._RSDK2Scene.objects.Clear();
                    _mapViewer.DrawScene();
                    _blocksViewer.RefreshObjList();
                    break;
                case 1:
                    _mapViewer._RSDK3Scene.objects.Clear();
                    _mapViewer.DrawScene();
                    _blocksViewer.RefreshObjList();
                    break;
                case 0:
                    _mapViewer._RSDK4Scene.objects.Clear();
                    _mapViewer.DrawScene();
                    _blocksViewer.RefreshObjList();
                    break;
            }
        }

        private void MenuItem_AddObject_Click(object sender, EventArgs e)
        {
            NewObjectForm frm = new NewObjectForm(LoadedRSDKver,0);

            switch (LoadedRSDKver) //Add an object to the object list using the values the user has given
            {
                case 3:
                    frm.RSObjects = _mapViewer.RSObjects;
                    break;
                case 2:
                    frm.NexusObjects = _mapViewer.NexusObjects;
                    break;
                case 1:
                    frm.CDObjects = _mapViewer.CDObjects;
                    break;
                case 0:
                    frm.S1Objects = _mapViewer.S1Objects;
                    break;
            }
            frm.SetupObjects();

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                switch (LoadedRSDKver) //Add an object to the object list using the values the user has given
                {
                    case 3:
                        RSDKvRS.Object Obj1 = new RSDKvRS.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK1Scene.objects.Add(Obj1);
                        break;
                    case 2:
                        RSDKv1.Object Obj2 = new RSDKv1.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK2Scene.objects.Add(Obj2);
                        break;
                    case 1:
                        RSDKv2.Object Obj3 = new RSDKv2.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK3Scene.objects.Add(Obj3);
                        break;
                    case 0:
                        RSDKvB.Object Obj4 = new RSDKvB.Object((byte)frm.Type, (byte)frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK4Scene.objects.Add(Obj4);
                        break;
                }
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
                    OldTiles = _RSDK1Scene.MapLayout;
                    OLDwidth = _RSDK1Scene.width;
                    OLDheight = _RSDK1Scene.height;
                    //Set the form data to the map data
                    frm.Mapv1 = _RSDK1Scene;
                    frm.Setup();
                    break;
                case 2:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK2Scene.MapLayout;
                    OLDwidth = _RSDK2Scene.width;
                    OLDheight = _RSDK2Scene.height;
                    //Set the form data to the map data
                    frm.Mapv2 = _RSDK2Scene;
                    frm.Setup();
                    break;
                case 1:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK3Scene.MapLayout;
                    OLDwidth = _RSDK3Scene.width;
                    OLDheight = _RSDK3Scene.height;
                    //Set the form data to the map data
                    frm.Mapv3 = _RSDK3Scene;
                    frm.Setup();
                    break;
                case 0:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK4Scene.MapLayout;
                    OLDwidth = _RSDK4Scene.width;
                    OLDheight = _RSDK4Scene.height;
                    //Set the form data to the map data
                    frm.Mapv4 = _RSDK4Scene;
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
                        OldTiles = _RSDK1Scene.MapLayout; //Get Old Chunks
                        _RSDK1Scene = frm.Mapv1; //Set the map data to the updated data
                        NewTiles = _RSDK1Scene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    case 2:
                        OldTiles = _RSDK2Scene.MapLayout; //Get Old Chunks
                        _RSDK2Scene = frm.Mapv2; //Set the map data to the updated data
                        NewTiles = _RSDK2Scene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    case 1:
                        OldTiles = _RSDK3Scene.MapLayout; //Get Old Chunks
                        _RSDK3Scene = frm.Mapv3; //Set the map data to the updated data
                        NewTiles = _RSDK3Scene.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawScene();
                        break;
                    case 0:
                        OldTiles = _RSDK4Scene.MapLayout; //Get Old Chunks
                        _RSDK4Scene = frm.Mapv4; //Set the map data to the updated data
                        NewTiles = _RSDK4Scene.MapLayout; //Get the updated Chunks
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

        private void MenuItem_RefreshChunks_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK4Chunks = new RSDKvB.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK4Chunks = _RSDK4Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK4Scene = _RSDK4Scene;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
                    _mapViewer._RSDK4CollisionMask = _RSDK4CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetScene();
                    break;
                case 1:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK3Chunks = new RSDKv2.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK3Chunks = _RSDK3Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK3Scene = _RSDK3Scene;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
                    _mapViewer._RSDK3CollisionMask = _RSDK3CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetScene();
                    break;
                case 2:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK2Chunks = new RSDKv1.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK2Chunks = _RSDK2Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK2Scene = _RSDK2Scene;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
                    _mapViewer._RSDK2CollisionMask = _RSDK2CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetScene();
                    break;
                case 3:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK1Chunks = new RSDKvRS.Tiles128x128(strm);
                    }
                    RSDKvRS.gfx gfx = new RSDKvRS.gfx(tiles, false);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage;
                    _blocksViewer._RSDK1Chunks = _RSDK1Chunks;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK1Scene = _RSDK1Scene;
                    _mapViewer._RSDK1Chunks = _RSDK1Chunks;
                    _mapViewer._RSDK1CollisionMask = _RSDK1CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetScene();
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_ShowGrid_Click(object sender, EventArgs e)
        {
            showgrid = _mapViewer.ShowGrid = MenuItem_ShowGrid.Checked = !showgrid; //Do we want a grid overlayed on our map?
            _mapViewer.DrawScene();
        }

        private void collisionMasksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(); //Show the about page
        }

        private void MenuItem_AddObjList_Click(object sender, EventArgs e)
        {
            if (_mapViewer.datapath == null)
            {
                FolderBrowserDialog fdlg = new FolderBrowserDialog();
                fdlg.Description = "Select Data Folder";

                if (fdlg.ShowDialog(this) == DialogResult.OK)
                {
                    _mapViewer.datapath = fdlg.SelectedPath + "\\";
                    LoadedObjDefinitions = true;
                }
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Object Definition Lists (*.txt)|*.txt";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (LoadedRSDKver)
                {
                    case 0:
                        _mapViewer.S1Objects.LoadObjList(dlg.FileName); //Load the object definitions into memory
                        _blocksViewer.RefreshObjList();
                        break;
                    case 1:
                        _mapViewer.CDObjects.LoadObjList(dlg.FileName); //Load the object definitions into memory
                        _blocksViewer.RefreshObjList();
                        break;
                    case 2:
                        _mapViewer.NexusObjects.LoadObjList(dlg.FileName); //Load the object definitions into memory
                        _blocksViewer.RefreshObjList();
                        break;
                    case 3:
                        _mapViewer.RSObjects.LoadObjList(dlg.FileName); //Load the object definitions into memory
                        _blocksViewer.RefreshObjList();
                        break;
                    default:
                        break;
                }
            }
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
                    RSDKvB.GameConfig gcB = new RSDKvB.GameConfig(datapath + "//Game//Gameconfig.bin");
                    _mapViewer.S1Objects.Objects.Clear();
                    _mapViewer.S1Objects.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));
                    for (int i = 0; i < gcB.ObjectsNames.Count; i++)
                    {
                        _mapViewer.S1Objects.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(gcB.ObjectsNames[i], (i + 1), 0, "", 0, 0, 0, 0));
                    }
                    RSDKvB.StageConfig scB = new RSDKvB.StageConfig(Stageconfig);
                    for (int i = gcB.ObjectsNames.Count; i < scB.ObjectsNames.Count + gcB.ObjectsNames.Count; i++)
                    {
                        _mapViewer.S1Objects.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(scB.ObjectsNames[i - gcB.ObjectsNames.Count], (i + 1), 0, "", 0, 0, 0, 0));
                    }
                    break;
                case 1:
                    RSDKv2.GameConfig gc2 = new RSDKv2.GameConfig(datapath + "//Game//Gameconfig.bin");
                    _mapViewer.CDObjects.Objects.Clear();
                    _mapViewer.CDObjects.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));
                    for (int i = 0; i < gc2.ObjectsNames.Count; i++)
                    {
                        _mapViewer.CDObjects.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(gc2.ObjectsNames[i], (i + 1), 0, "", 0, 0, 0, 0));
                    }
                    RSDKv2.StageConfig sc2 = new RSDKv2.StageConfig(Stageconfig);
                    for (int i = gc2.ObjectsNames.Count; i < sc2.ObjectsNames.Count + gc2.ObjectsNames.Count; i++)
                    {
                        _mapViewer.CDObjects.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(sc2.ObjectsNames[i - gc2.ObjectsNames.Count], (i + 1), 0, "", 0, 0, 0, 0));
                    }
                    break;
                case 2:
                    RSDKv1.GameConfig gc1 = new RSDKv1.GameConfig(datapath + "//Game//Gameconfig.bin");
                    _mapViewer.NexusObjects.Objects.Clear();
                    _mapViewer.NexusObjects.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));
                    for (int i = 0; i < gc1.ScriptFilepaths.Count; i++)
                    {
                        _mapViewer.NexusObjects.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(gc1.ScriptFilepaths[i]), (i + 1), 0, "", 0, 0, 0, 0));
                    }
                    RSDKv1.StageConfig sc1 = new RSDKv1.StageConfig(Stageconfig);
                    for (int i = gc1.ScriptFilepaths.Count; i < sc1.ObjectsNames.Count + gc1.ScriptFilepaths.Count; i++)
                    {
                        _mapViewer.NexusObjects.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(sc1.ObjectsNames[i - gc1.ScriptFilepaths.Count]), (i + 1), 0, "", 0, 0, 0, 0));
                    }
                    break;
                case 3:
                    Console.WriteLine("the Retro-Sonic Engine doesn't have global objects in a file lol");

                    RSDKvRS.Zoneconfig scRS = new RSDKvRS.Zoneconfig(Stageconfig);
                    for (int i = 30; i < scRS.Objects.Count + 30; i++)
                    {
                        //_mapViewer.RSObjects.Objects.Add(new Point((i), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(scRS.Objects[i - 30].FilePath), (i + 1), 0, "", 0, 0, 0, 0));
                    }

                    break;
            }
        }

    }

}
