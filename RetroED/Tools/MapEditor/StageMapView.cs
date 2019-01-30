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
    public partial class StageMapView : DockContent
    {

        //Where is the data folder?
        public string datapath;

        //0 is nothing, 1 for Chunks, 2 for objects
        public int PlacementMode = 0;

        //Used for DrawMap()
        private Bitmap PanelMap;
        
        //What do we want to show?
        public bool ShowMap = true;
        public bool ShowObjects = false;
        public bool ShowCollisionA = false;
        public bool ShowCollisionB = false;
        public bool ShowGrid = false;

        public List<Bitmap> CollisionPathA = new List<Bitmap>();
        public List<Bitmap> CollisionPathB = new List<Bitmap>();

        //Built-In Object Definitions for your convenience ;)

        //Retro-Sonic Global Objects
        public Object_Definitions.ObjectDefinitions ObjectDefinitions = new Object_Definitions.ObjectDefinitions();

        public List<Bitmap> Chunks = new List<Bitmap>();

        public Point tilePoint;

        public MainView Parent;

        public StageMapView(MainView p)
        {
            InitializeComponent();
            pnlMap.Paint += pnlMap_Paint; // Paint on the Form!
            pnlMap.Resize += ((sender, e) => Refresh()); //Resize the form (I think)
            Parent = p;
        }

        void pnlMap_Paint(object sender, PaintEventArgs e)
        {
            Size gridCellSize = new Size(128, 128);
            Pen p = new Pen(System.Drawing.Color.Yellow);

            if (Parent.Scene == null) return;
            e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255));
            Rectangle viewport4 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
            int startX4 = (viewport4.X / 128) - 1;
            if (startX4 < 0) startX4 = 0;
            int startY4 = (viewport4.Y / 128) - 1;
            if (startY4 < 0) startY4 = 0;
            int endX4 = startX4 + (pnlMap.Width / 128) + 1;
            int endY4 = startY4 + (pnlMap.Height / 128) + 1;
            for (int y = startY4; y < endY4; y++)
            {
                for (int x = startX4; x < endX4; x++)
                {
                    if (y < Parent.Scene.MapLayout.Length && x < Parent.Scene.MapLayout[y].Length && Parent.Scene.MapLayout[y][x] < Parent.Chunks.ChunkList.Length)
                    {
                        if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                        {
                            if (ShowMap)
                            {
                                if (Parent.Scene.MapLayout[y][x] > 0)
                                {
                                    e.Graphics.DrawImageUnscaled(Chunks[Parent.Scene.MapLayout[y][x]], x * 128, y * 128);
                                }
                                else { }
                            }
                            if (ShowCollisionA)
                            {
                                ushort Chunk = Parent.Scene.MapLayout[y][x];
                                for (int h = 0; h < 8; h++)
                                {
                                    for (int w = 0; w < 8; w++)
                                    {
                                        if (Parent.Scene.MapLayout[y][x] > 0)
                                        {
                                            int tx, ty;
                                            tx = (x * 8) + w;
                                            ty = (y * 8) + h;

                                            Bitmap cm = CollisionPathA[Parent.Chunks.ChunkList[Chunk].Mappings[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                            switch (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].Direction)
                                            {
                                                case 0:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                                                    break;
                                                case 1:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                                    break;
                                                case 2:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                                    break;
                                                case 3:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                                    break;
                                            }

                                            if (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag0 == 0) //All
                                            {
                                                for (int ix = 0; ix < cm.Width; ix++)
                                                {
                                                    for (int iy = 0; iy < cm.Height; iy++)
                                                    {
                                                        Color gotColor = cm.GetPixel(ix, iy);
                                                        if (gotColor == Color.FromArgb(255, 0, 255, 0))
                                                        {
                                                            cm.SetPixel(ix, iy, Color.FromArgb(255, 255, 255));
                                                        }
                                                    }
                                                }
                                                e.Graphics.DrawImage(cm, (x * 128) + (w * 16), (y * 128) + (h * 16));
                                            }

                                            if (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag0 == 1 || Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag0 == 4) //Top Only
                                            {
                                                for (int ix = 0; ix < cm.Width; ix++)
                                                {
                                                    for (int iy = 0; iy < cm.Height; iy++)
                                                    {
                                                        Color gotColor = cm.GetPixel(ix, iy);
                                                        if (gotColor == Color.FromArgb(255, 0, 255, 0))
                                                        {
                                                            cm.SetPixel(ix, iy, Color.FromArgb(255, 255, 0));
                                                        }
                                                    }
                                                }
                                                e.Graphics.DrawImage(cm, (x * 128) + (w * 16), (y * 128) + (h * 16));
                                            }

                                            if (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag0 == 2) //All But Top
                                            {
                                                for (int ix = 0; ix < cm.Width; ix++)
                                                {
                                                    for (int iy = 0; iy < cm.Height; iy++)
                                                    {
                                                        Color gotColor = cm.GetPixel(ix, iy);
                                                        if (gotColor == Color.FromArgb(255, 0, 255, 0))
                                                        {
                                                            cm.SetPixel(ix, iy, Color.FromArgb(255, 0, 0));
                                                        }
                                                    }
                                                }
                                                e.Graphics.DrawImage(cm, (x * 128) + (w * 16), (y * 128) + (h * 16));
                                            }
                                            //cm.Dispose();
                                        }
                                    }
                                }
                            }
                            if (ShowCollisionB)
                            {
                                ushort Chunk = Parent.Scene.MapLayout[y][x];
                                for (int h = 0; h < 8; h++)
                                {
                                    for (int w = 0; w < 8; w++)
                                    {
                                        if (Parent.Scene.MapLayout[y][x] > 0)
                                        {
                                            int tx, ty;
                                            tx = (x * 8) + w;
                                            ty = (y * 8) + h;

                                            Bitmap cm = CollisionPathA[Parent.Chunks.ChunkList[Chunk].Mappings[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                            switch (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].Direction)
                                            {
                                                case 0:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                                                    break;
                                                case 1:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                                    break;
                                                case 2:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                                    break;
                                                case 3:
                                                    cm.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                                    break;
                                            }

                                            if (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag1 == 0 || Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag1 == 4) //All
                                            {
                                                for (int ix = 0; ix < cm.Width; ix++)
                                                {
                                                    for (int iy = 0; iy < cm.Height; iy++)
                                                    {
                                                        Color gotColor = cm.GetPixel(ix, iy);
                                                        if (gotColor == Color.FromArgb(255, 0, 255, 0))
                                                        {
                                                            cm.SetPixel(ix, iy, Color.FromArgb(255, 255, 255));
                                                        }
                                                    }
                                                }
                                                e.Graphics.DrawImage(cm, (x * 128) + (w * 16), (y * 128) + (h * 16));
                                            }

                                            if (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag1 == 1) //Top Only
                                            {
                                                for (int ix = 0; ix < cm.Width; ix++)
                                                {
                                                    for (int iy = 0; iy < cm.Height; iy++)
                                                    {
                                                        Color gotColor = cm.GetPixel(ix, iy);
                                                        if (gotColor == Color.FromArgb(255, 0, 255, 0))
                                                        {
                                                            cm.SetPixel(ix, iy, Color.FromArgb(255, 255, 0));
                                                        }
                                                    }
                                                }
                                                e.Graphics.DrawImage(cm, (x * 128) + (w * 16), (y * 128) + (h * 16));
                                            }

                                            if (Parent.Chunks.ChunkList[Chunk].Mappings[h][w].CollisionFlag1 == 2) //All But Top
                                            {
                                                for (int ix = 0; ix < cm.Width; ix++)
                                                {
                                                    for (int iy = 0; iy < cm.Height; iy++)
                                                    {
                                                        Color gotColor = cm.GetPixel(ix, iy);
                                                        if (gotColor == Color.FromArgb(255, 0, 255, 0))
                                                        {
                                                            cm.SetPixel(ix, iy, Color.FromArgb(255, 0, 0));
                                                        }
                                                    }
                                                }
                                                e.Graphics.DrawImage(cm, (x * 128) + (w * 16), (y * 128) + (h * 16));
                                            }
                                            //cm.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (ShowObjects) //Heh, It works now :)
            {
                for (int o = 0; o < Parent.Scene.objects.Count; o++)
                {
                    if (e.ClipRectangle.IntersectsWith(new Rectangle(Parent.Scene.objects[o].xPos, Parent.Scene.objects[o].yPos, viewport4.Width, viewport4.Height)))
                    {
                        Object_Definitions.MapObject mapobj = ObjectDefinitions.GetObjectByType(Parent.Scene.objects[o].type, Parent.Scene.objects[o].subtype);
                        if (mapobj != null && mapobj.ID > 0)
                        {
                            Bitmap b = mapobj.RenderObject(Parent.engineType, datapath);
                            e.Graphics.DrawImageUnscaled(b, Parent.Scene.objects[o].xPos + mapobj.PivotX, Parent.Scene.objects[o].yPos + mapobj.PivotX);
                            b.Dispose();
                        }
                        else
                        {
                            Bitmap b = RetroED.Properties.Resources.OBJ;
                            e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, Parent.Scene.objects[o].xPos - b.Width / 2, Parent.Scene.objects[o].yPos - b.Height / 2);
                            b.Dispose();
                        }
                    }
                }
            }
            if (ShowGrid)
            {
                Pen pen = new Pen(Color.DarkGray);

                if (gridCellSize.Width >= 8 && gridCellSize.Height >= 8)
                {
                    int lft = 0 % gridCellSize.Width;
                    int top = 0 % gridCellSize.Height;

                    for (int i = 0; i <= Parent.Scene.width * 128; ++i)
                    {
                        e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, Parent.Scene.height * 128);
                    }

                    for (int j = 0; j <= Parent.Scene.height * 128; ++j)
                    {
                        e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, Parent.Scene.width * 128, top + j * gridCellSize.Height);
                    }
                }
            }

            if (Parent.engineType == Retro_Formats.EngineType.RSDKvRS)
            {
                Pen PSpen = new Pen(Color.White); //We want the player spawn circle to be white!
                e.Graphics.DrawEllipse(PSpen, Parent.Scene.PlayerXpos, Parent.Scene.PlayerYPos, 32, 32); //Draw Spawn Circle
            }

            e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128)); //Draw the selected tile!

            GC.Collect();
        }

        public void DrawScene(PaintEventArgs e = null)
        {
            if (PanelMap != null) { PanelMap.Dispose(); }
            if (e == null)
            {
                try
                {
                    Rectangle viewport = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
                    PanelMap = new Bitmap(viewport.Width, viewport.Height);
                    e = new PaintEventArgs(Graphics.FromImage(PanelMap), viewport);
                }
                catch(System.ArgumentException)
                {
                    Console.WriteLine("We've got an error lol");
                }
            }

            pnlMap_Paint(this, e);
            Refresh();
            GC.Collect();
        } //Exactly what it says

        public void SetScene()
        {
            LoadChunks();
            LoadCollisionMasks();
            Text = Parent.Scene.Title;
            AutoScrollMinSize = new Size(Parent.Scene.MapLayout[0].Length * 128, Parent.Scene.MapLayout.Length * 128);
        } //Setup Scene

        public void LoadCollisionMasks()
        {
            for (int i = 0; i < 1024; i++)
            {
                CollisionPathA.Add(Parent.Tileconfig.CollisionPath1[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                CollisionPathB.Add(Parent.Tileconfig.CollisionPath2[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
            }
        }

        public void ResizeScrollBars()
        {
            AutoScrollMinSize = new Size(Parent.Scene.width * 128, Parent.Scene.height * 128);
        } //Standard stuff

        void LoadChunks() //Load the chunks into memory!
        {
            Chunks.Clear();
            for (int i = 0; i < Parent.Chunks.MaxChunks; i++)
            {
                Bitmap b = Parent.Chunks.ChunkList[i].Render(Parent._loadedTiles); //render chunk to an image!
                b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                Chunks.Add(b); //Add it to the list!
            }
        }

        void pnlMap_MouseDown(object sender, MouseEventArgs e)
        {
            Point tilePointNew = new Point(e.X / 128, e.Y / 128); //Get the point on the map to place the chunk! It's divided by 128 so it sticks to the grid!
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (PlacementMode == 0)
                    {
                        tilePoint = tilePointNew;
                        DrawScene();
                    }
                    if (PlacementMode == 1)
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, (ushort)Parent._blocksViewer.selectedChunk); //Place Selected Chunk
                        DrawScene();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0)
                    {
                        tilePoint = tilePointNew;
                        DrawScene();
                        int selChunk;
                        selChunk = Parent.Scene.MapLayout[tilePoint.Y][tilePoint.X];
                        Parent._blocksViewer.BlocksList.SelectedIndex = selChunk;
                        Parent._blocksViewer.BlocksList.Refresh();
                    }
                    if (PlacementMode == 1)
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, 0); //Delete the chunk
                        DrawScene();
                    }
                    break;
                case MouseButtons.Middle:
                    tilePoint = tilePointNew;
                    DrawScene();
                    int chunk;
                    chunk = Parent.Scene.MapLayout[tilePoint.Y][tilePoint.X];
                    Parent._blocksViewer.BlocksList.SelectedIndex = chunk; //Change the selected index of the chunklist!
                    Parent._blocksViewer.BlocksList.Refresh();
                    break;
            }
        }

        void pnlMap_MouseMove(object sender, MouseEventArgs e)
        {
            Point tilePointNew = new Point(e.X / 128, e.Y / 128);
            Parent._blocksViewer.MousePosStatusLabel.Text = "Mouse Position = " + e.X + " X, " + e.Y + " Y";
            switch (e.Button) //Check what button is pressed
            {
                case MouseButtons.Left:
                    if (PlacementMode == 0 && tilePoint != tilePointNew)//It's a different tile right?
                    {
                        tilePoint = tilePointNew; //Change the location of the yellow box!
                        DrawScene(); //I Don't even need to say what this does...
                    }
                    if (PlacementMode == 1 && tilePoint != tilePointNew) //It's a different tile right?
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, (ushort)Parent._blocksViewer.selectedChunk); // Set the selected chunk on the map to the selected chunk in the chunk view
                        DrawScene();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0 && tilePoint != tilePointNew)//It's a different tile right?
                    {
                        tilePoint = tilePointNew;
                        DrawScene();
                    }
                    if (PlacementMode == 1 && tilePoint != tilePointNew)//It's a different tile right?
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, 0); //Delete the chunk
                        DrawScene();
                    }
                    break;
            }
        }

        public void SetChunk(Point chunkpoint, ushort NewChunk) //Place a chunk in the map 
        {
            if (tilePoint.X < Parent.Scene.width && tilePoint.Y < Parent.Scene.height) //The chunk will be on the map right?
            {
                Parent.Scene.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk; //The chunk at these co-ordinates will become "NewChunk"
            }
        }

    }

}
