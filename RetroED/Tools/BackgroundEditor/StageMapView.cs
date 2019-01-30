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
        public Retro_Formats.EngineType engineType;

        public int curlayer = 0;

        public bool DrawAllLayers = false;

        public Image _tiles;
        public MainView Parent;

        public int PlacementMode = 0;
        private Bitmap PanelMap;

        public bool ShowMap = true;
        public bool ShowBG = false;
        public bool ShowObjects = false;
        public bool ShowCollision = false;
        public bool ShowGrid = false;

        public List<Bitmap> Chunks = new List<Bitmap>();

        public Point tilePoint;

        public bool DrawLines = false;

        public StageMapView(MainView p)
        {
            Parent = p;
            InitializeComponent();
            pnlMap.Paint += pnlMap_Paint;
            pnlMap.Resize += ((sender, e) => Refresh());
        }

        void pnlMap_Paint(object sender, PaintEventArgs e)
        {
            Size gridCellSize = new Size(128, 128);
            Pen p = new Pen(System.Drawing.Color.Yellow);
            if (Parent.background == null) return;
            e.Graphics.Clear(Color.FromArgb(255, 255, 0, 255));

            Rectangle viewport4 = new Rectangle(-pnlMap.AutoScrollPosition.X, -pnlMap.AutoScrollPosition.Y, pnlMap.Width, pnlMap.Height);
            int startX4 = (viewport4.X / 128) - 1;
            if (startX4 < 0) startX4 = 0;
            int startY4 = (viewport4.Y / 128) - 1;
            if (startY4 < 0) startY4 = 0;
            int endX4 = startX4 + (pnlMap.Width / 128) + 1;
            int endY4 = startY4 + (pnlMap.Height / 128) + 1;
            if (DrawAllLayers)
            {
                for (int l = 0; l < Parent.background.Layers.Count; l++)
                {
                    for (int y = startY4; y < endY4; y++)
                    {
                        for (int x = startX4; x < endX4; x++)
                        {
                            if (y < Parent.background.Layers[l].MapLayout.Length && x < Parent.background.Layers[l].MapLayout[y].Length && Parent.background.Layers[l].MapLayout[y][x] < Parent.Chunks.ChunkList.Length)
                            {
                                if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                                {
                                    if (Parent.background.Layers[l].MapLayout[y][x] > 0)
                                    {
                                        e.Graphics.DrawImageUnscaled(Chunks[Parent.background.Layers[l].MapLayout[y][x]], x * 128, y * 128);
                                    }
                                    else { }
                                }
                            }
                        }
                    }
                }
            }
            else if (!DrawAllLayers)
            {
                for (int y = startY4; y < endY4; y++)
                {
                    for (int x = startX4; x < endX4; x++)
                    {
                        if (y < Parent.background.Layers[curlayer].MapLayout.Length && x < Parent.background.Layers[curlayer].MapLayout[y].Length && Parent.background.Layers[curlayer].MapLayout[y][x] < Parent.Chunks.ChunkList.Length)
                        {
                            if (e.ClipRectangle.IntersectsWith(new Rectangle(x * 128, y * 128, 128, 128)))
                            {
                                if (Parent.background.Layers[curlayer].MapLayout[y][x] > 0)
                                {
                                    e.Graphics.DrawImageUnscaled(Chunks[Parent.background.Layers[curlayer].MapLayout[y][x]], x * 128, y * 128);
                                }
                                else { }
                            }
                        }
                    }
                }
            }

            if (DrawLines)
            {
                for (int i = 0; i < Parent.background.Layers[curlayer].LineIndexes.Count; i++)
                {
                    e.Graphics.DrawLine(p, 0, Parent.background.Layers[curlayer].LineIndexes[i] * 4, Parent.background.Layers[curlayer].width * 128, Parent.background.Layers[curlayer].LineIndexes[i] * 4);
                }
            }

            if (ShowGrid)
            {
                Pen pen = new Pen(Color.DarkGray);

                if (gridCellSize.Width >= 8 && gridCellSize.Height >= 8)
                {
                    int lft = 0 % gridCellSize.Width;
                    int top = 0 % gridCellSize.Height;

                    for (int i = 0; i <= Parent.background.Layers[curlayer].width * 128; ++i)
                    {
                        e.Graphics.DrawLine(pen, lft + i * gridCellSize.Width, 0, lft + i * gridCellSize.Width, Parent.background.Layers[curlayer].height * 128);
                    }

                    for (int j = 0; j <= Parent.background.Layers[curlayer].height * 128; ++j)
                    {
                        e.Graphics.DrawLine(pen, 0, top + j * gridCellSize.Height, Parent.background.Layers[curlayer].width * 128, top + j * gridCellSize.Height);
                    }
                }

                //e.Graphics.TranslateTransform(e.ClipRectangle.X, e.ClipRectangle.Y);
                //e.Graphics.DrawEllipse(pen, -4, -4, 8, 8);
                //e.Graphics.ResetTransform();
            }
            e.Graphics.DrawRectangle(p, new Rectangle(tilePoint.X * 128, tilePoint.Y * 128, 128, 128));
            GC.Collect();
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
            GC.Collect();
        }

        public void SetLevel()
        {
            LoadChunks();
            AutoScrollMinSize = new Size(Parent.background.Layers[curlayer].MapLayout[0].Length * 128, Parent.background.Layers[curlayer].MapLayout.Length * 128);
        }

        void LoadChunks()
        {
            Chunks.Clear();
            for (int i = 0; i < Parent.Chunks.ChunkList.Length; i++)
            {
                Bitmap b = Parent.Chunks.ChunkList[i].Render(_tiles);
                b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                Chunks.Add(b);
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
                        SetChunk(tilePoint, (ushort)Parent._blocksViewer.selectedChunk);
                        DrawLevel();
                    }
                    break;
                case MouseButtons.Right:
                    if (PlacementMode == 0)
                    {
                        tilePoint = tilePointNew;
                        DrawLevel();
                        int selChunk;
                        selChunk = Parent.background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                        Parent._blocksViewer.BlocksList.SelectedIndex = selChunk;
                        Parent._blocksViewer.BlocksList.Refresh();
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
                    chunk = Parent.background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X];
                    Parent._blocksViewer.BlocksList.SelectedIndex = chunk;
                    Parent._blocksViewer.BlocksList.Refresh();
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
                        SetChunk(tilePoint, (ushort)Parent._blocksViewer.selectedChunk);
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
            if (tilePoint.X < Parent.background.Layers[curlayer].width && tilePoint.Y < Parent.background.Layers[curlayer].height)
            {
                Parent.background.Layers[curlayer].MapLayout[tilePoint.Y][tilePoint.X] = NewChunk;
            }
        }

    }

}
