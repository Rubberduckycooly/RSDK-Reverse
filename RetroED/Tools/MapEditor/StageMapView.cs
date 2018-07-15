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
        public bool ShowCollision = false;
        public bool ShowGrid = false;

        //Built-In Object Definitions for your convenience ;)

        //Retro-Sonic Global Objects
        public Object_Definitions.Retro_SonicObjects RSObjects = new Object_Definitions.Retro_SonicObjects();

        //Sonic Nexus Global Objects
        public Object_Definitions.SonicNexusObjects NexusObjects = new Object_Definitions.SonicNexusObjects();

        //Sonic CD Global Objects
        public Object_Definitions.SonicCDObjects CDObjects = new Object_Definitions.SonicCDObjects();

        //Sonic 1 Global Objects
        public Object_Definitions.Sonic1Objects S1Objects = new Object_Definitions.Sonic1Objects();

        //Sonic 2 Global Objects
        public Object_Definitions.Sonic2Objects S2Objects = new Object_Definitions.Sonic2Objects();

        public List<Bitmap> Chunks = new List<Bitmap>();

        public Point tilePoint;

        #region Retro-Sonic Development Kit
        public RSDKv1.Level _RSDK1Level;
        public RSDKv1.til _RSDK1Chunks;
        public RSDKv1.tcf _RSDK1CollisionMask;
        #endregion

        #region RSDKv1
        public RSDKv2.Level _RSDK2Level;
        public RSDKv2.Tiles128x128 _RSDK2Chunks;
        public RSDKv2.CollisionMask _RSDK2CollisionMask;
        #endregion

        #region RSDKv2
        public RSDKv3.Level _RSDK3Level;
        public RSDKv3.Tiles128x128 _RSDK3Chunks;
        public RSDKv3.CollisionMask _RSDK3CollisionMask;
        #endregion

        #region RSDKvB
        public RSDKv4.Level _RSDK4Level;
        public RSDKv4.Tiles128x128 _RSDK4Chunks;
        public RSDKv4.CollisionMask _RSDK4CollisionMask;
        #endregion

        public StageMapView()
        {
            InitializeComponent();
            pnlMap.Paint += pnlMap_Paint; // Paint on the Form!
            pnlMap.Resize += ((sender, e) => Refresh()); //Resize the form (I think)
        }

        void pnlMap_Paint(object sender, PaintEventArgs e)
        {
            Size gridCellSize = new Size(128, 128);
            Pen p = new Pen(System.Drawing.Color.Yellow);
            switch (loadedRSDKver)
            {
                case 0:
                    if (_RSDK4Level == null) return;
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
                            if (y < _RSDK4Level.MapLayout.Length && x < _RSDK4Level.MapLayout[y].Length && _RSDK4Level.MapLayout[y][x] < _RSDK4Chunks.BlockList.Count)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (_RSDK4Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK4Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollision)
                                    {

                                    }
                                    if (ShowObjects)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    if (ShowObjects) //Heh, It works now :)
                    {
                        for (int o = 0; o < _RSDK4Level.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(_RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos, viewport4.Width, viewport4.Height)))
                            {
                                Object_Definitions.MapObject mapobj = RSObjects.GetObjectByType(_RSDK4Level.objects[o].type, _RSDK4Level.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    e.Graphics.DrawImageUnscaled(mapobj.RenderObject(loadedRSDKver, datapath), _RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos);
                                }
                                else
                                {
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK4Level.objects[o].xPos, _RSDK4Level.objects[o].yPos);
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

                            for (int i = 0; i <= _RSDK4Level.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK4Level.height * 128);
                            }

                            for (int j = 0; j <= _RSDK4Level.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK4Level.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 1:
                    if (_RSDK3Level == null) return;
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
                            if (y < _RSDK3Level.MapLayout.Length && x < _RSDK3Level.MapLayout[y].Length && _RSDK3Level.MapLayout[y][x] < _RSDK3Chunks.BlockList.Count)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (_RSDK3Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK3Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollision)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    if (ShowObjects) //Heh, It works now :)
                    {
                        for (int o = 0; o < _RSDK3Level.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(_RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos, viewport3.Width, viewport3.Height)))
                            {
                                Object_Definitions.MapObject mapobj = RSObjects.GetObjectByType(_RSDK3Level.objects[o].type, _RSDK3Level.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    e.Graphics.DrawImageUnscaled(mapobj.RenderObject(loadedRSDKver, datapath), _RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos);
                                }
                                else
                                {
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK3Level.objects[o].xPos, _RSDK3Level.objects[o].yPos);
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

                            for (int i = 0; i <= _RSDK3Level.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK3Level.height * 128);
                            }

                            for (int j = 0; j <= _RSDK3Level.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK3Level.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 2:
                    if (_RSDK2Level == null) return;
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
                            if (y < _RSDK2Level.MapLayout.Length && x < _RSDK2Level.MapLayout[y].Length && _RSDK2Level.MapLayout[y][x] < _RSDK2Chunks.BlockList.Count)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (_RSDK2Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK2Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollision)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    if (ShowObjects) //Heh, It works now :)
                    {
                        for (int o = 0; o < _RSDK2Level.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(_RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos, viewport2.Width, viewport2.Height)))
                            {
                                Object_Definitions.MapObject mapobj = RSObjects.GetObjectByType(_RSDK2Level.objects[o].type, _RSDK2Level.objects[o].subtype);
                                if (mapobj != null && mapobj.ID > 0)
                                {
                                    e.Graphics.DrawImageUnscaled(mapobj.RenderObject(loadedRSDKver, datapath), _RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos);
                                }
                                else
                                {
                                    e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK2Level.objects[o].xPos, _RSDK2Level.objects[o].yPos);
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

                            for (int i = 0; i <= _RSDK2Level.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK2Level.height * 128);
                            }

                            for (int j = 0; j <= _RSDK2Level.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK2Level.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 3:
                    if (_RSDK1Level == null) return; //We're not going to draw a level if we don't have one lol!
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
                            if (y < _RSDK1Level.MapLayout.Length && x < _RSDK1Level.MapLayout[y].Length && _RSDK1Level.MapLayout[y][x] < _RSDK1Chunks.BlockList.Count)
                            {
                                // Draw the map, line by line!

                                //if (e.ClipRectangle.IntersectsWith(new Rectangle(0, 0, viewport1.Width, viewport1.Height)))
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)//Check if the user wants to show the map
                                    {
                                        if (_RSDK1Level.MapLayout[y][x] > 0) //Don't draw Chunk Zero, It's always transparent!
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK1Level.MapLayout[y][x]], x * 128, y * 128); //Draw the chunk to the map!
                                        }
                                        else { }
                                    }
                                    if (ShowCollision)
                                    {
                                        //in future this will show the collision masks of the tiles
                                    }
                                }
                            }
                        }

                    }
                    if (ShowObjects) //Heh, It works now :)
                    {
                        for (int o = 0; o < _RSDK1Level.objects.Count; o++)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(_RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos, viewport1.Width, viewport1.Height)))
                            {
                                Object_Definitions.MapObject mapobj = RSObjects.GetObjectByType(_RSDK1Level.objects[o].type, _RSDK1Level.objects[o].subtype);
                                if (mapobj != null && mapobj.ID >0)
                                {
                                   e.Graphics.DrawImageUnscaled(mapobj.RenderObject(loadedRSDKver,datapath), _RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos);
                                }
                                else
                                {
                                   e.Graphics.DrawImage(RetroED.Properties.Resources.OBJ, _RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos);
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

                            for (int i = 0; i <= _RSDK1Level.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK1Level.height * 128);
                            }

                            for (int j = 0; j <= _RSDK1Level.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK1Level.width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    Pen PSpen = new Pen(Color.White); //We want the player spawn circle to be white!
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128)); //Draw the selected tile!
                    e.Graphics.DrawEllipse(PSpen, _RSDK1Level.PlayerXpos, _RSDK1Level.PlayerYPos, 32, 32); //Draw Spawn Circle
                    break;
                default:
                    break;
            }
        }

        public void DrawLevel(PaintEventArgs e = null)
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
        } //Exactly what it says

        public void SetLevel()
        {
            switch (loadedRSDKver)
            {
                case 0:
                    LoadChunks();
                    Text = _RSDK4Level.Title;
                    AutoScrollMinSize = new Size(_RSDK4Level.MapLayout[0].Length * 128, _RSDK4Level.MapLayout.Length * 128);
                    break;
                case 1:
                    LoadChunks();
                    Text = _RSDK3Level.Title;
                    AutoScrollMinSize = new Size(_RSDK3Level.MapLayout[0].Length * 128, _RSDK3Level.MapLayout.Length * 128);
                    break;
                case 2:
                    LoadChunks();
                    Text = _RSDK2Level.Title;
                    AutoScrollMinSize = new Size(_RSDK2Level.MapLayout[0].Length * 128, _RSDK2Level.MapLayout.Length * 128);
                    break;
                case 3:
                    LoadChunks();
                    Text = _RSDK1Level.Title;
                    AutoScrollMinSize = new Size(_RSDK1Level.MapLayout[0].Length * 128, _RSDK1Level.MapLayout.Length * 128);
                    break;
                default:
                    break;
            }
        } //Setup Level

        public void ResizeScrollBars()
        {
            switch (loadedRSDKver)
            {
                case 0:
                    AutoScrollMinSize = new Size(_RSDK4Level.width * 128, _RSDK4Level.height * 128);
                    break;
                case 1:
                    AutoScrollMinSize = new Size(_RSDK3Level.width * 128, _RSDK3Level.height * 128);
                    break;
                case 2:
                    AutoScrollMinSize = new Size(_RSDK2Level.width * 128, _RSDK2Level.height * 128);
                    break;
                case 3:
                    AutoScrollMinSize = new Size(_RSDK1Level.width * 128, _RSDK1Level.height * 128);
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
                for (int i = 0; i < _RSDK4Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK4Chunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
            }
            if (loadedRSDKver == 1)
            {
                for (int i = 0; i < _RSDK3Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK3Chunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
            }
            if (loadedRSDKver == 2)
            {
                for (int i = 0; i < _RSDK2Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK2Chunks.BlockList[i].Render(_tiles); //render chunk to an image!
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255)); //add transparent colour!
                    Chunks.Add(b); //Add it to the list!
                }
            }
            if (loadedRSDKver == 3)
            {
                for (int i = 0; i < _RSDK1Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK1Chunks.BlockList[i].Render(_tiles); //render chunk to an image!
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
                        DrawLevel();
                    }
                    if (PlacementMode == 1)
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, (ushort)_ChunkView.selectedChunk); //Place Selected Chunk
                        DrawLevel();
                    }
                    if (PlacementMode == 2)
                    {
                        NewObjectForm frm = new NewObjectForm(0);
                        //Where is the mouse?
                        frm.XposNUD.Value = e.X;
                        frm.YposNUD.Value = e.Y;

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            switch (loadedRSDKver)
                            {
                                case 3:
                                        RSDKv1.Object Obj1 = new RSDKv1.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos); //make an object from the data we got!
                                    _RSDK1Level.objects.Add(Obj1); //Add it to the map!
                                    _ChunkView.RefreshObjList(); //Update the list!
                                    DrawLevel();
                                    break;
                                case 2:
                                        RSDKv2.Object Obj2 = new RSDKv2.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos); //make an object from the data we got!
                                    _RSDK2Level.objects.Add(Obj2); //Add it to the map!
                                    _ChunkView.RefreshObjList(); //Update the list!
                                    DrawLevel();
                                    break;
                                case 1:
                                        RSDKv3.Object Obj3 = new RSDKv3.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos); //make an object from the data we got!
                                    _RSDK3Level.objects.Add(Obj3); //Add it to the map!
                                    _ChunkView.RefreshObjList(); //Update the list!
                                    DrawLevel();
                                    break;
                                case 0:
                                    RSDKv4.Object Obj4 = new RSDKv4.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos); //make an object from the data we got!
                                    _RSDK4Level.objects.Add(Obj4); //Add it to the map!
                                    _ChunkView.RefreshObjList(); //Update the list!
                                    DrawLevel();
                                    break;
                            }
                        }
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0)
                    {
                        tilePoint = tilePointNew;
                        DrawLevel();
                        int selChunk;
                        switch (loadedRSDKver) //Copy chunk
                        {
                            case 0:
                                selChunk = _RSDK4Level.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 1:
                                selChunk = _RSDK3Level.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 2:
                                selChunk = _RSDK2Level.MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 3:
                                selChunk = _RSDK1Level.MapLayout[tilePoint.Y][tilePoint.X];
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
                        DrawLevel();
                    }
                    break;
                case MouseButtons.Middle:
                    tilePoint = tilePointNew;
                    DrawLevel();
                    int chunk;
                    switch (loadedRSDKver) //Check the RSDK version then Copy the chunk
                    {
                        case 0:
                            chunk = _RSDK4Level.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk; //Change the selected index of the chunklist!
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 1:
                            chunk = _RSDK3Level.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 2:
                            chunk = _RSDK2Level.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 3:
                            chunk = _RSDK1Level.MapLayout[tilePoint.Y][tilePoint.X];
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
                        DrawLevel(); //I Don't even need to say what this does...
                    }
                    if (PlacementMode == 1 && tilePoint != tilePointNew) //It's a different tile right?
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, (ushort)_ChunkView.selectedChunk); // Set the selected chunk on the map to the selected chunk in the chunk view
                        DrawLevel();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0 && tilePoint != tilePointNew)//It's a different tile right?
                    {
                        tilePoint = tilePointNew;
                        DrawLevel();
                    }
                    if (PlacementMode == 1 && tilePoint != tilePointNew)//It's a different tile right?
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, 0); //Delete the chunk
                        DrawLevel();
                    }
                    break;
            }
        }

        public void SetChunk(Point chunkpoint, ushort NewChunk) //Place a chunk in the map 
        {
            switch (loadedRSDKver) //What RSDK version is loaded?
            {
                case 0:
                    if (tilePoint.X < _RSDK4Level.width && tilePoint.Y < _RSDK4Level.height) //The chunk will be on the map right?
                    {
                    _RSDK4Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk; //The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                case 1:
                    if (tilePoint.X < _RSDK3Level.width && tilePoint.Y < _RSDK3Level.height) //The chunk will be on the map right?
                    {
                        _RSDK3Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;//The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                case 2:
                    if (tilePoint.X < _RSDK2Level.width && tilePoint.Y < _RSDK2Level.height) //The chunk will be on the map right?
                    {
                        _RSDK2Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;//The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                case 3:
                    if (tilePoint.X < _RSDK1Level.width && tilePoint.Y < _RSDK1Level.height) //The chunk will be on the map right?
                    {
                    _RSDK1Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;//The chunk at these co-ordinates will become "NewChunk"
                    }
                    break;
                default:
                    break;
            }
        }

    }

}
