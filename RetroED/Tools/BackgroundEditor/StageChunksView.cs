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
        public RSDKvRS.BGLayout _RSDKvRSBackground;
        #endregion

        #region RSDKv1
        public RSDKv1.Tiles128x128 _RSDK2Chunks;
        public RSDKv1.BGLayout _RSDKv1Background;
        #endregion

        #region RSDKv1
        public RSDKv2.Tiles128x128 _RSDK3Chunks;
        public RSDKv2.BGLayout _RSDKv2Background;
        #endregion

        #region RSDKvB
        public RSDKvB.Tiles128x128 _RSDK4Chunks;
        public RSDKvB.BGLayout _RSDKvBBackground;
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
                    frm1.BackgroundvRS = _RSDKvRSBackground;
                    break;
                case 2:
                    frm1.Backgroundv1 = _RSDKv1Background;
                    break;
                case 1:
                    frm2.Mapv3 = _RSDKv2Background;
                    break;
                case 0:
                    frm2.Mapv4 = _RSDKvBBackground;
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
                                _RSDKv2Background = frm2.Mapv3;
                                break;
                            case 0:
                                _RSDKvBBackground = frm2.Mapv4;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm2.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 1:
                                _RSDKv2Background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                                break;
                            case 0:
                                _RSDKvBBackground.HLines.RemoveAt(HpValuesList.SelectedIndex);
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
                                _RSDKvRSBackground = frm1.BackgroundvRS;
                                break;
                            case 2:
                                _RSDKv1Background = frm1.Backgroundv1;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm1.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 3:
                                _RSDKvRSBackground.HLines.RemoveAt(HpValuesList.SelectedIndex);
                                break;
                            case 2:
                                _RSDKv1Background.HLines.RemoveAt(HpValuesList.SelectedIndex);
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
                    frm1.BackgroundvRS = _RSDKvRSBackground;
                    break;
                case 2:
                    frm1.Backgroundv1 = _RSDKv1Background;
                    break;
                case 1:
                    frm2.Mapv3 = _RSDKv2Background;
                    break;
                case 0:
                    frm2.Mapv4 = _RSDKvBBackground;
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
                                _RSDKv2Background = frm2.Mapv3;
                                break;
                            case 0:
                                _RSDKvBBackground = frm2.Mapv4;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm2.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 1:
                                _RSDKv2Background.VLines.RemoveAt(VpValuesList.SelectedIndex);
                                break;
                            case 0:
                                _RSDKvBBackground.VLines.RemoveAt(VpValuesList.SelectedIndex);
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
                                _RSDKvRSBackground = frm1.BackgroundvRS;
                                break;
                            case 2:
                                _RSDKv1Background = frm1.Backgroundv1;
                                break;
                        }
                        RefreshParallaxList();
                    }
                    else if (frm1.RemoveVal)
                    {
                        switch (loadedRSDKver)
                        {
                            case 3:
                                _RSDKvRSBackground.VLines.RemoveAt(VpValuesList.SelectedIndex);
                                break;
                            case 2:
                                _RSDKv1Background.VLines.RemoveAt(VpValuesList.SelectedIndex);
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
                    for (int i = 0; i < _RSDKvRSBackground.HLines.Count; i++)
                    {
                        string line = _RSDKvRSBackground.HLines[i].RelativeSpeed + " - " + _RSDKvRSBackground.HLines[i].ConstantSpeed + " - " + _RSDKvRSBackground.HLines[i].Deform;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDKvRSBackground.VLines.Count; i++)
                    {
                        string line = _RSDKvRSBackground.VLines[i].RelativeSpeed + " - " + _RSDKvRSBackground.VLines[i].ConstantSpeed + " - " + _RSDKvRSBackground.VLines[i].Deform;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
                case 2:
                    for (int i = 0; i < _RSDKv1Background.HLines.Count; i++)
                    {
                        string line = _RSDKv1Background.HLines[i].RelativeSpeed + " - " + _RSDKv1Background.HLines[i].ConstantSpeed + " - " + _RSDKv1Background.HLines[i].Deform;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDKv1Background.VLines.Count; i++)
                    {
                        string line = _RSDKv1Background.VLines[i].RelativeSpeed + " - " + _RSDKv1Background.VLines[i].ConstantSpeed+ " - " + _RSDKv1Background.VLines[i].Deform;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
                case 1:
                    for (int i = 0; i < _RSDKv2Background.HLines.Count; i++)
                    {
                        string line = _RSDKv2Background.HLines[i].RelativeSpeed + " - " + _RSDKv2Background.HLines[i].ConstantSpeed + _RSDKv2Background.HLines[i].DrawLayer + " - " + _RSDKv2Background.HLines[i].Behaviour;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDKv2Background.VLines.Count; i++)
                    {
                        string line = _RSDKv2Background.VLines[i].RelativeSpeed + " - " + _RSDKv2Background.VLines[i].ConstantSpeed + " - " + _RSDKv2Background.VLines[i].DrawLayer + " - " + _RSDKv2Background.HLines[i].Behaviour;
                        VpValuesList.Items.Add(line);
                    }
                    RefreshLinePosList();
                    break;
                case 0:
                    for (int i = 0; i < _RSDKvBBackground.HLines.Count; i++)
                    {
                        string line = _RSDKvBBackground.HLines[i].RelativeSpeed + " - " + _RSDKvBBackground.HLines[i].ConstantSpeed + " - " + _RSDKvBBackground.HLines[i].DrawLayer + " - " + _RSDKvBBackground.HLines[i].Behaviour;
                        HpValuesList.Items.Add(line);
                    }
                    for (int i = 0; i < _RSDKvBBackground.VLines.Count; i++)
                    {
                        string line = _RSDKvBBackground.VLines[i].RelativeSpeed + " - " + _RSDKvBBackground.VLines[i].ConstantSpeed + " - " + _RSDKvBBackground.VLines[i].DrawLayer + " - " + _RSDKvBBackground.VLines[i].Behaviour;
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
                    for (int i = 0; i < _RSDKvRSBackground.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDKvRSBackground.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
                case 2:
                    for (int i = 0; i < _RSDKv1Background.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDKv1Background.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
                case 1:
                    for (int i = 0; i < _RSDKv2Background.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDKv2Background.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
                case 0:
                    for (int i = 0; i < _RSDKvBBackground.Layers[MapView.curlayer].LineIndexes.Count; i++)
                    {
                        string line = "Line: " + _RSDKvBBackground.Layers[MapView.curlayer].LineIndexes[i];
                        LineNumberListBox.Items.Add(line);
                    }
                    break;
            }

        }
    }
}
