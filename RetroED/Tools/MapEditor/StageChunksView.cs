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

        //private Point tilePoint;

        //What Chunk is selected?
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
                    for (int i = 0; i < _RSDK4Chunks.BlockList.Count; i++)
                    {
                        Bitmap b = _RSDK4Chunks.BlockList[i].Render(_tiles); //Render chunk using the tileset as a source
                        //b.MakeTransparent(Color.FromArgb(255, 255, 0, 255));
                        Chunks.Add(b); //Add it to the list of chunk images
                        BlocksList.Images.Add(b); //add it to the "List" of chunks that the user selects from
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
            selectedChunk = BlocksList.SelectedIndex; //Update the selected chunk
        }

        private void ObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ObjectList_DoubleClick(object sender, EventArgs e)
        {
            NewObjectForm frm = new NewObjectForm(1); //We are modifying an object so the "formtype" should be 1
            switch(loadedRSDKver) //Set the form's values to the selected Object's values
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
                if (frm.RemoveObject == 0) //Make sure we didn't decide to remove the object
                {
                    switch (loadedRSDKver) //Set the new values
                    {
                        case 3:
                            RSDKv1.Object Obj1 = new RSDKv1.Object(frm.Type, frm.Subtype, frm.Xpos, frm.Ypos); //Create a temp object to store our data
                            MapView._RSDK1Level.objects[ObjectList.SelectedIndex] = Obj1; //Modify the selected object's data
                            objectsV1[ObjectList.SelectedIndex] = Obj1; //Modify the selected object's data
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
                else if (frm.RemoveObject == 1)
                {
                    switch (loadedRSDKver)
                    {
                        case 3:
                            MapView._RSDK1Level.objects.RemoveAt(ObjectList.SelectedIndex); //Delete Selected Object
                            break;
                        case 2:
                            MapView._RSDK2Level.objects.RemoveAt(ObjectList.SelectedIndex); //Delete Selected Object
                            break;
                        case 1:
                            MapView._RSDK3Level.objects.RemoveAt(ObjectList.SelectedIndex); //Delete Selected Object
                            break;
                        case 0:
                            MapView._RSDK4Level.objects.RemoveAt(ObjectList.SelectedIndex); //Delete Selected Object
                            break;
                    }
                }
                
            }
            RefreshObjList(); //Reload the list of objects
        }

        public void RefreshObjList()
        {
            ObjectList.Items.Clear(); //We want a full refresh, so clear out all previous objects
            switch(loadedRSDKver)
            {
                case 3:
                    Object_Definitions.Retro_SonicObjects RSObj = new Object_Definitions.Retro_SonicObjects();
                    for (int i = 0; i < objectsV1.Count; i++)
                    {
                        string Obj;
                        string t = RSObj.GetObjectByType(objectsV1[i].type, objectsV1[i].subtype).Name; //Get the object name
                        if (t != null) { Obj = t + ", " + objectsV1[i].xPos + ", " + objectsV1[i].yPos; } //If the object's definition is found, we use it's name in the list
                        else { Obj = "Unnamed Object" + ", " + objectsV1[i].xPos + ", " + objectsV1[i].yPos; } //If not, call it "Unnamed Object"
                        ObjectList.Items.Add(Obj);
                    }
                    break;
                case 2:
                    Object_Definitions.SonicNexusObjects RSDK2Obj = new Object_Definitions.SonicNexusObjects();
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
                    Object_Definitions.SonicCDObjects RSDK3Obj = new Object_Definitions.SonicCDObjects();
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
                    Object_Definitions.Sonic1Objects RSDKBObj = new Object_Definitions.Sonic1Objects();
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
