using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelManiac
{
    public partial class MainForm : Form
    {
        public string fileName;

        public int ModelType = 0; //1 = vB, 0 = v5

        public int CurFrame = 0;
        public int CurVertex = 0;
        public int CurColour = 0;

        public RSDKv5.Model Modelv5 = new RSDKv5.Model();
        public RSDKvB.Model ModelvB = new RSDKvB.Model();

        public MainForm()
        {
            InitializeComponent();
        }

        public void New()
        {
            RSDKv5.Model Modelv5 = new RSDKv5.Model();
            RSDKvB.Model ModelvB = new RSDKvB.Model();
            RefreshHeader();
            RefreshFrameList();
            RefreshVertexList();
            RefreshUI();
        }

        public void Open(string filepath)
        {
            fileName = filepath;
            switch (ModelType)
            {
                case 0:
                    Modelv5 = new RSDKv5.Model(filepath);
                    RefreshFrameList();
                    RefreshVertexList();
                    RefreshUI();
                    RefreshHeader();
                    break;
                case 1:
                    ModelvB = new RSDKvB.Model(filepath);
                    RefreshFrameList();
                    RefreshVertexList();
                    RefreshUI();
                    RefreshHeader();
                    break;
            }
        }

        public void Save(string filepath)
        {
            fileName = filepath;
            switch (ModelType)
            {
                case 0:
                    Modelv5.Write(new RSDKv5.Writer(filepath));
                    break;
                case 1:
                    ModelvB.Write(new RSDKvB.Writer(filepath));
                    break;
            }
        }

        public void RefreshHeader()
        {
            switch(ModelType)
            {
                case 0:
                    ColoursCB.Enabled = true;
                    NormalsCB.Enabled = true;
                    UnknownCB.Enabled = true;
                    QuadsCB.Enabled = true;
                    ColoursCB.Checked = Modelv5.HasColours;
                    NormalsCB.Checked = Modelv5.HasNormals;
                    UnknownCB.Checked = Modelv5.HasUnknown;
                    QuadsCB.Checked = Modelv5.FaceVerticiesCount == 4;
                    break;
                case 1:
                    ColoursCB.Checked = false;
                    NormalsCB.Checked = false;
                    UnknownCB.Checked = false;
                    QuadsCB.Checked = false;
                    ColoursCB.Enabled = false;
                    NormalsCB.Enabled = false;
                    UnknownCB.Enabled = false;
                    QuadsCB.Enabled = false;
                    break;
            }
        }

        public void RefreshUI()
        {
            MeshColourGrid.Colors.Clear();

            switch(ModelType)
            {
                case 0:
                    if (Modelv5.HasColours)
                    {
                        MeshColourGrid.Enabled = true;
                        for (int i = 0; i < Modelv5.Colours.Count; i++)
                        {
                            Color c = Color.FromArgb(Modelv5.Colours[i].a, Modelv5.Colours[i].r, Modelv5.Colours[i].g, Modelv5.Colours[i].b);
                            MeshColourGrid.Colors.Add(c);
                        }
                    }
                    else
                    {
                        MeshColourGrid.Enabled = false;
                    }
                    if (Modelv5.HasNormals)
                    {
                        NormalXNUD.Enabled = true;
                        NormalYNUD.Enabled = true;
                        NormalZNUD.Enabled = true;

                        NormalXNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].normal.x;
                        NormalYNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].normal.y;
                        NormalZNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].normal.z;
                    }
                    else
                    {
                        NormalXNUD.Enabled = false;
                        NormalYNUD.Enabled = false;
                        NormalZNUD.Enabled = false;
                    }

                    VertexXNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].x;
                    VertexYNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].y;
                    VertexZNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].z;

                    if (Modelv5.FaceVerticiesCount == 4)
                    {
                        VertexWNUD.Enabled = true;
                        VertexWNUD.Value = (decimal)Modelv5.Frames[CurFrame].Vertices[CurVertex].w;
                    }
                    else
                    {
                        VertexWNUD.Enabled = false;
                    }

                    if (Modelv5.HasUnknown)
                    {
                        UnknownXNUD.Enabled = true;
                        UnknownYNUD.Enabled = true;

                        if (Modelv5.TexturePositions.Count > 1)
                        {
                            UnknownXNUD.Value = (decimal)Modelv5.TexturePositions[CurVertex].X;
                            UnknownYNUD.Value = (decimal)Modelv5.TexturePositions[CurVertex].Y;
                        }
                    }
                    else
                    {
                        UnknownXNUD.Enabled = false;
                        UnknownYNUD.Enabled = false;
                    }

                    break;
                case 1:
                    VertexXNUD.Value = (decimal)ModelvB.Vertices[CurVertex].x;
                    VertexYNUD.Value = (decimal)ModelvB.Vertices[CurVertex].y;
                    VertexZNUD.Value = (decimal)ModelvB.Vertices[CurVertex].z;

                    NormalXNUD.Value = (decimal)ModelvB.Vertices[CurVertex].normal.x;
                    NormalYNUD.Value = (decimal)ModelvB.Vertices[CurVertex].normal.y;
                    NormalZNUD.Value = (decimal)ModelvB.Vertices[CurVertex].normal.z;
                    break;
            }
        }

        public void RefreshVertexList()
        {
            VerticiesBox.Items.Clear();
            switch(ModelType)
            {
                case 0:
                    for (int i = 0; i < Modelv5.Frames[CurFrame].Vertices.Count; i++)
                    {
                        VerticiesBox.Items.Add(Modelv5.Frames[CurFrame].Vertices[i].x + " " + Modelv5.Frames[CurFrame].Vertices[i].y + " " + Modelv5.Frames[CurFrame].Vertices[i].z);
                    }
                    break;
                case 1:
                    for (int i = 0; i < ModelvB.Vertices.Count; i++)
                    {
                        VerticiesBox.Items.Add(ModelvB.Vertices[i].x + " " + ModelvB.Vertices[i].y + " " + ModelvB.Vertices[i].z);
                    }
                    break;
            }
        }

        public void RefreshFrameList()
        {
            FramesBox.Items.Clear();
            switch (ModelType)
            {
                case 0:
                    for (int i = 0; i < Modelv5.FramesCount; i++)
                    {
                        FramesBox.Items.Add("Frame " + (i + 1));
                    }
                    break;
                case 1:
                    FramesBox.Items.Add("Model Data");
                    break;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKv5 Models|*.bin|RSDKvB Models|*.bin";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ModelType = (dlg.FilterIndex-1);
                Open(dlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                Save(fileName);
            }
            else
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RSDKv5 Models|*.bin|RSDKvB Models|*.bin";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ModelType = dlg.FilterIndex-1;
                Save(dlg.FileName);
            }
        }

        private void NormalsCB_CheckedChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.HasNormals = NormalsCB.Checked;
                    break;
            }
            RefreshUI();
        }

        private void UnknownCB_CheckedChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.HasUnknown = UnknownCB.Checked;
                    if (Modelv5.TexturePositions.Count < Modelv5.VertexCount && Modelv5.HasUnknown)
                    {
                        int Diff = Modelv5.VertexCount - Modelv5.TexturePositions.Count;
                        for (int u = 0; u < Diff; u++)
                        {
                            Modelv5.TexturePositions.Add(new RSDKv5.Model.TexturePosition());
                        }
                    }
                    break;
            }
            RefreshUI();
        }

        private void ColoursCB_CheckedChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.HasColours = ColoursCB.Checked;
                    if (Modelv5.Colours.Count < Modelv5.VertexCount && Modelv5.HasColours)
                    {
                        int Diff = Modelv5.VertexCount - Modelv5.TexturePositions.Count;
                        for (int u = 0; u < Diff; u++)
                        {
                            Modelv5.Colours.Add(new RSDKv5.Model.Colour());
                        }
                    }
                    break;
            }
            RefreshUI();
        }

        private void QuadsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (QuadsCB.Checked)
            {
                switch (ModelType)
                {
                    case 0:
                        Modelv5.FaceVerticiesCount = 4;
                        break;
                }
            }
            else
            {
                switch (ModelType)
                {
                    case 0:
                        Modelv5.FaceVerticiesCount = 3;
                        break;
                }
            }
            RefreshUI();
        }

        private void FramesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FramesBox.SelectedIndex >= 0)
            {
                CurFrame = FramesBox.SelectedIndex;
                RefreshVertexList();
                RefreshUI();
            }
        }

        private void VerticiesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VerticiesBox.SelectedIndex >= 0)
            {
                CurVertex = VerticiesBox.SelectedIndex;
                RefreshUI();
            }
        }

        private void exportColoursToactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Palette Files|*.act";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Cyotek.Windows.Forms.IPaletteSerializer serializer;

                serializer = new Cyotek.Windows.Forms.AdobeColorTablePaletteSerializer();

                int PalettesCount = 1;
                try
                {
                    if (MeshColourGrid.Colors.Count > 256)
                    {
                        PalettesCount = MeshColourGrid.Colors.Count / 256;
                    }

                    Cyotek.Windows.Forms.ColorCollection[] Palettes = new Cyotek.Windows.Forms.ColorCollection[PalettesCount];
                    int cID = 0;

                    for (int i = 0; i < Palettes.Length; i++)
                    {
                        Palettes[i] = new Cyotek.Windows.Forms.ColorCollection();
                        for (int p = 0; p < 256; p++)
                        {
                            Palettes[i].Add(MeshColourGrid.Colors[cID]);
                            cID++;
                        }
                    }

                    string ext = System.IO.Path.GetExtension(dlg.FileName);
                    string raw = dlg.FileName.Replace(ext, "");
                    for (int i = 0; i < Palettes.Length; i++)
                    {
                        System.IO.Stream stream = System.IO.File.Create(raw + (i + 1) + ext);
                        serializer.Serialize(stream, Palettes[i]); //Save a .act file
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                
            }
        }

        private void importModelFromstlToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = ".obj Models|*obj|STL Models|*.stl|Binary STL Models|*stl";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string ext = System.IO.Path.GetFileName(dlg.FileName);
                switch (ModelType)
                {
                    case 0:
                        switch ((dlg.FilterIndex - 1))
                        {
                            case 0:
                                Modelv5.WriteAsOBJ(dlg.FileName + ".obj", System.IO.Path.GetFileNameWithoutExtension(dlg.FileName));
                                Modelv5.WriteMTL(new RSDKv5.Writer(dlg.FileName + ".mtl"));
                                break;
                            case 1:
                                Modelv5.WriteAsSTL(dlg.FileName + ".stl");
                                break;
                            case 2:
                                Modelv5.WriteAsSTLBinary(new RSDKv5.Writer(dlg.FileName + ".stl"));
                                break;
                        }
                        break;
                    case 1:
                        switch (dlg.FilterIndex - 1)
                        {
                            case 0:
                                ModelvB.WriteAsOBJ(dlg.FileName + ".obj", System.IO.Path.GetFileNameWithoutExtension(dlg.FileName));
                                ModelvB.WriteMTL(new RSDKvB.Writer(dlg.FileName + ".mtl"));
                                break;
                            case 1:
                                ModelvB.WriteAsSTL(dlg.FileName + ".stl");
                                break;
                            case 2:
                                ModelvB.WriteAsSTLBinary(new RSDKvB.Writer(dlg.FileName + ".stl"));
                                break;
                        }
                        break;
                }
            }
        }

        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            switch(ModelType)
            {
                case 0:
                    Modelv5.Frames[CurFrame].AddVertex();
                    Modelv5.VertexCount++;
                    break;
                case 1:
                    ModelvB.Vertices.Add(new RSDKvB.Model.Vertex());
                    break;
            }
            RefreshVertexList();
            RefreshUI();
        }

        private void DeleteVertexButton_Click(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.Frames[CurFrame].DeleteVertex(CurVertex);
                    Modelv5.VertexCount--;
                    if (CurVertex > 0) CurVertex--;
                    break;
                case 1:
                    ModelvB.Vertices.RemoveAt(CurVertex);
                    break;
            }
            RefreshVertexList();
            RefreshUI();
        }

        private void AddFrameButton_Click(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.AddFrame();                    
                    break;
            }
            RefreshFrameList();
            RefreshVertexList();
            RefreshUI();
        }

        private void DeleteFrameButton_Click(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.DeleteFrame(CurFrame);
                    if (CurFrame > 0) CurFrame--;
                    break;
            }
            RefreshFrameList();
            RefreshVertexList();
            RefreshUI();
        }

        private void MeshColourEditor_ColorChanged(object sender, EventArgs e)
        {
            MeshColourGrid.Colors[CurColour] = MeshColourEditor.Color;
            switch(ModelType)
            {
                case 0:
                    Modelv5.Colours[CurColour].a = MeshColourEditor.Color.A;
                    Modelv5.Colours[CurColour].r = MeshColourEditor.Color.R;
                    Modelv5.Colours[CurColour].g = MeshColourEditor.Color.G;
                    Modelv5.Colours[CurColour].b = MeshColourEditor.Color.B;
                    break;
                case 1:
                    break;
            }
        }

        private void MeshColourGrid_ColorChanged(object sender, EventArgs e)
        {
            CurColour = MeshColourGrid.ColorIndex;
            MeshColourEditor.Color = MeshColourGrid.Colors[MeshColourGrid.ColorIndex];
        }

        private void NormalXNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void NormalYNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void NormalZNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void VertexXNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void VertexYNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void VertexZNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void VertexWNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void UnknownXNUD_ValueChanged(object sender, EventArgs e)
        {
            switch(ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void UnknownYNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }

        private void AddColourButton_Click(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    RSDKv5.Model.Colour c = new RSDKv5.Model.Colour();
                    c.r = c.a = c.b = 255;
                    c.g = 0;
                    Modelv5.Colours.Add(c);
                    MeshColourGrid.Colors.Add(Color.FromArgb(255, 255, 0, 255));
                    break;
            }
        }

        private void DeleteColourButton_Click(object sender, EventArgs e)
        {
            switch (ModelType)
            {
                case 0:
                    Modelv5.Colours.RemoveAt(CurColour);
                    MeshColourGrid.Colors.RemoveAt(CurColour);
                    break;
            }
        }
    }
}
