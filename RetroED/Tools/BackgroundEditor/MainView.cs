using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.BackgroundEditor
{
    public partial class MainView : DockContent
    {
        enum Placementmode
        {
            NONE,
            PlaceTiles,
        }

        public Retro_Formats.EngineType engineType;

        private Image _loadedTiles;
        public StageChunksView _blocksViewer;
        public StageMapView _mapViewer;

        int curlayer = 0;

        public bool DrawLines = false;

        string tiles;
        string mappings;
        string Background;

        bool showgrid = false;
        int PlacementMode = 0;

        public Retro_Formats.Background background = new Retro_Formats.Background();
        public Retro_Formats.MetaTiles Chunks = new Retro_Formats.MetaTiles();

        public MainView()
        {
            InitializeComponent();
            _mapViewer = new StageMapView(this);
            _mapViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            _blocksViewer = new StageChunksView(this);
            _blocksViewer.Show(dpMain, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Sonic 1/Sonic 2 Background files (Backgrounds.bin)|Backgrounds.bin|Sonic CD Background files (Backgrounds.bin)|Backgrounds.bin|Sonic Nexus Background files (Backgrounds.bin)|Backgrounds.bin|Retro-Sonic Background files (ZoneBG.map)|ZoneBG.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                switch(ofd.FilterIndex - 1)
                {
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                }

                if (engineType == Retro_Formats.EngineType.RSDKvRS)
                {
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.gfx");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "Zone.til");
                    Background = ofd.FileName;
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(Background))
                    {
                        LoadLevel(ofd.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Tiles and Mappings need to exist in the same folder as act data, just like the game.");
                    }
                }

                if (engineType != Retro_Formats.EngineType.RSDKvRS)
                {
                    tiles = Path.Combine(Path.GetDirectoryName(ofd.FileName), "16x16Tiles.gif");
                    mappings = Path.Combine(Path.GetDirectoryName(ofd.FileName), "128x128Tiles.bin");
                    Background = ofd.FileName;
                    if (File.Exists(tiles) && File.Exists(mappings) && File.Exists(Background))
                    {
                        LoadLevel(ofd.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Tiles and Mappings need to exist in the same folder as act data, just like the game.");
                    }
                }

                string filename = Path.GetFileName(ofd.FileName);

                string dispname = "";

                if (filename != null)
                {
                    string dir = "";
                    string pth = Path.GetFileName(ofd.FileName);
                    string tmp = ofd.FileName.Replace(pth, "");
                    DirectoryInfo di = new DirectoryInfo(tmp);
                    dir = di.Name;
                    RetroED.MainForm.Instance.CurrentTabText = dir + "/" + pth;
                    dispname = dir + "/" + pth;
                }
                else
                {
                    RetroED.MainForm.Instance.CurrentTabText = "New Scene - RSDK Background Editor";
                    dispname = "New Scene - RSDK Background Editor";
                }

                RetroED.MainForm.Instance.rp.state = "RetroED - " + this.Text;
                switch (engineType)
                {
                    case Retro_Formats.EngineType.RSDKvB:
                        RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (RSDKvB)";
                        break;
                    case Retro_Formats.EngineType.RSDKv2:
                        RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (RSDKv2)";
                        break;
                    case Retro_Formats.EngineType.RSDKv1:
                        RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (RSDKv1)";
                        break;
                    case Retro_Formats.EngineType.RSDKvRS:
                        RetroED.MainForm.Instance.rp.details = "Editing: " + dispname + " (RSDKvRS)";
                        break;
                }
                SharpPresence.Discord.RunCallbacks();
                SharpPresence.Discord.UpdatePresence(RetroED.MainForm.Instance.rp);

            }
        }

        void LoadLevel(string level)
        {
            background.ImportFrom(engineType, level);
            Chunks.ImportFrom(engineType, mappings);
            using (var fs = new FileStream(tiles, FileMode.Open))
            {
                var bmp = new Bitmap(fs);
                _loadedTiles = (Bitmap)bmp.Clone();
            }
            _blocksViewer._tiles = _loadedTiles;
            _blocksViewer.SetChunks();
            _blocksViewer.RefreshParallaxList();

            _mapViewer._tiles = _loadedTiles;
            _mapViewer.SetLevel();
            _mapViewer.DrawLevel();
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (background != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image (*.png)|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap massive = new Bitmap(background.Layers[curlayer].MapLayout[0].Length * 128, background.Layers[curlayer].MapLayout.Length * 128);
                    using (Graphics g = Graphics.FromImage(massive))
                    {
                        for (int y = 0; y < background.Layers[curlayer].MapLayout.Length; y++)
                        {
                            for (int x = 0; x < background.Layers[curlayer].MapLayout[0].Length; x++)
                            {
                                g.DrawImage(Chunks.ChunkList[background.Layers[curlayer].MapLayout[y][x]].Render(_loadedTiles), x * 128, y * 128);
                            }
                        }
                    }
                    massive.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    massive.Dispose();
                    GC.Collect();
                }
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
                background.ExportTo(engineType,Background);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Sonic 1/Sonic 2 Background files (Backgrounds.bin)|Backgrounds.bin|Sonic CD Background files (Backgrounds.bin)|Backgrounds.bin|Sonic Nexus Background files (Backgrounds.bin)|Backgrounds.bin|Retro-Sonic Background files (ZoneBG.map)|ZoneBG.map";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                switch (dlg.FilterIndex - 1)
                {
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                }
                Background = dlg.FileName;
                background.ExportTo(engineType, Background);
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
            RSN_LayerPropertiesForm frm1 = new RSN_LayerPropertiesForm(engineType);
            CD12_LayerPropertiesForm frm2 = new CD12_LayerPropertiesForm(engineType);

            frm1.CurLayer = _mapViewer.curlayer;
            frm2.CurLayer = _mapViewer.curlayer;
            ushort[][] OldTiles = new ushort[0][];
            ushort[][] NewTiles = new ushort[0][];
            int OLDwidth = 0;
            int OLDheight = 0;

            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS:
                    OldTiles = background.Layers[curlayer].MapLayout;
                    OLDwidth = background.Layers[curlayer].width;
                    OLDheight = background.Layers[curlayer].height;
                    frm1.Background = background;
                    frm1.Setup();
                    break;
                case Retro_Formats.EngineType.RSDKv1:
                    OldTiles = background.Layers[curlayer].MapLayout;
                    OLDwidth = background.Layers[curlayer].width;
                    OLDheight = background.Layers[curlayer].height;
                    frm1.Background = background;
                    frm1.Setup();
                    break;
                case Retro_Formats.EngineType.RSDKv2:
                    OldTiles = background.Layers[curlayer].MapLayout;
                    OLDwidth = background.Layers[curlayer].width;
                    OLDheight = background.Layers[curlayer].height;
                    frm2.Background = background;
                    frm2.Setup();
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    OldTiles = background.Layers[curlayer].MapLayout;
                    OLDwidth = background.Layers[curlayer].width;
                    OLDheight = background.Layers[curlayer].height;
                    frm2.Background = background;
                    frm2.Setup();
                    break;
                default:
                    break;
            }

            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS:
                    if (frm1.ShowDialog(this) == DialogResult.OK)
                    {
                                background = frm1.Background;
                                NewTiles = background.Layers[curlayer].MapLayout;
                                CheckDimensions(engineType, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
                case Retro_Formats.EngineType.RSDKv1:
                    if (frm1.ShowDialog(this) == DialogResult.OK)
                    {
                                background = frm1.Background;
                                NewTiles = background.Layers[curlayer].MapLayout;
                                CheckDimensions(engineType, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
                case Retro_Formats.EngineType.RSDKv2:
                    if (frm2.ShowDialog(this) == DialogResult.OK)
                    {
                                background = frm2.Background;
                                NewTiles = background.Layers[curlayer].MapLayout;
                                CheckDimensions(engineType, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    if (frm2.ShowDialog(this) == DialogResult.OK)
                    {
                                background = frm2.Background;
                                NewTiles = background.Layers[curlayer].MapLayout;
                                CheckDimensions(engineType, OldTiles, NewTiles, OLDwidth, OLDheight);
                                _mapViewer.DrawLevel();
                    }
                    break;
            }
        }

        void CheckDimensions(Retro_Formats.EngineType RSDKver, ushort[][] OldTiles, ushort[][] NewTiles, int OLDwidth, int OLDheight)
        {
            if (RSDKver == Retro_Formats.EngineType.RSDKvRS)
            {
                if (background.Layers[curlayer].width != OLDwidth || background.Layers[curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)background.Layers[curlayer].width, (ushort)background.Layers[curlayer].height, RSDKver);
                }
            }
            if (RSDKver == Retro_Formats.EngineType.RSDKv1)
            {
                if (background.Layers[_mapViewer.curlayer].width != OLDwidth || background.Layers[_mapViewer.curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)background.Layers[_mapViewer.curlayer].width, (ushort)background.Layers[_mapViewer.curlayer].height, RSDKver);
                }

            }
            if (RSDKver == Retro_Formats.EngineType.RSDKv2)
            {
                if (background.Layers[curlayer].width != OLDwidth || background.Layers[curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)background.Layers[curlayer].width, (ushort)background.Layers[curlayer].height, RSDKver);
                }
            }
            if (RSDKver == Retro_Formats.EngineType.RSDKvB)
            {
                if (background.Layers[curlayer].width != OLDwidth || background.Layers[curlayer].height != OLDheight)
                {
                    Console.WriteLine("Different");
                    background.Layers[curlayer].MapLayout = UpdateMapDimensions(OldTiles, NewTiles, (ushort)OLDwidth, (ushort)OLDheight, (ushort)background.Layers[curlayer].width, (ushort)background.Layers[curlayer].height, RSDKver);
                }
            }
        }

        ushort[][] UpdateMapDimensions(ushort[][] OldTiles, ushort[][] NewTiles, ushort oldWidth, ushort oldHeight, ushort NewWidth, ushort NewHeight, Retro_Formats.EngineType RSDKver)
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
            return NewTiles;
        }

        private void MenuItem_ShowGrid_Click(object sender, EventArgs e)
        {
            if (!showgrid)
            {
                MenuItem_ShowGrid.Checked = true;
                showgrid = true;
                _mapViewer.ShowGrid = true;
                _mapViewer.DrawLevel();
            }
            else if (showgrid)
            {
                MenuItem_ShowGrid.Checked = false;
                showgrid = false;
                _mapViewer.ShowGrid = false;
                _mapViewer.DrawLevel();
            }
        }

        private void refreshChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chunks.ImportFrom(engineType, mappings);
            using (var fs = new System.IO.FileStream(tiles, System.IO.FileMode.Open))
            {
                var bmp = new Bitmap(fs);
                _loadedTiles = (Bitmap)bmp.Clone();
            }
            _blocksViewer._tiles = _loadedTiles;
            _blocksViewer.SetChunks();

            _mapViewer._tiles = _loadedTiles;
            _mapViewer.SetLevel();
        }

        private void clearChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ushort[][] NewTiles = new ushort[background.Layers[curlayer].height][];
            for (ushort i = 0; i < background.Layers[curlayer].height; i++)
            {
                // first create arrays child arrays to the width
                // a little inefficient, but at least they'll all be equal sized
                NewTiles[i] = new ushort[background.Layers[curlayer].width];
                for (int j = 0; j < background.Layers[curlayer].width; ++j)
                    NewTiles[i][j] = 0; // fill the tiles with blanks
            }
            background.Layers[curlayer].MapLayout = NewTiles;
            _mapViewer.DrawLevel();
        }

        private void MenuItem_DrawAllLayers_Click(object sender, EventArgs e)
        {
            if (_mapViewer.DrawAllLayers)
            {
                MenuItem_DrawAllLayers.Checked = _mapViewer.DrawAllLayers = false;
                _mapViewer.DrawLevel();
            }
            else
            {
                MenuItem_DrawAllLayers.Checked = _mapViewer.DrawAllLayers = true;
                _mapViewer.DrawLevel();
            }
        }

        private void MenuItem_SelectLayer_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //curlayer = MenuItem_SelectLayer.MenuItems.IndexOf(e.ClickedItem);
            //_mapViewer.curlayer = MenuItem_SelectLayer.MenuItems.IndexOf(e.ClickedItem);
            //_mapViewer.DrawLevel();
        }

        private void clearScrollInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            background.HLines.Clear();
            _blocksViewer.RefreshParallaxList();
        }

        private void addParallaxValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            background.HLines.Add(new Retro_Formats.Background.ScrollInfo());
            _blocksViewer.RefreshParallaxList();
        }

        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            background.Layers.Add(new Retro_Formats.Background.BackgroundLayer());
            /*MenuItem_SelectLayer.MenuItems.Clear();
            for (int i = 0; i < background.Layers.Count; i++)
            {
                MenuItem_SelectLayer.MenuItems.Add("Background Layer " + i.ToString());
            }*/
        }

        private void removeCurrentLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            background.Layers.RemoveAt(curlayer);
            curlayer = 0;
            /*MenuItem_SelectLayer.MenuItems.Clear();
            for (int i = 0; i < background.Layers.Count; i++)
            {
                MenuItem_SelectLayer.MenuItems.Add("Background Layer " + i.ToString());
            }*/
            _mapViewer.DrawLevel();
        }

        private void MenuItem_SelectLayer_Click(object sender, EventArgs e)
        {
            SelectLayerForm SLF = new SelectLayerForm();
            SLF.SelLayer = curlayer;
            for (int i = 0; i < background.Layers.Count; i++)
            {
                SLF.LayerList.Items.Add("Background Layer " + i);
            }

            if (SLF.ShowDialog(this) == DialogResult.OK)
            {
                _mapViewer.curlayer = curlayer = SLF.SelLayer;
                Console.WriteLine(SLF.SelLayer + " " + _mapViewer.curlayer + " " + curlayer);
                _blocksViewer.RefreshLinePosList();
                _mapViewer.DrawLevel();
            }

        }

        private void MenuItem_AddVPValues_Click(object sender, EventArgs e)
        {
            background.VLines.Add(new Retro_Formats.Background.ScrollInfo());
            _blocksViewer.RefreshParallaxList();
        }

        private void MenuItem_ClearVPvalues_Click(object sender, EventArgs e)
        {
            background.VLines.Clear();
            _blocksViewer.RefreshParallaxList();
        }

        private void MenuItem_ClearHPValues_Click(object sender, EventArgs e)
        {
            background.HLines.Clear();
            background.VLines.Clear();
            _blocksViewer.RefreshParallaxList();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            
            if (!menuItem4.Checked)
            {
                DrawLines = true;
                menuItem4.Checked = _mapViewer.DrawLines;
                _mapViewer.DrawLines = true;
                _mapViewer.DrawLevel();
            }
            else
            {
                DrawLines = false;
                menuItem4.Checked = _mapViewer.DrawLines;
                _mapViewer.DrawLines = false;
                _mapViewer.DrawLevel();
            }
        }

        private void menuItem_RedrawLayer_Click(object sender, EventArgs e)
        {
            _mapViewer.DrawLevel();
        }
    }

}
