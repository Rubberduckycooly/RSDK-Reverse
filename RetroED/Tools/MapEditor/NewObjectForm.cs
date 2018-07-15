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
        public ushort Subtype;
        public ushort Xpos;
        public ushort Ypos;

        //Are we making a new object or Editing an existing one?
        public int RemoveObject = 0;

        public NewObjectForm(int formType)
        {
            InitializeComponent();
            if (formType == (int)FormType.NewObject) //We can't delete an object that doesn't exist!
            {
                RemoveObjectButton.Enabled = false;
            }
        }

        private void TypeNUD_ValueChanged(object sender, EventArgs e)
        {
            Type = (ushort)TypeNUD.Value; //Change the Object's Type
        }

        private void SubtypeNUD_ValueChanged(object sender, EventArgs e)
        {
            Subtype = (ushort)SubtypeNUD.Value; //Change the Object's SubType
        }

        private void XposNUD_ValueChanged(object sender, EventArgs e)
        {
            Xpos = (ushort)XposNUD.Value; //Change where the object is on the map (X Position)
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
    }
}
