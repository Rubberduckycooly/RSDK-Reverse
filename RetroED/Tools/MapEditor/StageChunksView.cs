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
        public int loadedRSDKver = 0;
        public Image _tiles;

        public List<Bitmap> Chunks = new List<Bitmap>();

        public StageMapView MapView;

        //private Point tilePoint;
        public int selectedChunk;

        #region Retro-Sonic Development Kit
        public RSDKv1.til _RSDK1Chunks;
        public List<RSDKv1.Object> objectsV1 = new List<RSDKv1.Object>();
        #endregion

        #region RSDKv1
        public RSDKv2.Tiles128x128 _RSDK2Chunks;
        public List<RSDKv2.Object> objectsV2 = new List<RSDKv2.Object>();
        #endregion

        #region RSDKv2
        public RSDKv3.Tiles128x128 _RSDK3Chunks;
        public List<RSDKv3.Object> objectsV3 = new List<RSDKv3.Object>();
        #endregion

        #region RSDKvB
        public RSDKv4.Tiles128x128 _RSDK4Chunks;
        public List<RSDKv4.Object> objectsV4 = new List<RSDKv4.Object>();
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
                    for (int i = 0; i < _RSDK4Chunks.BlockList.Count; i++)
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
                    for (int i = 0; i < _RSDK3Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK3Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 2:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK2Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK2Chunks.BlockList[i].Render(_tiles);
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b);
                        BlocksList.Images.Add(b);
                    }
                    break;
                case 3:
                    BlocksList.Images.Clear();
                    for (int i = 0; i < _RSDK1Chunks.BlockList.Count; i++)
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

        private void ObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ObjectList_DoubleClick(object sender, EventArgs e)
        {
            NewObjectForm frm = new NewObjectForm();
            switch(loadedRSDKver)
            {
                case 3:
                    frm.TypeNUD.Value = objectsV1[ObjectList.SelectedIndex].type;
                    frm.SubtypeNUD.Value = objectsV1[ObjectList.SelectedIndex].subtype;
                    frm.XposNUD.Value = objectsV1[ObjectList.SelectedIndex].xPos;
                    frm.YposNUD.Value = objectsV1[ObjectList.SelectedIndex].yPos;
                    break;
                case 2:
                    frm.TypeNUD.Value = objectsV2[ObjectList.SelectedIndex].type;
                    frm.SubtypeNUD.Value = objectsV2[ObjectList.SelectedIndex].subtype;
                    frm.XposNUD.Value = objectsV2[ObjectList.SelectedIndex].xPos;
                    frm.YposNUD.Value = objectsV2[ObjectList.SelectedIndex].yPos;
                    break;
                case 1:
                    frm.TypeNUD.Value = objectsV3[ObjectList.SelectedIndex].type;
                    frm.SubtypeNUD.Value = objectsV3[ObjectList.SelectedIndex].subtype;
                    frm.XposNUD.Value = objectsV3[ObjectList.SelectedIndex].xPos;
                    frm.YposNUD.Value = objectsV3[ObjectList.SelectedIndex].yPos;
                    break;
                case 0:
                    frm.TypeNUD.Value = objectsV4[ObjectList.SelectedIndex].type;
                    frm.SubtypeNUD.Value = objectsV4[ObjectList.SelectedIndex].subtype;
                    frm.XposNUD.Value = objectsV4[ObjectList.SelectedIndex].xPos;
                    frm.YposNUD.Value = objectsV4[ObjectList.SelectedIndex].yPos;
                    break;
            }

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                switch (loadedRSDKver)
                {
                    case 3:
                        RSDKv1.Object Obj1 = new RSDKv1.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        MapView._RSDK1Level.objects[ObjectList.SelectedIndex] = Obj1;
                        objectsV1[ObjectList.SelectedIndex] = Obj1;
                        break;
                    case 2:
                        RSDKv2.Object Obj2 = new RSDKv2.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        MapView._RSDK2Level.objects[ObjectList.SelectedIndex] = Obj2;
                        objectsV2[ObjectList.SelectedIndex] = Obj2;
                        break;
                    case 1:
                        RSDKv3.Object Obj3 = new RSDKv3.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        MapView._RSDK3Level.objects[ObjectList.SelectedIndex] = Obj3;
                        objectsV3[ObjectList.SelectedIndex] = Obj3;
                        break;
                    case 0:
                        RSDKv4.Object Obj4 = new RSDKv4.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos);
                        MapView._RSDK4Level.objects[ObjectList.SelectedIndex] = Obj4;
                        objectsV4[ObjectList.SelectedIndex] = Obj4;
                        break;
                }
            }
            RefreshObjList();
        }

        public void RefreshObjList()
        {
            ObjectList.Items.Clear();
            switch(loadedRSDKver)
            {
                case 3:
                    Object_Definitions.Retro_SonicGlobalObjects RSObj = new Object_Definitions.Retro_SonicGlobalObjects();
                    for (int i = 0; i < objectsV1.Count; i++)
                    {
                        string Obj;
                        string t = RSObj.GetObjectByType(objectsV1[i].type, objectsV1[i].subtype).Name;
                        if (t != null) { Obj = t + ", " + objectsV1[i].xPos + ", " + objectsV1[i].yPos; }
                        else { Obj = "Unnamed Object" + ", " + objectsV1[i].xPos + ", " + objectsV1[i].yPos; }
                        ObjectList.Items.Add(Obj);
                    }
                    break;
                case 2:
                    Object_Definitions.SonicNexusGlobalObjects RSDK2Obj = new Object_Definitions.SonicNexusGlobalObjects();
                    for (int i = 0; i < objectsV2.Count; i++)
                    {
                        string Obj;
                        string t = RSDK2Obj.GetObjectByType(objectsV2[i].type, objectsV2[i].subtype).Name;
                        if (t != null) { Obj = t + ", " + objectsV2[i].xPos + ", " + objectsV2[i].yPos; }
                        else { Obj = "Unnamed Object" + ", " + objectsV2[i].xPos + ", " + objectsV2[i].yPos; }
                        ObjectList.Items.Add(Obj);
                    }
                    break;
                case 1:
                    Object_Definitions.SonicCDGlobalObjects RSDK3Obj = new Object_Definitions.SonicCDGlobalObjects();
                    for (int i = 0; i < objectsV3.Count; i++)
                    {
                        string Obj;
                        string t = RSDK3Obj.GetObjectByType(objectsV3[i].type, objectsV3[i].subtype).Name;
                        if (t != null) { Obj = t + ", " + objectsV3[i].xPos + ", " + objectsV3[i].yPos; }
                        else { Obj = "Unnamed Object" + ", " + objectsV3[i].xPos + ", " + objectsV3[i].yPos; }
                        ObjectList.Items.Add(Obj);
                    }
                    break;
                case 0:
                    Object_Definitions.Sonic1GlobalObjects RSDKBObj = new Object_Definitions.Sonic1GlobalObjects();
                    for (int i = 0; i < objectsV4.Count; i++)
                    {
                        string Obj;
                        string t = RSDKBObj.GetObjectByType(objectsV4[i].type, objectsV4[i].subtype).Name;
                        if (t != null) { Obj = t + ", " + objectsV4[i].xPos + ", " + objectsV4[i].yPos; }
                        else { Obj = "Unnamed Object" + ", " + objectsV4[i].xPos + ", " + objectsV4[i].yPos; }
                        ObjectList.Items.Add(Obj);
                    }
                    break;
            }
        }
    }
}
