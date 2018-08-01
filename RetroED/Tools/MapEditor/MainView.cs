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

        //Stack<UndoAction> UndoList;
        //Stack<UndoAction> RedoList;

        //The Path to these files
        string tiles;
        string mappings;
        string Map;
        string CollisionMasks;

        bool showgrid = false;
        int PlacementMode = 0;

        #region Retro-Sonic Development Kit
        RSDKv1.Level _RSDK1Level;
        RSDKv1.til _RSDK1Chunks;
        RSDKv1.tcf _RSDK1CollisionMask;
        #endregion

        #region RSDKv1
        RSDKv2.Level _RSDK2Level;
        RSDKv2.Tiles128x128 _RSDK2Chunks;
        RSDKv2.CollisionMask _RSDK2CollisionMask;
        #endregion

        #region RSDKv2
        RSDKv3.Level _RSDK3Level;
        RSDKv3.Tiles128x128 _RSDK3Chunks;
        RSDKv3.CollisionMask _RSDK3CollisionMask;
        #endregion

        #region RSDKvB
        RSDKv4.Level _RSDK4Level;
        RSDKv4.Tiles128x128 _RSDK4Chunks;
        RSDKv4.CollisionMask _RSDK4CollisionMask;
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

        void LoadLevel(string level, int RSDKver)
        {
            //Clears the map
            _mapViewer.DrawLevel();
            switch (RSDKver)
            {
                case 0:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK4Level = new RSDKv4.Level(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK4Chunks = new RSDKv4.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK4Chunks = _RSDK4Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV4 = _RSDK4Level.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK4Level = _RSDK4Level;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
                    _mapViewer._RSDK4CollisionMask = _RSDK4CollisionMask;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetLevel();
                    break;
                case 1:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK3Level = new RSDKv3.Level(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK3Chunks = new RSDKv3.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK3Chunks = _RSDK3Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV3 = _RSDK3Level.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK3Level = _RSDK3Level;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
                    _mapViewer._RSDK3CollisionMask = _RSDK3CollisionMask;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetLevel();
                    break;
                case 2:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK2Level = new RSDKv2.Level(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK2Chunks = new RSDKv2.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK2Chunks = _RSDK2Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV2 = _RSDK2Level.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK2Level = _RSDK2Level;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
                    _mapViewer._RSDK2CollisionMask = _RSDK2CollisionMask;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetLevel();
                    break;
                case 3:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK1Level = new RSDKv1.Level(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK1Chunks = new RSDKv1.til(strm);
                    }
                    RSDKv1.gfx gfx = new RSDKv1.gfx(tiles, false);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage;
                    _blocksViewer._RSDK1Chunks = _RSDK1Chunks;
                    _blocksViewer.SetChunks();
                    _blocksViewer.objectsV1 = _RSDK1Level.objects;
                    _blocksViewer.RefreshObjList();

                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer._tiles = gfx.gfxImage;
                    _mapViewer._RSDK1CollisionMask = _RSDK1CollisionMask;
                    _mapViewer._RSDK1Level = _RSDK1Level;
                    _mapViewer._RSDK1Chunks = _RSDK1Chunks;
                    _mapViewer.SetLevel();
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
                if (_RSDK1Level.width != OLDwidth || _RSDK1Level.height != OLDheight) //Well, Is it different?
                {
                    Console.WriteLine("Different"); //It is!
                    //Update the map!
                    _RSDK1Level.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDK1Level.width, (ushort)_RSDK1Level.height, RSDKver);
                    _mapViewer.DrawLevel(); //Draw the map
                }
            }
            if (RSDKver == 2)
            {
                if (_RSDK2Level.width != OLDwidth || _RSDK2Level.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK2Level.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK2Level.width, (ushort)_RSDK2Level.height, RSDKver);
                    _mapViewer.DrawLevel();
                }

            }
            if (RSDKver == 1)
            {
                if (_RSDK3Level.width != OLDwidth || _RSDK3Level.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK3Level.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK3Level.width, (ushort)_RSDK3Level.height, RSDKver);
                    _mapViewer.DrawLevel();
                }
            }
            if (RSDKver == 0)
            {
                if (_RSDK4Level.width != OLDwidth || _RSDK4Level.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK4Level.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK4Level.width, (ushort)_RSDK4Level.height, RSDKver);
                    _mapViewer.DrawLevel();
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
                    NewTiles[i][j] = 0xffff; // fill the new ones with blanks
            }

            for (ushort i = 0; i < NewHeight; i++)
            {
                // now resize all child arrays to the new width
                Array.Resize(ref NewTiles[i], NewWidth);
                for (ushort j = oldWidth; j < NewWidth; j++)
                    NewTiles[i][j] = 0xffff; // and fill with blanks if wider
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
                    _mapViewer._RSDK1Level.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawLevel(); //Let's redraw the level
                    break;
                case 2:
                    _mapViewer._RSDK2Level.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawLevel(); //Let's redraw the level
                    break;
                case 1:
                    _mapViewer._RSDK3Level.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawLevel(); //Let's redraw the level
                    break;
                case 0:
                    _mapViewer._RSDK4Level.objects.Clear(); //Clear the Object list (Delete all objects)
                    _mapViewer.DrawLevel(); //Let's redraw the level
                    break;
            }
        }

        private void addObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MenuItem_Open_Click(object sender, EventArgs e)
        {
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

                if (LoadedRSDKver == 3) //Are We displaying a Retro-Sonic Map?
                {
                    //Load the files needed to show the map
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.gfx");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.til");
                    Map = ofd.FileName;
                    CollisionMasks = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.tcf");
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(CollisionMasks))
                    {
                        LoadLevel(ofd.FileName, LoadedRSDKver);
                    }
                    else
                    {
                        MessageBox.Show("Tiles, Mappings and Collision Masks need to exist in the same folder as act data, just like the game.");
                    }
                }

                if (LoadedRSDKver != 3) //Are we NOT displaying a Retro-Sonic Map?
                {
                    //Load Map Data
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "16x16Tiles.gif");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "128x128Tiles.bin");
                    Map = ofd.FileName;
                    CollisionMasks = Path.Combine(Path.GetDirectoryName(ofd.FileName), "CollisionMasks.bin");
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(CollisionMasks))
                    {
                        LoadLevel(ofd.FileName, LoadedRSDKver);
                    }
                    else
                    {
                        MessageBox.Show("Tiles, Mappings and Collision Masks need to exist in the same folder as act data, just like the game.");
                    }
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
                        _mapViewer._RSDK4Level.Write(Map);
                        break;
                    case 1:
                        _mapViewer._RSDK3Level.Write(Map);
                        break;
                    case 2:
                        _mapViewer._RSDK2Level.Write(Map);
                        break;
                    case 3:
                        _mapViewer._RSDK1Level.Write(Map);
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
                        _mapViewer._RSDK4Level.Write(dlg.FileName);
                        break;
                    case 1:
                        _mapViewer._RSDK3Level.Write(dlg.FileName);
                        break;
                    case 2:
                        _mapViewer._RSDK2Level.Write(dlg.FileName);
                        break;
                    case 3:
                        _mapViewer._RSDK1Level.Write(dlg.FileName);
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
                    if (_RSDK4Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK4Level.MapLayout[0].Length * 128, _RSDK4Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK4Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK4Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK4Chunks.BlockList[_RSDK4Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK4Level.objects.Count; o++)
                                {
                                        Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK4Level.objects[o].type, _RSDK4Level.objects[o].subtype);
                                        if (mapobj != null && mapobj.ID > 0)
                                        {
                                            g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos);
                                        }
                                        else
                                        {
                                            g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos);
                                        }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK3Level.MapLayout[0].Length * 128, _RSDK3Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK3Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK3Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK3Chunks.BlockList[_RSDK3Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK3Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK3Level.objects[o].type, _RSDK3Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK2Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK2Level.MapLayout[0].Length * 128, _RSDK2Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK2Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK2Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK2Chunks.BlockList[_RSDK2Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK2Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK2Level.objects[o].type, _RSDK2Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDK1Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK1Level.MapLayout[0].Length * 128, _RSDK1Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK1Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK1Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK1Chunks.BlockList[_RSDK1Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                                for (int o = 0; o < _RSDK1Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK1Level.objects[o].type, _RSDK1Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos);
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
                    if (_RSDK4Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK4Level.MapLayout[0].Length * 128, _RSDK4Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK4Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK4Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK4Chunks.BlockList[_RSDK4Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK3Level.MapLayout[0].Length * 128, _RSDK3Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK3Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK3Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK3Chunks.BlockList[_RSDK3Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK2Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK2Level.MapLayout[0].Length * 128, _RSDK2Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK2Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK2Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK2Chunks.BlockList[_RSDK2Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDK1Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK1Level.MapLayout[0].Length * 128, _RSDK1Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK1Level.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK1Level.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK1Chunks.BlockList[_RSDK1Level.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
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
                    if (_RSDK4Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK4Level.MapLayout[0].Length * 128, _RSDK4Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK4Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK4Level.objects[o].type, _RSDK4Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK3Level.MapLayout[0].Length * 128, _RSDK3Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK3Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK3Level.objects[o].type, _RSDK3Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK2Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK2Level.MapLayout[0].Length * 128, _RSDK2Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK2Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK2Level.objects[o].type, _RSDK2Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDK1Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK) /*Just Render the map onto an image and save it*/
                        {
                            Bitmap massive = new Bitmap(_RSDK1Level.MapLayout[0].Length * 128, _RSDK1Level.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int o = 0; o < _RSDK1Level.objects.Count; o++)
                                {
                                    Object_Definitions.MapObject mapobj = _mapViewer.RSObjects.GetObjectByType(_RSDK1Level.objects[o].type, _RSDK1Level.objects[o].subtype);
                                    if (mapobj != null && mapobj.ID > 0)
                                    {
                                        g.DrawImageUnscaled(mapobj.RenderObject(_mapViewer.loadedRSDKver, _mapViewer.datapath), _RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos);
                                    }
                                    else
                                    {
                                        g.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos);
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
                    ushort[][] NewChunks1 = new ushort[_RSDK1Level.height][];
                    for (ushort i = 0; i < _RSDK1Level.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewChunks1[i] = new ushort[_RSDK1Level.width];
                        for (int j = 0; j < _RSDK1Level.width; ++j)
                            NewChunks1[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK1Level.MapLayout = NewChunks1;
                    _mapViewer.DrawLevel();
                    break;
                case 2:
                    ushort[][] NewTiles2 = new ushort[_RSDK2Level.height][];
                    for (ushort i = 0; i < _RSDK2Level.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles2[i] = new ushort[_RSDK2Level.width];
                        for (int j = 0; j < _RSDK2Level.width; ++j)
                            NewTiles2[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK2Level.MapLayout = NewTiles2;

                    _mapViewer.DrawLevel();
                    break;
                case 1:
                    ushort[][] NewTiles3 = new ushort[_RSDK3Level.height][];
                    for (ushort i = 0; i < _RSDK3Level.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles3[i] = new ushort[_RSDK3Level.width];
                        for (int j = 0; j < _RSDK3Level.width; ++j)
                            NewTiles3[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK3Level.MapLayout = NewTiles3;
                    _mapViewer.DrawLevel();
                    break;
                case 0:
                    ushort[][] NewTiles4 = new ushort[_RSDK4Level.height][];
                    for (ushort i = 0; i < _RSDK4Level.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles4[i] = new ushort[_RSDK4Level.width];
                        for (int j = 0; j < _RSDK4Level.width; ++j)
                            NewTiles4[i][j] = 0; // fill the chunks with blanks
                    }
                    _mapViewer._RSDK4Level.MapLayout = NewTiles4;
                    _mapViewer.DrawLevel();
                    break;
            }
        }

        private void MenuItem_ClearObjects_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Find out what RSDK version is loaded and clear the object list of objects
            {
                case 3:
                    _mapViewer._RSDK1Level.objects.Clear();
                    _mapViewer.DrawLevel();
                    _blocksViewer.RefreshObjList();
                    break;
                case 2:
                    _mapViewer._RSDK2Level.objects.Clear();
                    _mapViewer.DrawLevel();
                    _blocksViewer.RefreshObjList();
                    break;
                case 1:
                    _mapViewer._RSDK3Level.objects.Clear();
                    _mapViewer.DrawLevel();
                    _blocksViewer.RefreshObjList();
                    break;
                case 0:
                    _mapViewer._RSDK4Level.objects.Clear();
                    _mapViewer.DrawLevel();
                    _blocksViewer.RefreshObjList();
                    break;
            }
        }

        private void MenuItem_AddObject_Click(object sender, EventArgs e)
        {
            NewObjectForm frm = new NewObjectForm(0);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                switch (LoadedRSDKver) //Add an object to the object list using the values the user has given
                {
                    case 3:
                        RSDKv1.Object Obj1 = new RSDKv1.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK1Level.objects.Add(Obj1);
                        break;
                    case 2:
                        RSDKv2.Object Obj2 = new RSDKv2.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK2Level.objects.Add(Obj2);
                        break;
                    case 1:
                        RSDKv3.Object Obj3 = new RSDKv3.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK3Level.objects.Add(Obj3);
                        break;
                    case 0:
                        RSDKv4.Object Obj4 = new RSDKv4.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        _mapViewer._RSDK4Level.objects.Add(Obj4);
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
                    OldTiles = _RSDK1Level.MapLayout;
                    OLDwidth = _RSDK1Level.width;
                    OLDheight = _RSDK1Level.height;
                    //Set the form data to the map data
                    frm.Mapv1 = _RSDK1Level;
                    frm.Setup();
                    break;
                case 2:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK2Level.MapLayout;
                    OLDwidth = _RSDK2Level.width;
                    OLDheight = _RSDK2Level.height;
                    //Set the form data to the map data
                    frm.Mapv2 = _RSDK2Level;
                    frm.Setup();
                    break;
                case 1:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK3Level.MapLayout;
                    OLDwidth = _RSDK3Level.width;
                    OLDheight = _RSDK3Level.height;
                    //Set the form data to the map data
                    frm.Mapv3 = _RSDK3Level;
                    frm.Setup();
                    break;
                case 0:
                    //Backup the data, We'll use this later! :)
                    OldTiles = _RSDK4Level.MapLayout;
                    OLDwidth = _RSDK4Level.width;
                    OLDheight = _RSDK4Level.height;
                    //Set the form data to the map data
                    frm.Mapv4 = _RSDK4Level;
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
                        OldTiles = _RSDK1Level.MapLayout; //Get Old Chunks
                        _RSDK1Level = frm.Mapv1; //Set the map data to the updated data
                        NewTiles = _RSDK1Level.MapLayout; //Get the updated Chunks
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight); //Was the map size changed?
                        _mapViewer.DrawLevel();
                        break;
                    case 2:
                        OLDwidth = _RSDK2Level.width;
                        OLDheight = _RSDK2Level.height;
                        OldTiles = _RSDK2Level.MapLayout;
                        _RSDK2Level = frm.Mapv2;
                        NewTiles = _RSDK2Level.MapLayout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    case 1:
                        OLDwidth = _RSDK3Level.width;
                        OLDheight = _RSDK3Level.height;
                        OldTiles = _RSDK3Level.MapLayout;
                        _RSDK3Level = frm.Mapv3;
                        NewTiles = _RSDK3Level.MapLayout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    case 0:
                        OLDwidth = _RSDK4Level.width;
                        OLDheight = _RSDK4Level.height;
                        OldTiles = _RSDK4Level.MapLayout;
                        _RSDK4Level = frm.Mapv4;
                        NewTiles = _RSDK4Level.MapLayout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuItem_MapLayer_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowMap = MenuItem_MapLayer.Checked = !_mapViewer.ShowMap; //Are we going to show the Map Layout?
        }

        private void MenuItem_Objects_Click(object sender, EventArgs e)
        {
            _mapViewer.ShowObjects = MenuItem_Objects.Checked = !_mapViewer.ShowObjects; //Are we going to show the Objects?
        }

        private void MenuItem_CollisionMasks_Click(object sender, EventArgs e)
        {
            //Are we going to show the Collision Masks for each tile?
        }

        private void MenuItem_RefreshChunks_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //The Same as when the map was being setup
            {
                case 0:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK4Chunks = new RSDKv4.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK4Chunks = _RSDK4Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK4Level = _RSDK4Level;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
                    _mapViewer._RSDK4CollisionMask = _RSDK4CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetLevel();
                    break;
                case 1:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK3Chunks = new RSDKv3.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK3Chunks = _RSDK3Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK3Level = _RSDK3Level;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
                    _mapViewer._RSDK3CollisionMask = _RSDK3CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetLevel();
                    break;
                case 2:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK2Chunks = new RSDKv2.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK2Chunks = _RSDK2Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK2Level = _RSDK2Level;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
                    _mapViewer._RSDK2CollisionMask = _RSDK2CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetLevel();
                    break;
                case 3:
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK1Chunks = new RSDKv1.til(strm);
                    }
                    RSDKv1.gfx gfx = new RSDKv1.gfx(tiles, false);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage;
                    _blocksViewer._RSDK1Chunks = _RSDK1Chunks;
                    _blocksViewer.SetChunks();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK1Level = _RSDK1Level;
                    _mapViewer._RSDK1Chunks = _RSDK1Chunks;
                    _mapViewer._RSDK1CollisionMask = _RSDK1CollisionMask;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetLevel();
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_ShowGrid_Click(object sender, EventArgs e)
        {
            showgrid = _mapViewer.ShowGrid = MenuItem_ShowGrid.Checked = !showgrid; //Do we want a grid overlayed on our map?
            _mapViewer.DrawLevel();
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
    }

}
