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
        public RSDKvRS.Tiles128x128 _RSDK1Chunks;
        public RSDKvRS.BGLayout _RSDK1Background;
        #endregion

        #region RSDKv1
        public RSDKv1.Tiles128x128 _RSDK2Chunks;
        public RSDKv1.BGLayout _RSDK2Background;
        #endregion

        #region RSDKv1
        public RSDKv2.Tiles128x128 _RSDK3Chunks;
        public RSDKv2.BGLayout _RSDK3Background;
        #endregion

        #region RSDKvB
        public RSDKvB.Tiles128x128 _RSDK4Chunks;
        public RSDKvB.BGLayout _RSDK4Background;
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
                    for (int i = 0; i < _RSDK4Chunks.BlockList.Length; i++)
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
                    for (int i = 0; i < _RSDK3Chunks.BlockList.Length; i++)
                    {
                        Bitmap b = _RSDK3Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 2:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK2Chunks.BlockList.Length; i++)
                    {
                        Bitmap b = _RSDK2Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 3:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK1Chunks.BlockList.Length; i++)
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

        private void HpValuesList_DoubleClick(object sender, EventArgs e)
        {
            RSN_LineScrollForm frm1 = new RSN_LineScrollForm(loadedRSDKver, HpValuesList.SelectedIndex, 0);
            CD12_LineScrollForm frm2 = new CD12_LineScrollForm(loadedRSDKver, HpValuesList.SelectedIndex, 0);

            switch (loadedRSDKver)
            {
                case 3:
                    frm1.Mapv1 = _RSDK1Background;
                    break;
                case 2:
                    frm1.Mapv2 = _RSDK2Background;
                    break;
                case 1:
                    frm2.Mapv3 = _RSDK3Background;
                    break;
                case 0:
                    frm2.Mapv4 = _RSDK4Background;
                    break;
            }
            if (loadedRSDKver <= 1)
            {
                frm2.Setup();
                if (frm2.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm2.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 1:
                                _RSDK3Background = frm2.Mapv3;
                                break;
                            case 0:
                                _RSDK4Background = frm2.Mapv4;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm2.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 1:
                                _RSDK3Background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                                break;
                            case 0:
                                _RSDK4Background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                                break;
                        }
                        RefreshParallaxList();
                    }
                }
            }
            if (loadedRSDKver >= 2)
            {
                frm1.Setup();
                if (frm1.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm1.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 3:
                                _RSDK1Background = frm1.Mapv1;
                                break;
                            case 2:
                                _RSDK2Background = frm1.Mapv2;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm1.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 3:
                                _RSDK1Background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                                break;
                            case 2:
                                _RSDK2Background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                                break;
                        }
                        RefreshParallaxList();
                    }
                }
            }

        }

        private void VpValuesList_DoubleClick(object sender, EventArgs e)
        {
            RSN_LineScrollForm frm1 = new RSN_LineScrollForm(loadedRSDKver, VpValuesList.SelectedIndex, 1);
            CD12_LineScrollForm frm2 = new CD12_LineScrollForm(loadedRSDKver, VpValuesList.SelectedIndex, 1);

            switch (loadedRSDKver)
            {
                case 3:
                    frm1.Mapv1 = _RSDK1Background;
                    break;
                case 2:
                    frm1.Mapv2 = _RSDK2Background;
                    break;
                case 1:
                    frm2.Mapv3 = _RSDK3Background;
                    break;
                case 0:
                    frm2.Mapv4 = _RSDK4Background;
                    break;
            }
            if (loadedRSDKver <= 1)
            {
                frm2.Setup();
                if (frm2.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm2.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 1:
                                _RSDK3Background = frm2.Mapv3;
                                break;
                            case 0:
                                _RSDK4Background = frm2.Mapv4;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm2.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 1:
                                _RSDK3Background.VLines.RemoveAt(VpValuesList.SelectedIndex);
                                break;
                            case 0:
                                _RSDK4Background.VLines.RemoveAt(VpValuesList.SelectedIndex);
                                break;
                        }
                        RefreshParallaxList();
                    }
                }
            }
            if (loadedRSDKver >= 2)
            {
                frm1.Setup();
                if (frm1.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm1.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 3:
                                _RSDK1Background = frm1.Mapv1;
                                break;
                            case 2:
                                _RSDK2Background = frm1.Mapv2;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm1.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 3:
                                _RSDK1Background.VLines.RemoveAt(VpValuesList.SelectedIndex);
                                break;
                            case 2:
                                _RSDK2Background.VLines.RemoveAt(VpValuesList.SelectedIndex);
                                break;
                        }
                        RefreshParallaxList();
                    }
                }
            }

        }

        public void RefreshParallaxList()
        {
            HpValuesList.Items.Clear();
            VpValuesList.Items.Clear();

            switch (loadedRSDKver)
            {
                case 3:
                    for (int i = 0; i < _RSDK1Background.HLines.Count; i++)
                    {
                        string line = _RSDK1Background.HLines[i].RHSpeed + " - " + _RSDK1Background.HLines[i].CHSpeed + " - " + _RSDK1Background.HLines[i].Deform;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDK1Background.VLines.Count; i++)
                    {
                        string line = _RSDK1Background.VLines[i].RHSpeed + " - " + _RSDK1Background.VLines[i].CHSpeed + " - " + _RSDK1Background.VLines[i].Deform;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
                case 2:
                    for (int i = 0; i < _RSDK2Background.HLines.Count; i++)
                    {
                        string line = _RSDK2Background.HLines[i].RHSpeed + " - " + _RSDK2Background.HLines[i].CHSpeed + " - " + _RSDK2Background.HLines[i].Deform;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDK2Background.VLines.Count; i++)
                    {
                        string line = _RSDK2Background.VLines[i].RHSpeed + " - " + _RSDK2Background.VLines[i].CHSpeed + " - " + _RSDK2Background.VLines[i].Deform;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
                case 1:
                    for (int i = 0; i < _RSDK3Background.HLines.Count; i++)
                    {
                        string line = _RSDK3Background.HLines[i].RelativeSpeed + " - " + _RSDK3Background.HLines[i].ConstantSpeed + _RSDK3Background.HLines[i].Unknown1 + " - " + _RSDK3Background.HLines[i].Unknown;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDK3Background.VLines.Count; i++)
                    {
                        string line = _RSDK3Background.VLines[i].RelativeSpeed + " - " + _RSDK3Background.VLines[i].ConstantSpeed + " - " + _RSDK3Background.VLines[i].Unknown1 + " - " + _RSDK3Background.HLines[i].Unknown;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
                case 0:
                    for (int i = 0; i < _RSDK4Background.HLines.Count; i++)
                    {
                        string line = _RSDK4Background.HLines[i].RelativeSpeed + " - " + _RSDK4Background.HLines[i].ConstantSpeed + " - " + _RSDK4Background.HLines[i].Unknown1 + " - " + _RSDK4Background.HLines[i].Unknown;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDK4Background.VLines.Count; i++)
                    {
                        string line = _RSDK4Background.VLines[i].RelativeSpeed + " - " + _RSDK4Background.VLines[i].ConstantSpeed + " - " + _RSDK4Background.VLines[i].Unknown1 + " - " + _RSDK4Background.VLines[i].Unknown;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
            }
        }

        public void RefreshLinePosList()
        {
            LineNumberListBox.Items.Clear();

            switch (loadedRSDKver)
            {
                case 3:
                    for (int i = 0; i < _RSDK1Background.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDK1Background.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
                case 2:
                    for (int i = 0; i < _RSDK2Background.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDK2Background.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
                case 1:
                    for (int i = 0; i < _RSDK3Background.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDK3Background.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
                case 0:
                    for (int i = 0; i < _RSDK4Background.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDK4Background.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
            }

        }
    }
}
