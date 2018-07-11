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
                            Bitmap massive = new Bitmap(_RSDK4Background.MapLayout[0].Length * 128, _RSDK4Background.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK4Background.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK4Background.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK4Chunks.BlockList[_RSDK4Background.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
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
                            Bitmap massive = new Bitmap(_RSDK3Background.MapLayout[0].Length * 128, _RSDK3Background.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK3Background.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK3Background.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK3Chunks.BlockList[_RSDK3Background.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
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
                            Bitmap massive = new Bitmap(_RSDK2Background.MapLayout[0].Length * 128, _RSDK2Background.MapLayout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK2Background.MapLayout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK2Background.MapLayout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK2Chunks.BlockList[_RSDK2Background.MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
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
                            Bitmap massive = new Bitmap(_RSDK1Background.Layout[0].Length * 128, _RSDK1Background.Layout.Length * 128);
                            using (Graphics g = Graphics.FromImage(massive))
                            {
                                for (int y = 0; y < _RSDK1Background.Layout.Length; y++)
                                {
                                    for (int x = 0; x < _RSDK1Background.Layout[0].Length; x++)
                                    {
                                        g.DrawImage(_RSDK1Chunks.BlockList[_RSDK1Background.Layout[y][x]].Render(_loadedTiles), x * 128, y * 128);
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
            PropertiesForm frm = new PropertiesForm(LoadedRSDKver);

            ushort[][] OldTiles;
            ushort[][] NewTiles;
            int OLDwidth = 0;
            int OLDheight = 0;

            switch (LoadedRSDKver)
            {
                case 3:
                    OldTiles = _RSDK1Background.Layout;
                    OLDwidth = _RSDK1Background.Width;
                    OLDheight = _RSDK1Background.Height;
                    frm.Mapv1 = _RSDK1Background;
                    frm.Setup();
                    break;
                case 2:
                    frm.Mapv2 = _RSDK2Background;
                    frm.Setup();
                    break;
                case 1:
                    frm.Mapv3 = _RSDK3Background;
                    frm.Setup();
                    break;
                case 0:
                    frm.Mapv4 = _RSDK4Background;
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
                        OldTiles = _RSDK1Background.Layout;
                        Console.WriteLine(OLDwidth + " " + _RSDK1Background.Width + " " + OLDheight + " " + _RSDK1Background.Height);
                        _RSDK1Background = frm.Mapv1;
                        Console.WriteLine(OLDwidth + " " + _RSDK1Background.Width + " " + OLDheight + " " + _RSDK1Background.Height);
                        NewTiles = _RSDK1Background.Layout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    case 2:
                        OLDwidth = _RSDK2Background.width;
                        OLDheight = _RSDK2Background.height;
                        OldTiles = _RSDK2Background.MapLayout;
                        _RSDK2Background = frm.Mapv2;
                        NewTiles = _RSDK2Background.MapLayout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    case 1:
                        OLDwidth = _RSDK3Background.width;
                        OLDheight = _RSDK3Background.height;
                        OldTiles = _RSDK3Background.MapLayout;
                        _RSDK3Background = frm.Mapv3;
                        NewTiles = _RSDK3Background.MapLayout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    case 0:
                        OLDwidth = _RSDK4Background.width;
                        OLDheight = _RSDK4Background.height;
                        OldTiles = _RSDK4Background.MapLayout;
                        _RSDK4Background = frm.Mapv4;
                        NewTiles = _RSDK4Background.MapLayout;
                        CheckDimensions(LoadedRSDKver, OldTiles, NewTiles, OLDwidth, OLDheight);
                        _mapViewer.DrawLevel();
                        break;
                    default:
                        break;
                }
            }
        }

        void CheckDimensions(int RSDKver, ushort[][] OldTiles, ushort[][] NewTiles, int OLDwidth, int OLDheight)
        {

            if (RSDKver == 3)
            {
                if (_RSDK1Background.Width != OLDwidth || _RSDK1Background.Height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK1Background.Layout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK1Background.Width, (ushort)_RSDK1Background.Height, RSDKver);
                    _mapViewer.DrawLevel();
                }
            }
            if (RSDKver == 2)
            {
                if (_RSDK2Background.width != OLDwidth || _RSDK2Background.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK2Background.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK2Background.width, (ushort)_RSDK2Background.height, RSDKver);
                    _mapViewer.DrawLevel();
                }

            }
            if (RSDKver == 1)
            {
                if (_RSDK3Background.width != OLDwidth || _RSDK3Background.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK3Background.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK3Background.width, (ushort)_RSDK3Background.height, RSDKver);
                    _mapViewer.DrawLevel();
                }
            }
            if (RSDKver == 0)
            {
                if (_RSDK4Background.width != OLDwidth || _RSDK4Background.height != OLDheight)
                {
                    Console.WriteLine("Different");
                    _RSDK4Background.MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDwidth, (ushort)_RSDK4Background.width, (ushort)_RSDK4Background.height, RSDKver);
                    _mapViewer.DrawLevel();
                }
            }
        }

        ushort[][] UpdateMapDimensions(ushort[][] OldTiles, ushort[][] NewTiles, ushort oldWidth, ushort oldHeight, ushort NewWidth, ushort NewHeight, int RSDKver)
        {
            // fill the extended tile arrays with "empty" values

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
                    ushort[][] NewTiles1 = new ushort[_RSDK1Background.Height][];
                    for (ushort i = 0; i < _RSDK1Background.Height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles1[i] = new ushort[_RSDK1Background.Width];
                        for (int j = 0; j < _RSDK1Background.Width; ++j)
                            NewTiles1[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK1Background.Layout = NewTiles1;
                    _mapViewer.DrawLevel();
                    break;
                case 2:
                    ushort[][] NewTiles2 = new ushort[_RSDK2Background.height][];
                    for (ushort i = 0; i < _RSDK2Background.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles2[i] = new ushort[_RSDK2Background.width];
                        for (int j = 0; j < _RSDK2Background.width; ++j)
                            NewTiles2[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK2Background.MapLayout = NewTiles2;
                    _mapViewer.DrawLevel();
                    break;
                case 1:
                    ushort[][] NewTiles3 = new ushort[_RSDK3Background.height][];
                    for (ushort i = 0; i < _RSDK3Background.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles3[i] = new ushort[_RSDK3Background.width];
                        for (int j = 0; j < _RSDK3Background.width; ++j)
                            NewTiles3[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK3Background.MapLayout = NewTiles3;
                    _mapViewer.DrawLevel();
                    break;
                case 0:
                    ushort[][] NewTiles4 = new ushort[_RSDK4Background.height][];
                    for (ushort i = 0; i < _RSDK4Background.height; i++)
                    {
                        // first create arrays child arrays to the width
                        // a little inefficient, but at least they'll all be equal sized
                        NewTiles4[i] = new ushort[_RSDK4Background.width];
                        for (int j = 0; j < _RSDK4Background.width; ++j)
                            NewTiles4[i][j] = 0xffff; // fill the tiles with blanks
                    }
                    _mapViewer._RSDK4Background.MapLayout = NewTiles4;
                    _mapViewer.DrawLevel();
                    break;
            }
        }
    }

}
