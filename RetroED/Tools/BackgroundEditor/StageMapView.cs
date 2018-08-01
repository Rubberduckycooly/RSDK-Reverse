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
    public partial class StageMapView : DockContent
    {
        public int loadedRSDKver = 0;

        public int curlayer = 0;

        public bool DrawAllLayers = true;

        public Image _tiles;
        public StageChunksView _ChunkView;

        public int PlacementMode = 0;
        private Bitmap PanelMap;

        public bool ShowMap = true;
        public bool ShowBG = false;
        public bool ShowObjects = false;
        public bool ShowCollision = false;
        public bool ShowGrid = false;

        public List<Bitmap> Chunks = new List<Bitmap>();

        public Point tilePoint;

        #region Retro-Sonic Development Kit
        public RSDKv1.BGLayout _RSDK1Background;
        public RSDKv1.til _RSDK1Chunks;
        #endregion

        #region RSDKv1
        public RSDKv2.BGLayout _RSDK2Background;
        public RSDKv2.Tiles128x128 _RSDK2Chunks;
        #endregion

        #region RSDKv2
        public RSDKv3.BGLayout _RSDK3Background;
        public RSDKv3.Tiles128x128 _RSDK3Chunks;
        #endregion

        #region RSDKvB
        public RSDKv4.BGLayout _RSDK4Background;
        public RSDKv4.Tiles128x128 _RSDK4Chunks;
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
                    if (_RSDK4Background == null) return;
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
                            if (y < _RSDK4Background.Layers[curlayer].MapLayout.Length && x < _RSDK4Background.Layers[curlayer].MapLayout[y].Length && _RSDK4Background.Layers[curlayer].MapLayout[y][x] < _RSDK4Chunks.BlockList.Count)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                        if (_RSDK4Background.Layers[curlayer].MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK4Background.Layers[curlayer].MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
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

                            for (int i = 0; i <= _RSDK4Background.Layers[curlayer].width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK4Background.Layers[curlayer].height * 128);
                            }

                            for (int j = 0; j <= _RSDK4Background.Layers[curlayer].height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK4Background.Layers[curlayer].width * 128, top + j * gridCellSize.Height);
                            }
                        }

                        //e.Graphics.TranslateTransform(e.ClipRectangle.X, e.ClipRectangle.Y);
                        //e.Graphics.DrawEllipse(pen, -4, -4, 8, 8);
                        //e.Graphics.ResetTransform();
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 1:
                    if (_RSDK3Background == null) return;
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
                            if (y < _RSDK3Background.Layers[curlayer].MapLayout.Length && x < _RSDK3Background.Layers[curlayer].MapLayout[y].Length && _RSDK3Background.Layers[curlayer].MapLayout[y][x] < _RSDK3Chunks.BlockList.Count)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (_RSDK3Background.Layers[curlayer].MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK3Background.Layers[curlayer].MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
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

                            for (int i = 0; i <= _RSDK3Background.Layers[curlayer].width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK3Background.Layers[curlayer].height * 128);
                            }

                            for (int j = 0; j <= _RSDK3Background.Layers[curlayer].height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK3Background.Layers[curlayer].width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 2:
                    if (_RSDK2Background == null) return;
                    e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255));
                    Rectangle viewport2 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
                    int startX2 = (viewport2.X / 128) - 1;
                    if (startX2 < 0) startX2 = 0;
                    int startY2 = (viewport2.Y / 128) - 1;
                    if (startY2 < 0) startY2 = 0;
                    int endX2 = startX2 + (pnlMap.Width / 128) + 1;
                    int endY2 = startY2 + (pnlMap.Height / 128) + 1;
                    if (DrawAllLayers)
                    {
                        for (int i = 0; i < _RSDK2Background.Layers.Count; i++)
                        {
                            for (int y = startY2; y < endY2; y++)
                            {
                                for (int x = startX2; x < endX2; x++)
                                {
                                    if (y < _RSDK2Background.Layers[i].MapLayout.Length && x < _RSDK2Background.Layers[i].MapLayout[y].Length && _RSDK2Background.Layers[i].MapLayout[y][x] < _RSDK2Chunks.BlockList.Count)
                                    {
                                        if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                        {
                                            if (ShowMap)
                                            {
                                                if (_RSDK2Background.Layers[i].MapLayout[y][x] > 0)
                                                {
                                                    e.Graphics.DrawImageUnscaled(Chunks[_RSDK2Background.Layers[i].MapLayout[y][x]], x * 128, y * 128);
                                                }
                                                else { }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (!DrawAllLayers)
                    {
                        for (int y = startY2; y < endY2; y++)
                        {
                            for (int x = startX2; x < endX2; x++)
                            {
                                if (y < _RSDK2Background.Layers[curlayer].MapLayout.Length && x < _RSDK2Background.Layers[curlayer].MapLayout[y].Length && _RSDK2Background.Layers[curlayer].MapLayout[y][x] < _RSDK2Chunks.BlockList.Count)
                                {
                                    if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                    {
                                        if (ShowMap)
                                        {
                                            if (_RSDK2Background.Layers[curlayer].MapLayout[y][x] > 0)
                                            {
                                                e.Graphics.DrawImageUnscaled(Chunks[_RSDK2Background.Layers[curlayer].MapLayout[y][x]], x * 128, y * 128);
                                            }
                                            else { }
                                        }
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

                            for (int i = 0; i <= _RSDK2Background.Layers[curlayer].width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK2Background.Layers[curlayer].height * 128);
                            }

                            for (int j = 0; j <= _RSDK2Background.Layers[curlayer].height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK2Background.Layers[curlayer].width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
                    e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
                    break;
                case 3:
                    if (_RSDK1Background == null) return;
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
                            if (y < _RSDK1Background.Layers[curlayer].MapLayout.Length && x < _RSDK1Background.Layers[curlayer].MapLayout[y].Length && _RSDK1Background.Layers[curlayer].MapLayout[y][x] < _RSDK1Chunks.BlockList.Count)
                            {
                                //if (e.ClipRectangle.IntersectsWith(new Rectangle(0, 0, viewport1.Width, viewport1.Height)))
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (ShowMap)
                                    {
                                        if (_RSDK1Background.Layers[curlayer].MapLayout[y][x] > 0)
                                        {
                                            e.Graphics.DrawImageUnscaled(Chunks[_RSDK1Background.Layers[curlayer].MapLayout[y][x]], x * 128, y * 128);
                                        }
                                        else { }
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

                            for (int i = 0; i <= _RSDK1Background.Layers[curlayer].width * 128; ++i)
                            {
                                e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, _RSDK1Background.Layers[curlayer].height * 128);
                            }

                            for (int j = 0; j <= _RSDK1Background.Layers[curlayer].height * 128; ++j)
                            {
                                e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, _RSDK1Background.Layers[curlayer].width * 128, top + j * gridCellSize.Height);
                            }
                        }
                    }
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
                    AutoScrollMinSize = new Size(_RSDK4Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK4Background.Layers[curlayer].MapLayout.Length * 128);
                    break;
                case 1:
                    LoadChunks();
                    AutoScrollMinSize = new Size(_RSDK3Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK3Background.Layers[curlayer].MapLayout.Length * 128);
                    break;
                case 2:
                    LoadChunks();
                    AutoScrollMinSize = new Size(_RSDK2Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK2Background.Layers[curlayer].MapLayout.Length * 128);
                    break;
                case 3:
                    LoadChunks();
                    AutoScrollMinSize = new Size(_RSDK1Background.Layers[curlayer].MapLayout[0].Length * 128, _RSDK1Background.Layers[curlayer].MapLayout.Length * 128);
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
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                    Chunks.Add(b);
                }
            }
            if (loadedRSDKver == 1)
            {
                for (int i = 0; i < _RSDK3Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK3Chunks.BlockList[i].Render(_tiles);
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                    Chunks.Add(b);
                }
            }
            if (loadedRSDKver == 2)
            {
                for (int i = 0; i < _RSDK2Chunks.BlockList.Count; i++)
                {
                    Bitmap b = _RSDK2Chunks.BlockList[i].Render(_tiles);
                    b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
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
                                selChunk = _RSDK4Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 1:
                                selChunk = _RSDK3Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 2:
                                selChunk = _RSDK2Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                                _ChunkView.BlocksList.SelectedIndex = selChunk;
                                _ChunkView.BlocksList.Refresh();
                                break;
                            case 3:
                                selChunk = _RSDK1Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
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
                            chunk = _RSDK4Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 1:
                            chunk = _RSDK3Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 2:
                            chunk = _RSDK2Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                            _ChunkView.BlocksList.SelectedIndex = chunk;
                            _ChunkView.BlocksList.Refresh();
                            break;
                        case 3:
                            chunk = _RSDK1Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
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
                    if (tilePoint.X < _RSDK4Background.Layers[curlayer].width && tilePoint.Y < _RSDK4Background.Layers[curlayer].height)
                    {
                    _RSDK4Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                case 1:
                    if (tilePoint.X < _RSDK3Background.Layers[curlayer].width && tilePoint.Y < _RSDK3Background.Layers[curlayer].height)
                    {
                        _RSDK3Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                case 2:
                    if (tilePoint.X < _RSDK2Background.Layers[curlayer].width && tilePoint.Y < _RSDK2Background.Layers[curlayer].height)
                    {
                        _RSDK2Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                case 3:
                    if (tilePoint.X < _RSDK1Background.Layers[curlayer].width && tilePoint.Y < _RSDK1Background.Layers[curlayer].height)
                    {
                    _RSDK1Background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
                    }
                    break;
                default:
                    break;
            }
        }

    }

}
