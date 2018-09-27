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

        public RSDKvRS.Scene Mapv1;
        public RSDKv1.Scene Mapv2;
        public RSDKv2.Scene Mapv3;
        public RSDKvB.Scene Mapv4;

        public PropertiesForm(int RSDKver)
        {
            InitializeComponent();
            LoadedRSDKver = RSDKver;
        }

        public void Setup()
        {
            switch (LoadedRSDKver) //Set the UI values to the loaded ones
            {
                case 3:
                    MapNameBox.Text = Mapv1.Title;
                    MapWidthNUD.Value = Mapv1.width;
                    MapHeightNUD.Value = Mapv1.height;
                    RSMusicNUD.Value = Mapv1.Music;
                    RSBGNUD.Value = Mapv1.Background;
                    RSPlayerPosXNUD.Value = Mapv1.PlayerXpos;
                    RSPlayerPosYNUD.Value = Mapv1.PlayerYPos;
                    MidpointNUD.Enabled = false;
                    Layer0NUD.Enabled = false;
                    Layer1NUD.Enabled = false;
                    Layer2NUD.Enabled = false;
                    Layer3NUD.Enabled = false;
                    break;
                case 2:
                    MapNameBox.Text = Mapv2.Title;
                    MapWidthNUD.Value = Mapv2.width;
                    MapHeightNUD.Value = Mapv2.height;
                    MidpointNUD.Value = Mapv2.Midpoint;
                    Layer0NUD.Value = Mapv2.ActiveLayer0;
                    Layer1NUD.Value = Mapv2.ActiveLayer1;
                    Layer2NUD.Value = Mapv2.ActiveLayer2;
                    Layer3NUD.Value = Mapv2.ActiveLayer3;
                    RSMusicNUD.Enabled = false;
                    RSBGNUD.Enabled = false;
                    RSPlayerPosXNUD.Enabled = false;
                    RSPlayerPosYNUD.Enabled = false;
                    break;
                case 1:
                    MapNameBox.Text = Mapv3.Title;
                    MapWidthNUD.Value = Mapv3.width;
                    MapHeightNUD.Value = Mapv3.height;
                    MidpointNUD.Value = Mapv3.Midpoint;
                    Layer0NUD.Value = Mapv3.ActiveLayer0;
                    Layer1NUD.Value = Mapv3.ActiveLayer1;
                    Layer2NUD.Value = Mapv3.ActiveLayer2;
                    Layer3NUD.Value = Mapv3.ActiveLayer3;
                    RSMusicNUD.Enabled = false;
                    RSBGNUD.Enabled = false;
                    RSPlayerPosXNUD.Enabled = false;
                    RSPlayerPosYNUD.Enabled = false;
                    break;
                case 0:
                    MapNameBox.Text = Mapv4.Title;
                    MapWidthNUD.Value = Mapv4.width;
                    MapHeightNUD.Value = Mapv4.height;
                    MidpointNUD.Value = Mapv4.Midpoint;
                    Layer0NUD.Value = Mapv4.ActiveLayer0;
                    Layer1NUD.Value = Mapv4.ActiveLayer1;
                    Layer2NUD.Value = Mapv4.ActiveLayer2;
                    Layer3NUD.Value = Mapv4.ActiveLayer3;
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
            switch (LoadedRSDKver) //Change the name of the map
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
            switch (LoadedRSDKver) //Change Map Width
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
            switch (LoadedRSDKver) //Change Map Height
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
            if (LoadedRSDKver == 3) //if we are using a Retro-Sonic map, Set the Music value
            {
                Mapv1.Music = (byte)RSMusicNUD.Value;
            }
        }

        private void RSBGNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3) //if we are using a Retro-Sonic map, Set the Background value
            {
                Mapv1.Background = (byte)RSBGNUD.Value;
            }
        }

        private void RSPlayerPosXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3) //if we are using a Retro-Sonic map, Set the Player Spawn Xpos
            {
                Mapv1.PlayerXpos = (ushort)RSPlayerPosXNUD.Value;
            }
        }

        private void RSPlayerPosYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (LoadedRSDKver == 3) //if we are using a Retro-Sonic map, Set the Player Spawn Ypos
            {
                Mapv1.PlayerYPos = (ushort)RSPlayerPosYNUD.Value;
            }
        }

        private void MidpointNUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Change Map Layer Midpoint
            {
                case 2:
                    Mapv2.Midpoint = (byte)MidpointNUD.Value;
                    break;
                case 1:
                    Mapv3.Midpoint = (byte)MidpointNUD.Value;
                    break;
                case 0:
                    Mapv4.Midpoint = (byte)MidpointNUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Layer0NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Change Map Layer 0
            {
                case 2:
                    Mapv2.ActiveLayer0 = (byte)Layer0NUD.Value;
                    break;
                case 1:
                    Mapv3.ActiveLayer0 = (byte)Layer0NUD.Value;
                    break;
                case 0:
                    Mapv4.ActiveLayer0 = (byte)Layer0NUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Layer1NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Change Map Layer 1
            {
                case 2:
                    Mapv2.ActiveLayer1 = (byte)Layer1NUD.Value;
                    break;
                case 1:
                    Mapv3.ActiveLayer1 = (byte)Layer1NUD.Value;
                    break;
                case 0:
                    Mapv4.ActiveLayer1 = (byte)Layer1NUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Layer2NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Change Map Layer 2
            {
                case 2:
                    Mapv2.ActiveLayer2 = (byte)Layer2NUD.Value;
                    break;
                case 1:
                    Mapv3.ActiveLayer2 = (byte)Layer2NUD.Value;
                    break;
                case 0:
                    Mapv4.ActiveLayer2 = (byte)Layer2NUD.Value;
                    break;
                default:
                    break;
            }
        }

        private void Layer3NUD_ValueChanged(object sender, EventArgs e)
        {
            switch (LoadedRSDKver) //Change Map Layer 3
            {
                case 2:
                    Mapv2.ActiveLayer3 = (byte)Layer3NUD.Value;
                    break;
                case 1:
                    Mapv3.ActiveLayer3 = (byte)Layer3NUD.Value;
                    break;
                case 0:
                    Mapv4.ActiveLayer3 = (byte)Layer3NUD.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
