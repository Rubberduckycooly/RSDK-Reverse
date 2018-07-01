using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RetroED.Tools.MapEditor
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
        #endregion

        #region RSDKv1
        public RSDKv2.Tiles128x128 _RSDK2Chunks;
        #endregion

        #region RSDKv2
        public RSDKv3.Tiles128x128 _RSDK3Chunks;
        #endregion

        #region RSDKvB
        public RSDKv4.Tiles128x128 _RSDK4Chunks;
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
                        //b.Dispose();
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
                        //b.Dispose();
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
                        //b.Dispose();
                    }
                    break;
                default:
                    break;
            }
            BlocksList.SelectedIndex = 0;
        }

        private void BlocksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlocksList.SelectedIndex >= 0 && BlocksList.SelectedIndex != selectedChunk)
            {
            selectedChunk = BlocksList.SelectedIndex;
            MapView.SetChunk(MapView.tilePoint, (ushort)selectedChunk);
            }

        }
    }
}
