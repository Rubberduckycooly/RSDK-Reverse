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
        //What RSDK version is loaded?
        public int loadedRSDKver = 0;

        //Where is the data folder?
        public string datapath;

        //the tileset
        public Image _tiles;

        //A reference to the chunk/object viewer
        public StageChunksView _ChunkView;

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
            switch (loadedRSDKver)
            {
                case 0:
                    if (Parent._RSDKBScene == null) return;
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
                            if (y < Parent._RSDKBScene.MapLayout.Length && x < Parent._RSDKBScene.MapLayout[y].Length && Parent._RSDKBScene.MapLayout[y][x] < Parent._RSDKBChunks.BlockList.Length)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (Parent._RSDKBScene.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[Parent._RSDKBScene.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollisionA)
                                    {
                                        ushort Chunk = Parent._RSDKBScene.MapLayout[y][x];
                                        for (int h = 0; h < 8; h++)
                                        {
                                            for (int w = 0; w < 8; w++)
                                            {
                                                if (Parent._RSDKBScene.MapLayout[y][x] > 0)
                                                {
                                                    int tx, ty;
                                                    tx = (x * 8) + w;
                                                    ty = (y * 8) + h;

                                                    Bitmap cm = CollisionPathA[Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].Tile16x16].Clone(new Rectangle(0,0,16,16), System.Drawing.Imaging.PixelFormat.DontCare);

                                                    switch (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].Direction)
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

                                                    if (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 0) //All
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

                                                    if (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 1 || Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 4) //Top Only
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

                                                    if (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 2) //All But Top
                                                    {
                                                        for (int ix = 0; ix < cm.Width; ix++)
                                                        {
                                                            for (int iy = 0; iy < cm.Height; iy++)
                                                            {
                                                                Color gotColor = cm.GetPixel(ix, iy);
                                                                if (gotColor == Color.FromArgb(255,0, 255, 0))
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
                                        ushort Chunk = Parent._RSDKBScene.MapLayout[y][x];
                                        for (int h = 0; h < 8; h++)
                                        {
                                            for (int w = 0; w < 8; w++)
                                            {
                                                if (Parent._RSDKBScene.MapLayout[y][x] > 0)
                                                {
                                                    int tx, ty;
                                                    tx = (x * 8) + w;
                                                    ty = (y * 8) + h;

                                                    Bitmap cm = CollisionPathA[Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                                    switch (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].Direction)
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

                                                    if (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 0 || Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 4) //All
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

                                                    if (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 1) //Top Only
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

                                                    if (Parent._RSDKBChunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 2) //All But Top
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
                        for (int o = 0; o < Parent._RSDKBScene.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(Parent._RSDKBScene.objects[o].xPos, Parent._RSDKBScene.objects[o].yPos, viewport4.Width, viewport4.Height)))
                            {
                                Object_Definitions.MapObject mapobj = ObjectDefinitions.GetObjectByType(Parent._RSDKBScene.objects[o].type, Parent._RSDKBScene.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    Bitmap b = mapobj.RenderObject(loadedRSDKver, datapath);
                                    e.Graphics.DrawImageUnscaled(b, Parent._RSDKBScene.objects[o].xPos + mapobj.PivotX, Parent._RSDKBScene.objects[o].yPos + mapobj.PivotX);
                                    b.Dispose();
                                }
                                else
                                {
                                    Bitmap b = RetroED.Properties.Resources.OBJ;
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, Parent._RSDKBScene.objects[o].xPos - b.Width / 2, Parent._RSDKBScene.objects[o].yPos - b.Height / 2);
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

                            for (int i = 0; i <= Parent._RSDKBScene.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, Parent._RSDKBScene.height * 128);
                            }

                            for (int j = 0; j <= Parent._RSDKBScene.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, Parent._RSDKBScene.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 1:
                    if (Parent._RSDK1Scene == null) return;
                    e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255));
                    Rectangle viewport3 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
                    int startX3 = (viewport3.X / 128) - 1;
                    if (startX3 < 0) startX3 = 0;
                    int startY3 = (viewport3.Y / 128) - 1;
                    if (startY3 < 0) startY3 = 0;
                    int endX3 = startX3 + (pnlMap.Width / 128) + 1;
                    int endY3 = startY3 + (pnlMap.Height / 128) + 1;
                    for (int y = startY3; y < endY3; y++)
                    {
                        for (int x = startX3; x < endX3; x++)
                        {
                            if (y < Parent._RSDK2Scene.MapLayout.Length && x < Parent._RSDK2Scene.MapLayout[y].Length && Parent._RSDK2Scene.MapLayout[y][x] < Parent._RSDK2Chunks.BlockList.Length)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (Parent._RSDK2Scene.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[Parent._RSDK2Scene.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollisionA)
                                    {
                                        ushort Chunk = Parent._RSDK2Scene.MapLayout[y][x];
                                        for (int h = 0; h < 8; h++)
                                        {
                                            for (int w = 0; w < 8; w++)
                                            {
                                                if (Parent._RSDK2Scene.MapLayout[y][x] > 0)
                                                {
                                                    int tx, ty;
                                                    tx = (x * 8) + w;
                                                    ty = (y * 8) + h;

                                                    Bitmap cm = CollisionPathA[Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                                    switch (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].Direction)
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

                                                    if (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 0 || Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 4) //All
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

                                                    if (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 1) //Top Only
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

                                                    if (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 2) //All But Top
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
                                        ushort Chunk = Parent._RSDK2Scene.MapLayout[y][x];
                                        for (int h = 0; h < 8; h++)
                                        {
                                            for (int w = 0; w < 8; w++)
                                            {
                                                if (Parent._RSDK2Scene.MapLayout[y][x] > 0)
                                                {
                                                    int tx, ty;
                                                    tx = (x * 8) + w;
                                                    ty = (y * 8) + h;

                                                    Bitmap cm = CollisionPathA[Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                                    switch (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].Direction)
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

                                                    if (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 0 || Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 4) //All
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

                                                    if (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 1) //Top Only
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

                                                    if (Parent._RSDK2Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 2) //All But Top
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
                        for (int o = 0; o < Parent._RSDK2Scene.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(Parent._RSDK2Scene.objects[o].xPos, Parent._RSDK2Scene.objects[o].yPos, viewport3.Width, viewport3.Height)))
                            {
                                Object_Definitions.MapObject mapobj = ObjectDefinitions.GetObjectByType(Parent._RSDK2Scene.objects[o].type, Parent._RSDK2Scene.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    Bitmap b = mapobj.RenderObject(loadedRSDKver, datapath);
                                    e.Graphics.DrawImageUnscaled(b, Parent._RSDK2Scene.objects[o].xPos + mapobj.PivotX, Parent._RSDK2Scene.objects[o].yPos + mapobj.PivotY);
                                    b.Dispose();
                                }
                                else
                                {
                                    Bitmap b = RetroED.Properties.Resources.OBJ;
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, Parent._RSDK2Scene.objects[o].xPos - b.Width / 2, Parent._RSDK2Scene.objects[o].yPos - b.Height / 2);
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

                            for (int i = 0; i <= Parent._RSDK2Scene.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, Parent._RSDK2Scene.height * 128);
                            }

                            for (int j = 0; j <= Parent._RSDK2Scene.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, Parent._RSDK2Scene.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 2:
                    if (Parent._RSDK1Scene == null) return;
                    e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255));
                    Rectangle viewport2 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
                    int startX2 = (viewport2.X / 128) - 1;
                    if (startX2 < 0) startX2 = 0;
                    int startY2 = (viewport2.Y / 128) - 1;
                    if (startY2 < 0) startY2 = 0;
                    int endX2 = startX2 + (pnlMap.Width / 128) + 1;
                    int endY2 = startY2 + (pnlMap.Height / 128) + 1;
                    for (int y = startY2; y < endY2; y++)
                    {
                        for (int x = startX2; x < endX2; x++)
                        {
                            if (y < Parent._RSDK1Scene.MapLayout.Length && x < Parent._RSDK1Scene.MapLayout[y].Length && Parent._RSDK1Scene.MapLayout[y][x] < Parent._RSDK1Chunks.BlockList.Length)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (Parent._RSDK1Scene.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[Parent._RSDK1Scene.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollisionA)
                                    {
                                        ushort Chunk = Parent._RSDK1Scene.MapLayout[y][x];
                                        for (int h = 0; h < 8; h++)
                                        {
                                            for (int w = 0; w < 8; w++)
                                            {
                                                if (Parent._RSDK1Scene.MapLayout[y][x] > 0)
                                                {
                                                    int tx, ty;
                                                    tx = (x * 8) + w;
                                                    ty = (y * 8) + h;

                                                    Bitmap cm = CollisionPathA[Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                                    switch (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].Direction)
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

                                                    if (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 0) //All
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

                                                    if (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 1) //Top Only
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

                                                    if (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag0 == 2) //All But Top
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
                                        ushort Chunk = Parent._RSDK1Scene.MapLayout[y][x];
                                        for (int h = 0; h < 8; h++)
                                        {
                                            for (int w = 0; w < 8; w++)
                                            {
                                                if (Parent._RSDK1Scene.MapLayout[y][x] > 0)
                                                {
                                                    int tx, ty;
                                                    tx = (x * 8) + w;
                                                    ty = (y * 8) + h;

                                                    Bitmap cm = CollisionPathA[Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].Tile16x16].Clone(new Rectangle(0, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare);

                                                    switch (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].Direction)
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

                                                    if (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 0 || Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 4) //All
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

                                                    if (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 1) //Top Only
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

                                                    if (Parent._RSDK1Chunks.BlockList[Chunk].Mapping[h][w].CollisionFlag1 == 2) //All But Top
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
                        for (int o = 0; o < Parent._RSDK1Scene.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(Parent._RSDK1Scene.objects[o].xPos, Parent._RSDK1Scene.objects[o].yPos, viewport2.Width, viewport2.Height)))
                            {
                                Object_Definitions.MapObject mapobj = ObjectDefinitions.GetObjectByType(Parent._RSDK1Scene.objects[o].type, Parent._RSDK1Scene.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    Bitmap b = mapobj.RenderObject(loadedRSDKver, datapath);
                                    Console.WriteLine(mapobj.Flip);
                                    if (mapobj.Flip == 1) { b.RotateFlip(RotateFlipType.RotateNoneFlipX); }
                                    if (mapobj.Flip == 2) { b.RotateFlip(RotateFlipType.RotateNoneFlipY); }
                                    if (mapobj.Flip == 3) { b.RotateFlip(RotateFlipType.RotateNoneFlipXY); }
                                    e.Graphics.DrawImageUnscaled(b, Parent._RSDK1Scene.objects[o].xPos - b.Width/2, Parent._RSDK1Scene.objects[o].yPos - b.Height / 2);
                                    b.Dispose();
                                }
                                else
                                {
                                    Bitmap b = RetroED.Properties.Resources.OBJ;
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, Parent._RSDK1Scene.objects[o].xPos + mapobj.PivotX, Parent._RSDK1Scene.objects[o].yPos + mapobj.PivotY);
                                    b.Dispose();
                                }
                                //Console.WriteLine(o);
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

                            for (int i = 0; i <= Parent._RSDK1Scene.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, Parent._RSDK1Scene.height * 128);
                            }

                            for (int j = 0; j <= Parent._RSDK1Scene.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, Parent._RSDK1Scene.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 3:
                    if (Parent._RSDKRSScene == null) return; //We're not going to draw a Scene if we don't have one lol!
                    if (PanelMap != null) { PanelMap.Dispose(); } //Delet this, We don't need it anymore
                    e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255)); //Fill the "Canvas" with the colour FF00FF
                    //Setup the viewport, this defines where on the map we are (I think)
                    Rectangle viewport1 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
                    int startX1 = (viewport1.X / 128) - 1; //Where to start drawing (Xpos)
                    if (startX1 < 0) startX1 = 0;
                    int startY1 = (viewport1.Y / 128) - 1; //Where to start drawing (Ypos)
                    if (startY1 < 0) startY1 = 0;
                    int endX1 = startX1 + (pnlMap.Width / 128) + 1; //Where to Stop drawing (Xpos)
                    int endY1 = startY1 + (pnlMap.Height / 128) + 1; //Where to Stop drawing (Ypos)
                    for (int y = startY1; y < endY1; y++)
                    {
                        for (int x = startX1; x < endX1; x++)
                        {
                            if (y < Parent._RSDKRSScene.MapLayout.Length && x < Parent._RSDKRSScene.MapLayout[y].Length && Parent._RSDKRSScene.MapLayout[y][x] < Parent._RSDKRSChunks.BlockList.Length)
                            {
                                // Draw the map, line by line!

                                //if (e.ClipRectangle.IntersectsWith(new Rectangle(0, 0, viewport1.Width, viewport1.Height)))
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)//Check if the user wants to show the map
                                    {
                                        if (Parent._RSDKRSScene.MapLayout[y][x] > 0) //Don't draw Chunk Zero, It's always transparent!
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[Parent._RSDKRSScene.MapLayout[y][x]], x * 128, y * 128); //Draw the chunk to the map!
                                        }
                                        else { }
                                    }
                                    if (ShowCollisionA)
                                    {
                                        //in future this will show the collision masks of the tiles
                                    }
                                }
                            }
                        }

                    }
                    if (ShowObjects) //Heh, It works now :)
                    {
                        for (int o = 0; o < Parent._RSDKRSScene.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(Parent._RSDKRSScene.objects[o].xPos, Parent._RSDKRSScene.objects[o].yPos, viewport1.Width, viewport1.Height)))
                            {
                                Object_Definitions.MapObject mapobj = ObjectDefinitions.GetObjectByType(Parent._RSDKRSScene.objects[o].type, Parent._RSDKRSScene.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    Bitmap b = mapobj.RenderObject(loadedRSDKver, datapath);
                                    e.Graphics.DrawImageUnscaled(b, Parent._RSDKRSScene.objects[o].xPos + mapobj.PivotX, Parent._RSDKRSScene.objects[o].yPos + mapobj.PivotY);
                                    b.Dispose();
                                }
                                else
                                {
                                    Bitmap b = RetroED.Properties.Resources.OBJ;
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, Parent._RSDKRSScene.objects[o].xPos - b.Width / 2, Parent._RSDKRSScene.objects[o].yPos - b.Height / 2);
                                    b.Dispose();
                                }
                                //Console.WriteLine(o);
                            }
                        }
                    }

                    if (ShowGrid) //If we want a grid, then draw it over the map
                    {
                        Pen pen = new Pen(Color.DarkGray);

                        if (gridCellSize.Width >= 8 && gridCellSize.Height >= 8)
                        {
                            int lft = 0 % gridCellSize.Width;
                            int top = 0 % gridCellSize.Height;

                            for (int i = 0; i <= Parent._RSDKRSScene.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, Parent._RSDKRSScene.height * 128);
                            }

                            for (int j = 0; j <= Parent._RSDKRSScene.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, Parent._RSDKRSScene.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    Pen PSpen = new Pen(Color.White); //We want the player spawn circle to be white!
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128)); //Draw the selected tile!
                    e.Graphics.DrawEllipse(PSpen, Parent._RSDKRSScene.PlayerXpos, Parent._RSDKRSScene.PlayerYPos, 32, 32); //Draw Spawn Circle
                    break;
                default:
                    break;
            }
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
            switch (loadedRSDKver)
            {
                case 0:
                    LoadChunks();
                    LoadCollisionMasks();
                    Text = Parent._RSDKBScene.Title;
                    AutoScrollMinSize = new Size(Parent._RSDKBScene.MapLayout[0].Length * 128, Parent._RSDKBScene.MapLayout.Length * 128);
                    break;
                case 1:
                    LoadChunks();
                    LoadCollisionMasks();
                    Text = Parent._RSDK2Scene.Title;
                    AutoScrollMinSize = new Size(Parent._RSDK2Scene.MapLayout[0].Length * 128, Parent._RSDK2Scene.MapLayout.Length * 128);
                    break;
                case 2:
                    LoadChunks();
                    LoadCollisionMasks();
                    Text = Parent._RSDK1Scene.Title;
                    AutoScrollMinSize = new Size(Parent._RSDK1Scene.MapLayout[0].Length * 128, Parent._RSDK1Scene.MapLayout.Length * 128);
                    break;
                case 3:
                    LoadChunks();
                    LoadCollisionMasks();
                    Text = Parent._RSDKRSScene.Title;
                    AutoScrollMinSize = new Size(Parent._RSDKRSScene.MapLayout[0].Length * 128, Parent._RSDKRSScene.MapLayout.Length * 128);
                    break;
                default:
                    break;
            }
        } //Setup Scene

        public void LoadCollisionMasks()
        {
            //
            switch (loadedRSDKver)
            {
                case 0:
                    for (int i = 0; i < 1024; i++)
                    {
                        CollisionPathA.Add(Parent._RSDKBCollisionMask.CollisionPath1[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                        CollisionPathB.Add(Parent._RSDKBCollisionMask.CollisionPath2[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                    }
                    break;
                case 1:
                    for (int i = 0; i < 1024; i++)
                    {
                        CollisionPathA.Add(Parent._RSDK2CollisionMask.CollisionPath1[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                        CollisionPathB.Add(Parent._RSDK2CollisionMask.CollisionPath2[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                    }
                    break;
                case 2:
                    for (int i = 0; i < 1024; i++)
                    {
                        CollisionPathA.Add(Parent._RSDK1CollisionMask.CollisionPath1[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                        CollisionPathB.Add(Parent._RSDK1CollisionMask.CollisionPath2[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                    }
                    break;
                case 3:
                    for (int i = 0; i < 1024; i++)
                    {
                        //CollisionPathA.Add(Parent._RSDKRSCollisionMask.CollisionPath1[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                        //CollisionPathB.Add(Parent._RSDKRSCollisionMask.CollisionPath2[i].DrawCMask(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0)/*, Tiles[curColisionMask]*/));
                    }
                    break;
                default:
                    break;
            }
        }

        public void ResizeScrollBars()
        {
            switch (loadedRSDKver)
            {
                case 0:
                    AutoScrollMinSize = new Size(Parent._RSDKBScene.width * 128, Parent._RSDKBScene.height * 128);
                    break;
                case 1:
                    AutoScrollMinSize = new Size(Parent._RSDK1Scene.width * 128, Parent._RSDK1Scene.height * 128);
                    break;
                case 2:
                    AutoScrollMinSize = new Size(Parent._RSDK1Scene.width * 128, Parent._RSDK1Scene.height * 128);
                    break;
                case 3:
                    AutoScrollMinSize = new Size(Parent._RSDKRSScene.width * 128, Parent._RSDKRSScene.height * 128);
                    break;
                default:
                    break;
            }
        } //Standard stuff

        void LoadChunks() //Load the chunks into memory!
        {
            Chunks.Clear();
            if (loadedRSDKver == 0)
            {
                for (int i = 0; i < Parent._RSDKBChunks.BlockList.Length; i++)
                {
                    Bitmap b = Parent._RSDKBChunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
            }
            if (loadedRSDKver == 1)
            {
                for (int i = 0; i < Parent._RSDK2Chunks.BlockList.Length; i++)
                {
                    Bitmap b = Parent._RSDK2Chunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
            }
            if (loadedRSDKver == 2)
            {
                for (int i = 0; i < Parent._RSDK1Chunks.BlockList.Length; i++)
                {
                    Bitmap b = Parent._RSDK1Chunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
            }
            if (loadedRSDKver == 3)
            {
                for (int i = 0; i < Parent._RSDKRSChunks.BlockList.Length; i++)
                {
                    Bitmap b = Parent._RSDKRSChunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 0, 0, 0)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
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
                        SetChunk(tilePoint, (ushort)_ChunkView.selectedChunk); //Place Selected Chunk
                        DrawScene();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0)
                    {
                        tilePoint = tilePointNew;
                        DrawScene();
                        int selChunk;
                        switch (loadedRSDKver) //Copy chunk
                        {
                            case 0:
                                selChunk = Parent._RSDKBScene.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 1:
                                selChunk = Parent._RSDK1Scene.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 2:
                                selChunk = Parent._RSDK1Scene.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 3:
                                selChunk = Parent._RSDKRSScene.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            default:
                                break;
                        }
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
                    switch (loadedRSDKver) //Check the RSDK version then Copy the chunk
                    {
                        case 0:
                            chunk = Parent._RSDKBScene.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk; //Change the selected index of the chunklist!
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 1:
                            chunk = Parent._RSDK1Scene.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 2:
                            chunk = Parent._RSDK1Scene.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 3:
                            chunk = Parent._RSDKRSScene.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        void pnlMap_MouseMove(object sender, MouseEventArgs e)
        {
            Point tilePointNew = new Point(e.X / 128, e.Y / 128);
            _ChunkView.MousePosStatusLabel.Text = "Mouse Position = " + e.X + " X, " + e.Y + " Y";
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
                        SetChunk(tilePoint, (ushort)_ChunkView.selectedChunk); // Set the selected chunk on the map to the selected chunk in the chunk view
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
            switch (loadedRSDKver) //What RSDK version is loaded?
            {
                case 0:
                    if (tilePoint.X < Parent._RSDKBScene.width && tilePoint.Y < Parent._RSDKBScene.height) //The chunk will be on the map right?
                    {
                    Parent._RSDKBScene.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk; //The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                case 1:
                    if (tilePoint.X < Parent._RSDK2Scene.width && tilePoint.Y < Parent._RSDK2Scene.height) //The chunk will be on the map right?
                    {
                        Parent._RSDK2Scene.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;//The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                case 2:
                    if (tilePoint.X < Parent._RSDK1Scene.width && tilePoint.Y < Parent._RSDK1Scene.height) //The chunk will be on the map right?
                    {
                        Parent._RSDK1Scene.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;//The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                case 3:
                    if (tilePoint.X < Parent._RSDKRSScene.width && tilePoint.Y < Parent._RSDKRSScene.height) //The chunk will be on the map right?
                    {
                    Parent._RSDKRSScene.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;//The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                default:
                    break;
            }
        }

    }

}
