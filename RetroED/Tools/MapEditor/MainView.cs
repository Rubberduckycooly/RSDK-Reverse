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
        public int LoadedRSDKver = 0;

        private Image _loadedTiles;
        private StageChunksView _blocksViewer;
        private StageMapView _mapViewer;

        string tiles;
        string mappings;
        string Map;
        string Background;
        string CollisionMasks;

        #region Retro-Sonic Development Kit
        RSDKv1.Level _RSDK1Level;
        RSDKv1.BGLayout _RSDK1Background;
        RSDKv1.til _RSDK1Chunks;
        RSDKv1.tcf _RSDK1CollisionMask;
        #endregion

        #region RSDKv1
        RSDKv2.Level _RSDK2Level;
        RSDKv2.BGLayout _RSDK2Background;
        RSDKv2.Tiles128x128 _RSDK2Chunks;
        RSDKv2.CollisionMask _RSDK2CollisionMask;
        #endregion

        #region RSDKv2
        RSDKv3.Level _RSDK3Level;
        RSDKv3.BGLayout _RSDK3Background;
        RSDKv3.Tiles128x128 _RSDK3Chunks;
        RSDKv3.CollisionMask _RSDK3CollisionMask;
        #endregion

        #region RSDKvB
        RSDKv4.Level _RSDK4Level;
        RSDKv4.BGLayout _RSDK4Background;
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
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Sonic 1/Sonic 2 Act#.bin files (Act*.bin)|Act*.bin|Sonic CD Act#.bin files (Act*.bin)|Act*.bin|Sonic Nexus Act#.bin files (Act*.bin)|Act*.bin|Retro-Sonic Act#.map files (Act*.map)|Act*.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadedRSDKver = ofd.FilterIndex - 1;

                if (LoadedRSDKver == 3)
                {
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.gfx");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.til");
                    Map = ofd.FileName;
                    Background = Path.Combine(Path.GetDirectoryName(ofd.FileName), "ZoneBG.map");
                    CollisionMasks = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.tcf");
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(Background) && File.Exists(CollisionMasks))
                    {
                        LoadLevel(ofd.FileName, LoadedRSDKver);
                    }
                    else
                    {
                        MessageBox.Show("Tiles, Mappings, Backgrounds and Collision Masks need to exist in the same folder as act data, just like the game.");
                    }
                }

                if (LoadedRSDKver != 3)
                {
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "16x16Tiles.gif");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "128x128Tiles.bin");
                    Map = ofd.FileName;
                    Background = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Backgrounds.bin");
                    CollisionMasks = Path.Combine(Path.GetDirectoryName(ofd.FileName), "CollisionMasks.bin");
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(Background) && File.Exists(CollisionMasks))
                    {
                        LoadLevel(ofd.FileName, LoadedRSDKver);
                    }
                    else
                    {
                        MessageBox.Show("Tiles, Mappings, Backgrounds and Collision Masks need to exist in the same folder as act data, just like the game.");
                    }
                }
            }
        }

        void LoadLevel(string level, int RSDKver)
        {
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

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK4Level = _RSDK4Level;
                    _mapViewer._RSDK4Chunks = _RSDK4Chunks;
                    _mapViewer._RSDK4Background = _RSDK4Background;
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

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK3Level = _RSDK3Level;
                    _mapViewer._RSDK3Chunks = _RSDK3Chunks;
                    _mapViewer._RSDK3Background = _RSDK3Background;
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

                    _mapViewer._tiles = _loadedTiles;
                    _mapViewer._RSDK2Level = _RSDK2Level;
                    _mapViewer._RSDK2Chunks = _RSDK2Chunks;
                    _mapViewer._RSDK2Background = _RSDK2Background;
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

                    _mapViewer.loadedRSDKver = LoadedRSDKver;
                    _mapViewer._tiles = gfx.gfxImage;
                    _mapViewer._RSDK1Background = _RSDK1Background;
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
            switch (LoadedRSDKver)
            {
                case 0:
                    if (_RSDK4Level != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PNG Image (*.png)|*.png";
                        if (sfd.ShowDialog() == DialogResult.OK)
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
                        if (sfd.ShowDialog() == DialogResult.OK)
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
                        if (sfd.ShowDialog() == DialogResult.OK)
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
                        if (sfd.ShowDialog() == DialogResult.OK)
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mapLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLayersToolStripMenuItem.Checked)
            {
                mapLayersToolStripMenuItem.Checked = false;
                _mapViewer.ShowMap = false;
            }
            else if (!mapLayersToolStripMenuItem.Checked)
            {
                mapLayersToolStripMenuItem.Checked = true;
                _mapViewer.ShowMap = true;
            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Sonic 1/Sonic 2 Act#.bin files (Act*.bin)|Act*.bin|Sonic CD Act#.bin files (Act*.bin)|Act*.bin|Sonic Nexus Act#.bin files (Act*.bin)|Act*.bin|Retro-Sonic Act#.map files (Act*.map)|Act*.map";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (LoadedRSDKver)
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
    }
}
