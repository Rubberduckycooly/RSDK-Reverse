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
    public partial class PropertiesForm : Form
    {

        public int LoadedRSDKver;

        public RSDKv1.Level Mapv1;
        public RSDKv2.Level Mapv2;
        public RSDKv3.Level Mapv3;
        public RSDKv4.Level Mapv4;

        public PropertiesForm(int RSDKver)
        {
            InitializeComponent();
            LoadedRSDKver = RSDKver;
        }

        public void Setup()
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    MapNameBox.Text = Mapv1.Title;
                    MapWidthNUD.Value = Mapv1.width;
                    MapHeightNUD.Value = Mapv1.height;
                    RSMusicNUD.Value = Mapv1.Music;
                    RSBGNUD.Value = Mapv1.Background;
                    RSPlayerPosXNUD.Value = Mapv1.PlayerXpos;
                    RSPlayerPosYNUD.Value = Mapv1.PlayerYPos;
                    break;
                case 2:
                    MapNameBox.Text = Mapv2.Title;
                    MapWidthNUD.Value = Mapv2.width;
                    MapHeightNUD.Value = Mapv2.height;
                    RSMusicNUD.Enabled = false;
                    RSBGNUD.Enabled = false;
                    RSPlayerPosXNUD.Enabled = false;
                    RSPlayerPosYNUD.Enabled = false;
                    break;
                case 1:
                    MapNameBox.Text = Mapv3.Title;
                    MapWidthNUD.Value = Mapv3.width;
                    MapHeightNUD.Value = Mapv3.height;
                    RSMusicNUD.Enabled = false;
                    RSBGNUD.Enabled = false;
                    RSPlayerPosXNUD.Enabled = false;
                    RSPlayerPosYNUD.Enabled = false;
                    break;
                case 0:
                    MapNameBox.Text = Mapv4.Title;
                    MapWidthNUD.Value = Mapv4.width;
                    MapHeightNUD.Value = Mapv4.height;
                    RSMusicNUD.Enabled = false;
                    RSBGNUD.Enabled = false;
                    RSPlayerPosXNUD.Enabled = false;
                    RSPlayerPosYNUD.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MapNameBox_TextChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.Title = MapNameBox.Text;
                    break;
                case 2:
                    Mapv2.Title = MapNameBox.Text;
                    break;
                case 1:
                    Mapv3.Title = MapNameBox.Text;
                    break;
                case 0:
                    Mapv4.Title = MapNameBox.Text;
                    break;
                default:
                    break;
            }
        }

        private void MapWidthNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.width = (int)MapWidthNUD.Value;
                    break;
                case 2:
                    Mapv2.width = (int)MapWidthNUD.Value;
                    break;
                case 1:
                    Mapv3.width = (int)MapWidthNUD.Value;
                    break;
                case 0:
                    Mapv4.width = (int)MapWidthNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void MapHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver)
            {
                case 3:
                    Mapv1.height = (int)MapHeightNUD.Value;
                    break;
                case 2:
                    Mapv2.height = (int)MapHeightNUD.Value;
                    break;
                case 1:
                    Mapv3.height = (int)MapHeightNUD.Value;
                    break;
                case 0:
                    Mapv4.height = (int)MapHeightNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void RSMusicNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3)
            {
                Mapv1.Music = (byte)RSMusicNUD.Value;
            }
        }

        private void RSBGNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3)
            {
                Mapv1.Background = (byte)RSBGNUD.Value;
            }
        }

        private void RSPlayerPosXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3)
            {
                Mapv1.PlayerXpos = (ushort)RSPlayerPosXNUD.Value;
            }
        }

        private void RSPlayerPosYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3)
            {
                Mapv1.PlayerYPos = (ushort)RSPlayerPosYNUD.Value;
            }
        }
    }
}
