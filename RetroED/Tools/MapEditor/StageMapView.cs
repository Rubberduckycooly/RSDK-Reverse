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
        public int loadedRSDKver = 0;

        public Image _tiles;
        public StageChunksView _ChunkView;

        public int PlacementMode = 0;
        private Bitmap PanelMap;

        public bool ShowMap = true;
        public bool ShowBG = false;
        public bool ShowObjects = false;
        public bool ShowCollision = false;
        public bool ShowGrid = false;

        //Built-In Object Definitions for your convenience

        //Retro-Sonic Global Objects
        Object_Definitions.Retro_SonicGlobalObjects RSObjects = new Object_Definitions.Retro_SonicGlobalObjects();

        //Sonic Nexus Global Objects
        Object_Definitions.SonicNexusGlobalObjects NexusObjects = new Object_Definitions.SonicNexusGlobalObjects();

        //Sonic CD Global Objects
        Object_Definitions.SonicCDGlobalObjects CDObjects = new Object_Definitions.SonicCDGlobalObjects();

        //Sonic 1 Global Objects
        Object_Definitions.Sonic1GlobalObjects S1Objects = new Object_Definitions.Sonic1GlobalObjects();

        //Sonic 2 Global Objects
        Object_Definitions.Sonic2GlobalObjects S2Objects = new Object_Definitions.Sonic2GlobalObjects();

        public List<Bitmap> Chunks = new List<Bitmap>();

        public Point tilePoint;

        #region Retro-Sonic Development Kit
        public RSDKv1.Level _RSDK1Level;
        public RSDKv1.BGLayout _RSDK1Background;
        public RSDKv1.til _RSDK1Chunks;
        public RSDKv1.tcf _RSDK1CollisionMask;
        #endregion

        #region RSDKv1
        public RSDKv2.Level _RSDK2Level;
        public RSDKv2.BGLayout _RSDK2Background;
        public RSDKv2.Tiles128x128 _RSDK2Chunks;
        public RSDKv2.CollisionMask _RSDK2CollisionMask;
        #endregion

        #region RSDKv2
        public RSDKv3.Level _RSDK3Level;
        public RSDKv3.BGLayout _RSDK3Background;
        public RSDKv3.Tiles128x128 _RSDK3Chunks;
        public RSDKv3.CollisionMask _RSDK3CollisionMask;
        #endregion

        #region RSDKvB
        public RSDKv4.Level _RSDK4Level;
        public RSDKv4.BGLayout _RSDK4Background;
        public RSDKv4.Tiles128x128 _RSDK4Chunks;
        public RSDKv4.CollisionMask _RSDK4CollisionMask;
        #endregion

        public StageMapView()
        {
            InitializeComponent();
            pnlMap.Paint += pnlMap_Paint;
            pnlMap.Resize += ((sender, e) => Refresh());
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
                                    if (ShowBG)
                                    {
                                        if (_RSDK4Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK4Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
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

                        //e.Graphics.TranslateTransform(e.ClipRectangle.X, e.ClipRectangle.Y);
                        //e.Graphics.DrawEllipse(pen, -4, -4, 8, 8);
                        //e.Graphics.ResetTransform();
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
                                    /*if (ShowBG)
                                    {
                                        if (_RSDK3Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK3Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }*/
                                    if (ShowMap)
                                    {
                                        if (_RSDK3Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK3Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    /*if (ShowCollision)
                                    {

                                    }
                                    if (ShowObjects)
                                    {

                                    }*/
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

                        //e.Graphics.TranslateTransform(e.ClipRectangle.X, e.ClipRectangle.Y);
                        //e.Graphics.DrawEllipse(pen, -4, -4, 8, 8);
                        //e.Graphics.ResetTransform();
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
                                    if (ShowBG)
                                    {
                                        if (_RSDK2Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK2Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
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
                                    if (ShowObjects)
                                    {

                                    }
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

                            for (int i = 0; i <= _RSDK2Level.width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK2Level.height * 128);
                            }

                            for (int j = 0; j <= _RSDK2Level.height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK2Level.width * 128, top + j * gridCellSize.Height);
                            }
                        }

                        //e.Graphics.TranslateTransform(e.ClipRectangle.X, e.ClipRectangle.Y);
                        //e.Graphics.ResetTransform();
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 3:
                    if (_RSDK1Level == null) return;
                    if (PanelMap != null) { PanelMap.Dispose(); }
                    e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255));
                    Rectangle viewport1 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
                    int startX1 = (viewport1.X / 128) - 1;
                    if (startX1 < 0) startX1 = 0;
                    int startY1 = (viewport1.Y / 128) - 1;
                    if (startY1 < 0) startY1 = 0;
                    int endX1 = startX1 + (pnlMap.Width / 128) + 1;
                    int endY1 = startY1 + (pnlMap.Height / 128) + 1;
                    for (int y = startY1; y < endY1; y++)
                    {
                        for (int x = startX1; x < endX1; x++)
                        {
                            if (y < _RSDK1Level.MapLayout.Length && x < _RSDK1Level.MapLayout[y].Length && _RSDK1Level.MapLayout[y][x] < _RSDK1Chunks.BlockList.Count)
                            {
                                //if (e.ClipRectangle.IntersectsWith(new Rectangle(0, 0, viewport1.Width, viewport1.Height)))
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowBG)
                                    {
                                        if (_RSDK1Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK1Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowMap)
                                    {
                                        if (_RSDK1Level.MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK1Level.MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
                                    }
                                    if (ShowCollision)
                                    {

                                    }
                                }
                            }
                        }

                        if (ShowObjects)
                        {
                            Console.WriteLine(_RSDK1Level.objects.Count);
                            /*for (int o = 0; o < _RSDK1Level.objects.Count; o++)
                            {
                                Object_Definitions.MapObject mapobj = RSObjects.GetObjectByType(_RSDK1Level.objects[o].type, _RSDK1Level.objects[o].subtype);
                                if (mapobj != null)
                                {
                                    e.Graphics.DrawImageUnscaled(mapobj.RenderObject(), new Point(_RSDK1Level.objects[o].xPos, _RSDK1Level.objects[o].yPos));
                                }
                                Console.WriteLine(o);
                            }*/
                        }

                    }
                    if (ShowGrid)
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
                    Pen PSpen = new Pen(Color.White);
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    e.Graphics.DrawEllipse(PSpen, _RSDK1Level.PlayerXpos, _RSDK1Level.PlayerYPos, 32, 32);
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
        }

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
        }

        void LoadChunks()
        {
            Chunks.Clear();
            if (loadedRSDKver == 0)
            {
                for (int i = 0; i < _RSDK4Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK4Chunks.BlockList[i].Render(_tiles);
                    b.MakeTransparent(Color.FromArgb(255, 0, 0, 0));
                    Chunks.Add(b);
                }
            }
            if (loadedRSDKver == 1)
            {
                for (int i = 0; i < _RSDK3Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK3Chunks.BlockList[i].Render(_tiles);
                    b.MakeTransparent(Color.FromArgb(255, 0, 0, 0));
                    Chunks.Add(b);
                }
            }
            if (loadedRSDKver == 2)
            {
                for (int i = 0; i < _RSDK2Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK2Chunks.BlockList[i].Render(_tiles);
                    b.MakeTransparent(Color.FromArgb(255, 0, 0, 0));
                    Chunks.Add(b);
                }
            }
            if (loadedRSDKver == 3)
            {
                for (int i = 0; i < _RSDK1Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK1Chunks.BlockList[i].Render(_tiles);
                    b.MakeTransparent(Color.FromArgb(255, 0, 0, 0));
                    Chunks.Add(b);
                }
            }
        }

        void pnlMap_MouseDown(object sender, MouseEventArgs e)
        {
            Point tilePointNew = new Point(e.X / 128, e.Y / 128);
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
                        SetChunk(tilePoint, (ushort)_ChunkView.selectedChunk);
                        DrawLevel();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0)
                    {
                        tilePoint = tilePointNew;
                        DrawLevel();
                        int selChunk;
                        switch (loadedRSDKver)
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
                        SetChunk(tilePoint, 0);
                        DrawLevel();
                    }
                    break;
                case MouseButtons.Middle:
                    tilePoint = tilePointNew;
                    DrawLevel();
                    int chunk;
                    switch (loadedRSDKver)
                    {
                        case 0:
                            chunk = _RSDK4Level.MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (PlacementMode == 0 && tilePoint != tilePointNew)
                    {
                        tilePoint = tilePointNew;
                        DrawLevel();
                    }
                    if (PlacementMode == 1 && tilePoint != tilePointNew)
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, (ushort)_ChunkView.selectedChunk);
                        DrawLevel();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0 && tilePoint != tilePointNew)
                    {
                        tilePoint = tilePointNew;
                        DrawLevel();
                    }
                    if (PlacementMode == 1 && tilePoint != tilePointNew)
                    {
                        tilePoint = tilePointNew;
                        SetChunk(tilePoint, 0);
                        DrawLevel();
                    }
                    break;
            }
        }

        public void SetChunk(Point chunkpoint, ushort NewChunk)
        {
            switch (loadedRSDKver)
            {
                case 0:
                    if (tilePoint.X < _RSDK4Level.width && tilePoint.Y < _RSDK4Level.height)
                    {
                    _RSDK4Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                case 1:
                    if (tilePoint.X < _RSDK3Level.width && tilePoint.Y < _RSDK3Level.height)
                    {
                        _RSDK3Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                case 2:
                    if (tilePoint.X < _RSDK2Level.width && tilePoint.Y < _RSDK2Level.height)
                    {
                        _RSDK2Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                case 3:
                    if (tilePoint.X < _RSDK1Level.width && tilePoint.Y < _RSDK1Level.height)
                    {
                    _RSDK1Level.MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                default:
                    break;
            }
        }

    }

}
