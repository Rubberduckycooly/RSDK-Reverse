using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.BackgroundEditor
{
    public partial class StageChunksView : DockContent
    {
        public int loadedRSDKver = 0;
        public Image _tiles;

        public List<Bitmap> Chunks = new List<Bitmap>();

        public StageMapView MapView;

        //private Point tilePoint;
        public int selectedChunk;

        #region Retro-Sonic Development Kit
        public RSDKv1.til _RSDK1Chunks;
        public RSDKv1.BGLayout _RSDK1Background;
        #endregion

        #region RSDKv1
        public RSDKv2.Tiles128x128 _RSDK2Chunks;
        public RSDKv2.BGLayout _RSDK2Background;
        #endregion

        #region RSDKv2
        public RSDKv3.Tiles128x128 _RSDK3Chunks;
        public RSDKv3.BGLayout _RSDK3Background;
        #endregion

        #region RSDKvB
        public RSDKv4.Tiles128x128 _RSDK4Chunks;
        public RSDKv4.BGLayout _RSDK4Background;
        #endregion

        public StageChunksView(StageMapView mpv)
        {
            InitializeComponent();
            MapView = mpv;
        }

        public void SetChunks()
        {
            LoadChunks(_tiles);
            Refresh();
        }

        void LoadChunks(Image Tiles)
        {
            switch (loadedRSDKver)
            {
                case 0:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK4Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK4Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                        //b.Dispose();
                    }
                    break;
                case 1:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK3Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK3Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 2:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK2Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK2Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 3:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK1Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK1Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 0, 0, 0));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                default:
                    break;
            }
            BlocksList.SelectedIndex = 0;
        }

        private void BlocksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedChunk = BlocksList.SelectedIndex;
            Console.WriteLine("New Chunk " + selectedChunk);
        }

        private void pValuesList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pValuesList_DoubleClick(object sender, EventArgs e)
        {
            LineScrollForm frm = new LineScrollForm(loadedRSDKver, pValuesList.SelectedIndex);
            switch(loadedRSDKver)
            {
                case 3:
                    frm.Mapv1 = _RSDK1Background;
                    break;
                case 2:
                    frm.Mapv2 = _RSDK2Background;
                    break;
                case 1:
                    frm.Mapv3 = _RSDK3Background;
                    break;
                case 0:
                    frm.Mapv4 = _RSDK4Background;
                    break;
            }
            frm.Setup();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                switch (loadedRSDKver)
                {
                    case 3:
                        _RSDK1Background = frm.Mapv1;
                        break;
                    case 2:
                        _RSDK2Background = frm.Mapv2;
                        break;
                    case 1:
                        _RSDK3Background = frm.Mapv3;
                        break;
                    case 0:
                        _RSDK4Background = frm.Mapv4;
                        break;
                }
                RefreshParallaxList();
            }

        }

        public void RefreshParallaxList()
        {
            pValuesList.Items.Clear();
            switch(loadedRSDKver)
            {
                case 3:
                    for (int i = 0; i < _RSDK1Background.Lines.Count; i++)
                    {
                        string line = _RSDK1Background.Lines[i].LineNo + " - " + _RSDK1Background.Lines[i].OverallSpeed + " - " + _RSDK1Background.Lines[i].Deform;
                        pValuesList.Items.Add(line);
                    }
                    break;
                case 2:
                    for (int i = 0; i < _RSDK2Background.Lines.Count; i++)
                    {
                        //string line = _RSDK2Background.Lines[i].LineNo + " - " + _RSDK2Background.Lines[i].OverallSpeed + " - " + _RSDK2Background.Lines[i].Deform;
                        //pValuesList.Items.Add(line);
                    }
                    break;
                case 1:
                    for (int i = 0; i < _RSDK3Background.Lines.Count; i++)
                    {
                        string line = _RSDK3Background.Lines[i].LineNo + " - " + _RSDK3Background.Lines[i].RelativeSpeed + " - " + _RSDK3Background.Lines[i].ConstantSpeed + " - " + _RSDK3Background.Lines[i].Unknown;
                        pValuesList.Items.Add(line);
                    }
                    break;
                case 0:
                    for (int i = 0; i < _RSDK4Background.Lines.Count; i++)
                    {
                        //string line = _RSDK4Background.Lines[i].LineNo + " - " + _RSDK4Background.Lines[i].OverallSpeed + " - " + _RSDK4Background.Lines[i].Deform;
                        //pValuesList.Items.Add(line);
                    }
                    break;
            }
        }

    }
}
