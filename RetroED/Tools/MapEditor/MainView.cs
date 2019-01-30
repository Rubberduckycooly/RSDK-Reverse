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
        public Retro_Formats.EngineType engineType;

        //Stage's Tileset
        public Image _loadedTiles;

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
        string chunks;
        string scene;
        string collisionMasks;
        string stageconfig;

        bool showgrid = false;
        int PlacementMode = 0;

        public string FilePath;

        public string DataDirectory = null;
        public bool DataDirLoaded = false;

        new RetroED.Extensions.EntityToolbar.EntityToolbar EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar();

        public RSDKvRS.ZoneList RStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList CStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList SStagesvRS = new RSDKvRS.ZoneList();
        public RSDKvRS.ZoneList BStagesvRS = new RSDKvRS.ZoneList();

        public Retro_Formats.Gameconfig Gameconfig = new Retro_Formats.Gameconfig();
        public Retro_Formats.Scene Scene = new Retro_Formats.Scene();
        public Retro_Formats.MetaTiles Chunks = new Retro_Formats.MetaTiles();
        public RSDKvB.Tileconfig Tileconfig = new RSDKvB.Tileconfig();
        public Retro_Formats.Stageconfig Stageconfig = new Retro_Formats.Stageconfig();

        List<string> ObjectNames = new List<string>();
        List<Retro_Formats.Object> ObjectTypes = new List<Retro_Formats.Object>();

        public MainView()
        {
            InitializeComponent();
            _mapViewer = new StageMapView(this);
            _mapViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            _blocksViewer = new StageChunksView(this);
            _blocksViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar();
            EntityToolbar.Dock = DockStyle.Fill;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
        }

        void LoadScene(string ScenePath)
        {
            //Clears the map
            _mapViewer.DrawScene();
            Scene.ImportFrom(engineType, ScenePath);
            Chunks.ImportFrom(engineType, chunks);
            Tileconfig = new RSDKvB.Tileconfig(collisionMasks);
            Stageconfig.ImportFrom(engineType, stageconfig);

            using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
            {
                var bmp = new Bitmap(fs);
                _loadedTiles = (Bitmap)bmp.Clone();
            }
            _blocksViewer.SetChunks();

            _mapViewer.SetScene();
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

        void CheckDimensions(ushort[][] OldTiles, ushort[][] NewTiles, int OLDwidth, int OLDheight) //Check to see if the map is a different size to another map/an older version of this map
        {
            if (Scene.width != OLDwidth || Scene.height != OLDheight)
            {
                Console.WriteLine("Different");
                Scene.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)Scene.width, (ushort)Scene.height);
                _mapViewer.DrawScene();
            }
        }

        ushort[][] UpdateMapDimensions(ushort[][] OldTiles, ushort[][] NewTiles, ushort oldWidth, ushort oldHeight, ushort NewWidth, ushort NewHeight)
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
            switch (MessageBox.Show(this, "WARNING: you are going to remove ALL objects in the stage! THIS ACTION CANNOT BE UNDONE! Continue?", "RetroED Map Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return;
                case System.Windows.Forms.DialogResult.Yes:
                    break;
            }
            Scene.objects.Clear(); //Clear the Object list (Delete all objects)
            _mapViewer.DrawScene(); //Let's redraw the Scene
        }

        /// <summary>
        /// Sets the GameConfig property in relation to the DataDirectory property.
        /// </summary>
        private void SetGameConfig()
        {
            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS:
                    RStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "Zones.mdf"));
                    CStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "CZones.mdf"));
                    SStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "SStages.mdf"));
                    BStagesvRS = new RSDKvRS.ZoneList(Path.Combine(DataDirectory, "TitleScr", "BStages.mdf"));
                    break;
                default:
                    Gameconfig.ImportFrom(engineType, Path.Combine(DataDirectory, "Game", "GameConfig.bin"));
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
            switch(engineType)
            {
                default:
                    exists = File.Exists(Path.Combine(directoryToCheck, "Game", "GameConfig.bin"));
                    break;
                case Retro_Formats.EngineType.RSDKvRS:
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

            switch(engineType)
            {
                default:
                    RetroED.Extensions.DataSelect.SceneSelect dlg = new RetroED.Extensions.DataSelect.SceneSelect(Gameconfig);

                    dlg.ShowDialog();
                    if (dlg.Result.FilePath != null)
                    {
                        string SelectedScene = Path.GetFileName(dlg.Result.FilePath);
                        string SelectedZone = dlg.Result.FilePath.Replace(SelectedScene, "");

                        categoryID = dlg.Result.Category;

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
                case Retro_Formats.EngineType.RSDKvRS:
                    RetroED.Extensions.DataSelect.SceneSelect dlgRS = new RetroED.Extensions.DataSelect.SceneSelect(RStagesvRS, CStagesvRS, SStagesvRS, BStagesvRS);

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
                DataDirectory = null;
                DataDirLoaded = false;
                engineType = dlg.engineType;
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
                engineType = ofd.FilterIndex - 1;


        }*/
        }

        public void Open(string folderpath)
        {
            if (engineType == Retro_Formats.EngineType.RSDKvRS) //Are We displaying a Retro-Sonic Map?
            {
                //Load the files needed to show the map
                tiles = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.gfx");
                chunks = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.til");
                scene = folderpath;
                collisionMasks = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.tcf");
                stageconfig = Path.Combine(Path.GetDirectoryName(folderpath), "Zone.zcf");
                if (File.Exists(tiles) && File.Exists(chunks) && File.Exists(collisionMasks) && File.Exists(stageconfig))
                {
                    LoadScene(folderpath);
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
                    switch (engineType)
                    {
                        case Retro_Formats.EngineType.RSDKvB:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKvB)";
                            break;
                        case Retro_Formats.EngineType.RSDKv2:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv2)";
                            break;
                        case Retro_Formats.EngineType.RSDKv1:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv1)";
                            break;
                        case Retro_Formats.EngineType.RSDKvRS:
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
            else //Are we NOT displaying a Retro-Sonic Map?
            {
                //Load Map Data
                tiles = Path.Combine(Path.GetDirectoryName(folderpath), "16x16Tiles.gif");
                chunks = Path.Combine(Path.GetDirectoryName(folderpath), "128x128Tiles.bin");
                scene = folderpath;
                collisionMasks = Path.Combine(Path.GetDirectoryName(folderpath), "collisionMasks.bin");
                stageconfig = Path.Combine(Path.GetDirectoryName(folderpath), "Stageconfig.bin");
                if (File.Exists(tiles) && File.Exists(chunks) && File.Exists(collisionMasks) && File.Exists(stageconfig))
                {
                    LoadScene(folderpath);
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
                    switch (engineType)
                    {
                        case Retro_Formats.EngineType.RSDKvB:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKvB)";
                            break;
                        case Retro_Formats.EngineType.RSDKv2:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv2)";
                            break;
                        case Retro_Formats.EngineType.RSDKv1:
                            Parent.rp.details = "Editing: " + dispname + " (RSDKv1)";
                            break;
                        case Retro_Formats.EngineType.RSDKvRS:
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
            if (scene == null) //Do we have a map file open?
            {
                saveAsToolStripMenuItem_Click(this, e); //If not, then let the user make one
            }
            else
            {
                Scene.ExportTo(engineType, scene);
            }
        }

        private void MenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Sonic 1/Sonic 2 Act#.bin files (Act*.bin)|Act*.bin|Sonic CD Act#.bin files (Act*.bin)|Act*.bin|Sonic Nexus Act#.bin files (Act*.bin)|Act*.bin|Retro-Sonic Act#.map files (Act*.map)|Act*.map";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (engineType) //Find out what RSDK version is loaded and then write the map data to the selected file
                {
                    case Retro_Formats.EngineType.RSDKvB:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                    case Retro_Formats.EngineType.RSDKv2:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case Retro_Formats.EngineType.RSDKv1:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case Retro_Formats.EngineType.RSDKvRS:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                }
                scene = dlg.FileName;
                Scene.ExportTo(engineType, scene);
            }
        }

        private void MenuItem_ExportFullImage_Click(object sender, EventArgs e)
        {
            if (Scene != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image (*.png)|*.png";
                if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                {
                    Bitmap massive = new Bitmap(Scene.MapLayout[0].Length * 128, Scene.MapLayout.Length * 128);
                    using (Graphics g = Graphics.FromImage(massive))
                    {
                        for (int y = 0; y < Scene.MapLayout.Length; y++)
                        {
                            for (int x = 0; x < Scene.MapLayout[0].Length; x++)
                            {
                                g.DrawImage(Chunks.ChunkList[Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                            }
                        }
                        for (int o = 0; o < Scene.objects.Count; o++)
                        {
                            Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(Scene.objects[o].type, Scene.objects[o].subtype);
                            if (mapobj != null && mapobj.ID > 0)
                            {
                                g.DrawImageUnscaled(mapobj.RenderObject(engineType, _mapViewer.datapath), Scene.objects[o].xPos + mapobj.PivotX, Scene.objects[o].yPos + mapobj.PivotY);
                            }
                            else
                            {
                                g.DrawImage(RetroED.Properties.Resources.OBJ, Scene.objects[o].xPos, Scene.objects[o].yPos);
                            }
                        }
                    }
                    massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    massive.Dispose();
                }
            }
        }

        private void MenuItem_ExportMapImage_Click(object sender, EventArgs e)
        {
            if (Scene != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image (*.png)|*.png";
                if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                {
                    Bitmap massive = new Bitmap(Scene.MapLayout[0].Length * 128, Scene.MapLayout.Length * 128);
                    using (Graphics g = Graphics.FromImage(massive))
                    {
                        for (int y = 0; y < Scene.MapLayout.Length; y++)
                        {
                            for (int x = 0; x < Scene.MapLayout[0].Length; x++)
                            {
                                g.DrawImage(Chunks.ChunkList[Scene.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                            }
                        }
                    }
                    massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    massive.Dispose();
                }
            }
        }

        private void MenuItem_ExportObjImage_Click(object sender, EventArgs e)
        {
            if (Scene != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image (*.png)|*.png";
                if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                {
                    Bitmap massive = new Bitmap(Scene.MapLayout[0].Length * 128, Scene.MapLayout.Length * 128);
                    using (Graphics g = Graphics.FromImage(massive))
                    {
                        for (int o = 0; o < Scene.objects.Count; o++)
                        {
                            Object_Definitions.MapObject mapobj = _mapViewer.ObjectDefinitions.GetObjectByType(Scene.objects[o].type, Scene.objects[o].subtype);
                            if (mapobj != null && mapobj.ID > 0)
                            {
                                g.DrawImageUnscaled(mapobj.RenderObject(engineType, _mapViewer.datapath), Scene.objects[o].xPos, Scene.objects[o].yPos);
                            }
                            else
                            {
                                g.DrawImage(RetroED.Properties.Resources.OBJ, Scene.objects[o].xPos, Scene.objects[o].yPos);
                            }
                        }
                    }
                    massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    massive.Dispose();
                }
            }
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            Close(); //Close the "Applet"
        }

        private void MenuItem_ClearChunks_Click(object sender, EventArgs e)
        {
            ushort[][] NewTiles = new ushort[Scene.height][];
            for (ushort i = 0; i < Scene.height; i++)
            {
                // first create arrays child arrays to the width
                // a little inefficient, but at least they'll all be equal sized
                NewTiles[i] = new ushort[Scene.width];
                for (int j = 0; j < Scene.width; ++j)
                    NewTiles[i][j] = 0; // fill the chunks with blanks
            }
            Scene.MapLayout = NewTiles;
            _mapViewer.DrawScene();
        }

        private void MenuItem_ClearObjects_Click(object sender, EventArgs e)
        {
            Scene.objects.Clear();
            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypes, this);
            EntityToolbar.engineType = engineType;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entities = Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
            _mapViewer.DrawScene();
        }

        private void MenuItem_MapProp_Click(object sender, EventArgs e)
        {
            PropertiesForm frm = new PropertiesForm(engineType);

            ushort[][] OldTiles;
            ushort[][] NewTiles;
            int OLDwidth = 0;
            int OLDheight = 0;

            //Backup the data, We'll use this later! :)
            OldTiles = Scene.MapLayout;
            OLDwidth = Scene.width;
            OLDheight = Scene.height;
            //Set the form data to the map data
            frm.Scene = Scene;
            frm.Setup();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                OldTiles = Scene.MapLayout; //Get Old Chunks
                Scene = frm.Scene; //Set the map data to the updated data
                NewTiles = Scene.MapLayout; //Get the updated Chunks
                CheckDimensions(OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                _mapViewer.DrawScene();
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

        private void MenuItem_collisionMasks_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowCollisionA = MenuItem_CollisionMasksLyrA.Checked = !_mapViewer.ShowCollisionA; //Are we going to show the Collision Masks for each tile?
            _mapViewer.ShowCollisionB = MenuItem_CollisionMasksLyrB.Checked = false;
            _mapViewer.DrawScene();
        }

        private void MenuItem_collisionMasksLyrB_Click(object sender, EventArgs e)
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
            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvB:
                    LoadRSDKBObjectsFromStageconfig(datapath);
                    break;
                case Retro_Formats.EngineType.RSDKv2:
                    LoadRSDK2ObjectsFromStageconfig(datapath);
                    break;
                case Retro_Formats.EngineType.RSDKv1:
                    LoadRSDK1ObjectsFromStageconfig(datapath);
                    break;
                case Retro_Formats.EngineType.RSDKvRS:
                    LoadRSDKObjectDefinitionsFromStageconfig(datapath);
                    break;
            }
        }

        public void LoadRSDKObjectDefinitionsFromStageconfig(string datapath)
        {
            RSDKvRS.Zoneconfig _RSDKRSZoneconfig = new RSDKvRS.Zoneconfig(stageconfig);
            _mapViewer.ObjectDefinitions.Objects.Clear();

            ObjectNames.Clear();
            ObjectTypes.Clear();

            List<string> Sheets = new List<string>();

            Sheets.Add(datapath + "\\Objects\\Titlecard1.gfx");
            Sheets.Add("???");
            Sheets.Add(datapath + "\\Objects\\Shields.gfx");
            Sheets.Add("???");
            Sheets.Add(datapath + "\\Objects\\General.gfx");
            Sheets.Add(datapath + "\\Objects\\General2.gfx");
            

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("BlankObject", 0, 0, datapath + "Blank Objects Don't need sprites lmao", 0, 0, 0, 0, 0, 0, 0));
            ObjectTypes.Add(new Retro_Formats.Object(0, 0, 0, 0, 0, "BlankObject"));
            ObjectNames.Add("BlankObject");

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(1, 0), new Object_Definitions.MapObject("Ring", 1, 0, datapath + "\\Objects\\General.gfx", 0, 0, 16, 16, -8, -8, 0));
            ObjectNames.Add("Ring");
            ObjectTypes.Add(new Retro_Formats.Object(1, 0, 0, 0, 1, "Ring"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(2, 0), new Object_Definitions.MapObject("DroppedRing", 2, 0, datapath + "//Objects//General.gfx", 0, 0, 16, 16, -8, -8, 0));
            ObjectNames.Add("DroppedRing");
            ObjectTypes.Add(new Retro_Formats.Object(2, 0, 0, 0, 2, "DroppedRing"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(3, 0), new Object_Definitions.MapObject("RingSparkle", 3, 0, datapath + "//Objects//General.gfx", 0, 0, 16, 16, -8, -8, 0));
            ObjectNames.Add("RingSparkle");
            ObjectTypes.Add(new Retro_Formats.Object(3, 0, 0, 0, 3, "RingSparkle"));

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
            ObjectTypes.Add(new Retro_Formats.Object(4, 0, 0, 0, 4, "ItemBox"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(5, 0), new Object_Definitions.MapObject("BrokenItemBox", 5, 0, datapath + "\\Objects\\General.gfx", 24, 0, 30, 32,-15,-16,0));
            ObjectNames.Add("BrokenItemBox");
            ObjectTypes.Add(new Retro_Formats.Object(5, 0, 0, 0, 5, "BrokenItemBox"));

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
            ObjectTypes.Add(new Retro_Formats.Object(6, 0, 0, 0, 6, "YellowSpring"));

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
            ObjectTypes.Add(new Retro_Formats.Object(7, 0, 0, 0, 7, "RedSpring"));

            //TO-DO: MAP THE SPIKE DIRECTIONS
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 0), new Object_Definitions.MapObject("Spikes", 8, 0, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 1), new Object_Definitions.MapObject("Spikes", 8, 1, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 2), new Object_Definitions.MapObject("Spikes", 8, 2, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(8, 3), new Object_Definitions.MapObject("Spikes", 8, 3, datapath + "\\Objects\\General.gfx", 118, 160, 32, 32, -16, -16, 0));
            ObjectNames.Add("Spikes");
            ObjectTypes.Add(new Retro_Formats.Object(8, 0, 0, 0, 8, "Spikes"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(9, 0), new Object_Definitions.MapObject("Checkpoint", 9, 0, datapath + "\\Objects\\General.gfx", 240, 0, 16, 48, -8, -24, 0));
            ObjectNames.Add("Checkpoint");
            ObjectTypes.Add(new Retro_Formats.Object(9, 0, 0, 0, 9, "Checkpoint"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(10, 0), new Object_Definitions.MapObject("UnknownObject(Type10)", 10, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type10)");
            ObjectTypes.Add(new Retro_Formats.Object(10, 0, 0, 0, 10, "UnknownObject(Type10)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(11, 0), new Object_Definitions.MapObject("UnknownObject(Type11)", 11, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type11)");
            ObjectTypes.Add(new Retro_Formats.Object(11, 0, 0, 0, 11, "UnknownObject(Type11)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(12, 0), new Object_Definitions.MapObject("UnknownObject(Type12)", 12, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type12)");
            ObjectTypes.Add(new Retro_Formats.Object(12, 0, 0, 0, 12, "UnknownObject(Type12)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(13, 0), new Object_Definitions.MapObject("UnknownObject(Type13)", 13, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type13)");
            ObjectTypes.Add(new Retro_Formats.Object(13, 0, 0, 0, 13, "UnknownObject(Type13)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(14, 0), new Object_Definitions.MapObject("UnknownObject(Type14)", 14, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type14)");
            ObjectTypes.Add(new Retro_Formats.Object(14, 0, 0, 0, 14, "UnknownObject(Type14)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(15, 0), new Object_Definitions.MapObject("UnknownObject(Type15)", 15, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type15)");
            ObjectTypes.Add(new Retro_Formats.Object(15, 0, 0, 0, 15, "UnknownObject(Type15)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(16, 0), new Object_Definitions.MapObject("UnknownObject(Type16)", 16, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type16)");
            ObjectTypes.Add(new Retro_Formats.Object(16, 0, 0, 0, 16, "UnknownObject(Type16)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(17, 0), new Object_Definitions.MapObject("UnknownObject(Type17)", 17, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type17)");
            ObjectTypes.Add(new Retro_Formats.Object(17, 0, 0, 0, 17, "UnknownObject(Type17)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(18, 0), new Object_Definitions.MapObject("SignPost", 18, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("SignPost");
            ObjectTypes.Add(new Retro_Formats.Object(18, 0, 0, 0, 18, "SignPost"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(19, 0), new Object_Definitions.MapObject("EggPrison", 19, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("EggPrison");
            ObjectTypes.Add(new Retro_Formats.Object(19, 0, 0, 0, 19, "EggPrison"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(20, 0), new Object_Definitions.MapObject("SmallExplosion", 20, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("SmallExplosion");
            ObjectTypes.Add(new Retro_Formats.Object(20, 0, 0, 0, 20, "SmallExplosion"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(21, 0), new Object_Definitions.MapObject("BigExplosion", 21, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("BigExplosion");
            ObjectTypes.Add(new Retro_Formats.Object(21, 0, 0, 0, 21, "BigExplosion"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(22, 0), new Object_Definitions.MapObject("EggPrisonDebris", 22, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("EggPrisonDebris");
            ObjectTypes.Add(new Retro_Formats.Object(22, 0, 0, 0, 22, "EggPrisonDebris"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(23, 0), new Object_Definitions.MapObject("Animal", 23, 0, datapath + "\\Objects\\General2.gfx", 64, 0, 48, 48, -24, -24, 0));
            ObjectNames.Add("Animal");
            ObjectTypes.Add(new Retro_Formats.Object(23, 0, 0, 0, 23, "Animal"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(24, 0), new Object_Definitions.MapObject("UnknownObject(Type24)", 24, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type24)");
            ObjectTypes.Add(new Retro_Formats.Object(24, 0, 0, 0, 24, "UnknownObject(Type24)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(25, 0), new Object_Definitions.MapObject("UnknownObject(Type25)", 25, 0, datapath + "//Sprites//", 0, 0, 0, 0, 0, 0, 0));
            ObjectNames.Add("UnknownObject(Type25)");
            ObjectTypes.Add(new Retro_Formats.Object(25, 0, 0, 0, 25, "UnknownObject(Type25)"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(26, 0), new Object_Definitions.MapObject("BigRing", 26, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("BigRing");
            ObjectTypes.Add(new Retro_Formats.Object(26, 0, 0, 0, 26, "BigRing"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(27, 0), new Object_Definitions.MapObject("WaterSplash", 27, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("WaterSplash");
            ObjectTypes.Add(new Retro_Formats.Object(27, 0, 0, 0, 27, "WaterSplash"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(28, 0), new Object_Definitions.MapObject("AirBubbleSpawner", 28, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("AirBubbleSpawner");
            ObjectTypes.Add(new Retro_Formats.Object(28, 0, 0, 0, 28, "AirBubbleSpawner"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(29, 0), new Object_Definitions.MapObject("SmallAirBubble", 29, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("SmallAirBubble");
            ObjectTypes.Add(new Retro_Formats.Object(29, 0, 0, 0, 29, "SmallAirBubble"));

            _mapViewer.ObjectDefinitions.Objects.Add(new Point(30, 0), new Object_Definitions.MapObject("SmokePuff", 30, 0, datapath + "\\Objects\\General.gfx", 256, 0, 64, 64, -32, -32, 0));
            ObjectNames.Add("SmokePuff");
            ObjectTypes.Add(new Retro_Formats.Object(30, 0, 0, 0, 30, "SmokePuff"));

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
                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 30), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath), (i + 30), 0, "", 0, 0, 0, 0));
                    ObjectNames.Add(Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath));
                    ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(_RSDKRSZoneconfig.Objects[i - 1].FilePath)));
                }
            }
            for (int i = 0; i < Scene.objects.Count; i++)
            {
                Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypes, this);
            EntityToolbar.engineType = engineType;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entities = Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);

        }

        public void LoadRSDK1ObjectsFromStageconfig(string datapath)
        {
            _mapViewer.ObjectDefinitions.Objects.Clear();
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(1, 0), new Object_Definitions.MapObject("Player Spawn", 0, 0, "", 0, 0, 0, 0));

            ObjectNames.Clear();
            ObjectTypes.Clear();

            ObjectNames.Add("BlankObject");
            ObjectTypes.Add(new Retro_Formats.Object(0, 0, 0, 0, 0, "BlankObject"));

            ObjectNames.Add("PlayerSpawn");
            ObjectTypes.Add(new Retro_Formats.Object(1, 0, 0, 0, 1, "PlayerSpawn"));

            if (Stageconfig.LoadGlobalScripts)
            {
                for (int i = 1; i <= Gameconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Gameconfig.ScriptPaths[i - 1].FilePath));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
                    }
                }
                for (int i = 1; i <= Stageconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Stageconfig.ScriptPaths[i - 1].FilePath));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath)));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath)));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath), (i + Gameconfig.ScriptPaths.Count + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath)));
                    }
                }
                for (int i = 0; i < Scene.objects.Count; i++)
                {
                    Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
                }
            }
            else
            {
                for (int i = 1; i <= Stageconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Stageconfig.ScriptPaths[i - 1].FilePath));

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

                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath)));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i +  1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ScriptPaths[i - 1].FilePath)));
                    }
                }
                for (int i = 0; i < Scene.objects.Count; i++)
                {
                    Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
                }
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypes,this);
            EntityToolbar.engineType = engineType;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entities = Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);

        }

        public void LoadRSDK2ObjectsFromStageconfig(string datapath)
        {
            _mapViewer.ObjectDefinitions.Objects.Clear();
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));

            bool UsingBytecode = Directory.Exists(datapath + "//Scripts//Bytecode");

            ObjectNames.Clear();
            ObjectTypes.Clear();

            ObjectNames.Add("BlankObject");

            ObjectTypes.Add(new Retro_Formats.Object(0, 0, 0, 0, 0, "BlankObject"));

            if (Stageconfig.LoadGlobalScripts)
            {
                for (int i = 1; i <= Gameconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Gameconfig.ScriptPaths[i - 1].FilePath));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
                    }
                }
                for (int i = 1; i <= Stageconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Stageconfig.ScriptPaths[i - 1].FilePath));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < Scene.objects.Count; i++)
                {
                    try
                    {
                        Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
                    }
                    catch (Exception ex)
                    {
                        Scene.objects[i].Name = ObjectNames[0];
                    }
                }
            }
            else
            {
                for (int i = 1; i <= Stageconfig.ObjectsNames.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Stageconfig.ScriptPaths[i - 1].FilePath));

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

                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < Scene.objects.Count; i++)
                {
                    Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
                }
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypes, this);
            EntityToolbar.engineType = engineType;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entities = Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
        }

        public void LoadRSDKBObjectsFromStageconfig(string datapath)
        {
            Gameconfig.ImportFrom(engineType, datapath + "//Game//Gameconfig.bin");
            Stageconfig.ImportFrom(engineType,stageconfig);
            _mapViewer.ObjectDefinitions.Objects.Clear();
            _mapViewer.ObjectDefinitions.Objects.Add(new Point(0, 0), new Object_Definitions.MapObject("Blank Object", 0, 0, "", 0, 0, 0, 0));

            bool UsingBytecode = Directory.Exists(datapath + "//Scripts//Bytecode");

            if (!Directory.Exists(datapath + "//Scripts"))
            {
                Directory.CreateDirectory(datapath + "//Scripts");
            }

            ObjectNames.Clear();
            ObjectTypes.Clear();

            ObjectNames.Add("BlankObject");

            ObjectTypes.Add(new Retro_Formats.Object(0, 0, 0, 0, 0, "BlankObject"));

            if (Stageconfig.LoadGlobalScripts)
            {
                for (int i = 1; i <= Gameconfig.ScriptPaths.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Gameconfig.ScriptPaths[i - 1].FilePath));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Gameconfig.ScriptPaths[i - 1].FilePath)));
                    }
                }
                for (int i = 1; i <= Stageconfig.ObjectsNames.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Stageconfig.ObjectsNames[i - 1]));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < Scene.objects.Count; i++)
                {
                    try
                    {
                        Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message);
                    }
                }
            }
            else
            {
                for (int i = 1; i <= Stageconfig.ObjectsNames.Count; i++)
                {
                    try
                    {
                        RSDKv1.Script Script = new RSDKv1.Script(new StreamReader(datapath + "//Scripts//" + Stageconfig.ObjectsNames[i - 1]));

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
                                    ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                                    ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
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

                                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), iconSubType), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                                    }
                                    break;
                            }
                        }

                        if (SingleIcon)
                        {
                            _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + Gameconfig.ScriptPaths.Count + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + Gameconfig.ScriptPaths.Count + 1), 0, datapath + "//Sprites//" + Sheet, X, Y, Width, Height, PivotX, PivotY, 0));
                            ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                            ObjectTypes.Add(new Retro_Formats.Object((byte)(i + 1), 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _mapViewer.ObjectDefinitions.Objects.Add(new Point((i + 1), 0), new Object_Definitions.MapObject(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]), (i + 1), 0, "", 0, 0, 0, 0));
                        ObjectNames.Add(Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1]));
                        ObjectTypes.Add(new Retro_Formats.Object((byte)i, 0, 0, 0, i, Path.GetFileNameWithoutExtension(Stageconfig.ObjectsNames[i - 1])));
                    }
                }
                for (int i = 0; i < Scene.objects.Count; i++)
                {
                    try
                    {
                        Scene.objects[i].Name = ObjectNames[Scene.objects[i].type];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + ex.Message);
                    }
                }
            }

            _blocksViewer.tabControl.TabPages[1].Controls.Clear();
            EntityToolbar = new RetroED.Extensions.EntityToolbar.EntityToolbar(ObjectTypes, this);
            EntityToolbar.engineType = engineType;
            EntityToolbar.Dock = DockStyle.Fill;
            EntityToolbar.Entities = Scene.objects;
            _blocksViewer.tabControl.TabPages[1].Controls.Add(EntityToolbar);
        }

        private void menuItem_RedrawMap_Click(object sender, EventArgs e)
        {
            _mapViewer.DrawScene();
        }

        private void MenuItem_RefreshChunks_Click(object sender, EventArgs e)
        {
            Chunks.ImportFrom(engineType, chunks);
            _blocksViewer.SetChunks();
            _mapViewer.SetScene();
        }

        private void MenuItem_ReloadMap_Click(object sender, EventArgs e)
        {
            Scene.ImportFrom(engineType, scene);
            _blocksViewer.SetChunks();
            _mapViewer.SetScene();
        }

        private void MenuItem_ReloadTileset_Click(object sender, EventArgs e)
        {
            using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
            {
                var bmp = new Bitmap(fs);
                _loadedTiles = (Bitmap)bmp.Clone();
            }
            _blocksViewer.SetChunks();
            _mapViewer.SetScene();
        }

        private void MenuItem_ReloadTileconfig_Click(object sender, EventArgs e)
        {
            Tileconfig = new RSDKvB.Tileconfig(collisionMasks);
            _mapViewer.SetScene();
        }

        private void MenuItem_ReloadStageconfig_Click(object sender, EventArgs e)
        {
            Stageconfig.ImportFrom(engineType, stageconfig);
            _mapViewer.SetScene();
        }

        private void menuItem_deleteObject_Click(object sender, EventArgs e)
        {
            if (EntityToolbar.entitiesList.SelectedIndex > 0)
            {
                EntityToolbar.Entities.RemoveAt(EntityToolbar.entitiesList.SelectedIndex);
                _mapViewer.DrawScene();
            }
        }
    }
}
