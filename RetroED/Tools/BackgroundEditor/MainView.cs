using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RetroED.Tools.BackgroundEditor
{
    public partial class MainView : Form
    {
        enum Placementmode
        {
            NONE,
            PlaceTiles,
            PlaceObjects
        }

        public int LoadedRSDKver = 0;

        private Image _loadedTiles;
        private StageChunksView _blocksViewer;
        private StageMapView _mapViewer;

        int curlayer = 1;

        //Stack<UndoAction> UndoList;
        //Stack<UndoAction> RedoList;

        string tiles;
        string mappings;
        string Background;

        bool showgrid = false;
        int PlacementMode = 0;

        #region Retro-Sonic Development Kit
        RSDKv1.BGLayout _RSDK1Background;
        RSDKv1.til _RSDK1Chunks;
        #endregion

        #region RSDKv1
        RSDKv2.BGLayout _RSDK2Background;
        RSDKv2.Tiles128x128 _RSDK2Chunks;
        #endregion

        #region RSDKv2
        RSDKv3.BGLayout _RSDK3Background;
        RSDKv3.Tiles128x128 _RSDK3Chunks;
        #endregion

        #region RSDKvB
        RSDKv4.BGLayout _RSDK4Background;
        RSDKv4.Tiles128x128 _RSDK4Chunks;
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

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Sonic 1/Sonic 2 Background files (Backgrounds.bin)|Backgrounds.bin|Sonic CD Background files (Backgrounds.bin)|Backgrounds.bin|Sonic Nexus Background files (Backgrounds.bin)|Backgrounds.bin|Retro-Sonic Background files (ZoneBG.map)|ZoneBG.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadedRSDKver = ofd.FilterIndex - 1;

                if (LoadedRSDKver == 3)
                {
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.gfx");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.til");
                    Background = ofd.FileName;
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(Background))
                    {
                        LoadLevel(ofd.FileName, LoadedRSDKver);
                    }
                    else
                    {
                        MessageBox.Show("Tiles and Mappings need to exist in the same folder as act data, just like the game.");
                    }
                }

                if (LoadedRSDKver != 3)
                {
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "16x16Tiles.gif");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "128x128Tiles.bin");
                    Background = ofd.FileName;
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(Background))
                    {
                        LoadLevel(ofd.FileName, LoadedRSDKver);
                    }
                    else
                    {
                        MessageBox.Show("Tiles and Mappings need to exist in the same folder as act data, just like the game.");
                    }
                }
            }
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
                        _RSDK4Background = new RSDKv4.BGLayout(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK4Chunks = new RSDKv4.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK4Chunks = _RSDK4Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer._RSDK4Background = _RSDK4Background;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.RefreshParallaxList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
                    _mapViewer._RSDK4Background = _RSDK4Background;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetLevel();
                    _mapViewer.DrawLevel();

                    selectedLayerToolStripMenuItem.DropDownItems.Clear();
                    for (int i = 0; i < _RSDK4Background.Layers.Count; i++)
                    {
                        selectedLayerToolStripMenuItem.DropDownItems.Add("Background Layer " + i.ToString());
                    }

                    break;
                case 1:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK3Background = new RSDKv3.BGLayout(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK3Chunks = new RSDKv3.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK3Chunks = _RSDK3Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer._RSDK3Background = _RSDK3Background;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.RefreshParallaxList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
                    _mapViewer._RSDK3Background = _RSDK3Background;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetLevel();
                    _mapViewer.DrawLevel();

                    selectedLayerToolStripMenuItem.DropDownItems.Clear();
                    for (int i = 0; i < _RSDK3Background.Layers.Count; i++)
                    {
                        selectedLayerToolStripMenuItem.DropDownItems.Add("Background Layer " + i.ToString());
                    }

                    break;
                case 2:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK2Background = new RSDKv2.BGLayout(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK2Chunks = new RSDKv2.Tiles128x128(strm);
                    }
                    _loadedTiles = Bitmap.FromFile(tiles);
                    _blocksViewer._RSDK2Chunks = _RSDK2Chunks;
                    _blocksViewer._tiles = _loadedTiles;
                    _blocksViewer._RSDK2Background = _RSDK2Background;
                    _blocksViewer.loadedRSDKver = RSDKver;
                    _blocksViewer.SetChunks();
                    _blocksViewer.RefreshParallaxList();

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
                    _mapViewer._RSDK2Background = _RSDK2Background;
                    _mapViewer.loadedRSDKver = RSDKver;
                    _mapViewer.SetLevel();
                    _mapViewer.DrawLevel();

                    selectedLayerToolStripMenuItem.DropDownItems.Clear();
                    for (int i = 0; i < _RSDK2Background.Layers.Count; i++)
                    {
                        selectedLayerToolStripMenuItem.DropDownItems.Add("Background Layer " + i.ToString());
                    }
                    break;
                case 3:
                    using (Stream strm = File.OpenRead(level))
                    {
                        _RSDK1Background = new RSDKv1.BGLayout(strm);
                    }
                    using (Stream strm = File.OpenRead(mappings))
                    {
                        _RSDK1Chunks = new RSDKv1.til(strm);
                    }
                    RSDKv1.gfx gfx = new RSDKv1.gfx(tiles, false);

                    _loadedTiles = gfx.gfxImage;

                    _blocksViewer.loadedRSDKver = LoadedRSDKver;
                    _blocksViewer._tiles = gfx.gfxImage;
                    _blocksViewer._RSDK1Background = _RSDK1Background;
                    _blocksViewer._RSDK1Chunks = _RSDK1Chunks;
                    _blocksViewer.SetChunks();
                    _blocksViewer.RefreshParallaxList();

                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer._tiles = gfx.gfxImage;
                    _mapViewer._RSDK1Background = _RSDK1Background;
                    _mapViewer._RSDK1Chunks = _RSDK1Chunks;
                    _mapViewer.SetLevel();
                    _mapViewer.DrawLevel();

                    selectedLayerToolStripMenuItem.DropDownItems.Clear();
                    for (int i = 0; i < _RSDK1Background.Layers.Count; i++)
                    {
                        selectedLayerToolStripMenuItem.DropDownItems.Add("Background Layer " + i.ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 0:
                    if (_RSDK4Background != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap massive = new Bitmap(_RSDK4Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK4Background.Layers[curlayer].MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK4Background.Layers[curlayer].MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK4Background.Layers[curlayer].MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK4Chunks.BlockList[_RSDK4Background.Layers[curlayer].MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 1:
                    if (_RSDK3Background != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap massive = new Bitmap(_RSDK3Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK3Background.Layers[curlayer].MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK3Background.Layers[curlayer].MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK3Background.Layers[curlayer].MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK3Chunks.BlockList[_RSDK3Background.Layers[curlayer].MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 2:
                    if (_RSDK2Background != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap massive = new Bitmap(_RSDK2Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK2Background.Layers[curlayer].MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK2Background.Layers[curlayer].MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK2Background.Layers[curlayer].MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK2Chunks.BlockList[_RSDK2Background.Layers[curlayer].MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                                    }
                                }
                            }
                            massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            massive.Dispose();
                        }
                    }
                    break;
                case 3:
                    if (_RSDK1Background != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap massive = new Bitmap(_RSDK1Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK1Background.Layers[curlayer].MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK1Background.Layers[curlayer].MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK1Background.Layers[curlayer].MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK1Chunks.BlockList[_RSDK1Background.Layers[curlayer].MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Background == null)
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
            else
            {
                switch (LoadedRSDKver)
                {
                    case 0:
                        _mapViewer._RSDK4Background.Write(Background);
                        break;
                    case 1:
                        _mapViewer._RSDK3Background.Write(Background);
                        break;
                    case 2:
                        _mapViewer._RSDK2Background.Write(Background);
                        break;
                    case 3:
                        _mapViewer._RSDK1Background.Write(Background);
                        break;
                    default:
                        break;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Sonic 1/Sonic 2 Background files (Backgrounds.bin)|Backgrounds.bin|Sonic CD Background files (Backgrounds.bin)|Backgrounds.bin|Sonic Nexus Background files (Backgrounds.bin)|Backgrounds.bin|Retro-Sonic Background files (ZoneBG.map)|ZoneBG.map";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (LoadedRSDKver)
                {
                    case 0:
                        _mapViewer._RSDK4Background.Write(dlg.FileName);
                        break;
                    case 1:
                        _mapViewer._RSDK3Background.Write(dlg.FileName);
                        break;
                    case 2:
                        _mapViewer._RSDK2Background.Write(dlg.FileName);
                        break;
                    case 3:
                        _mapViewer._RSDK1Background.Write(dlg.FileName);
                        break;
                    default:
                        break;
                }
            }
        }

        private void PlaceTileButton_Click(object sender, EventArgs e)
        {
            if (PlacementMode != 1)
            {
                PlacementMode = (int)Placementmode.PlaceTiles;
                PlaceTileButton.Checked = true;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceTileButton.BackColor = SystemColors.ControlDarkDark;
            }
            else if (PlacementMode == 1)
            {
                PlacementMode = (int)Placementmode.NONE;
                PlaceTileButton.Checked = false;
                _mapViewer.PlacementMode = PlacementMode;
                PlaceTileButton.BackColor = SystemColors.Control;
            }
        }

        private void mapPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RSN_LayerPropertiesForm frm1 = new RSN_LayerPropertiesForm(LoadedRSDKver);
            CD12_LayerPropertiesForm frm2 = new CD12_LayerPropertiesForm(LoadedRSDKver);

            frm1.CurLayer = _mapViewer.curlayer;
            frm2.CurLayer = _mapViewer.curlayer;
            ushort[][] OldTiles = new ushort[0][];
            ushort[][] NewTiles = new ushort[0][];
            int OLDwidth = 0;
            int OLDheight = 0;
            Console.WriteLine(_mapViewer.curlayer);
            switch (LoadedRSDKver)
            {
                case 3:
                    OldTiles = _RSDK1Background.Layers[curlayer].MapLayout;
                    OLDwidth = _RSDK1Background.Layers[curlayer].width;
                    OLDheight = _RSDK1Background.Layers[curlayer].height;
                    frm1.Mapv1 = _RSDK1Background;
                    frm1.Setup();
                    break;
                case 2:
                    OldTiles = _RSDK2Background.Layers[curlayer].MapLayout;
                    OLDwidth = _RSDK2Background.Layers[curlayer].width;
                    OLDheight = _RSDK2Background.Layers[curlayer].height;
                    frm1.Mapv2 = _RSDK2Background;
                    frm1.Setup();
                    break;
                case 1:
                    OldTiles = _RSDK3Background.Layers[curlayer].MapLayout;
                    OLDwidth = _RSDK3Background.Layers[curlayer].width;
                    OLDheight = _RSDK3Background.Layers[curlayer].height;
                    frm2.Mapv3 = _RSDK3Background;
                    frm2.Setup();
                    break;
                case 0:
                    OldTiles = _RSDK4Background.Layers[curlayer].MapLayout;
                    OLDwidth = _RSDK4Background.Layers[curlayer].width;
                    OLDheight = _RSDK4Background.Layers[curlayer].height;
                    frm2.Mapv4 = _RSDK4Background;
                    frm2.Setup();
                    break;
                default:
                    break;
            }

            switch (LoadedRSDKver)
            {
                case 3:
                    if (frm1.ShowDialog(this) == DialogResult.OK)
                    {
                                _RSDK1Background = frm1.Mapv1;
                                NewTiles = _RSDK1Background.Layers[curlayer].MapLayout;
                                CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
                case 2:
                    if (frm1.ShowDialog(this) == DialogResult.OK)
                    {
                                _RSDK2Background = frm1.Mapv2;
                                NewTiles = _RSDK2Background.Layers[curlayer].MapLayout;
                                CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
                case 1:
                    if (frm2.ShowDialog(this) == DialogResult.OK)
                    {
                                _RSDK3Background = frm2.Mapv3;
                                NewTiles = _RSDK3Background.Layers[curlayer].MapLayout;
                                CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
                case 0:
                    if (frm2.ShowDialog(this) == DialogResult.OK)
                    {
                                _RSDK4Background = frm2.Mapv4;
                                NewTiles = _RSDK4Background.Layers[curlayer].MapLayout;
                                CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
            }
        }

        void CheckDimensions(int RSDKver, ushort[][] OldTiles, ushort[][] NewTiles, int OLDwidth, int OLDheight)
        {

            if (RSDKver == 3)
            {
                if (_RSDK1Background.Layers[curlayer].width != OLDwidth || _RSDK1Background.Layers[curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK1Background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDK1Background.Layers[curlayer].width, (ushort)_RSDK1Background.Layers[curlayer].height, RSDKver);
                }
            }
            if (RSDKver == 2)
            {
                if (_RSDK2Background.Layers[_mapViewer.curlayer].width != OLDwidth || _RSDK2Background.Layers[_mapViewer.curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK2Background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDK2Background.Layers[_mapViewer.curlayer].width, (ushort)_RSDK2Background.Layers[_mapViewer.curlayer].height, RSDKver);
                }

            }
            if (RSDKver == 1)
            {
                if (_RSDK3Background.Layers[curlayer].width != OLDwidth || _RSDK3Background.Layers[curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK3Background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDK3Background.Layers[curlayer].width, (ushort)_RSDK3Background.Layers[curlayer].height, RSDKver);
                }
            }
            if (RSDKver == 0)
            {
                if (_RSDK4Background.Layers[curlayer].width != OLDwidth || _RSDK4Background.Layers[curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK4Background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)_RSDK4Background.Layers[curlayer].width, (ushort)_RSDK4Background.Layers[curlayer].height, RSDKver);
                }
            }
        }

        ushort[][] UpdateMapDimensions(ushort[][] OldTiles, ushort[][] NewTiles, ushort oldWidth, ushort oldHeight, ushort NewWidth, ushort NewHeight, int RSDKver)
        {
            // fill the extended tile arrays with "empty" values

            // if we're actaully getting shorter, do nothing!
            if (oldWidth > 0 && oldHeight > 0)
            {
                for (ushort i = oldHeight; i < NewHeight; i++)
                {
                    // first create arrays child arrays to the old width
                    // a little inefficient, but at least they'll all be equal sized
                    NewTiles[i] = new ushort[oldWidth];
                    for (int j = 0; j < oldWidth; ++j)
                        NewTiles[i][j] = 0xffff; // fill the new ones with blanks
                }
            }
            else
            {
                NewTiles = new ushort[NewHeight][];
                oldWidth = 1;
                oldHeight = 1;
            }

            for (ushort i = 0; i < NewHeight; i++)
            {
                // now resize all child arrays to the new width
                Array.Resize(ref NewTiles[i], NewWidth);
                for (ushort j = oldWidth; j < NewWidth; j++)
                    NewTiles[i][j] = 0xffff; // and fill with blanks if wider
            }
            return NewTiles;
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showgrid)
            {
                showGridToolStripMenuItem.Checked = true;
                showgrid = true;
                _mapViewer.ShowGrid = true;
                _mapViewer.DrawLevel();
            }
            else if (showgrid)
            {
                showGridToolStripMenuItem.Checked = false;
                showgrid = false;
                _mapViewer.ShowGrid = false;
                _mapViewer.DrawLevel();
            }
        }

        private void refreshChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
                switch (LoadedRSDKver)
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
                    _mapViewer._RSDK4Background = _RSDK4Background;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
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
                    _mapViewer._RSDK3Background = _RSDK3Background;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
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
                    _mapViewer._RSDK2Background = _RSDK2Background;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
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
                    _mapViewer._RSDK1Background = _RSDK1Background;
                    _mapViewer._RSDK1Chunks = _RSDK1Chunks;
                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer.SetLevel();
                    break;
                    default:
                        break;
                }
        }

        private void clearChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(LoadedRSDKver)
            {
                case 3:
                    ushort[][] NewTiles1 = new ushort[_RSDK1Background.Layers[curlayer].height][];
                    for (ushort i = 0; i < _RSDK1Background.Layers[curlayer].height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles1[i] = new ushort[_RSDK1Background.Layers[curlayer].width];
                        for (int j = 0; j < _RSDK1Background.Layers[curlayer].width; ++j)
                            NewTiles1[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK1Background.Layers[curlayer].MapLayout = NewTiles1;
                    _mapViewer.DrawLevel();
                    break;
                case 2:
                    ushort[][] NewTiles2 = new ushort[_RSDK2Background.Layers[_mapViewer.curlayer].height][];
                    for (ushort i = 0; i < _RSDK2Background.Layers[_mapViewer.curlayer].height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles2[i] = new ushort[_RSDK2Background.Layers[_mapViewer.curlayer].width];
                        for (int j = 0; j < _RSDK2Background.Layers[_mapViewer.curlayer].width; ++j)
                            NewTiles2[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK2Background.Layers[curlayer].MapLayout = NewTiles2;
                    _mapViewer.DrawLevel();
                    break;
                case 1:
                    ushort[][] NewTiles3 = new ushort[_RSDK3Background.Layers[curlayer].height][];
                    for (ushort i = 0; i < _RSDK3Background.Layers[curlayer].height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles3[i] = new ushort[_RSDK3Background.Layers[curlayer].width];
                        for (int j = 0; j < _RSDK3Background.Layers[curlayer].width; ++j)
                            NewTiles3[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK3Background.Layers[curlayer].MapLayout = NewTiles3;
                    _mapViewer.DrawLevel();
                    break;
                case 0:
                    ushort[][] NewTiles4 = new ushort[_RSDK4Background.Layers[curlayer].height][];
                    for (ushort i = 0; i < _RSDK4Background.Layers[curlayer].height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles4[i] = new ushort[_RSDK4Background.Layers[curlayer].width];
                        for (int j = 0; j < _RSDK4Background.Layers[curlayer].width; ++j)
                            NewTiles4[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK4Background.Layers[curlayer].MapLayout = NewTiles4;
                    _mapViewer.DrawLevel();
                    break;
            }
        }

        private void drawAllLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mapViewer.DrawAllLayers)
            {
                drawAllLayersToolStripMenuItem.Checked = _mapViewer.DrawAllLayers = false;
                _mapViewer.DrawLevel();
            }
            else
            {
                drawAllLayersToolStripMenuItem.Checked = _mapViewer.DrawAllLayers = true;
                _mapViewer.DrawLevel();
            }
        }

        private void selectedLayerToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            curlayer = selectedLayerToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            _mapViewer.curlayer = selectedLayerToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            _mapViewer.DrawLevel();
        }

        private void clearParallaxValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(LoadedRSDKver)
            {
                case 0:
                    _RSDK4Background.Lines.Clear();
                    _blocksViewer.RefreshParallaxList();
                    break;
                case 1:
                    _RSDK3Background.Lines.Clear();
                    _blocksViewer.RefreshParallaxList();
                    break;
                case 2:
                    _RSDK2Background.Lines.Clear();
                    _blocksViewer.RefreshParallaxList();
                    break;
                case 3:
                    _RSDK1Background.Lines.Clear();
                    _blocksViewer.RefreshParallaxList();
                    break;
            }
        }

        private void addParallaxValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 0:
                    _RSDK4Background.Lines.Add(new RSDKv4.BGLayout.ParallaxValues());
                    _blocksViewer.RefreshParallaxList();
                    break;
                case 1:
                    _RSDK3Background.Lines.Add(new RSDKv3.BGLayout.ParallaxValues());
                    _blocksViewer.RefreshParallaxList();
                    break;
                case 2:
                    _RSDK2Background.Lines.Add(new RSDKv2.BGLayout.ParallaxValues());
                    _blocksViewer.RefreshParallaxList();
                    break;
                case 3:
                    _RSDK1Background.Lines.Add(new RSDKv1.BGLayout.ParallaxValues());
                    _blocksViewer.RefreshParallaxList();
                    break;
            }
        }
    }

}
