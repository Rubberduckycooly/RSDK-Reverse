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
        public Retro_Formats.EngineType engineType;
        public Image _tiles;

        public List<Bitmap> Chunks = new List<Bitmap>();

        public MainView Parent;

        //private Point tilePoint;
        public int selectedChunk;

        public StageChunksView(MainView p)
        {
            InitializeComponent();
            Parent = p;
        }

        public void SetChunks()
        {
            LoadChunks(_tiles);
            Refresh();
        }

        void LoadChunks(Image Tiles)
        {
            BlocksList.Images.Clear();
            for (int i = 0; i < Parent.Chunks.ChunkList.Length; i++)
            {
                Bitmap b = Parent.Chunks.ChunkList[i].Render(_tiles);
                //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                Chunks.Add(b);
                BlocksList.Images.Add(b);
                //b.Dispose();
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
            RSN_LineScrollForm frm1 = new RSN_LineScrollForm(engineType, HpValuesList.SelectedIndex, 0);
            CD12_LineScrollForm frm2 = new CD12_LineScrollForm(engineType, HpValuesList.SelectedIndex, 0);

            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS:
                    frm1.Background = Parent.background;
                    break;
                case Retro_Formats.EngineType.RSDKv1:
                    frm1.Background = Parent.background;
                    break;
                case Retro_Formats.EngineType.RSDKv2:
                    frm2.Background = Parent.background;
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    frm2.Background = Parent.background;
                    break;
            }
            if (engineType == Retro_Formats.EngineType.RSDKvRS || engineType == Retro_Formats.EngineType.RSDKv1)
            {
                frm1.Setup();
                if (frm1.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm1.RemoveVal)
                    {
                        Parent.background = frm1.Background;
                        RefreshParallaxList();
                    }
                    else if (frm1.RemoveVal)
                    {
                        Parent.background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                        RefreshParallaxList();
                    }
                }
            }
            else
            {
                frm2.Setup();
                if (frm2.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm2.RemoveVal)
                    {
                        Parent.background = frm2.Background;
                        RefreshParallaxList();
                    }
                    else if (frm2.RemoveVal)
                    {
                        Parent.background.HLines.RemoveAt(HpValuesList.SelectedIndex);
                        RefreshParallaxList();
                    }
                }
            }

        }

        private void VpValuesList_DoubleClick(object sender, EventArgs e)
        {
            RSN_LineScrollForm frm1 = new RSN_LineScrollForm(engineType, VpValuesList.SelectedIndex, 1);
            CD12_LineScrollForm frm2 = new CD12_LineScrollForm(engineType, VpValuesList.SelectedIndex, 1);

            switch (engineType)
            {
                case Retro_Formats.EngineType.RSDKvRS:
                    frm1.Background = Parent.background;
                    break;
                case Retro_Formats.EngineType.RSDKv1:
                    frm1.Background = Parent.background;
                    break;
                case Retro_Formats.EngineType.RSDKv2:
                    frm2.Background = Parent.background;
                    break;
                case Retro_Formats.EngineType.RSDKvB:
                    frm2.Background = Parent.background;
                    break;
            }
            if (engineType == Retro_Formats.EngineType.RSDKvRS || engineType == Retro_Formats.EngineType.RSDKv1)
            {
                frm1.Setup();
                if (frm1.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm1.RemoveVal)
                    {
                        Parent.background = frm1.Background;
                        RefreshParallaxList();
                    }
                    else if (frm1.RemoveVal)
                    {
                        Parent.background.VLines.RemoveAt(HpValuesList.SelectedIndex);
                        RefreshParallaxList();
                    }
                }
            }
            else
            {
                frm2.Setup();
                if (frm2.ShowDialog(this) == DialogResult.OK)
                {
                    if (!frm2.RemoveVal)
                    {
                        Parent.background = frm2.Background;
                        RefreshParallaxList();
                    }
                    else if (frm2.RemoveVal)
                    {
                        Parent.background.VLines.RemoveAt(HpValuesList.SelectedIndex);
                        RefreshParallaxList();
                    }
                }
            }

        }

        public void RefreshParallaxList()
        {
            HpValuesList.Items.Clear();
            VpValuesList.Items.Clear();

            for (int i = 0; i < Parent.background.HLines.Count; i++)
            {
                string line = "Line";
                if (engineType == Retro_Formats.EngineType.RSDKvRS || engineType == Retro_Formats.EngineType.RSDKv1) line = Parent.background.HLines[i].RelativeSpeed + " - " + Parent.background.HLines[i].ConstantSpeed + " - " + Parent.background.HLines[i].Behaviour;
                else line = Parent.background.HLines[i].RelativeSpeed + " - " + Parent.background.HLines[i].ConstantSpeed + " - " + Parent.background.HLines[i].Behaviour + " - " + Parent.background.HLines[i].DrawLayer;
                HpValuesList.Items.Add(line);
            }
            for (int i = 0; i < Parent.background.VLines.Count; i++)
            {
                string line = "Line";
                if (engineType == Retro_Formats.EngineType.RSDKvRS || engineType == Retro_Formats.EngineType.RSDKv1) line = Parent.background.VLines[i].RelativeSpeed + " - " + Parent.background.VLines[i].ConstantSpeed + " - " + Parent.background.VLines[i].Behaviour;
                else line = Parent.background.VLines[i].RelativeSpeed + " - " + Parent.background.VLines[i].ConstantSpeed + " - " + Parent.background.VLines[i].Behaviour + " - " + Parent.background.VLines[i].DrawLayer;
                VpValuesList.Items.Add(line);
            }
            RefreshLinePosList();
        }

        public void RefreshLinePosList()
        {
            LineNumberListBox.Items.Clear();

            for (int i = 0; i < Parent.background.Layers[Parent._mapViewer.curlayer].LineIndexes.Count; i++)
            {
                string line = "Line: " + Parent.background.Layers[Parent._mapViewer.curlayer].LineIndexes[i];
                LineNumberListBox.Items.Add(line);
            }
        }
    }
}
