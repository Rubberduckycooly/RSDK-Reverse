using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectTypeFixer
{
    public partial class MainForm : Form
    {

        Retro_Formats.Scene scene = new Retro_Formats.Scene();

        int OldSize = 0;
        int NewSize = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OldGCSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            OldSize = (int)OldGCSizeNUD.Value;
        }

        private void NewGCSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            NewSize = (int)NewGCSizeNUD.Value;
        }

        private void FixStageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RSDKvRS Scenes|Act*.map|RSDKv1 Scenes|Act*.bin|RSDKv2 Scenes|Act*.bin|RSDKvB Scenes|Act*.bin";

            Retro_Formats.EngineType engineType = Retro_Formats.EngineType.RSDKv2;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                switch(dlg.FilterIndex-1)
                {
                    case 0:
                        engineType = Retro_Formats.EngineType.RSDKvRS;
                        break;
                    case 1:
                        engineType = Retro_Formats.EngineType.RSDKv1;
                        break;
                    case 2:
                        engineType = Retro_Formats.EngineType.RSDKv2;
                        break;
                    case 3:
                        engineType = Retro_Formats.EngineType.RSDKvB;
                        break;
                }
                scene.ImportFrom(engineType, dlg.FileName);
                scene.ExportTo(engineType, dlg.FileName + ".bak"); //create a backup

                int diff = NewSize - OldSize;

                for (int i = 0; i < scene.objects.Count; i++)
                {
                    if (scene.objects[i].type > OldSize)
                    {
                        scene.objects[i].type += (byte)diff;
                    }
                }

                scene.ExportTo(engineType, dlg.FileName); //save our scene
            }
        }
    }
}
