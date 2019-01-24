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
        //What RSDK version are we using?
        public int loadedRSDKver = 0;

        //The Stage's Tileset
        public Image _tiles;

        //A List of all the chunks (in image form)
        public List<Bitmap> Chunks = new List<Bitmap>();

        //a reference to The Map view
        public StageMapView MapView;

        public MainView Parent;

        //What Chunk is selected?
        public int selectedChunk;

        public StageChunksView(StageMapView mpv,MainView p)
        {
            InitializeComponent();
            MapView = mpv;
            Parent = p;
        }

        public void SetChunks() //Load Chunks. Used for initializing
        {
            LoadChunks(_tiles);
            Refresh();
        }

        void LoadChunks(Image Tiles) //Load the chunks into a list!
        {
            switch (loadedRSDKver)
            {
                case 0:
                    BlocksList.Images.Clear(); //Chunk zero should be ID Zero
                    for (int i = 0; i < Parent._RSDKBChunks.BlockList.Length; i++)
                    {
                        Bitmap b = Parent._RSDKBChunks.BlockList[i].Render(_tiles); //Render chunk using the tileset as a source
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b); //Add it to the list of chunk images
                        BlocksList.Images.Add(b); //add it to the "List" of chunks that the user selects from
                        //b.Dispose();
                    }
                    break;
                case 1:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < Parent._RSDK2Chunks.BlockList.Length; i++)
                    {
                        Bitmap b = Parent._RSDK2Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 2:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < Parent._RSDK1Chunks.BlockList.Length; i++)
                    {
                        Bitmap b = Parent._RSDK1Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 3:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < Parent._RSDKRSChunks.BlockList.Length; i++)
                    {
                        Bitmap b = Parent._RSDKRSChunks.BlockList[i].Render(_tiles);
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
            selectedChunk = BlocksList.SelectedIndex; //Update the selected chunk
        }
    }
}
