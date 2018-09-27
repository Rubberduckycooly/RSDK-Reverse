using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Tools.MapEditor
{
    public partial class NewObjectForm : Form
    {

        enum FormType //Are we making a new object or Editing an existing one?
        {
            NewObject,
            EditObject
        }

        //Object Values
        public ushort Type;
        public short Subtype;
        public short Xpos;
        public ushort Ypos;

        public int RSDKver = 0;

        //Retro-Sonic Global Objects
        public Object_Definitions.Retro_SonicObjects RSObjects = new Object_Definitions.Retro_SonicObjects();

        //Sonic Nexus Global Objects
        public Object_Definitions.SonicNexusObjects NexusObjects = new Object_Definitions.SonicNexusObjects();

        //Sonic CD Global Objects
        public Object_Definitions.SonicCDObjects CDObjects = new Object_Definitions.SonicCDObjects();

        //Sonic 1/2 Global Objects
        public Object_Definitions.Sonic1Objects S1Objects = new Object_Definitions.Sonic1Objects();

        //Are we making a new object or Editing an existing one?
        public int RemoveObject = 0;

        public NewObjectForm(int RSDKVersion, int formType)
        {
            RSDKver = RSDKVersion;
            InitializeComponent();
            if (formType == (int)FormType.NewObject) //We can't delete an object that doesn't exist!
            {
                RemoveObjectButton.Enabled = false;
            }
        }

        public void SetupObjects()
        {
            switch (RSDKver) //Set the object Definitions
            {
                case 3:
                    foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in RSObjects.Objects)
                    {
                        ObjectSelectBox.Items.Add(RSObjects.Objects[obj.Key].Name);
                    }
                    break;
                case 2:
                    foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in NexusObjects.Objects)
                    {
                        ObjectSelectBox.Items.Add(NexusObjects.Objects[obj.Key].Name);
                    }
                    break;
                case 1:
                    foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in CDObjects.Objects)
                    {
                        ObjectSelectBox.Items.Add(CDObjects.Objects[obj.Key].Name);
                    }
                    break;
                case 0:
                    foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in S1Objects.Objects)
                    {
                        ObjectSelectBox.Items.Add(S1Objects.Objects[obj.Key].Name);
                    }
                    break;
            }
        }

        private void TypeNUD_ValueChanged(object sender, EventArgs e)
        {
            Type = (ushort)TypeNUD.Value; //Change the Object's Type
        }

        private void SubtypeNUD_ValueChanged(object sender, EventArgs e)
        {
            Subtype = (short)SubtypeNUD.Value; //Change the Object's SubType
        }

        private void XposNUD_ValueChanged(object sender, EventArgs e)
        {
            Xpos = (short)XposNUD.Value; //Change where the object is on the map (X Position)
        }

        private void YposNUD_ValueChanged(object sender, EventArgs e)
        {
            Ypos = (ushort)YposNUD.Value; //Change where the object is on the map (Y Position)
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; //OK
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RemoveObjectButton_Click(object sender, EventArgs e)
        {
            RemoveObject = 1;
            this.DialogResult = DialogResult.OK; //We want to remove the object so set remove object to 1
            this.Close();
        }

        private void ObjectSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ObjectSelectBox.SelectedIndex >= 0)
            {
                int nType = 0;
                int nSubType = 0;
                int i = 0;

                switch (RSDKver) //Set the object Definitions
                {
                    case 3:
                        foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in RSObjects.Objects)
                        {
                            if (i == ObjectSelectBox.SelectedIndex)
                            {
                                nType = RSObjects.Objects[obj.Key].ID;
                                nSubType = RSObjects.Objects[obj.Key].SubType;
                                break;
                            }
                            i++;
                        }
                        break;
                    case 2:
                        foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in NexusObjects.Objects)
                        {
                            if (i == ObjectSelectBox.SelectedIndex)
                            {
                                nType = NexusObjects.Objects[obj.Key].ID;
                                nSubType = NexusObjects.Objects[obj.Key].SubType;
                                break;
                            }
                            i++;
                        }
                        break;
                    case 1:
                        foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in CDObjects.Objects)
                        {
                            if (i == ObjectSelectBox.SelectedIndex)
                            {
                                nType = CDObjects.Objects[obj.Key].ID;
                                nSubType = CDObjects.Objects[obj.Key].SubType;
                                break;
                            }
                            i++;
                        }
                        break;
                    case 0:
                        foreach (System.Collections.Generic.KeyValuePair<Point, Object_Definitions.MapObject> obj in S1Objects.Objects)
                        {
                            if (i == ObjectSelectBox.SelectedIndex)
                            {
                                nType = S1Objects.Objects[obj.Key].ID;
                                nSubType = S1Objects.Objects[obj.Key].SubType;
                                break;
                            }
                            i++;
                        }
                        break;
                }

                Type = (ushort)nType;
                TypeNUD.Value = (ushort)nType;

                Subtype = (byte)nSubType;
                SubtypeNUD.Value = (byte)nSubType;
            }
        }
    }
}
