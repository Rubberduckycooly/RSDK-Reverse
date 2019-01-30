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

        //A List of all the chunks (in image form)
        public List<Bitmap> Chunks = new List<Bitmap>();

        public MainView Parent;

        //What Chunk is selected?
        public int selectedChunk;

        public StageChunksView(MainView p)
        {
            InitializeComponent();
            Parent = p;
        }

        public void SetChunks() //Load Chunks. Used for initializing
        {
            LoadChunks(Parent._loadedTiles);
            Refresh();
        }

        void LoadChunks(Image Tiles) //Load the chunks into a list!
        {
            BlocksList.Images.Clear(); //Chunk zero should be ID Zero
            for (int i = 0; i < Parent.Chunks.ChunkList.Length; i++)
            {
                Bitmap b = Parent.Chunks.ChunkList[i].Render(Parent._loadedTiles); //Render chunk using the tileset as a source
                                                                            //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                Chunks.Add(b); //Add it to the list of chunk images
                BlocksList.Images.Add(b); //add it to the "List" of chunks that the user selects from
                                          //b.Dispose();
            }
            BlocksList.SelectedIndex = 0;
        }

        private void BlocksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedChunk = BlocksList.SelectedIndex; //Update the selected chunk
        }
    }
}
